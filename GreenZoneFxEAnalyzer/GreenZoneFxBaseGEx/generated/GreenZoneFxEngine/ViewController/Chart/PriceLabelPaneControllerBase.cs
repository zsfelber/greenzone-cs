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
		public static bool RmiGetProperty(IPriceLabelPaneController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case PriceLabelPaneControllerProps.PROPERTY_15_PLAINFONTS_ID:
					value = controller.PlainFonts;
					return true;
				case PriceLabelPaneControllerProps.PROPERTY_16_STRINGFORMAT_ID:
					value = controller.StringFormat;
					return true;
				case PriceLabelPaneControllerProps.PROPERTY_17_STRINGBRUSH_ID:
					value = controller.StringBrush;
					return true;
				case PriceLabelPaneControllerProps.PROPERTY_18_FONTS_ID:
					value = controller.Fonts;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IPriceLabelPaneController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				case PriceLabelPaneControllerProps.PROPERTY_15_PLAINFONTS_ID:
					controller.PlainFonts = (Boolean) value;
					return true;
				case PriceLabelPaneControllerProps.PROPERTY_16_STRINGFORMAT_ID:
					controller.StringFormat = (StringFormat) value;
					return true;
				case PriceLabelPaneControllerProps.PROPERTY_17_STRINGBRUSH_ID:
					controller.StringBrush = (Brush) value;
					return true;
				case PriceLabelPaneControllerProps.PROPERTY_18_FONTS_ID:
					controller.Fonts = (Font[]) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IPriceLabelPaneController controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
		}

		public static void AddDependencies(IPriceLabelPaneController controller, bool goToParent)
		{
		}

		public static void SerializationRead(IPriceLabelPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			controller.PlainFonts = (Boolean) info.GetValue("PlainFonts", typeof(Boolean));
			controller.StringFormat = (StringFormat) info.GetValue("StringFormat", typeof(StringFormat));
			controller.StringBrush = (Brush) info.GetValue("StringBrush", typeof(Brush));
			controller.Fonts = (Font[]) info.GetValue("Fonts", typeof(Font[]));
		}

		public static void SerializationWrite(IPriceLabelPaneController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public event PropertyChangedEventHandler IPriceLabelPaneController_PlainFonts_Changed;
		public event PropertyChangedEventHandler IPriceLabelPaneController_StringFormat_Changed;
		public event PropertyChangedEventHandler IPriceLabelPaneController_StringBrush_Changed;
		public event PropertyChangedEventHandler IPriceLabelPaneController_Fonts_Changed;

		public PriceLabelPaneControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			PriceLabelPaneControllerProps.AddDependencies(this, false);
		}

		public PriceLabelPaneControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			PriceLabelPaneControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			PriceLabelPaneControllerProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected PriceLabelPaneControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			PriceLabelPaneControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			PriceLabelPaneControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			PriceLabelPaneControllerProps.SerializationWrite(this, info, context, false);
		}


		public new virtual IChartSectionPanelController Parent
		{
			get {
				return (IChartSectionPanelController) ((Controller)this).Parent;
			}
		}

		Boolean _IPriceLabelPaneController_PlainFonts;
		public virtual Boolean PlainFonts
		{
			get {
				return _IPriceLabelPaneController_PlainFonts;
			}
			set {
				if (_IPriceLabelPaneController_PlainFonts != value) {
					_IPriceLabelPaneController_PlainFonts= value;
					changed[PriceLabelPaneControllerProps.PROPERTY_15_PLAINFONTS_ID] = true;
					if (IPriceLabelPaneController_PlainFonts_Changed != null)
						IPriceLabelPaneController_PlainFonts_Changed(this, new PropertyChangedEventArgs("PlainFonts", value));
				}
			}
		}

		StringFormat _IPriceLabelPaneController_StringFormat;
		public StringFormat StringFormat
		{
			get {
				return _IPriceLabelPaneController_StringFormat;
			}
			set {
				if (_IPriceLabelPaneController_StringFormat != value) {
					_IPriceLabelPaneController_StringFormat= value;
					changed[PriceLabelPaneControllerProps.PROPERTY_16_STRINGFORMAT_ID] = true;
					if (IPriceLabelPaneController_StringFormat_Changed != null)
						IPriceLabelPaneController_StringFormat_Changed(this, new PropertyChangedEventArgs("StringFormat", value));
				}
			}
		}

		Brush _IPriceLabelPaneController_StringBrush;
		public Brush StringBrush
		{
			get {
				return _IPriceLabelPaneController_StringBrush;
			}
			set {
				if (_IPriceLabelPaneController_StringBrush != value) {
					_IPriceLabelPaneController_StringBrush= value;
					changed[PriceLabelPaneControllerProps.PROPERTY_17_STRINGBRUSH_ID] = true;
					if (IPriceLabelPaneController_StringBrush_Changed != null)
						IPriceLabelPaneController_StringBrush_Changed(this, new PropertyChangedEventArgs("StringBrush", value));
				}
			}
		}

		Font[] _IPriceLabelPaneController_Fonts;
		public Font[] Fonts
		{
			get {
				return _IPriceLabelPaneController_Fonts;
			}
			set {
				if (_IPriceLabelPaneController_Fonts != value) {
					_IPriceLabelPaneController_Fonts= value;
					changed[PriceLabelPaneControllerProps.PROPERTY_18_FONTS_ID] = true;
					if (IPriceLabelPaneController_Fonts_Changed != null)
						IPriceLabelPaneController_Fonts_Changed(this, new PropertyChangedEventArgs("Fonts", value));
				}
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (PriceLabelPaneControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (PriceLabelPaneControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
