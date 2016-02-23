// Mt4ControlPanelBridge.cpp : main project file.

#include "stdafx.h"
#include "Mt4ControlPanelBridge.h"

using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Data;
using namespace System::Drawing;
using namespace System::Runtime::InteropServices;
using namespace System::Threading;
using namespace System::Windows::Forms;

using namespace Mt4ControlPanelGui;

MqlStr::MqlStr() { init   (); }
MqlStr::~MqlStr() { dealloc(); }
void MqlStr::operator= (String ^managedStr) { assign(managedStr); }

void MqlStr::init() { 
	string = 0; 
	len = 0; 
}
void MqlStr::assign(String^ str) {
	dealloc();
	if (str==nullptr) {
		string = 0;
		len = 0;
	} else {
		string = (char*)(void*)Marshal::StringToHGlobalAnsi(str);
		len = str->Length;
	}
}
void MqlStr::dealloc() {
	if(string != 0) {
		Marshal::FreeHGlobal((IntPtr)(void*)string);
		string = 0;
	}
}

namespace Mt4ControlPanelBridge {

	void Mt4ControlPanelBridge::Init(IntPtr _symbol, int digits)
	{
		try {
			const char* symbol = (const char*)(void*)_symbol;

			MainForm::Init(gcnew String(symbol), digits);

			while (!MainForm::Started)
			{
				Thread::Sleep(100);
			}
		} catch (Exception ^e) {
			MessageBox::Show(e->ToString());
		}
	}

	void Mt4ControlPanelBridge::Tick(double bid, double ask, int numOrders, IntPtr _dargs, IntPtr _sargs)
	{
		try {
			double *dargs = (double*)(void*)_dargs;
			MqlStr *sargs = (MqlStr*)(void*)_sargs;

			Monitor::Enter(MainForm::Lock);

			MainForm::Tick(bid, ask);
			if (MainForm::Order->BeingSaved)
			{
				Order ^order = MainForm::Order;
				dargs[0] = order->Ticket;
				dargs[1] = (int)order->Type;
				dargs[2] = order->Lots;
				dargs[3] = order->OpenPrice;
				dargs[4] = PropertyHandler::ToUnixTime(order->OpenTime);
				dargs[5] = order->SL;
				dargs[6] = order->TP;
				dargs[7] = order->ClosePrice;
				dargs[8] = order->Slip;
				dargs[9] = order->Magic;
				sargs[0] = order->Comment;
				order->BeingSaved = false;
			}
			if (numOrders >= 0)
			{
				MainForm::RecordOrders(numOrders);
			}
		} catch (Exception ^e) {
			MessageBox::Show(e->ToString());
		} finally {
			Monitor::Exit(MainForm::Lock);
		}
	}

	void Mt4ControlPanelBridge::AddOrder(int ticket, int type, double lots, double openPrice, int openTime, double sl, double tp, double closePrice, int closeTime, int magic, IntPtr _comment)
	{
		try {
			const char* comment = (const char*)(void*)_comment;

			Order ^order = gcnew Order();

			order->Ticket = ticket;
			order->Type = (OrderType)type;
			order->Lots = lots;
			order->OpenPrice = openPrice;
			order->OpenTime = openTime == 0 ? DateTime() : PropertyHandler::FromUnixTime(openTime);
			order->SL = sl;
			order->TP = tp;
			order->ClosePrice = closePrice;
			order->CloseTime = closeTime == 0 ? DateTime() : PropertyHandler::FromUnixTime(closeTime);
			order->Magic = magic;
			order->Comment = gcnew String(comment);

			MainForm::AddOrder(order);
		} catch (Exception ^e) {
			MessageBox::Show(e->ToString());
		}
	}

}