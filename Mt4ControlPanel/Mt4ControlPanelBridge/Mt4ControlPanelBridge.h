#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;

struct MqlStr
{
	int len;
	char *string;

	MqlStr();
	~MqlStr();
	void operator= (String ^managedStr);

private:
	void init();
	void assign(String^ str);
	void dealloc();
};

namespace Mt4ControlPanelBridge {

	/// <summary>
	/// Summary for Mt4ControlPanelBridge
	/// </summary>
	public ref class Mt4ControlPanelBridge
	{
	public:

		static void Init(IntPtr _symbol, int digits);
		static void Tick(double bid, double ask, int numOrders, IntPtr _dargs, IntPtr _sargs);
		static void AddOrder(int ticket, int type, double lots, double openPrice, int openTime, double sl, double tp, double closePrice, int closeTime, int magic, IntPtr _comment);

	};
}
