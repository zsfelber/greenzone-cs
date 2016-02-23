// MillionDollarFx.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <math.h>

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;
using namespace System::Threading;


#define Max(a,b) (((a)>=(b))?(a):(b))
#define Min(a,b) (((a)<=(b))?(a):(b))

double MathMax(double a,double b) {
    return Max(a,b);
}
double MathMin(double a,double b) {
    return Min(a,b);
}


struct MqlStr {
	int	len;
	char* string;
	MqlStr() {
	}
	MqlStr(int len, char* string) : len(len), string(string) {
	}
};


public ref class Invoker {

	static int cntid = 0;

	#define lifts_len 80
	double *lifts;
	double *lift_qs;
	int lifts_cur;

	double total_lift;
	double total_lift_q;

public:

	static Object ^lock = gcnew Object();

	int sessionId;

	static Dictionary<int,Invoker^>^ invokers = gcnew Dictionary<int,Invoker^>();

#ifdef DEBUG
	[DllImport("Kernel32.dll")]
    static bool AllocConsole();
	[DllImport("Kernel32.dll")]
    static bool FreeConsole();
#endif

	static Invoker() {
#ifdef DEBUG
		AllocConsole();
#endif
	}

	Invoker() {
		sessionId = ++cntid;
		lifts = new double[800];
		lift_qs = new double[800];
		lifts_cur = 0;
		invokers[sessionId] = this;
	}

	void Deinit() {
		delete lifts;
		delete lift_qs;
		invokers->Remove(sessionId);
	}

	void Invoke(int period, int hlperiod, 
		      int Mode_Grizzly, int Mode_Adaptive,
		      double Scalping_1Stick, 
		      double Scalping_Lift0, double Scalping_Lift1, 
		      double Scalping_Lift0_Spread, double Scalping_Lift1_Spread, 
		      double Scalping_Factor, double Asymm, double Scalping_Pips,
		      double Scalping_Lift_Plus, double Scalping_Adaptive_Max, 
		      double Scalping_Lift_Minus, double Scalping_Adaptive_Min, 
		      double Trailing_Resolution_Max, double Scalping_Adaptive_Deviation,
		      double Asymm_Max, int MaxOrders_Level,
		      double BBlower, double BBupper, 
		      double asymm, double trailingLimit, double trailingResolution,
		      double realAvgSpread, double point, double Bid, double Ask, double dist,
		      int _channelId, 
			  double *result, MqlStr *strings) {

		double scalpingLift0;
		double scalpingFactor;
		double spreadLiftRatio;

		//mdp 1.3/2
		MaxOrders_Level = MathMax(0,MaxOrders_Level);
		MaxOrders_Level = MathMin(4,MaxOrders_Level);

		scalpingLift0 = Scalping_Lift0;
		scalpingFactor = Scalping_Factor;

		double Scalping_Lift;
		spreadLiftRatio = (realAvgSpread/point - Scalping_Lift0_Spread ) / (Scalping_Lift1_Spread - Scalping_Lift0_Spread);
		spreadLiftRatio = MathMax(0,spreadLiftRatio);
		spreadLiftRatio = MathMin(1,spreadLiftRatio);

		//mdp 1.3/2
		double spreadLiftRatio2 = 1.0 - MaxOrders_Level / 4.0;
		spreadLiftRatio = (spreadLiftRatio+spreadLiftRatio2)/2.0;
		bool wasVirgin = lifts_cur<lifts_len;

		if (Mode_Adaptive) {
			if (wasVirgin) {
				Scalping_Lift = 0;
			} else {
				double avg=total_lift/lifts_len;
				double d;

				d = Scalping_Adaptive_Deviation + spreadLiftRatio * Scalping_Adaptive_Max;
				double deviation = d * sqrt(total_lift_q/lifts_len - avg*avg);

				Scalping_Lift = (avg+deviation)/point;

				Scalping_Lift = MathMin( Scalping_Lift1, Scalping_Lift );
				Scalping_Lift = MathMax( Scalping_Lift0, Scalping_Lift );
			}
		} else {
			Scalping_Lift = scalpingLift0 + spreadLiftRatio * (Scalping_Lift1 - Scalping_Lift0);
		}

		double lift = Scalping_Lift * point;

		int isJustCrossed = 0;
		double scalp;
		double price;
		if (lift == 0) {
			scalp = 0;
			price = 0;
		} else {
			if (Bid - dist*Scalping_1Stick < BBlower - lift) {
				isJustCrossed = -1;
				price = BBlower - lift + dist*Scalping_1Stick;
			} else if (Bid + dist*Scalping_1Stick > BBupper + lift) {
				isJustCrossed = 1;
				price = BBlower + lift - dist*Scalping_1Stick;
			} else {
				price = 0;
			}

			if (Scalping_Pips == 0) {
				scalp = scalpingFactor * lift;
			} else {
				scalp = Scalping_Pips * point;
			}
		}

		double high = result[7] - BBupper + dist*Scalping_1Stick;
		double low = BBlower - result[8] + dist*Scalping_1Stick;

		if (result[7] || result[8]) {
			bool hasBeenMod = false;
			double v2;
			if (result[7] && high>=Scalping_Lift0*0.333333333*point) {
				v2 = high*high;
				lifts[lifts_cur] = high;
				lift_qs[lifts_cur] = v2;
				total_lift += high;
				total_lift_q += v2;
				hasBeenMod = true;
				lifts_cur++;
				if (lifts_cur==lifts_len*8) {
					memcpy(lifts,lifts+lifts_len*7,lifts_len*sizeof(double));
					memcpy(lift_qs,lift_qs+lifts_len*7,lifts_len*sizeof(double));
					lifts_cur=lifts_len;
				}
			} else if (result[8] && low>=Scalping_Lift0*0.333333333*point) {
				v2 = low*low;
				lifts[lifts_cur] = -low;
				lift_qs[lifts_cur] = v2;
				total_lift -= low;
				total_lift_q += v2;
				hasBeenMod = true;
				lifts_cur++;
				if (lifts_cur==lifts_len*8) {
					memcpy(lifts,lifts+lifts_len*7,lifts_len*sizeof(double));
					memcpy(lift_qs,lift_qs+lifts_len*7,lifts_len*sizeof(double));
					lifts_cur=lifts_len;
				}
			}

			if (hasBeenMod) {

				if (wasVirgin) {
					bool isVirgin = lifts_cur<lifts_len;

					if (!isVirgin) {
						reverseLifts();
					}
#ifdef DEBUG
					Console::WriteLine("sessionId:"+sessionId+"  hasBeenMod && wasVirgin  isVirgin:"+isVirgin+" lifts_len:"+lifts_len);
#endif
				} else {
					total_lift -= lifts[lifts_cur-lifts_len];
					total_lift_q -= lift_qs[lifts_cur-lifts_len];
				}
			}
		}

		result[0] = scalpingLift0;
		result[1] = lift;
		result[2] = isJustCrossed;
		result[3] = scalp;
		result[4] = asymm;
		result[5] = price;
		result[6] = trailingResolution;
		result[7] = high;
		result[8] = low;
		result[9] = lifts[0];
	}


private:

	void reverseLifts() {
	   double v;
	   for (int i=0; i<lifts_len/2; i++) {
		  v = lifts[lifts_len-1-i];
		  lifts[lifts_len-1-i] = lifts[i];
		  lifts[i] = v;

		  v = lift_qs[lifts_len-1-i];
		  lift_qs[lifts_len-1-i] = lift_qs[i];
		  lift_qs[i] = v;
	   }
	}

};







