// eanalyzer.h
// #ifndef __Lib_eanalyzer__
// #define __Lib_eanalyzer__

#pragma once

#if defined DLL_EXPORT
#define DECLDIR __declspec(dllexport)
#else
#define DECLDIR __declspec(dllimport)
#endif

extern "C"
{

	DECLDIR int __stdcall InitializeRobot(
							int id, const char* accountName, int accountNumber, const char* symbol, int period, 
							bool isTesting, bool isDemo, double variables[], 
							// output
							int statusInfo[], char* messageBuffer);

	DECLDIR void __stdcall DeinitializeRobot(const int id) ;

	DECLDIR void __stdcall tick(const int id, const double bid, const double ask, double open, double low, double high, double close, int time) ;


	void CheckWindowClosed(int id) ;


}

// #endif// __Lib_eanalyzer__
