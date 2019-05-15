/////////////////////////////////////////////////////////////////////
//
//	PDF417 Barcode Encoder
//
//	Save Pdf417 barcode image
//
//	Author: Uzi Granot
//	Version: 1.0
//	Date: April 1, 2019
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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Pdf417EncoderLibrary;

namespace Pdf417EncoderDemo
{
/// <summary>
/// Error sopt control
/// </summary>
public enum ErrorSpotControl
	{
	None,
	White,
	Black,
	Alternate,
	}

/// <summary>
/// Save barcode as an image file
/// </summary>
public partial class SaveImage : Form
	{
	private Pdf417Encoder Pdf417Encoder;
	private Bitmap Pdf417BitmapImage;
	private Bitmap BGImageBitmap;

	// Image file format
	private static readonly ImageFormat[] FileFormat = {ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Bmp, ImageFormat.Gif};

	/// <summary>
	/// SaveImage constructor
	/// </summary>
	/// <param name="Pdf417Encoder">Pdf417Encoder class</param>
	/// <param name="Pdf417BitmapImage">Barcode Bitmap image</param>
	public SaveImage
			(
			Pdf417Encoder Pdf417Encoder,
			Bitmap Pdf417BitmapImage
			)
		{
		// save arguments
		this.Pdf417Encoder = Pdf417Encoder;
		this.Pdf417BitmapImage = Pdf417BitmapImage;

		// initialize components
		InitializeComponent();
		return;
		}

	// initialization
	private void OnLoad(object sender, EventArgs e)
		{
		// display initial values
		ModulesWidthLabel.Text = Pdf417Encoder.BarColumns.ToString();
		ModulesHeightLabel.Text = Pdf417Encoder.DataRows.ToString();
		ModuleWidthLabel.Text = Pdf417Encoder.NarrowBarWidth.ToString();
		ModuleHeightLabel.Text = Pdf417Encoder.RowHeight.ToString();
		QuietZoneLabel.Text = Pdf417Encoder.QuietZone.ToString();
		ImageWidthLabel.Text = Pdf417Encoder.ImageWidth.ToString();
		ImageHeightLabel.Text = Pdf417Encoder.ImageHeight.ToString();
		int ImageSide = (int) (1.1 * Math.Sqrt(Pdf417Encoder.ImageWidth * Pdf417Encoder.ImageWidth +
			Pdf417Encoder.ImageHeight * Pdf417Encoder.ImageHeight));
		BrushWidthTextBox.Text = ImageSide.ToString();
		BrushHeightTextBox.Text = ImageSide.ToString();
		CameraDistanceTextBox.Text = (2 * ImageSide).ToString();

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
		ErrorDiameterTextBox.Text = (2 * Pdf417Encoder.NarrowBarWidth).ToString();
		ErrorNumberTextBox.Text = "10";
		return;
		}

	// image file format
	private void OnImageFileFormat(object sender, ListControlConvertEventArgs e)
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
			BarcodePosXTextBox.Text = (BrushWidth / 2).ToString();
			BarcodePosYTextBox.Text = (BrushHeight / 2).ToString();
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

	// enable/disable controls
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
		BarcodePosXTextBox.Enabled = !NoneRadioButton.Checked;
		BarcodePosYTextBox.Enabled = !NoneRadioButton.Checked;
		ImageRotationTextBox.Enabled = !NoneRadioButton.Checked;
		CameraDistanceTextBox.Enabled = !NoneRadioButton.Checked;
		CameraViewRotationTextBox.Enabled = !NoneRadioButton.Checked;
		return;
		}

