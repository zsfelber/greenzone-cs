// This is the main DLL file.

#include "stdafx.h"

#include "MDPForm.h"
#include "mdpanalyzer.h"
#include "ServerValidate.h"

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;
using namespace System::Runtime::InteropServices;

Mutex M1;

namespace mdpanalyzer {

	public ref class Globals abstract sealed {
	public:
		static Hashtable^ forms = gcnew Hashtable();

	};

	int index=0;

	std::vector<MDP*> mdps;

	std::map<int,MDPAnalyzer**> analyzers;

	const char *XAxisOption_str[]={ "BOLLINGER_WIDTH","BOLLINGER_DEVIATION" };
	
	const char *DarknessOption_str[]={ "MAX_MIN","AVERAGE","SIMULATED" };

	int channelPeriodMillis[10];

	__declspec(dllexport) int __stdcall InitializeRobot(
							const char *accountName, const int accountNumber,
							const char *username, const int debug, int validated[], int statuscode[], char* returnbuffer,
							const char *symbol, double point, double spread, int channelPeriodMillis[],
							int time[], double open[], double low[], double high[], double close[], double volume[],
							int arrayLen, int period, int timeMillis) {
		try {
			String ^s="";
			int dp = (int)log(0.1,point);
			if (!index) {
				CLogFlags flags;
				__smartlog.SetFilePath(L"c:\\mdpanalyzer.log");
				__smartlog.GetFlags(flags);
				flags.m_bLineFlag = true;
				flags.m_bFileFlag = true;
				__smartlog.SetFlags(flags);

				smartlog(SEV_INFO) << 
					s+"Starting mdpanalyzer... accountName:"+gcnew String(accountName)+
					" accountNumber:"+accountNumber+" symbol:"+gcnew String(symbol);

				if (LOG_SEVERITY>=SEV_DEBUG_2) {
					int addr1 = (int) (void*)channelPeriodMillis;
					int addr2 = (int) (void*)time;
					int addr3 = (int) (void*)open;
					int addr4 = (int) (void*)low;
					int addr5 = (int) (void*)high;
					int addr6 = (int) (void*)close;
					int addr7 = (int) (void*)volume;
					smartlog(SEV_DEBUG_2) << s+"point:"+d(point,dp,true)+" spread:"+d(spread,dp,true)+
						" accountNumber:"+accountNumber+" @:"+addr1+" "+addr2+" "+addr3+" "+addr4+" "+addr5+" "+addr6+" "+addr7;
					String ^tbl="input timeseries:\r\n";
					for (int i=0; i<arrayLen; i++) {
						tbl = tbl+i4(i)+"   "+(time[i]/3600/24/7)+" "+(time[i]/3600/24)+" "+(time[i]/3600)+" "+(time[i]/60)+" "+time[i]+" :  "+d(open[i],dp,true)+"  "+d(low[i],dp,true)+"  "+d(high[i],dp,true)+"  "+d(close[i],dp,true)+"   Vol:"+volume[i]+"\r\n";
					}
					smartlog(SEV_DEBUG_2) << tbl;
				}
				//int time[], double open[], double low[], double high[], double close[], double volume[]) {
			}
			InitializeRobotImpl(username, accountNumber, debug, validated, statuscode, returnbuffer) ;

			smartlog(SEV_DEBUG_1) << 
				s+"InitializeRobotImpl ok:  username:"+gcnew String(username)+" accountNumber:"+accountNumber+
				"debug:"+debug+" validated:"+validated[0]+" statuscode:"+statuscode[0]+
				" returnbuffer:"+gcnew String(returnbuffer);
			TradingRuntime &runtime = GetRuntime("mdpanalyzer", accountName, accountNumber, symbol);
			runtime.GetSegment(time, open, low, high, close, volume, spread, arrayLen, period, timeMillis);

			smartlog(SEV_DEBUG_1) << s+"GetRuntime ok:" + 
				"accountName:"+gcnew String(accountName)+" symbol:"+gcnew String(symbol);

			MDP *mdp = new MDP(runtime, point, spread, channelPeriodMillis);
			int id=-1;
			for (unsigned int i=0; i<mdps.size(); i++) {
				if (!mdps[i]) {
					mdps[i] = mdp;
					id = i;
					break;
				}
			}
			if (id == -1) {
				mdps.push_back(mdp);
				id = mdps.size()-1;
			}

			smartlog(SEV_DEBUG_1) << s+"new MDP() ok:" + 
				"point:"+d(point,dp,true)+" spread:"+d(spread,dp,true)+" return id:"+id;
			return id;
		} catch ( Exception ^e ) {
			String ^s="";
			smartlog(SEV_ERROR) << s+e;
		} catch (const char* err) {
			smartlog(SEV_ERROR) << "Error (const char*):" << err;
		} catch (const int err) {
			smartlog(SEV_ERROR) << "Error (const int):" << err;
		}
	}

	__declspec(dllexport) void __stdcall DeinitializeRobot(const int id) {
		String ^s="";
		smartlog(SEV_DEBUG_1) << s+"DeinitializeRobot... id:"+id;

		MDPForm ^form = (MDPForm^) Globals::forms[id];
		form->markLoaded();
		mdps[id]->Deinitialized = true;
	}

