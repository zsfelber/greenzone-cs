using GreenZoneFxEngine.Trading;
using GreenZoneFxEngine.Types;
using GreenZoneFxEngine.Util;
using GreenZoneUtil.GreenRmi;
using GreenZoneUtil.ViewController;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace GreenZoneFxEngine.Trading
{
	public static class ServerUserRuntimeProps
	{
		public const int PROPERTY_11_SELECTEDORDER_ID = 11;
		public static bool RmiGetProperty(IServerUserRuntime controller, int propertyId, out object value, bool goToParent)
		{
			if (goToParent) {
				if (UserRuntimeProps.RmiGetProperty(controller, propertyId, out value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ServerUserRuntimeProps.PROPERTY_11_SELECTEDORDER_ID:
					value = controller.SelectedOrder;
					return true;
				default:
					value = null;
					return false;
			}
		}
		public static bool RmiSetProperty(IServerUserRuntime controller, int propertyId, object value, bool goToParent)
		{
			if (goToParent) {
				if (UserRuntimeProps.RmiSetProperty(controller, propertyId, value, true)) {
					return true;
				}
			}
			switch (propertyId)
			{
				case ServerUserRuntimeProps.PROPERTY_11_SELECTEDORDER_ID:
					controller.SelectedOrder = (IOrder) value;
					return true;
				default:
					return false;
			}
		}
		public static void Initialize(IServerUserRuntime controller, GreenRmiObjectBuffer buffer, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.Initialize(controller, buffer, true);
			}
		}

		public static void AddDependencies(IServerUserRuntime controller, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.AddDependencies(controller, true);
			}
		}

		public static void SerializationRead(IServerUserRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.SerializationRead(controller, info, context, true);
			}
			controller.SelectedOrder = (IOrder) info.GetValue("SelectedOrder", typeof(IOrder));
		}

		public static void SerializationWrite(IServerUserRuntime controller, SerializationInfo info, StreamingContext context, bool goToParent)
		{
			if (goToParent) {
				UserRuntimeProps.SerializationWrite(controller, info, context, true);
			}
			info.AddValue("SelectedOrder", controller.SelectedOrder);
		}

	}
	public abstract class ServerUserRuntimeBase : UserRuntimeEx, IServerUserRuntime
	{

		bool ___initialized = false;

		public event PropertyChangedEventHandler IServerUserRuntime_SelectedOrder_Changed;

		public ServerUserRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, ISeriesManagerCache cache)
			: base(rmiManager, parent, cache)
		{
			___initialized = true;
			ServerUserRuntimeProps.AddDependencies(this, false);
		}

		public ServerUserRuntimeBase(GreenRmiManager rmiManager, IChartRuntime parent, IExecSession session, ISeriesManagerCache icache)
			: base(rmiManager, parent, session, icache)
		{
			___initialized = true;
			ServerUserRuntimeProps.AddDependencies(this, false);
		}

		public ServerUserRuntimeBase(GreenRmiManager rmiManager, GreenRmiObjectBuffer buffer)
			: base(rmiManager, buffer)
		{
			ServerUserRuntimeProps.Initialize(this, buffer, false);
			___initialized = true;
			ServerUserRuntimeProps.AddDependencies(this, false);
			if (!___initialized) Console.Write(0);  // Omit compiler warnings...
		}

		protected ServerUserRuntimeBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			ServerUserRuntimeProps.SerializationRead(this, info, context, false);
			___initialized = true;
			ServerUserRuntimeProps.AddDependencies(this, false);
		}

		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			ServerUserRuntimeProps.SerializationWrite(this, info, context, false);
		}

		public abstract Double MarketInfo(symbol symbol, MarketInfoConst type);

		public abstract Double MarketInfo(String _symbol, MarketInfoConst type);

		public abstract Int32 GetLastError();

		public abstract String symbol();

		public abstract Int32 iBars(String _symbol, TimePeriodConst timeframe);

		public abstract Int32 iBarShift(String _symbol, TimePeriodConst timeframe, datetime time, Boolean exact);

		public abstract Double iClose(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iHigh(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Int32 Highest(String _symbol, TimePeriodConst timeframe, SeriesArrayPool type, Int32 count, Int32 start);

		public abstract Int32 iHighest(String _symbol, TimePeriodConst timeframe, SeriesArrayPool type, Int32 count, Int32 start);

		public abstract Double iLow(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Int32 Lowest(String _symbol, TimePeriodConst timeframe, SeriesArrayPool type, Int32 count, Int32 start);

		public abstract Int32 iLowest(String _symbol, TimePeriodConst timeframe, SeriesArrayPool type, Int32 count, Int32 start);

		public abstract Double iOpen(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract datetime iTime(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iVolume(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Int32 iBars(symbol symbol, TimePeriodConst timeframe);

		public abstract Int32 iBarShift(symbol symbol, TimePeriodConst timeframe, datetime time, Boolean exact);

		public abstract Double iClose(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iHigh(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Int32 iHighest(symbol symbol, TimePeriodConst timeframe, SeriesArrayPool type, Int32 count, Int32 start);

		public abstract Double iLow(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Int32 iLowest(symbol symbol, TimePeriodConst timeframe, SeriesArrayPool type, Int32 count, Int32 start);

		public abstract Double iOpen(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract datetime iTime(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iVolume(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract GeneralFile FileOpen(String filename, FileConstraint mode);

		public abstract GeneralFile FileOpen(String filename, FileConstraint mode, params String[] delimiters);

		public abstract GeneralFile FileOpenHistory(String filename, FileConstraint mode);

		public abstract GeneralFile FileOpenHistory(String filename, FileConstraint mode, params String[] delimiters);

		public abstract void FileClose(GeneralFile file);

		public abstract void FileDelete(String filename);

		public abstract void FileFlush(GeneralFile file);

		public abstract Boolean FileIsEnding(GeneralFile file);

		public abstract Boolean FileIsLineEnding(GeneralFile file);

		public abstract Int32 FileReadArray<T>(GeneralFile file, T[] array, Int32 start, Int32 count);

		public abstract Double FileReadDouble(GeneralFile file, FileDoubleType size);

		public abstract Int32 FileReadInteger(GeneralFile file, FileIntegerType size);

		public abstract Double FileReadNumber(GeneralFile file);

		public abstract String FileReadString(GeneralFile file, Int32 length);

		public abstract Boolean FileSeek(GeneralFile file, Int64 offset, SeekOrigin origin);

		public abstract Int64 FileSize(GeneralFile file);

		public abstract Int64 FileTell(GeneralFile file);

		public abstract Int32 FileWrite(GeneralFile file, params Object[] args);

		public abstract Int32 FileWriteArray<T>(GeneralFile file, T[] args, Int32 start, Int32 count);

		public abstract Int32 FileWriteDouble(GeneralFile file, Double value, FileDoubleType size);

		public abstract Int32 FileWriteInteger(GeneralFile file, Int32 value, FileIntegerType size);

		public abstract Int32 FileWriteString(GeneralFile file, String value, Int32 length);

		public abstract void Sleep(Int32 millis);

		public abstract void Alert(String info);

		public abstract Double MathAbs(Double d);

		public abstract Int32 MathAbs(Int32 d);

		public abstract Int32 MathMax(Int32 n1, Int32 n2);

		public abstract Int32 MathMin(Int32 n1, Int32 n2);

		public abstract Double MathMax(Double n1, Double n2);

		public abstract Double MathMin(Double n1, Double n2);

		public abstract Int32 MathRand();

		public abstract Double NormalizeDouble(Double d, Int32 precision);

		public abstract String DoubleToStr(Double d, Int32 digits);

		public abstract void MathSrand(Int32 seed);

		public abstract Double MathRound(Double d);

		public abstract Double MathPow(Double d, Double p);

		public abstract Int32 StringLen(String str);

		public abstract String StringSubstr(String text, Int32 start, Int32 length);

		public abstract String StringConcatenate(params Object[] args);

		public abstract Int32 ArraySize(DArr array);

		public abstract Int32 ArraySize(IArr array);

		public abstract Int32 ArraySize<T>(T[] array);

		public abstract Int32 ArrayInitialize(Double[] array, Double value);

		public abstract Int32 ArrayInitialize(Double[,] array, Double value);

		public abstract Int32 ArrayInitialize(DArr array, Double value);

		public abstract Int32 ArrayResize<T>(ref T[] array, Int32 new_size);

		public abstract datetime StrToTime(String datestr);

		public abstract String TimeToStr(datetime date, DateTimeFormat mode);

		public abstract Int32 TimeYear(datetime date);

		public abstract Int32 TimeMonth(datetime date);

		public abstract Int32 TimeDay(datetime date);

		public abstract DayOfWeek TimeDayOfWeek(datetime date);

		public abstract Int32 TimeDayOfYear(datetime date);

		public abstract Int32 TimeHour(datetime date);

		public abstract Int32 TimeMinute(datetime date);

		public abstract Int32 TimeSecond(datetime date);

		public abstract Int32 GetTickCount();

		public abstract void Print(String msg);

		public abstract void Print(params Object[] msgs);

		public abstract String orderSymbol();

		public abstract Boolean OrderSelect(Int32 index, TradeSelectMode select, TradePool pool);

		public abstract IOrder OrderSend(String _symbol, OrderType cmd, Double volume, Double price, Int32 slippage, Double stoploss, Double takeprofit, String comment, Int32 magic, datetime expirationDatetime, Color arrow_color);

		public abstract IOrder OrderSend(symbol symbol, OrderType cmd, Double volume, Double price, Int32 slippage, Double stoploss, Double takeprofit, String comment, Int32 magic, datetime expirationDatetime, Color arrow_color);

		public abstract IOrder OrderClose(Int32 ticket, Double lots, Double price, Int32 slippage, Color color);

		public abstract IOrder OrderDelete(Int32 ticket, Color color);

		public abstract IOrder OrderModify(Int32 ticket, Double price, Double stoploss, Double takeprofit, datetime expiration, Color color);

		public abstract Double GetIndicatorValue<T>(String _symbol, TimePeriodConst timeframe, IndicatorLine _mode, Int32 shift, params Object[] args)
			where T : IIndicatorRuntime;

		public abstract Double GetIndicatorValue<T>(String _symbol, TimePeriodConst timeframe, Int32 mode, Int32 shift, params Object[] args)
			where T : IIndicatorRuntime;

		public abstract Double GetIndicatorValue<T>(symbol symbol, TimePeriodConst timeframe, IndicatorLine _mode, Int32 shift, params Object[] args)
			where T : IIndicatorRuntime;

		public abstract Double GetIndicatorValue<T>(symbol symbol, TimePeriodConst timeframe, Int32 mode, Int32 shift, params Object[] args)
			where T : IIndicatorRuntime;

		public abstract Double GetIndicatorValue(String _symbol, TimePeriodConst timeframe, String name, IndicatorLine _mode, Int32 shift, params Object[] args);

		public abstract Double GetIndicatorValue(String _symbol, TimePeriodConst timeframe, String name, Int32 mode, Int32 shift, params Object[] args);

		public abstract Double GetIndicatorValue(symbol symbol, TimePeriodConst timeframe, String name, IndicatorLine _mode, Int32 shift, params Object[] args);

		public abstract Double GetIndicatorValue(symbol symbol, TimePeriodConst timeframe, String name, Int32 mode, Int32 shift, params Object[] args);

		public abstract IArraySeriesManagerCache GetIndicatorCacheOnArray(DArr priceData);

		public abstract Double iCustom(String _symbol, TimePeriodConst timeframe, String name, Int32 mode, Int32 shift, params Object[] args);

		public abstract Double iCustom(String _symbol, TimePeriodConst timeframe, String name, IndicatorLine mode, Int32 shift, params Object[] args);

		public abstract Double iCustom(symbol symbol, TimePeriodConst timeframe, String name, IndicatorLine mode, Int32 shift, params Object[] args);

		public abstract Double iAC(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iAC(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iAD(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iAD(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iAlligator(String _symbol, TimePeriodConst timeframe, Int32 jaw_period, Int32 jaw_shift, Int32 teeth_period, Int32 teeth_shift, Int32 lips_period, Int32 lips_shift, MovingAverageMethod ma_method, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iAlligator(symbol symbol, TimePeriodConst timeframe, Int32 jaw_period, Int32 jaw_shift, Int32 teeth_period, Int32 teeth_shift, Int32 lips_period, Int32 lips_shift, MovingAverageMethod ma_method, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iADX(String _symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iADX(symbol symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iATR(String _symbol, TimePeriodConst timeframe, Int32 period, Int32 shift);

		public abstract Double iATR(symbol symbol, TimePeriodConst timeframe, Int32 period, Int32 shift);

		public abstract Double iAO(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iAO(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iBearsPower(String _symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iBearsPower(symbol symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iBands(String _symbol, TimePeriodConst timeframe, Int32 period, Double deviation, Int32 bands_shift, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iBands(symbol symbol, TimePeriodConst timeframe, Int32 period, Double deviation, Int32 bands_shift, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iBands(String _symbol, TimePeriodConst timeframe, Int32 period, Double deviation, Int32 bands_shift, PriceConstant applied_price, MovingAverageMethod ma_method, IndicatorLine mode, Int32 shift);

		public abstract Double iBands(symbol symbol, TimePeriodConst timeframe, Int32 period, Double deviation, Int32 bands_shift, PriceConstant applied_price, MovingAverageMethod ma_method, IndicatorLine mode, Int32 shift);

		public abstract Double iBandsOnArray(DArr array, Int32 total, Int32 period, Int32 deviation, Int32 bands_shift, IndicatorLine mode, Int32 shift);

		public abstract Double iBandsOnArray(DArr array, Int32 total, Int32 period, Int32 deviation, Int32 bands_shift, MovingAverageMethod ma_method, IndicatorLine mode, Int32 shift);

		public abstract Double iBullsPower(String _symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iBullsPower(symbol symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iCCI(String _symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iCCI(symbol symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iCCIOnArray(DArr array, Int32 total, Int32 period, Int32 shift);

		public abstract Double iDeMarker(String _symbol, TimePeriodConst timeframe, Int32 period, Int32 shift);

		public abstract Double iDeMarker(symbol symbol, TimePeriodConst timeframe, Int32 period, Int32 shift);

		public abstract Double iEnvelopes(String _symbol, TimePeriodConst timeframe, Int32 ma_period, MovingAverageMethod ma_method, Int32 ma_shift, PriceConstant applied_price, Double deviation, IndicatorLine mode, Int32 shift);

		public abstract Double iEnvelopes(symbol symbol, TimePeriodConst timeframe, Int32 ma_period, MovingAverageMethod ma_method, Int32 ma_shift, PriceConstant applied_price, Double deviation, IndicatorLine mode, Int32 shift);

		public abstract Double iEnvelopesOnArray(DArr array, Int32 total, Int32 ma_period, MovingAverageMethod ma_method, Int32 ma_shift, Double deviation, IndicatorLine mode, Int32 shift);

		public abstract Double iForce(String _symbol, TimePeriodConst timeframe, Int32 period, MovingAverageMethod ma_method, PriceConstant applied_price, Int32 shift);

		public abstract Double iForce(symbol symbol, TimePeriodConst timeframe, Int32 period, MovingAverageMethod ma_method, PriceConstant applied_price, Int32 shift);

		public abstract Double iFractals(String _symbol, TimePeriodConst timeframe, IndicatorLine mode, Int32 shift);

		public abstract Double iFractals(symbol symbol, TimePeriodConst timeframe, IndicatorLine mode, Int32 shift);

		public abstract Double iGator(String _symbol, TimePeriodConst timeframe, Int32 jaw_period, Int32 jaw_shift, Int32 teeth_period, Int32 teeth_shift, Int32 lips_period, Int32 lips_shift, MovingAverageMethod ma_method, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iGator(symbol symbol, TimePeriodConst timeframe, Int32 jaw_period, Int32 jaw_shift, Int32 teeth_period, Int32 teeth_shift, Int32 lips_period, Int32 lips_shift, MovingAverageMethod ma_method, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iIchimoku(String _symbol, TimePeriodConst timeframe, Int32 tenkan_sen, Int32 kijun_sen, Int32 senkou_span_b, IndicatorLine mode, Int32 shift);

		public abstract Double iIchimoku(symbol symbol, TimePeriodConst timeframe, Int32 tenkan_sen, Int32 kijun_sen, Int32 senkou_span_b, IndicatorLine mode, Int32 shift);

		public abstract Double iBWMFI(String _symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iBWMFI(symbol symbol, TimePeriodConst timeframe, Int32 shift);

		public abstract Double iMomentum(String _symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iMomentum(symbol symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iMomentumOnArray(DArr array, Int32 total, Int32 period, Int32 shift);

		public abstract Double iMFI(String _symbol, TimePeriodConst timeframe, Int32 period, Int32 shift);

		public abstract Double iMFI(symbol symbol, TimePeriodConst timeframe, Int32 period, Int32 shift);

		public abstract Double iMA(String _symbol, TimePeriodConst timeframe, Int32 period, Int32 ma_shift, MovingAverageMethod ma_method, PriceConstant applied_price, Int32 shift);

		public abstract Double iMA(symbol symbol, TimePeriodConst timeframe, Int32 period, Int32 ma_shift, MovingAverageMethod ma_method, PriceConstant applied_price, Int32 shift);

		public abstract Double iPrice(String _symbol, TimePeriodConst timeframe, Int32 period, PriceConstant type, Int32 shift);

		public abstract Double iPrice(symbol symbol, TimePeriodConst timeframe, Int32 period, PriceConstant type, Int32 shift);

		public abstract Double iMAOnArray(DArr array, Int32 total, Int32 period, Int32 ma_shift, MovingAverageMethod ma_method, Int32 shift);

		public abstract Double iOsMA(String _symbol, TimePeriodConst timeframe, Int32 fast_ema_period, Int32 slow_ema_period, Int32 signal_period, PriceConstant applied_price, Int32 shift);

		public abstract Double iOsMA(symbol symbol, TimePeriodConst timeframe, Int32 fast_ema_period, Int32 slow_ema_period, Int32 signal_period, PriceConstant applied_price, Int32 shift);

		public abstract Double iMACD(String _symbol, TimePeriodConst timeframe, Int32 fast_ema_period, Int32 slow_ema_period, Int32 signal_period, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iMACD(symbol symbol, TimePeriodConst timeframe, Int32 fast_ema_period, Int32 slow_ema_period, Int32 signal_period, PriceConstant applied_price, IndicatorLine mode, Int32 shift);

		public abstract Double iOBV(String _symbol, TimePeriodConst timeframe, PriceConstant applied_price, Int32 shift);

		public abstract Double iOBV(symbol symbol, TimePeriodConst timeframe, PriceConstant applied_price, Int32 shift);

		public abstract Double iSAR(String _symbol, TimePeriodConst timeframe, Double step, Double maximum, Int32 shift);

		public abstract Double iSAR(symbol symbol, TimePeriodConst timeframe, Double step, Double maximum, Int32 shift);

		public abstract Double iRSI(String _symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iRSI(symbol symbol, TimePeriodConst timeframe, Int32 period, PriceConstant applied_price, Int32 shift);

		public abstract Double iRSIOnArray(DArr array, Int32 total, Int32 period, Int32 shift);

		public abstract Double iRVI(String _symbol, TimePeriodConst timeframe, Int32 period, IndicatorLine mode, Int32 shift);

		public abstract Double iRVI(symbol symbol, TimePeriodConst timeframe, Int32 period, IndicatorLine mode, Int32 shift);

		public abstract Double iStdDev(String _symbol, TimePeriodConst timeframe, Int32 ma_period, Int32 ma_shift, MovingAverageMethod ma_method, PriceConstant applied_price, Int32 shift);

		public abstract Double iStdDev(symbol symbol, TimePeriodConst timeframe, Int32 ma_period, Int32 ma_shift, MovingAverageMethod ma_method, PriceConstant applied_price, Int32 shift);

		public abstract Double iStdDevOnArray(DArr array, Int32 total, Int32 ma_period, Int32 ma_shift, MovingAverageMethod ma_method, Int32 shift);

		public abstract Double iStochastic(String _symbol, TimePeriodConst timeframe, Int32 _Kperiod, Int32 _Dperiod, Int32 slowing, Int32 method, Int32 price_field, IndicatorLine mode, Int32 shift);

		public abstract Double iStochastic(symbol symbol, TimePeriodConst timeframe, Int32 _Kperiod, Int32 _Dperiod, Int32 slowing, Int32 method, Int32 price_field, IndicatorLine mode, Int32 shift);

		public abstract Double iWPR(String _symbol, TimePeriodConst timeframe, Int32 period, Int32 shift);

		public abstract Double iWPR(symbol symbol, TimePeriodConst timeframe, Int32 period, Int32 shift);

		public abstract Int32 WindowFind(String name);

		public abstract void WindowRedraw();

		public abstract Boolean ObjectCreate(String name, GraphObjectType type, Int32 window, datetime time1, Double price1, Int32 time2, Double price2, Int32 time3, Double price3);

		public abstract GraphObjectType ObjectType(String name);

		public abstract Int32 ObjectFind(String name);

		public abstract String ObjectName(GraphObjectProperty index);

		public abstract Int32 ObjectsTotal(Int32 type);

		public abstract Boolean ObjectMove(String name, Int32 point, datetime time1, Double price1);

		public abstract Double ObjectGet(String name, GraphObjectProperty index);

		public abstract Boolean ObjectSet(String name, GraphObjectProperty index, Double value);

		public abstract Boolean ObjectSet(String name, GraphObjectProperty index, Int32 value);

		public abstract Boolean ObjectSet(String name, GraphObjectProperty index, DrawingStylesWidth1 value);

		public abstract Boolean ObjectSet(String name, GraphObjectProperty index, Color value);

		public abstract Boolean ObjectSetText(String name, String text, Int32 font_size, String font, Color text_color);

		public abstract Int32 ObjectsDeleteAll(Int32 window, GraphObjectType type);

		public abstract Boolean ObjectDelete(String name);

		public abstract void ObjectsRedraw();

		public abstract Boolean CompareDouble(Double A, Double B);

		public abstract Int32 i(Boolean b);

		public abstract Double nd(Double d);

		public abstract String ErrorDescription(Int32 e);

		public abstract void printError(String err);

		public abstract void printLog(String msg, Int32 severity);


		public new virtual IServerChartRuntime Parent
		{
			get {
				return (IServerChartRuntime) ((IUserRuntime)this).Parent;
			}
			set {
				((IUserRuntime)this).Parent = value;
			}
		}

		public new virtual IServerSeriesManagerRuntime SeriesManager
		{
			get {
				return (IServerSeriesManagerRuntime) ((IUserRuntime)this).SeriesManager;
			}
			set {
				((IUserRuntime)this).SeriesManager = value;
			}
		}

		public new virtual IServerSeriesManagerCache Cache
		{
			get {
				return (IServerSeriesManagerCache) ((IUserRuntime)this).Cache;
			}
			set {
				((IUserRuntime)this).Cache = value;
			}
		}

		public virtual Int32 Bars
		{
			get {
				return Cache.Bars;
			}
		}

		public virtual Double Bid
		{
			get {
				return Cache.Bid;
			}
		}

		public virtual Double Ask
		{
			get {
				return Cache.Ask;
			}
		}

		public virtual Double Point
		{
			get {
				return Cache.Point;
			}
		}

		public virtual Int32 Digits
		{
			get {
				return Cache.Digits;
			}
		}

		public virtual DArr Open
		{
			get {
				return Cache.Open;
			}
		}

		public virtual DArr Low
		{
			get {
				return Cache.Low;
			}
		}

		public virtual DArr High
		{
			get {
				return Cache.High;
			}
		}

		public virtual DArr Close
		{
			get {
				return Cache.Close;
			}
		}

		public virtual LArr LTime
		{
			get {
				return Cache.LTime;
			}
		}

		public virtual LArrAsIArr Time
		{
			get {
				return Cache.Time;
			}
		}

		public virtual DArr Volume
		{
			get {
				return Cache.Volume;
			}
		}

		public virtual symbol Symbol
		{
			get {
				return Cache.Symbol;
			}
		}

		public virtual TimePeriodConst Period
		{
			get {
				return Cache.Period;
			}
		}

		public virtual String AccountServer
		{
			get {
				return Environment.AccountServer;
			}
		}

		public virtual String AccountCurrency
		{
			get {
				return Environment.AccountCurrency;
			}
		}

		public virtual Int32 AccountNumber
		{
			get {
				return Environment.AccountNumber;
			}
		}

		public virtual Int32 AccountFreeMarginMode
		{
			get {
				return Environment.AccountFreeMarginMode;
			}
		}

		public virtual Int32 AccountStopoutLevel
		{
			get {
				return Environment.AccountStopoutLevel;
			}
		}

		public virtual Int32 AccountStopoutMode
		{
			get {
				return Environment.AccountStopoutMode;
			}
		}

		public virtual Boolean IsConnected
		{
			get {
				return Environment.IsConnected;
			}
		}

		public virtual Boolean IsStopped
		{
			get {
				return Environment.IsStopped;
			}
		}

		public virtual Boolean IsTesting
		{
			get {
				return !Environment.EnvironmentType.IsOnline();
			}
		}

		public virtual Int32 LastError
		{
			get {
				return Environment.LastError;
			}
		}

		public virtual datetime TimeCurrent
		{
			get {
				return Environment.Session.Time;
			}
		}

		IOrder _IServerUserRuntime_SelectedOrder;
		public virtual IOrder SelectedOrder
		{
			get {
				return _IServerUserRuntime_SelectedOrder;
			}
			set {
				if (_IServerUserRuntime_SelectedOrder != value) {
					_IServerUserRuntime_SelectedOrder= value;
					changed[ServerUserRuntimeProps.PROPERTY_11_SELECTEDORDER_ID] = true;
					if (IServerUserRuntime_SelectedOrder_Changed != null)
						IServerUserRuntime_SelectedOrder_Changed(this, new PropertyChangedEventArgs("SelectedOrder", value));
				}
			}
		}

		public virtual OrderType OrderType
		{
			get {
				return SelectedOrder.Type;
			}
		}

		public virtual symbol OrderSymbol
		{
			get {
				return SelectedOrder.Symbol;
			}
		}

		public virtual Int32 OrderTicket
		{
			get {
				return SelectedOrder.Ticket;
			}
		}

		public virtual Double OrderLots
		{
			get {
				return SelectedOrder.Lots;
			}
		}

		public virtual Double OrderStopLoss
		{
			get {
				return SelectedOrder.StopLoss;
			}
		}

		public virtual Double OrderTakeProfit
		{
			get {
				return SelectedOrder.TakeProfit;
			}
		}

		public virtual datetime OrderOpenTime
		{
			get {
				return SelectedOrder.OpenTime;
			}
		}

		public virtual Double OrderOpenPrice
		{
			get {
				return SelectedOrder.OpenPrice;
			}
		}

		public virtual datetime OrderCloseTime
		{
			get {
				return SelectedOrder.CloseTime;
			}
		}

		public virtual Double OrderClosePrice
		{
			get {
				return SelectedOrder.ClosePrice;
			}
		}

		public virtual String OrderComment
		{
			get {
				return SelectedOrder.Comment;
			}
		}

		public virtual datetime OrderExpiration
		{
			get {
				return SelectedOrder.Expiration;
			}
		}

		public virtual Int32 OrderMagicNumber
		{
			get {
				return SelectedOrder.MagicNumber;
			}
		}

		public virtual Double OrderCommission
		{
			get {
				return SelectedOrder.Commission;
			}
		}

		public virtual Double OrderProfit
		{
			get {
				return SelectedOrder.Profit;
			}
		}

		public virtual Double OrderSwap
		{
			get {
				return SelectedOrder.Swap;
			}
		}

		public virtual Int32 OrdersTotal
		{
			get {
				return Environment.Orders.Orders.Count;
			}
		}

		public virtual Int32 OrdersHistoryTotal
		{
			get {
				return Environment.Orders.OrdersHistory.Count;
			}
		}

		public abstract Int32 IndicatorCounted
		{
			get ;
		}

		public virtual Int32 WindowBarsPerChart
		{
			get {
				return Parent.WindowBarsPerChart;
			}
		}

		public virtual Int32 WindowFirstVisibleBar
		{
			get {
				return Parent.WindowFirstVisibleBar;
			}
		}


		public override object RmiGetProperty(int propertyId)
		{
			object value;
			if (ServerUserRuntimeProps.RmiGetProperty(this, propertyId, out value, false))
				return value;
			else
				return base.RmiGetProperty(propertyId);
		}
		public override void RmiSetProperty(int propertyId, object value)
		{
			if (ServerUserRuntimeProps.RmiSetProperty(this, propertyId, value, false))
				return;
			else 
				base.RmiSetProperty(propertyId, value);
		}
	}
}
