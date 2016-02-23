// EAnalyzerTester.cpp : main project file.

#include "stdafx.h"

#include "eanalyzer.h"
#include "zsfTraderLib.h"

using namespace System;

double random() {
	return ((double)rand())/RAND_MAX;
}

int main(array<System::String ^> ^args)
{

	double variables[34];
	for (int i=0; i<34; i++) variables[i] = 0;
	int statusInfo[1];
	char* messageBuffer = "012345678901234567890123456789";
	variables[MODE_POINT] = 0.00001;
	int id1 = InitializeRobot(0, "Test Account", 12345, "EURUSD", 15, 
							 true, true, variables, 
							 // output
							 statusInfo, messageBuffer);
	variables[MODE_POINT] = 0.001;
	int id2 = InitializeRobot(0, "Test Account", 12345, "USDJPY", 15,
							 true, true, variables, 
							 // output
							 statusInfo, messageBuffer);
	variables[MODE_POINT] = 0.00001;
	int id3 = InitializeRobot(0, "Test Account", 12345, "GBPUSD", 15,
							 true, true, variables, 
							 // output
							 statusInfo, messageBuffer);

	/* initialize random seed: */
	srand ( time(NULL) );
	
	int id[]={id1,id2,id3};
	double bid[] = {1.23000,103.123,1.31001};
	double sprmn[] = {0.00007,0.010,0.00012};
	double sprmx[] = {0.00014,0.018,0.00022};
	double time = 30*365.25*24*60*60;
	for (int i=0; i<100000000000; i++) {
		for (int j=0; j<3; j++) {
			bid[j] *= 0.0099+random()*0.0002;
			double spread = sprmx[j]+random()*(sprmx[j]-sprmn[j]);
			tick(id[j], bid[j], bid[j]+spread, 0, 0, 0, 0, time);
			int slp = random();
			Sleep(slp*1000);
			time += slp;
		}
	}
	

	DeinitializeRobot(id1);
	DeinitializeRobot(id2);
	DeinitializeRobot(id3);

	Console::WriteLine(L"Hello World");

	return 0;
}

