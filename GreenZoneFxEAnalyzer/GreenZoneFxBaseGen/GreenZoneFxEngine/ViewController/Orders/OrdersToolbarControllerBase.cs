using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Orders
{
	public static class OrdersToolbarControllerProps
	{
		public const int PROPERTY_14_SYMBOLCB_ID = 14;
		public const int PROPERTY_15_OPERATIONCB_ID = 15;
		public const int PROPERTY_16_EXPERTCB_ID = 16;
		public const int PROPERTY_17_MAGICCB_ID = 17;
		public const int PROPERTY_18_GROUPBYCB_ID = 18;
		public const int PROPERTY_19_BUYCHECKBOX_ID = 19;
		public const int PROPERTY_20_SELLCHECKBOX_ID = 20;
		public const int PROPERTY_21_LIMITCHECKBOX_ID = 21;
		public const int PROPERTY_22_STOPCHECKBOX_ID = 22;
		public const int PROPERTY_23_SHOWFILTERSCHECKBOX_ID = 23;
		public const int PROPERTY_24_COMMENTTB_ID = 24;
		public const int PROPERTY_25_FROMDTP_ID = 25;
		public const int PROPERTY_26_TODTP_ID = 26;
		public const int PROPERTY_27_RESETBUTTON_ID = 27;
		public const int PROPERTY_28_CLOSECHARTBUTTON2_ID = 28;
		public const int PROPERTY_29_PANEL1_ID = 29;
		public static bool RmiGetProperty(IOrdersToolbarController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_14_SYMBOLCB_ID:
					value = controller.SymbolCb;
					return true;
				case PROPERTY_15_OPERATIONCB_ID:
					value = controller.OperationCb;
					return true;
				case PROPERTY_16_EXPERTCB_ID:
					value = controller.ExpertCb;
					return true;
				case PROPERTY_17_MAGICCB_ID:
					value = controller.MagicCb;
					return true;
				case PROPERTY_18_GROUPBYCB_ID:
					value = controller.GroupByCb;
					return true;
				case PROPERTY_19_BUYCHECKBOX_ID:
					value = controller.BuyCheckBox;
					return true;
				case PROPERTY_20_SELLCHECKBOX_ID:
					value = controller.SellCheckBox;
					return true;
				case PROPERTY_21_LIMITCHECKBOX_ID:
					value = controller.LimitCheckBox;
					return true;
				case PROPERTY_22_STOPCHECKBOX_ID:
					value = controller.StopCheckBox;
					return true;
				case PROPERTY_23_SHOWFILTERSCHECKBOX_ID:
					value = controller.ShowFiltersCheckBox;
					return true;
				case PROPERTY_24_COMMENTTB_ID:
					value = controller.CommentTb;
					return true;
				case PROPERTY_25_FROMDTP_ID:
					value = controller.FromDtp;
					return true;
				case PROPERTY_26_TODTP_ID:
					value = controller.ToDtp;
					return true;
				case PROPERTY_27_RESETBUTTON_ID:
					value = controller.ResetButton;
					return true;
				case PROPERTY_28_CLOSECHARTBUTTON2_ID:
					value = controller.CloseChartButton2;
					return true;
				case PROPERTY_29_PANEL1_ID:
					value = controller.Panel1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersToolbarController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersToolbarController controller, GreenRmiObjectBuffer buffer)
		{
			controller.SymbolCb = (ComboController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_14_SYMBOLCB_ID];
			controller.OperationCb = (ComboController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_15_OPERATIONCB_ID];
			controller.ExpertCb = (ComboController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_16_EXPERTCB_ID];
			controller.MagicCb = (ComboController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_17_MAGICCB_ID];
			controller.GroupByCb = (ComboController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_18_GROUPBYCB_ID];
			controller.BuyCheckBox = (ToggleButtonController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_19_BUYCHECKBOX_ID];
			controller.SellCheckBox = (ToggleButtonController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_20_SELLCHECKBOX_ID];
			controller.LimitCheckBox = (ToggleButtonController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_21_LIMITCHECKBOX_ID];
			controller.StopCheckBox = (ToggleButtonController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_22_STOPCHECKBOX_ID];
			controller.ShowFiltersCheckBox = (ToggleButtonController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_23_SHOWFILTERSCHECKBOX_ID];
			controller.CommentTb = (LabelledController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_24_COMMENTTB_ID];
			controller.FromDtp = (FieldController<DateTime>) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_25_FROMDTP_ID];
			controller.ToDtp = (FieldController<DateTime>) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_26_TODTP_ID];
			controller.ResetButton = (ButtonController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_27_RESETBUTTON_ID];
			controller.CloseChartButton2 = (ButtonController) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_28_CLOSECHARTBUTTON2_ID];
			controller.Panel1 = (Controller) buffer.ChangedProps[OrdersToolbarControllerProps.PROPERTY_29_PANEL1_ID];
		}

		public static void AddDependencies(IOrdersToolbarController controller)
		{
			controller.Dependencies.Add(controller.SymbolCb);
			controller.Dependencies.Add(controller.OperationCb);
			controller.Dependencies.Add(controller.ExpertCb);
			controller.Dependencies.Add(controller.MagicCb);
			controller.Dependencies.Add(controller.GroupByCb);
			controller.Dependencies.Add(controller.BuyCheckBox);
			controller.Dependencies.Add(controller.SellCheckBox);
			controller.Dependencies.Add(controller.LimitCheckBox);
			controller.Dependencies.Add(controller.StopCheckBox);
			controller.Dependencies.Add(controller.ShowFiltersCheckBox);
			controller.Dependencies.Add(controller.CommentTb);
			controller.Dependencies.Add(controller.FromDtp);
			controller.Dependencies.Add(controller.ToDtp);
			controller.Dependencies.Add(controller.ResetButton);
			controller.Dependencies.Add(controller.CloseChartButton2);
			controller.Dependencies.Add(controller.Panel1);
		}

		public static void SerializationRead(IOrdersToolbarController controller, SerializationInfo info, StreamingContext context)
		{
			controller.SymbolCb = (ComboController) info.GetValue("SymbolCb", typeof(ComboController));
			controller.OperationCb = (ComboController) info.GetValue("OperationCb", typeof(ComboController));
			controller.ExpertCb = (ComboController) info.GetValue("ExpertCb", typeof(ComboController));
			controller.MagicCb = (ComboController) info.GetValue("MagicCb", typeof(ComboController));
			controller.GroupByCb = (ComboController) info.GetValue("GroupByCb", typeof(ComboController));
			controller.BuyCheckBox = (ToggleButtonController) info.GetValue("BuyCheckBox", typeof(ToggleButtonController));
			controller.SellCheckBox = (ToggleButtonController) info.GetValue("SellCheckBox", typeof(ToggleButtonController));
			controller.LimitCheckBox = (ToggleButtonController) info.GetValue("LimitCheckBox", typeof(ToggleButtonController));
			controller.StopCheckBox = (ToggleButtonController) info.GetValue("StopCheckBox", typeof(ToggleButtonController));
			controller.ShowFiltersCheckBox = (ToggleButtonController) info.GetValue("ShowFiltersCheckBox", typeof(ToggleButtonController));
			controller.CommentTb = (LabelledController) info.GetValue("CommentTb", typeof(LabelledController));
			controller.FromDtp = (FieldController<DateTime>) info.GetValue("FromDtp", typeof(FieldController<DateTime>));
			controller.ToDtp = (FieldController<DateTime>) info.GetValue("ToDtp", typeof(FieldController<DateTime>));
			controller.ResetButton = (ButtonController) info.GetValue("ResetButton", typeof(ButtonController));
			controller.CloseChartButton2 = (ButtonController) info.GetValue("CloseChartButton2", typeof(ButtonController));
			controller.Panel1 = (Controller) info.GetValue("Panel1", typeof(Controller));
		}

		public static void SerializationWrite(IOrdersToolbarController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SymbolCb", controller.SymbolCb);
			info.AddValue("OperationCb", controller.OperationCb);
			info.AddValue("ExpertCb", controller.ExpertCb);
			info.AddValue("MagicCb", controller.MagicCb);
			info.AddValue("GroupByCb", controller.GroupByCb);
			info.AddValue("BuyCheckBox", controller.BuyCheckBox);
			info.AddValue("SellCheckBox", controller.SellCheckBox);
			info.AddValue("LimitCheckBox", controller.LimitCheckBox);
			info.AddValue("StopCheckBox", controller.StopCheckBox);
			info.AddValue("ShowFiltersCheckBox", controller.ShowFiltersCheckBox);
			info.AddValue("CommentTb", controller.CommentTb);
			info.AddValue("FromDtp", controller.FromDtp);
			info.AddValue("ToDtp", controller.ToDtp);
			info.AddValue("ResetButton", controller.ResetButton);
			info.AddValue("CloseChartButton2", controller.CloseChartButton2);
			info.AddValue("Panel1", controller.Panel1);
		}

	}
	public abstract class OrdersToolbarControllerBase : Controller, IOrdersToolbarController
	{

		bool ___initialized = false;


		public OrdersToolbarControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrdersToolbarControllerProps.AddDependencies(this);
		}

		public OrdersToolbarControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersToolbarControllerProps.Initialize(this, buffer);
			___initialized = true;
			OrdersToolbarControllerProps.AddDependencies(this);
		}

		protected OrdersToolbarControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersToolbarControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			OrdersToolbarControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersToolbarControllerProps.SerializationWrite(this, info, context);
		}

		ComboController symbolCb;
		public ComboController SymbolCb
		{
			get {
				return symbolCb;
			}
			set {
				if (!___initialized) {
					symbolCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_14_SYMBOLCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController operationCb;
		public ComboController OperationCb
		{
			get {
				return operationCb;
			}
			set {
				if (!___initialized) {
					operationCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_15_OPERATIONCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController expertCb;
		public ComboController ExpertCb
		{
			get {
				return expertCb;
			}
			set {
				if (!___initialized) {
					expertCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_16_EXPERTCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController magicCb;
		public ComboController MagicCb
		{
			get {
				return magicCb;
			}
			set {
				if (!___initialized) {
					magicCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_17_MAGICCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController groupByCb;
		public ComboController GroupByCb
		{
			get {
				return groupByCb;
			}
			set {
				if (!___initialized) {
					groupByCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_18_GROUPBYCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController buyCheckBox;
		public ToggleButtonController BuyCheckBox
		{
			get {
				return buyCheckBox;
			}
			set {
				if (!___initialized) {
					buyCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_19_BUYCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController sellCheckBox;
		public ToggleButtonController SellCheckBox
		{
			get {
				return sellCheckBox;
			}
			set {
				if (!___initialized) {
					sellCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_20_SELLCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController limitCheckBox;
		public ToggleButtonController LimitCheckBox
		{
			get {
				return limitCheckBox;
			}
			set {
				if (!___initialized) {
					limitCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_21_LIMITCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController stopCheckBox;
		public ToggleButtonController StopCheckBox
		{
			get {
				return stopCheckBox;
			}
			set {
				if (!___initialized) {
					stopCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_22_STOPCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController showFiltersCheckBox;
		public ToggleButtonController ShowFiltersCheckBox
		{
			get {
				return showFiltersCheckBox;
			}
			set {
				if (!___initialized) {
					showFiltersCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_23_SHOWFILTERSCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController commentTb;
		public LabelledController CommentTb
		{
			get {
				return commentTb;
			}
			set {
				if (!___initialized) {
					commentTb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_24_COMMENTTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> fromDtp;
		public FieldController<DateTime> FromDtp
		{
			get {
				return fromDtp;
			}
			set {
				if (!___initialized) {
					fromDtp= value;
					changed[OrdersToolbarControllerProps.PROPERTY_25_FROMDTP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> toDtp;
		public FieldController<DateTime> ToDtp
		{
			get {
				return toDtp;
			}
			set {
				if (!___initialized) {
					toDtp= value;
					changed[OrdersToolbarControllerProps.PROPERTY_26_TODTP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController resetButton;
		public ButtonController ResetButton
		{
			get {
				return resetButton;
			}
			set {
				if (!___initialized) {
					resetButton= value;
					changed[OrdersToolbarControllerProps.PROPERTY_27_RESETBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController closeChartButton2;
		public ButtonController CloseChartButton2
		{
			get {
				return closeChartButton2;
			}
			set {
				if (!___initialized) {
					closeChartButton2= value;
					changed[OrdersToolbarControllerProps.PROPERTY_28_CLOSECHARTBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Controller panel1;
		public Controller Panel1
		{
			get {
				return panel1;
			}
			set {
				if (!___initialized) {
					panel1= value;
					changed[OrdersToolbarControllerProps.PROPERTY_29_PANEL1_ID] = true;
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
			if (OrdersToolbarControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrdersToolbarControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