	// browse for background image
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
			BarcodePosXTextBox.Text = (BGImageBitmap.Width / 2).ToString();
			BarcodePosYTextBox.Text = (BGImageBitmap.Height / 2).ToString();;
			}
		else
			{
			BGImageBitmap = null;
			ImageBGWidthLabel.Text = string.Empty;
			ImageBGHeightLabel.Text = string.Empty;
			BarcodePosXTextBox.Text = string.Empty;
			BarcodePosYTextBox.Text = string.Empty;
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

	// save image
	private void OnSaveClick(object sender, EventArgs e)
		{
		// reset some variables
		int CameraDistance = 0;
		int CameraRotation = 0;
		int OutputImageWidth = 0;
		int OutputImageHeight = 0;
		int Pdf417PosX = 0;
		int Pdf417PosY = 0;
		int ViewXRotation = 0;
		ErrorSpotControl ErrorControl = ErrorSpotControl.None;
		int ErrorDiameter = 0;
		int ErrorSpots = 0;

		// display barcode over image made with a brush
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

		// display barcode over an image
		if(ImageRadioButton.Checked)
			{
			// image must be defined
			if(BGImageBitmap == null)
				{
				MessageBox.Show("Background image must be defined");
				return;
				}

			OutputImageWidth = BGImageBitmap.Width;
			OutputImageHeight = BGImageBitmap.Height;
			}

		if(!NoneRadioButton.Checked)
			{
			// barcode position X
			if(!int.TryParse(BarcodePosXTextBox.Text.Trim(), out Pdf417PosX) || Pdf417PosX <= 0 || Pdf417PosX >= OutputImageWidth)
				{
				MessageBox.Show("Barcode position X must be within image width");
				return;
				}

			// barcode position Y
			if(!int.TryParse(BarcodePosYTextBox.Text.Trim(), out Pdf417PosY) || Pdf417PosY <= 0 || Pdf417PosY >= OutputImageHeight)
				{
				MessageBox.Show("Barcode position Y must be within image height");
				return;
				}

			// rotation
			if(!int.TryParse(ImageRotationTextBox.Text.Trim(), out CameraRotation) || CameraRotation < -360 || CameraRotation > 360)
				{
				MessageBox.Show("Rotation must be -360 to 360");
				return;
				}

			// camera distance
			if(!int.TryParse(CameraDistanceTextBox.Text.Trim(), out CameraDistance) || CameraDistance < 10 * Pdf417Encoder.NarrowBarWidth)
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
 
			int MaxSpotDiameter = Math.Min(OutputImageHeight, OutputImageWidth) / 8;
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

		// display barcode image by itself
		if(NoneRadioButton.Checked)
			{		
			OutputBitmap = Pdf417BitmapImage;
			}

		else
			{
			if(ImageRadioButton.Checked)
				{
				// normal case of plain barcode image
				OutputBitmap = new Bitmap(BGImageBitmap);
				}
			else
				{
				// create background area brush
				Brush AreaBrush = (int) HatchStyleComboBox.SelectedItem < 0 ? (Brush) new SolidBrush(BrushColorButton.BackColor) :
					(Brush) new HatchBrush((HatchStyle) ((int) HatchStyleComboBox.SelectedItem), Color.Black, BrushColorButton.BackColor);

				// create bitmap image object and and paint it with the brush
				OutputBitmap = new Bitmap(OutputImageWidth, OutputImageHeight);
				Graphics Graphics = Graphics.FromImage(OutputBitmap);
				Graphics.FillRectangle(AreaBrush, 0, 0, OutputImageWidth, OutputImageHeight);
				}

			if(ViewXRotation == 0)
				{
				OutputBitmap = CreateBarcodeImage(OutputBitmap, Pdf417PosX, Pdf417PosY, CameraRotation);
				}
			else
				{
				OutputBitmap = CreateBarcodeImage(OutputBitmap, Pdf417PosX, Pdf417PosY,
							CameraRotation, CameraDistance, ViewXRotation);
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
	private Bitmap CreateBarcodeImage
			(
			Bitmap OutputImage,
			int ImageCenterPosX,
			int ImageCenterPosY,
			double Rotation
			)
		{
		// Pdf417Matrix width and height
		int MatrixWidth = Pdf417Encoder.BarColumns;
		int MatrixHeight = Pdf417Encoder.DataRows;

		// barcode image width and height
		int ImageWidth = Pdf417Encoder.ImageWidth;
		int ImageHeight = Pdf417Encoder.ImageHeight;

		// transformation matrix
		Matrix Matrix = new Matrix();
		Matrix.Translate(ImageCenterPosX , ImageCenterPosY);
		if(Rotation != 0) Matrix.Rotate((float) Rotation);

		// create graphics object
		Graphics Graphics = Graphics.FromImage(OutputImage);

		// attach transformation matrix
		Graphics.Transform = Matrix;

		// image origin
		int XOffset = -ImageWidth / 2;
		int YOffset = -ImageHeight / 2;

		// set barcode area within background image to white
		Graphics.FillRectangle(Brushes.White, XOffset, YOffset, ImageWidth, ImageHeight);

		// one bar width and height
		int BarWidth = Pdf417Encoder.NarrowBarWidth;
		int BarHeight = Pdf417Encoder.RowHeight;

		// quiet zone 
		int QuietZone = Pdf417Encoder.QuietZone;

		// adjust offset
		XOffset += QuietZone;
		int SaveXOffet = XOffset;
		YOffset += QuietZone;

		// convert result matrix to output matrix
		for(int Row = 0; Row < MatrixHeight; Row++)
			{
			for(int Col = 0; Col < MatrixWidth; Col++)
				{
				// bar is black
				if(Pdf417Encoder.Pdf417BarcodeMatrix[Row, Col])
					{
					Graphics.FillRectangle(Brushes.Black, (float) XOffset, (float) YOffset, BarWidth, BarHeight);
					}

				// update horizontal offset
				XOffset += BarWidth;
				}

			// update vertical offset
			XOffset = SaveXOffet;
			YOffset += BarHeight;
			}
		return OutputImage;
		}

	// create perspective barcode image with background and rotation
	private Bitmap CreateBarcodeImage
			(
			Bitmap OutputImage,
			int ImageCenterPosX,
			int ImageCenterPosY,
			double Rotation,
			double CameraDistance,
			double ViewXRotation
			)
		{
		// Pdf417Matrix width and height
		int MatrixWidth = Pdf417Encoder.BarColumns;
		int MatrixHeight = Pdf417Encoder.DataRows;

		// barcode image width and height
		int ImageWidth = Pdf417Encoder.ImageWidth;
		int ImageHeight = Pdf417Encoder.ImageHeight;

		// create graphics object
		Graphics Graphics = Graphics.FromImage(OutputImage);

		// image origin
		int XOffset = -ImageWidth / 2;
		int YOffset = -ImageHeight / 2;

		// create perspective object
		Perspective Perspective = new Perspective(ImageCenterPosX, ImageCenterPosY, Rotation, CameraDistance, ViewXRotation);

		// polygon
		PointF[] Polygon = new PointF[4];
		Perspective.GetPolygon(XOffset, YOffset, ImageWidth, ImageHeight, Polygon);

		// clear the area for barcode
		Graphics.FillPolygon(Brushes.White, Polygon);

		// one bar width and height
		int BarWidth = Pdf417Encoder.NarrowBarWidth;
		int BarHeight = Pdf417Encoder.RowHeight;

		// quiet zone 
		int QuietZone = Pdf417Encoder.QuietZone;

		// adjust offset
		XOffset += QuietZone;
		int SaveXOffet = XOffset;
		YOffset += QuietZone;

		// convert result matrix to output matrix
		for(int Row = 0; Row < MatrixHeight; Row++)
			{
			for(int Col = 0; Col < MatrixWidth; Col++)
				{
				// bar is black
				if(Pdf417Encoder.Pdf417BarcodeMatrix[Row, Col])
					{
					Perspective.GetPolygon(XOffset, YOffset, BarWidth, BarHeight, Polygon);
					Graphics.FillPolygon(Brushes.Black, Polygon);
					}

				// update horizontal offset
				XOffset += BarWidth;
				}

			// update vertical offset
			XOffset = SaveXOffet;
			YOffset += BarHeight;
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

	// get file name from user
	private string SaveFileName()
		{
		// save file dialog box
		SaveFileDialog Dialog = new SaveFileDialog();
		Dialog.AddExtension = true;
		ImageFormat ImageFormat = (ImageFormat) ImageFormatComboBox.SelectedItem;
		Dialog.Filter = string.Format("{0} Image|*.{1}", ImageFormat.ToString(), ImageFormat.ToString().ToLower());

		Dialog.Title = "Save barcode Image";
		Dialog.InitialDirectory =  Directory.GetCurrentDirectory();
		Dialog.RestoreDirectory = true;
		Dialog.FileName = string.Format("Pdf417Barcode.{0}", ((ImageFormat) ImageFormatComboBox.SelectedItem).ToString().ToLower());
		if(Dialog.ShowDialog() == DialogResult.OK) return Dialog.FileName;
		return null;
		}

	// cancel
	private void OnCancelClick(object sender, EventArgs e)
		{
		DialogResult = DialogResult.Cancel;
		return;
		}
	}
}
