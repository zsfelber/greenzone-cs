// Metatrader4.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"

#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include <math.h>

#include "MainDllBase.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Diagnostics;
using namespace System::IO;
using namespace System::IO::Pipes;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;
using namespace System::Runtime::Serialization::Formatters::Binary;
using namespace System::Threading;

#define MT4_EXPFUNC __declspec(dllexport)
#define export extern "C" __declspec( dllexport )

struct MqlStr {
	int	len;
	char* string;
	MqlStr() {
	}
	MqlStr(int len, char* string) : len(len), string(string) {
	}
};

#ifdef __cplusplus
extern "C"
{
#endif

	MT4_EXPFUNC void __stdcall Log(int sessionId, const char *message) {
		try
		{
			Session ^session = Globals::getSession(sessionId);
			session->log(gcnew String(message));
		}
		catch ( Exception^ e ) 
		{
            Console::WriteLine("Exception:");
			Console::WriteLine( e->Message );
            Console::WriteLine(e->StackTrace);
		}
	}

	MT4_EXPFUNC int __stdcall FxSubInit(MqlStr login[]) {
		try
		{
			Session ^session = Globals::createSession(login);
#ifdef DEBUG
            session->log("FxSubInit()  success.  sessionId:"+session->sessionId);
#else
            session->log("FxSubInit()  success.");
#endif
			return session->sessionId;
		}
		catch ( Exception^ e ) 
		{
            Console::WriteLine("Exception:");
			Console::WriteLine( e->Message );
            Console::WriteLine(e->StackTrace);
			String ^errmsg = "path:"+AppDomain::CurrentDomain->BaseDirectory + " error:"+e->Message;
			login[4].len = errmsg->Length;
			login[4].string = (char*)(void*)Marshal::StringToHGlobalAnsi( errmsg );
			return 0;
		}
	}

	MT4_EXPFUNC bool __stdcall FxSubDeinit(int sessionId, MqlStr login[]) {
		try
		{
			Session ^session = Globals::getSession(sessionId);
#ifdef DEBUG
            session->log("FxSubDeinit()  ...  sessionId:"+sessionId);
#else
            session->log("FxSubDeinit()  ...");
#endif
			Globals::deinitSession(sessionId);
			return true;
		}
		catch ( Exception^ e ) 
		{
            Console::WriteLine("Exception:");
			Console::WriteLine( e->Message );
            Console::WriteLine(e->StackTrace);
			String ^errmsg = "path:"+AppDomain::CurrentDomain->BaseDirectory + " error:"+e->Message;
			login[4].len = errmsg->Length;
			login[4].string = (char*)(void*)Marshal::StringToHGlobalAnsi( errmsg );
			return false;
		}
	}

	MT4_EXPFUNC bool __stdcall FxSubInvoke(int sessionId, int method, int time, double arguments[], double variables[], MqlStr strings[]) {
		try
		{
			Session ^session = Globals::getSession(sessionId);
			session->runMethod(method, time, arguments, variables, strings);
			return true;
		}
		catch ( Exception^ e ) 
		{
            Console::WriteLine("Exception:");
			Console::WriteLine( e->Message );
            Console::WriteLine(e->StackTrace);
			String ^errmsg = "path:"+AppDomain::CurrentDomain->BaseDirectory + " error:"+e->Message;
			strings[0].len = errmsg->Length;
			strings[0].string = (char*)(void*)Marshal::StringToHGlobalAnsi( errmsg );
			return false;
		}
	}

#ifdef __cplusplus
}
#endif
