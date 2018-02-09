#pragma once
#include "CallList.h"

namespace testwork {

	using namespace System;
	using namespace System::IO;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::Data::SqlClient;	
	using namespace System::Collections::Generic;	
	using namespace System::Text;
	using namespace testwork;
	
//	using namespace System::Data::OleDb;

	/// <summary>
	/// Summary for Form1
	///
	/// WARNING: If you change the name of this class, you will need to change the
	///          'Resource File Name' property for the managed resource compiler tool
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:		
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
#pragma region vision
	private: System::Windows::Forms::Button^  button1;




































	private: System::Windows::Forms::Button^  button2;







private: System::Windows::Forms::Button^  button3;































	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::Label^  label4;
	private: System::Windows::Forms::Label^  label5;
	private: System::Windows::Forms::Label^  label6;
	private: System::Windows::Forms::Label^  label7;
	private: System::Windows::Forms::Label^  label8;
public: System::Data::DataSet^  dataSet1;
private: 
private: System::Data::DataTable^  dataCustomers;
public: 
private: System::Data::DataColumn^  dataId;
private: System::Data::DataColumn^  dataLogin;
private: System::Data::DataColumn^  dataName;
private: System::Data::DataColumn^  dataBalans;
private: System::Data::DataColumn^  dataTaxId;
private: System::Data::DataColumn^  dataBalans_date;
private: System::Data::DataColumn^  dataСall_direction;
private: System::Data::DataColumn^  dataTarif_id;
private: System::Data::DataTable^  dataPayments;
private: System::Data::DataColumn^  dataColumn1;
private: System::Data::DataColumn^  dataColumn2;
private: System::Data::DataColumn^  dataColumn3;
private: System::Data::DataColumn^  dataColumn4;
private: System::Data::DataTable^  dataPhones;
private: System::Data::DataColumn^  dataColumn5;
private: System::Data::DataColumn^  dataColumn6;
private: System::Data::DataTable^  dataTarif;
private: System::Data::DataColumn^  dataColumn7;
private: System::Data::DataColumn^  dataColumn8;
private: System::Data::DataColumn^  dataColumn9;
private: System::Data::DataTable^  dataTax;
private: System::Data::DataColumn^  dataColumn10;
private: System::Data::DataColumn^  dataColumn11;
private: System::Data::DataColumn^  dataColumn12;
private: System::Data::DataTable^  dataTrunks;
private: System::Data::DataColumn^  dataColumn13;
private: System::Data::DataColumn^  dataColumn14;
private: System::Data::DataTable^  dataCalls;
private: System::Data::DataColumn^  dataColumn15;
private: System::Data::DataColumn^  dataColumn16;
private: System::Data::DataColumn^  dataColumn17;
private: System::Data::DataColumn^  dataColumn18;
private: System::Data::DataColumn^  dataColumn19;
private: System::Windows::Forms::DataGridView^  dataGridView7;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  trunkDataGridViewTextBoxColumn1;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  customerDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridView^  dataGridView5;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  thenumberDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  thedateDataGridViewTextBoxColumn1;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  trunkDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  durationDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  callerIDDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridView^  dataGridView6;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  phoneDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  cidDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridView^  dataGridView4;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  idDataGridViewTextBoxColumn2;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  thesumDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  customerDataGridViewTextBoxColumn1;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  thedateDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridView^  dataGridView3;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  idDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  codeDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  costDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridView^  dataGridView2;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  idDataGridViewTextBoxColumn1;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  codeDataGridViewTextBoxColumn1;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  taxvalueDataGridViewTextBoxColumn;
public: System::Windows::Forms::DataGridView^  dataGridView1;
private: 
private: System::Windows::Forms::DataGridViewTextBoxColumn^  idDataGridViewTextBoxColumn3;
public: 
private: System::Windows::Forms::DataGridViewTextBoxColumn^  loginDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  nameDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  balansDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  taxDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  balansdateDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  calldirectionDataGridViewTextBoxColumn;
private: System::Windows::Forms::DataGridViewTextBoxColumn^  tarifidDataGridViewTextBoxColumn;
private: System::Windows::Forms::Label^  label9;
private: System::Windows::Forms::Label^  label10;
public: System::Windows::Forms::TextBox^  textBox2;
private: 

public: System::Windows::Forms::TextBox^  textBox1;
private: 



































#pragma endregion

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
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->button3 = (gcnew System::Windows::Forms::Button());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->label8 = (gcnew System::Windows::Forms::Label());
			this->dataSet1 = (gcnew System::Data::DataSet());
			this->dataCustomers = (gcnew System::Data::DataTable());
			this->dataId = (gcnew System::Data::DataColumn());
			this->dataLogin = (gcnew System::Data::DataColumn());
			this->dataName = (gcnew System::Data::DataColumn());
			this->dataBalans = (gcnew System::Data::DataColumn());
			this->dataTaxId = (gcnew System::Data::DataColumn());
			this->dataBalans_date = (gcnew System::Data::DataColumn());
			this->dataСall_direction = (gcnew System::Data::DataColumn());
			this->dataTarif_id = (gcnew System::Data::DataColumn());
			this->dataPayments = (gcnew System::Data::DataTable());
			this->dataColumn1 = (gcnew System::Data::DataColumn());
			this->dataColumn2 = (gcnew System::Data::DataColumn());
			this->dataColumn3 = (gcnew System::Data::DataColumn());
			this->dataColumn4 = (gcnew System::Data::DataColumn());
			this->dataPhones = (gcnew System::Data::DataTable());
			this->dataColumn5 = (gcnew System::Data::DataColumn());
			this->dataColumn6 = (gcnew System::Data::DataColumn());
			this->dataTarif = (gcnew System::Data::DataTable());
			this->dataColumn7 = (gcnew System::Data::DataColumn());
			this->dataColumn8 = (gcnew System::Data::DataColumn());
			this->dataColumn9 = (gcnew System::Data::DataColumn());
			this->dataTax = (gcnew System::Data::DataTable());
			this->dataColumn10 = (gcnew System::Data::DataColumn());
			this->dataColumn11 = (gcnew System::Data::DataColumn());
			this->dataColumn12 = (gcnew System::Data::DataColumn());
			this->dataTrunks = (gcnew System::Data::DataTable());
			this->dataColumn13 = (gcnew System::Data::DataColumn());
			this->dataColumn14 = (gcnew System::Data::DataColumn());
			this->dataCalls = (gcnew System::Data::DataTable());
			this->dataColumn15 = (gcnew System::Data::DataColumn());
			this->dataColumn16 = (gcnew System::Data::DataColumn());
			this->dataColumn17 = (gcnew System::Data::DataColumn());
			this->dataColumn18 = (gcnew System::Data::DataColumn());
			this->dataColumn19 = (gcnew System::Data::DataColumn());
			this->dataGridView7 = (gcnew System::Windows::Forms::DataGridView());
			this->trunkDataGridViewTextBoxColumn1 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->customerDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->dataGridView5 = (gcnew System::Windows::Forms::DataGridView());
			this->thenumberDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->thedateDataGridViewTextBoxColumn1 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->trunkDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->durationDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->callerIDDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->dataGridView6 = (gcnew System::Windows::Forms::DataGridView());
			this->phoneDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->cidDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->dataGridView4 = (gcnew System::Windows::Forms::DataGridView());
			this->idDataGridViewTextBoxColumn2 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->thesumDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->customerDataGridViewTextBoxColumn1 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->thedateDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->dataGridView3 = (gcnew System::Windows::Forms::DataGridView());
			this->idDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->codeDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->costDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->dataGridView2 = (gcnew System::Windows::Forms::DataGridView());
			this->idDataGridViewTextBoxColumn1 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->codeDataGridViewTextBoxColumn1 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->taxvalueDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->dataGridView1 = (gcnew System::Windows::Forms::DataGridView());
			this->idDataGridViewTextBoxColumn3 = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->loginDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->nameDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->balansDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->taxDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->balansdateDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->calldirectionDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->tarifidDataGridViewTextBoxColumn = (gcnew System::Windows::Forms::DataGridViewTextBoxColumn());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->label10 = (gcnew System::Windows::Forms::Label());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataSet1))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataCustomers))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataPayments))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataPhones))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataTarif))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataTax))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataTrunks))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataCalls))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView7))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView5))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView6))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView4))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView3))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView2))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView1))->BeginInit();
			this->SuspendLayout();
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(15, 572);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(162, 23);
			this->button1->TabIndex = 0;
			this->button1->Text = L"Пересчитать баланс";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &Form1::button1_Click);
			// 
			// button2
			// 
			this->button2->Location = System::Drawing::Point(238, 572);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(144, 23);
			this->button2->TabIndex = 2;
			this->button2->Text = L"Показать звонки";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &Form1::button2_Click);
			// 
			// button3
			// 
			this->button3->Location = System::Drawing::Point(715, 572);
			this->button3->Name = L"button3";
			this->button3->Size = System::Drawing::Size(179, 23);
			this->button3->TabIndex = 9;
			this->button3->Text = L"Сохранить изменения в БД";
			this->button3->UseVisualStyleBackColor = true;
			this->button3->Click += gcnew System::EventHandler(this, &Form1::button3_Click);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->label3->Location = System::Drawing::Point(81, 189);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(50, 13);
			this->label3->TabIndex = 14;
			this->label3->Text = L"Налоги";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->label4->Location = System::Drawing::Point(82, 359);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(54, 13);
			this->label4->TabIndex = 15;
			this->label4->Text = L"Тарифы";
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->label5->Location = System::Drawing::Point(423, 189);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(59, 13);
			this->label5->TabIndex = 16;
			this->label5->Text = L"Платежи";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->label6->Location = System::Drawing::Point(424, 359);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(50, 13);
			this->label6->TabIndex = 17;
			this->label6->Text = L"Звонки";
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->label7->Location = System::Drawing::Point(766, 189);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(68, 13);
			this->label7->TabIndex = 18;
			this->label7->Text = L"Телефоны";
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 8.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(204)));
			this->label8->Location = System::Drawing::Point(767, 359);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(50, 13);
			this->label8->TabIndex = 19;
			this->label8->Text = L"Транки";
			// 
			// dataSet1
			// 
			this->dataSet1->DataSetName = L"NewDataSet";
			cli::array< System::String^ >^ __mcTemp__1 = gcnew cli::array< System::String^  >(1) {L"login"};
			cli::array< System::String^ >^ __mcTemp__2 = gcnew cli::array< System::String^  >(1) {L"cid"};
			cli::array< System::String^ >^ __mcTemp__3 = gcnew cli::array< System::String^  >(1) {L"login"};
			cli::array< System::String^ >^ __mcTemp__4 = gcnew cli::array< System::String^  >(1) {L"customer"};
			cli::array< System::String^ >^ __mcTemp__5 = gcnew cli::array< System::String^  >(1) {L"code"};
			cli::array< System::String^ >^ __mcTemp__6 = gcnew cli::array< System::String^  >(1) {L"tarif_id"};
			cli::array< System::String^ >^ __mcTemp__7 = gcnew cli::array< System::String^  >(1) {L"code"};
			cli::array< System::String^ >^ __mcTemp__8 = gcnew cli::array< System::String^  >(1) {L"tax"};
			cli::array< System::String^ >^ __mcTemp__9 = gcnew cli::array< System::String^  >(1) {L"login"};
			cli::array< System::String^ >^ __mcTemp__10 = gcnew cli::array< System::String^  >(1) {L"customer"};
			cli::array< System::String^ >^ __mcTemp__11 = gcnew cli::array< System::String^  >(1) {L"trunk"};
			cli::array< System::String^ >^ __mcTemp__12 = gcnew cli::array< System::String^  >(1) {L"trunk"};
			this->dataSet1->Relations->AddRange(gcnew cli::array< System::Data::DataRelation^  >(6) {(gcnew System::Data::DataRelation(L"Relation2", 
				L"Customers", L"Phones", __mcTemp__1, __mcTemp__2, false)), (gcnew System::Data::DataRelation(L"Relation1", L"Customers", 
				L"Payments", __mcTemp__3, __mcTemp__4, false)), (gcnew System::Data::DataRelation(L"Relation5", L"Tarif", L"Customers", __mcTemp__5, 
				__mcTemp__6, false)), (gcnew System::Data::DataRelation(L"Relation4", L"Tax", L"Customers", __mcTemp__7, __mcTemp__8, false)), 
				(gcnew System::Data::DataRelation(L"Relation3", L"Customers", L"Trunks", __mcTemp__9, __mcTemp__10, false)), (gcnew System::Data::DataRelation(L"Relation6", 
				L"Trunks", L"Calls", __mcTemp__11, __mcTemp__12, false))});
			this->dataSet1->Tables->AddRange(gcnew cli::array< System::Data::DataTable^  >(7) {this->dataCustomers, this->dataPayments, 
				this->dataPhones, this->dataTarif, this->dataTax, this->dataTrunks, this->dataCalls});
			// 
			// dataCustomers
			// 
			this->dataCustomers->Columns->AddRange(gcnew cli::array< System::Data::DataColumn^  >(8) {this->dataId, this->dataLogin, 
				this->dataName, this->dataBalans, this->dataTaxId, this->dataBalans_date, this->dataСall_direction, this->dataTarif_id});
			cli::array< System::String^ >^ __mcTemp__13 = gcnew cli::array< System::String^  >(1) {L"login"};
			cli::array< System::String^ >^ __mcTemp__14 = gcnew cli::array< System::String^  >(1) {L"id"};
			cli::array< System::String^ >^ __mcTemp__15 = gcnew cli::array< System::String^  >(1) {L"code"};
			cli::array< System::String^ >^ __mcTemp__16 = gcnew cli::array< System::String^  >(1) {L"tax"};
			cli::array< System::String^ >^ __mcTemp__17 = gcnew cli::array< System::String^  >(1) {L"code"};
			cli::array< System::String^ >^ __mcTemp__18 = gcnew cli::array< System::String^  >(1) {L"tarif_id"};
			this->dataCustomers->Constraints->AddRange(gcnew cli::array< System::Data::Constraint^  >(4) {(gcnew System::Data::UniqueConstraint(L"Constraint1", 
				__mcTemp__13, false)), (gcnew System::Data::UniqueConstraint(L"Constraint2", __mcTemp__14, true)), (gcnew System::Data::ForeignKeyConstraint(L"Relation4", 
				L"Tax", __mcTemp__15, __mcTemp__16, System::Data::AcceptRejectRule::None, System::Data::Rule::Cascade, System::Data::Rule::Cascade)), 
				(gcnew System::Data::ForeignKeyConstraint(L"Relation5", L"Tarif", __mcTemp__17, __mcTemp__18, System::Data::AcceptRejectRule::None, 
				System::Data::Rule::Cascade, System::Data::Rule::Cascade))});
			this->dataCustomers->PrimaryKey = gcnew cli::array< System::Data::DataColumn^  >(1) {this->dataId};
			this->dataCustomers->TableName = L"Customers";
			// 
			// dataId
			// 
			this->dataId->AllowDBNull = false;
			this->dataId->AutoIncrement = true;
			this->dataId->Caption = L"id";
			this->dataId->ColumnName = L"id";
			this->dataId->DataType = System::Int32::typeid;
			// 
			// dataLogin
			// 
			this->dataLogin->AllowDBNull = false;
			this->dataLogin->Caption = L"Логин";
			this->dataLogin->ColumnName = L"login";
			// 
			// dataName
			// 
			this->dataName->Caption = L"ФИО";
			this->dataName->ColumnName = L"name";
			// 
			// dataBalans
			// 
			this->dataBalans->Caption = L"Баланс";
			this->dataBalans->ColumnName = L"balans";
			this->dataBalans->DataType = System::Double::typeid;
			// 
			// dataTaxId
			// 
			this->dataTaxId->Caption = L"Налог";
			this->dataTaxId->ColumnName = L"tax";
			this->dataTaxId->DataType = System::Int32::typeid;
			// 
			// dataBalans_date
			// 
			this->dataBalans_date->Caption = L"Дата проверки баланса";
			this->dataBalans_date->ColumnName = L"balans_date";
			this->dataBalans_date->DataType = System::DateTime::typeid;
			// 
			// dataСall_direction
			// 
			this->dataСall_direction->Caption = L"Направление вызова";
			this->dataСall_direction->ColumnName = L"call_direction";
			// 
			// dataTarif_id
			// 
			this->dataTarif_id->Caption = L"Тариф";
			this->dataTarif_id->ColumnName = L"tarif_id";
			this->dataTarif_id->DataType = System::Int32::typeid;
			// 
			// dataPayments
			// 
			this->dataPayments->Columns->AddRange(gcnew cli::array< System::Data::DataColumn^  >(4) {this->dataColumn1, this->dataColumn2, 
				this->dataColumn3, this->dataColumn4});
			cli::array< System::String^ >^ __mcTemp__19 = gcnew cli::array< System::String^  >(1) {L"id"};
			cli::array< System::String^ >^ __mcTemp__20 = gcnew cli::array< System::String^  >(1) {L"login"};
			cli::array< System::String^ >^ __mcTemp__21 = gcnew cli::array< System::String^  >(1) {L"customer"};
			this->dataPayments->Constraints->AddRange(gcnew cli::array< System::Data::Constraint^  >(2) {(gcnew System::Data::UniqueConstraint(L"Constraint1", 
				__mcTemp__19, true)), (gcnew System::Data::ForeignKeyConstraint(L"Relation1", L"Customers", __mcTemp__20, __mcTemp__21, System::Data::AcceptRejectRule::None, 
				System::Data::Rule::Cascade, System::Data::Rule::Cascade))});
			this->dataPayments->PrimaryKey = gcnew cli::array< System::Data::DataColumn^  >(1) {this->dataColumn1};
			this->dataPayments->TableName = L"Payments";
			// 
			// dataColumn1
			// 
			this->dataColumn1->AllowDBNull = false;
			this->dataColumn1->AutoIncrement = true;
			this->dataColumn1->ColumnName = L"id";
			this->dataColumn1->DataType = System::Int32::typeid;
			// 
			// dataColumn2
			// 
			this->dataColumn2->Caption = L"Сумма платежа";
			this->dataColumn2->ColumnName = L"the_sum";
			this->dataColumn2->DataType = System::Double::typeid;
			// 
			// dataColumn3
			// 
			this->dataColumn3->Caption = L"Пользователь";
			this->dataColumn3->ColumnName = L"customer";
			// 
			// dataColumn4
			// 
			this->dataColumn4->Caption = L"Дата приема платежа";
			this->dataColumn4->ColumnName = L"the_date";
			this->dataColumn4->DataType = System::DateTime::typeid;
			// 
			// dataPhones
			// 
			this->dataPhones->Columns->AddRange(gcnew cli::array< System::Data::DataColumn^  >(2) {this->dataColumn5, this->dataColumn6});
			cli::array< System::String^ >^ __mcTemp__22 = gcnew cli::array< System::String^  >(1) {L"phone"};
			cli::array< System::String^ >^ __mcTemp__23 = gcnew cli::array< System::String^  >(1) {L"login"};
			cli::array< System::String^ >^ __mcTemp__24 = gcnew cli::array< System::String^  >(1) {L"cid"};
			this->dataPhones->Constraints->AddRange(gcnew cli::array< System::Data::Constraint^  >(2) {(gcnew System::Data::UniqueConstraint(L"Constraint1", 
				__mcTemp__22, true)), (gcnew System::Data::ForeignKeyConstraint(L"Relation2", L"Customers", __mcTemp__23, __mcTemp__24, System::Data::AcceptRejectRule::None, 
				System::Data::Rule::Cascade, System::Data::Rule::Cascade))});
			this->dataPhones->PrimaryKey = gcnew cli::array< System::Data::DataColumn^  >(1) {this->dataColumn5};
			this->dataPhones->TableName = L"Phones";
			// 
			// dataColumn5
			// 
			this->dataColumn5->AllowDBNull = false;
			this->dataColumn5->Caption = L"Номер телефона";
			this->dataColumn5->ColumnName = L"phone";
			// 
			// dataColumn6
			// 
			this->dataColumn6->Caption = L"Пользователь";
			this->dataColumn6->ColumnName = L"cid";
			// 
			// dataTarif
			// 
			this->dataTarif->Columns->AddRange(gcnew cli::array< System::Data::DataColumn^  >(3) {this->dataColumn7, this->dataColumn8, 
				this->dataColumn9});
			cli::array< System::String^ >^ __mcTemp__25 = gcnew cli::array< System::String^  >(1) {L"id"};
			cli::array< System::String^ >^ __mcTemp__26 = gcnew cli::array< System::String^  >(1) {L"code"};
			this->dataTarif->Constraints->AddRange(gcnew cli::array< System::Data::Constraint^  >(2) {(gcnew System::Data::UniqueConstraint(L"Constraint1", 
				__mcTemp__25, true)), (gcnew System::Data::UniqueConstraint(L"Constraint2", __mcTemp__26, false))});
			this->dataTarif->PrimaryKey = gcnew cli::array< System::Data::DataColumn^  >(1) {this->dataColumn7};
			this->dataTarif->TableName = L"Tarif";
			// 
			// dataColumn7
			// 
			this->dataColumn7->AllowDBNull = false;
			this->dataColumn7->AutoIncrement = true;
			this->dataColumn7->ColumnName = L"id";
			this->dataColumn7->DataType = System::Int32::typeid;
			// 
			// dataColumn8
			// 
			this->dataColumn8->Caption = L"Код тарифа";
			this->dataColumn8->ColumnName = L"code";
			this->dataColumn8->DataType = System::Int32::typeid;
			// 
			// dataColumn9
			// 
			this->dataColumn9->Caption = L"Стоимость минуты";
			this->dataColumn9->ColumnName = L"cost";
			this->dataColumn9->DataType = System::Double::typeid;
			// 
			// dataTax
			// 
			this->dataTax->Columns->AddRange(gcnew cli::array< System::Data::DataColumn^  >(3) {this->dataColumn10, this->dataColumn11, 
				this->dataColumn12});
			cli::array< System::String^ >^ __mcTemp__27 = gcnew cli::array< System::String^  >(1) {L"id"};
			cli::array< System::String^ >^ __mcTemp__28 = gcnew cli::array< System::String^  >(1) {L"code"};
			this->dataTax->Constraints->AddRange(gcnew cli::array< System::Data::Constraint^  >(2) {(gcnew System::Data::UniqueConstraint(L"Constraint1", 
				__mcTemp__27, true)), (gcnew System::Data::UniqueConstraint(L"Constraint2", __mcTemp__28, false))});
			this->dataTax->PrimaryKey = gcnew cli::array< System::Data::DataColumn^  >(1) {this->dataColumn10};
			this->dataTax->TableName = L"Tax";
			// 
			// dataColumn10
			// 
			this->dataColumn10->AllowDBNull = false;
			this->dataColumn10->AutoIncrement = true;
			this->dataColumn10->ColumnName = L"id";
			this->dataColumn10->DataType = System::Int32::typeid;
			// 
			// dataColumn11
			// 
			this->dataColumn11->Caption = L"Код налога";
			this->dataColumn11->ColumnName = L"code";
			this->dataColumn11->DataType = System::Int32::typeid;
			// 
			// dataColumn12
			// 
			this->dataColumn12->Caption = L"Размер налога";
			this->dataColumn12->ColumnName = L"tax_value";
			this->dataColumn12->DataType = System::Double::typeid;
			// 
			// dataTrunks
			// 
			this->dataTrunks->Columns->AddRange(gcnew cli::array< System::Data::DataColumn^  >(2) {this->dataColumn13, this->dataColumn14});
			cli::array< System::String^ >^ __mcTemp__29 = gcnew cli::array< System::String^  >(1) {L"trunk"};
			cli::array< System::String^ >^ __mcTemp__30 = gcnew cli::array< System::String^  >(1) {L"login"};
			cli::array< System::String^ >^ __mcTemp__31 = gcnew cli::array< System::String^  >(1) {L"customer"};
			this->dataTrunks->Constraints->AddRange(gcnew cli::array< System::Data::Constraint^  >(2) {(gcnew System::Data::UniqueConstraint(L"Constraint1", 
				__mcTemp__29, true)), (gcnew System::Data::ForeignKeyConstraint(L"Relation3", L"Customers", __mcTemp__30, __mcTemp__31, System::Data::AcceptRejectRule::None, 
				System::Data::Rule::Cascade, System::Data::Rule::Cascade))});
			this->dataTrunks->PrimaryKey = gcnew cli::array< System::Data::DataColumn^  >(1) {this->dataColumn13};
			this->dataTrunks->TableName = L"Trunks";
			// 
			// dataColumn13
			// 
			this->dataColumn13->AllowDBNull = false;
			this->dataColumn13->Caption = L"Транк";
			this->dataColumn13->ColumnName = L"trunk";
			this->dataColumn13->DataType = System::Int32::typeid;
			// 
			// dataColumn14
			// 
			this->dataColumn14->AllowDBNull = false;
			this->dataColumn14->Caption = L"Пользователь";
			this->dataColumn14->ColumnName = L"customer";
			// 
			// dataCalls
			// 
			this->dataCalls->Columns->AddRange(gcnew cli::array< System::Data::DataColumn^  >(5) {this->dataColumn15, this->dataColumn16, 
				this->dataColumn17, this->dataColumn18, this->dataColumn19});
			cli::array< System::String^ >^ __mcTemp__32 = gcnew cli::array< System::String^  >(3) {L"the_number", L"the_date", L"trunk"};
			cli::array< System::String^ >^ __mcTemp__33 = gcnew cli::array< System::String^  >(1) {L"trunk"};
			cli::array< System::String^ >^ __mcTemp__34 = gcnew cli::array< System::String^  >(1) {L"trunk"};
			this->dataCalls->Constraints->AddRange(gcnew cli::array< System::Data::Constraint^  >(2) {(gcnew System::Data::UniqueConstraint(L"Constraint1", 
				__mcTemp__32, true)), (gcnew System::Data::ForeignKeyConstraint(L"Relation6", L"Trunks", __mcTemp__33, __mcTemp__34, System::Data::AcceptRejectRule::None, 
				System::Data::Rule::Cascade, System::Data::Rule::Cascade))});
			this->dataCalls->PrimaryKey = gcnew cli::array< System::Data::DataColumn^  >(3) {this->dataColumn15, this->dataColumn16, this->dataColumn17};
			this->dataCalls->TableName = L"Calls";
			// 
			// dataColumn15
			// 
			this->dataColumn15->AllowDBNull = false;
			this->dataColumn15->Caption = L"Номер нашего абонента";
			this->dataColumn15->ColumnName = L"the_number";
			// 
			// dataColumn16
			// 
			this->dataColumn16->AllowDBNull = false;
			this->dataColumn16->Caption = L"Дата звонка";
			this->dataColumn16->ColumnName = L"the_date";
			this->dataColumn16->DataType = System::DateTime::typeid;
			// 
			// dataColumn17
			// 
			this->dataColumn17->AllowDBNull = false;
			this->dataColumn17->Caption = L"Транк";
			this->dataColumn17->ColumnName = L"trunk";
			this->dataColumn17->DataType = System::Int32::typeid;
			// 
			// dataColumn18
			// 
			this->dataColumn18->Caption = L"Длительность разговора";
			this->dataColumn18->ColumnName = L"duration";
			this->dataColumn18->DataType = System::Double::typeid;
			// 
			// dataColumn19
			// 
			this->dataColumn19->Caption = L"Информация об аппоненте";
			this->dataColumn19->ColumnName = L"CallerID";
			// 
			// dataGridView7
			// 
			this->dataGridView7->AutoGenerateColumns = false;
			this->dataGridView7->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView7->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(2) {this->trunkDataGridViewTextBoxColumn1, 
				this->customerDataGridViewTextBoxColumn});
			this->dataGridView7->DataMember = L"Trunks";
			this->dataGridView7->DataSource = this->dataSet1;
			this->dataGridView7->Location = System::Drawing::Point(677, 400);
			this->dataGridView7->Name = L"dataGridView7";
			this->dataGridView7->Size = System::Drawing::Size(217, 150);
			this->dataGridView7->TabIndex = 35;
			// 
			// trunkDataGridViewTextBoxColumn1
			// 
			this->trunkDataGridViewTextBoxColumn1->DataPropertyName = L"trunk";
			this->trunkDataGridViewTextBoxColumn1->HeaderText = L"trunk";
			this->trunkDataGridViewTextBoxColumn1->Name = L"trunkDataGridViewTextBoxColumn1";
			this->trunkDataGridViewTextBoxColumn1->Width = 75;
			// 
			// customerDataGridViewTextBoxColumn
			// 
			this->customerDataGridViewTextBoxColumn->DataPropertyName = L"customer";
			this->customerDataGridViewTextBoxColumn->HeaderText = L"customer";
			this->customerDataGridViewTextBoxColumn->Name = L"customerDataGridViewTextBoxColumn";
			this->customerDataGridViewTextBoxColumn->Width = 75;
			// 
			// dataGridView5
			// 
			this->dataGridView5->AutoGenerateColumns = false;
			this->dataGridView5->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView5->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(5) {this->thenumberDataGridViewTextBoxColumn, 
				this->thedateDataGridViewTextBoxColumn1, this->trunkDataGridViewTextBoxColumn, this->durationDataGridViewTextBoxColumn, this->callerIDDataGridViewTextBoxColumn});
			this->dataGridView5->DataMember = L"Calls";
			this->dataGridView5->DataSource = this->dataSet1;
			this->dataGridView5->Location = System::Drawing::Point(240, 400);
			this->dataGridView5->Name = L"dataGridView5";
			this->dataGridView5->Size = System::Drawing::Size(423, 150);
			this->dataGridView5->TabIndex = 34;
			// 
			// thenumberDataGridViewTextBoxColumn
			// 
			this->thenumberDataGridViewTextBoxColumn->DataPropertyName = L"the_number";
			this->thenumberDataGridViewTextBoxColumn->HeaderText = L"the_number";
			this->thenumberDataGridViewTextBoxColumn->Name = L"thenumberDataGridViewTextBoxColumn";
			this->thenumberDataGridViewTextBoxColumn->Width = 75;
			// 
			// thedateDataGridViewTextBoxColumn1
			// 
			this->thedateDataGridViewTextBoxColumn1->DataPropertyName = L"the_date";
			this->thedateDataGridViewTextBoxColumn1->HeaderText = L"the_date";
			this->thedateDataGridViewTextBoxColumn1->Name = L"thedateDataGridViewTextBoxColumn1";
			this->thedateDataGridViewTextBoxColumn1->Width = 75;
			// 
			// trunkDataGridViewTextBoxColumn
			// 
			this->trunkDataGridViewTextBoxColumn->DataPropertyName = L"trunk";
			this->trunkDataGridViewTextBoxColumn->HeaderText = L"trunk";
			this->trunkDataGridViewTextBoxColumn->Name = L"trunkDataGridViewTextBoxColumn";
			this->trunkDataGridViewTextBoxColumn->Width = 50;
			// 
			// durationDataGridViewTextBoxColumn
			// 
			this->durationDataGridViewTextBoxColumn->DataPropertyName = L"duration";
			this->durationDataGridViewTextBoxColumn->HeaderText = L"duration";
			this->durationDataGridViewTextBoxColumn->Name = L"durationDataGridViewTextBoxColumn";
			this->durationDataGridViewTextBoxColumn->Width = 50;
			// 
			// callerIDDataGridViewTextBoxColumn
			// 
			this->callerIDDataGridViewTextBoxColumn->DataPropertyName = L"CallerID";
			this->callerIDDataGridViewTextBoxColumn->HeaderText = L"CallerID";
			this->callerIDDataGridViewTextBoxColumn->Name = L"callerIDDataGridViewTextBoxColumn";
			// 
			// dataGridView6
			// 
			this->dataGridView6->AutoGenerateColumns = false;
			this->dataGridView6->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView6->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(2) {this->phoneDataGridViewTextBoxColumn, 
				this->cidDataGridViewTextBoxColumn});
			this->dataGridView6->DataMember = L"Phones";
			this->dataGridView6->DataSource = this->dataSet1;
			this->dataGridView6->Location = System::Drawing::Point(677, 222);
			this->dataGridView6->Name = L"dataGridView6";
			this->dataGridView6->Size = System::Drawing::Size(217, 150);
			this->dataGridView6->TabIndex = 33;
			// 
			// phoneDataGridViewTextBoxColumn
			// 
			this->phoneDataGridViewTextBoxColumn->DataPropertyName = L"phone";
			this->phoneDataGridViewTextBoxColumn->HeaderText = L"phone";
			this->phoneDataGridViewTextBoxColumn->Name = L"phoneDataGridViewTextBoxColumn";
			this->phoneDataGridViewTextBoxColumn->Width = 75;
			// 
			// cidDataGridViewTextBoxColumn
			// 
			this->cidDataGridViewTextBoxColumn->DataPropertyName = L"cid";
			this->cidDataGridViewTextBoxColumn->HeaderText = L"cid";
			this->cidDataGridViewTextBoxColumn->Name = L"cidDataGridViewTextBoxColumn";
			this->cidDataGridViewTextBoxColumn->Width = 75;
			// 
			// dataGridView4
			// 
			this->dataGridView4->AutoGenerateColumns = false;
			this->dataGridView4->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView4->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(4) {this->idDataGridViewTextBoxColumn2, 
				this->thesumDataGridViewTextBoxColumn, this->customerDataGridViewTextBoxColumn1, this->thedateDataGridViewTextBoxColumn});
			this->dataGridView4->DataMember = L"Payments";
			this->dataGridView4->DataSource = this->dataSet1;
			this->dataGridView4->Location = System::Drawing::Point(238, 222);
			this->dataGridView4->Name = L"dataGridView4";
			this->dataGridView4->Size = System::Drawing::Size(423, 150);
			this->dataGridView4->TabIndex = 32;
			// 
			// idDataGridViewTextBoxColumn2
			// 
			this->idDataGridViewTextBoxColumn2->DataPropertyName = L"id";
			this->idDataGridViewTextBoxColumn2->HeaderText = L"id";
			this->idDataGridViewTextBoxColumn2->Name = L"idDataGridViewTextBoxColumn2";
			this->idDataGridViewTextBoxColumn2->Width = 30;
			// 
			// thesumDataGridViewTextBoxColumn
			// 
			this->thesumDataGridViewTextBoxColumn->DataPropertyName = L"the_sum";
			this->thesumDataGridViewTextBoxColumn->HeaderText = L"the_sum";
			this->thesumDataGridViewTextBoxColumn->Name = L"thesumDataGridViewTextBoxColumn";
			this->thesumDataGridViewTextBoxColumn->Width = 110;
			// 
			// customerDataGridViewTextBoxColumn1
			// 
			this->customerDataGridViewTextBoxColumn1->DataPropertyName = L"customer";
			this->customerDataGridViewTextBoxColumn1->HeaderText = L"customer";
			this->customerDataGridViewTextBoxColumn1->Name = L"customerDataGridViewTextBoxColumn1";
			// 
			// thedateDataGridViewTextBoxColumn
			// 
			this->thedateDataGridViewTextBoxColumn->DataPropertyName = L"the_date";
			this->thedateDataGridViewTextBoxColumn->HeaderText = L"the_date";
			this->thedateDataGridViewTextBoxColumn->Name = L"thedateDataGridViewTextBoxColumn";
			this->thedateDataGridViewTextBoxColumn->Width = 110;
			// 
			// dataGridView3
			// 
			this->dataGridView3->AutoGenerateColumns = false;
			this->dataGridView3->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView3->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {this->idDataGridViewTextBoxColumn, 
				this->codeDataGridViewTextBoxColumn, this->costDataGridViewTextBoxColumn});
			this->dataGridView3->DataMember = L"Tarif";
			this->dataGridView3->DataSource = this->dataSet1;
			this->dataGridView3->Location = System::Drawing::Point(3, 400);
			this->dataGridView3->Name = L"dataGridView3";
			this->dataGridView3->Size = System::Drawing::Size(221, 150);
			this->dataGridView3->TabIndex = 31;
			// 
			// idDataGridViewTextBoxColumn
			// 
			this->idDataGridViewTextBoxColumn->DataPropertyName = L"id";
			this->idDataGridViewTextBoxColumn->HeaderText = L"id";
			this->idDataGridViewTextBoxColumn->Name = L"idDataGridViewTextBoxColumn";
			this->idDataGridViewTextBoxColumn->Width = 30;
			// 
			// codeDataGridViewTextBoxColumn
			// 
			this->codeDataGridViewTextBoxColumn->DataPropertyName = L"code";
			this->codeDataGridViewTextBoxColumn->HeaderText = L"code";
			this->codeDataGridViewTextBoxColumn->Name = L"codeDataGridViewTextBoxColumn";
			this->codeDataGridViewTextBoxColumn->Width = 50;
			// 
			// costDataGridViewTextBoxColumn
			// 
			this->costDataGridViewTextBoxColumn->DataPropertyName = L"cost";
			this->costDataGridViewTextBoxColumn->HeaderText = L"cost";
			this->costDataGridViewTextBoxColumn->Name = L"costDataGridViewTextBoxColumn";
			this->costDataGridViewTextBoxColumn->Width = 75;
			// 
			// dataGridView2
			// 
			this->dataGridView2->AutoGenerateColumns = false;
			this->dataGridView2->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView2->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(3) {this->idDataGridViewTextBoxColumn1, 
				this->codeDataGridViewTextBoxColumn1, this->taxvalueDataGridViewTextBoxColumn});
			this->dataGridView2->DataMember = L"Tax";
			this->dataGridView2->DataSource = this->dataSet1;
			this->dataGridView2->Location = System::Drawing::Point(3, 222);
			this->dataGridView2->Name = L"dataGridView2";
			this->dataGridView2->Size = System::Drawing::Size(221, 150);
			this->dataGridView2->TabIndex = 30;
			// 
			// idDataGridViewTextBoxColumn1
			// 
			this->idDataGridViewTextBoxColumn1->DataPropertyName = L"id";
			this->idDataGridViewTextBoxColumn1->HeaderText = L"id";
			this->idDataGridViewTextBoxColumn1->Name = L"idDataGridViewTextBoxColumn1";
			this->idDataGridViewTextBoxColumn1->Width = 30;
			// 
			// codeDataGridViewTextBoxColumn1
			// 
			this->codeDataGridViewTextBoxColumn1->DataPropertyName = L"code";
			this->codeDataGridViewTextBoxColumn1->HeaderText = L"code";
			this->codeDataGridViewTextBoxColumn1->Name = L"codeDataGridViewTextBoxColumn1";
			this->codeDataGridViewTextBoxColumn1->Width = 50;
			// 
			// taxvalueDataGridViewTextBoxColumn
			// 
			this->taxvalueDataGridViewTextBoxColumn->DataPropertyName = L"tax_value";
			this->taxvalueDataGridViewTextBoxColumn->HeaderText = L"tax_value";
			this->taxvalueDataGridViewTextBoxColumn->Name = L"taxvalueDataGridViewTextBoxColumn";
			this->taxvalueDataGridViewTextBoxColumn->Width = 75;
			// 
			// dataGridView1
			// 
			this->dataGridView1->AutoGenerateColumns = false;
			this->dataGridView1->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dataGridView1->Columns->AddRange(gcnew cli::array< System::Windows::Forms::DataGridViewColumn^  >(8) {this->idDataGridViewTextBoxColumn3, 
				this->loginDataGridViewTextBoxColumn, this->nameDataGridViewTextBoxColumn, this->balansDataGridViewTextBoxColumn, this->taxDataGridViewTextBoxColumn, 
				this->balansdateDataGridViewTextBoxColumn, this->calldirectionDataGridViewTextBoxColumn, this->tarifidDataGridViewTextBoxColumn});
			this->dataGridView1->DataMember = L"Customers";
			this->dataGridView1->DataSource = this->dataSet1;
			this->dataGridView1->Location = System::Drawing::Point(1, 18);
			this->dataGridView1->Name = L"dataGridView1";
			this->dataGridView1->Size = System::Drawing::Size(891, 184);
			this->dataGridView1->TabIndex = 29;
			// 
			// idDataGridViewTextBoxColumn3
			// 
			this->idDataGridViewTextBoxColumn3->DataPropertyName = L"id";
			this->idDataGridViewTextBoxColumn3->HeaderText = L"id";
			this->idDataGridViewTextBoxColumn3->Name = L"idDataGridViewTextBoxColumn3";
			this->idDataGridViewTextBoxColumn3->Width = 30;
			// 
			// loginDataGridViewTextBoxColumn
			// 
			this->loginDataGridViewTextBoxColumn->DataPropertyName = L"login";
			this->loginDataGridViewTextBoxColumn->HeaderText = L"login";
			this->loginDataGridViewTextBoxColumn->Name = L"loginDataGridViewTextBoxColumn";
			this->loginDataGridViewTextBoxColumn->Width = 150;
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this->nameDataGridViewTextBoxColumn->DataPropertyName = L"name";
			this->nameDataGridViewTextBoxColumn->HeaderText = L"name";
			this->nameDataGridViewTextBoxColumn->Name = L"nameDataGridViewTextBoxColumn";
			this->nameDataGridViewTextBoxColumn->Width = 250;
			// 
			// balansDataGridViewTextBoxColumn
			// 
			this->balansDataGridViewTextBoxColumn->DataPropertyName = L"balans";
			this->balansDataGridViewTextBoxColumn->HeaderText = L"balans";
			this->balansDataGridViewTextBoxColumn->Name = L"balansDataGridViewTextBoxColumn";
			this->balansDataGridViewTextBoxColumn->Width = 50;
			// 
			// taxDataGridViewTextBoxColumn
			// 
			this->taxDataGridViewTextBoxColumn->DataPropertyName = L"tax";
			this->taxDataGridViewTextBoxColumn->HeaderText = L"tax";
			this->taxDataGridViewTextBoxColumn->Name = L"taxDataGridViewTextBoxColumn";
			this->taxDataGridViewTextBoxColumn->Width = 50;
			// 
			// balansdateDataGridViewTextBoxColumn
			// 
			this->balansdateDataGridViewTextBoxColumn->DataPropertyName = L"balans_date";
			this->balansdateDataGridViewTextBoxColumn->HeaderText = L"balans_date";
			this->balansdateDataGridViewTextBoxColumn->Name = L"balansdateDataGridViewTextBoxColumn";
			this->balansdateDataGridViewTextBoxColumn->Width = 70;
			// 
			// calldirectionDataGridViewTextBoxColumn
			// 
			this->calldirectionDataGridViewTextBoxColumn->DataPropertyName = L"call_direction";
			this->calldirectionDataGridViewTextBoxColumn->HeaderText = L"call_direction";
			this->calldirectionDataGridViewTextBoxColumn->Name = L"calldirectionDataGridViewTextBoxColumn";
			// 
			// tarifidDataGridViewTextBoxColumn
			// 
			this->tarifidDataGridViewTextBoxColumn->DataPropertyName = L"tarif_id";
			this->tarifidDataGridViewTextBoxColumn->HeaderText = L"tarif_id";
			this->tarifidDataGridViewTextBoxColumn->Name = L"tarifidDataGridViewTextBoxColumn";
			this->tarifidDataGridViewTextBoxColumn->Width = 50;
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(546, 576);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(19, 13);
			this->label9->TabIndex = 45;
			this->label9->Text = L"до";
			// 
			// label10
			// 
			this->label10->AutoSize = true;
			this->label10->Location = System::Drawing::Point(388, 576);
			this->label10->Name = L"label10";
			this->label10->Size = System::Drawing::Size(57, 13);
			this->label10->TabIndex = 44;
			this->label10->Text = L"начиная с";
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(571, 572);
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(92, 20);
			this->textBox2->TabIndex = 43;
			this->textBox2->Text = L"01.01.2007";
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(450, 572);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(92, 20);
			this->textBox1->TabIndex = 42;
			this->textBox1->Text = L"01.01.2006";
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(900, 610);
			this->Controls->Add(this->label9);
			this->Controls->Add(this->label10);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->dataGridView7);
			this->Controls->Add(this->dataGridView5);
			this->Controls->Add(this->dataGridView6);
			this->Controls->Add(this->dataGridView4);
			this->Controls->Add(this->dataGridView3);
			this->Controls->Add(this->dataGridView2);
			this->Controls->Add(this->dataGridView1);
			this->Controls->Add(this->label8);
			this->Controls->Add(this->label7);
			this->Controls->Add(this->label6);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->button3);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->button1);
			this->FormBorderStyle = System::Windows::Forms::FormBorderStyle::FixedDialog;
			this->Name = L"Form1";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"Тестовое задание";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataSet1))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataCustomers))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataPayments))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataPhones))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataTarif))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataTax))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataTrunks))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataCalls))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView7))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView5))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView6))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView4))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView3))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView2))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->dataGridView1))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

		

		// Выполнение подсчета баланса
		private: System::Void button1_Click(System::Object^  sender, System::EventArgs^  e) 
		{
			DataGridViewRowCollection ^drc = dataGridView1->Rows;
				
      DataGridViewRow ^dr =	drc[dataGridView1->CurrentCellAddress.Y];

      // Получение общей суммы платежей абонента
      // Получение значения логина для выбранной строки в Customers
			String ^login = dr->Cells[1]->Value->ToString();
      // Получение представления таблицы Payments
      DataView ^payments = gcnew DataView(dataSet1->Tables["Payments"]);
      // Установка фильтра для отбора тех значений, которые относятся к нашему абоненту
      payments->RowFilter = "customer = '" + login + "'";
      // Посчет суммы платежей нашего абонента
      double sum = 0;
			for (int i = 0; i < payments->Count; i++)
				sum += Convert::ToDouble(payments[i]->Row["the_sum"]->ToString());
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select sum(the_summ)
       * from Payments
       * where :login = customer
       * Результат запишем в переменную sum
      */

      // Получение суммы длительностей разговоров
      // Получение представления таблицы Phones
      DataView ^phones = gcnew DataView(dataSet1->Tables["Phones"]);
      // Получение номера телефона, который зарегистрирован за нашим абонентом
      phones->RowFilter = "cid = '" + login + "'";
      // Если его нет - то ошибка
      if (phones->Count == 0)
      {
				MessageBox::Show("Ошибка. У абонента "+login+" не зарегистрирован телефон.");
        return;
      }
      String ^phone = phones[0]->Row["phone"]->ToString();
      // Получение представления таблицы Calls
      DataView ^calls = gcnew DataView(dataSet1->Tables["Calls"]);
      // Оставление звонков с найденным номером
      calls->RowFilter = "the_number = '" + phone + "'";
      // Подсчет общего количества проговоренных минут
      double sumDuration = 0;
			for (int i = 0; i < calls->Count; i++)
				sumDuration += Convert::ToDouble(calls[i]->Row["duration"]->ToString());
      
//			foreach (DataRowView dRow in calls)
  //      sumDuration += Convert.ToDouble(dRow["duration"].ToString());
      // Если наш абонент владеет транком, то сумма будет нулем, в этом случае 
      // ищем сумму разговоров через таблицу Trunks
      if (sumDuration == 0)
      {
        // Получение представления таблицы Trunks
        DataView ^trunks = gcnew DataView(dataSet1->Tables["Trunks"]);
        // Получение номера телефона, который зарегистрирован за нашим абонентом
        trunks->RowFilter = "customer = '" + login + "'";
        // Если у нашего абонента нет транка, то sumDuration не пересчитывается
        // (возможно он еще вообще не звонил)
        if (trunks->Count == 0)
          goto ex;
				int trunk = Convert::ToInt32(trunks[0]->Row["trunk"]->ToString());                
        // Оставление звонков с найденным номером
        calls->RowFilter = "trunk = " + trunk.ToString();
        // Подсчет общего количества проговоренных минут
        sumDuration = 0;
				for (int i = 0; i < calls->Count; i++)
					sumDuration += Convert::ToDouble(calls[i]->Row["duration"]->ToString());  
      }
ex:
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select sum(c.duration)
       * from Calls c, Phones ph
       * where :login = ph.cid && c.the_number = ph.phone
       * 
       * 
       * select sum(c.duration)
       * from Calls c, Trunks t
       * where :login = t.customer && t.trunk = c.trunk       
       * Результатом будет наибольшая из сумм
       * Запишем ее в переменную sumDuration
      */

      // Получение стоимости минуты разговора
      // Получение кода тарифа нашего абонента
			int code = Convert::ToInt32(dr->Cells[7]->Value->ToString());
      // Получение представления таблицы Tarif
      DataView ^tarifs = gcnew DataView(dataSet1->Tables["Tarif"]);
      //  Оставление записи с указанным кодом
      tarifs->RowFilter = "code = "+ code.ToString();
      // Получение стоимости минуты разговора
			double cost = Convert::ToDouble(tarifs[0]->Row["cost"]->ToString());
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select cost
       * from Tarif
       * where :tarif_id = code
       * Результат запишем в переменную cost
      */

      // Получение налога
      // Получение кода тарифа нашего абонента
			int tax = Convert::ToInt32(dr->Cells[4]->Value->ToString());
      // Получение представления таблицы Tarif
      DataView ^taxs = gcnew DataView(dataSet1->Tables["Tax"]);
      //  Оставление записи с указанным кодом
      taxs->RowFilter = "code = " + tax.ToString();
      // Получение стоимости минуты разговора
			double tax_value = Convert::ToDouble(taxs[0]->Row["tax_value"]->ToString());
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select tax_value
       * from Tax
       * where :tax = code
       * Результат запишем в переменную tax
      */

      // Установка нового баланса (из суммы внесенных средств вычитается налог)
      dr->Cells[3]->Value = sum - (sum * tax_value / 100) - sumDuration * cost;      
		}

		//public static Form1 ^f1 = this;

		// Показ списка звонков
		private: System::Void button2_Click(System::Object^  sender, System::EventArgs^  e) 
		{												
			CallList ^callList = gcnew CallList();
			
			try
      {
				Convert::ToDateTime(textBox1->Text);
				Convert::ToDateTime(textBox2->Text);
      }
      catch (FormatException ^)
      {
				MessageBox::Show("Ошибка. Неверно указаны даты.");
        return;
      }
      
			DataGridViewRowCollection ^drc = dataGridView1->Rows;
      DataGridViewRow ^dr = drc[dataGridView1->CurrentCellAddress.Y];

      // Получение значения логина для выбранной строки в Customers
      String ^login = dr->Cells[1]->Value->ToString();

      // Получение стоимости минуты разговора
      // Получение кода тарифа нашего абонента
      int code = Convert::ToInt32(dr->Cells[7]->Value->ToString());
      // Получение представления таблицы Tarif
      DataView ^tarifs = gcnew DataView(dataSet1->Tables["Tarif"]);
      //  Оставление записи с указанным кодом
      tarifs->RowFilter = "code = " + code.ToString();
      // Получение стоимости минуты разговора
      double cost = Convert::ToDouble(tarifs[0]->Row["cost"]->ToString());
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select cost
       * from Tarif
       * where :tarif_id = code
       * Результат запишем в переменную cost
      */

      // Получение представления таблицы Phones
      DataView ^phones = gcnew DataView(dataSet1->Tables["Phones"]);
      // Получение номера телефона, который зарегистрирован за нашим абонентом
      phones->RowFilter = "cid = '" + login + "'";
      // Если его нет - то ошибка
      if (phones->Count == 0)
      {
        MessageBox::Show("Ошибка. У абонента " + login + " не зарегистрирован телефон.");
        return;
      }
      String ^phone = phones[0]->Row["phone"]->ToString();
      // Получение представления таблицы Calls
      DataView ^calls = gcnew DataView(dataSet1->Tables["Calls"]);
      // Оставление звонков с найденным номером
      calls->RowFilter = "the_number = '" + phone + "'";
      if (calls->Count == 0)
      { 
       // Получение представления таблицы Trunks
        DataView ^trunks = gcnew DataView(dataSet1->Tables["Trunks"]);
        // Получение номера телефона, который зарегистрирован за нашим абонентом
        trunks->RowFilter = "customer = '" + login + "'";
        // Если у нашего абонента нет транка, то sumDuration не пересчитывается
        // (возможно он еще вообще не звонил)
        if (trunks->Count == 0)
          goto ex;
        int trunk = Convert::ToInt32(trunks[0]->Row["trunk"]->ToString());                
        // Оставление звонков с найденным номером
        calls->RowFilter = "trunk = " + trunk.ToString();        
      }
    ex:
      calls->RowFilter += " and the_date>='" + Convert::ToDateTime(textBox1->Text) + "' and the_date<='" + Convert::ToDateTime(textBox2->Text) + "'";
      double sumCalls = 0;
      for (int i =0; i< calls->Count; i++)
			{
        calls[i]->Row["duration"] = Convert::ToDouble(calls[i]->Row["duration"]->ToString()) * cost;
        sumCalls += Convert::ToDouble(calls[i]->Row["duration"]->ToString());			
			}			
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select count(c.*)
       * from Calls c, Phones ph
       * where ph.cid = :login && c.the_number = ph.phone
       * 
       * Если count(c.*) = 0, то
       * select c.the_date, (c.duration * cost) as price, c.CallerID
       * from Calls c, Trunks t
       * where c.trunk = t.trunk && t.customer = :login
       * 
       * в противном случае
       * 
       * select c.the_date, (c.duration * cost) as price, c.CallerID
       * from Calls c, Phones ph
       * where c.the_number = ph.phone && ph.cid = :login       
      */

      callList->dataGridView1->DataMember = "";
      callList->dataGridView1->DataSource = calls;
      callList->dataGridView1->Columns[0]->Visible = false;
      callList->dataGridView1->Columns[2]->Visible = false;
      callList->dataGridView1->Columns[3]->HeaderText = "cost";
      callList->textBox1->Text = sumCalls.ToString();
			
			callList->ShowDialog();			
      dataSet1->RejectChanges();			
		}


		// Сохранение изменений в БД
		private: System::Void button3_Click(System::Object^  sender, System::EventArgs^  e) 
		{
			dataSet1->AcceptChanges();
			dataSet1->WriteXml(Application::StartupPath+"\\TestBD.xml");
		}

		// Загрузка БД при запуске программы
		private: System::Void Form1_Load(System::Object^  sender, System::EventArgs^  e) 
		{
			try
      {
				dataSet1->ReadXml(Application::StartupPath+"\\TestBD.xml");
        dataSet1->AcceptChanges();
      }
			catch (FileNotFoundException ^)
      {
				MessageBox::Show("Ошибка при загрузке базы данных: " +Application::StartupPath +"\\TestBD.xml");
      }
		}
};
}

