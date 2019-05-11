/////////////////////////////////////////////////////////////////////
//
//	PDF417 Barcode Encoder
//
//	Adjust barcode width to height ratio
//
//	Author: Uzi Granot
//	Version: 1.0
//	Date: May 7, 2019
//	Copyright (C) 2019 Uzi Granot. All Rights Reserved
//
//	PDF417 barcode encoder class and the attached test/demo
//  applications are free software.
//	Software developed by this author is licensed under CPOL 1.02.
//
//	The main points of CPOL 1.02 subject to the terms of the License are:
//
//	Source Code and Executable Files can be used in commercial applications;
//	Source Code and Executable Files can be redistributed; and
//	Source Code can be modified to create derivative works.
//	No claim of suitability, guarantee, or any warranty whatsoever is
//	provided. The software is provided "as-is".
//	The Article accompanying the Work may not be distributed or republished
//	without the Author's consent
//
//	Version History
//	---------------
//
//	Version 1.0 2019/04/01
//		Original version
/////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using Pdf417EncoderLibrary;

namespace Pdf417EncoderDemo
{
public partial class AdjustLayout : Form
	{
	private Pdf417Encoder Encoder;
	private int CheckedField = 0;

	// constructor
	public AdjustLayout
			(
			Pdf417Encoder Encoder
			)
		{
		this.Encoder = Encoder;
		InitializeComponent();
		return;
		}

	// form initialization
	private void OnLoad(object sender, EventArgs e)
		{
		WidthHeightTextBox.Text = ((double) Encoder.ImageWidth / Encoder.ImageHeight).ToString("0.0");
		DataRowsTextBox.Text = Encoder.DataRows.ToString();
		DataColumnsTextBox.Text = Encoder.DataColumns.ToString();
		DataRowsTextBox.Enabled = false;
		DataColumnsTextBox.Enabled = false;
		return;
		}

	// Radio button width to height ratio is activated or deactivated
	private void OnWidthHeightChanged(object sender, EventArgs e)
		{
		if(!((RadioButton) sender).Checked)
			{
			WidthHeightTextBox.Text = ((double) Encoder.ImageWidth / Encoder.ImageHeight).ToString("0.0");
			WidthHeightTextBox.Enabled = false;
			}
		else
			{
			CheckedField = 0;
			WidthHeightTextBox.Enabled = true;
			}
		return;
		}

	// radio button data rows activated or deactivated
	private void OnDataRowsChanged(object sender, EventArgs e)
		{
		if(!((RadioButton) sender).Checked)
			{
			DataRowsTextBox.Text = Encoder.DataRows.ToString();
			DataRowsTextBox.Enabled = false;
			}
		else
			{
			CheckedField = 1;
			DataRowsTextBox.Enabled = true;
			}
		return;
		}

	// radio button data columns activated or deactivated
	private void OnDataColumnsChanged(object sender, EventArgs e)
		{
		if(!((RadioButton) sender).Checked)
			{
			DataColumnsTextBox.Text = Encoder.DataColumns.ToString();
			DataColumnsTextBox.Enabled = false;
			}
		else
			{
			CheckedField = 2;
			DataColumnsTextBox.Enabled = true;
			}
		return;
		}

	// OK button pressed
	private void OnOK(object sender, EventArgs e)
		{
		switch(CheckedField)
			{
			case 0:
				if(!double.TryParse(WidthHeightTextBox.Text.Trim(), out double WidthHeight) ||
					WidthHeight < 0.1 || WidthHeight > 10.0 || !Encoder.WidthToHeightRatio(WidthHeight))
					{
					MessageBox.Show("Invalid width to height ratio");
					return;
					}
				break;

			case 1:
			if(!int.TryParse(DataRowsTextBox.Text.Trim(), out int DataRows) || !Encoder.SetDataRows(DataRows))
					{
					MessageBox.Show("Invalid number of data rows");
					return;
					}
				break;

			case 2:
			if(!int.TryParse(DataColumnsTextBox.Text.Trim(), out int DataColumns) || !Encoder.SetDataColumns(DataColumns))
					{
					MessageBox.Show("Invalid number of data columns");
					return;
					}
				break;
			}

		DialogResult = DialogResult.OK;
		return;
		}
	}
}