	__declspec(dllexport) void __stdcall analyze(const int id, const int timesecs, const int timemillis, const double bid, const double ask) {
		if (!ok()) {
			return;
		}

		try {
			String ^s = "";
			if (!analyzers[id]) {
				smartlog(SEV_DEBUG_1) << s+"no analyzers["+id+"] new MDPAnalyzer*[]";
				analyzers[id] = new MDPAnalyzer*[10];
				for (int i=0; i<10; i++) {
					analyzers[id][i] = NULL;
				}
			} else if (index <= 1) {
				smartlog(SEV_DEBUG_1) << "analyzers["+id+"] present";
			}
			for (int i=0; i<10; i++) {
				MDPAnalyzer *analyzer = analyzers[id][i];
				if (!analyzer) {
					analyzer = new MDPAnalyzer(mdps[id],i);
					analyzers[id][i] = analyzer;
					smartlog(SEV_DEBUG_2) << s+"no analyzers["+id+"]["+i+"] new MDPAnalyzer";
				} else if (index == 0) {
					smartlog(SEV_DEBUG_2) << s+"analyzers["+id+"]["+i+"] present";
				}

				analyzer->run(timesecs, timemillis, bid, ask);
				if (index == 0) {
					smartlog(SEV_DEBUG_2) << s+"analyzer->run ok";
				}
			}

			MDPForm ^form = (MDPForm^) Globals::forms[id];
			if (form==nullptr) {
				form = gcnew MDPForm(id,mdps[id]->point,mdps[id]->spread);
				smartlog(SEV_DEBUG_1) << s+"gcnew MDPForm("+id+") ok";
				Globals::forms[id] = form;
				form->start();
				smartlog(SEV_DEBUG_1) << s+"form->start ok";
			} else if (form->Finished) {
				form->start();
				smartlog(SEV_DEBUG_1) << s+"form->start again ok";
			}

			if (index%50000 == 0) {
			}
			index++;
		} catch ( Exception ^e ) {
			String ^s="";
			smartlog(SEV_ERROR) << s+e;
		} catch (const char* err) {
			smartlog(SEV_ERROR) << "Error (const char*):" << err;
		} catch (const int err) {
			smartlog(SEV_ERROR) << "Error (const int):" << err;
		}
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



	MDP::MDP(TradingRuntime &_runtime, double _point, double _spread, int _channelPeriodMillis[]) :
		runtime(_runtime), point(_point), spread(_spread),
		Deinitialized(false)
	{
		memcpy(channelPeriodMillis,_channelPeriodMillis,10*sizeof(int));
	}

	AnalysisField::AnalysisField(Field &_field, MovingAvgField &_mavgField, BollingerField &_bollField) :
		field(_field), mavgField(_mavgField), bollField(_bollField)
	{
	}

	FieldAnalysis::FieldAnalysis(MDP *_mdp, int _channel, Field &field, MovingAvgField &mavgField, BollingerField &bollField) : 
			mdp(_mdp),channel(_channel),
			maxPrice(0),minPrice(0),maxDeviation(0),
			fieldFrom(NULL),fieldTo(NULL)
	{

		start(field, mavgField, bollField);
	}

	FieldAnalysis::~FieldAnalysis() {
		if (fieldFrom) {
			delete fieldFrom;
		}
		if (fieldTo) {
			delete fieldTo;
		}
	}

	void FieldAnalysis::start(Field &field, MovingAvgField &mavgField, BollingerField &bollField) {
		fieldFrom = new AnalysisField(field, mavgField, bollField);
	}

	void FieldAnalysis::end(Field &field, MovingAvgField &mavgField, BollingerField &bollField) {
		fieldTo = new AnalysisField(field, mavgField, bollField);
	}

	Status FieldAnalysis::updateShort(Field &field, MovingAvgField &mavgField, BollingerField &bollField) {
		Status status = TRADE_SHORT;
		maxPrice = MathMax(maxPrice,field.Ask());
		minPrice = MathMin(minPrice,field.Ask());
		if (field.Ask() < mavgField.GetValueAsk()) {
			status = NO_TRADE;
			end(field, mavgField, bollField);
		} else {
			double deviation = (field.Bid() - mavgField.GetValueBid()) / bollField.GetDeviationBid();
			maxDeviation = Max(maxDeviation,deviation);
		}
		return status;
	}

	Status FieldAnalysis::updateLong(Field &field, MovingAvgField &mavgField, BollingerField &bollField) {
		Status status = TRADE_LONG;
		maxPrice = MathMax(maxPrice,field.Bid());
		minPrice = MathMin(minPrice,field.Bid());
		if (field.Bid() > mavgField.GetValueBid()) {
			status = NO_TRADE;
			end(field, mavgField, bollField);
		} else {
			double deviation = (mavgField.GetValueAsk() - field.Ask()) / bollField.GetDeviationAsk();
			maxDeviation = Max(maxDeviation,deviation);
		}
		return status;
	}



	
	MDPAnalyzer::MDPAnalyzer(MDP *_mdp, int _channel) : 
		mdp(_mdp),channel(_channel),
		lastInd(200000),status(NO_TRADE) {
	}

	void MDPAnalyzer::run(const int time, const int timeMillis, const double bid, const double ask) {
		SegmentId segmentId;
		segmentId.ftype = 
		segmentId.Address = GetAddressDivisor();
		SymbolCacheSegment *segment = mdp->runtime.GetSegment(time/3600/24);
		if (segment) {
			int index = segment->AddTick(time, timeMillis, bid, ask);
			MovingAvgArgs a;
			BollingerArgs b;
			b.PeriodMillis = a.PeriodMillis = mdp->channelPeriodMillis[channel];
			b.Mode = a.Mode = MAVG_SMA;

			Field &field = segment->Get().Items[index];
			MovingAvgField &mavgField = segment->Get(a).Items[index];
			BollingerField &bollField = segment->Get(b).Items[index];

			String ^s="";
			smartlog(SEV_DEBUG_3) << s+"run(0)";

			switch (status) {
			case NO_TRADE:
				smartlog(SEV_DEBUG_3) << s+"run(1)";
				if (field.Bid() > bollField.GetUpperBid(mavgField)) {
					status = TRADE_SHORT;
					currentFA = new FieldAnalysis(mdp,channel,field, mavgField, bollField);
					smartlog(SEV_DEBUG_3) << s+"run: currentFA = new FieldAnalysis(...) ok";
					result.push_back(currentFA);
					smartlog(SEV_DEBUG_3) << s+"run(2)";
				} else if (field.Ask() < bollField.GetLowerAsk(mavgField)) {
					status = TRADE_LONG;
					currentFA = new FieldAnalysis(mdp,channel,field, mavgField, bollField);
					result.push_back(currentFA);
					smartlog(SEV_DEBUG_3) << s+"run(3)";
				}
				break;
			case TRADE_SHORT:
				status = currentFA->updateShort(field, mavgField, bollField);
				smartlog(SEV_DEBUG_3) << s+"run(4)";
				break;
			case TRADE_LONG:
				status = currentFA->updateLong(field, mavgField, bollField);
				smartlog(SEV_DEBUG_3) << s+"run(5)";
				break;
			}
		}
	}

	
	array<String^> ^getChannelInfo(int id) {
		String ^s = "";
		array<String^> ^info = gcnew array<String^>(10);
		for (int i=0; i<10; i++) {
			info[i] = s + "Chan" + i2(i) + " " + millisToTxt(mdps[id]->channelPeriodMillis[i]);
		}
		return info;
	}

	Image ^generateAnalysisImage(	int id, int channelA, int channelB, 
									XAxisOption xAxisOpt, DarknessOption darknessOpt, 
									int width, int height) {
		String ^s="";
		smartlog(SEV_DEBUG_1) << 
			s+"generateAnalysisImage id:"+id+
			" channelA:"+channelA+" channelB:"+channelB+
			" xAxisOpt:"+gcnew String(XAxisOption_str[xAxisOpt])+
			" darknessOpt:"+gcnew String(DarknessOption_str[darknessOpt])+
			" width:"+width+" height:"+height;
		MDPAnalyzer *analyzerA = analyzers[id][channelA];
		MDPAnalyzer *analyzerB = analyzers[id][channelB];

		Bitmap ^bmpOut = gcnew Bitmap(width, height);
		for (int x=0; x < width; x++) {
			for (int y=0; y < height; y++) {
				int c = 255*(x+y)/(width+height-2);
				bmpOut->SetPixel(x, y, Color::FromArgb(c,c,c));
			}
		}

		smartlog(SEV_DEBUG_1) << "generateAnalysisImage ok";
		return bmpOut;
	}

	Image ^generateChartImage(	int id, int channel, int height) {
		String ^s="";
		smartlog(SEV_DEBUG_1) << 
			s+"generateChartImage id:"+id+
			" channel:"+channel+" height:"+height;
		MDP *mdp = mdps[id];

		Bitmap ^bmpOut = gcnew Bitmap(10*height, 2*height);
		for (int x=0; x < 10*height; x++) {
			for (int y=0; y < 2*height; y++) {
				int c = 255*(x+y)/(12*height-2);
				bmpOut->SetPixel(x, y, Color::FromArgb(c,c,c));
			}
		}

		smartlog(SEV_DEBUG_1) << "generateChartImage ok";
		return bmpOut;
	}

	void CheckWindowClosed(int id) {
		String ^s="";
		smartlog(SEV_DEBUG_1) << s+"CheckWindowClosed... id:"+id;

		if (mdps[id]->Deinitialized) {
			String ^s="";
			smartlog(SEV_INFO) << s+"It's already deinitialized, Deleting... :  id:"+id;
			delete mdps[id];
			mdps[id] = NULL;
			for (int i=0; i<10; i++) {
				delete analyzers[id][i];
			}
			delete[] analyzers[id];
			analyzers[id] = NULL;
		}
	}
}
