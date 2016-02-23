#pragma once

#define MT4_IMPFUNC __declspec(dllimport)
#define export extern "C" __declspec( dllimport )

#ifdef __cplusplus
extern "C"
{
#endif

	struct MqlStr
	{
		int len;
		char *string;
	};

	void MT4_IMPFUNC __stdcall Init(const char* symbol, int digits);

	void MT4_IMPFUNC __stdcall Tick(double bid, double ask, int numorders, double *dargs, MqlStr *sargs);

	void MT4_IMPFUNC __stdcall AddOrder(int ticket, int type, double lots, double openPrice, int openTime, double sl, double tp, double closePrice, int closeTime, int magic, const char* comment);



#ifdef __cplusplus
}
#endif
