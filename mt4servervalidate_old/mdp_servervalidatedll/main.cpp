/*=============================================================================
ServerValidate
=============================================================================*/

// This dll handles the communication to the web interface
//
//


//#define CURL_STATICLIB
//#define WIN32_LEAN_AND_MEAN

#include "stdafx.h"
#include "SmartLog.h"

#include <algorithm>
#include <vector>
#include "xmlParser.h"
#include "convert.h"
#include "CURLRetriever.h"
#include "HTTPRspCodeString.h"
#include "HTTPRspCodeClassString.h"
#include "HTTPRspCodeDescription.h"
#include "GenComputerString.h"
#include <string>
#include <sstream>
#include <jni.h>

#define Max(a,b) (((a)>=(b))?(a):(b))
#define Min(a,b) (((a)<=(b))?(a):(b))

map<int,map<int,int>> intBuffer;
map<int,map<int,double*>> doubleArrays;

double MathMax(double a,double b) {
    return Max(a,b);
}
double MathMin(double a,double b) {
    return Min(a,b);
}

int findArrayLast(double array[], int len) {
   for (int i=len-1; i>=0; i--) {
      if (0!=array[i]) {
         return (i);
      }
   }
   return (-1);
}

void reverseArray(double array[], int len) {
   double v;
   for (int i=0; i<len/2; i++) {
      v = array[len-1-i];
      array[len-1-i] = array[i];
      array[i] = v;
   }
}

#define MT4_EXPFUNC __declspec(dllexport)

LogSeverity LOG_SEVERITY = SEV_DEBUG_2;

#define export extern "C" __declspec( dllexport )




