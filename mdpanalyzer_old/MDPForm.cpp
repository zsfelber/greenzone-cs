#include "StdAfx.h"

#include "MDPForm.h"
#include "mdpanalyzer.h"

namespace mdpanalyzer {

	MDPForm::MDPForm(int _id, double _point, double _spread) : 
		id(_id),point(_point),spread(_spread),finished(false)
	{
		String ^s="";

		InitializeComponent();

		int dp = (int)log(0.1,point);
		numUpDownPoint->Value = Decimal(point);
		numUpDownPoint->DecimalPlaces = dp;

		numUpDownSpread->Value = Decimal(spread);
		numUpDownSpread->DecimalPlaces = dp;

		numUpDownSL->Increment = Decimal(point);
		numUpDownSL->DecimalPlaces = dp;

		numUpDownTrailingSL->Increment = Decimal(point);
		numUpDownTrailingSL->DecimalPlaces = dp;

		array<String^> ^info = getChannelInfo(id);
		for each (String ^channelInf in info) {
			comboBoxA->Items->Add(channelInf);
			comboBoxB->Items->Add(channelInf);
			comboBoxChartChannel->Items->Add(channelInf);
		}
	}

	MDPForm::~MDPForm()
	{
		if (components)
		{
			delete components;
		}
	}

	void MDPForm::start()
	{
		Thread^ oThread = gcnew Thread( gcnew ThreadStart( this, &MDPForm::startImpl ) );
		oThread->Start();
	}

	void MDPForm::startImpl()
	{
		Application::Run(this);
		Monitor::Enter(this);
		try {
			finished = true;
		} finally {
			Monitor::Exit(this);
		}
	}

	void MDPForm::markLoaded() {
		Monitor::Enter(this);
		try {
			progressBar1->Style = ProgressBarStyle::Blocks;
			Enabled = true;
		} finally {
			Monitor::Exit(this);
		}
	}

	void MDPForm::generateImageAnalysis() {
		int channelA = comboBoxA->SelectedIndex;
		int channelB = comboBoxB->SelectedIndex;
		XAxisOption xAxisOpt;
		if (radioXaxis1->Checked) {
			xAxisOpt = BOLLINGER_WIDTH;
		} else {
			xAxisOpt = BOLLINGER_DEVIATION;
		}
		DarknessOption darknessOpt;
		if (radioDark1->Checked) {
			darknessOpt = MAX_MIN;
		} else if (radioDark2->Checked) {
			darknessOpt = AVERAGE;
		} else {
			darknessOpt = SIMULATED;
		}

		pictureBox1->Image = generateAnalysisImage(	id, channelA, channelB, 
													xAxisOpt, darknessOpt, 
													400, 400);
	}

	void MDPForm::saveImageAnalysis() {
		MemoryStream ^ms = gcnew MemoryStream();       
		pictureBox1->Image->Save(ms, Imaging::ImageFormat::Png);
		array<unsigned char> ^bmpBytes = ms->GetBuffer();
		// bmpOut->Dispose();
		ms->Close();
	}

	void MDPForm::generateImageChart() {
		int channel = comboBoxChartChannel->SelectedIndex;

		pictureBox2->Image = generateChartImage(	id, channel, 580);
	}

	void MDPForm::saveImageChart() {
		MemoryStream ^ms = gcnew MemoryStream();       
		pictureBox2->Image->Save(ms, Imaging::ImageFormat::Png);
		array<unsigned char> ^bmpBytes = ms->GetBuffer();
		// bmpOut->Dispose();
		ms->Close();
	}

	System::Void MDPForm::checkBoxMvInterv_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
		bool checked = checkBoxMvInterv->Checked;
		comboBoxIntervLen->Enabled = checked;
		comboBoxIntervStep->Enabled = checked;
		trackBarMvInterv->Enabled = checked;
	}

	System::Void MDPForm::radioDark3_CheckedChanged(System::Object^  sender, System::EventArgs^  e) {
		numUpDownSL->Enabled=radioDark3->Checked;
		numUpDownTrailingSL->Enabled=radioDark3->Checked;
	}

	System::Void MDPForm::drawButton1_Click(System::Object^  sender, System::EventArgs^  e) {
		generateImageAnalysis();
	}

	System::Void MDPForm::drawButton2_Click(System::Object^  sender, System::EventArgs^  e) {
		generateImageChart();
	}

	System::Void MDPForm::numUpDownSL_ValueChanged(System::Object^  sender, System::EventArgs^  e) {
		if (numUpDownSL->Value.CompareTo(Decimal(0))<0) {
			numUpDownSL->Value = Decimal(0);
		}
		if (numUpDownSL->Value.CompareTo(Decimal(500*point))>0) {
			numUpDownSL->Value = Decimal(500*point);
		}
	}
	System::Void MDPForm::numUpDownTrailingSL_ValueChanged(System::Object^  sender, System::EventArgs^  e) {
		if (numUpDownTrailingSL->Value.CompareTo(Decimal(0))<0) {
			numUpDownTrailingSL->Value = Decimal(0);
		}
		if (numUpDownTrailingSL->Value.CompareTo(Decimal(500*point))>0) {
			numUpDownTrailingSL->Value = Decimal(500*point);
		}
	}
	System::Void MDPForm::MDPForm_FormClosed(System::Object^  sender, System::Windows::Forms::FormClosedEventArgs^  e) {
		CheckWindowClosed(id);
	}

}
