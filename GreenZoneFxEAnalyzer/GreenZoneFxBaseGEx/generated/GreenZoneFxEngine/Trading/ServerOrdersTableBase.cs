using GreenZoneFxEngine.Trading;
using GreenZoneUtil.GreenRmi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerOrdersTableProps
	{
		public static bool RmiGetProperty(IServerOrdersTable controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (OrdersTableProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerOrdersTable controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (OrdersTableProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				default:
					return false;
			}
		}
		public static void Initialize(IServerOrdersTable controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				OrdersTableProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerOrdersTable controller, bool goToParent)
		{
			if (goToParent) {
				OrdersTableProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerOrdersTable controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrdersTableProps.SerializationRead(controller, info, context, true);
			}
		}

		public static void SerializationWrite(IServerOrdersTable controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				OrdersTableProps.SerializationWrite(controller, info, context, true);
			}
		}

	}
	public abstract class ServerOrdersTableBase : OrdersTableBase, IServerOrdersTable
	{

		bool ___initialized = false;


		public ServerOrdersTableBase(GreenRmiManager rmiManager)
			: base(rmiManager)
		{
			___initialized = true;
			ServerOrdersTableProps.AddDependencies(this, false);
		}

		public ServerOrdersTableBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerOrdersTableProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerOrdersTableProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerOrdersTableBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerOrdersTableProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerOrdersTableProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerOrdersTableProps.SerializationWrite(this, info, context, false);
		}

		public abstract List<O> GetOrders<O>(TradePool pool)
			where O : IOrder;

		public abstract void Load<O>(BinaryReader r, TradePool pool)
			where O : IOrder;

		public abstract void Save<O>(BinaryWriter w, TradePool pool)
			where O : IOrder;

		public abstract void Tick(Double Bid, Double Ask, Double Volume);

		public abstract void Add(ITradeOrder order);

		public abstract void Add(IHistoryOrder order);

		public abstract void Add(IHistoryOrderEtc order);

		public abstract void CloseOrDelete(ITradeOrder order);

		public abstract void GenerateFromChildren();

		public abstract void Generate<O>(List<O> orders, TradePool pool)
			where O : IOrder;

		public abstract void UpdateOrder(ITradeOrder order);


		public new virtual IServerOrdersTable Parent
		{
			get {
				return (IServerOrdersTable) ((IOrdersTable)this).Parent;
			}
			set {
				((IOrdersTable)this).Parent = value;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerOrdersTableProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerOrdersTableProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
