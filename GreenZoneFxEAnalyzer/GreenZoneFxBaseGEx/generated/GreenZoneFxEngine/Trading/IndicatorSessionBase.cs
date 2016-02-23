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
		public static bool RmiGetProperty(IIndicatorSession controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecSessionProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case IndicatorSessionProps.PROPERTY_5_BUFFERS_ID:
					value = controller.Buffers;
					return true;
				case IndicatorSessionProps.PROPERTY_6_LEVELS_ID:
					value = controller.Levels;
					return true;
				case IndicatorSessionProps.PROPERTY_7_SHORTNAME_ID:
					value = controller.ShortName;
					return true;
				case IndicatorSessionProps.PROPERTY_8_WINDOWTYPE_ID:
					value = controller.WindowType;
					return true;
				case IndicatorSessionProps.PROPERTY_9_INDICATORMINIMUM_ID:
					value = controller.IndicatorMinimum;
					return true;
				case IndicatorSessionProps.PROPERTY_10_INDICATORMAXIMUM_ID:
					value = controller.IndicatorMaximum;
					return true;
				case IndicatorSessionProps.PROPERTY_11_INDICATORDIGITS_ID:
					value = controller.IndicatorDigits;
					return true;
				case IndicatorSessionProps.PROPERTY_12_DISPLAYSCALE_ID:
					value = controller.DisplayScale;
					return true;
				case IndicatorSessionProps.PROPERTY_13_ENABLESCROLL_ID:
					value = controller.EnableScroll;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IIndicatorSession controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (ExecSessionProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case IndicatorSessionProps.PROPERTY_5_BUFFERS_ID:
					controller.Buffers = (List<Dictionary<String,Object>>) value;
					return true;
				case IndicatorSessionProps.PROPERTY_6_LEVELS_ID:
					controller.Levels = (List<Dictionary<String,Object>>) value;
					return true;
				case IndicatorSessionProps.PROPERTY_9_INDICATORMINIMUM_ID:
					controller.IndicatorMinimum = (Double) value;
					return true;
				case IndicatorSessionProps.PROPERTY_10_INDICATORMAXIMUM_ID:
					controller.IndicatorMaximum = (Double) value;
					return true;
				case IndicatorSessionProps.PROPERTY_11_INDICATORDIGITS_ID:
					controller.IndicatorDigits = (Int32) value;
					return true;
				case IndicatorSessionProps.PROPERTY_12_DISPLAYSCALE_ID:
					controller.DisplayScale = (Int32) value;
					return true;
				case IndicatorSessionProps.PROPERTY_13_ENABLESCROLL_ID:
					controller.EnableScroll = (Boolean) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IIndicatorSession controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.Initialize(controller, buffer, true);
			}
			controller.ShortName = (String) buffer.ChangedProps[IndicatorSessionProps.PROPERTY_7_SHORTNAME_ID];
			controller.WindowType = (IndicatorWindowType) buffer.ChangedProps[IndicatorSessionProps.PROPERTY_8_WINDOWTYPE_ID];
		}

		public static void AddDependencies(IIndicatorSession controller, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IIndicatorSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.SerializationRead(controller, info, context, true);
			}
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

		public static void SerializationWrite(IIndicatorSession controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				ExecSessionProps.SerializationWrite(controller, info, context, true);
			}
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

		public event PropertyChangedEventHandler IIndicatorSession_Buffers_Changed;
		public event PropertyChangedEventHandler IIndicatorSession_Levels_Changed;
		public event PropertyChangedEventHandler IIndicatorSession_IndicatorMinimum_Changed;
		public event PropertyChangedEventHandler IIndicatorSession_IndicatorMaximum_Changed;
		public event PropertyChangedEventHandler IIndicatorSession_IndicatorDigits_Changed;
		public event PropertyChangedEventHandler IIndicatorSession_DisplayScale_Changed;
		public event PropertyChangedEventHandler IIndicatorSession_EnableScroll_Changed;

		public IndicatorSessionBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			IndicatorSessionProps.AddDependencies(this, false);
		}

		public IndicatorSessionBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			IndicatorSessionProps.Initialize(this, buffer, false);
			___initialized = true;
			IndicatorSessionProps.AddDependencies(this, false);
		}

		protected IndicatorSessionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			IndicatorSessionProps.SerializationRead(this, info, context, false);
			___initialized = true;
			IndicatorSessionProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IndicatorSessionProps.SerializationWrite(this, info, context, false);
		}


		List<Dictionary<String,Object>> _IIndicatorSession_Buffers;
		public List<Dictionary<String,Object>> Buffers
		{
			get {
				return _IIndicatorSession_Buffers;
			}
			set {
				if (_IIndicatorSession_Buffers != value) {
					_IIndicatorSession_Buffers= value;
					changed[IndicatorSessionProps.PROPERTY_5_BUFFERS_ID] = true;
					if (IIndicatorSession_Buffers_Changed != null)
						IIndicatorSession_Buffers_Changed(this, new PropertyChangedEventArgs("Buffers", value));
				}
			}
		}

		List<Dictionary<String,Object>> _IIndicatorSession_Levels;
		public List<Dictionary<String,Object>> Levels
		{
			get {
				return _IIndicatorSession_Levels;
			}
			set {
				if (_IIndicatorSession_Levels != value) {
					_IIndicatorSession_Levels= value;
					changed[IndicatorSessionProps.PROPERTY_6_LEVELS_ID] = true;
					if (IIndicatorSession_Levels_Changed != null)
						IIndicatorSession_Levels_Changed(this, new PropertyChangedEventArgs("Levels", value));
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

		String _IIndicatorSession_ShortName;
		public String ShortName
		{
			get {
				return _IIndicatorSession_ShortName;
			}
			set {
				if (!___initialized) {
					_IIndicatorSession_ShortName= value;
					changed[IndicatorSessionProps.PROPERTY_7_SHORTNAME_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		IndicatorWindowType _IIndicatorSession_WindowType;
		public IndicatorWindowType WindowType
		{
			get {
				return _IIndicatorSession_WindowType;
			}
			set {
				if (!___initialized) {
					_IIndicatorSession_WindowType= value;
					changed[IndicatorSessionProps.PROPERTY_8_WINDOWTYPE_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Double _IIndicatorSession_IndicatorMinimum;
		public Double IndicatorMinimum
		{
			get {
				return _IIndicatorSession_IndicatorMinimum;
			}
			set {
				if (_IIndicatorSession_IndicatorMinimum != value) {
					_IIndicatorSession_IndicatorMinimum= value;
					changed[IndicatorSessionProps.PROPERTY_9_INDICATORMINIMUM_ID] = true;
					if (IIndicatorSession_IndicatorMinimum_Changed != null)
						IIndicatorSession_IndicatorMinimum_Changed(this, new PropertyChangedEventArgs("IndicatorMinimum", value));
				}
			}
		}

		Double _IIndicatorSession_IndicatorMaximum;
		public Double IndicatorMaximum
		{
			get {
				return _IIndicatorSession_IndicatorMaximum;
			}
			set {
				if (_IIndicatorSession_IndicatorMaximum != value) {
					_IIndicatorSession_IndicatorMaximum= value;
					changed[IndicatorSessionProps.PROPERTY_10_INDICATORMAXIMUM_ID] = true;
					if (IIndicatorSession_IndicatorMaximum_Changed != null)
						IIndicatorSession_IndicatorMaximum_Changed(this, new PropertyChangedEventArgs("IndicatorMaximum", value));
				}
			}
		}

		Int32 _IIndicatorSession_IndicatorDigits;
		public Int32 IndicatorDigits
		{
			get {
				return _IIndicatorSession_IndicatorDigits;
			}
			set {
				if (_IIndicatorSession_IndicatorDigits != value) {
					_IIndicatorSession_IndicatorDigits= value;
					changed[IndicatorSessionProps.PROPERTY_11_INDICATORDIGITS_ID] = true;
					if (IIndicatorSession_IndicatorDigits_Changed != null)
						IIndicatorSession_IndicatorDigits_Changed(this, new PropertyChangedEventArgs("IndicatorDigits", value));
				}
			}
		}

		Int32 _IIndicatorSession_DisplayScale;
		public Int32 DisplayScale
		{
			get {
				return _IIndicatorSession_DisplayScale;
			}
			set {
				if (_IIndicatorSession_DisplayScale != value) {
					_IIndicatorSession_DisplayScale= value;
					changed[IndicatorSessionProps.PROPERTY_12_DISPLAYSCALE_ID] = true;
					if (IIndicatorSession_DisplayScale_Changed != null)
						IIndicatorSession_DisplayScale_Changed(this, new PropertyChangedEventArgs("DisplayScale", value));
				}
			}
		}

		Boolean _IIndicatorSession_EnableScroll;
		public Boolean EnableScroll
		{
			get {
				return _IIndicatorSession_EnableScroll;
			}
			set {
				if (_IIndicatorSession_EnableScroll != value) {
					_IIndicatorSession_EnableScroll= value;
					changed[IndicatorSessionProps.PROPERTY_13_ENABLESCROLL_ID] = true;
					if (IIndicatorSession_EnableScroll_Changed != null)
						IIndicatorSession_EnableScroll_Changed(this, new PropertyChangedEventArgs("EnableScroll", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (IndicatorSessionProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (IndicatorSessionProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