#ifdef __cplusplus
extern "C"
{
#endif


    //+------------------------------------------------------------------+
    //|                                                                  |
    //+------------------------------------------------------------------+

void initLog() {
	CLogFlags flags;
	__smartlog.SetFilePath("c:\\milliondollarpips.log");
	__smartlog.GetFlags(flags);
	flags.m_bLineFlag = true;
	flags.m_bFileFlag = true;
	__smartlog.SetFlags(flags);
}


#ifdef NO_VALIDATE_ZSF_ONLY
	MT4_EXPFUNC
#endif
	void  __stdcall ValidateUser(const char *server, const char *username, const int metatraderid, const int debug, int &validated, int &statuscode, char* returnbuffer)
    {

        std::string aOpenRequest;
        std::string requestResult;
        //std::string macAddr = GetMACAddr();
		std::string compID;
		try {
			compID = GetComputerString(25);
		} catch (std::wstring *exception) {
			compID = "12345";
			delete exception;
		}

        CURLRetriever::InitializeCURL();

        aOpenRequest = server;
        aOpenRequest += "/checkStatus.php?username=";

        aOpenRequest += username;
        aOpenRequest += "&compid=";
        aOpenRequest += compID;
        aOpenRequest += "&mt4acctid=";
        aOpenRequest += stringify(metatraderid);

        CURLRetriever urlRetriever;
		if (debug == 1)
			MessageBox(GetActiveWindow(),aOpenRequest.c_str(),"Request",MB_OK);

        HRESULT hResult = urlRetriever.Initialize(aOpenRequest.c_str());

        LONG lResponseCode = 0;
        std::string sRetrievedContent;
        std::string sRetrievingErrorDescription;
        unsigned int codeClass;
        int retries = 0;
        do
        {


            hResult = urlRetriever.Retrieve(lResponseCode, sRetrievedContent, sRetrievingErrorDescription);
            codeClass = lResponseCode / 100;
            retries++;
        }
        while (codeClass == 5 && retries < 10);

        if (codeClass > 2)
        {
            HTTPRspCodeDescription codeDescr;
            std::string codeStringText = "Communications problem with server.  Description: ";
            codeStringText += codeDescr.getInfo( lResponseCode );
            //strcpy(returnbuffer,codeStringText.c_str());
        }



        if (hResult != S_OK)
        {
            std::string codeStringText = "Communications problem with server.  Description: ";
            codeStringText += sRetrievingErrorDescription.c_str();
            //strcpy(returnbuffer,codeStringText.c_str());
        }

        urlRetriever.Uninitialize();
        CURLRetriever::UninitializeCURL();

		if (debug == 1)
			MessageBox(GetActiveWindow(),sRetrievedContent.c_str(),"Response",MB_OK);

        XMLNode xMainNode=XMLNode::parseString(sRetrievedContent.c_str(),"validateStatus");

        XMLNode xStatusCodeNode=xMainNode.getChildNode("status");

        if (xStatusCodeNode.getText() != NULL)
        {
            statuscode = convertToInt(xStatusCodeNode.getText());
        }
        else
        {
            statuscode = 0;
        }


        XMLNode xStatusTextNode=xMainNode.getChildNode("statustext");

        if (xStatusTextNode.getText() != NULL)
        {
            strcpy(returnbuffer,xStatusTextNode.getText());
        }
        else
        {
            strcpy(returnbuffer," ");
        }


        XMLNode xValidatedNode=xMainNode.getChildNode("validated");

        if (xValidatedNode.getText() != NULL)
        {
            validated = convertToInt(xValidatedNode.getText());
        }
        else
        {
            validated = 0;
        }

		aOpenRequest.resize(0);
        std::string().swap(aOpenRequest);

        sRetrievedContent.resize(0);
        std::string().swap(sRetrievedContent);

        sRetrievingErrorDescription.resize(0);
        std::string().swap(sRetrievingErrorDescription);
    }



	void InitializeRobot0(const char *username, const int metatraderid, const int debug, int &validated, int &statuscode, char* returnbuffer) {

		statuscode = 0;
		validated = 0;
#ifdef NO_VALIDATE_ZSF_ONLY
		initLog();
		statuscode = 1;
		validated = 1;
#else
		ValidateUser ("https://validation.openthinkingsystems.com", username, metatraderid, debug, validated, statuscode, returnbuffer);
#endif
	}

	// 1.2
	MT4_EXPFUNC void __stdcall InitializeRobot(const char *username, const int metatraderid, const int debug, int _validated[], int _statuscode[], char* returnbuffer) {
#ifdef NO_VALIDATE_ZSF_ONLY
		initLog();
#endif
		returnbuffer[0] = 0;
#ifdef NO_VALIDATE_ZSF_ONLY
		smartlog(SEV_DEBUG_1) << "InitializeRobot  metatraderid:"<< metatraderid;
#endif
		int &validated = intBuffer[1][0];
		int &statuscode = intBuffer[1][1];
		InitializeRobot0(username, metatraderid, debug, validated, statuscode, returnbuffer);
		_validated[0] = validated;
		_statuscode[0] = statuscode;
#ifdef NO_VALIDATE_ZSF_ONLY
		smartlog(SEV_DEBUG_1) << "InitializeRobot  validated:"<< validated << " statuscode:" << statuscode << " returnbuffer:"<<returnbuffer;
#endif
	}

	// Jfx
	int InitializeRobot13Impl(const char *username, const int metatraderid, const int debug, int &_validated, int &_statuscode, char* returnbuffer, int lifts_len) {
		int id0 = intBuffer[0][0];
		int id = 0;
		for (int i=1; i<=id0; i++) {
			if (intBuffer[i][2] == -1) {
				id = i;
				break;
			}
		}
		if (id == 0) {
			id = ++(intBuffer[0][0]);
		}
		int &validated = intBuffer[id][0];
		int &statuscode = intBuffer[id][1];
		intBuffer[id][2] = lifts_len;
		InitializeRobot0(username, metatraderid, debug, validated, statuscode, returnbuffer);
		_validated = validated;
		_statuscode = statuscode;
		return id;
	}

	// Jfx
	MT4_EXPFUNC int __cdecl InitializeRobotJfx(const char *username, const int metatraderid, const int debug, int *_validated, int *_statuscode, char** returnbuffer, int lifts_len) {
		*returnbuffer = new char[256];
		return InitializeRobot13Impl(username, metatraderid, debug, *_validated, *_statuscode, *returnbuffer, lifts_len);
	}

	// 1.3
	MT4_EXPFUNC int __stdcall InitializeRobot13(const char *username, const int metatraderid, const int debug, int _validated[], int _statuscode[], char* returnbuffer, int lifts_len) {
		return InitializeRobot13Impl(username, metatraderid, debug, _validated[0], _statuscode[0], returnbuffer, lifts_len);
	}

	// Jfx
	MT4_EXPFUNC void __cdecl DeinitializeRobotJfx(const int id) {
		intBuffer[id][0]=0;
		intBuffer[id][1]=0;

		if (id == intBuffer[0][0]) {
			int numChan = intBuffer[id][3];
			for (int i=numChan; i<numChan; i++) {
				delete doubleArrays[id][i];
			}

			intBuffer[id][3] = 0;
			intBuffer[0][0]--;
		} else {
			int lifts_len = intBuffer[id][2];
			int numChan = intBuffer[id][3];
			for (int i=numChan; i<numChan; i++) {
				for (int j=0; j<lifts_len; j++) {
					doubleArrays[id][i][j] = 0;
				}
			}
			intBuffer[id][2] = -1;
			intBuffer[id][3] = 0;
		}
	}

	// 1.3 (see *.def)
	MT4_EXPFUNC void __stdcall DeinitializeRobot13(const int id) {
		//DeinitializeRobotJfx(id);

		intBuffer[id][0]=0;
		intBuffer[id][1]=0;

		if (id == intBuffer[0][0]) {
			intBuffer[id][3] = 0;
			intBuffer[0][0]--;
		} else {
			intBuffer[id][2] = -1;
			intBuffer[id][3] = 0;
		}
	}

	void fun0(
			int &validated,
			const int period, const int hlperiod, 
			const int Mode_Grizzly, const int Mode_Adaptive,
			const double Scalping_1Stick, 
			const double Scalping_Lift0, const double Scalping_Lift1, 
			const double Scalping_Lift0_Spread, const double Scalping_Lift1_Spread, 
			const double Scalping_Factor, const double Asymm, const double Scalping_Pips,
			const double Scalping_Lift_Plus, const double Scalping_Adaptive_Max, 
			const double Scalping_Lift_Minus, const double Scalping_Adaptive_Min, 
			const double Trailing_Resolution_Max, const double Scalping_Adaptive_Deviation,
			const double Asymm_Max, int MaxOrders_Level,
			const double BBlower, const double BBupper, 
			double asymm, double trailingLimit, double trailingResolution,
			const double realAvgSpread, const double point, const double Bid, const double Ask, const double dist,
			double _lifts[], 
			double &_result0, double &_result1, double &_result2, double &_result3, double &_result4, 
			double &_result5, double &_result6, double &_result7, double &_result8, double &_result9) {

		if (validated != 1) {
			_result0 = 0;
			_result1 = 0;
			_result2 = 0;
			_result3 = 0;
			_result4 = 0;
			_result5 = 0;
			_result6 = 0;
			return;
		}

		double scalpingLift0;
		double scalpingFactor;
		double spreadLiftRatio;

		// security feature (!)
		int v1 = validated;

		//mdp 1.3/2
		if (Scalping_Factor==-0.5) {
			MaxOrders_Level = MathMax(0,MaxOrders_Level);
			MaxOrders_Level = MathMin(4,MaxOrders_Level);
		} else {
			MaxOrders_Level = MathMax(-2,MaxOrders_Level);
			MaxOrders_Level = MathMin(2,MaxOrders_Level);
		}
			

		scalpingLift0 = Scalping_Lift0;
		scalpingFactor = Scalping_Factor;

		// security feature (!)
		int v2 = v1;

		int lifts_len = _result9;

		double Scalping_Lift;
		spreadLiftRatio = (realAvgSpread/point - Scalping_Lift0_Spread ) / (Scalping_Lift1_Spread - Scalping_Lift0_Spread);
		spreadLiftRatio = MathMax(0,spreadLiftRatio);
		spreadLiftRatio = MathMin(1,spreadLiftRatio);
		//mdp 1.3/2
		if (Scalping_Factor==-0.5) {
			double spreadLiftRatio2 = 1.0 - MaxOrders_Level / 4.0;
			spreadLiftRatio = (spreadLiftRatio+spreadLiftRatio2)/2.0;
		}

		// security feature (!)
		int v3 = v2;

		int v4;
		if (Mode_Adaptive) {
			int limit = lifts_len;
			//mdp 1.2
			if (Scalping_Factor>0) {
				switch (MaxOrders_Level) {
				case 2:
					limit = lifts_len * Scalping_Adaptive_Max;
					break;
				case 1:
					limit = lifts_len * (2*Scalping_Adaptive_Max + 1) / 3;
					break;
				}
			}

			double avg=0.0;
			int cnt=0;
			for(int i=0;i<limit;i++) {
				if (_lifts[i]) {
					avg+=abs(_lifts[i]);
					cnt++;
				}
			}
			if (cnt == 0) {
				Scalping_Lift = 0;
				// security feature (!)
				v4 = v3;
			} else {
				avg/=cnt;

				double sum=0.0;
				for(int i=0;i<limit;i++) {
					if (_lifts[i]) {
						double newres=abs(_lifts[i])-avg;
						sum+=newres*newres;
					}
				}

				double d;

				//mdp 1.2
				if (Scalping_Factor>0) {
					d = Scalping_Adaptive_Deviation + spreadLiftRatio * 0.5;
					switch (MaxOrders_Level) {
					case -1:
						d += Scalping_Adaptive_Min;
						break;
					case -2:
						d += Scalping_Adaptive_Min * 2;
						break;
					}
				} else {
					d = Scalping_Adaptive_Deviation + spreadLiftRatio * Scalping_Adaptive_Max;
				}
				double deviation = d * sqrt(sum/cnt);
				// security feature (!)
				v4 = v3;

				Scalping_Lift = (avg+deviation)/point;
				double sl1 = Scalping_Lift1;
				//mdp 1.3(2), elvérzett
				if (Scalping_Factor==-1.5) {
					switch (MaxOrders_Level) {
					case 2:
						sl1 -= Scalping_Lift_Minus;
						break;
					case 1:
						break;
					case 0:
						sl1 += Scalping_Lift_Plus;
						break;
					case -1:
						sl1 += 2*Scalping_Lift_Plus;
						break;
					case -2:
						sl1 += 3*Scalping_Lift_Plus;
						break;
					}
				}
				Scalping_Lift = MathMin( sl1, Scalping_Lift );
				//mdp 1.3/2
				if (Scalping_Factor==-0.5) {
					Scalping_Lift = MathMax( Scalping_Lift0, Scalping_Lift );
				}
			}
		} else {
			Scalping_Lift = scalpingLift0 + spreadLiftRatio * (Scalping_Lift1 - Scalping_Lift0);

			//mdp 1.2
			if (Scalping_Factor>0) {
				switch (MaxOrders_Level) {
				case 2:
					Scalping_Lift -= 2*Scalping_Lift_Minus;
					break;
				case 1:
					Scalping_Lift -= Scalping_Lift_Minus;
					break;
				case -1:
					Scalping_Lift += Scalping_Lift_Plus;
					break;
				case -2:
					Scalping_Lift += 2*Scalping_Lift_Plus;
					break;
				}
			}
			// security feature (!)
			v4 = v3;
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
			}

			if (Scalping_Pips == 0) {
				scalp = scalpingFactor * lift;
			} else {
				scalp = Scalping_Pips * point;
			}
		}

		// security feature (!)
		int v5 = v4;

		double high = _result7 - BBupper + dist*Scalping_1Stick;
		double low = BBlower - _result8 + dist*Scalping_1Stick;
		bool wasVirgin = _lifts[lifts_len-1]==0;
		if (_result7 || _result8) {
			if (_result7 && high>=Scalping_Lift0*0.333333333*point) {
				memcpy(_lifts+1,_lifts,(lifts_len-1)*sizeof(double));
				_lifts[0] = high;
			} else if (_result8 && low>=Scalping_Lift0*0.333333333*point) {
				memcpy(_lifts+1,_lifts,(lifts_len-1)*sizeof(double));
				_lifts[0] = -low;
			}
		}
		_result9 = wasVirgin;

		// security feature (!)
		if (v5 != 1) {
			lift += 1500;
			scalpingLift0 += 1500;
		}

		_result0 = scalpingLift0;
		_result1 = lift;
		_result2 = isJustCrossed;
		_result3 = scalp;
		_result4 = asymm;
		_result5 = price;
		_result6 = trailingResolution;
		_result7 = high;
		_result8 = low;
	}

	// 1.2
	MT4_EXPFUNC void __stdcall fun(
			const int period, const int hlperiod, 
			const int Mode_Grizzly, const int Mode_Adaptive,
			const double Scalping_1Stick, 
			const double Scalping_Lift0, const double Scalping_Lift1, 
			const double Scalping_Lift0_Spread, const double Scalping_Lift1_Spread, 
			const double Scalping_Factor, const double Asymm, const double Scalping_Pips,
			const double Scalping_Lift_Plus, const double Scalping_Adaptive_Max, 
			const double Scalping_Lift_Minus, const double Scalping_Adaptive_Min, 
			const double Trailing_Resolution_Max, const double Scalping_Adaptive_Deviation,
			const double Asymm_Max, int MaxOrders_Level,
			const double BBlower, const double BBupper, 
			double asymm, double trailingLimit, double trailingResolution,
			const double realAvgSpread, const double point, const double Bid, const double Ask, const double dist,
			double _lifts[], double _result[]) {
		
		int &validated = intBuffer[1][0];
		fun0(validated,
			 period, hlperiod, 
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
			 realAvgSpread, point, Bid, Ask, dist,
			 _lifts, 
			 _result[0], _result[1], _result[2], _result[3], _result[4], 
			 _result[5], _result[6], _result[7], _result[8], _result[9]) ;
	}

	// Jfx
	MT4_EXPFUNC void __cdecl funJfx(
			const int id,
			const int period, const int hlperiod, 
			const int Mode_Grizzly, const int Mode_Adaptive,
			const double Scalping_1Stick, 
			const double Scalping_Lift0, const double Scalping_Lift1, 
			const double Scalping_Lift0_Spread, const double Scalping_Lift1_Spread, 
			const double Scalping_Factor, const double Asymm, const double Scalping_Pips,
			const double Scalping_Lift_Plus, const double Scalping_Adaptive_Max, 
			const double Scalping_Lift_Minus, const double Scalping_Adaptive_Min, 
			const double Trailing_Resolution_Max, const double Scalping_Adaptive_Deviation,
			const double Asymm_Max, int MaxOrders_Level,
			const double BBlower, const double BBupper, 
			double asymm, double trailingLimit, double trailingResolution,
			const double realAvgSpread, const double point, const double Bid, const double Ask, const double dist,
			int _channelId, 
			double &_result0, double &_result1, double &_result2, double &_result3, double &_result4, 
			double &_result5, double &_result6, double &_result7, double &_result8, double &_result9) {

		int &validated = intBuffer[id][0];
		int &statuscode = intBuffer[id][1];
		double *_lifts = doubleArrays[id][_channelId];
		int lifts_len = intBuffer[id][2];
		int numChan = intBuffer[id][3];
		if (numChan <= _channelId) {
			for (int i=numChan; i<=_channelId; i++) {
				doubleArrays[id][i] = _lifts = new double[lifts_len];
			}
			intBuffer[id][3] = numChan = _channelId+1;
		}
		fun0(validated,
			 period, hlperiod, 
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
			 realAvgSpread, point, Bid, Ask, dist,
			 _lifts, 
			 _result0, _result1, _result2, _result3, _result4, 
			 _result5, _result6, _result7, _result8, _result9) ;

		bool wasVirgin = _result9;
		bool isVirgin = _lifts[lifts_len-1]==0;
		if (wasVirgin && !isVirgin) {
			int f = findArrayLast(_lifts, lifts_len);
			reverseArray(_lifts,f+1);
		}
		_result9 = isVirgin;
	}

	// 1.3
	MT4_EXPFUNC void __stdcall fun13(
			const int id,
			const int period, const int hlperiod, 
			const int Mode_Grizzly, const int Mode_Adaptive,
			const double Scalping_1Stick, 
			const double Scalping_Lift0, const double Scalping_Lift1, 
			const double Scalping_Lift0_Spread, const double Scalping_Lift1_Spread, 
			const double Scalping_Factor, const double Asymm, const double Scalping_Pips,
			const double Scalping_Lift_Plus, const double Scalping_Adaptive_Max, 
			const double Scalping_Lift_Minus, const double Scalping_Adaptive_Min, 
			const double Trailing_Resolution_Max, const double Scalping_Adaptive_Deviation,
			const double Asymm_Max, int MaxOrders_Level,
			const double BBlower, const double BBupper, 
			double asymm, double trailingLimit, double trailingResolution,
			const double realAvgSpread, const double point, const double Bid, const double Ask, const double dist,
			int _channelId, double _lifts[], double _result[]) {
		//funJfx(id,
		//	   period, hlperiod, 
		//	   Mode_Grizzly, Mode_Adaptive,
		//	   Scalping_1Stick, 
		//	   Scalping_Lift0, Scalping_Lift1, 
		//	   Scalping_Lift0_Spread, Scalping_Lift1_Spread, 
		//	   Scalping_Factor, Asymm, Scalping_Pips,
		//	   Scalping_Lift_Plus, Scalping_Adaptive_Max, 
		//	   Scalping_Lift_Minus, Scalping_Adaptive_Min, 
		//	   Trailing_Resolution_Max, Scalping_Adaptive_Deviation,
		//	   Asymm_Max, MaxOrders_Level,
		//	   BBlower, BBupper, 
		//	   asymm, trailingLimit, trailingResolution,
		//	   realAvgSpread, point, Bid, Ask, dist,
		//	   _channelId, 
		//	   _result[0], _result[1], _result[2], _result[3], _result[4], 
		//	   _result[5], _result[6], _result[7], _result[8], _result[9]);
		//
		//bool wasVirgin = _result9;
		//bool isVirgin = _lifts[lifts_len-1]==0;
		//if (wasVirgin && !isVirgin) {
		//	int f = findArrayLast(_lifts, lifts_len);
		//	reverseArray(_lifts,f+1);
		//}
		//_result9 = isVirgin;

		int numChan = intBuffer[id][3];
		if (numChan <= _channelId) {
			intBuffer[id][3] = numChan = _channelId+1;
		}

		int &validated = intBuffer[id][0];
		fun0(validated,
			 period, hlperiod, 
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
			 realAvgSpread, point, Bid, Ask, dist,
			 _lifts, 
			 _result[0], _result[1], _result[2], _result[3], _result[4], 
			 _result[5], _result[6], _result[7], _result[8], _result[9]) ;
	}

	// test1
	MT4_EXPFUNC void __cdecl test1(const int id) {
		initLog();
		smartlog(SEV_INFO) << "test1  id:"+id;
	}

	BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
	{
		switch (fdwReason)
		{
		case DLL_PROCESS_ATTACH:
			// attach to process
			// return FALSE to fail DLL load
			break;

		case DLL_PROCESS_DETACH:
			// detach from process
			break;

		case DLL_THREAD_ATTACH:
			// attach to thread
			break;

		case DLL_THREAD_DETACH:
			// detach from thread
			break;
		}
		return TRUE; // succesful
	}



#ifdef __cplusplus
}
#endif

