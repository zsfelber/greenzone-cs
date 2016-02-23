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

    public class GreenRmiSerializer
    {
        internal GreenRmiSerializer(GreenRmiManager manager)
        {
            this.manager = manager;
        }

        readonly GreenRmiManager manager;
        public GreenRmiManager Manager
        {
            get
            {
                return manager;
            }
        }

        public void WriteRmiObjects(BinaryWriter w, List<GreenRmiBound> rmiObjects)
        {
            w.Write(rmiObjects.Count);
            foreach (var obj in rmiObjects)
            {
                WriteRmiObject(w, obj);
            }
        }

        public List<GreenRmiObjectBuffer> ReadRmiObjects(BinaryReader r)
        {
            int cnt = r.ReadInt32();
            List<GreenRmiObjectBuffer> result = new List<GreenRmiObjectBuffer>();
            for (int i = 0; i < cnt; i++)
            {
                GreenRmiObjectBuffer obj = ReadRmiObject(r);
                result.Add(obj);
            }
            return result;
        }

        public void WriteRmiObject(BinaryWriter w, GreenRmiBound rmiObject)
        {
            Write(w, rmiObject.RmiId);
            Write(w, rmiObject.RmiChanged);

            ArrayList changedProps = new ArrayList();
            bool[] ch = rmiObject.RmiChanged;
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i])
                {
                    changedProps.Add(rmiObject.RmiGetProperty(i));
                }
                else
                {
                    changedProps.Add(null);
                }
            }
            Write(w, changedProps);
            if (rmiObject.RmiVirgin)
            {
                Write(w, rmiObject.GetType().FullName);
                Write(w, rmiObject.Dependencies);
            }
            else
            {
                w.Write((byte)1);
                w.Write((string)null);
                w.Write((byte)13);
                w.Write(0);
            }
        }

        public GreenRmiObjectBuffer ReadRmiObject(BinaryReader r)
        {
            int rmiId  = (int)Read(r);
            ArrayList changed = (ArrayList)Read(r);
            ArrayList changedProps = (ArrayList)Read(r);
            string newObjTypeName = (string)Read(r);
            ArrayList dependenies = (ArrayList)Read(r);
            GreenRmiObjectBuffer buf = new GreenRmiObjectBuffer(rmiId, changed, changedProps, newObjTypeName, dependenies);
            return buf;
        }

        internal void Write(BinaryWriter w, object obj)
        {
            switch (obj.GetType().FullName)
            {
                case "System.String":
                    w.Write((byte)1);
                    w.Write((string)obj);
                    return;
                case "System.Double":
                    w.Write((byte)3);
                    w.Write((double)obj);
                    return;
                case "System.Float":
                    w.Write((byte)4);
                    w.Write((float)obj);
                    return;
                case "System.Int64":
                    w.Write((byte)5);
                    w.Write((long)obj);
                    return;
                case "System.Int32":
                    w.Write((byte)6);
                    w.Write((int)obj);
                    return;
                case "System.Int16":
                    w.Write((byte)7);
                    w.Write((short)obj);
                    return;
                case "System.UInt64":
                    w.Write((byte)8);
                    w.Write((ulong)obj);
                    return;
                case "System.UInt32":
                    w.Write((byte)9);
                    w.Write((uint)obj);
                    return;
                case "System.UInt16":
                    w.Write((byte)10);
                    w.Write((ushort)obj);
                    return;
                case "System.Byte":
                    w.Write((byte)11);
                    w.Write((byte)obj);
                    return;
                case "System.Char":
                    w.Write((byte)12);
                    w.Write((char)obj);
                    return;
                default:
                    break;
            }

            if (obj is GreenRmiBound)
            {
                w.Write((byte)2);
                w.Write(((GreenRmiBound)obj).RmiId.ToString());
            }
            else if (obj is IEnumerable)
            {
                w.Write((byte)13);
                MemoryStream stream = new MemoryStream();
                BinaryWriter w2 = new BinaryWriter(stream);
                int cnt = 0;
                foreach (var o in (IEnumerable)obj)
                {
                    Write(w2, o);
                    cnt++;
                }
                w.Write(cnt);
                w.Write(stream.ToArray());
            }
            else if (obj is Enum)
            {
                w.Write((byte)14);
                w.Write(Convert.ToInt32(obj));
            }
            else if (obj is DictionaryEntry)
            {
                DictionaryEntry e = (DictionaryEntry)obj;
                w.Write((byte)15);
                Write(w, e.Key);
                Write(w, e.Value);
            }
            else
            {
                throw new NotSupportedException();
            }
        }


        internal object Read(BinaryReader r)
        {
            int typeCode = r.ReadByte();
            switch (typeCode)
            {
                case 1:
                    return r.ReadString();
                case 2:
                    int rmiId = r.ReadInt32();
                    return new UnresolvedRmiObject(rmiId);
                case 3:
                    return r.ReadDouble();
                case 4:
                    return r.ReadSingle();
                case 5:
                    return r.ReadInt64();
                case 6:
                case 14:
                    return r.ReadInt32();
                case 7:
                    return r.ReadInt16();
                case 8:
                    return r.ReadUInt64();
                case 9:
                    return r.ReadUInt32();
                case 10:
                    return r.ReadUInt16();
                case 11:
                    return r.ReadByte();
                case 12:
                    return r.ReadChar();
                case 13:
                    MemoryStream stream = new MemoryStream();
                    int cnt = r.ReadInt32();
                    ArrayList result = new ArrayList();
                    for (int i = 0; i < cnt; i++ )
                    {
                        object obj = Read(r);
                        result.Add(obj);
                    }
                    return result;
                case 15:
                    object key = Read(r);
                    object value = Read(r);
                    return new DictionaryEntry(key, value);
                default:
                    throw new NotSupportedException();
            }
        }
    }


    public class GreenRmiObjectBuffer
    {
        internal GreenRmiObjectBuffer(int rmiId, ArrayList changed, ArrayList changedProps, string newObjectTypeId, ArrayList dependencies)
        {
            this.id = rmiId;
            this.changed = (bool[])changed.ToArray(typeof(bool));
            this.changedProps = changedProps;
            this.newObjectTypeId = newObjectTypeId;
            this.dependencies = dependencies;
        }

        readonly int id;
        public int Id
        {
            get
            {
                return id;
            }
        }

        readonly bool[] changed;
        public bool[] Changed
        {
            get
            {
                return changed;
            }
        }

        readonly ArrayList changedProps;
        public ArrayList ChangedProps
        {
            get
            {
                return changedProps;
            }
        }

        readonly string newObjectTypeId;
        public string NewObjectTypeId
        {
            get
            {
                return newObjectTypeId;
            }
        }

        readonly ArrayList dependencies;
        public ArrayList Dependencies
        {
            get
            {
                return dependencies;
            }
        }

        readonly List<UnresolvedRmiObject> resolving = new List<UnresolvedRmiObject>();
        public List<UnresolvedRmiObject> Resolving
        {
            get
            {
                return resolving;
            }
        }

        bool resolved;
        public bool Resolved
        {
            get
            {
                return resolved;
            }
            set
            {
                resolved = value;
            }
        }

    }

    public class UnresolvedRmiObject
    {
        internal UnresolvedRmiObject(int rmiId)
        {
            this.id = rmiId;
        }

        readonly int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
    }

}
