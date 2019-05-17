/////////////////////////////////////////////////////////////////////
//
//	QR Code Encoder Library
//
//	QR Code Encoder demo/test application.
//
//	Author: Uzi Granot
//	Original Version: 1.0
//	Date: June 30, 2018
//	Copyright (C) 2018-2019 Uzi Granot. All Rights Reserved
//	For full version history please look at QREncode.cs
//
//	QR Code Library C# class library and the attached test/demo
//  applications are free software.
//	Software developed by this author is licensed under CPOL 1.02.
//	Some portions of the QRCodeVideoDecoder are licensed under GNU Lesser
//	General Public License v3.0.
//
//	The solution is made of 3 projects:
//	1. QRCodeEncoderLibrary: QR code encoding.
//	2. QRCodeEncoderDemo: Create QR Code images.
//	3. QRCodeConsoleDemo: Demo app for net standard
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
/////////////////////////////////////////////////////////////////////

using QRCodeEncoderLibrary;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QRCodeEncoderDemo
{
/// <summary>
/// Test QR Code Encoder
/// </summary>
public partial class QRCodeEncoderDemo : Form
	{
	private QREncoder QRCodeEncoder;
	private Bitmap QRCodeImage;
	private Rectangle QRCodeImageArea = new Rectangle();

	/// <summary>
	/// Constructor
	/// </summary>
	public QRCodeEncoderDemo()
		{
		InitializeComponent();
		return;
		}

	/// <summary>
	/// Test program initialization
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event arguments</param>
	private void OnLoad(object sender, EventArgs e)
		{
		// program title
		Text = "QRCodeEncoderDemo - " + QREncoder.VersionNumber + " \u00a9 2018-2019 Uzi Granot. All rights reserved.";

		#if DEBUG
		// current directory
		string CurDir = Environment.CurrentDirectory;
		string WorkDir = CurDir.Replace("bin\\Debug", "Work");
		if(WorkDir != CurDir && Directory.Exists(WorkDir)) Environment.CurrentDirectory = WorkDir;
		#endif

		// create encoder object
		QRCodeEncoder = new QREncoder();

		// load error correction combo box
		ErrorCorrectionComboBox.Items.Add("L (7%)");
		ErrorCorrectionComboBox.Items.Add("M (15%)");
		ErrorCorrectionComboBox.Items.Add("Q (25%)");
		ErrorCorrectionComboBox.Items.Add("H (30%)");
		ErrorCorrectionComboBox.SelectedIndex = 1;

		ModuleSizeTextBox.Text = "4";
		QuietZoneTextBox.Text = "16";

		// set initial screen
		SetScreen();

		// force resize
		OnResize(sender, e);
		return;
		}

	/// <summary>
	/// Create QR Code image
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event arguments</param>
	private void OnEncode(object sender, EventArgs e)
		{
		// get error correction code
		ErrorCorrection ErrorCorrection = (ErrorCorrection) ErrorCorrectionComboBox.SelectedIndex;

		// get module size
		string ModuleStr = ModuleSizeTextBox.Text.Trim();
		if(!int.TryParse(ModuleStr, out int ModuleSize) || ModuleSize < 1 || ModuleSize > 100)
			{
			MessageBox.Show("Module size error.");
			return;
			}

		// get quiet zone
		string QuietStr = QuietZoneTextBox.Text.Trim();
		if(!int.TryParse(QuietStr, out int QuietZone) || QuietZone < 1 || QuietZone > 100)
			{
			MessageBox.Show("Quiet zone error.");
			return;
			}

		// get data for QR Code
		string Data = DataTextBox.Text.Trim();
		if(Data.Length == 0)
			{
			MessageBox.Show("Data must not be empty.");
			return;
			}

		// disable buttons
		EnableButtons(false);

		try
			{
			QRCodeEncoder.ErrorCorrection = ErrorCorrection;
			QRCodeEncoder.ModuleSize = ModuleSize;
			QRCodeEncoder.QuietZone = QuietZone;

			// multi segment
			if(SeparatorCheckBox.Checked && Data.IndexOf('|') >= 0)
				{
				string[] Segments = Data.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

				// encode data
				QRCodeEncoder.Encode(Segments);
				}

			// single segment
			else
				{
				// encode data
				QRCodeEncoder.Encode(Data);
				}

			// create bitmap
			QRCodeImage = QRCodeEncoder.CreateQRCodeBitmap();
			}

		catch (Exception Ex)
			{
			MessageBox.Show("Encoding exception.\r\n" + Ex.Message);
			}

		// enable buttons
		EnableButtons(true);

		// repaint panel
		Invalidate();
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// save barcode image
	/////////////////////////////////////////////////////////////////////

	private void OnSavePNG(object sender, EventArgs e)
		{
		// save file dialog box
		SaveFileDialog Dialog = new SaveFileDialog();
		Dialog.DefaultExt = ".png";
		Dialog.AddExtension = true;
		Dialog.Filter = "Png image files (*.png)|*.png";
		Dialog.Title = "Save barcode in PNG format";
		Dialog.InitialDirectory =  Directory.GetCurrentDirectory();
		Dialog.RestoreDirectory = true;
		Dialog.FileName = "QRCodePNGImage.png";
		if(Dialog.ShowDialog() != DialogResult.OK) return;

		// save image as png file
		QRCodeEncoder.SaveQRCodeToPngFile(Dialog.FileName);

		// start image editor
		Process.Start(Dialog.FileName);
		return;
		}

	/// <summary>
	/// Save image
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event arguments</param>
	private void OnSaveImage(object sender, EventArgs e)
        {
		// save QR code image screen
		if(QRCodeImage != null)
			{
			SaveImage Dialog = new SaveImage(QRCodeEncoder, QRCodeImage);
			Dialog.ShowDialog(this);
			}
		return;
		}

	/// <summary>
	/// Restore program defaults
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event arguments</param>
	private void OnDefault(object sender, EventArgs e)
		{
		QRCodeImage = null;
		SetScreen();
		Invalidate();
		return;
		}

	/// <summary>
	/// Paint QR Code image
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event arguments</param>
	private void OnPaint(object sender, PaintEventArgs e)
		{
		// no image
		if(QRCodeImage == null) return;

		// calculate image area width and height to preserve aspect ratio
		Rectangle ImageRect = new Rectangle
			{
			Height = (QRCodeImageArea.Width * QRCodeImage.Height) / QRCodeImage.Width
			};
		if(ImageRect.Height <= QRCodeImageArea.Height)
			{
			ImageRect.Width = QRCodeImageArea.Width;
			}
		else
			{
			ImageRect.Width = (QRCodeImageArea.Height * QRCodeImage.Width) / QRCodeImage.Height;
			ImageRect.Height = QRCodeImageArea.Height;
			}

		// calculate position
		ImageRect.X = QRCodeImageArea.X + (QRCodeImageArea.Width - ImageRect.Width) / 2;
		ImageRect.Y = QRCodeImageArea.Y + (QRCodeImageArea.Height - ImageRect.Height) / 2;
		e.Graphics.DrawImage(QRCodeImage, ImageRect);
		return;
		}

	/// <summary>
	/// Enabled buttons
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event arguments</param>
	private void EnableButtons
			(
			bool Enabled
			)
		{
		EncodeButton.Enabled = Enabled;
		SaveToPngButton.Enabled = QRCodeImage != null && Enabled;
		SaveImageButton.Enabled = QRCodeImage != null && Enabled;
		DefaultButton.Enabled = Enabled;
		return;
		}

	/// <summary>
	/// Set screen based on program state
	/// </summary>
	private void SetScreen()
		{
		DataTextBox.Text = Text + "\r\n" +
				"Code Project article QR Code Encoder and Decoder .NET Class Library Written in C#\r\n" +
				"https://www.codeproject.com/Articles/1250071/QR-Code-Encoder-and-Decoder-NET-class-library-writ";
		EnableButtons(true);
		return;
		}

	/// <summary>
	/// Resize frame
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event arguments</param>
	private void OnResize(object sender, EventArgs e)
		{
		if(ClientSize.Width == 0) return;

		// center header label
		HeaderLabel.Left = (ClientSize.Width - HeaderLabel.Width) / 2;

		// put data text box at the bottom of client area
		DataTextBox.Top = ClientSize.Height - DataTextBox.Height - 8;
		DataTextBox.Width = ClientSize.Width - 2 * DataTextBox.Left;

		// put data label above text box
		DataLabel.Top = DataTextBox.Top - DataLabel.Height - 3;

		// put separator check box above and to the right of the text box
		SeparatorCheckBox.Top = DataTextBox.Top - SeparatorCheckBox.Height - 3;
		SeparatorCheckBox.Left = DataTextBox.Right - SeparatorCheckBox.Width;

		// put buttons half way between header and data text
		ButtonsGroupBox.Top = (DataLabel.Top + HeaderLabel.Top - ButtonsGroupBox.Height) / 2;

		// image area
		QRCodeImageArea.X = ButtonsGroupBox.Right + 4;
		QRCodeImageArea.Y = HeaderLabel.Bottom + 4;
		QRCodeImageArea.Width = ClientSize.Width - QRCodeImageArea.X - 4;
		QRCodeImageArea.Height = DataLabel.Top - QRCodeImageArea.Y - 4;

		// force re-paint
		Invalidate();
		return;
		}
	}
}
