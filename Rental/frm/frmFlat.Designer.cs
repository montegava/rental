namespace Rental
{
    partial class frmFlat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFlat));
            this.label1 = new System.Windows.Forms.Label();
            this.intupADDRESS = new System.Windows.Forms.TextBox();
            this.intupROOM_COUNT = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.intupFLOOR = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.intupBATH_UNIT = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.intupBUILD = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.intupFURNITURE = new System.Windows.Forms.ComboBox();
            this.intupSTATE = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.intupMECHANIC = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.inputNAME = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.intupPRICE = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.intupCOMMENT = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.inputCONTENT = new System.Windows.Forms.TextBox();
            this.inputLINK = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.inputPHONE = new System.Windows.Forms.ListBox();
            this.context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.contextDel = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lvImagList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnAddNewImage = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.inputREGION = new System.Windows.Forms.ComboBox();
            this.chCooler = new System.Windows.Forms.CheckBox();
            this.chWasher = new System.Windows.Forms.CheckBox();
            this.chTV = new System.Windows.Forms.CheckBox();
            this.chFridge = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.inputLESSOR = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.inputRENT_TO = new System.Windows.Forms.DateTimePicker();
            this.inputRENT_FROM = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.inputTERM = new System.Windows.Forms.ComboBox();
            this.inputEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.inputCategory = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.inputType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.context.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество комнат";
            // 
            // intupADDRESS
            // 
            this.intupADDRESS.Location = new System.Drawing.Point(93, 72);
            this.intupADDRESS.Multiline = true;
            this.intupADDRESS.Name = "intupADDRESS";
            this.intupADDRESS.Size = new System.Drawing.Size(304, 45);
            this.intupADDRESS.TabIndex = 1;
            // 
            // intupROOM_COUNT
            // 
            this.intupROOM_COUNT.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.intupROOM_COUNT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intupROOM_COUNT.FormattingEnabled = true;
            this.intupROOM_COUNT.Items.AddRange(new object[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.intupROOM_COUNT.Location = new System.Drawing.Point(461, 160);
            this.intupROOM_COUNT.Name = "intupROOM_COUNT";
            this.intupROOM_COUNT.Size = new System.Drawing.Size(88, 21);
            this.intupROOM_COUNT.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Адресс";
            // 
            // intupFLOOR
            // 
            this.intupFLOOR.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.intupFLOOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intupFLOOR.FormattingEnabled = true;
            this.intupFLOOR.Items.AddRange(new object[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25"});
            this.intupFLOOR.Location = new System.Drawing.Point(249, 160);
            this.intupFLOOR.Name = "intupFLOOR";
            this.intupFLOOR.Size = new System.Drawing.Size(88, 21);
            this.intupFLOOR.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Этаж";
            // 
            // intupBATH_UNIT
            // 
            this.intupBATH_UNIT.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.intupBATH_UNIT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intupBATH_UNIT.FormattingEnabled = true;
            this.intupBATH_UNIT.Items.AddRange(new object[] {
            "-- неизвестно --",
            "совместно",
            "раздельно"});
            this.intupBATH_UNIT.Location = new System.Drawing.Point(93, 196);
            this.intupBATH_UNIT.Name = "intupBATH_UNIT";
            this.intupBATH_UNIT.Size = new System.Drawing.Size(88, 21);
            this.intupBATH_UNIT.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Санузел";
            // 
            // intupBUILD
            // 
            this.intupBUILD.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.intupBUILD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intupBUILD.FormattingEnabled = true;
            this.intupBUILD.Items.AddRange(new object[] {
            "-- неизвестно --",
            "кирпичный",
            "панельный",
            "коттедж",
            "часть дома"});
            this.intupBUILD.Location = new System.Drawing.Point(93, 160);
            this.intupBUILD.Name = "intupBUILD";
            this.intupBUILD.Size = new System.Drawing.Size(88, 21);
            this.intupBUILD.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 163);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Вид дома";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 283);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Мебель";
            // 
            // intupFURNITURE
            // 
            this.intupFURNITURE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intupFURNITURE.FormattingEnabled = true;
            this.intupFURNITURE.Items.AddRange(new object[] {
            "-- неизвестно --",
            "есть",
            "нет",
            "частично"});
            this.intupFURNITURE.Location = new System.Drawing.Point(93, 280);
            this.intupFURNITURE.Name = "intupFURNITURE";
            this.intupFURNITURE.Size = new System.Drawing.Size(150, 21);
            this.intupFURNITURE.TabIndex = 11;
            // 
            // intupSTATE
            // 
            this.intupSTATE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intupSTATE.FormattingEnabled = true;
            this.intupSTATE.Items.AddRange(new object[] {
            "-- неизвестно --",
            "евроремонт",
            "новая",
            "хорошее",
            "плохое"});
            this.intupSTATE.Location = new System.Drawing.Point(249, 199);
            this.intupSTATE.Name = "intupSTATE";
            this.intupSTATE.Size = new System.Drawing.Size(88, 21);
            this.intupSTATE.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(187, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Состояние";
            // 
            // intupMECHANIC
            // 
            this.intupMECHANIC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.intupMECHANIC.FormattingEnabled = true;
            this.intupMECHANIC.Items.AddRange(new object[] {
            "-- неизвестно --",
            "отсутствует",
            "присутствует"});
            this.intupMECHANIC.Location = new System.Drawing.Point(93, 310);
            this.intupMECHANIC.Name = "intupMECHANIC";
            this.intupMECHANIC.Size = new System.Drawing.Size(88, 21);
            this.intupMECHANIC.TabIndex = 15;
            this.intupMECHANIC.SelectedIndexChanged += new System.EventHandler(this.intupMECHANIC_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Быт. техника";
            // 
            // inputNAME
            // 
            this.inputNAME.Location = new System.Drawing.Point(93, 19);
            this.inputNAME.Name = "inputNAME";
            this.inputNAME.Size = new System.Drawing.Size(456, 20);
            this.inputNAME.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "ФИО хозяина";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(44, 350);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Цена";
            // 
            // intupPRICE
            // 
            this.intupPRICE.Location = new System.Drawing.Point(93, 347);
            this.intupPRICE.Name = "intupPRICE";
            this.intupPRICE.Size = new System.Drawing.Size(171, 20);
            this.intupPRICE.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 378);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Примечания";
            // 
            // intupCOMMENT
            // 
            this.intupCOMMENT.Location = new System.Drawing.Point(93, 375);
            this.intupCOMMENT.Multiline = true;
            this.intupCOMMENT.Name = "intupCOMMENT";
            this.intupCOMMENT.Size = new System.Drawing.Size(456, 46);
            this.intupCOMMENT.TabIndex = 20;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(353, 694);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(475, 694);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 23;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.inputCONTENT);
            this.groupBox1.Controls.Add(this.inputLINK);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(561, 127);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подробно";
            // 
            // inputCONTENT
            // 
            this.inputCONTENT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputCONTENT.Location = new System.Drawing.Point(3, 36);
            this.inputCONTENT.Multiline = true;
            this.inputCONTENT.Name = "inputCONTENT";
            this.inputCONTENT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputCONTENT.Size = new System.Drawing.Size(387, 86);
            this.inputCONTENT.TabIndex = 0;
            // 
            // inputLINK
            // 
            this.inputLINK.AutoSize = true;
            this.inputLINK.Dock = System.Windows.Forms.DockStyle.Top;
            this.inputLINK.Location = new System.Drawing.Point(3, 16);
            this.inputLINK.Name = "inputLINK";
            this.inputLINK.Size = new System.Drawing.Size(0, 13);
            this.inputLINK.TabIndex = 2;
            this.inputLINK.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.inputLINK_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.inputPHONE);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(396, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(162, 108);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Контакты:";
            // 
            // inputPHONE
            // 
            this.inputPHONE.ContextMenuStrip = this.context;
            this.inputPHONE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPHONE.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputPHONE.FormattingEnabled = true;
            this.inputPHONE.ItemHeight = 20;
            this.inputPHONE.Location = new System.Drawing.Point(3, 16);
            this.inputPHONE.Name = "inputPHONE";
            this.inputPHONE.Size = new System.Drawing.Size(156, 89);
            this.inputPHONE.TabIndex = 3;
            // 
            // context
            // 
            this.context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextAdd,
            this.contextDel});
            this.context.Name = "context";
            this.context.Size = new System.Drawing.Size(127, 48);
            // 
            // contextAdd
            // 
            this.contextAdd.Image = ((System.Drawing.Image)(resources.GetObject("contextAdd.Image")));
            this.contextAdd.Name = "contextAdd";
            this.contextAdd.Size = new System.Drawing.Size(126, 22);
            this.contextAdd.Text = "Добавить";
            this.contextAdd.Click += new System.EventHandler(this.contextAdd_Click);
            // 
            // contextDel
            // 
            this.contextDel.Image = ((System.Drawing.Image)(resources.GetObject("contextDel.Image")));
            this.contextDel.Name = "contextDel";
            this.contextDel.Size = new System.Drawing.Size(126, 22);
            this.contextDel.Text = "Удалить";
            this.contextDel.Click += new System.EventHandler(this.contextDel_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblType);
            this.groupBox5.Controls.Add(this.inputType);
            this.groupBox5.Controls.Add(this.lblCategory);
            this.groupBox5.Controls.Add(this.inputCategory);
            this.groupBox5.Controls.Add(this.inputEmail);
            this.groupBox5.Controls.Add(this.lblEmail);
            this.groupBox5.Controls.Add(this.lvImagList);
            this.groupBox5.Controls.Add(this.pbImage);
            this.groupBox5.Controls.Add(this.btnAddNewImage);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.chCooler);
            this.groupBox5.Controls.Add(this.chWasher);
            this.groupBox5.Controls.Add(this.chTV);
            this.groupBox5.Controls.Add(this.chFridge);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.inputLESSOR);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.inputRENT_TO);
            this.groupBox5.Controls.Add(this.inputRENT_FROM);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.inputTERM);
            this.groupBox5.Controls.Add(this.inputNAME);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.intupADDRESS);
            this.groupBox5.Controls.Add(this.intupROOM_COUNT);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.intupCOMMENT);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.intupFLOOR);
            this.groupBox5.Controls.Add(this.intupPRICE);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.intupBATH_UNIT);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.intupMECHANIC);
            this.groupBox5.Controls.Add(this.intupBUILD);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.intupSTATE);
            this.groupBox5.Controls.Add(this.intupFURNITURE);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 127);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(561, 561);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Информация";
            // 
            // lvImagList
            // 
            this.lvImagList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvImagList.GridLines = true;
            this.lvImagList.Location = new System.Drawing.Point(93, 456);
            this.lvImagList.Name = "lvImagList";
            this.lvImagList.Size = new System.Drawing.Size(292, 97);
            this.lvImagList.TabIndex = 40;
            this.lvImagList.UseCompatibleStateImageBehavior = false;
            this.lvImagList.View = System.Windows.Forms.View.Details;
            this.lvImagList.SelectedIndexChanged += new System.EventHandler(this.lvImagList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Image";
            this.columnHeader1.Width = 300;
            // 
            // pbImage
            // 
            this.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbImage.Location = new System.Drawing.Point(391, 427);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(158, 124);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 39;
            this.pbImage.TabStop = false;
            this.pbImage.DoubleClick += new System.EventHandler(this.pbImage_DoubleClick);
            // 
            // btnAddNewImage
            // 
            this.btnAddNewImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewImage.Location = new System.Drawing.Point(93, 427);
            this.btnAddNewImage.Name = "btnAddNewImage";
            this.btnAddNewImage.Size = new System.Drawing.Size(144, 23);
            this.btnAddNewImage.TabIndex = 37;
            this.btnAddNewImage.Text = "Прикрепить фото >>>";
            this.btnAddNewImage.UseVisualStyleBackColor = true;
            this.btnAddNewImage.Click += new System.EventHandler(this.btnAddNewImage_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.inputREGION);
            this.groupBox3.Location = new System.Drawing.Point(403, 67);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(146, 49);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Район";
            // 
            // inputREGION
            // 
            this.inputREGION.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.inputREGION.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inputREGION.FormattingEnabled = true;
            this.inputREGION.Items.AddRange(new object[] {
            "-- неизвестно --",
            "Коминтерновский",
            "Центральный",
            "Ж/Д",
            "Левобережный",
            "Советский",
            "Ленинский"});
            this.inputREGION.Location = new System.Drawing.Point(6, 16);
            this.inputREGION.Name = "inputREGION";
            this.inputREGION.Size = new System.Drawing.Size(134, 21);
            this.inputREGION.TabIndex = 35;
            // 
            // chCooler
            // 
            this.chCooler.AutoSize = true;
            this.chCooler.Enabled = false;
            this.chCooler.Location = new System.Drawing.Point(458, 312);
            this.chCooler.Name = "chCooler";
            this.chCooler.Size = new System.Drawing.Size(96, 17);
            this.chCooler.TabIndex = 34;
            this.chCooler.Text = "Кондиционер ";
            this.chCooler.UseVisualStyleBackColor = true;
            // 
            // chWasher
            // 
            this.chWasher.AutoSize = true;
            this.chWasher.Enabled = false;
            this.chWasher.Location = new System.Drawing.Point(325, 312);
            this.chWasher.Name = "chWasher";
            this.chWasher.Size = new System.Drawing.Size(129, 17);
            this.chWasher.TabIndex = 33;
            this.chWasher.Text = "Cтиральная машина";
            this.chWasher.UseVisualStyleBackColor = true;
            // 
            // chTV
            // 
            this.chTV.AutoSize = true;
            this.chTV.Enabled = false;
            this.chTV.Location = new System.Drawing.Point(282, 312);
            this.chTV.Name = "chTV";
            this.chTV.Size = new System.Drawing.Size(40, 17);
            this.chTV.TabIndex = 32;
            this.chTV.Text = "TV";
            this.chTV.UseVisualStyleBackColor = true;
            // 
            // chFridge
            // 
            this.chFridge.AutoSize = true;
            this.chFridge.Enabled = false;
            this.chFridge.Location = new System.Drawing.Point(191, 312);
            this.chFridge.Name = "chFridge";
            this.chFridge.Size = new System.Drawing.Size(93, 17);
            this.chFridge.TabIndex = 31;
            this.chFridge.Text = "Холодильник";
            this.chFridge.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(376, 202);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 13);
            this.label16.TabIndex = 29;
            this.label16.Text = "Кто сдал";
            // 
            // inputLESSOR
            // 
            this.inputLESSOR.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.inputLESSOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inputLESSOR.FormattingEnabled = true;
            this.inputLESSOR.Items.AddRange(new object[] {
            "-- неизвестно --",
            "Хозяин",
            "Мы",
            "Агенство"});
            this.inputLESSOR.Location = new System.Drawing.Point(461, 199);
            this.inputLESSOR.Name = "inputLESSOR";
            this.inputLESSOR.Size = new System.Drawing.Size(88, 21);
            this.inputLESSOR.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(258, 242);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(18, 13);
            this.label15.TabIndex = 28;
            this.label15.Text = "от";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(408, 239);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 27;
            this.label14.Text = "до";
            // 
            // inputRENT_TO
            // 
            this.inputRENT_TO.Location = new System.Drawing.Point(428, 236);
            this.inputRENT_TO.Name = "inputRENT_TO";
            this.inputRENT_TO.Size = new System.Drawing.Size(121, 20);
            this.inputRENT_TO.TabIndex = 25;
            // 
            // inputRENT_FROM
            // 
            this.inputRENT_FROM.Location = new System.Drawing.Point(278, 239);
            this.inputRENT_FROM.Name = "inputRENT_FROM";
            this.inputRENT_FROM.Size = new System.Drawing.Size(119, 20);
            this.inputRENT_FROM.TabIndex = 24;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 242);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Срок сдачи";
            // 
            // inputTERM
            // 
            this.inputTERM.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.inputTERM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inputTERM.FormattingEnabled = true;
            this.inputTERM.Items.AddRange(new object[] {
            "на длительный срок",
            "на сутки"});
            this.inputTERM.Location = new System.Drawing.Point(93, 238);
            this.inputTERM.Name = "inputTERM";
            this.inputTERM.Size = new System.Drawing.Size(150, 21);
            this.inputTERM.TabIndex = 23;
            // 
            // inputEmail
            // 
            this.inputEmail.Location = new System.Drawing.Point(93, 45);
            this.inputEmail.Name = "inputEmail";
            this.inputEmail.Size = new System.Drawing.Size(226, 20);
            this.inputEmail.TabIndex = 44;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(55, 48);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(32, 13);
            this.lblEmail.TabIndex = 43;
            this.lblEmail.Text = "Email";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(19, 126);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(60, 13);
            this.lblCategory.TabIndex = 45;
            this.lblCategory.Text = "Категория";
            // 
            // inputCategory
            // 
            this.inputCategory.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.inputCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inputCategory.FormattingEnabled = true;
            this.inputCategory.Items.AddRange(new object[] {
            "-- неизвестно --",
            "Квартира",
            "Комната",
            "Дом",
            "Помещение, офис"});
            this.inputCategory.Location = new System.Drawing.Point(93, 123);
            this.inputCategory.Name = "inputCategory";
            this.inputCategory.Size = new System.Drawing.Size(138, 21);
            this.inputCategory.TabIndex = 46;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(246, 126);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(26, 13);
            this.lblType.TabIndex = 47;
            this.lblType.Text = "Тип";
            // 
            // inputType
            // 
            this.inputType.AutoCompleteCustomSource.AddRange(new string[] {
            "-- неизвестно --",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            ">9"});
            this.inputType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inputType.FormattingEnabled = true;
            this.inputType.Items.AddRange(new object[] {
            "-- неизвестно --",
            "Сдам",
            "Сниму"});
            this.inputType.Location = new System.Drawing.Point(278, 123);
            this.inputType.Name = "inputType";
            this.inputType.Size = new System.Drawing.Size(138, 21);
            this.inputType.TabIndex = 48;
            // 
            // frmFlat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 724);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmFlat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить в избранное";
            this.Load += new System.EventHandler(this.frmFlat_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmFlat_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.context.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox intupADDRESS;
        public System.Windows.Forms.ComboBox intupROOM_COUNT;
        public System.Windows.Forms.ComboBox intupFLOOR;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox intupBATH_UNIT;
        public System.Windows.Forms.ComboBox intupBUILD;
        public System.Windows.Forms.ComboBox intupFURNITURE;
        public System.Windows.Forms.ComboBox intupSTATE;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox intupMECHANIC;
        public System.Windows.Forms.TextBox inputNAME;
        public System.Windows.Forms.TextBox intupPRICE;
        public System.Windows.Forms.TextBox intupCOMMENT;
        public System.Windows.Forms.TextBox inputCONTENT;
        public System.Windows.Forms.LinkLabel inputLINK;
        public System.Windows.Forms.ListBox inputPHONE;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.ComboBox inputTERM;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.ComboBox inputLESSOR;
        public System.Windows.Forms.DateTimePicker inputRENT_TO;
        public System.Windows.Forms.DateTimePicker inputRENT_FROM;
        private System.Windows.Forms.ContextMenuStrip context;
        private System.Windows.Forms.ToolStripMenuItem contextAdd;
        private System.Windows.Forms.ToolStripMenuItem contextDel;
        public System.Windows.Forms.CheckBox chCooler;
        public System.Windows.Forms.CheckBox chWasher;
        public System.Windows.Forms.CheckBox chTV;
        public System.Windows.Forms.CheckBox chFridge;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ComboBox inputREGION;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnAddNewImage;
        private System.Windows.Forms.ListView lvImagList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        public System.Windows.Forms.Label lblCategory;
        public System.Windows.Forms.ComboBox inputCategory;
        public System.Windows.Forms.TextBox inputEmail;
        public System.Windows.Forms.Label lblEmail;
        public System.Windows.Forms.Label lblType;
        public System.Windows.Forms.ComboBox inputType;
    }
}