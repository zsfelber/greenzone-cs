// Mt4ControlPanel.cpp : main project file.

#include "stdafx.h"

using namespace System;
using namespace System::Reflection;
using namespace System::Windows::Forms;

#define MT4_EXPFUNC __declspec(dllexport)
#define export extern "C" __declspec( dllexport )

#ifdef __cplusplus
extern "C"
{
#endif

	struct MqlStr
	{
		int len;
		char *string;
	};

	ref class Mt4ControlPanelBridge
	{
		static Assembly ^BridgeDll;
		static Type ^BridgeClass;
		static MethodInfo ^MInit;
		static MethodInfo ^MTick;
		static MethodInfo ^MAddOrder;

	public:
		static void CheckInit() {
			if (BridgeDll == nullptr) {
				try {
					BridgeDll = Assembly::LoadFrom("experts/libraries/Mt4ControlPanelBridge.dll");
				} catch (Exception ^e) {
					MessageBox::Show("Not found experts/libraries/Mt4ControlPanelBridge.dll : loading from default location...");

					BridgeDll = Assembly::LoadFrom("F:/workspaces/general_web/ForexRobots/windows_dll/Mt4ControlPanel/Debug/Mt4ControlPanelBridge.dll");
				}

				BridgeClass = BridgeDll->GetType("Mt4ControlPanelBridge.Mt4ControlPanelBridge");

				array<Type^>^ tarr = gcnew array<Type^> { IntPtr::typeid, int::typeid };
				MInit = BridgeClass->GetMethod("Init", tarr);

				tarr = gcnew array<Type^> { double::typeid, double::typeid, int::typeid, IntPtr::typeid, IntPtr::typeid };
				MTick = BridgeClass->GetMethod("Tick", tarr);

				tarr = gcnew array<Type^> { int::typeid, int::typeid, double::typeid, double::typeid, int::typeid, double::typeid, double::typeid, double::typeid, int::typeid, int::typeid, IntPtr::typeid};
				MAddOrder = BridgeClass->GetMethod("AddOrder", tarr);
			}
		}

		static void Init(const char* symbol, int digits) {
			array<Object^>^ oarr = gcnew array<Object^> { (IntPtr)(void*)symbol, digits };
			MInit->Invoke(nullptr, oarr);
		}

		static void Tick(double bid, double ask, int numOrders, double *dargs, MqlStr *sargs)
		{
			array<Object^>^ oarr = gcnew array<Object^> { bid, ask, numOrders, (IntPtr)dargs, (IntPtr)sargs };
			MTick->Invoke(nullptr, oarr);
		}

		static void AddOrder(int ticket, int type, double lots, double openPrice, int openTime, double sl, double tp, double closePrice, int closeTime, int magic, const char* comment)
		{
			array<Object^>^ oarr = gcnew array<Object^> { ticket, type, lots, openPrice, openTime, sl, tp, closePrice, closeTime, magic, (IntPtr)(void*)comment };
			MAddOrder->Invoke(nullptr, oarr);
		}
	};

	void MT4_EXPFUNC __stdcall Init(const char* symbol, int digits)
	{
		try {
			Mt4ControlPanelBridge::CheckInit();
			Mt4ControlPanelBridge::Init(symbol, digits);
		} catch (Exception ^e) {
			MessageBox::Show(e->ToString());
		}
	}

	void MT4_EXPFUNC __stdcall Deinit()
	{
		try {
			Application::Exit();
		} catch (Exception ^e) {
			MessageBox::Show(e->ToString());
		}
	}

	void MT4_EXPFUNC __stdcall Tick(double bid, double ask, int numOrders, double *dargs, MqlStr *sargs)
	{
		try {
			Mt4ControlPanelBridge::Tick(bid, ask, numOrders, dargs, sargs);
		} catch (Exception ^e) {
			MessageBox::Show(e->ToString());
		}
	}

	void MT4_EXPFUNC __stdcall AddOrder(int ticket, int type, double lots, double openPrice, int openTime, double sl, double tp, double closePrice, int closeTime, int magic, const char* comment)
	{
		try {
			Mt4ControlPanelBridge::AddOrder(ticket, type, lots, openPrice, openTime, sl, tp, closePrice, closeTime, magic, comment);
		} catch (Exception ^e) {
			MessageBox::Show(e->ToString());
		}
	}

	

#ifdef __cplusplus
}
#endif
