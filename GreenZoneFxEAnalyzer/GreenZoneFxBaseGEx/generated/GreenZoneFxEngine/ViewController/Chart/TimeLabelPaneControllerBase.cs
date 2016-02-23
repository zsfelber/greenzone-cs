using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class TimeLabelPaneControllerProps
	{
		public const int PROPERTY_15_STRINGBRUSH_ID = 15;
		public const int PROPERTY_16_STRINGFORMAT_ID = 16;
		public const int PROPERTY_17_FONTS_ID = 17;
		public static bool RmiGetProperty(ITimeLabelPaneController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case TimeLabelPaneControllerProps.PROPERTY_15_STRINGBRUSH_ID:
					value = controller.StringBrush;
					return true;
				case TimeLabelPaneControllerProps.PROPERTY_16_STRINGFORMAT_ID:
					value = controller.StringFormat;
					return true;
				case TimeLabelPaneControllerProps.PROPERTY_17_FONTS_ID:
					value = controller.Fonts;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ITimeLabelPaneController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case TimeLabelPaneControllerProps.PROPERTY_15_STRINGBRUSH_ID:
					controller.StringBrush = (Brush) value;
					return true;
				case TimeLabelPaneControllerProps.PROPERTY_16_STRINGFORMAT_ID:
					controller.StringFormat = (StringFormat) value;
					return true;
				case TimeLabelPaneControllerProps.PROPERTY_17_FONTS_ID:
					controller.Fonts = (Font[]) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ITimeLabelPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(ITimeLabelPaneController controller, bool goToParent)
		{
		}

		public static void SerializationRead(ITimeLabelPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.StringBrush = (Brush) info.GetValue("StringBrush", typeof(Brush));
			controller.StringFormat = (StringFormat) info.GetValue("StringFormat", typeof(StringFormat));
			controller.Fonts = (Font[]) info.GetValue("Fonts", typeof(Font[]));
		}

		public static void SerializationWrite(ITimeLabelPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			info.AddValue("StringBrush", controller.StringBrush);
			info.AddValue("StringFormat", controller.StringFormat);
			info.AddValue("Fonts", controller.Fonts);
		}

	}
	public abstract class TimeLabelPaneControllerBase : ClientController, ITimeLabelPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler ITimeLabelPaneController_StringBrush_Changed;
		public event PropertyChangedEventHandler ITimeLabelPaneController_StringFormat_Changed;
		public event PropertyChangedEventHandler ITimeLabelPaneController_Fonts_Changed;

		public TimeLabelPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			TimeLabelPaneControllerProps.AddDependencies(this, false);
		}

		public TimeLabelPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			TimeLabelPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			TimeLabelPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected TimeLabelPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			TimeLabelPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			TimeLabelPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			TimeLabelPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IChartController Parent
		{
			get {
				return (IChartController) ((Controller)this).Parent;
			}
		}

		Brush _ITimeLabelPaneController_StringBrush;
		public Brush StringBrush
		{
			get {
				return _ITimeLabelPaneController_StringBrush;
			}
			set {
				if (_ITimeLabelPaneController_StringBrush != value) {
					_ITimeLabelPaneController_StringBrush= value;
					changed[TimeLabelPaneControllerProps.PROPERTY_15_STRINGBRUSH_ID] = true;
					if (ITimeLabelPaneController_StringBrush_Changed != null)
						ITimeLabelPaneController_StringBrush_Changed(this, new PropertyChangedEventArgs("StringBrush", value));
				}
			}
		}

		StringFormat _ITimeLabelPaneController_StringFormat;
		public StringFormat StringFormat
		{
			get {
				return _ITimeLabelPaneController_StringFormat;
			}
			set {
				if (_ITimeLabelPaneController_StringFormat != value) {
					_ITimeLabelPaneController_StringFormat= value;
					changed[TimeLabelPaneControllerProps.PROPERTY_16_STRINGFORMAT_ID] = true;
					if (ITimeLabelPaneController_StringFormat_Changed != null)
						ITimeLabelPaneController_StringFormat_Changed(this, new PropertyChangedEventArgs("StringFormat", value));
				}
			}
		}

		Font[] _ITimeLabelPaneController_Fonts;
		public Font[] Fonts
		{
			get {
				return _ITimeLabelPaneController_Fonts;
			}
			set {
				if (_ITimeLabelPaneController_Fonts != value) {
					_ITimeLabelPaneController_Fonts= value;
					changed[TimeLabelPaneControllerProps.PROPERTY_17_FONTS_ID] = true;
					if (ITimeLabelPaneController_Fonts_Changed != null)
						ITimeLabelPaneController_Fonts_Changed(this, new PropertyChangedEventArgs("Fonts", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (TimeLabelPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (TimeLabelPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
