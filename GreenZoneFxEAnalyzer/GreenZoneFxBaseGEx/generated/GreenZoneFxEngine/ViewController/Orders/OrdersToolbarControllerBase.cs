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
		public static bool RmiGetProperty(IOrdersToolbarController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrdersToolbarControllerProps.PROPERTY_14_SYMBOLCB_ID:
					value = controller.SymbolCb;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_15_OPERATIONCB_ID:
					value = controller.OperationCb;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_16_EXPERTCB_ID:
					value = controller.ExpertCb;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_17_MAGICCB_ID:
					value = controller.MagicCb;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_18_GROUPBYCB_ID:
					value = controller.GroupByCb;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_19_BUYCHECKBOX_ID:
					value = controller.BuyCheckBox;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_20_SELLCHECKBOX_ID:
					value = controller.SellCheckBox;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_21_LIMITCHECKBOX_ID:
					value = controller.LimitCheckBox;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_22_STOPCHECKBOX_ID:
					value = controller.StopCheckBox;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_23_SHOWFILTERSCHECKBOX_ID:
					value = controller.ShowFiltersCheckBox;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_24_COMMENTTB_ID:
					value = controller.CommentTb;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_25_FROMDTP_ID:
					value = controller.FromDtp;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_26_TODTP_ID:
					value = controller.ToDtp;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_27_RESETBUTTON_ID:
					value = controller.ResetButton;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_28_CLOSECHARTBUTTON2_ID:
					value = controller.CloseChartButton2;
					return true;
				case OrdersToolbarControllerProps.PROPERTY_29_PANEL1_ID:
					value = controller.Panel1;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersToolbarController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersToolbarController controller, GreenRmiObjectBuffer buffer, bool goToParent)
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

		public static void AddDependencies(IOrdersToolbarController controller, bool goToParent)
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

		public static void SerializationRead(IOrdersToolbarController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IOrdersToolbarController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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
			OrdersToolbarControllerProps.AddDependencies(this, false);
		}

		public OrdersToolbarControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersToolbarControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			OrdersToolbarControllerProps.AddDependencies(this, false);
		}

		protected OrdersToolbarControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersToolbarControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrdersToolbarControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersToolbarControllerProps.SerializationWrite(this, info, context, false);
		}


		ComboController _IOrdersToolbarController_SymbolCb;
		public ComboController SymbolCb
		{
			get {
				return _IOrdersToolbarController_SymbolCb;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_SymbolCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_14_SYMBOLCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IOrdersToolbarController_OperationCb;
		public ComboController OperationCb
		{
			get {
				return _IOrdersToolbarController_OperationCb;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_OperationCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_15_OPERATIONCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IOrdersToolbarController_ExpertCb;
		public ComboController ExpertCb
		{
			get {
				return _IOrdersToolbarController_ExpertCb;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_ExpertCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_16_EXPERTCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IOrdersToolbarController_MagicCb;
		public ComboController MagicCb
		{
			get {
				return _IOrdersToolbarController_MagicCb;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_MagicCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_17_MAGICCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IOrdersToolbarController_GroupByCb;
		public ComboController GroupByCb
		{
			get {
				return _IOrdersToolbarController_GroupByCb;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_GroupByCb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_18_GROUPBYCB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IOrdersToolbarController_BuyCheckBox;
		public ToggleButtonController BuyCheckBox
		{
			get {
				return _IOrdersToolbarController_BuyCheckBox;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_BuyCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_19_BUYCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IOrdersToolbarController_SellCheckBox;
		public ToggleButtonController SellCheckBox
		{
			get {
				return _IOrdersToolbarController_SellCheckBox;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_SellCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_20_SELLCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IOrdersToolbarController_LimitCheckBox;
		public ToggleButtonController LimitCheckBox
		{
			get {
				return _IOrdersToolbarController_LimitCheckBox;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_LimitCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_21_LIMITCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IOrdersToolbarController_StopCheckBox;
		public ToggleButtonController StopCheckBox
		{
			get {
				return _IOrdersToolbarController_StopCheckBox;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_StopCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_22_STOPCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ToggleButtonController _IOrdersToolbarController_ShowFiltersCheckBox;
		public ToggleButtonController ShowFiltersCheckBox
		{
			get {
				return _IOrdersToolbarController_ShowFiltersCheckBox;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_ShowFiltersCheckBox= value;
					changed[OrdersToolbarControllerProps.PROPERTY_23_SHOWFILTERSCHECKBOX_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		LabelledController _IOrdersToolbarController_CommentTb;
		public LabelledController CommentTb
		{
			get {
				return _IOrdersToolbarController_CommentTb;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_CommentTb= value;
					changed[OrdersToolbarControllerProps.PROPERTY_24_COMMENTTB_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> _IOrdersToolbarController_FromDtp;
		public FieldController<DateTime> FromDtp
		{
			get {
				return _IOrdersToolbarController_FromDtp;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_FromDtp= value;
					changed[OrdersToolbarControllerProps.PROPERTY_25_FROMDTP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		FieldController<DateTime> _IOrdersToolbarController_ToDtp;
		public FieldController<DateTime> ToDtp
		{
			get {
				return _IOrdersToolbarController_ToDtp;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_ToDtp= value;
					changed[OrdersToolbarControllerProps.PROPERTY_26_TODTP_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IOrdersToolbarController_ResetButton;
		public ButtonController ResetButton
		{
			get {
				return _IOrdersToolbarController_ResetButton;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_ResetButton= value;
					changed[OrdersToolbarControllerProps.PROPERTY_27_RESETBUTTON_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ButtonController _IOrdersToolbarController_CloseChartButton2;
		public ButtonController CloseChartButton2
		{
			get {
				return _IOrdersToolbarController_CloseChartButton2;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_CloseChartButton2= value;
					changed[OrdersToolbarControllerProps.PROPERTY_28_CLOSECHARTBUTTON2_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		Controller _IOrdersToolbarController_Panel1;
		public Controller Panel1
		{
			get {
				return _IOrdersToolbarController_Panel1;
			}
			set {
				if (!___initialized) {
					_IOrdersToolbarController_Panel1= value;
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
			if (OrdersToolbarControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrdersToolbarControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
