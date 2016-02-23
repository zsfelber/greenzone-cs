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
		public static bool RmiGetProperty(IOrdersGridController controller, int propertyId, out object value, bool goToParent)
		{
			switch (propertyId)
			{
				case OrdersGridControllerProps.PROPERTY_20_TICKETCOLUMN_ID:
					value = controller.TicketColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_21_EXPERTCOLUMN_ID:
					value = controller.ExpertColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_22_OPERATIONCOLUMN_ID:
					value = controller.OperationColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_23_TYPECOLUMN_ID:
					value = controller.TypeColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_24_SYMBOLCOLUMN_ID:
					value = controller.SymbolColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_25_LOTSCOLUMN_ID:
					value = controller.LotsColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_26_OPENTIMECOLUMN_ID:
					value = controller.OpenTimeColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_27_OPENPRICECOLUMN_ID:
					value = controller.OpenPriceColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_28_STOPLOSSCOLUMN_ID:
					value = controller.StopLossColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_29_TAKEPROFITCOLUMN_ID:
					value = controller.TakeProfitColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_30_CLOSETIMECOLUMN_ID:
					value = controller.CloseTimeColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_31_CLOSEPRICECOLUMN_ID:
					value = controller.ClosePriceColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_32_COMMISSIONCOLUMN_ID:
					value = controller.CommissionColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_33_SWAPCOLUMN_ID:
					value = controller.SwapColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_34_PROFITCOLUMN_ID:
					value = controller.ProfitColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_35_COMMENTCOLUMN_ID:
					value = controller.CommentColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_36_EXPIRATIONCOLUMN_ID:
					value = controller.ExpirationColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_37_MAGICNUMBERCOLUMN_ID:
					value = controller.MagicNumberColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_38_BALANCECOLUMN_ID:
					value = controller.BalanceColumn;
					return true;
				case OrdersGridControllerProps.PROPERTY_39_SHOWCOLUMNSCMSTR_ID:
					value = controller.ShowColumnsCmstr;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IOrdersGridController controller, int propertyId, object value, bool goToParent)
		{
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IOrdersGridController controller, GreenRmiObjectBuffer buffer, bool goToParent)
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

		public static void AddDependencies(IOrdersGridController controller, bool goToParent)
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

		public static void SerializationRead(IOrdersGridController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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

		public static void SerializationWrite(IOrdersGridController controller, SerializationInfo info, StreamingContext context, bool goToParent)
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
			OrdersGridControllerProps.AddDependencies(this, false);
		}

		public OrdersGridControllerBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			OrdersGridControllerProps.Initialize(this, buffer, false);
			___initialized = true;
			OrdersGridControllerProps.AddDependencies(this, false);
		}

		protected OrdersGridControllerBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			OrdersGridControllerProps.SerializationRead(this, info, context, false);
			___initialized = true;
			OrdersGridControllerProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			OrdersGridControllerProps.SerializationWrite(this, info, context, false);
		}


		GridColumnController _IOrdersGridController_TicketColumn;
		public GridColumnController TicketColumn
		{
			get {
				return _IOrdersGridController_TicketColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_TicketColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_20_TICKETCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_ExpertColumn;
		public GridColumnController ExpertColumn
		{
			get {
				return _IOrdersGridController_ExpertColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_ExpertColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_21_EXPERTCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_OperationColumn;
		public GridColumnController OperationColumn
		{
			get {
				return _IOrdersGridController_OperationColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_OperationColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_22_OPERATIONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_TypeColumn;
		public GridColumnController TypeColumn
		{
			get {
				return _IOrdersGridController_TypeColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_TypeColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_23_TYPECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_SymbolColumn;
		public GridColumnController SymbolColumn
		{
			get {
				return _IOrdersGridController_SymbolColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_SymbolColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_24_SYMBOLCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_LotsColumn;
		public GridColumnController LotsColumn
		{
			get {
				return _IOrdersGridController_LotsColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_LotsColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_25_LOTSCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_OpenTimeColumn;
		public GridColumnController OpenTimeColumn
		{
			get {
				return _IOrdersGridController_OpenTimeColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_OpenTimeColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_26_OPENTIMECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_OpenPriceColumn;
		public GridColumnController OpenPriceColumn
		{
			get {
				return _IOrdersGridController_OpenPriceColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_OpenPriceColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_27_OPENPRICECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_StopLossColumn;
		public GridColumnController StopLossColumn
		{
			get {
				return _IOrdersGridController_StopLossColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_StopLossColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_28_STOPLOSSCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_TakeProfitColumn;
		public GridColumnController TakeProfitColumn
		{
			get {
				return _IOrdersGridController_TakeProfitColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_TakeProfitColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_29_TAKEPROFITCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_CloseTimeColumn;
		public GridColumnController CloseTimeColumn
		{
			get {
				return _IOrdersGridController_CloseTimeColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_CloseTimeColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_30_CLOSETIMECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_ClosePriceColumn;
		public GridColumnController ClosePriceColumn
		{
			get {
				return _IOrdersGridController_ClosePriceColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_ClosePriceColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_31_CLOSEPRICECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_CommissionColumn;
		public GridColumnController CommissionColumn
		{
			get {
				return _IOrdersGridController_CommissionColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_CommissionColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_32_COMMISSIONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_SwapColumn;
		public GridColumnController SwapColumn
		{
			get {
				return _IOrdersGridController_SwapColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_SwapColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_33_SWAPCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_ProfitColumn;
		public GridColumnController ProfitColumn
		{
			get {
				return _IOrdersGridController_ProfitColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_ProfitColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_34_PROFITCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_CommentColumn;
		public GridColumnController CommentColumn
		{
			get {
				return _IOrdersGridController_CommentColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_CommentColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_35_COMMENTCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_ExpirationColumn;
		public GridColumnController ExpirationColumn
		{
			get {
				return _IOrdersGridController_ExpirationColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_ExpirationColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_36_EXPIRATIONCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_MagicNumberColumn;
		public GridColumnController MagicNumberColumn
		{
			get {
				return _IOrdersGridController_MagicNumberColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_MagicNumberColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_37_MAGICNUMBERCOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		GridColumnController _IOrdersGridController_BalanceColumn;
		public GridColumnController BalanceColumn
		{
			get {
				return _IOrdersGridController_BalanceColumn;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_BalanceColumn= value;
					changed[OrdersGridControllerProps.PROPERTY_38_BALANCECOLUMN_ID] = true;
				}
				else
				{
					throw new NotSupportedException("Readonly property already initialized.");
				}
			}
		}

		ComboController _IOrdersGridController_ShowColumnsCmstr;
		public ComboController ShowColumnsCmstr
		{
			get {
				return _IOrdersGridController_ShowColumnsCmstr;
			}
			set {
				if (!___initialized) {
					_IOrdersGridController_ShowColumnsCmstr= value;
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
			if (OrdersGridControllerProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (OrdersGridControllerProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
