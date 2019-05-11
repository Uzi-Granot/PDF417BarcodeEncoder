namespace Pdf417EncoderDemo
{
	partial class AdjustLayout
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
			this.WidthHeightRadioButton = new System.Windows.Forms.RadioButton();
			this.WidthHeightTextBox = new System.Windows.Forms.TextBox();
			this.DataRowsTextBox = new System.Windows.Forms.TextBox();
			this.DataRowsRadioButton = new System.Windows.Forms.RadioButton();
			this.DataColumnsTextBox = new System.Windows.Forms.TextBox();
			this.DataColumnsRadioButton = new System.Windows.Forms.RadioButton();
			this.OK_Button = new System.Windows.Forms.Button();
			this.Cancel_Button = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// WidthHeightRadioButton
			// 
			this.WidthHeightRadioButton.AutoSize = true;
			this.WidthHeightRadioButton.Checked = true;
			this.WidthHeightRadioButton.Location = new System.Drawing.Point(15, 47);
			this.WidthHeightRadioButton.Name = "WidthHeightRadioButton";
			this.WidthHeightRadioButton.Size = new System.Drawing.Size(191, 20);
			this.WidthHeightRadioButton.TabIndex = 0;
			this.WidthHeightRadioButton.TabStop = true;
			this.WidthHeightRadioButton.Tag = "0";
			this.WidthHeightRadioButton.Text = "Barcode width to height ratio";
			this.WidthHeightRadioButton.UseVisualStyleBackColor = true;
			this.WidthHeightRadioButton.CheckedChanged += new System.EventHandler(this.OnWidthHeightChanged);
			// 
			// WidthHeightTextBox
			// 
			this.WidthHeightTextBox.Location = new System.Drawing.Point(59, 76);
			this.WidthHeightTextBox.Name = "WidthHeightTextBox";
			this.WidthHeightTextBox.Size = new System.Drawing.Size(86, 22);
			this.WidthHeightTextBox.TabIndex = 1;
			// 
			// DataRowsTextBox
			// 
			this.DataRowsTextBox.Location = new System.Drawing.Point(59, 149);
			this.DataRowsTextBox.Name = "DataRowsTextBox";
			this.DataRowsTextBox.Size = new System.Drawing.Size(86, 22);
			this.DataRowsTextBox.TabIndex = 3;
			// 
			// DataRowsRadioButton
			// 
			this.DataRowsRadioButton.AutoSize = true;
			this.DataRowsRadioButton.Location = new System.Drawing.Point(15, 120);
			this.DataRowsRadioButton.Name = "DataRowsRadioButton";
			this.DataRowsRadioButton.Size = new System.Drawing.Size(84, 20);
			this.DataRowsRadioButton.TabIndex = 2;
			this.DataRowsRadioButton.Tag = "";
			this.DataRowsRadioButton.Text = "Data rows";
			this.DataRowsRadioButton.UseVisualStyleBackColor = true;
			this.DataRowsRadioButton.CheckedChanged += new System.EventHandler(this.OnDataRowsChanged);
			// 
			// DataColumnsTextBox
			// 
			this.DataColumnsTextBox.Location = new System.Drawing.Point(59, 228);
			this.DataColumnsTextBox.Name = "DataColumnsTextBox";
			this.DataColumnsTextBox.Size = new System.Drawing.Size(86, 22);
			this.DataColumnsTextBox.TabIndex = 5;
			// 
			// DataColumnsRadioButton
			// 
			this.DataColumnsRadioButton.AutoSize = true;
			this.DataColumnsRadioButton.Location = new System.Drawing.Point(15, 199);
			this.DataColumnsRadioButton.Name = "DataColumnsRadioButton";
			this.DataColumnsRadioButton.Size = new System.Drawing.Size(106, 20);
			this.DataColumnsRadioButton.TabIndex = 4;
			this.DataColumnsRadioButton.Tag = "";
			this.DataColumnsRadioButton.Text = "Data columns";
			this.DataColumnsRadioButton.UseVisualStyleBackColor = true;
			this.DataColumnsRadioButton.CheckedChanged += new System.EventHandler(this.OnDataColumnsChanged);
			// 
			// OK_Button
			// 
			this.OK_Button.Location = new System.Drawing.Point(34, 288);
			this.OK_Button.Name = "OK_Button";
			this.OK_Button.Size = new System.Drawing.Size(74, 37);
			this.OK_Button.TabIndex = 6;
			this.OK_Button.Text = "OK";
			this.OK_Button.UseVisualStyleBackColor = true;
			this.OK_Button.Click += new System.EventHandler(this.OnOK);
			// 
			// Cancel_Button
			// 
			this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_Button.Location = new System.Drawing.Point(127, 287);
			this.Cancel_Button.Name = "Cancel_Button";
			this.Cancel_Button.Size = new System.Drawing.Size(74, 37);
			this.Cancel_Button.TabIndex = 7;
			this.Cancel_Button.Text = "Cancel";
			this.Cancel_Button.UseVisualStyleBackColor = true;
			// 
			// AdjustLayout
			// 
			this.AcceptButton = this.OK_Button;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(241, 342);
			this.Controls.Add(this.Cancel_Button);
			this.Controls.Add(this.OK_Button);
			this.Controls.Add(this.DataColumnsTextBox);
			this.Controls.Add(this.DataColumnsRadioButton);
			this.Controls.Add(this.DataRowsTextBox);
			this.Controls.Add(this.DataRowsRadioButton);
			this.Controls.Add(this.WidthHeightTextBox);
			this.Controls.Add(this.WidthHeightRadioButton);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AdjustLayout";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Adjust Width to Height Ratio";
			this.Load += new System.EventHandler(this.OnLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton WidthHeightRadioButton;
		private System.Windows.Forms.TextBox WidthHeightTextBox;
		private System.Windows.Forms.TextBox DataRowsTextBox;
		private System.Windows.Forms.RadioButton DataRowsRadioButton;
		private System.Windows.Forms.TextBox DataColumnsTextBox;
		private System.Windows.Forms.RadioButton DataColumnsRadioButton;
		private System.Windows.Forms.Button OK_Button;
		private System.Windows.Forms.Button Cancel_Button;
	}
}