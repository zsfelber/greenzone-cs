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
using GreenZoneUtil.Util;
using System.Reflection;
using System.Runtime.Serialization;
using GreenZoneParser.Reflect;

namespace GreenZoneUtil.GreenRmi
{
    public enum GreenRmiSide
    {
        Server,
        Client
    }

    public enum GreenRmiFieldType
    {
        Normal,
        Readonly,
        Abstract,
        Simple,
        New,
        Virtual
    }

    public enum GreenRmiMethodType
    {
        Normal,
        Simple,
        Rmi,
    }

    public static class GreenRmiEx
    {
        public static GreenRmiSide CounterSide(this GreenRmiSide side)
        {
            switch (side)
            {
                case GreenRmiSide.Server: return GreenRmiSide.Client;
                case GreenRmiSide.Client: return GreenRmiSide.Server;
                default: throw new NotSupportedException();
            }
        }
    }

    [AttributeUsage(AttributeTargets.Interface)]
    public class GreenRmiAttribute : Attribute
    {
        string baseClass;
        public string BaseClass
        {
            get
            {
                return baseClass;
            }
            set
            {
                baseClass = value;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple=true)]
    public class GreenRmiInsertBodyAttribute : Attribute
    {
        public GreenRmiInsertBodyAttribute(string definition)
        {
            this.definition = definition;
        }

