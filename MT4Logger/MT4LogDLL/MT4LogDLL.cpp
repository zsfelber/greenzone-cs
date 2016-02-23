// This is the main DLL file.

#include "stdafx.h"

#include "MT4LogDLL.h"

using namespace System::Runtime::InteropServices;
using namespace System::Text;

/*BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}
*/

__declspec(dllexport) void InitializeLogger(char* filename, int severity) {
	LPTSTR lpFilename=new char[10000];
	HMODULE hDllmodule = GetModuleHandle("MT4Logger");
	GetModuleFileName(hDllmodule,lpFilename,10000);

	String ^explib = gcnew String("\\experts\\libraries\\");
	String ^modulePath = gcnew String(lpFilename);
	int i = modulePath->IndexOf(explib);
	String ^mt4Path = modulePath->Substring(0,i);
	String ^logFile = mt4Path + "\\experts\\files\\" + gcnew String(filename);

	String ^s="";
	smartlog(SEV_INFO) << s+"logFile:"+logFile;

	const char* logFileStr = (char*)(void*)Marshal::StringToHGlobalAnsi(logFile);

	CLogFlags flags;
	LOG_SEVERITY = (LogSeverity)severity;
	__smartlog.SetFilePath(CString(logFileStr));
	__smartlog.GetFlags(flags);
	flags.m_bLineFlag = true;
	flags.m_bFileFlag = true;
	__smartlog.SetFlags(flags);

}

__declspec(dllexport) void PrintLog(char* msg, int severity) {
	smartlog(severity) << msg;
}
