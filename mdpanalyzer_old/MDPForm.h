#pragma once

namespace mdpanalyzer {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;
	using namespace System::Threading;
	using System::Drawing::Graphics;
	using System::Drawing::Rectangle;

	/// <summary>
	/// Summary for MDPForm
	/// </summary>
	public ref class MDPForm : public System::Windows::Forms::Form
	{
	public:
		MDPForm(int id, double point, double spread);

		~MDPForm();

		void start();

		void markLoaded();

		property bool Finished {
			bool get() {
				Monitor::Enter(this);
				try {
					return finished;
				} finally {
					Monitor::Exit(this);
				}
			}
		}

	protected:

	private: System::Windows::Forms::MenuStrip^  menuStrip1;
	protected: 
	private: System::Windows::Forms::ToolStripMenuItem^  toolStripMenuItem1;
	private: System::Windows::Forms::ToolStripMenuItem^  openToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^  saveAsToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^  saveImageAsToolStripMenuItem;


	private: System::Windows::Forms::CheckBox^  checkBoxMvInterv;


	private: System::Windows::Forms::TextBox^  textBoxCurMvInterv;
	private: System::Windows::Forms::TextBox^  textBox1;
	private: System::Windows::Forms::Label^  label4;
	private: System::Windows::Forms::Label^  label5;
	private: System::Windows::Forms::GroupBox^  groupBox3;
	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::ComboBox^  comboBoxIntervLen;
	private: System::Windows::Forms::ComboBox^  comboBoxIntervStep;

	private: System::Windows::Forms::Label^  label6;
	private: System::Windows::Forms::GroupBox^  groupBox4;
	private: System::Windows::Forms::GroupBox^  groupBox5;
	private: System::Windows::Forms::Button^  drawButton1;

	private: System::Windows::Forms::ProgressBar^  progressBar1;
	private: System::Windows::Forms::TabControl^  tabControl1;
	private: System::Windows::Forms::TabPage^  tabPage1;
	private: System::Windows::Forms::PictureBox^  pictureBox1;
	private: System::Windows::Forms::PictureBox^  pictureBox2;
	private: System::Windows::Forms::TrackBar^  trackBarMvInterv;
	private: System::Windows::Forms::GroupBox^  groupBox6;
	private: System::Windows::Forms::Label^  label7;
	private: System::Windows::Forms::NumericUpDown^  numUpDownSpread;
	private: System::Windows::Forms::Label^  label8;
	private: System::Windows::Forms::NumericUpDown^  numUpDownTrailingSL;

	private: System::Windows::Forms::NumericUpDown^  numUpDownSL;

	private: System::Windows::Forms::Label^  label9;
	private: System::Windows::Forms::RadioButton^  radioDark3;
	private: System::Windows::Forms::Label^  label10;
	private: System::Windows::Forms::NumericUpDown^  numUpDownPoint;
	private: System::Windows::Forms::Label^  label11;
	private: System::Windows::Forms::ComboBox^  comboBoxChartChannel;
	private: System::Windows::Forms::Button^  drawButton2;





	private: System::Windows::Forms::TabPage^  tabPage2;





	protected: 

	private: 
		void startImpl();

		void generateImageAnalysis() ;

		void saveImageAnalysis() ;

		void generateImageChart() ;

		void saveImageChart() ;

		int id;

		double point;

		double spread;

		bool finished;

	private: System::Windows::Forms::GroupBox^  groupBox1;
	private: System::Windows::Forms::RadioButton^  radioXaxis2;
	private: System::Windows::Forms::RadioButton^  radioXaxis1;
	private: System::Windows::Forms::GroupBox^  groupBox2;
	private: System::Windows::Forms::RadioButton^  radioDark2;
	private: System::Windows::Forms::RadioButton^  radioDark1;
	private: System::Windows::Forms::ComboBox^  comboBoxA;
	private: System::Windows::Forms::ComboBox^  comboBoxB;


	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::Label^  label2;


	protected: 

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->radioXaxis2 = (gcnew System::Windows::Forms::RadioButton());
			this->radioXaxis1 = (gcnew System::Windows::Forms::RadioButton());
			this->groupBox2 = (gcnew System::Windows::Forms::GroupBox());
			this->radioDark3 = (gcnew System::Windows::Forms::RadioButton());
			this->radioDark2 = (gcnew System::Windows::Forms::RadioButton());
			this->radioDark1 = (gcnew System::Windows::Forms::RadioButton());
			this->comboBoxA = (gcnew System::Windows::Forms::ComboBox());
			this->comboBoxB = (gcnew System::Windows::Forms::ComboBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->menuStrip1 = (gcnew System::Windows::Forms::MenuStrip());
			this->toolStripMenuItem1 = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->openToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->saveAsToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->saveImageAsToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->checkBoxMvInterv = (gcnew System::Windows::Forms::CheckBox());
			this->textBoxCurMvInterv = (gcnew System::Windows::Forms::TextBox());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->groupBox3 = (gcnew System::Windows::Forms::GroupBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->comboBoxIntervLen = (gcnew System::Windows::Forms::ComboBox());
			this->comboBoxIntervStep = (gcnew System::Windows::Forms::ComboBox());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->groupBox4 = (gcnew System::Windows::Forms::GroupBox());
			this->groupBox5 = (gcnew System::Windows::Forms::GroupBox());
			this->drawButton1 = (gcnew System::Windows::Forms::Button());
			this->progressBar1 = (gcnew System::Windows::Forms::ProgressBar());
			this->tabControl1 = (gcnew System::Windows::Forms::TabControl());
			this->tabPage1 = (gcnew System::Windows::Forms::TabPage());
			this->groupBox6 = (gcnew System::Windows::Forms::GroupBox());
			this->numUpDownPoint = (gcnew System::Windows::Forms::NumericUpDown());
			this->label10 = (gcnew System::Windows::Forms::Label());
			this->numUpDownTrailingSL = (gcnew System::Windows::Forms::NumericUpDown());
			this->numUpDownSL = (gcnew System::Windows::Forms::NumericUpDown());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->numUpDownSpread = (gcnew System::Windows::Forms::NumericUpDown());
			this->pictureBox1 = (gcnew System::Windows::Forms::PictureBox());
			this->trackBarMvInterv = (gcnew System::Windows::Forms::TrackBar());
			this->tabPage2 = (gcnew System::Windows::Forms::TabPage());
			this->drawButton2 = (gcnew System::Windows::Forms::Button());
			this->label11 = (gcnew System::Windows::Forms::Label());
			this->comboBoxChartChannel = (gcnew System::Windows::Forms::ComboBox());
			this->pictureBox2 = (gcnew System::Windows::Forms::PictureBox());
			this->groupBox1->SuspendLayout();
			this->groupBox2->SuspendLayout();
			this->menuStrip1->SuspendLayout();
			this->groupBox3->SuspendLayout();
			this->groupBox4->SuspendLayout();
			this->groupBox5->SuspendLayout();
			this->tabControl1->SuspendLayout();
			this->tabPage1->SuspendLayout();
			this->groupBox6->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numUpDownPoint))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numUpDownTrailingSL))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numUpDownSL))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numUpDownSpread))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox1))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->trackBarMvInterv))->BeginInit();
			this->tabPage2->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox2))->BeginInit();
			this->SuspendLayout();
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->radioXaxis2);
			this->groupBox1->Controls->Add(this->radioXaxis1);
			this->groupBox1->Location = System::Drawing::Point(412, 89);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(190, 73);
			this->groupBox1->TabIndex = 1;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = L"X axis";
			// 
			// radioXaxis2
			// 
			this->radioXaxis2->AutoSize = true;
			this->radioXaxis2->Location = System::Drawing::Point(6, 42);
			this->radioXaxis2->Name = L"radioXaxis2";
			this->radioXaxis2->Size = System::Drawing::Size(111, 17);
			this->radioXaxis2->TabIndex = 1;
			this->radioXaxis2->Text = L"Bollinger deviation";
			this->radioXaxis2->UseVisualStyleBackColor = true;
			// 
			// radioXaxis1
			// 
			this->radioXaxis1->AutoSize = true;
			this->radioXaxis1->Checked = true;
			this->radioXaxis1->Location = System::Drawing::Point(6, 19);
			this->radioXaxis1->Name = L"radioXaxis1";
			this->radioXaxis1->Size = System::Drawing::Size(93, 17);
			this->radioXaxis1->TabIndex = 0;
			this->radioXaxis1->TabStop = true;
			this->radioXaxis1->Text = L"Bollinger width";
			this->radioXaxis1->UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this->groupBox2->Controls->Add(this->radioDark3);
			this->groupBox2->Controls->Add(this->radioDark2);
			this->groupBox2->Controls->Add(this->radioDark1);
			this->groupBox2->Location = System::Drawing::Point(412, 168);
			this->groupBox2->Name = L"groupBox2";
			this->groupBox2->Size = System::Drawing::Size(190, 95);
			this->groupBox2->TabIndex = 2;
			this->groupBox2->TabStop = false;
			this->groupBox2->Text = L"Darkness code";
			// 
			// radioDark3
			// 
			this->radioDark3->AutoSize = true;
			this->radioDark3->Location = System::Drawing::Point(6, 65);
			this->radioDark3->Name = L"radioDark3";
			this->radioDark3->Size = System::Drawing::Size(141, 17);
			this->radioDark3->TabIndex = 2;
			this->radioDark3->TabStop = true;
			this->radioDark3->Text = L"Simulated by Parameters";
			this->radioDark3->UseVisualStyleBackColor = true;
			this->radioDark3->CheckedChanged += gcnew System::EventHandler(this, &MDPForm::radioDark3_CheckedChanged);
			// 
			// radioDark2
			// 
			this->radioDark2->AutoSize = true;
			this->radioDark2->Location = System::Drawing::Point(6, 42);
			this->radioDark2->Name = L"radioDark2";
			this->radioDark2->Size = System::Drawing::Size(95, 17);
			this->radioDark2->TabIndex = 1;
			this->radioDark2->Text = L"Average Value";
			this->radioDark2->UseVisualStyleBackColor = true;
			// 
			// radioDark1
			// 
			this->radioDark1->AutoSize = true;
			this->radioDark1->Checked = true;
			this->radioDark1->Location = System::Drawing::Point(6, 19);
			this->radioDark1->Name = L"radioDark1";
			this->radioDark1->Size = System::Drawing::Size(143, 17);
			this->radioDark1->TabIndex = 0;
			this->radioDark1->TabStop = true;
			this->radioDark1->Text = L"Maximum minus Minimum";
			this->radioDark1->UseVisualStyleBackColor = true;
			// 
			// comboBoxA
			// 
			this->comboBoxA->FormattingEnabled = true;
			this->comboBoxA->Location = System::Drawing::Point(63, 19);
			this->comboBoxA->Name = L"comboBoxA";
			this->comboBoxA->Size = System::Drawing::Size(121, 21);
			this->comboBoxA->TabIndex = 3;
			// 
			// comboBoxB
			// 
			this->comboBoxB->FormattingEnabled = true;
			this->comboBoxB->Location = System::Drawing::Point(63, 46);
			this->comboBoxB->Name = L"comboBoxB";
			this->comboBoxB->Size = System::Drawing::Size(121, 21);
			this->comboBoxB->TabIndex = 4;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(3, 22);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(56, 13);
			this->label1->TabIndex = 5;
			this->label1->Text = L"Channel A";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(3, 49);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(56, 13);
			this->label2->TabIndex = 6;
			this->label2->Text = L"Channel B";
			// 
			// menuStrip1
			// 
			this->menuStrip1->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) {this->toolStripMenuItem1});
			this->menuStrip1->Location = System::Drawing::Point(0, 0);
			this->menuStrip1->Name = L"menuStrip1";
			this->menuStrip1->Size = System::Drawing::Size(733, 24);
			this->menuStrip1->TabIndex = 7;
			this->menuStrip1->Text = L"menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this->toolStripMenuItem1->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(3) {this->openToolStripMenuItem, 
				this->saveAsToolStripMenuItem, this->saveImageAsToolStripMenuItem});
			this->toolStripMenuItem1->Name = L"toolStripMenuItem1";
			this->toolStripMenuItem1->Size = System::Drawing::Size(35, 20);
			this->toolStripMenuItem1->Text = L"File";
			// 
			// openToolStripMenuItem
			// 
			this->openToolStripMenuItem->Name = L"openToolStripMenuItem";
			this->openToolStripMenuItem->Size = System::Drawing::Size(157, 22);
			this->openToolStripMenuItem->Text = L"Load";
			// 
			// saveAsToolStripMenuItem
			// 
			this->saveAsToolStripMenuItem->Name = L"saveAsToolStripMenuItem";
			this->saveAsToolStripMenuItem->Size = System::Drawing::Size(157, 22);
			this->saveAsToolStripMenuItem->Text = L"Save As";
			// 
			// saveImageAsToolStripMenuItem
			// 
			this->saveImageAsToolStripMenuItem->Name = L"saveImageAsToolStripMenuItem";
			this->saveImageAsToolStripMenuItem->Size = System::Drawing::Size(157, 22);
			this->saveImageAsToolStripMenuItem->Text = L"Save Image As";
			// 
			// checkBoxMvInterv
			// 
			this->checkBoxMvInterv->AutoSize = true;
			this->checkBoxMvInterv->Location = System::Drawing::Point(6, 22);
			this->checkBoxMvInterv->Name = L"checkBoxMvInterv";
			this->checkBoxMvInterv->Size = System::Drawing::Size(133, 17);
			this->checkBoxMvInterv->TabIndex = 9;
			this->checkBoxMvInterv->Text = L"Enable moving interval";
			this->checkBoxMvInterv->UseVisualStyleBackColor = true;
			this->checkBoxMvInterv->CheckedChanged += gcnew System::EventHandler(this, &MDPForm::checkBoxMvInterv_CheckedChanged);
			// 
			// textBoxCurMvInterv
			// 
			this->textBoxCurMvInterv->Location = System::Drawing::Point(63, 19);
			this->textBoxCurMvInterv->Name = L"textBoxCurMvInterv";
			this->textBoxCurMvInterv->ReadOnly = true;
			this->textBoxCurMvInterv->Size = System::Drawing::Size(121, 20);
			this->textBoxCurMvInterv->TabIndex = 11;
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(63, 45);
			this->textBox1->Name = L"textBox1";
			this->textBox1->ReadOnly = true;
			this->textBox1->Size = System::Drawing::Size(121, 20);
			this->textBox1->TabIndex = 12;
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(3, 45);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(20, 13);
			this->label4->TabIndex = 13;
			this->label4->Text = L"To";
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(3, 22);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(30, 13);
			this->label5->TabIndex = 14;
			this->label5->Text = L"From";
			// 
			// groupBox3
			// 
			this->groupBox3->Controls->Add(this->textBoxCurMvInterv);
			this->groupBox3->Controls->Add(this->label5);
			this->groupBox3->Controls->Add(this->textBox1);
			this->groupBox3->Controls->Add(this->label4);
			this->groupBox3->Location = System::Drawing::Point(412, 412);
			this->groupBox3->Name = L"groupBox3";
			this->groupBox3->Size = System::Drawing::Size(190, 72);
			this->groupBox3->TabIndex = 15;
			this->groupBox3->TabStop = false;
			this->groupBox3->Text = L"Current Interval";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(3, 57);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(40, 13);
			this->label3->TabIndex = 16;
			this->label3->Text = L"Length";
			// 
			// comboBoxIntervLen
			// 
			this->comboBoxIntervLen->Enabled = false;
			this->comboBoxIntervLen->FormattingEnabled = true;
			this->comboBoxIntervLen->Location = System::Drawing::Point(63, 54);
			this->comboBoxIntervLen->Name = L"comboBoxIntervLen";
			this->comboBoxIntervLen->Size = System::Drawing::Size(121, 21);
			this->comboBoxIntervLen->TabIndex = 17;
			// 
			// comboBoxIntervStep
			// 
			this->comboBoxIntervStep->Enabled = false;
			this->comboBoxIntervStep->FormattingEnabled = true;
			this->comboBoxIntervStep->Location = System::Drawing::Point(63, 81);
			this->comboBoxIntervStep->Name = L"comboBoxIntervStep";
			this->comboBoxIntervStep->Size = System::Drawing::Size(121, 21);
			this->comboBoxIntervStep->TabIndex = 18;
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Location = System::Drawing::Point(3, 84);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(29, 13);
			this->label6->TabIndex = 19;
			this->label6->Text = L"Step";
			// 
			// groupBox4
			// 
			this->groupBox4->Controls->Add(this->comboBoxIntervLen);
			this->groupBox4->Controls->Add(this->label6);
			this->groupBox4->Controls->Add(this->checkBoxMvInterv);
			this->groupBox4->Controls->Add(this->label3);
			this->groupBox4->Controls->Add(this->comboBoxIntervStep);
			this->groupBox4->Location = System::Drawing::Point(412, 287);
			this->groupBox4->Name = L"groupBox4";
			this->groupBox4->Size = System::Drawing::Size(190, 119);
			this->groupBox4->TabIndex = 20;
			this->groupBox4->TabStop = false;
			this->groupBox4->Text = L"Moving interval";
			// 
			// groupBox5
			// 
			this->groupBox5->Controls->Add(this->comboBoxA);
			this->groupBox5->Controls->Add(this->comboBoxB);
			this->groupBox5->Controls->Add(this->label1);
			this->groupBox5->Controls->Add(this->label2);
			this->groupBox5->Location = System::Drawing::Point(412, 2);
			this->groupBox5->Name = L"groupBox5";
			this->groupBox5->Size = System::Drawing::Size(190, 81);
			this->groupBox5->TabIndex = 21;
			this->groupBox5->TabStop = false;
			this->groupBox5->Text = L"Channels";
			// 
			// drawButton1
			// 
			this->drawButton1->Location = System::Drawing::Point(613, 265);
			this->drawButton1->Name = L"drawButton1";
			this->drawButton1->Size = System::Drawing::Size(75, 23);
			this->drawButton1->TabIndex = 22;
			this->drawButton1->Text = L"Draw";
			this->drawButton1->UseVisualStyleBackColor = true;
			this->drawButton1->Click += gcnew System::EventHandler(this, &MDPForm::drawButton1_Click);
			// 
			// progressBar1
			// 
			this->progressBar1->Location = System::Drawing::Point(412, 269);
			this->progressBar1->Name = L"progressBar1";
			this->progressBar1->Size = System::Drawing::Size(190, 15);
			this->progressBar1->Style = System::Windows::Forms::ProgressBarStyle::Marquee;
			this->progressBar1->TabIndex = 23;
			// 
			// tabControl1
			// 
			this->tabControl1->Controls->Add(this->tabPage1);
			this->tabControl1->Controls->Add(this->tabPage2);
			this->tabControl1->Location = System::Drawing::Point(0, 27);
			this->tabControl1->Name = L"tabControl1";
			this->tabControl1->SelectedIndex = 0;
			this->tabControl1->Size = System::Drawing::Size(733, 529);
			this->tabControl1->TabIndex = 24;
			// 
			// tabPage1
			// 
			this->tabPage1->BackColor = System::Drawing::SystemColors::Control;
			this->tabPage1->Controls->Add(this->groupBox6);
			this->tabPage1->Controls->Add(this->pictureBox1);
			this->tabPage1->Controls->Add(this->progressBar1);
			this->tabPage1->Controls->Add(this->groupBox1);
			this->tabPage1->Controls->Add(this->drawButton1);
			this->tabPage1->Controls->Add(this->groupBox2);
			this->tabPage1->Controls->Add(this->groupBox5);
			this->tabPage1->Controls->Add(this->trackBarMvInterv);
			this->tabPage1->Controls->Add(this->groupBox4);
			this->tabPage1->Controls->Add(this->groupBox3);
			this->tabPage1->Location = System::Drawing::Point(4, 22);
			this->tabPage1->Name = L"tabPage1";
			this->tabPage1->Padding = System::Windows::Forms::Padding(3);
			this->tabPage1->Size = System::Drawing::Size(725, 503);
			this->tabPage1->TabIndex = 0;
			this->tabPage1->Text = L"Analysis";
			// 
			// groupBox6
			// 
			this->groupBox6->Controls->Add(this->numUpDownPoint);
			this->groupBox6->Controls->Add(this->label10);
			this->groupBox6->Controls->Add(this->numUpDownTrailingSL);
			this->groupBox6->Controls->Add(this->numUpDownSL);
			this->groupBox6->Controls->Add(this->label9);
			this->groupBox6->Controls->Add(this->label8);
			this->groupBox6->Controls->Add(this->label7);
			this->groupBox6->Controls->Add(this->numUpDownSpread);
			this->groupBox6->Location = System::Drawing::Point(613, 2);
			this->groupBox6->Name = L"groupBox6";
			this->groupBox6->Size = System::Drawing::Size(104, 261);
			this->groupBox6->TabIndex = 25;
			this->groupBox6->TabStop = false;
			this->groupBox6->Text = L"Parameters";
			// 
			// numUpDownPoint
			// 
			this->numUpDownPoint->Enabled = false;
			this->numUpDownPoint->Location = System::Drawing::Point(6, 32);
			this->numUpDownPoint->Name = L"numUpDownPoint";
			this->numUpDownPoint->ReadOnly = true;
			this->numUpDownPoint->Size = System::Drawing::Size(92, 20);
			this->numUpDownPoint->TabIndex = 10;
			// 
			// label10
			// 
			this->label10->AutoSize = true;
			this->label10->Location = System::Drawing::Point(3, 16);
			this->label10->Name = L"label10";
			this->label10->Size = System::Drawing::Size(31, 13);
			this->label10->TabIndex = 9;
			this->label10->Text = L"Point";
			// 
			// numUpDownTrailingSL
			// 
			this->numUpDownTrailingSL->Enabled = false;
			this->numUpDownTrailingSL->Location = System::Drawing::Point(6, 166);
			this->numUpDownTrailingSL->Name = L"numUpDownTrailingSL";
			this->numUpDownTrailingSL->Size = System::Drawing::Size(92, 20);
			this->numUpDownTrailingSL->TabIndex = 7;
			this->numUpDownTrailingSL->ValueChanged += gcnew System::EventHandler(this, &MDPForm::numUpDownTrailingSL_ValueChanged);
			// 
			// numUpDownSL
			// 
			this->numUpDownSL->Enabled = false;
			this->numUpDownSL->Location = System::Drawing::Point(6, 123);
			this->numUpDownSL->Name = L"numUpDownSL";
			this->numUpDownSL->Size = System::Drawing::Size(92, 20);
			this->numUpDownSL->TabIndex = 6;
			this->numUpDownSL->ValueChanged += gcnew System::EventHandler(this, &MDPForm::numUpDownSL_ValueChanged);
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(3, 150);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(68, 13);
			this->label9->TabIndex = 5;
			this->label9->Text = L"Trailing S/L :";
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(3, 107);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(31, 13);
			this->label8->TabIndex = 4;
			this->label8->Text = L"S/L :";
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Location = System::Drawing::Point(3, 58);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(47, 13);
			this->label7->TabIndex = 1;
			this->label7->Text = L"Spread :";
			// 
			// numUpDownSpread
			// 
			this->numUpDownSpread->Enabled = false;
			this->numUpDownSpread->Location = System::Drawing::Point(6, 75);
			this->numUpDownSpread->Name = L"numUpDownSpread";
			this->numUpDownSpread->ReadOnly = true;
			this->numUpDownSpread->Size = System::Drawing::Size(92, 20);
			this->numUpDownSpread->TabIndex = 0;
			// 
			// pictureBox1
			// 
			this->pictureBox1->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->pictureBox1->Location = System::Drawing::Point(6, 6);
			this->pictureBox1->Name = L"pictureBox1";
			this->pictureBox1->Size = System::Drawing::Size(400, 400);
			this->pictureBox1->TabIndex = 24;
			this->pictureBox1->TabStop = false;
			// 
			// trackBarMvInterv
			// 
			this->trackBarMvInterv->Enabled = false;
			this->trackBarMvInterv->Location = System::Drawing::Point(6, 425);
			this->trackBarMvInterv->Margin = System::Windows::Forms::Padding(3, 0, 3, 0);
			this->trackBarMvInterv->Name = L"trackBarMvInterv";
			this->trackBarMvInterv->Size = System::Drawing::Size(400, 45);
			this->trackBarMvInterv->TabIndex = 8;
			// 
			// tabPage2
			// 
			this->tabPage2->BackColor = System::Drawing::SystemColors::Control;
			this->tabPage2->Controls->Add(this->drawButton2);
			this->tabPage2->Controls->Add(this->label11);
			this->tabPage2->Controls->Add(this->comboBoxChartChannel);
			this->tabPage2->Controls->Add(this->pictureBox2);
			this->tabPage2->Location = System::Drawing::Point(4, 22);
			this->tabPage2->Name = L"tabPage2";
			this->tabPage2->Padding = System::Windows::Forms::Padding(3);
			this->tabPage2->Size = System::Drawing::Size(725, 503);
			this->tabPage2->TabIndex = 1;
			this->tabPage2->Text = L"View Chart";
			// 
			// drawButton2
			// 
			this->drawButton2->Location = System::Drawing::Point(249, 392);
			this->drawButton2->Name = L"drawButton2";
			this->drawButton2->Size = System::Drawing::Size(75, 23);
			this->drawButton2->TabIndex = 3;
			this->drawButton2->Text = L"Draw";
			this->drawButton2->UseVisualStyleBackColor = true;
			this->drawButton2->Click += gcnew System::EventHandler(this, &MDPForm::drawButton2_Click);
			// 
			// label11
			// 
			this->label11->AutoSize = true;
			this->label11->Location = System::Drawing::Point(8, 397);
			this->label11->Name = L"label11";
			this->label11->Size = System::Drawing::Size(46, 13);
			this->label11->TabIndex = 2;
			this->label11->Text = L"Channel";
			// 
			// comboBoxChartChannel
			// 
			this->comboBoxChartChannel->FormattingEnabled = true;
			this->comboBoxChartChannel->Location = System::Drawing::Point(60, 394);
			this->comboBoxChartChannel->Name = L"comboBoxChartChannel";
			this->comboBoxChartChannel->Size = System::Drawing::Size(121, 21);
			this->comboBoxChartChannel->TabIndex = 1;
			// 
			// pictureBox2
			// 
			this->pictureBox2->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->pictureBox2->Location = System::Drawing::Point(5, 5);
			this->pictureBox2->Name = L"pictureBox2";
			this->pictureBox2->Size = System::Drawing::Size(715, 380);
			this->pictureBox2->TabIndex = 0;
			this->pictureBox2->TabStop = false;
			// 
			// MDPForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(733, 555);
			this->Controls->Add(this->tabControl1);
			this->Controls->Add(this->menuStrip1);
			this->Enabled = false;
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedSingle;
			this->MainMenuStrip = this->menuStrip1;
			this->Name = L"MDPForm";
			this->Text = L"zsf Robots Analy7er";
			this->FormClosed += gcnew System::Windows::Forms::FormClosedEventHandler(this, &MDPForm::MDPForm_FormClosed);
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->groupBox2->ResumeLayout(false);
			this->groupBox2->PerformLayout();
			this->menuStrip1->ResumeLayout(false);
			this->menuStrip1->PerformLayout();
			this->groupBox3->ResumeLayout(false);
			this->groupBox3->PerformLayout();
			this->groupBox4->ResumeLayout(false);
			this->groupBox4->PerformLayout();
			this->groupBox5->ResumeLayout(false);
			this->groupBox5->PerformLayout();
			this->tabControl1->ResumeLayout(false);
			this->tabPage1->ResumeLayout(false);
			this->tabPage1->PerformLayout();
			this->groupBox6->ResumeLayout(false);
			this->groupBox6->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numUpDownPoint))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numUpDownTrailingSL))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numUpDownSL))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->numUpDownSpread))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox1))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->trackBarMvInterv))->EndInit();
			this->tabPage2->ResumeLayout(false);
			this->tabPage2->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->pictureBox2))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

	private: System::Void checkBoxMvInterv_CheckedChanged(System::Object^  sender, System::EventArgs^  e) ;

	private: System::Void radioDark3_CheckedChanged(System::Object^  sender, System::EventArgs^  e) ;

	private: System::Void drawButton1_Click(System::Object^  sender, System::EventArgs^  e) ;

	private: System::Void drawButton2_Click(System::Object^  sender, System::EventArgs^  e) ;

	private: System::Void numUpDownSL_ValueChanged(System::Object^  sender, System::EventArgs^  e) ;

	private: System::Void numUpDownTrailingSL_ValueChanged(System::Object^  sender, System::EventArgs^  e) ;

	private: System::Void MDPForm_FormClosed(System::Object^  sender, System::Windows::Forms::FormClosedEventArgs^  e) ;
};
}
