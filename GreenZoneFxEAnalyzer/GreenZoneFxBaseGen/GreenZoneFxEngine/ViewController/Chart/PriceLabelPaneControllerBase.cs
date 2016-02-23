using GreenZoneFxEngine.ViewController.Chart;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Chart
{
	public static class PriceLabelPaneControllerProps
	{
		public const int PROPERTY_15_PLAINFONTS_ID = 15;
		public const int PROPERTY_16_STRINGFORMAT_ID = 16;
		public const int PROPERTY_17_STRINGBRUSH_ID = 17;
		public const int PROPERTY_18_FONTS_ID = 18;
		public static bool RmiGetProperty(IPriceLabelPaneController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_15_PLAINFONTS_ID:
					value = controller.PlainFonts;
					return true;
				case PROPERTY_16_STRINGFORMAT_ID:
					value = controller.StringFormat;
					return true;
				case PROPERTY_17_STRINGBRUSH_ID:
					value = controller.StringBrush;
					return true;
				case PROPERTY_18_FONTS_ID:
					value = controller.Fonts;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IPriceLabelPaneController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				case PROPERTY_15_PLAINFONTS_ID:
					controller.PlainFonts = (Boolean) value;
					return true;
				case PROPERTY_16_STRINGFORMAT_ID:
					controller.StringFormat = (StringFormat) value;
					return true;
				case PROPERTY_17_STRINGBRUSH_ID:
					controller.StringBrush = (Brush) value;
					return true;
				case PROPERTY_18_FONTS_ID:
					controller.Fonts = (Font[]) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IPriceLabelPaneController controller, GreenRmiObjectBuffer buffer)
		{
		}

		public static void AddDependencies(IPriceLabelPaneController controller)
		{
		}

		public static void SerializationRead(IPriceLabelPaneController controller, SerializationInfo info, StreamingContext context)
		{
			controller.PlainFonts = (Boolean) info.GetValue("PlainFonts", typeof(Boolean));
			controller.StringFormat = (StringFormat) info.GetValue("StringFormat", typeof(StringFormat));
			controller.StringBrush = (Brush) info.GetValue("StringBrush", typeof(Brush));
			controller.Fonts = (Font[]) info.GetValue("Fonts", typeof(Font[]));
		}

		public static void SerializationWrite(IPriceLabelPaneController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("PlainFonts", controller.PlainFonts);
			info.AddValue("StringFormat", controller.StringFormat);
			info.AddValue("StringBrush", controller.StringBrush);
			info.AddValue("Fonts", controller.Fonts);
		}

	}
	public abstract class PriceLabelPaneControllerBase : ClientController, IPriceLabelPaneController
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler PlainFontsChanged;
		public event PropertyChangedEventHandler StringFormatChanged;
		public event PropertyChangedEventHandler StringBrushChanged;
		public event PropertyChangedEventHandler FontsChanged;

		public PriceLabelPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			PriceLabelPaneControllerProps.AddDependencies(this);
		}

		public PriceLabelPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			PriceLabelPaneControllerProps.Initialize(this, buffer);
			___initialized = true;
			PriceLabelPaneControllerProps.AddDependencies(this);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected PriceLabelPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			PriceLabelPaneControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			PriceLabelPaneControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			PriceLabelPaneControllerProps.SerializationWrite(this, info, context);
		}

		public new IChartSectionPanelController Parent
		{
			get {
				return (IChartSectionPanelController) base.Parent;
			}
		}

		Boolean plainFonts;
		public Boolean PlainFonts
		{
			get {
				return plainFonts;
			}
			set {
				if (plainFonts != value) {
					plainFonts= value;
					changed[PriceLabelPaneControllerProps.PROPERTY_15_PLAINFONTS_ID] = true;
					if (PlainFontsChanged != null)
						PlainFontsChanged(this, new PropertyChangedEventArgs("PlainFonts", value));
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
					changed[PriceLabelPaneControllerProps.PROPERTY_16_STRINGFORMAT_ID] = true;
					if (StringFormatChanged != null)
						StringFormatChanged(this, new PropertyChangedEventArgs("StringFormat", value));
				}
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
					changed[PriceLabelPaneControllerProps.PROPERTY_17_STRINGBRUSH_ID] = true;
					if (StringBrushChanged != null)
						StringBrushChanged(this, new PropertyChangedEventArgs("StringBrush", value));
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
					changed[PriceLabelPaneControllerProps.PROPERTY_18_FONTS_ID] = true;
					if (FontsChanged != null)
						FontsChanged(this, new PropertyChangedEventArgs("Fonts", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (PriceLabelPaneControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!PriceLabelPaneControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
