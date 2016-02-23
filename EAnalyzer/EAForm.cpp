#include "stdafx.h"
#include "EAForm.h"
#include "eanalyzer.h"

using namespace System::Windows::Forms;
using namespace EAnalyzer_CS;

namespace eanalyzer {
	EAForm2::EAForm2(int id, ZsfTraderConfig ^config) :
		EAForm(id, config)
	{
	}

	void EAForm2::EAForm_FormClosed(Object ^sender, FormClosedEventArgs ^e) {
		CheckWindowClosed(Id) ;
	}

}//namespace eanalyzer
