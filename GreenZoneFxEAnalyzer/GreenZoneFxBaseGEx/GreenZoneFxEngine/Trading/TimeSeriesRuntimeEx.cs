using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{

    public abstract class TimeSeriesRuntimeEx : TimeSeriesRuntimeBase
    {
        public TimeSeriesRuntimeEx(GreenRmiManager rmiManager)
            : base(rmiManager)
        {
        }

        public TimeSeriesRuntimeEx(GreenRmiObjectBuffer buffer)
            : base(rmiManager, buffer)
        {
        }

        protected TimeSeriesRuntimeEx(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

        // throws TimeSeriesNotLoadedException
        public override void Load(datetime focusedTime)
        {
            if (Online)
            {
                // TODO generated at serialization
            }
            else
            {
                if (!InputFileIsRead)
                {
                    LoadFile(focusedTime);
                }
                else
                {
                    int i = Begin_time.BinarySearch((long)focusedTime, -1);
                    if (i < 0)
                    {
                        i = ~i;// NOTE
                        //        it's the greatest lower item than focusedTime, because of arrayOrder:-1
                        //        ~i - 1  would be with arrayOrder:1
                    }
                    find(i, focusedTime);
                }
            }
        }

        public override void LoadForward(datetime focusedTime)
        {
            if (Online)
            {
                // TODO generated at serialization
            }
            else
            {
                if (!InputFileIsRead)
                {
                    throw new NotSupportedException();
                }

                int i;
                //NOTE reverse indexing
                for (i = IndOffset - 1; i >= 0; i--)
                {
                    //FIXME  round 1:ok
                    if (Begin_time[i] > (long)focusedTime)
                    {
                        i++;
                        break;
                    }
                }

                find(i, focusedTime);
            }
        }

        public override void LoadForward(int offset)
        {
            if (Online)
            {
                // TODO generated at serialization
            }
            else
            {
                if (!InputFileIsRead)
                {
                    throw new NotSupportedException();
                }

                //NOTE reverse indexing
                int i = IndOffset - offset;

                find(i);
            }
        }

        public override void LoadAtTotal(long total_ind)
        {
            if (Online)
            {
                // TODO ?
            }
            else
            {
                if (!InputFileIsRead)
                {
                    throw new NotSupportedException();
                }

                //NOTE reverse indexing
                findTot(total_ind);
            }
        }

        void find(int index)
        {
            //NOTE reverse indexing
            long tot_ind = FileOffsetTo - index;

            find(index, tot_ind);
        }

        void findTot(long tot_ind)
        {
            //NOTE reverse indexing
            int index = (int)(FileOffsetTo - tot_ind);

            find(index, tot_ind);
        }

        void find(int index, long tot_ind)
        {
            int bufSz = Parent.Parent.ProgramOptions.BufferSize;
            int bufSz4 = bufSz / 4;

            if (tot_ind < 0 || tot_ind >= RecordCount)
            {
                throw new TimeSeriesEOFException();
            }
            //NOTE reverse indexing
            else if ((index < bufSz4 && FileOffsetTo < RecordCount - 1) || (index >= IndCount - bufSz4 && FileOffset > 0))
            {
                FillFromFile(tot_ind);
            }
            else
            {
                IndOffset = index;
            }
        }

        void find(int index, datetime focusedTime)
        {
            int bufSz = Parent.Parent.ProgramOptions.BufferSize;
            int bufSz4 = bufSz / 4;

            //NOTE reverse indexing
            long tot_ind = FileOffsetTo - index;

            if (tot_ind < 0 || tot_ind >= RecordCount)
            {
                throw new TimeSeriesEOFException();
            }
            //NOTE reverse indexing
            else if ((index < bufSz4 && FileOffsetTo < RecordCount - 1) || (index >= IndCount - bufSz4 && FileOffset > 0))
            {
                FillFromFile(focusedTime);
            }
            else
            {
                IndOffset = index;
            }
        }

        // Server
        protected abstract void LoadFile(datetime focusedTime);
        protected abstract void LoadFile();
        protected abstract void FillFromFile(datetime focusedTime);
        protected abstract void FillFromFile(long tot_ind);

    }

}