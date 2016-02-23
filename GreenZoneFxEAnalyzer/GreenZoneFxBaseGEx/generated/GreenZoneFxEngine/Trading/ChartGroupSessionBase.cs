using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ChartGroupSessionProps
	{
		public const int PROPERTY_1_CURSORPOSITION_ID = 1;
		public const int PROPERTY_2_CHARTSESSIONS_ID = 2;
		public static bool RmiGetProperty(IChartGroupSession controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartGroupSessionProps.PROPERTY_1_CURSORPOSITION_ID:
					value = controller.CursorPosition;
					return true;
				case ChartGroupSessionProps.PROPERTY_2_CHARTSESSIONS_ID:
					value = controller.ChartSessions;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IChartGroupSession controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case ChartGroupSessionProps.PROPERTY_1_CURSORPOSITION_ID:
					controller.CursorPosition = (Int32) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IChartGroupSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			controller.ChartSessions = (List<IChartSession>) buffer.ChangedProps[ChartGroupSessionProps.PROPERTY_2_CHARTSESSIONS_ID];
		}

		public static void AddDependencies(IChartGroupSession controller, bool goToParent)
		{
			controller.Dependencies.AddRange(controller.ChartSessions);
		}

		public static void SerializationRead(IChartGroupSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.CursorPosition = (Int32) info.GetValue("CursorPosition", typeof(Int32));
			controller.ChartSessions = (List<IChartSession>) info.GetValue("ChartSessions", typeof(List<IChartSession>));
		}

		public static void SerializationWrite(IChartGroupSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("CursorPosition", controller.CursorPosition);
			info.AddValue("ChartSessions", controller.ChartSessions);
		}

	}
	public abstract class ChartGroupSessionBase : RmiBase, IChartGroupSession
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IChartGroupSession_CursorPosition_Changed;

		public ChartGroupSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ChartGroupSessionProps.AddDependencies(this, false);
		}

		public ChartGroupSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ChartGroupSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			ChartGroupSessionProps.AddDependencies(this, false);
		}

		protected ChartGroupSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ChartGroupSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ChartGroupSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ChartGroupSessionProps.SerializationWrite(this, info, context, false);
		}

		public abstract void AddChart(IChartSession chartSession);

		public abstract void RemoveChart(IChartSession chart);


		Int32 _IChartGroupSession_CursorPosition;
		public Int32 CursorPosition
		{
			get {
				return _IChartGroupSession_CursorPosition;
			}
			set {
				if (_IChartGroupSession_CursorPosition != value) {
					_IChartGroupSession_CursorPosition= value;
					changed[ChartGroupSessionProps.PROPERTY_1_CURSORPOSITION_ID] = true;
					if (IChartGroupSession_CursorPosition_Changed != null)
						IChartGroupSession_CursorPosition_Changed(this, new PropertyChangedEventArgs("CursorPosition", value));
				}
			}
		}

		List<IChartSession> _IChartGroupSession_ChartSessions;
		public List<IChartSession> ChartSessions
		{
			get {
				return _IChartGroupSession_ChartSessions;
			}
			set {
				if (!___initialized) {
					_IChartGroupSession_ChartSessions= value;
					changed[ChartGroupSessionProps.PROPERTY_2_CHARTSESSIONS_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ChartGroupSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ChartGroupSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
