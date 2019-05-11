namespace Pdf417EncoderDemo
{
	partial class SaveImage
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
			this.SaveButton = new System.Windows.Forms.Button();
			this.SaveImageButton = new System.Windows.Forms.Button();
			this.ImageFormatComboBox = new System.Windows.Forms.ComboBox();
			this.Label8 = new System.Windows.Forms.Label();
			this.ImageWidthLabel = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.HeaderLabel = new System.Windows.Forms.Label();
			this.ModulesWidthLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.QuietZoneLabel = new System.Windows.Forms.Label();
			this.ModuleWidthLabel = new System.Windows.Forms.Label();
			this.ModuleHeightLabel = new System.Windows.Forms.Label();
			this.ModulesHeightLabel = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.ImageHeightLabel = new System.Windows.Forms.Label();
			this.Label25 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.ImageRotationTextBox = new System.Windows.Forms.TextBox();
			this.ImageBGWidthLabel = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.ImageBGHeightLabel = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.BarcodePosYTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.ImageBrowseButton = new System.Windows.Forms.Button();
			this.BarcodePosXTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ImageFileNameTextBox = new System.Windows.Forms.TextBox();
			this.HatchStyleComboBox = new System.Windows.Forms.ComboBox();
			this.BrushHeightTextBox = new System.Windows.Forms.TextBox();
			this.BrushWidthTextBox = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.BrushColorButton = new System.Windows.Forms.Button();
			this.CameraDistanceTextBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.CameraViewRotationTextBox = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.ImageRadioButton = new System.Windows.Forms.RadioButton();
			this.BrushRadioButton = new System.Windows.Forms.RadioButton();
			this.NoneRadioButton = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ErrorNumberTextBox = new System.Windows.Forms.TextBox();
			this.ErrorDiameterTextBox = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.ErrorAlternateRadioButton = new System.Windows.Forms.RadioButton();
			this.ErrorBlackRadioButton = new System.Windows.Forms.RadioButton();
			this.ErrorWhiteRadioButton = new System.Windows.Forms.RadioButton();
			this.ErrorNoneRadioButton = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// SaveButton
			// 
			this.SaveButton.Location = new System.Drawing.Point(179, 517);
			this.SaveButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(100, 38);
			this.SaveButton.TabIndex = 3;
			this.SaveButton.Text = "Save";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.OnSaveClick);
			// 
			// SaveImageButton
			// 
			this.SaveImageButton.Location = new System.Drawing.Point(289, 517);
			this.SaveImageButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.SaveImageButton.Name = "SaveImageButton";
			this.SaveImageButton.Size = new System.Drawing.Size(100, 38);
			this.SaveImageButton.TabIndex = 4;
			this.SaveImageButton.Text = "Exit";
			this.SaveImageButton.UseVisualStyleBackColor = true;
			this.SaveImageButton.Click += new System.EventHandler(this.OnCancelClick);
			// 
			// ImageFormatComboBox
			// 
			this.ImageFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ImageFormatComboBox.FormattingEnabled = true;
			this.ImageFormatComboBox.Location = new System.Drawing.Point(137, 272);
			this.ImageFormatComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ImageFormatComboBox.Name = "ImageFormatComboBox";
			this.ImageFormatComboBox.Size = new System.Drawing.Size(94, 24);
			this.ImageFormatComboBox.TabIndex = 15;
			this.ImageFormatComboBox.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.OnImageFileFormat);
			// 
			// Label8
			// 
			this.Label8.AutoSize = true;
			this.Label8.Location = new System.Drawing.Point(16, 275);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(103, 16);
			this.Label8.TabIndex = 14;
			this.Label8.Text = "Image file format";
			// 
			// ImageWidthLabel
			// 
			this.ImageWidthLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ImageWidthLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ImageWidthLabel.Location = new System.Drawing.Point(175, 202);
			this.ImageWidthLabel.Name = "ImageWidthLabel";
			this.ImageWidthLabel.Size = new System.Drawing.Size(56, 22);
			this.ImageWidthLabel.TabIndex = 11;
			this.ImageWidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label1
			// 
			this.Label1.AutoSize = true;
			this.Label1.Location = new System.Drawing.Point(16, 203);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(106, 16);
			this.Label1.TabIndex = 10;
			this.Label1.Text = "Image width (pix)";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 103);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(113, 16);
			this.label4.TabIndex = 4;
			this.label4.Text = "Module width (pix)";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(16, 168);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(134, 16);
			this.label7.TabIndex = 8;
			this.label7.Text = "Quiet zone width (pix)";
			// 
			// HeaderLabel
			// 
			this.HeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.HeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.HeaderLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HeaderLabel.Location = new System.Drawing.Point(138, 9);
			this.HeaderLabel.Name = "HeaderLabel";
			this.HeaderLabel.Size = new System.Drawing.Size(292, 32);
			this.HeaderLabel.TabIndex = 0;
			this.HeaderLabel.Text = "Save PDF417 Barcode Image";
			this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ModulesWidthLabel
			// 
			this.ModulesWidthLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ModulesWidthLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ModulesWidthLabel.Location = new System.Drawing.Point(175, 32);
			this.ModulesWidthLabel.Name = "ModulesWidthLabel";
			this.ModulesWidthLabel.Size = new System.Drawing.Size(56, 22);
			this.ModulesWidthLabel.TabIndex = 1;
			this.ModulesWidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 35);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(150, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "PDF417 width (modules)";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.QuietZoneLabel);
			this.groupBox1.Controls.Add(this.ModuleWidthLabel);
			this.groupBox1.Controls.Add(this.ModuleHeightLabel);
			this.groupBox1.Controls.Add(this.ModulesHeightLabel);
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.ImageHeightLabel);
			this.groupBox1.Controls.Add(this.Label25);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.ModulesWidthLabel);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.ImageWidthLabel);
			this.groupBox1.Controls.Add(this.Label1);
			this.groupBox1.Controls.Add(this.ImageFormatComboBox);
			this.groupBox1.Controls.Add(this.Label8);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Location = new System.Drawing.Point(7, 47);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(246, 319);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Pdf417 Barcode to Image";
			// 
			// QuietZoneLabel
			// 
			this.QuietZoneLabel.BackColor = System.Drawing.SystemColors.Info;
			this.QuietZoneLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.QuietZoneLabel.Location = new System.Drawing.Point(175, 165);
			this.QuietZoneLabel.Name = "QuietZoneLabel";
			this.QuietZoneLabel.Size = new System.Drawing.Size(56, 22);
			this.QuietZoneLabel.TabIndex = 9;
			this.QuietZoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ModuleWidthLabel
			// 
			this.ModuleWidthLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ModuleWidthLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ModuleWidthLabel.Location = new System.Drawing.Point(175, 100);
			this.ModuleWidthLabel.Name = "ModuleWidthLabel";
			this.ModuleWidthLabel.Size = new System.Drawing.Size(56, 22);
			this.ModuleWidthLabel.TabIndex = 5;
			this.ModuleWidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ModuleHeightLabel
			// 
			this.ModuleHeightLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ModuleHeightLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ModuleHeightLabel.Location = new System.Drawing.Point(175, 133);
			this.ModuleHeightLabel.Name = "ModuleHeightLabel";
			this.ModuleHeightLabel.Size = new System.Drawing.Size(56, 22);
			this.ModuleHeightLabel.TabIndex = 7;
			this.ModuleHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ModulesHeightLabel
			// 
			this.ModulesHeightLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ModulesHeightLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ModulesHeightLabel.Location = new System.Drawing.Point(175, 66);
			this.ModulesHeightLabel.Name = "ModulesHeightLabel";
			this.ModulesHeightLabel.Size = new System.Drawing.Size(56, 22);
			this.ModulesHeightLabel.TabIndex = 3;
			this.ModulesHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(16, 69);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(155, 16);
			this.label19.TabIndex = 2;
			this.label19.Text = "PDF417 height (modules)";
			// 
			// ImageHeightLabel
			// 
			this.ImageHeightLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ImageHeightLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ImageHeightLabel.Location = new System.Drawing.Point(175, 236);
			this.ImageHeightLabel.Name = "ImageHeightLabel";
			this.ImageHeightLabel.Size = new System.Drawing.Size(56, 22);
			this.ImageHeightLabel.TabIndex = 13;
			this.ImageHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Label25
			// 
			this.Label25.AutoSize = true;
			this.Label25.Location = new System.Drawing.Point(16, 239);
			this.Label25.Name = "Label25";
			this.Label25.Size = new System.Drawing.Size(111, 16);
			this.Label25.TabIndex = 12;
			this.Label25.Text = "Image height (pix)";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(16, 134);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(118, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Module height (pix)";
			// 
			// ImageRotationTextBox
			// 
			this.ImageRotationTextBox.Location = new System.Drawing.Point(216, 352);
			this.ImageRotationTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ImageRotationTextBox.Name = "ImageRotationTextBox";
			this.ImageRotationTextBox.Size = new System.Drawing.Size(56, 22);
			this.ImageRotationTextBox.TabIndex = 21;
			this.ImageRotationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// ImageBGWidthLabel
			// 
			this.ImageBGWidthLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ImageBGWidthLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ImageBGWidthLabel.Location = new System.Drawing.Point(216, 226);
			this.ImageBGWidthLabel.Name = "ImageBGWidthLabel";
			this.ImageBGWidthLabel.Size = new System.Drawing.Size(56, 22);
			this.ImageBGWidthLabel.TabIndex = 13;
			this.ImageBGWidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(13, 355);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(141, 16);
			this.label20.TabIndex = 20;
			this.label20.Text = "Image rotation (degree)";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(13, 229);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(177, 16);
			this.label12.TabIndex = 12;
			this.label12.Text = "Image background width (pix)";
			// 
			// ImageBGHeightLabel
			// 
			this.ImageBGHeightLabel.BackColor = System.Drawing.SystemColors.Info;
			this.ImageBGHeightLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ImageBGHeightLabel.Location = new System.Drawing.Point(216, 252);
			this.ImageBGHeightLabel.Name = "ImageBGHeightLabel";
			this.ImageBGHeightLabel.Size = new System.Drawing.Size(56, 22);
			this.ImageBGHeightLabel.TabIndex = 15;
			this.ImageBGHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 316);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(186, 16);
			this.label6.TabIndex = 18;
			this.label6.Text = "PDF417 center Y position (pix)";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(13, 255);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(182, 16);
			this.label10.TabIndex = 14;
			this.label10.Text = "Image background height (pix)";
			// 
			// BarcodePosYTextBox
			// 
			this.BarcodePosYTextBox.Location = new System.Drawing.Point(216, 313);
			this.BarcodePosYTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BarcodePosYTextBox.Name = "BarcodePosYTextBox";
			this.BarcodePosYTextBox.Size = new System.Drawing.Size(56, 22);
			this.BarcodePosYTextBox.TabIndex = 19;
			this.BarcodePosYTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 290);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(184, 16);
			this.label5.TabIndex = 16;
			this.label5.Text = "PDF417 center X position (pix)";
			// 
			// ImageBrowseButton
			// 
			this.ImageBrowseButton.Location = new System.Drawing.Point(194, 163);
			this.ImageBrowseButton.Name = "ImageBrowseButton";
			this.ImageBrowseButton.Size = new System.Drawing.Size(78, 31);
			this.ImageBrowseButton.TabIndex = 9;
			this.ImageBrowseButton.Text = "Browse";
			this.ImageBrowseButton.UseVisualStyleBackColor = true;
			this.ImageBrowseButton.Click += new System.EventHandler(this.OnImageBrowse);
			// 
			// BarcodePosXTextBox
			// 
			this.BarcodePosXTextBox.Location = new System.Drawing.Point(216, 287);
			this.BarcodePosXTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BarcodePosXTextBox.Name = "BarcodePosXTextBox";
			this.BarcodePosXTextBox.Size = new System.Drawing.Size(56, 22);
			this.BarcodePosXTextBox.TabIndex = 17;
			this.BarcodePosXTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 178);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 16);
			this.label2.TabIndex = 10;
			this.label2.Text = "Image";
			// 
			// ImageFileNameTextBox
			// 
			this.ImageFileNameTextBox.Location = new System.Drawing.Point(16, 199);
			this.ImageFileNameTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ImageFileNameTextBox.Name = "ImageFileNameTextBox";
			this.ImageFileNameTextBox.Size = new System.Drawing.Size(256, 22);
			this.ImageFileNameTextBox.TabIndex = 11;
			this.ImageFileNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.ImageFileNameTextBox.TextChanged += new System.EventHandler(this.ImageFileNameChanged);
			// 
			// HatchStyleComboBox
			// 
			this.HatchStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.HatchStyleComboBox.FormattingEnabled = true;
			this.HatchStyleComboBox.Location = new System.Drawing.Point(82, 67);
			this.HatchStyleComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.HatchStyleComboBox.Name = "HatchStyleComboBox";
			this.HatchStyleComboBox.Size = new System.Drawing.Size(190, 24);
			this.HatchStyleComboBox.TabIndex = 4;
			this.HatchStyleComboBox.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.OnHatchStyleFormat);
			// 
			// BrushHeightTextBox
			// 
			this.BrushHeightTextBox.Location = new System.Drawing.Point(216, 124);
			this.BrushHeightTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BrushHeightTextBox.Name = "BrushHeightTextBox";
			this.BrushHeightTextBox.Size = new System.Drawing.Size(56, 22);
			this.BrushHeightTextBox.TabIndex = 8;
			this.BrushHeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.BrushHeightTextBox.TextChanged += new System.EventHandler(this.OnBrushSizeChanged);
			// 
			// BrushWidthTextBox
			// 
			this.BrushWidthTextBox.Location = new System.Drawing.Point(216, 98);
			this.BrushWidthTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.BrushWidthTextBox.Name = "BrushWidthTextBox";
			this.BrushWidthTextBox.Size = new System.Drawing.Size(56, 22);
			this.BrushWidthTextBox.TabIndex = 6;
			this.BrushWidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.BrushWidthTextBox.TextChanged += new System.EventHandler(this.OnBrushSizeChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(9, 101);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(176, 16);
			this.label11.TabIndex = 5;
			this.label11.Text = "Brush background width (pix)";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(9, 127);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(181, 16);
			this.label15.TabIndex = 7;
			this.label15.Text = "Brush background height (pix)";
			// 
			// BrushColorButton
			// 
			this.BrushColorButton.Location = new System.Drawing.Point(8, 62);
			this.BrushColorButton.Name = "BrushColorButton";
			this.BrushColorButton.Size = new System.Drawing.Size(64, 31);
			this.BrushColorButton.TabIndex = 3;
			this.BrushColorButton.Text = "Color";
			this.BrushColorButton.UseVisualStyleBackColor = true;
			this.BrushColorButton.Click += new System.EventHandler(this.OnSelectColor);
			// 
			// CameraDistanceTextBox
			// 
			this.CameraDistanceTextBox.Location = new System.Drawing.Point(216, 389);
			this.CameraDistanceTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CameraDistanceTextBox.Name = "CameraDistanceTextBox";
			this.CameraDistanceTextBox.Size = new System.Drawing.Size(56, 22);
			this.CameraDistanceTextBox.TabIndex = 23;
			this.CameraDistanceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(13, 392);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(135, 16);
			this.label9.TabIndex = 22;
			this.label9.Text = "Camera distance (pix)";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(13, 418);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(161, 16);
			this.label18.TabIndex = 24;
			this.label18.Text = "Camera view rotation (deg)";
			// 
			// CameraViewRotationTextBox
			// 
			this.CameraViewRotationTextBox.Location = new System.Drawing.Point(216, 415);
			this.CameraViewRotationTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.CameraViewRotationTextBox.Name = "CameraViewRotationTextBox";
			this.CameraViewRotationTextBox.Size = new System.Drawing.Size(56, 22);
			this.CameraViewRotationTextBox.TabIndex = 25;
			this.CameraViewRotationTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.ImageRotationTextBox);
			this.groupBox5.Controls.Add(this.HatchStyleComboBox);
			this.groupBox5.Controls.Add(this.CameraDistanceTextBox);
			this.groupBox5.Controls.Add(this.label20);
			this.groupBox5.Controls.Add(this.label9);
			this.groupBox5.Controls.Add(this.label18);
			this.groupBox5.Controls.Add(this.ImageBGWidthLabel);
			this.groupBox5.Controls.Add(this.CameraViewRotationTextBox);
			this.groupBox5.Controls.Add(this.label6);
			this.groupBox5.Controls.Add(this.BarcodePosYTextBox);
			this.groupBox5.Controls.Add(this.ImageRadioButton);
			this.groupBox5.Controls.Add(this.label5);
			this.groupBox5.Controls.Add(this.BrushHeightTextBox);
			this.groupBox5.Controls.Add(this.BarcodePosXTextBox);
			this.groupBox5.Controls.Add(this.label12);
			this.groupBox5.Controls.Add(this.BrushRadioButton);
			this.groupBox5.Controls.Add(this.ImageBGHeightLabel);
			this.groupBox5.Controls.Add(this.BrushWidthTextBox);
			this.groupBox5.Controls.Add(this.NoneRadioButton);
			this.groupBox5.Controls.Add(this.label10);
			this.groupBox5.Controls.Add(this.label11);
			this.groupBox5.Controls.Add(this.BrushColorButton);
			this.groupBox5.Controls.Add(this.label15);
			this.groupBox5.Controls.Add(this.ImageBrowseButton);
			this.groupBox5.Controls.Add(this.ImageFileNameTextBox);
			this.groupBox5.Controls.Add(this.label2);
			this.groupBox5.Location = new System.Drawing.Point(268, 45);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(291, 455);
			this.groupBox5.TabIndex = 2;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Background";
			// 
			// ImageRadioButton
			// 
			this.ImageRadioButton.AutoSize = true;
			this.ImageRadioButton.Location = new System.Drawing.Point(189, 28);
			this.ImageRadioButton.Name = "ImageRadioButton";
			this.ImageRadioButton.Size = new System.Drawing.Size(61, 20);
			this.ImageRadioButton.TabIndex = 2;
			this.ImageRadioButton.Text = "Image";
			this.ImageRadioButton.UseVisualStyleBackColor = true;
			this.ImageRadioButton.CheckedChanged += new System.EventHandler(this.OnBackgroundRadioButton);
			// 
			// BrushRadioButton
			// 
			this.BrushRadioButton.AutoSize = true;
			this.BrushRadioButton.Location = new System.Drawing.Point(113, 28);
			this.BrushRadioButton.Name = "BrushRadioButton";
			this.BrushRadioButton.Size = new System.Drawing.Size(60, 20);
			this.BrushRadioButton.TabIndex = 1;
			this.BrushRadioButton.Text = "Brush";
			this.BrushRadioButton.UseVisualStyleBackColor = true;
			this.BrushRadioButton.CheckedChanged += new System.EventHandler(this.OnBackgroundRadioButton);
			// 
			// NoneRadioButton
			// 
			this.NoneRadioButton.AutoSize = true;
			this.NoneRadioButton.Location = new System.Drawing.Point(41, 28);
			this.NoneRadioButton.Name = "NoneRadioButton";
			this.NoneRadioButton.Size = new System.Drawing.Size(56, 20);
			this.NoneRadioButton.TabIndex = 0;
			this.NoneRadioButton.Text = "None";
			this.NoneRadioButton.UseVisualStyleBackColor = true;
			this.NoneRadioButton.CheckedChanged += new System.EventHandler(this.OnBackgroundRadioButton);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ErrorNumberTextBox);
			this.groupBox2.Controls.Add(this.ErrorDiameterTextBox);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.ErrorAlternateRadioButton);
			this.groupBox2.Controls.Add(this.ErrorBlackRadioButton);
			this.groupBox2.Controls.Add(this.ErrorWhiteRadioButton);
			this.groupBox2.Controls.Add(this.ErrorNoneRadioButton);
			this.groupBox2.Location = new System.Drawing.Point(12, 372);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(243, 128);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Add Errors";
			// 
			// ErrorNumberTextBox
			// 
			this.ErrorNumberTextBox.Location = new System.Drawing.Point(173, 88);
			this.ErrorNumberTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ErrorNumberTextBox.Name = "ErrorNumberTextBox";
			this.ErrorNumberTextBox.Size = new System.Drawing.Size(56, 22);
			this.ErrorNumberTextBox.TabIndex = 7;
			this.ErrorNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// ErrorDiameterTextBox
			// 
			this.ErrorDiameterTextBox.Location = new System.Drawing.Point(173, 58);
			this.ErrorDiameterTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ErrorDiameterTextBox.Name = "ErrorDiameterTextBox";
			this.ErrorDiameterTextBox.Size = new System.Drawing.Size(56, 22);
			this.ErrorDiameterTextBox.TabIndex = 5;
			this.ErrorDiameterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(11, 91);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(103, 16);
			this.label17.TabIndex = 6;
			this.label17.Text = "Number of spots";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(11, 61);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(118, 16);
			this.label14.TabIndex = 4;
			this.label14.Text = "Spot diameter (pix)";
			// 
			// ErrorAlternateRadioButton
			// 
			this.ErrorAlternateRadioButton.AutoSize = true;
			this.ErrorAlternateRadioButton.Location = new System.Drawing.Point(188, 27);
			this.ErrorAlternateRadioButton.Name = "ErrorAlternateRadioButton";
			this.ErrorAlternateRadioButton.Size = new System.Drawing.Size(42, 20);
			this.ErrorAlternateRadioButton.TabIndex = 3;
			this.ErrorAlternateRadioButton.Text = "Alt";
			this.ErrorAlternateRadioButton.UseVisualStyleBackColor = true;
			this.ErrorAlternateRadioButton.CheckedChanged += new System.EventHandler(this.OnErrorRadioButton);
			// 
			// ErrorBlackRadioButton
			// 
			this.ErrorBlackRadioButton.AutoSize = true;
			this.ErrorBlackRadioButton.Location = new System.Drawing.Point(127, 27);
			this.ErrorBlackRadioButton.Name = "ErrorBlackRadioButton";
			this.ErrorBlackRadioButton.Size = new System.Drawing.Size(59, 20);
			this.ErrorBlackRadioButton.TabIndex = 2;
			this.ErrorBlackRadioButton.Text = "Black";
			this.ErrorBlackRadioButton.UseVisualStyleBackColor = true;
			this.ErrorBlackRadioButton.CheckedChanged += new System.EventHandler(this.OnErrorRadioButton);
			// 
			// ErrorWhiteRadioButton
			// 
			this.ErrorWhiteRadioButton.AutoSize = true;
			this.ErrorWhiteRadioButton.Location = new System.Drawing.Point(65, 27);
			this.ErrorWhiteRadioButton.Name = "ErrorWhiteRadioButton";
			this.ErrorWhiteRadioButton.Size = new System.Drawing.Size(60, 20);
			this.ErrorWhiteRadioButton.TabIndex = 1;
			this.ErrorWhiteRadioButton.Text = "White";
			this.ErrorWhiteRadioButton.UseVisualStyleBackColor = true;
			this.ErrorWhiteRadioButton.CheckedChanged += new System.EventHandler(this.OnErrorRadioButton);
			// 
			// ErrorNoneRadioButton
			// 
			this.ErrorNoneRadioButton.AutoSize = true;
			this.ErrorNoneRadioButton.Location = new System.Drawing.Point(7, 27);
			this.ErrorNoneRadioButton.Name = "ErrorNoneRadioButton";
			this.ErrorNoneRadioButton.Size = new System.Drawing.Size(56, 20);
			this.ErrorNoneRadioButton.TabIndex = 0;
			this.ErrorNoneRadioButton.Text = "None";
			this.ErrorNoneRadioButton.UseVisualStyleBackColor = true;
			this.ErrorNoneRadioButton.CheckedChanged += new System.EventHandler(this.OnErrorRadioButton);
			// 
			// SaveImage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(568, 568);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.SaveImageButton);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.HeaderLabel);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveImage";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Save PDF417 Barcode Image";
			this.Load += new System.EventHandler(this.OnLoad);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button SaveImageButton;
		private System.Windows.Forms.ComboBox ImageFormatComboBox;
		private System.Windows.Forms.Label Label8;
		private System.Windows.Forms.Label ImageWidthLabel;
		private System.Windows.Forms.Label Label1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label HeaderLabel;
		private System.Windows.Forms.Label ModulesWidthLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox BarcodePosYTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button ImageBrowseButton;
		private System.Windows.Forms.TextBox BarcodePosXTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox ImageFileNameTextBox;
		private System.Windows.Forms.Label ImageBGWidthLabel;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label ImageBGHeightLabel;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox BrushHeightTextBox;
		private System.Windows.Forms.TextBox BrushWidthTextBox;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button BrushColorButton;
		private System.Windows.Forms.ComboBox HatchStyleComboBox;
		private System.Windows.Forms.TextBox CameraDistanceTextBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox CameraViewRotationTextBox;
		private System.Windows.Forms.TextBox ImageRotationTextBox;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton ImageRadioButton;
		private System.Windows.Forms.RadioButton BrushRadioButton;
		private System.Windows.Forms.RadioButton NoneRadioButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox ErrorNumberTextBox;
		private System.Windows.Forms.TextBox ErrorDiameterTextBox;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.RadioButton ErrorAlternateRadioButton;
		private System.Windows.Forms.RadioButton ErrorBlackRadioButton;
		private System.Windows.Forms.RadioButton ErrorWhiteRadioButton;
		private System.Windows.Forms.RadioButton ErrorNoneRadioButton;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label ImageHeightLabel;
		private System.Windows.Forms.Label Label25;
		private System.Windows.Forms.Label ModulesHeightLabel;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label ModuleHeightLabel;
		private System.Windows.Forms.Label QuietZoneLabel;
		private System.Windows.Forms.Label ModuleWidthLabel;
	}
}