        readonly string definition;
        public string Definition
        {
            get
            {
                return definition;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class GreenRmiFieldAttribute : Attribute
    {
        public GreenRmiFieldAttribute(GreenRmiFieldType type)
        {
            this.type = type;
        }

        public GreenRmiFieldAttribute(GreenRmiFieldType type, string definition)
        {
            if (type != GreenRmiFieldType.Simple)
            {
                throw new ArgumentException("type:"+type+" should be Simple if definition argument is passed");
            }
            this.type = type;
            this.definition = definition;
        }

        public GreenRmiFieldAttribute(GreenRmiFieldType type, Type parentType)
        {
            if (type != GreenRmiFieldType.New)
            {
                throw new ArgumentException("type:" + type + " should be New if parentType argument is passed");
            }
            this.type = type;
            this.parentType = parentType;
        }

        readonly GreenRmiFieldType type;
        public GreenRmiFieldType Type
        {
            get
            {
                return type;
            }
        }

        readonly string definition;
        public string Definition
        {
            get
            {
                return definition;
            }
        }

        readonly Type parentType;
        public Type ParentType
        {
            get
            {
                return parentType;
            }
        }

        string modifiers;
        public string Modifiers
        {
            get
            {
                return modifiers;
            }
            set
            {
                modifiers = value;
            }
        }

        string fieldModifiers;
        public string FieldModifiers
        {
            get
            {
                return fieldModifiers;
            }
            set
            {
                fieldModifiers = value;
            }
        }

        string fieldInitialization;
        public string FieldInitialization
        {
            get
            {
                return fieldInitialization;
            }
            set
            {
                fieldInitialization = value;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class GreenRmiMethodAttribute : Attribute
    {
        public GreenRmiMethodAttribute(GreenRmiMethodType type)
        {
            if (type == GreenRmiMethodType.Simple)
            {
                throw new ArgumentException("type:" + type + " should not be Simple if definition argument is omitted");
            }
            this.type = type;
        }

        public GreenRmiMethodAttribute(GreenRmiMethodType type, string definition)
        {
            if (type != GreenRmiMethodType.Simple)
            {
                throw new ArgumentException("type:" + type + " should be Simple if definition argument is passed");
            }
            this.type = type;
            this.definition = definition;
        }

        readonly GreenRmiMethodType type;
        public GreenRmiMethodType Type
        {
            get
            {
                return type;
            }
        }

        readonly string definition;
        public string Definition
        {
            get
            {
                return definition;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class GreenRmiNonSerialAttribute : Attribute
    {
        public GreenRmiNonSerialAttribute()
        {
        }
    }

    public interface GreenRmiBound : ISerializable
    {
        GreenRmiManager RmiManager
        {
            get;
        }

        List<GreenRmiBound> Dependencies
        {
            get;
        }

        
        int RmiId
        {
            get;
            set;
        }
        bool[] RmiChanged
        {
            get;
        }
        bool RmiSomethingChanged
        {
            get;
            set;
        }
        bool RmiVirgin
        {
            get;
            set;
        }

        object RmiGetProperty(int propertyId);

        void RmiSetProperty(int propertyId, object value);

        object RmiInvoke(int methodId, object[] args);
    }



    public class GreenRmiManager
    {
        readonly Dictionary<int, GreenRmiBound> rmiObjects = new Dictionary<int, GreenRmiBound>();
        readonly Dictionary<int, GreenRmiObjectBuffer> lastReceivedBuffers = new Dictionary<int, GreenRmiObjectBuffer>();

        int curId = 1000;
        readonly SortedSet<int> idHoles = new SortedSet<int>();

        public GreenRmiManager(GreenRmiNetowrkLayer networkLayer, GreenRmiSide side)
        {
            this.networkLayer = networkLayer;
            this.side = side;
        }

        readonly GreenRmiSide side;
        public GreenRmiSide Side
        {
            get
            {
                return side;
            }
        }

        readonly GreenRmiNetowrkLayer networkLayer;
        public GreenRmiNetowrkLayer NetworkLayer
        {
            get
            {
                return networkLayer;
            }
        }

        // TODO fill
        Resolver resolver;
        public Resolver Resolver
        {
            get
            {
                return resolver;
            }
        }

 
        public void Register(GreenRmiBound rmiObject)
        {
            int id;
            if (idHoles.Count > 0)
            {
                id = idHoles.ElementAt(0);
                idHoles.Remove(id);
            }
            else
            {
                id = curId++;
            }
            rmiObject.RmiId = id;
            rmiObjects[id] = rmiObject;
        }

        public void Unregister(GreenRmiBound rmiObject)
        {
            int id = rmiObject.RmiId;
            rmiObjects.Remove(id);
            idHoles.Add(id);
        }

        public object InvokeMethodFar(GreenRmiBound rmiObject, int methodId, object[] args, bool sync = true)
        {
            object result = networkLayer.InvokeMethodFar(rmiObject, methodId, args, sync);
            if (result is UnresolvedRmiObject)
            {
                result = Resolve(((UnresolvedRmiObject)result).Id);
            }
            return result;
        }

        internal object InvokeMethodHere(int objId, int methodId, object[] args)
        {
            GreenRmiBound rmiObject = rmiObjects[objId];
            object result = rmiObject.RmiInvoke(methodId, args);
            return result;
        }

        internal void SendAll()
        {
            List<GreenRmiBound> result = new List<GreenRmiBound>();

            try
            {
                foreach (var rmiObject in rmiObjects.Values)
                {
                    if (rmiObject.RmiSomethingChanged)
                    {
                        bool reallyChanged;

                        if (lastReceivedBuffers.ContainsKey(rmiObject.RmiId))
                        {
                            GreenRmiObjectBuffer lastRecBuf = lastReceivedBuffers[rmiObject.RmiId];

                            reallyChanged = false;
                            for (int i = 0; i < rmiObject.RmiChanged.Length; i++)
                            {
                                if (rmiObject.RmiChanged[i])
                                {
                                    if (object.Equals(rmiObject.RmiGetProperty(i), lastRecBuf.ChangedProps[i]))
                                    {
                                        rmiObject.RmiChanged[i] = false;
                                    }
                                    else
                                    {
                                        reallyChanged = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            reallyChanged = true;
                        }

                        if (reallyChanged)
                        {
                            result.Add(rmiObject);
                        }
                    }
                }

                networkLayer.WriteRmiObjects(result);

            }
            finally
            {
                lastReceivedBuffers.Clear();

                foreach (var rmiObject in result)
                {
                    bool[] ch = rmiObject.RmiChanged;
                    for (int i = 0; i < ch.Length; i++)
                    {
                        ch[i] = false;
                    }
                    rmiObject.RmiVirgin = false;
                    rmiObject.RmiSomethingChanged = false;
                }
            }
        }

        internal void ReceiveAll()
        {
            List<GreenRmiObjectBuffer> buffers = networkLayer.ReadRmiObjects();
            foreach (var buffer in buffers)
            {
                lastReceivedBuffers[buffer.Id] = buffer;
            }
            // Pass 1
            foreach (var buffer in buffers)
            {
                Resolve(buffer);
            }
            // Pass 2 (required)
            foreach (var buffer in buffers)
            {
                Resolve(buffer);
                if (!buffer.Resolved)
                {
                    throw new NotSupportedException("Unresolved object  id:"+buffer.Id+"  type:"+buffer.NewObjectTypeId);
                }
            }
        }


        GreenRmiBound Resolve(int rmiId)
        {
            if (lastReceivedBuffers.ContainsKey(rmiId))
            {
                GreenRmiObjectBuffer buffer = lastReceivedBuffers[rmiId];
                GreenRmiBound rmiObject = Resolve(buffer);
                return rmiObject;
            }
            else if (rmiObjects.ContainsKey(rmiId))
            {
                GreenRmiBound rmiObject = rmiObjects[rmiId];
                return rmiObject;
            }
            else
            {
                throw new ArgumentException("rmiId: " + rmiId);
            }
        }

        GreenRmiBound Resolve(GreenRmiObjectBuffer buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (buffer.Resolved)
            {
                if (rmiObjects.ContainsKey(buffer.Id))
                {
                    GreenRmiBound rmiObject = rmiObjects[buffer.Id];
                    return rmiObject;
                }
                else
                {
                    throw new NotSupportedException("Buffer is resolved but does not appear in rmiObjects");
                }
            }
            else
            {

                bool resolved = false;

                try
                {
                    for (int i = 0; i < buffer.Dependencies.Count; i++)
                    {
                        var v = buffer.Dependencies[i];
                        if (v is UnresolvedRmiObject)
                        {
                            if (buffer.Resolving.Contains(v))
                            {
                                throw new NotSupportedException("Circular dependency : " + buffer.Resolving);
                            }
                            try
                            {
                                buffer.Resolving.Add((UnresolvedRmiObject)v);

                                v = Resolve(((UnresolvedRmiObject)v).Id);
                                buffer.Dependencies[i] = v;
                            }
                            finally
                            {
                                buffer.Resolving.Remove((UnresolvedRmiObject)v);
                            }
                        }
                    }

                    for (int i = 0; i < buffer.Changed.Length; i++)
                    {
                        if (buffer.Changed[i])
                        {
                            object v = buffer.ChangedProps[i];
                            if (v is UnresolvedRmiObject && rmiObjects.ContainsKey(((UnresolvedRmiObject)v).Id))
                            {
                                buffer.ChangedProps[i] = rmiObjects[((UnresolvedRmiObject)v).Id];
                            }
                        }
                    }

                    GreenRmiBound rmiObject;

                    if (rmiObjects.ContainsKey(buffer.Id))
                    {
                        rmiObject = rmiObjects[buffer.Id];
                    }
                    else
                    {
                        if (buffer.NewObjectTypeId == null)
                        {
                            throw new NotSupportedException();
                        }

                        Type classType = Type.GetType(buffer.NewObjectTypeId);
                        ConstructorInfo c = classType.GetConstructor(new Type[] { typeof(GreenRmiManager), typeof(GreenRmiObjectBuffer) });
                        rmiObject = (GreenRmiBound)c.Invoke(new object[] { this, buffer });
                        rmiObjects[buffer.Id] = rmiObject;
                    }

                    if (rmiObject == null)
                    {
                        throw new NotSupportedException();
                    }

                    bool allPropertiesResolved = true;
                    for (int i = 0; i < buffer.Changed.Length; i++)
                    {
                        if (buffer.Changed[i])
                        {
                            object v = buffer.ChangedProps[i];
                            if (v is UnresolvedRmiObject)
                            {
                                allPropertiesResolved = false;
                            }
                            else
                            {
                                rmiObject.RmiSetProperty(i, v);
                            }
                        }
                    }

                    resolved = allPropertiesResolved;

                    return rmiObject;
                }
                finally
                {
                    buffer.Resolved = resolved;
                }
            }
        }
    }


}
