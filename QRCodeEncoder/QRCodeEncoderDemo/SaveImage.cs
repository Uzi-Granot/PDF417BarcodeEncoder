/////////////////////////////////////////////////////////////////////
//
//	QR Code Encoder Library
//
//	QR Code encoder save image screen.
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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace QRCodeEncoderDemo
{
public enum ErrorSpotControl
	{
	None,
	White,
	Black,
	Alternate,
	}

/// <summary>
/// Save QR code as an image file
/// </summary>
public partial class SaveImage : Form
	{
	private QREncoder QREncoder;
	private Bitmap QRCodeBitmapImage;
	private Bitmap BGImageBitmap;

	/// <summary>
	/// Image file format
	/// </summary>
	public static readonly ImageFormat[] FileFormat = {ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Bmp, ImageFormat.Gif};

	public SaveImage
			(
			QREncoder QREncoder,
			Bitmap QRCodeBitmapImage
			)
		{
		// save arguments
		this.QREncoder = QREncoder;
		this.QRCodeBitmapImage = QRCodeBitmapImage;

		// initialize component
		InitializeComponent();
		return;
		}

	private void OnLoad(object sender, EventArgs e)
		{
		// display initial values
		QRCodeDimensionLabel.Text = QREncoder.QRCodeDimension.ToString();
		ModuleSizeLabel.Text = QREncoder.ModuleSize.ToString();
		QuietZoneLabel.Text = QREncoder.QuietZone.ToString();
		QRImageDimensionLabel.Text = QREncoder.QRCodeImageDimension.ToString();
		int BGImageSide = (int) (1.6 * QREncoder.QRCodeImageDimension);
		BrushWidthTextBox.Text = BGImageSide.ToString();
		BrushHeightTextBox.Text = BGImageSide.ToString();
		CameraDistanceTextBox.Text = (2 * BGImageSide).ToString();

		// load image file type combo box
		foreach(ImageFormat ImageFormat in FileFormat) ImageFormatComboBox.Items.Add(ImageFormat);
		ImageFormatComboBox.SelectedIndex = 0;

		// load hatch style combo box
		for(int Index = -1; Index < 53; Index++) HatchStyleComboBox.Items.Add(Index);
		HatchStyleComboBox.SelectedIndex = 0;

		BrushColorButton.BackColor = Color.LightSkyBlue;
		ImageRotationTextBox.Text = "0";
		CameraViewRotationTextBox.Text = "0";
		CameraViewRotationTextBox.Text = "0";

		// set none radio button
		NoneRadioButton.Checked = true;

		// set none error radio button
		ErrorNoneRadioButton.Checked = true;
		ErrorDiameterTextBox.Text = (QREncoder.ModuleSize * 2).ToString();
		ErrorNumberTextBox.Text = "10";
		return;
		}

	private void OnImageFileFormat
			(
			object sender,
			ListControlConvertEventArgs e
			)
		{
		ImageFormat Format = (ImageFormat) e.ListItem;
		e.Value = string.Format("{0} (*.{1})", Format.ToString(), Format.ToString().ToLower());
		return;
		}

	// brush hatch style format
	private void OnHatchStyleFormat(object sender, ListControlConvertEventArgs e)
		{
		int Item = (int) e.ListItem;
		e.Value = (int) e.ListItem < 0 ? "Solid" : ((HatchStyle) e.ListItem).ToString();
		return;
		}

	// user changed brush background area size
	private void OnBrushSizeChanged(object sender, EventArgs e)
		{
		if(int.TryParse(BrushWidthTextBox.Text, out int BrushWidth) && BrushWidth > 0 && BrushWidth <= 10000 &&
			int.TryParse(BrushHeightTextBox.Text, out int BrushHeight) && BrushHeight > 0 && BrushHeight <= 10000)
			{
			QRCodePosXTextBox.Text = (BrushWidth / 2).ToString();
			QRCodePosYTextBox.Text = (BrushHeight / 2).ToString();
			}
		return;
		}

	// background radio button checked
	private void OnBackgroundRadioButton(object sender, EventArgs e)
		{
		// radio button changed from off to on
		if(((RadioButton) sender).Checked)
			{
			// enable/disable controls
			EnableControls();
			}
		return;
		}

	private void EnableControls()
		{
		// background image
		ImageBrowseButton.Enabled = ImageRadioButton.Checked;
		ImageFileNameTextBox.Enabled = ImageRadioButton.Checked;

		// background brush
		BrushColorButton.Enabled = BrushRadioButton.Checked;
		HatchStyleComboBox.Enabled = BrushRadioButton.Checked;
		BrushWidthTextBox.Enabled = BrushRadioButton.Checked;
		BrushHeightTextBox.Enabled = BrushRadioButton.Checked;

		// background image or brush
		QRCodePosXTextBox.Enabled = !NoneRadioButton.Checked;
		QRCodePosYTextBox.Enabled = !NoneRadioButton.Checked;
		ImageRotationTextBox.Enabled = !NoneRadioButton.Checked;
		CameraDistanceTextBox.Enabled = !NoneRadioButton.Checked;
		CameraViewRotationTextBox.Enabled = !NoneRadioButton.Checked;
		return;
		}

	private void OnImageBrowse(object sender, EventArgs e)
		{
		// open file dialog box
		OpenFileDialog Dialog = new OpenFileDialog();
		Dialog.Filter = "Image Files(*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF)|*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF|All files (*.*)|*.*";
		Dialog.Title = "Load Background Image";
		Dialog.InitialDirectory =  Directory.GetCurrentDirectory();
		Dialog.RestoreDirectory = true;
		if(Dialog.ShowDialog() == DialogResult.OK) ImageFileNameTextBox.Text = Dialog.FileName;
		return;
		}

	// background image file name changed
	private void ImageFileNameChanged(object sender, EventArgs e)
		{
		string FileName = ImageFileNameTextBox.Text.Trim();
		if(File.Exists(FileName))
			{
			BGImageBitmap = new Bitmap(FileName);
			ImageBGWidthLabel.Text = BGImageBitmap.Width.ToString();
			ImageBGHeightLabel.Text = BGImageBitmap.Height.ToString();
			QRCodePosXTextBox.Text = (BGImageBitmap.Width / 2).ToString();
			QRCodePosYTextBox.Text = (BGImageBitmap.Height / 2).ToString();;
			}
		else
			{
			BGImageBitmap = null;
			ImageBGWidthLabel.Text = string.Empty;
			ImageBGHeightLabel.Text = string.Empty;
			QRCodePosXTextBox.Text = string.Empty;
			QRCodePosYTextBox.Text = string.Empty;
			}
		return;
		}

	// select color
	private void OnSelectColor(object sender, EventArgs e)
		{
		ColorDialog Dialog = new ColorDialog();
		Dialog.FullOpen = true;
		if(Dialog.ShowDialog(this) == DialogResult.OK) BrushColorButton.BackColor = Dialog.Color;
		return;
		}

	// add error spots
	private void OnErrorRadioButton(object sender, EventArgs e)
		{
		// radio button changed from off to on
		if(((RadioButton) sender).Checked)
			{
			// enable/disable controls
			ErrorDiameterTextBox.Enabled = !ErrorNoneRadioButton.Checked;
			ErrorNumberTextBox.Enabled = !ErrorNoneRadioButton.Checked;
			}
		return;
		}

	private void OnSaveClick(object sender, EventArgs e)
		{
		// reset some variables
		int CameraDistance = 0;
		int CameraRotation = 0;
		int OutputImageWidth = 0;
		int OutputImageHeight = 0;
		int QRCodePosX = 0;
		int QRCodePosY = 0;
		int ViewXRotation = 0;
		ErrorSpotControl ErrorControl = ErrorSpotControl.None;
		int ErrorDiameter = 0;
		int ErrorSpots = 0;

		// display qr code over image made with a brush
		if(BrushRadioButton.Checked)
			{
			// area width
			if(!int.TryParse(BrushWidthTextBox.Text.Trim(), out OutputImageWidth) ||
				OutputImageWidth <= 0 || OutputImageWidth >= 100000)
				{
				MessageBox.Show("Brush background width is invalid");
				return;
				}

			// area width
			if(!int.TryParse(BrushHeightTextBox.Text.Trim(), out OutputImageHeight) ||
				OutputImageHeight <= 0 || OutputImageHeight >= 100000)
				{
				MessageBox.Show("Brush background height is invalid");
				return;
				}
			}

		// display qr code over an image
		if(ImageRadioButton.Checked)
			{
			// image must be defined
			if(this.BGImageBitmap == null)
				{
					MessageBox.Show("Background image must be defined");
				return;
				}

			OutputImageWidth = this.BGImageBitmap.Width;
			OutputImageHeight = this.BGImageBitmap.Height;
			}

		if(!NoneRadioButton.Checked)
			{
			// QR code position X
			if(!int.TryParse(QRCodePosXTextBox.Text.Trim(), out QRCodePosX) || QRCodePosX <= 0 || QRCodePosX >= OutputImageWidth)
				{
				MessageBox.Show("QR code position X must be within image width");
				return;
				}

			// QR code position Y
			if(!int.TryParse(QRCodePosYTextBox.Text.Trim(), out QRCodePosY) || QRCodePosY <= 0 || QRCodePosY >= OutputImageHeight)
				{
				MessageBox.Show("QR code position Y must be within image height");
				return;
				}

			// rotation
			if(!int.TryParse(ImageRotationTextBox.Text.Trim(), out CameraRotation) || CameraRotation < -360 || CameraRotation > 360)
				{
				MessageBox.Show("Rotation must be -360 to 360");
				return;
				}

			// camera distance
			if(!int.TryParse(CameraDistanceTextBox.Text.Trim(), out CameraDistance) || CameraDistance < 10 * QREncoder.ModuleSize)
				{
				MessageBox.Show("Camera distance is invalid");
				return;
				}

			// Axis X Rotation
			if(!int.TryParse(CameraViewRotationTextBox.Text.Trim(), out ViewXRotation) || ViewXRotation > 160 || ViewXRotation < -160)
				{
				MessageBox.Show("View X rotation invalid");
				return;
				}
			}

		// error
		if(!ErrorNoneRadioButton.Checked)
			{
			if(ErrorWhiteRadioButton.Checked) ErrorControl = ErrorSpotControl.White;
			else if(ErrorBlackRadioButton.Checked) ErrorControl = ErrorSpotControl.Black;
			else ErrorControl = ErrorSpotControl.Alternate;
 
			int MaxSpotDiameter = QREncoder.QRCodeImageDimension / 8;
			if(!int.TryParse(ErrorDiameterTextBox.Text.Trim(), out ErrorDiameter) ||
				ErrorDiameter <= 0 || ErrorDiameter > MaxSpotDiameter)
				{
				MessageBox.Show("Error diameter is invalid");
				return;
				}

			if(!int.TryParse(ErrorNumberTextBox.Text.Trim(), out ErrorSpots) ||
				ErrorSpots <= 0 || ErrorSpots > 100)
				{
				MessageBox.Show("Number of error spots is invalid");
				return;
				}
			}

		// get file name
		string FileName = SaveFileName();
		if(FileName == null) return;

		// output bitmap
		Bitmap OutputBitmap;

		// display QR Code image by itself
		if(NoneRadioButton.Checked)
			{		
			OutputBitmap = QRCodeBitmapImage; // QREncoder.CreateQRCodeBitmap();
			}

		else
			{
			if(ImageRadioButton.Checked)
				{
				OutputBitmap = new Bitmap(this.BGImageBitmap);
				}
			else
				{
				// create area brush
				Brush AreaBrush = (int) HatchStyleComboBox.SelectedItem < 0 ? (Brush) new SolidBrush(BrushColorButton.BackColor) :
					(Brush) new HatchBrush((HatchStyle) ((int) HatchStyleComboBox.SelectedItem), Color.Black, BrushColorButton.BackColor);

				// create picture object and and paint it with the brush
				OutputBitmap = new Bitmap(OutputImageWidth, OutputImageHeight);
				Graphics Graphics = Graphics.FromImage(OutputBitmap);
				Graphics.FillRectangle(AreaBrush, 0, 0, OutputImageWidth, OutputImageHeight);
				}

			if(ViewXRotation == 0)
				{
				OutputBitmap = CreateQRCodeImage(OutputBitmap, QRCodePosX, QRCodePosY, CameraRotation);
				}
			else
				{
				OutputBitmap = CreateQRCodeImage(OutputBitmap, QRCodePosX, QRCodePosY, CameraRotation, CameraDistance, ViewXRotation);
				}
			}

		// Error spots
		if(ErrorControl != ErrorSpotControl.None)
			{
			AddErrorSpots(OutputBitmap, ErrorControl, ErrorDiameter, ErrorSpots);
			}

		// save image
		FileStream FS = new FileStream(FileName, FileMode.Create);
		OutputBitmap.Save(FS, (ImageFormat) ImageFormatComboBox.SelectedItem);
		FS.Close();

		// start image editor
		Process.Start(FileName);
		return;
		}

	// create barcode image with background and rotation
	private Bitmap CreateQRCodeImage
			(
			Bitmap OutputImage,
			int ImageCenterPosX,
			int ImageCenterPosY,
			double Rotation
			)
		{
		// transformation matrix
		Matrix Matrix = new Matrix();
		Matrix.Translate(ImageCenterPosX , ImageCenterPosY);
		if(Rotation != 0) Matrix.Rotate((float) Rotation);

		// create graphics object
		Graphics Graphics = Graphics.FromImage(OutputImage);

		// attach transformation matrix
		Graphics.Transform = Matrix;

		// image origin
		int ImageDimension = QREncoder.QRCodeImageDimension;
		int XOffset = -ImageDimension / 2;
		int YOffset = XOffset;

		// set barcode area within background image to white
		Graphics.FillRectangle(Brushes.White, XOffset, YOffset, ImageDimension, ImageDimension);

		// one bar width and height
		int ModuleSize = QREncoder.ModuleSize;

		// quiet zone 
		int QuietZone = QREncoder.QuietZone;

		// adjust offset
		XOffset += QuietZone;
		int SaveXOffet = XOffset;
		YOffset += QuietZone;

		// barcode image width and height
		int MatrixDimension = QREncoder.QRCodeDimension;

		// convert result matrix to output matrix
		for(int Row = 0; Row < MatrixDimension; Row++)
			{
			for(int Col = 0; Col < MatrixDimension; Col++)
				{
				// bar is black
				if(QREncoder.QRCodeMatrix[Row, Col])
					{
					Graphics.FillRectangle(Brushes.Black, (float) XOffset, (float) YOffset, ModuleSize, ModuleSize);
					}

				// update horizontal offset
				XOffset += ModuleSize;
				}

			// update vertical offset
			XOffset = SaveXOffet;
			YOffset += ModuleSize;
			}
		return OutputImage;
		}

	// create perspective barcode image with background and rotation
	private Bitmap CreateQRCodeImage
			(
			Bitmap OutputImage,
			int ImageCenterPosX,
			int ImageCenterPosY,
			double Rotation,
			double CameraDistance,
			double ViewXRotation
			)
		{
		// create graphics object
		Graphics Graphics = Graphics.FromImage(OutputImage);

		// create perspective object
		Perspective Perspective = new Perspective(ImageCenterPosX, ImageCenterPosY, Rotation, CameraDistance, ViewXRotation);

		// image origin
		int ImageDimension = QREncoder.QRCodeImageDimension;
		int XOffset = -ImageDimension / 2;
		int YOffset = -XOffset;

		// polygon
		PointF[] Polygon = new PointF[4];
		Perspective.GetPolygon(XOffset, YOffset, ImageDimension, ImageDimension, Polygon);

		// clear the area for barcode
		Graphics.FillPolygon(Brushes.White, Polygon);

		// one bar width and height
		int ModuleSize = QREncoder.ModuleSize;

		// quiet zone 
		int QuietZone = QREncoder.QuietZone;

		// adjust offset
		XOffset += QuietZone;
		int SaveXOffet = XOffset;
		YOffset += QuietZone;

		// convert result matrix to output matrix
		int MatrixDimension = QREncoder.QRCodeDimension;
		for(int Row = 0; Row < MatrixDimension; Row++)
			{
			for(int Col = 0; Col < MatrixDimension; Col++)
				{
				// bar is black
				if(QREncoder.QRCodeMatrix[Row, Col])
					{
					Perspective.GetPolygon(XOffset, YOffset, ModuleSize, ModuleSize, Polygon);
					Graphics.FillPolygon(Brushes.Black, Polygon);
					}

				// update horizontal offset
				XOffset += ModuleSize;
				}

			// update vertical offset
			XOffset = SaveXOffet;
			YOffset += ModuleSize;
			}
		return OutputImage;
		}

	// Add error spots for testing
	private static void AddErrorSpots
			(
			Bitmap ImageBitmap,
			ErrorSpotControl ErrorControl,
			double ErrorDiameter,
			double ErrorSpotsCount
			)
		{
		// random number generator
		Random RandNum = new Random();

		// create graphics object
		Graphics Graphics = Graphics.FromImage(ImageBitmap);

		double XRange = ImageBitmap.Width - ErrorDiameter - 4;
		double YRange = ImageBitmap.Height - ErrorDiameter - 4;
		Brush SpotBrush = ErrorControl == ErrorSpotControl.Black ? Brushes.Black : Brushes.White;

		for(int Index = 0; Index < ErrorSpotsCount; Index++)
			{
			double XPos = RandNum.NextDouble() * XRange;
			double YPos = RandNum.NextDouble() * YRange;
			if(ErrorControl == ErrorSpotControl.Alternate) SpotBrush = (Index & 1) == 0 ? Brushes.White : Brushes.Black;
			Graphics.FillEllipse(SpotBrush, (float) XPos, (float) YPos, (float) ErrorDiameter, (float) ErrorDiameter);
			}
		return;
		}


	private string SaveFileName()
		{
		// save file dialog box
		SaveFileDialog Dialog = new SaveFileDialog();
		Dialog.AddExtension = true;
		ImageFormat Format = (ImageFormat) ImageFormatComboBox.SelectedItem;
		Dialog.Filter = string.Format("{0} Image|*.{1}", Format.ToString(), Format.ToString().ToLower());
		Dialog.Title = "Save QR Code Image";
		Dialog.InitialDirectory =  Directory.GetCurrentDirectory();
		Dialog.RestoreDirectory = true;
		Dialog.FileName = string.Format("QRCode{0}{1}.{2}",
			QREncoder.ModuleSize.ToString(), QREncoder.ErrorCorrection.ToString(), Format.ToString().ToLower());
		if(Dialog.ShowDialog() == DialogResult.OK) return Dialog.FileName;
		return null;
		}

	private void OnCancelClick(object sender, EventArgs e)
		{
		DialogResult = DialogResult.Cancel;
		return;
		}
	}
}
