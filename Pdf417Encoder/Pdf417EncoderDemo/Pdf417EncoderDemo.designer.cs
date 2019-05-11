namespace Pdf417EncoderDemo
{
	partial class Pdf417EncoderDemo
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
			this.ButtonsGroupBox = new System.Windows.Forms.GroupBox();
			this.AdjustLayoutButton = new System.Windows.Forms.Button();
			this.SamplesComboBox = new System.Windows.Forms.ComboBox();
			this.LoadFileButton = new System.Windows.Forms.Button();
			this.SaveImageButton = new System.Windows.Forms.Button();
			this.EncodeButton = new System.Windows.Forms.Button();
			this.RowHeightTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.EncodingComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.CharacterSetComboBox = new System.Windows.Forms.ComboBox();
			this.DefDataColTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.ErrorCorrectionComboBox = new System.Windows.Forms.ComboBox();
			this.DataTextBox = new System.Windows.Forms.TextBox();
			this.DataLabel = new System.Windows.Forms.Label();
			this.HeaderLabel = new System.Windows.Forms.Label();
			this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
			this.DataColumnsLabel = new System.Windows.Forms.Label();
			this.DataRowsLabel = new System.Windows.Forms.Label();
			this.WidthHeightLabel = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.QuietZoneTextBox = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.BarWidthTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SavePngButton = new System.Windows.Forms.Button();
			this.ButtonsGroupBox.SuspendLayout();
			this.OptionsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonsGroupBox
			// 
			this.ButtonsGroupBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ButtonsGroupBox.Controls.Add(this.SavePngButton);
			this.ButtonsGroupBox.Controls.Add(this.AdjustLayoutButton);
			this.ButtonsGroupBox.Controls.Add(this.SamplesComboBox);
			this.ButtonsGroupBox.Controls.Add(this.LoadFileButton);
			this.ButtonsGroupBox.Controls.Add(this.SaveImageButton);
			this.ButtonsGroupBox.Controls.Add(this.EncodeButton);
			this.ButtonsGroupBox.Location = new System.Drawing.Point(8, 534);
			this.ButtonsGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ButtonsGroupBox.Name = "ButtonsGroupBox";
			this.ButtonsGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ButtonsGroupBox.Size = new System.Drawing.Size(644, 55);
			this.ButtonsGroupBox.TabIndex = 4;
			this.ButtonsGroupBox.TabStop = false;
			// 
			// AdjustLayoutButton
			// 
			this.AdjustLayoutButton.Location = new System.Drawing.Point(188, 11);
			this.AdjustLayoutButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.AdjustLayoutButton.Name = "AdjustLayoutButton";
			this.AdjustLayoutButton.Size = new System.Drawing.Size(80, 32);
			this.AdjustLayoutButton.TabIndex = 2;
			this.AdjustLayoutButton.Text = "Adjust";
			this.AdjustLayoutButton.UseVisualStyleBackColor = true;
			this.AdjustLayoutButton.Click += new System.EventHandler(this.OnAdjustLayout);
			// 
			// SamplesComboBox
			// 
			this.SamplesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SamplesComboBox.FormattingEnabled = true;
			this.SamplesComboBox.Location = new System.Drawing.Point(461, 16);
			this.SamplesComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.SamplesComboBox.Name = "SamplesComboBox";
			this.SamplesComboBox.Size = new System.Drawing.Size(171, 24);
			this.SamplesComboBox.TabIndex = 5;
			this.SamplesComboBox.SelectedIndexChanged += new System.EventHandler(this.SamplesChanged);
			// 
			// LoadFileButton
			// 
			this.LoadFileButton.Location = new System.Drawing.Point(6, 11);
			this.LoadFileButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.LoadFileButton.Name = "LoadFileButton";
			this.LoadFileButton.Size = new System.Drawing.Size(80, 32);
			this.LoadFileButton.TabIndex = 0;
			this.LoadFileButton.Text = "Load File";
			this.LoadFileButton.UseVisualStyleBackColor = true;
			this.LoadFileButton.Click += new System.EventHandler(this.OnLoadFile);
			// 
			// SaveImageButton
			// 
			this.SaveImageButton.Location = new System.Drawing.Point(370, 11);
			this.SaveImageButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.SaveImageButton.Name = "SaveImageButton";
			this.SaveImageButton.Size = new System.Drawing.Size(80, 32);
			this.SaveImageButton.TabIndex = 4;
			this.SaveImageButton.Text = "Save";
			this.SaveImageButton.UseVisualStyleBackColor = true;
			this.SaveImageButton.Click += new System.EventHandler(this.OnSaveImage);
			// 
			// EncodeButton
			// 
			this.EncodeButton.Location = new System.Drawing.Point(97, 11);
			this.EncodeButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.EncodeButton.Name = "EncodeButton";
			this.EncodeButton.Size = new System.Drawing.Size(80, 32);
			this.EncodeButton.TabIndex = 1;
			this.EncodeButton.Text = "Encode";
			this.EncodeButton.UseVisualStyleBackColor = true;
			this.EncodeButton.Click += new System.EventHandler(this.OnEncode);
			// 
			// RowHeightTextBox
			// 
			this.RowHeightTextBox.Location = new System.Drawing.Point(110, 190);
			this.RowHeightTextBox.MaxLength = 2;
			this.RowHeightTextBox.Name = "RowHeightTextBox";
			this.RowHeightTextBox.Size = new System.Drawing.Size(52, 22);
			this.RowHeightTextBox.TabIndex = 9;
			this.RowHeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 193);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(101, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "Row height (pix)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = "Encoding";
			// 
			// EncodingComboBox
			// 
			this.EncodingComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.EncodingComboBox.FormattingEnabled = true;
			this.EncodingComboBox.Location = new System.Drawing.Point(9, 29);
			this.EncodingComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.EncodingComboBox.Name = "EncodingComboBox";
			this.EncodingComboBox.Size = new System.Drawing.Size(90, 24);
			this.EncodingComboBox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 106);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(90, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "ISO 8859-Part";
			// 
			// CharacterSetComboBox
			// 
			this.CharacterSetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CharacterSetComboBox.FormattingEnabled = true;
			this.CharacterSetComboBox.Location = new System.Drawing.Point(9, 126);
			this.CharacterSetComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.CharacterSetComboBox.Name = "CharacterSetComboBox";
			this.CharacterSetComboBox.Size = new System.Drawing.Size(153, 24);
			this.CharacterSetComboBox.TabIndex = 5;
			// 
			// DefDataColTextBox
			// 
			this.DefDataColTextBox.Location = new System.Drawing.Point(110, 246);
			this.DefDataColTextBox.MaxLength = 2;
			this.DefDataColTextBox.Name = "DefDataColTextBox";
			this.DefDataColTextBox.Size = new System.Drawing.Size(52, 22);
			this.DefDataColTextBox.TabIndex = 13;
			this.DefDataColTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 58);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 16);
			this.label5.TabIndex = 2;
			this.label5.Text = "Error Correction";
			// 
			// ErrorCorrectionComboBox
			// 
			this.ErrorCorrectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ErrorCorrectionComboBox.FormattingEnabled = true;
			this.ErrorCorrectionComboBox.Location = new System.Drawing.Point(9, 78);
			this.ErrorCorrectionComboBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.ErrorCorrectionComboBox.Name = "ErrorCorrectionComboBox";
			this.ErrorCorrectionComboBox.Size = new System.Drawing.Size(102, 24);
			this.ErrorCorrectionComboBox.TabIndex = 3;
			// 
			// DataTextBox
			// 
			this.DataTextBox.Location = new System.Drawing.Point(28, 462);
			this.DataTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.DataTextBox.Multiline = true;
			this.DataTextBox.Name = "DataTextBox";
			this.DataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.DataTextBox.Size = new System.Drawing.Size(598, 64);
			this.DataTextBox.TabIndex = 3;
			// 
			// DataLabel
			// 
			this.DataLabel.AutoSize = true;
			this.DataLabel.Location = new System.Drawing.Point(30, 440);
			this.DataLabel.Name = "DataLabel";
			this.DataLabel.Size = new System.Drawing.Size(247, 16);
			this.DataLabel.TabIndex = 2;
			this.DataLabel.Text = "Enter your data to be encoded in this box";
			// 
			// HeaderLabel
			// 
			this.HeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.HeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.HeaderLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HeaderLabel.Location = new System.Drawing.Point(205, 9);
			this.HeaderLabel.Name = "HeaderLabel";
			this.HeaderLabel.Size = new System.Drawing.Size(247, 39);
			this.HeaderLabel.TabIndex = 0;
			this.HeaderLabel.Text = "PDF417 Barcode Encoder";
			this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// OptionsGroupBox
			// 
			this.OptionsGroupBox.BackColor = System.Drawing.SystemColors.ControlLight;
			this.OptionsGroupBox.Controls.Add(this.DataColumnsLabel);
			this.OptionsGroupBox.Controls.Add(this.DataRowsLabel);
			this.OptionsGroupBox.Controls.Add(this.WidthHeightLabel);
			this.OptionsGroupBox.Controls.Add(this.label10);
			this.OptionsGroupBox.Controls.Add(this.label9);
			this.OptionsGroupBox.Controls.Add(this.label8);
			this.OptionsGroupBox.Controls.Add(this.label7);
			this.OptionsGroupBox.Controls.Add(this.QuietZoneTextBox);
			this.OptionsGroupBox.Controls.Add(this.label6);
			this.OptionsGroupBox.Controls.Add(this.BarWidthTextBox);
			this.OptionsGroupBox.Controls.Add(this.label1);
			this.OptionsGroupBox.Controls.Add(this.EncodingComboBox);
			this.OptionsGroupBox.Controls.Add(this.label3);
			this.OptionsGroupBox.Controls.Add(this.ErrorCorrectionComboBox);
			this.OptionsGroupBox.Controls.Add(this.label5);
			this.OptionsGroupBox.Controls.Add(this.DefDataColTextBox);
			this.OptionsGroupBox.Controls.Add(this.RowHeightTextBox);
			this.OptionsGroupBox.Controls.Add(this.CharacterSetComboBox);
			this.OptionsGroupBox.Controls.Add(this.label4);
			this.OptionsGroupBox.Controls.Add(this.label2);
			this.OptionsGroupBox.Location = new System.Drawing.Point(8, 8);
			this.OptionsGroupBox.Name = "OptionsGroupBox";
			this.OptionsGroupBox.Size = new System.Drawing.Size(171, 372);
			this.OptionsGroupBox.TabIndex = 1;
			this.OptionsGroupBox.TabStop = false;
			// 
			// DataColumnsLabel
			// 
			this.DataColumnsLabel.BackColor = System.Drawing.SystemColors.Info;
			this.DataColumnsLabel.Location = new System.Drawing.Point(110, 341);
			this.DataColumnsLabel.Name = "DataColumnsLabel";
			this.DataColumnsLabel.Size = new System.Drawing.Size(52, 22);
			this.DataColumnsLabel.TabIndex = 19;
			this.DataColumnsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DataRowsLabel
			// 
			this.DataRowsLabel.BackColor = System.Drawing.SystemColors.Info;
			this.DataRowsLabel.Location = new System.Drawing.Point(110, 311);
			this.DataRowsLabel.Name = "DataRowsLabel";
			this.DataRowsLabel.Size = new System.Drawing.Size(52, 22);
			this.DataRowsLabel.TabIndex = 17;
			this.DataRowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// WidthHeightLabel
			// 
			this.WidthHeightLabel.BackColor = System.Drawing.SystemColors.Info;
			this.WidthHeightLabel.Location = new System.Drawing.Point(110, 282);
			this.WidthHeightLabel.Name = "WidthHeightLabel";
			this.WidthHeightLabel.Size = new System.Drawing.Size(52, 22);
			this.WidthHeightLabel.TabIndex = 15;
			this.WidthHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(7, 344);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(88, 16);
			this.label10.TabIndex = 18;
			this.label10.Text = "Data columns";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(7, 314);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(66, 16);
			this.label9.TabIndex = 16;
			this.label9.Text = "Data rows";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(7, 285);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(83, 16);
			this.label8.TabIndex = 14;
			this.label8.Text = "Width/Height";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(6, 249);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(88, 16);
			this.label7.TabIndex = 12;
			this.label7.Text = "Data columns";
			// 
			// QuietZoneTextBox
			// 
			this.QuietZoneTextBox.Location = new System.Drawing.Point(110, 218);
			this.QuietZoneTextBox.MaxLength = 2;
			this.QuietZoneTextBox.Name = "QuietZoneTextBox";
			this.QuietZoneTextBox.Size = new System.Drawing.Size(52, 22);
			this.QuietZoneTextBox.TabIndex = 11;
			this.QuietZoneTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(7, 221);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 10;
			this.label6.Text = "Quiet zone (pix)";
			// 
			// BarWidthTextBox
			// 
			this.BarWidthTextBox.Location = new System.Drawing.Point(110, 162);
			this.BarWidthTextBox.MaxLength = 2;
			this.BarWidthTextBox.Name = "BarWidthTextBox";
			this.BarWidthTextBox.Size = new System.Drawing.Size(52, 22);
			this.BarWidthTextBox.TabIndex = 7;
			this.BarWidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 165);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 16);
			this.label1.TabIndex = 6;
			this.label1.Text = "Bar width (pix)";
			// 
			// SavePngButton
			// 
			this.SavePngButton.Location = new System.Drawing.Point(279, 11);
			this.SavePngButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
			this.SavePngButton.Name = "SavePngButton";
			this.SavePngButton.Size = new System.Drawing.Size(80, 32);
			this.SavePngButton.TabIndex = 3;
			this.SavePngButton.Text = "Save PNG";
			this.SavePngButton.UseVisualStyleBackColor = true;
			this.SavePngButton.Click += new System.EventHandler(this.OnSavePNG);
			// 
			// Pdf417EncoderDemo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.ClientSize = new System.Drawing.Size(664, 601);
			this.Controls.Add(this.OptionsGroupBox);
			this.Controls.Add(this.ButtonsGroupBox);
			this.Controls.Add(this.DataTextBox);
			this.Controls.Add(this.DataLabel);
			this.Controls.Add(this.HeaderLabel);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(640, 600);
			this.Name = "Pdf417EncoderDemo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.OnLoad);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
			this.Resize += new System.EventHandler(this.OnResize);
			this.ButtonsGroupBox.ResumeLayout(false);
			this.OptionsGroupBox.ResumeLayout(false);
			this.OptionsGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.GroupBox ButtonsGroupBox;
		private System.Windows.Forms.Button SaveImageButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button EncodeButton;
		private System.Windows.Forms.ComboBox ErrorCorrectionComboBox;
		private System.Windows.Forms.TextBox DataTextBox;
		private System.Windows.Forms.Label DataLabel;
		private System.Windows.Forms.Label HeaderLabel;
		private System.Windows.Forms.Button LoadFileButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox CharacterSetComboBox;
		private System.Windows.Forms.TextBox DefDataColTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox EncodingComboBox;
		private System.Windows.Forms.TextBox RowHeightTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox OptionsGroupBox;
		private System.Windows.Forms.ComboBox SamplesComboBox;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox QuietZoneTextBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox BarWidthTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button AdjustLayoutButton;
		private System.Windows.Forms.Label DataColumnsLabel;
		private System.Windows.Forms.Label DataRowsLabel;
		private System.Windows.Forms.Label WidthHeightLabel;
		private System.Windows.Forms.Button SavePngButton;
		}
}

