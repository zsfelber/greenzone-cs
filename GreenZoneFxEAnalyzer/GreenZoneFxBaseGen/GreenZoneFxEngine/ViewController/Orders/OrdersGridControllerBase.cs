using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.ViewController.Orders
{
	public static class OrdersGridControllerProps
	{
		public const int PROPERTY_20_TICKETCOLUMN_ID = 20;
		public const int PROPERTY_21_EXPERTCOLUMN_ID = 21;
		public const int PROPERTY_22_OPERATIONCOLUMN_ID = 22;
		public const int PROPERTY_23_TYPECOLUMN_ID = 23;
		public const int PROPERTY_24_SYMBOLCOLUMN_ID = 24;
		public const int PROPERTY_25_LOTSCOLUMN_ID = 25;
		public const int PROPERTY_26_OPENTIMECOLUMN_ID = 26;
		public const int PROPERTY_27_OPENPRICECOLUMN_ID = 27;
		public const int PROPERTY_28_STOPLOSSCOLUMN_ID = 28;
		public const int PROPERTY_29_TAKEPROFITCOLUMN_ID = 29;
		public const int PROPERTY_30_CLOSETIMECOLUMN_ID = 30;
		public const int PROPERTY_31_CLOSEPRICECOLUMN_ID = 31;
		public const int PROPERTY_32_COMMISSIONCOLUMN_ID = 32;
		public const int PROPERTY_33_SWAPCOLUMN_ID = 33;
		public const int PROPERTY_34_PROFITCOLUMN_ID = 34;
		public const int PROPERTY_35_COMMENTCOLUMN_ID = 35;
		public const int PROPERTY_36_EXPIRATIONCOLUMN_ID = 36;
		public const int PROPERTY_37_MAGICNUMBERCOLUMN_ID = 37;
		public const int PROPERTY_38_BALANCECOLUMN_ID = 38;
		public const int PROPERTY_39_SHOWCOLUMNSCMSTR_ID = 39;
		public static bool RmiGetProperty(IOrdersGridController controller, int propertyId, out object value)
		{
			switch (propertyId)
			{
				case PROPERTY_20_TICKETCOLUMN_ID:
					value = controller.TicketColumn;
					return true;
				case PROPERTY_21_EXPERTCOLUMN_ID:
					value = controller.ExpertColumn;
					return true;
				case PROPERTY_22_OPERATIONCOLUMN_ID:
					value = controller.OperationColumn;
					return true;
				case PROPERTY_23_TYPECOLUMN_ID:
					value = controller.TypeColumn;
					return true;
				case PROPERTY_24_SYMBOLCOLUMN_ID:
					value = controller.SymbolColumn;
					return true;
				case PROPERTY_25_LOTSCOLUMN_ID:
					value = controller.LotsColumn;
					return true;
				case PROPERTY_26_OPENTIMECOLUMN_ID:
					value = controller.OpenTimeColumn;
					return true;
				case PROPERTY_27_OPENPRICECOLUMN_ID:
					value = controller.OpenPriceColumn;
					return true;
				case PROPERTY_28_STOPLOSSCOLUMN_ID:
					value = controller.StopLossColumn;
					return true;
				case PROPERTY_29_TAKEPROFITCOLUMN_ID:
					value = controller.TakeProfitColumn;
					return true;
				case PROPERTY_30_CLOSETIMECOLUMN_ID:
					value = controller.CloseTimeColumn;
					return true;
				case PROPERTY_31_CLOSEPRICECOLUMN_ID:
					value = controller.ClosePriceColumn;
					return true;
				case PROPERTY_32_COMMISSIONCOLUMN_ID:
					value = controller.CommissionColumn;
					return true;
				case PROPERTY_33_SWAPCOLUMN_ID:
					value = controller.SwapColumn;
					return true;
				case PROPERTY_34_PROFITCOLUMN_ID:
					value = controller.ProfitColumn;
					return true;
				case PROPERTY_35_COMMENTCOLUMN_ID:
					value = controller.CommentColumn;
					return true;
				case PROPERTY_36_EXPIRATIONCOLUMN_ID:
					value = controller.ExpirationColumn;
					return true;
				case PROPERTY_37_MAGICNUMBERCOLUMN_ID:
					value = controller.MagicNumberColumn;
					return true;
				case PROPERTY_38_BALANCECOLUMN_ID:
					value = controller.BalanceColumn;
					return true;
				case PROPERTY_39_SHOWCOLUMNSCMSTR_ID:
					value = controller.ShowColumnsCmstr;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersGridController controller, int propertyId, object value)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersGridController controller, GreenRmiObjectBuffer buffer)
		{
			controller.TicketColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_20_TICKETCOLUMN_ID];
			controller.ExpertColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_21_EXPERTCOLUMN_ID];
			controller.OperationColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_22_OPERATIONCOLUMN_ID];
			controller.TypeColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_23_TYPECOLUMN_ID];
			controller.SymbolColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_24_SYMBOLCOLUMN_ID];
			controller.LotsColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_25_LOTSCOLUMN_ID];
			controller.OpenTimeColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_26_OPENTIMECOLUMN_ID];
			controller.OpenPriceColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_27_OPENPRICECOLUMN_ID];
			controller.StopLossColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_28_STOPLOSSCOLUMN_ID];
			controller.TakeProfitColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_29_TAKEPROFITCOLUMN_ID];
			controller.CloseTimeColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_30_CLOSETIMECOLUMN_ID];
			controller.ClosePriceColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_31_CLOSEPRICECOLUMN_ID];
			controller.CommissionColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_32_COMMISSIONCOLUMN_ID];
			controller.SwapColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_33_SWAPCOLUMN_ID];
			controller.ProfitColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_34_PROFITCOLUMN_ID];
			controller.CommentColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_35_COMMENTCOLUMN_ID];
			controller.ExpirationColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_36_EXPIRATIONCOLUMN_ID];
			controller.MagicNumberColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_37_MAGICNUMBERCOLUMN_ID];
			controller.BalanceColumn = (GridColumnController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_38_BALANCECOLUMN_ID];
			controller.ShowColumnsCmstr = (ComboController) buffer.ChangedProps[OrdersGridControllerProps.PROPERTY_39_SHOWCOLUMNSCMSTR_ID];
		}

		public static void AddDependencies(IOrdersGridController controller)
		{
			controller.Dependencies.Add(controller.TicketColumn);
			controller.Dependencies.Add(controller.ExpertColumn);
			controller.Dependencies.Add(controller.OperationColumn);
			controller.Dependencies.Add(controller.TypeColumn);
			controller.Dependencies.Add(controller.SymbolColumn);
			controller.Dependencies.Add(controller.LotsColumn);
			controller.Dependencies.Add(controller.OpenTimeColumn);
			controller.Dependencies.Add(controller.OpenPriceColumn);
			controller.Dependencies.Add(controller.StopLossColumn);
			controller.Dependencies.Add(controller.TakeProfitColumn);
			controller.Dependencies.Add(controller.CloseTimeColumn);
			controller.Dependencies.Add(controller.ClosePriceColumn);
			controller.Dependencies.Add(controller.CommissionColumn);
			controller.Dependencies.Add(controller.SwapColumn);
			controller.Dependencies.Add(controller.ProfitColumn);
			controller.Dependencies.Add(controller.CommentColumn);
			controller.Dependencies.Add(controller.ExpirationColumn);
			controller.Dependencies.Add(controller.MagicNumberColumn);
			controller.Dependencies.Add(controller.BalanceColumn);
			controller.Dependencies.Add(controller.ShowColumnsCmstr);
		}

		public static void SerializationRead(IOrdersGridController controller, SerializationInfo info, StreamingContext context)
		{
			controller.TicketColumn = (GridColumnController) info.GetValue("TicketColumn", typeof(GridColumnController));
			controller.ExpertColumn = (GridColumnController) info.GetValue("ExpertColumn", typeof(GridColumnController));
			controller.OperationColumn = (GridColumnController) info.GetValue("OperationColumn", typeof(GridColumnController));
			controller.TypeColumn = (GridColumnController) info.GetValue("TypeColumn", typeof(GridColumnController));
			controller.SymbolColumn = (GridColumnController) info.GetValue("SymbolColumn", typeof(GridColumnController));
			controller.LotsColumn = (GridColumnController) info.GetValue("LotsColumn", typeof(GridColumnController));
			controller.OpenTimeColumn = (GridColumnController) info.GetValue("OpenTimeColumn", typeof(GridColumnController));
			controller.OpenPriceColumn = (GridColumnController) info.GetValue("OpenPriceColumn", typeof(GridColumnController));
			controller.StopLossColumn = (GridColumnController) info.GetValue("StopLossColumn", typeof(GridColumnController));
			controller.TakeProfitColumn = (GridColumnController) info.GetValue("TakeProfitColumn", typeof(GridColumnController));
			controller.CloseTimeColumn = (GridColumnController) info.GetValue("CloseTimeColumn", typeof(GridColumnController));
			controller.ClosePriceColumn = (GridColumnController) info.GetValue("ClosePriceColumn", typeof(GridColumnController));
			controller.CommissionColumn = (GridColumnController) info.GetValue("CommissionColumn", typeof(GridColumnController));
			controller.SwapColumn = (GridColumnController) info.GetValue("SwapColumn", typeof(GridColumnController));
			controller.ProfitColumn = (GridColumnController) info.GetValue("ProfitColumn", typeof(GridColumnController));
			controller.CommentColumn = (GridColumnController) info.GetValue("CommentColumn", typeof(GridColumnController));
			controller.ExpirationColumn = (GridColumnController) info.GetValue("ExpirationColumn", typeof(GridColumnController));
			controller.MagicNumberColumn = (GridColumnController) info.GetValue("MagicNumberColumn", typeof(GridColumnController));
			controller.BalanceColumn = (GridColumnController) info.GetValue("BalanceColumn", typeof(GridColumnController));
			controller.ShowColumnsCmstr = (ComboController) info.GetValue("ShowColumnsCmstr", typeof(ComboController));
		}

		public static void SerializationWrite(IOrdersGridController controller, SerializationInfo info, StreamingContext context)
		{
			info.AddValue("TicketColumn", controller.TicketColumn);
			info.AddValue("ExpertColumn", controller.ExpertColumn);
			info.AddValue("OperationColumn", controller.OperationColumn);
			info.AddValue("TypeColumn", controller.TypeColumn);
			info.AddValue("SymbolColumn", controller.SymbolColumn);
			info.AddValue("LotsColumn", controller.LotsColumn);
			info.AddValue("OpenTimeColumn", controller.OpenTimeColumn);
			info.AddValue("OpenPriceColumn", controller.OpenPriceColumn);
			info.AddValue("StopLossColumn", controller.StopLossColumn);
			info.AddValue("TakeProfitColumn", controller.TakeProfitColumn);
			info.AddValue("CloseTimeColumn", controller.CloseTimeColumn);
			info.AddValue("ClosePriceColumn", controller.ClosePriceColumn);
			info.AddValue("CommissionColumn", controller.CommissionColumn);
			info.AddValue("SwapColumn", controller.SwapColumn);
			info.AddValue("ProfitColumn", controller.ProfitColumn);
			info.AddValue("CommentColumn", controller.CommentColumn);
			info.AddValue("ExpirationColumn", controller.ExpirationColumn);
			info.AddValue("MagicNumberColumn", controller.MagicNumberColumn);
			info.AddValue("BalanceColumn", controller.BalanceColumn);
			info.AddValue("ShowColumnsCmstr", controller.ShowColumnsCmstr);
		}

	}
	public abstract class OrdersGridControllerBase : GridController, IOrdersGridController
	{

		bool ___initialized = false;


		public OrdersGridControllerBase(GreenRmiManager rmiManager, Controller parent)
			: base(rmiManager, parent)
		{
			___initialized = true;
			OrdersGridControllerProps.AddDependencies(this);
		}

		public OrdersGridControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersGridControllerProps.Initialize(this, buffer);
			___initialized = true;
			OrdersGridControllerProps.AddDependencies(this);
		}

		protected OrdersGridControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersGridControllerProps.SerializationRead(this, info, context);
			___initialized = true;
			OrdersGridControllerProps.AddDependencies(this);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersGridControllerProps.SerializationWrite(this, info, context);
		}

		GridColumnController ticketColumn;
		public GridColumnController TicketColumn
		{
			get {
				return ticketColumn;
			}
			set {
				if (!___initialized) {
					ticketColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_20_TICKETCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController expertColumn;
		public GridColumnController ExpertColumn
		{
			get {
				return expertColumn;
			}
			set {
				if (!___initialized) {
					expertColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_21_EXPERTCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController operationColumn;
		public GridColumnController OperationColumn
		{
			get {
				return operationColumn;
			}
			set {
				if (!___initialized) {
					operationColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_22_OPERATIONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController typeColumn;
		public GridColumnController TypeColumn
		{
			get {
				return typeColumn;
			}
			set {
				if (!___initialized) {
					typeColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_23_TYPECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController symbolColumn;
		public GridColumnController SymbolColumn
		{
			get {
				return symbolColumn;
			}
			set {
				if (!___initialized) {
					symbolColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_24_SYMBOLCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController lotsColumn;
		public GridColumnController LotsColumn
		{
			get {
				return lotsColumn;
			}
			set {
				if (!___initialized) {
					lotsColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_25_LOTSCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController openTimeColumn;
		public GridColumnController OpenTimeColumn
		{
			get {
				return openTimeColumn;
			}
			set {
				if (!___initialized) {
					openTimeColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_26_OPENTIMECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController openPriceColumn;
		public GridColumnController OpenPriceColumn
		{
			get {
				return openPriceColumn;
			}
			set {
				if (!___initialized) {
					openPriceColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_27_OPENPRICECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController stopLossColumn;
		public GridColumnController StopLossColumn
		{
			get {
				return stopLossColumn;
			}
			set {
				if (!___initialized) {
					stopLossColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_28_STOPLOSSCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController takeProfitColumn;
		public GridColumnController TakeProfitColumn
		{
			get {
				return takeProfitColumn;
			}
			set {
				if (!___initialized) {
					takeProfitColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_29_TAKEPROFITCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController closeTimeColumn;
		public GridColumnController CloseTimeColumn
		{
			get {
				return closeTimeColumn;
			}
			set {
				if (!___initialized) {
					closeTimeColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_30_CLOSETIMECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController closePriceColumn;
		public GridColumnController ClosePriceColumn
		{
			get {
				return closePriceColumn;
			}
			set {
				if (!___initialized) {
					closePriceColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_31_CLOSEPRICECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController commissionColumn;
		public GridColumnController CommissionColumn
		{
			get {
				return commissionColumn;
			}
			set {
				if (!___initialized) {
					commissionColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_32_COMMISSIONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController swapColumn;
		public GridColumnController SwapColumn
		{
			get {
				return swapColumn;
			}
			set {
				if (!___initialized) {
					swapColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_33_SWAPCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController profitColumn;
		public GridColumnController ProfitColumn
		{
			get {
				return profitColumn;
			}
			set {
				if (!___initialized) {
					profitColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_34_PROFITCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController commentColumn;
		public GridColumnController CommentColumn
		{
			get {
				return commentColumn;
			}
			set {
				if (!___initialized) {
					commentColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_35_COMMENTCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController expirationColumn;
		public GridColumnController ExpirationColumn
		{
			get {
				return expirationColumn;
			}
			set {
				if (!___initialized) {
					expirationColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_36_EXPIRATIONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController magicNumberColumn;
		public GridColumnController MagicNumberColumn
		{
			get {
				return magicNumberColumn;
			}
			set {
				if (!___initialized) {
					magicNumberColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_37_MAGICNUMBERCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController balanceColumn;
		public GridColumnController BalanceColumn
		{
			get {
				return balanceColumn;
			}
			set {
				if (!___initialized) {
					balanceColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_38_BALANCECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController showColumnsCmstr;
		public ComboController ShowColumnsCmstr
		{
			get {
				return showColumnsCmstr;
			}
			set {
				if (!___initialized) {
					showColumnsCmstr= value;
					changed[OrdersGridControllerProps.PROPERTY_39_SHOWCOLUMNSCMSTR_ID] = true;
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
			if (OrdersGridControllerProps.RmiGetProperty(this, propertyId, out value))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (!OrdersGridControllerProps.RmiSetProperty(this, propertyId, value))
				base.RmiSetProperty(propertyId, value);
		}
	}
}
