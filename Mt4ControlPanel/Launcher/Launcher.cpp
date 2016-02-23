// Launcher.cpp : main project file.

#include "stdafx.h"
#include "Mt4ControlPanel.h"

using namespace Mt4ControlPanelGui;
using namespace System;
using namespace System::Threading;

[STAThreadAttribute]
int main(array<String ^> ^args)
{
	Random^ randObj= gcnew Random;

	Init("EURUSD M1", 5);

	Tick(0, 0, 1, 0, 0);
	AddOrder(12123340, 0, 0.1, 1.34500, 2000000, 1.34100, 1.35000, 0, 0, 0, "cmt 1");

	double dargs[100];
	MqlStr sargs[10];
	sargs[0].string = 0;
	sargs[0].len = 0;
	int ticket = 20000000;
	while (MainForm::Running)
	{
		double rndBid = 1.344 + randObj->Next(0,1000) * 0.001 * 0.002;
		double rndSpread = randObj->Next(0,1000) * 0.001 * 0.00020;
		rndBid = Math::Round(rndBid, 5);
		rndSpread = Math::Round(rndSpread, 5);

		dargs[1] = -1;
		Tick(rndBid, rndBid + rndSpread, -1, dargs, sargs);

		if (dargs[1] >= 0) {
			Tick(rndBid, rndBid + rndSpread, 1, dargs, sargs);

			AddOrder(ticket, dargs[1], dargs[2], dargs[3], PropertyHandler::ToUnixTime(DateTime::Now), dargs[5], dargs[6], dargs[7], 0, dargs[9], sargs[0].string);

			ticket++;
		}
		Thread::Sleep(randObj->Next(10,2000));
	}

	return 0;
}