#define MT4_EXPFUNC __declspec(dllexport)
#define export extern "C" __declspec( dllexport )

#ifdef __cplusplus
extern "C"
{
#endif

	MT4_EXPFUNC void __stdcall Log(int sessionId, const char *message) {
		try {
			Monitor::Enter(Invoker::lock);
			Console::WriteLine("sessionId:"+sessionId+"  "+gcnew String(message));
		} finally {
			Monitor::Exit(Invoker::lock);
		}
	}

	MT4_EXPFUNC int __stdcall FxSubInit(MqlStr *login) {
		try {
			Monitor::Enter(Invoker::lock);
			Invoker ^invoker = gcnew Invoker();
			Log(invoker->sessionId, "Initialized");
			return invoker->sessionId;
		} catch (Exception ^ex) {
			Console::WriteLine("FxSubInit : "+ex);
			return 0;
		} finally {
			Monitor::Exit(Invoker::lock);
		}
	}

	MT4_EXPFUNC bool __stdcall FxSubDeinit(int sessionId, MqlStr *login) {
		try {
			Monitor::Enter(Invoker::lock);
			Invoker ^invoker = Invoker::invokers[sessionId];
			invoker->Deinit();
			Log(sessionId, "Deinitialized");
			return true;
		} catch (Exception ^ex) {
			Console::WriteLine("FxSubDeinit : "+ex);
			return false;
		} finally {
			Monitor::Exit(Invoker::lock);
		}
	}

	MT4_EXPFUNC bool __stdcall FxSubInvoke(int sessionId, 
								int period, int hlperiod, 
								int Mode_Grizzly, int Mode_Adaptive,
								double Scalping_1Stick, 
								double Scalping_Lift0, double Scalping_Lift1, 
								double Scalping_Lift0_Spread, double Scalping_Lift1_Spread, 
								double Scalping_Factor, double Asymm, double Scalping_Pips,
								double Scalping_Lift_Plus, double Scalping_Adaptive_Max, 
								double Scalping_Lift_Minus, double Scalping_Adaptive_Min, 
								double Trailing_Resolution_Max, double Scalping_Adaptive_Deviation,
								double Asymm_Max, int MaxOrders_Level,
								double BBlower, double BBupper, 
								double asymm, double trailingLimit, double trailingResolution,
								double realAvgSpread, double _point, double bid, double ask, double dist,
								int _channelId, 
								double *result, MqlStr *strings) {

		try {
			Invoker ^invoker;
			try {
				Monitor::Enter(Invoker::lock);
				invoker = Invoker::invokers[sessionId];
			} finally {
				Monitor::Exit(Invoker::lock);
			}

			invoker->Invoke(	period, hlperiod, 
								Mode_Grizzly, Mode_Adaptive,
								Scalping_1Stick, 
								Scalping_Lift0, Scalping_Lift1, 
								Scalping_Lift0_Spread, Scalping_Lift1_Spread, 
								Scalping_Factor, Asymm, Scalping_Pips,
								Scalping_Lift_Plus, Scalping_Adaptive_Max, 
								Scalping_Lift_Minus, Scalping_Adaptive_Min, 
								Trailing_Resolution_Max, Scalping_Adaptive_Deviation,
								Asymm_Max, MaxOrders_Level,
								BBlower, BBupper, 
								asymm, trailingLimit, trailingResolution,
								realAvgSpread, _point, bid, ask, dist,
								_channelId,
								result, strings);
			return true;
		} catch (Exception ^ex) {
			Console::WriteLine("FxSubInvoke : "+ex);
			return false;
		}
	}

#ifdef __cplusplus
}
#endif
