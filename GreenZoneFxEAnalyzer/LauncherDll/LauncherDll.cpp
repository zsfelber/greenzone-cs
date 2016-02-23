// This is the main DLL file.

#include "stdafx.h"
using namespace System;
using namespace System::IO;
using namespace System::Reflection;
using namespace System::Windows::Forms;

#include "LauncherDll.h"



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

	ref class Globals {
		internal :
		static Assembly ^assembly;
		static Type ^entryPoint;
	};

	MT4_EXPFUNC void __stdcall launchGreenZoneFxAnalyzer() {
		try
		{
            File::AppendAllText("fxanalyzer.out", "launchGreenZoneFxAnalyzer()  1"+"\n");

			if (Globals::assembly == nullptr) {
				Globals::assembly = Assembly::LoadFrom("experts\\libraries\\GreenZoneFxEAnalyzer.dll");
				Globals::entryPoint = Globals::assembly->GetType("GreenZoneFxEAnalyzer.Program");
			}
            File::AppendAllText("fxanalyzer.out", "launchGreenZoneFxAnalyzer()  2"+"\n");
            //MethodInfo ^m = Globals::entryPoint->GetMethod("StartInThread");
			MethodInfo ^m = Globals::entryPoint->GetMethod("Main");

			m->Invoke(nullptr, gcnew array<Object^>{});
            File::AppendAllText("fxanalyzer.out", "launchGreenZoneFxAnalyzer()  3"+"\n");
		}
		catch ( Exception^ ex ) 
		{
            File::AppendAllText("fxanalyzer.out", "launchGreenZoneFxAnalyzer()  ex:"+ex->Message+"\n"+ex->StackTrace+"\n");
            MessageBox::Show(ex->Message + "\n\nUnable to load green zone fx", "Error", MessageBoxButtons::OK, MessageBoxIcon::Error);
            Console::WriteLine("Exception:");
			Console::WriteLine( ex->Message );
            Console::WriteLine(ex->StackTrace);
		}
	}

	MT4_EXPFUNC void __stdcall finishGreenZoneFxAnalyzer() {
		try
		{
            File::AppendAllText("fxanalyzer.out", "finishGreenZoneFxAnalyzer()  1"+"\n");
            MethodInfo ^m = Globals::entryPoint->GetMethod("Stop");

			m->Invoke(nullptr, gcnew array<Object^>{});
		}
		catch ( Exception^ ex ) 
		{
            File::AppendAllText("fxanalyzer.out", "finishGreenZoneFxAnalyzer()  ex:"+ex->Message+"\n"+ex->StackTrace+"\n");
            MessageBox::Show(ex->Message + "\n\nUnable to stop green zone fx", "Error", MessageBoxButtons::OK, MessageBoxIcon::Error);
            Console::WriteLine("Exception:");
			Console::WriteLine( ex->Message );
            Console::WriteLine(ex->StackTrace);
		}
	}

	MT4_EXPFUNC void __stdcall testRefs(int *i) {
		try
		{
			long addr = (long)(void*)i;
			File::AppendAllText("fxanalyzer.out", "i : "+addr+"\n");

			(*i)++;
		}
		catch ( Exception^ ex ) 
		{
			File::AppendAllText("fxanalyzer.out", "testRefs()  ex:"+ex->Message+"\n"+ex->StackTrace+"\n");
			MessageBox::Show(ex->Message + "\n\nUnable to load green zone fx", "Error", MessageBoxButtons::OK, MessageBoxIcon::Error);
			Console::WriteLine("Exception:");
			Console::WriteLine( ex->Message );
			Console::WriteLine(ex->StackTrace);
		}
	}


#ifdef __cplusplus
}
#endif
