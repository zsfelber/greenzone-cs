#pragma once

#include "SmartLog.h"

using namespace System;
using namespace System::IO;

public ref class SmartLog {
	static EALogger::DEALogger ^Logger = nullptr;
	static Mutex *MxLogFile = NULL;
	static CSmartLog *cSmartLog = NULL;
	static CSmartLog *cliSmartLog = NULL;

	static void InitLog(CSmartLog *log, String ^file);

public:
	static Mutex &mx();
	static CSmartLog &cs();
	static CSmartLog &clis();
	static void InitC(String ^file);
	static void InitCli(String ^file);

private:
	static void Log(String ^s, int severity, String ^filePath, String ^lineNumber, String ^function, String ^functionSign) ;
};

void InitSmartLogC(String ^file);
void InitSmartLogCli(String ^file);
