using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class IndicatorSessionProps
	{
		public const int PROPERTY_5_BUFFERS_ID = 5;
		public const int PROPERTY_6_LEVELS_ID = 6;
		public const int PROPERTY_7_SHORTNAME_ID = 7;
		public const int PROPERTY_8_WINDOWTYPE_ID = 8;
		public const int PROPERTY_9_INDICATORMINIMUM_ID = 9;
		public const int PROPERTY_10_INDICATORMAXIMUM_ID = 10;
		public const int PROPERTY_11_INDICATORDIGITS_ID = 11;
		public const int PROPERTY_12_DISPLAYSCALE_ID = 12;
		public const int PROPERTY_13_ENABLESCROLL_ID = 13;
		public static bool RmiGetProperty(IIndicatorSession controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_5_BUFFERS_ID:
					value = controller.Buffers;
					return true;
				case PROPERTY_6_LEVELS_ID:
					value = controller.Levels;
					return true;
				case PROPERTY_7_SHORTNAME_ID:
					value = controller.ShortName;
					return true;
				case PROPERTY_8_WINDOWTYPE_ID:
					value = controller.WindowType;
					return true;
				case PROPERTY_9_INDICATORMINIMUM_ID:
					value = controller.IndicatorMinimum;
					return true;
				case PROPERTY_10_INDICATORMAXIMUM_ID:
					value = controller.IndicatorMaximum;
					return true;
				case PROPERTY_11_INDICATORDIGITS_ID:
					value = controller.IndicatorDigits;
					return true;
				case PROPERTY_12_DISPLAYSCALE_ID:
					value = controller.DisplayScale;
					return true;
				case PROPERTY_13_ENABLESCROLL_ID:
					value = controller.EnableScroll;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorSession controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_5_BUFFERS_ID:
					controller.Buffers = (List<Dictionary<String,Object>>) value;
					return true;
				case PROPERTY_6_LEVELS_ID:
					controller.Levels = (List<Dictionary<String,Object>>) value;
					return true;
				case PROPERTY_9_INDICATORMINIMUM_ID:
					controller.IndicatorMinimum = (Double) value;
					return true;
				case PROPERTY_10_INDICATORMAXIMUM_ID:
					controller.IndicatorMaximum = (Double) value;
					return true;
				case PROPERTY_11_INDICATORDIGITS_ID:
					controller.IndicatorDigits = (Int32) value;
					return true;
				case PROPERTY_12_DISPLAYSCALE_ID:
					controller.DisplayScale = (Int32) value;
					return true;
				case PROPERTY_13_ENABLESCROLL_ID:
					controller.EnableScroll = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorSession controller, GreenRmiObjectBuffer buffer)
		{
			controller.ShortName = (String) buffer.ChangedProps[IndicatorSessionProps.PROPERTY_7_SHORTNAME_ID];
			controller.WindowType = (IndicatorWindowType) buffer.ChangedProps[IndicatorSessionProps.PROPERTY_8_WINDOWTYPE_ID];
		}

		public static void AddDependencies(IIndicatorSession controller)
		{
		}

		public static void SerializationRead(IIndicatorSession controller, SerializationInfo info, StreamingContext context)
		{
			controller.Buffers = (List<Dictionary<String,Object>>) info.GetValue("Buffers", typeof(List<Dictionary<String,Object>>));
			controller.Levels = (List<Dictionary<String,Object>>) info.GetValue("Levels", typeof(List<Dictionary<String,Object>>));
			controller.ShortName = (String) info.GetValue("ShortName", typeof(String));
			controller.WindowType = (IndicatorWindowType) info.GetValue("WindowType", typeof(IndicatorWindowType));
			controller.IndicatorMinimum = (Double) info.GetValue("IndicatorMinimum", typeof(Double));
			controller.IndicatorMaximum = (Double) info.GetValue("IndicatorMaximum", typeof(Double));
			controller.IndicatorDigits = (Int32) info.GetValue("IndicatorDigits", typeof(Int32));
			controller.DisplayScale = (Int32) info.GetValue("DisplayScale", typeof(Int32));
			controller.EnableScroll = (Boolean) info.GetValue("EnableScroll", typeof(Boolean));
		}

		public static void SerializationWrite(IIndicatorSession controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Buffers", controller.Buffers);
			info.AddValue("Levels", controller.Levels);
			info.AddValue("ShortName", controller.ShortName);
			info.AddValue("WindowType", controller.WindowType);
			info.AddValue("IndicatorMinimum", controller.IndicatorMinimum);
			info.AddValue("IndicatorMaximum", controller.IndicatorMaximum);
			info.AddValue("IndicatorDigits", controller.IndicatorDigits);
			info.AddValue("DisplayScale", controller.DisplayScale);
			info.AddValue("EnableScroll", controller.EnableScroll);
		}

	}
	public abstract class IndicatorSessionBase : ExecSessionBase, IIndicatorSession
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler BuffersChanged;
		public event PropertyChangedEventHandler LevelsChanged;
		public event PropertyChangedEventHandler IndicatorMinimumChanged;
		public event PropertyChangedEventHandler IndicatorMaximumChanged;
		public event PropertyChangedEventHandler IndicatorDigitsChanged;
		public event PropertyChangedEventHandler DisplayScaleChanged;
		public event PropertyChangedEventHandler EnableScrollChanged;

		public IndicatorSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			IndicatorSessionProps.AddDependencies(this);
		}

		public IndicatorSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorSessionProps.Initialize(this, buffer);
			___initialized = true;
			IndicatorSessionProps.AddDependencies(this);
		}

		protected IndicatorSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorSessionProps.SerializationRead(this, info, context);
			___initialized = true;
			IndicatorSessionProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorSessionProps.SerializationWrite(this, info, context);
		}

		List<Dictionary<String,Object>> buffers;
		public List<Dictionary<String,Object>> Buffers
		{
			get {
				return buffers;
			}
			set {
				if (buffers != value) {
					buffers= value;
					changed[IndicatorSessionProps.PROPERTY_5_BUFFERS_ID] = true;
					if (BuffersChanged != null)
						BuffersChanged(this, new PropertyChangedEventArgs("Buffers", value));
				}
			}
		}

		List<Dictionary<String,Object>> levels;
		public List<Dictionary<String,Object>> Levels
		{
			get {
				return levels;
			}
			set {
				if (levels != value) {
					levels= value;
					changed[IndicatorSessionProps.PROPERTY_6_LEVELS_ID] = true;
					if (LevelsChanged != null)
						LevelsChanged(this, new PropertyChangedEventArgs("Levels", value));
				}
			}
		}

		public abstract Int32 NumBuffers
		{
			get ;
			set ;
		}

		public abstract Int32 NumLevels
		{
			get ;
			set ;
		}

		public virtual Mt4ExecutableInfo IndicatorInfo
		{
			get {
				return ExecutableInfo;
			}
		}

		String shortName;
		public String ShortName
		{
			get {
				return shortName;
			}
			set {
				if (!___initialized) {
					shortName= value;
					changed[IndicatorSessionProps.PROPERTY_7_SHORTNAME_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IndicatorWindowType windowType;
		public IndicatorWindowType WindowType
		{
			get {
				return windowType;
			}
			set {
				if (!___initialized) {
					windowType= value;
					changed[IndicatorSessionProps.PROPERTY_8_WINDOWTYPE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Double indicatorMinimum;
		public Double IndicatorMinimum
		{
			get {
				return indicatorMinimum;
			}
			set {
				if (indicatorMinimum != value) {
					indicatorMinimum= value;
					changed[IndicatorSessionProps.PROPERTY_9_INDICATORMINIMUM_ID] = true;
					if (IndicatorMinimumChanged != null)
						IndicatorMinimumChanged(this, new PropertyChangedEventArgs("IndicatorMinimum", value));
				}
			}
		}

		Double indicatorMaximum;
		public Double IndicatorMaximum
		{
			get {
				return indicatorMaximum;
			}
			set {
				if (indicatorMaximum != value) {
					indicatorMaximum= value;
					changed[IndicatorSessionProps.PROPERTY_10_INDICATORMAXIMUM_ID] = true;
					if (IndicatorMaximumChanged != null)
						IndicatorMaximumChanged(this, new PropertyChangedEventArgs("IndicatorMaximum", value));
				}
			}
		}

		Int32 indicatorDigits;
		public Int32 IndicatorDigits
		{
			get {
				return indicatorDigits;
			}
			set {
				if (indicatorDigits != value) {
					indicatorDigits= value;
					changed[IndicatorSessionProps.PROPERTY_11_INDICATORDIGITS_ID] = true;
					if (IndicatorDigitsChanged != null)
						IndicatorDigitsChanged(this, new PropertyChangedEventArgs("IndicatorDigits", value));
				}
			}
		}

		Int32 displayScale;
		public Int32 DisplayScale
		{
			get {
				return displayScale;
			}
			set {
				if (displayScale != value) {
					displayScale= value;
					changed[IndicatorSessionProps.PROPERTY_12_DISPLAYSCALE_ID] = true;
					if (DisplayScaleChanged != null)
						DisplayScaleChanged(this, new PropertyChangedEventArgs("DisplayScale", value));
				}
			}
		}

		Boolean enableScroll;
		public Boolean EnableScroll
		{
			get {
				return enableScroll;
			}
			set {
				if (enableScroll != value) {
					enableScroll= value;
					changed[IndicatorSessionProps.PROPERTY_13_ENABLESCROLL_ID] = true;
					if (EnableScrollChanged != null)
						EnableScrollChanged(this, new PropertyChangedEventArgs("EnableScroll", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorSessionProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!IndicatorSessionProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
