// mdpanalyzer.h

#pragma once

#include <vector>
#include <map>
#include "zsfTraderLib.h"

using namespace System;

namespace mdpanalyzer {

	enum Status {
		NO_TRADE,
		TRADE_LONG,
		TRADE_SHORT
	};

	enum XAxisOption {
		BOLLINGER_WIDTH,
		BOLLINGER_DEVIATION
	};

	enum DarknessOption {
		MAX_MIN,
		AVERAGE,
		SIMULATED
	};

	const char *XAxisOption_str[];

	const char *DarknessOption_str[];

	class MDP {
	public:
		bool Deinitialized;

		TradingRuntime &runtime;

		double point;

		double spread;

		int channelPeriodMillis[10];

		MDP(TradingRuntime &runtime, double point, double spread, int channelPeriodMillis[]);
	};

	struct AnalysisField {

		Field field;

		MovingAvgField mavgField;

		BollingerField bollField;

		AnalysisField(Field &field, MovingAvgField &mavgField, BollingerField &bollField);
	};

	class FieldAnalysis {
		MDP *mdp;

		int channel;

		double maxPrice,minPrice,maxDeviation;

		AnalysisField *fieldFrom;

		AnalysisField *fieldTo;

		void start(Field &field, MovingAvgField &mavgField, BollingerField &bollField) ;

		void end(Field &field, MovingAvgField &mavgField, BollingerField &bollField) ;

	public:

		FieldAnalysis(MDP *mdp, int channel, Field &field, MovingAvgField &mavgField, BollingerField &bollField) ;

		~FieldAnalysis() ;

		Status updateShort(Field &field, MovingAvgField &mavgField, BollingerField &bollField) ;

		Status updateLong(Field &field, MovingAvgField &mavgField, BollingerField &bollField) ;
	};

	class MDPAnalyzer {
		MDP *mdp;

		int channel;

		int lastInd;

		Status status;

		FieldAnalysis *currentFA;

		std::vector<FieldAnalysis*> result;

	public:
		MDPAnalyzer(MDP *mdp, int _channel) ;

		void run(const int time, const int timeMillis, const double bid, const double ask) ;

	};

	array<String^> ^getChannelInfo(int id);

	Image ^generateAnalysisImage(	int id, int channelA, int channelB, 
									XAxisOption xAxisOpt, DarknessOption darknessOpt, 
									int width, int height);

	Image ^generateChartImage(	int id, int channel, int height);

	void CheckWindowClosed(int id) ;
}