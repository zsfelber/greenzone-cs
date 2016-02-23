// MillionDollarFx.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Metatrader4_imports.h"
#include <string>
#include <sstream>
#include <math.h>

using namespace System;

int sessionId;

#define Max(a,b) (((a)>=(b))?(a):(b))
#define Min(a,b) (((a)<=(b))?(a):(b))

#define lifts_len 80
double lifts[800];
double lift_qs[800];
int lifts_cur = 0;

double total_lift;
double total_lift_q;

double MathMax(double a,double b) {
    return Max(a,b);
}
double MathMin(double a,double b) {
    return Min(a,b);
}

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

public ref class Invoker {

public:

	static void init(int _sessionId) {
		sessionId = _sessionId;
	}

	static void fun1(IntPtr p_arguments, IntPtr p_result, IntPtr p_strings) {

		double *arguments = (double*) (void*) p_arguments;
		double *result = (double*) (void*) p_result;
		MqlStr *strings = (MqlStr*) (void*) p_strings;

		const int period = arguments[0];
		const int hlperiod = arguments[1];
		const int Mode_Grizzly = arguments[2];
		const int Mode_Adaptive = arguments[3];
		const double Scalping_1Stick = arguments[4];
		const double Scalping_Lift0 = arguments[5];
		const double Scalping_Lift1 = arguments[6];
		const double Scalping_Lift0_Spread = arguments[7];
		const double Scalping_Lift1_Spread = arguments[8];
		const double Scalping_Factor = arguments[9];
		const double Asymm = arguments[10];
		const double Scalping_Pips = arguments[11];
		const double Scalping_Lift_Plus = arguments[12];
		const double Scalping_Adaptive_Max = arguments[13];
		const double Scalping_Lift_Minus = arguments[14];
		const double Scalping_Adaptive_Min = arguments[15];
		const double Trailing_Resolution_Max = arguments[16];
		const double Scalping_Adaptive_Deviation = arguments[17];
		const double Asymm_Max = arguments[18];
		int MaxOrders_Level = arguments[19];
		const double BBlower = arguments[20];
		const double BBupper = arguments[21];
		double asymm = arguments[22];
		double trailingLimit = arguments[23];
		double trailingResolution = arguments[24];
		const double realAvgSpread = arguments[25];
		const double point = arguments[26];
		const double Bid = arguments[27];
		const double Ask = arguments[28];
		const double dist = arguments[29];
		const int _channelId = arguments[30];

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
					std::stringstream ss;
					ss<<"hasBeenMod && wasVirgin  isVirgin:"<<isVirgin<<" lifts_len:"<<lifts_len;
					Log(sessionId,ss.str().c_str());
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


};


