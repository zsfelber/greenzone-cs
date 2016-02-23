// This is the main DLL file.

#include "stdafx.h"

#include "EAForm.h"
#include "eanalyzer.h"
#include <SmartLog.h>
#include <zsfTraderLib.h>
#include <vector>
#include <map>

using namespace eanalyzer;

using namespace System;
using namespace System::IO;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Runtime::InteropServices;


//namespace eanalyzer {

	public ref class EAGlobals abstract {
	public:
		static EAForm2 ^form;
	};


	DECLDIR int __stdcall InitializeRobot(
							int id, const char* accountName, int accountNumber, const char* symbol, int period, 
							bool isTesting, bool isDemo, double variables[], 
							// output
							int statusInfo[], char* messageBuffer) {
		try {
			InitSmartLogC("c:\\eanalyzer.log");
			InitSmartLogCli("c:\\eanalyzer_cli.log");
			smartlog(SEV_PRINT) << sstring()+"InitializeRobot  starting   id:" + id + " accountName:" + accountName + " accountNumber:" + accountNumber + " symbol:" + symbol + " period:" + period << "Log file:" + __smartlog().GetFilePath();

			ChartSpace ^chartSpace = TraderSpace::GetChartSpace(id);
			if (chartSpace==nullptr) {
				ZsfAccountId::TreeType acctType = isTesting?ZsfAccountId::TreeType::TESTING:ZsfAccountId::TreeType::NORMAL;
				TraderSpace::GetChartConfig(ZsfTraderId::TreeType::UI, gcnew ZsfAccountId(gcnew String(accountName), accountNumber, acctType), gcnew String(symbol), period, 0, "eanalyzer", variables);
				chartSpace = TraderSpace::GetChartConfig(ZsfTraderId::TreeType::LIVE, gcnew ZsfAccountId(gcnew String(accountName), accountNumber, acctType), gcnew String(symbol), period, 0, "eanalyzer", variables);
			}
			if (EAGlobals::form == nullptr) {
				EAGlobals::form = gcnew EAForm2(chartSpace->Id, TraderSpace::uiTraderSpace);
				EAGlobals::form->Start();
			}
			return chartSpace->Id;
		} catch ( Exception ^e ) {
			String ^s="InitializeRobot  ";
			smartlog(SEV_ERROR) << s+e;
		} catch (const char* err) {
			smartlog(SEV_ERROR) << "InitializeRobot  Error (const char*):" << err;
		} catch (const int err) {
			smartlog(SEV_ERROR) << "InitializeRobot  Error (const int):" << err;
		}
		return -1;
	}

	DECLDIR void __stdcall DeinitializeRobot(const int id) {
		try {
			String ^s="";
			smartlog(SEV_DEBUG_1) << s+"DeinitializeRobot... id:"+id;

			ChartSpace ^chartSpace = TraderSpace::GetChartSpace(id);
			if (chartSpace==nullptr) {
				throw gcnew Exception(s+"ChartSpace not found.  id:"+id);
			}
			if (EAGlobals::form != nullptr) {
				EAGlobals::form->MarkLoaded();
			}
		} catch ( Exception ^e ) {
			String ^s="DeinitializeRobot ";
			smartlog(SEV_ERROR) << s+e;
		} catch (const char* err) {
			smartlog(SEV_ERROR) << "DeinitializeRobot Error (const char*):" << err;
		} catch (const int err) {
			smartlog(SEV_ERROR) << "DeinitializeRobot Error (const int):" << err;
		}
	}

	DECLDIR void __stdcall tick(const int id, const double bid, const double ask, double open, double low, double high, double close, int time) {
		try {
			ChartSpace ^chartSpace = TraderSpace::GetChartSpace(id);
			if (chartSpace==nullptr) {
				String ^s="";
				throw gcnew Exception(s+"ChartSpace not found.  id:"+id);
			}
			chartSpace->ReceiveTick(bid, ask, open, low, high, close, time);
		} catch ( Exception ^e ) {
			String ^s="tick ";
			smartlog(SEV_ERROR) << s+e;
		} catch (const char* err) {
			smartlog(SEV_ERROR) << "tick Error (const char*):" << err;
		} catch (const int err) {
			smartlog(SEV_ERROR) << "tick Error (const int):" << err;
		}
	}

	BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
	{

		switch (fdwReason)
		{
		case DLL_PROCESS_ATTACH:
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

	void CheckWindowClosed(int id) {
		String ^s="";
		smartlog(SEV_DEBUG_1) << s+"CheckWindowClosed  id:"+id;
	}
//}
