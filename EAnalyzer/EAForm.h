#pragma once
#include "stdafx.h"
#include "zsfTraderLib.h"

using namespace System::Windows::Forms;
using namespace EAnalyzer_CS;

namespace eanalyzer {

	/// <summary>
	/// Summary for EAForm
	/// </summary>
	public ref class EAForm2 : public EAForm
	{
	public:
		EAForm2(int id, ZsfTraderConfig ^config);

	protected: 
		virtual void EAForm_FormClosed(Object ^sender, FormClosedEventArgs ^e) override;
	};

}//namespace eanalyzer
