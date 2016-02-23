using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GreenZoneUtil.ViewController;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace GreenZoneUtil.GreenRmi
{

    public class GreenRmiNetowrkLayer
    {

        Socket socket;
        NetworkStream conStream;
        BinaryReader r;
        BinaryWriter w;

        object threadLock = new object();

        bool receiveReturnAsync = false;

        GreenRmiBound currentFarMethodObj;
        int currentFarMethodId;


        public GreenRmiNetowrkLayer(GreenRmiSide side)
        {
            this.side = side;
            this.manager = new GreenRmiManager(this, side);
            this.serializer = new GreenRmiSerializer(this.manager);
        }

        readonly GreenRmiSide side;
        public GreenRmiSide Side
        {
            get
            {
                return side;
            }
        }

        readonly GreenRmiManager manager;
        public GreenRmiManager Manager
        {
            get
            {
                return manager;
            }
        }

        readonly GreenRmiSerializer serializer;
        public GreenRmiSerializer Serializer
        {
            get
            {
                return serializer;
            }
        }

        public void Listen(Socket socket)
        {
            this.socket = socket;
            this.conStream = new NetworkStream(socket);
            this.conStream.ReadTimeout = 10;
            r = new BinaryReader(conStream);
            w = new BinaryWriter(conStream);

            lock (threadLock)
            {
                while (true)
                {
                    int objId = r.ReadInt32();

                    // See **
                    if (objId == 0)
                    {
                        Monitor.Wait(threadLock);
                        if (receiveReturnAsync)
                        {
                            receiveReturn();
                        }
                    }
                    else
                    {
                        InvokeMethodHere(objId);
                    }
                }
            }
        }

        object receiveReturn()
        {
            lock (threadLock)
            {
                try
                {
                    object result = serializer.Read(r);
                    if (receiveReturnAsync && result != null)
                    {
                        Console.WriteLine("WARNING receiveReturn()   ASYNC  but return value is not null   result:" + result + "  currentFarMethodObj:" + currentFarMethodObj + " currentFarMethodId:" + currentFarMethodId);
                    }
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR receiveReturn()   currentFarMethodObj:"+currentFarMethodObj+" currentFarMethodId:"+currentFarMethodId+"  error:"+ e.Message);
                    return null;
                }
                finally
                {
                    receiveReturnAsync = false;
                    Monitor.Pulse(threadLock);
                }
            }
        }

        void willReceiveReturnAsync()
        {
            receiveReturnAsync = true;
            lock (threadLock)
            {
                Monitor.Pulse(threadLock);
            }
        }

        public object InvokeMethodFar(GreenRmiBound rmiObject, int methodId, object[] args, bool sync)
        {
            currentFarMethodObj = rmiObject;
            currentFarMethodId = methodId;

            w.Write(rmiObject.RmiId);
            w.Write(methodId);
            w.Write(sync);
            serializer.Write(w, args);
            manager.SendAll();

            if (sync)
            {
                manager.ReceiveAll();
                object result = receiveReturn();
                return result;
            }
            else
            {
                willReceiveReturnAsync();
                return null;
            }
        }

        void InvokeMethodHere(int objId)
        {
            int methodId = r.ReadInt32();
            bool sync = r.ReadBoolean();
            ArrayList _args = (ArrayList)serializer.Read(r);
            object[] args = _args.ToArray();
            manager.ReceiveAll();

            object result;
            try
            {
                result = manager.InvokeMethodHere(objId, methodId, args);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR InvokeMethodHere(" + objId + ") " + e.Message);
                result = null;
            }

            if (sync)
            {
                manager.SendAll();
            }

            // See **
            w.Write(0);

            serializer.Write(w, result);
        }

        public void WriteRmiObjects(List<GreenRmiBound> rmiObjects)
        {
            serializer.WriteRmiObjects(w, rmiObjects);
        }

        public List<GreenRmiObjectBuffer> ReadRmiObjects()
        {
            List<GreenRmiObjectBuffer> result = serializer.ReadRmiObjects(r);
            return result;
        }
    }

    public class GreenRmiNetowrkLayerServer
    {
        int port;

        TcpListener server;

        public static EventHandler ClientConnected;

        public GreenRmiNetowrkLayerServer(int port)
        {
            this.port = port;
            StartListening();
        }

        void StartListening()
        {
            bool shouldSleep = true;

            try
            {
                Console.WriteLine("GreenRmiNetowrkManagerServer Listening port:" + port);
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                shouldSleep = false;

                while (true)
                {
                    Socket incomingConnection = server.AcceptSocket();
                    Thread listenThread = new Thread(new ParameterizedThreadStart(ListenThread));
                    listenThread.IsBackground = true;
                    listenThread.Start(incomingConnection);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Server has crashed. error: " + e.Message);
            }

            if (shouldSleep)
            {
                Console.WriteLine("Waiting 10 sec...");
                Thread.Sleep(10000);
            }

            Console.WriteLine("Restarting...");
            StartListening();
        }

        public void ListenThread(object _incomingConnection)
        {
            Socket incomingConnection = (Socket)_incomingConnection;
            try
            {
                GreenRmiNetowrkLayer layer = new GreenRmiNetowrkLayer(GreenRmiSide.Server);
                Console.WriteLine("GreenRmiNetowrkManagerServer Client connected:" + incomingConnection.RemoteEndPoint + "   to server here:" + incomingConnection.LocalEndPoint);

                ClientConnected(layer, EventArgs.Empty);

                // NOTE blocking infinite loop
                layer.Listen(incomingConnection);
            }
            catch (Exception e)
            {
                Console.WriteLine("Client has crashed :" + incomingConnection.RemoteEndPoint + "   to server here:" + incomingConnection.LocalEndPoint+"  error: " + e.Message);
            }
            finally
            {
                try
                {
                    incomingConnection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot close server socket : " + e.Message);
                }
            }

            Console.WriteLine("Client finished. " + incomingConnection.RemoteEndPoint + "   server here:" + incomingConnection.LocalEndPoint );
        }
    }

    public class GreenRmiNetowrkLayerClient
    {
        string address;
        int port;

        TcpClient client;
        Thread listenThread;

        public GreenRmiNetowrkLayerClient(string address, int port)
        {
            this.address = address;
            this.port = port;
            this.layer = new GreenRmiNetowrkLayer(GreenRmiSide.Client);
            StartListening();
        }

        readonly GreenRmiNetowrkLayer layer;
        public GreenRmiNetowrkLayer Layer
        {
            get
            {
                return layer;
            }
        }

        void StartListening()
        {
            listenThread = new Thread(new ThreadStart(ListenThread));
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        public void ListenThread()
        {
            bool shouldSleep = true;
            try
            {
                client = new TcpClient(address, port);
                Console.WriteLine("GreenRmiNetowrkManagerServer Connected to server:" + client.Client.RemoteEndPoint+"  from here:"+client.Client.LocalEndPoint);
                shouldSleep = false;

                // NOTE blocking infinite loop
                layer.Listen(client.Client);
            }
            catch (Exception e)
            {
                Console.WriteLine("Client has crashed. error: " + e.Message);
            }
            finally
            {
                try
                {
                    client.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot close client : "+e.Message);
                }
            }

            if (shouldSleep)
            {
                Console.WriteLine("Waiting 10 sec...");
                Thread.Sleep(10000);
            }

            Console.WriteLine("Restarting...");
            StartListening();
        }
    }
}
