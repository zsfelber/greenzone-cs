using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace Submarine.Base
{
    public sealed class SslTcpServer
    {
        public class Session
        {
            internal Session(X509Certificate serverCertificate, TcpListener listener)
            {
                this.ServerCertificate = serverCertificate;
                this.Listener = listener;
            }

            public X509Certificate ServerCertificate { get; set; }

            public TcpListener Listener { get; set; }
        }

        public class Connection
        {
            public Connection(Session session)
            {
                this.Session = session;
                Accept();
            }

            public Session Session { get; set; }

            public TcpClient Client { get; set; }

            public SslStream SslStream { get; set; }


            private void Accept()
            {
                Client = Session.Listener.AcceptTcpClient();
                // A client has connected. Create the 
                // SslStream using the client's network stream.
                SslStream = new SslStream(Client.GetStream(), false);

                // Authenticate the server but don't require the client to authenticate.
                try
                {
                    SslStream.AuthenticateAsServer(Session.ServerCertificate, false, SslProtocols.Tls, true);
#if (DEBUG)
                    // Display the properties and settings for the authenticated stream.
                    SslBase.DisplaySecurityLevel(SslStream);
                    SslBase.DisplaySecurityServices(SslStream);
                    SslBase.DisplayCertificateInformation(SslStream);
                    SslBase.DisplayStreamProperties(SslStream);
#endif
                    // Set timeouts for the read and write to 5 seconds.
                    SslStream.ReadTimeout = 5000;
                    SslStream.WriteTimeout = 5000;
                }
                catch (AuthenticationException e)
                {
                    Console.WriteLine("Exception: {0}", e.Message);
                    if (e.InnerException != null)
                    {
                        Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                    }
                    Console.WriteLine("Authentication failed - closing the connection.");
                    Close();
                }
            }

            public void Close()
            {
                // The client stream will be closed with the sslStream
                // because we specified this behavior when creating
                // the sslStream.
                if (SslStream != null)
                {
                    SslStream.Close();
                }
                if (Client != null)
                {
                    Client.Close();
                }
                SslStream = null;
                Client = null;
            }
        }

        // The certificate parameter specifies the name of the file 
        // containing the machine certificate.
        public static Session RunServer(string certificate, int port)
        {
            X509Certificate serverCertificate = X509Certificate.CreateFromCertFile(certificate);
            // Create a TCP/IP (IPv4) socket and listen for incoming connections.
            TcpListener listener = new TcpListener(IPAddress.Any, port);//8080
            listener.Start();

            return new Session(serverCertificate, listener);
        }
    }
}