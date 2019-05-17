namespace QRCodeEncoderDemo
{
	partial class QRCodeEncoderDemo
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QRCodeEncoderDemo));
			this.HeaderLabel = new System.Windows.Forms.Label();
			this.DefaultButton = new System.Windows.Forms.Button();
			this.EncodeButton = new System.Windows.Forms.Button();
			this.SaveImageButton = new System.Windows.Forms.Button();
			this.DataTextBox = new System.Windows.Forms.TextBox();
			this.DataLabel = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.ErrorCorrectionComboBox = new System.Windows.Forms.ComboBox();
			this.ButtonsGroupBox = new System.Windows.Forms.GroupBox();
			this.QuietZoneTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.ModuleSizeTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SaveToPngButton = new System.Windows.Forms.Button();
			this.SeparatorCheckBox = new System.Windows.Forms.CheckBox();
			this.ButtonsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// HeaderLabel
			// 
			this.HeaderLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.HeaderLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.HeaderLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HeaderLabel.Location = new System.Drawing.Point(241, 13);
			this.HeaderLabel.Name = "HeaderLabel";
			this.HeaderLabel.Size = new System.Drawing.Size(210, 32);
			this.HeaderLabel.TabIndex = 0;
			this.HeaderLabel.Text = "QR Code Encoder";
			this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DefaultButton
			// 
			this.DefaultButton.Location = new System.Drawing.Point(13, 314);
			this.DefaultButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DefaultButton.Name = "DefaultButton";
			this.DefaultButton.Size = new System.Drawing.Size(122, 38);
			this.DefaultButton.TabIndex = 9;
			this.DefaultButton.Text = "Default";
			this.DefaultButton.UseVisualStyleBackColor = true;
			this.DefaultButton.Click += new System.EventHandler(this.OnDefault);
			// 
			// EncodeButton
			// 
			this.EncodeButton.Location = new System.Drawing.Point(13, 176);
			this.EncodeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.EncodeButton.Name = "EncodeButton";
			this.EncodeButton.Size = new System.Drawing.Size(122, 38);
			this.EncodeButton.TabIndex = 6;
			this.EncodeButton.Text = "Encode";
			this.EncodeButton.UseVisualStyleBackColor = true;
			this.EncodeButton.Click += new System.EventHandler(this.OnEncode);
			// 
			// SaveImageButton
			// 
			this.SaveImageButton.Location = new System.Drawing.Point(13, 268);
			this.SaveImageButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.SaveImageButton.Name = "SaveImageButton";
			this.SaveImageButton.Size = new System.Drawing.Size(122, 38);
			this.SaveImageButton.TabIndex = 8;
			this.SaveImageButton.Text = "Save Image";
			this.SaveImageButton.UseVisualStyleBackColor = true;
			this.SaveImageButton.Click += new System.EventHandler(this.OnSaveImage);
			// 
			// DataTextBox
			// 
			this.DataTextBox.Location = new System.Drawing.Point(11, 414);
			this.DataTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.DataTextBox.Multiline = true;
			this.DataTextBox.Name = "DataTextBox";
			this.DataTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.DataTextBox.Size = new System.Drawing.Size(569, 83);
			this.DataTextBox.TabIndex = 4;
			// 
			// DataLabel
			// 
			this.DataLabel.AutoSize = true;
			this.DataLabel.Location = new System.Drawing.Point(13, 392);
			this.DataLabel.Name = "DataLabel";
			this.DataLabel.Size = new System.Drawing.Size(247, 16);
			this.DataLabel.TabIndex = 2;
			this.DataLabel.Text = "Enter your data to be encoded in this box";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 17);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(99, 16);
			this.label5.TabIndex = 0;
			this.label5.Text = "Error Correction";
			// 
			// ErrorCorrectionComboBox
			// 
			this.ErrorCorrectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ErrorCorrectionComboBox.FormattingEnabled = true;
			this.ErrorCorrectionComboBox.Location = new System.Drawing.Point(13, 38);
			this.ErrorCorrectionComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.ErrorCorrectionComboBox.Name = "ErrorCorrectionComboBox";
			this.ErrorCorrectionComboBox.Size = new System.Drawing.Size(78, 24);
			this.ErrorCorrectionComboBox.TabIndex = 1;
			// 
			// ButtonsGroupBox
			// 
			this.ButtonsGroupBox.BackColor = System.Drawing.SystemColors.Control;
			this.ButtonsGroupBox.Controls.Add(this.QuietZoneTextBox);
			this.ButtonsGroupBox.Controls.Add(this.label2);
			this.ButtonsGroupBox.Controls.Add(this.ModuleSizeTextBox);
			this.ButtonsGroupBox.Controls.Add(this.label1);
			this.ButtonsGroupBox.Controls.Add(this.SaveToPngButton);
			this.ButtonsGroupBox.Controls.Add(this.SaveImageButton);
			this.ButtonsGroupBox.Controls.Add(this.label5);
			this.ButtonsGroupBox.Controls.Add(this.EncodeButton);
			this.ButtonsGroupBox.Controls.Add(this.ErrorCorrectionComboBox);
			this.ButtonsGroupBox.Controls.Add(this.DefaultButton);
			this.ButtonsGroupBox.Location = new System.Drawing.Point(11, 13);
			this.ButtonsGroupBox.Name = "ButtonsGroupBox";
			this.ButtonsGroupBox.Size = new System.Drawing.Size(146, 363);
			this.ButtonsGroupBox.TabIndex = 1;
			this.ButtonsGroupBox.TabStop = false;
			// 
			// QuietZoneTextBox
			// 
			this.QuietZoneTextBox.Location = new System.Drawing.Point(13, 139);
			this.QuietZoneTextBox.Name = "QuietZoneTextBox";
			this.QuietZoneTextBox.Size = new System.Drawing.Size(42, 22);
			this.QuietZoneTextBox.TabIndex = 5;
			this.QuietZoneTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 119);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "Quiet zone (pix)";
			// 
			// ModuleSizeTextBox
			// 
			this.ModuleSizeTextBox.Location = new System.Drawing.Point(13, 89);
			this.ModuleSizeTextBox.Name = "ModuleSizeTextBox";
			this.ModuleSizeTextBox.Size = new System.Drawing.Size(42, 22);
			this.ModuleSizeTextBox.TabIndex = 3;
			this.ModuleSizeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 70);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Module size (pix)";
			// 
			// SaveToPngButton
			// 
			this.SaveToPngButton.Location = new System.Drawing.Point(13, 222);
			this.SaveToPngButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.SaveToPngButton.Name = "SaveToPngButton";
			this.SaveToPngButton.Size = new System.Drawing.Size(122, 38);
			this.SaveToPngButton.TabIndex = 7;
			this.SaveToPngButton.Text = "Save PNG";
			this.SaveToPngButton.UseVisualStyleBackColor = true;
			this.SaveToPngButton.Click += new System.EventHandler(this.OnSavePNG);
			// 
			// SeparatorCheckBox
			// 
			this.SeparatorCheckBox.AutoSize = true;
			this.SeparatorCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.SeparatorCheckBox.Location = new System.Drawing.Point(350, 391);
			this.SeparatorCheckBox.Name = "SeparatorCheckBox";
			this.SeparatorCheckBox.Size = new System.Drawing.Size(230, 20);
			this.SeparatorCheckBox.TabIndex = 3;
			this.SeparatorCheckBox.Text = "Use pipe | to create data segments";
			this.SeparatorCheckBox.UseVisualStyleBackColor = true;
			// 
			// QRCodeEncoderDemo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.ClientSize = new System.Drawing.Size(594, 511);
			this.Controls.Add(this.SeparatorCheckBox);
			this.Controls.Add(this.ButtonsGroupBox);
			this.Controls.Add(this.DataTextBox);
			this.Controls.Add(this.DataLabel);
			this.Controls.Add(this.HeaderLabel);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimumSize = new System.Drawing.Size(610, 550);
			this.Name = "QRCodeEncoderDemo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "QR Code Encoder";
			this.Load += new System.EventHandler(this.OnLoad);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
			this.Resize += new System.EventHandler(this.OnResize);
			this.ButtonsGroupBox.ResumeLayout(false);
			this.ButtonsGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label HeaderLabel;
		private System.Windows.Forms.Button DefaultButton;
		private System.Windows.Forms.Button EncodeButton;
		private System.Windows.Forms.Button SaveImageButton;
		private System.Windows.Forms.TextBox DataTextBox;
		private System.Windows.Forms.Label DataLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox ErrorCorrectionComboBox;
		private System.Windows.Forms.GroupBox ButtonsGroupBox;
		private System.Windows.Forms.CheckBox SeparatorCheckBox;
		private System.Windows.Forms.TextBox QuietZoneTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox ModuleSizeTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button SaveToPngButton;
		}
}

