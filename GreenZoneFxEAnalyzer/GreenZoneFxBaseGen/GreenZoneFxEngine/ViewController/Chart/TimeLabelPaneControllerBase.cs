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
		public static bool RmiGetProperty(ITimeLabelPaneController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_15_STRINGBRUSH_ID:
					value = controller.StringBrush;
					return true;
				case PROPERTY_16_STRINGFORMAT_ID:
					value = controller.StringFormat;
					return true;
				case PROPERTY_17_FONTS_ID:
					value = controller.Fonts;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(ITimeLabelPaneController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_15_STRINGBRUSH_ID:
					controller.StringBrush = (Brush) value;
					return true;
				case PROPERTY_16_STRINGFORMAT_ID:
					controller.StringFormat = (StringFormat) value;
					return true;
				case PROPERTY_17_FONTS_ID:
					controller.Fonts = (Font[]) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(ITimeLabelPaneController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(ITimeLabelPaneController controller)
		{
		}

		public static void SerializationRead(ITimeLabelPaneController controller, SerializationInfo info, StreamingContext context)
		{
			controller.StringBrush = (Brush) info.GetValue("StringBrush", typeof(Brush));
			controller.StringFormat = (StringFormat) info.GetValue("StringFormat", typeof(StringFormat));
			controller.Fonts = (Font[]) info.GetValue("Fonts", typeof(Font[]));
		}

		public static void SerializationWrite(ITimeLabelPaneController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("StringBrush", controller.StringBrush);
			info.AddValue("StringFormat", controller.StringFormat);
			info.AddValue("Fonts", controller.Fonts);
		}

	}
	public abstract class TimeLabelPaneControllerBase : ClientController, ITimeLabelPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler StringBrushChanged;
		public event PropertyChangedEventHandler StringFormatChanged;
		public event PropertyChangedEventHandler FontsChanged;

		public TimeLabelPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			TimeLabelPaneControllerProps.AddDependencies(this);
		}

		public TimeLabelPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			TimeLabelPaneControllerProps.Initialize(this, buffer);
			___initialized = true;
			TimeLabelPaneControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected TimeLabelPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			TimeLabelPaneControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			TimeLabelPaneControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			TimeLabelPaneControllerProps.SerializationWrite(this, info, context);
		}

		public new IChartController Parent
		{
			get {
				return (IChartController) base.Parent;
			}
		}

		Brush stringBrush;
		public Brush StringBrush
		{
			get {
				return stringBrush;
			}
			set {
				if (stringBrush != value) {
					stringBrush= value;
					changed[TimeLabelPaneControllerProps.PROPERTY_15_STRINGBRUSH_ID] = true;
					if (StringBrushChanged != null)
						StringBrushChanged(this, new PropertyChangedEventArgs("StringBrush", value));
				}
			}
		}

		StringFormat stringFormat;
		public StringFormat StringFormat
		{
			get {
				return stringFormat;
			}
			set {
				if (stringFormat != value) {
					stringFormat= value;
					changed[TimeLabelPaneControllerProps.PROPERTY_16_STRINGFORMAT_ID] = true;
					if (StringFormatChanged != null)
						StringFormatChanged(this, new PropertyChangedEventArgs("StringFormat", value));
				}
			}
		}

		Font[] fonts;
		public Font[] Fonts
		{
			get {
				return fonts;
			}
			set {
				if (fonts != value) {
					fonts= value;
					changed[TimeLabelPaneControllerProps.PROPERTY_17_FONTS_ID] = true;
					if (FontsChanged != null)
						FontsChanged(this, new PropertyChangedEventArgs("Fonts", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (TimeLabelPaneControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!TimeLabelPaneControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
