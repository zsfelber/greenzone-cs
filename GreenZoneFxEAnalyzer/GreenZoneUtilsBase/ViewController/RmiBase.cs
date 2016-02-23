using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.Util;
using System.Runtime.Serialization;

namespace GreenZoneUtil.ViewController
{
    public interface IRmiBase : GreenRmiBound
    {
    }

    public class RmiBase : IRmiBase
    {

        public RmiBase(GreenRmiManager rmiManager)
        {
            changed = new bool[100];
            for (int i = 0; i < 100; i++) changed[i] = true;

            this.rmiManager = rmiManager;
        }

        public RmiBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
            : this(rmiManager)
        {
        }

        protected RmiBase(SerializationInfo info, StreamingContext context)
        {
            this.rmiManager = (GreenRmiManager)context.Context;
        }



        protected readonly GreenRmiManager rmiManager;
        public GreenRmiManager RmiManager
        {
            get { return rmiManager; }
        }


        protected readonly List<GreenRmiBound> dependencies = new List<GreenRmiBound>();
        public List<GreenRmiBound> Dependencies
        {
            get { return dependencies; }
        }

        int rmiId;
        public int RmiId
        {
            get
            {
                return rmiId;
            }
            set
            {
                rmiId = value;
            }
        }

        protected readonly bool[] changed;
        public bool[] RmiChanged
        {
            get { return changed; }
        }

        protected bool somethingChanged = true;
        public bool RmiSomethingChanged
        {
            get
            {
                return somethingChanged;
            }
            set
            {
                somethingChanged = value;
            }
        }

        protected bool virgin = true;
        public bool RmiVirgin
        {
            get
            {
                return virgin;
            }
            set
            {
                virgin = value;
            }
        }

        public virtual object RmiGetProperty(int propertyId)
        {
            switch (propertyId)
            {
                default:
                    throw new NotSupportedException();
            }
        }

        public virtual void RmiSetProperty(int propertyId, object value)
        {
            switch (propertyId)
            {
                default:
                    throw new NotSupportedException();
            }
        }

        public virtual object RmiInvoke(int methodId, object[] args)
        {
            switch (methodId)
            {
                default:
                    throw new NotSupportedException();
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }

}
