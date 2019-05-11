/////////////////////////////////////////////////////////////////////
//
//	PDF417 Barcode Encoder
//
//	Pdf417EncoderDemo class
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
using System.IO;
using System.Windows.Forms;
using Pdf417EncoderLibrary;

namespace Pdf417EncoderDemo
{
/// <summary>
/// PDF417 Encoder demo class
/// </summary>
public partial class Pdf417EncoderDemo : Form
	{
	private Pdf417Encoder Encoder;
	private Bitmap Pdf417Bitmap;
	private Rectangle BarcodeImageArea = new Rectangle();

	private static readonly string[] EncodingControlText =
		{
		"Auto",
		"Byte only",
		"No numeric",
		};

	private static readonly string[] ErrorCorrLevelText =
		{
		"Level 0",
		"Level 1",
		"Level 2",
		"Level 3",
		"Level 4",
		"Level 5",
		"Level 6",
		"Level 7",
		"Level 8",
		"Auto Low",
		"Auto Normal",
		"Auto Medium",
		"Auto High"
		};

	private static readonly string[] CharacterSetText =
		{
		"1 Western European",
		"2 Central European",
		"3 Latin",	
		"4 Baltic",
		"5 Cyrillic",
		"6 Arabic",
		"7 Greek",
		"8 Hebrew",
		"9 Turkish",
		"13 Estonian",
		"15 French",	
		};

	private static readonly string[] SamplesText =
		{
		"Program Name",
		"English",
		"Greek",
		"Hebrew",
		"French",
		"Numeric",
		"Punctuation",
		};

	private static string[] Samples;

	private static readonly string English = "PDF417 is a stacked linear barcode symbol format used in a variety of applications; primarily transport, identification \r\n" +
					"cards, and inventory management. PDF stands for Portable Data File. The 417 signifies that each pattern in the code \r\n" +
					"consists of bars and spaces, and that each pattern is 17 units long. The PDF417 symbology was invented by \r\n" +
					"Dr. Ynjiun P. Wang at Symbol Technologies in 1991. (Wang 1993) It is ISO standard 15438.";

	private static readonly string Greek = "Το PDF417 είναι ένα σετ γραμμικής μορφής συμβόλου γραμμικού κώδικα που χρησιμοποιείται σε \r\n" + 
					"διάφορες εφαρμογές. κυρίως μεταφορές, κάρτες αναγνώρισης και διαχείριση αποθεμάτων. Το PDF αντιπροσωπεύει το Φορητό \r\n" +
					"Αρχείο Δεδομένων. Το 417 δηλώνει ότι κάθε μοτίβο στον κώδικα αποτελείται από ράβδους και διαστήματα και ότι κάθε \r\n" +
					"μοτίβο έχει μήκος 17 μονάδων. Η συμβολολογία PDF417 επινοήθηκε από τον Dr. Ynjiun P. Wang στη Symbol Technologies \r\n" +
					"το 1991. (Wang 1993) Είναι το πρότυπο ISO 15438.";

	private static readonly string Hebrew = ".יצרנים של ציוד ברקוד ומשתמשים בטכנולוגיה הברקוד דורשים הציבור זמין מפרטים סימבולוגיה סטנדרטית שאליה הם יכולים להתייחס בעת פיתוח ציוד סטנדרטים היישום. הכוונה וההבנה של כי הסימבולוגיה המוצגת בתקן בינלאומי זה נמצאת כולה ברשות הציבור וללא כל הגבלות המשתמש, הרישיונות והאגרות";

	private static readonly string French = "PDF417 est un format de symbole de code à barres linéaire empilé utilisé dans diverses applications. principalement \r\n" +
					"les transports, les cartes d’identité et la gestion des stocks. PDF est l'acronyme de Portable Data File. Le code 417 \r\n" +
					"signifie que chaque modèle du code est composé de barres et d'espaces et qu'il comporte 17 unités de long. La symbologie \r\n" +
					"PDF417 a été inventée par le Dr Ynjiun P. Wang chez Symbol Technologies en 1991. (Wang 1993) \r\n" +
					"Il s'agit de la norme ISO 15438.";
	
	private static readonly string Numeric = "0123456789012345678909876543210987654321001234567890123456789098765432109876543210";

	//private static readonly string Croation = "Općina KRŠAN\r\nBlaškovići 12\r\n52232 K R Š A N\r\nšđčćž\r\nŠĐČĆŽ";
	private static readonly string Punctuation = "Abc, efg 99 > 88\r\nhij [~~~~~] klmnop\r\n0123@456789012345!!!!!!]";

	/// <summary>
	/// Pdf417EncoderDemo constructor
	/// </summary>
	public Pdf417EncoderDemo()
		{
		InitializeComponent();
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// initialization
	/////////////////////////////////////////////////////////////////////

	private void OnLoad(object sender, EventArgs e)
		{
		#if DEBUG
		// current directory
		string CurDir = Environment.CurrentDirectory;
		string WorkDir = CurDir.Replace("bin\\Debug", "Work");
		if(WorkDir != CurDir && Directory.Exists(WorkDir)) Environment.CurrentDirectory = WorkDir;
		#endif

		// program title
		Text = "Pdf417EncoderDemo - " + Pdf417Encoder.VersionNumber + " \u00a9 2019 Uzi Granot. All rights reserved.";

		// create samples array
		Samples = new string[]
			{
			Text,
			English,
			Greek,
			Hebrew,
			French,
			Numeric,
			Punctuation,
			};

		// load encoding control combo box
		for(int Index = 0; Index < EncodingControlText.Length; Index++)
			{
			EncodingComboBox.Items.Add(EncodingControlText[Index]);
			}
		EncodingComboBox.SelectedIndex = 0;

		// load error correction combo box
		for(int Index = 0; Index < ErrorCorrLevelText.Length; Index++)
			{
			ErrorCorrectionComboBox.Items.Add(ErrorCorrLevelText[Index]);
			}
		ErrorCorrectionComboBox.SelectedIndex = 10;

		// load language ISO combo box
		for(int Index = 0; Index < CharacterSetText.Length; Index++)
			{
			CharacterSetComboBox.Items.Add(CharacterSetText[Index]);
			}
		CharacterSetComboBox.SelectedIndex = 0;

		// set narrow bar width
		BarWidthTextBox.Text = "4";

		// set row height
		RowHeightTextBox.Text = "12";

		// set quiet zone
		QuietZoneTextBox.Text = "8";

		// set data columns
		DefDataColTextBox.Text = "4";

		// load samples
		for(int Index = 0; Index < SamplesText.Length; Index++)
			{
			SamplesComboBox.Items.Add(SamplesText[Index]);
			}
		SamplesComboBox.SelectedIndex = 0;

		// create encoder object
		Encoder = new Pdf417Encoder();

		// enable buttons
		EnableButtons(true);

		// force resize
		OnResize(sender, e);
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// user changed sample's combo box selection
	/////////////////////////////////////////////////////////////////////

	private void SamplesChanged(object sender, EventArgs e)
		{
		// load initial data text box
		int Index = SamplesComboBox.SelectedIndex;
		DataTextBox.Text = Samples[Index];
		if(Index == 2) CharacterSetComboBox.SelectedIndex = 6;
		else if(Index == 3) CharacterSetComboBox.SelectedIndex = 7;
		else if(Index == 4) CharacterSetComboBox.SelectedIndex = 10;
		else CharacterSetComboBox.SelectedIndex = 0;
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// load text file to data text box
	/////////////////////////////////////////////////////////////////////

	private void OnLoadFile(object sender, EventArgs e)
		{
		// save file dialog box
		OpenFileDialog Dialog = new OpenFileDialog();
		Dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
		Dialog.Title = "Load file";
		Dialog.InitialDirectory =  Directory.GetCurrentDirectory();
		Dialog.RestoreDirectory = true;
		if(Dialog.ShowDialog() != DialogResult.OK) return;

		// read the contents of the file into a stream
		Stream TextStream = Dialog.OpenFile();
		string Text = null;
		using(StreamReader Reader = new StreamReader(TextStream))
            {
            Text = Reader.ReadToEnd();
            }
		if(Text == null) return;
		DataTextBox.Text = Text;
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// create pdf417 barcode image file
	/////////////////////////////////////////////////////////////////////

	private void OnEncode(object sender, EventArgs e)
		{
		// get bar width
		if(!int.TryParse(BarWidthTextBox.Text.Trim(), out int BarWidth) || BarWidth < 1 || BarWidth > 20)
			{
			MessageBox.Show("Narrow bar width must be 1 to 20 pixels");
			return;
			}

		// get row height
		if(!int.TryParse(RowHeightTextBox.Text.Trim(), out int RowHeight) || RowHeight < 3 * BarWidth || RowHeight > 60)
			{
			MessageBox.Show("Row height must be 3 times bar width or more in pixels");
			return;
			}

		// get quiet zone
		if(!int.TryParse(QuietZoneTextBox.Text.Trim(), out int QuietZone) || QuietZone < 2 * BarWidth || RowHeight > 40)
			{
			MessageBox.Show("Quiet zone must be 2 times bar width or more in pixels");
			return;
			}

		// get default data columns
		if(!int.TryParse(DefDataColTextBox.Text.Trim(), out int DefDataColumns) || DefDataColumns < 1 || DefDataColumns > 30)
			{
			MessageBox.Show("Number of data columns must be 1 to 30");
			return;
			}

		// disable buttons
		EnableButtons(false);

		// clear current image
		Pdf417Bitmap = null;

		// trap encoding errors
		try
			{
			// encoding request
			Encoder.EncodingControl = (EncodingControl) EncodingComboBox.SelectedIndex;

			// error correction request
			Encoder.ErrorCorrection = (ErrorCorrectionLevel) ErrorCorrectionComboBox.SelectedIndex;

			// language or character set
			int Part = CharacterSetComboBox.SelectedIndex;
			if(Part == 10) Part = 15;
			else if(Part == 9) Part = 13;
			else Part++;
			Encoder.GlobalLabelIDCharacterSet = string.Format("ISO-8859-{0}", Part);

			// narrow bar width
			Encoder.NarrowBarWidth = BarWidth;

			// row height
			Encoder.RowHeight = RowHeight;

			// quiet zone
			Encoder.QuietZone = QuietZone;

			// default data columns
			Encoder.DefaultDataColumns = DefDataColumns;

			// load barcode data
			Encoder.Encode(DataTextBox.Text);

			// create bitmap image
			Pdf417Bitmap = Encoder.CreateBarcodeBitmap();

			// update screen labels
			WidthHeightLabel.Text = ((double) Encoder.ImageWidth / Encoder.ImageHeight).ToString("0.0");
			DataRowsLabel.Text = Encoder.DataRows.ToString();
			DataColumnsLabel.Text = Encoder.DataColumns.ToString();


#if DEBUG
			string ArgLine = string.Format("-Col:{0} -Error:{1} -Width:{2} -Height:{3} -Quiet:{4} -t \"{5}\" \"{6}\"",
				10, 7, 6, 20, 15, "TestFile.txt",  "Test Image.png");
			Pdf417CommandLine.Encode(ArgLine);
#endif
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
	// allow user to adjust barcode aspect ratio
	/////////////////////////////////////////////////////////////////////

	private void OnAdjustLayout(object sender, EventArgs e)
		{
		AdjustLayout Dialog = new AdjustLayout(Encoder);
		if(Dialog.ShowDialog() == DialogResult.OK)
			{
			WidthHeightLabel.Text = ((double) Encoder.ImageWidth / Encoder.ImageHeight).ToString("0.0");
			DataRowsLabel.Text = Encoder.DataRows.ToString();
			DataColumnsLabel.Text = Encoder.DataColumns.ToString();

			// create bitmap image
			Pdf417Bitmap = Encoder.CreateBarcodeBitmap();

			// repaint panel
			Invalidate();
			}
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
		Dialog.FileName = "Pdf417Barcode.png";
		if(Dialog.ShowDialog() != DialogResult.OK) return;

		// save image as png file
		Encoder.SaveBarcodeToPngFile(Dialog.FileName);

		// start image editor
		Process.Start(Dialog.FileName);
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// save barcode image
	/////////////////////////////////////////////////////////////////////

	private void OnSaveImage(object sender, EventArgs e)
        {
		SaveImage Dialog = new SaveImage(Encoder, Pdf417Bitmap);
		Dialog.ShowDialog(this);
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// Enable/Disable buttons
	/////////////////////////////////////////////////////////////////////

	private void EnableButtons
			(
			bool Enabled
			)
		{
		EncodeButton.Enabled = Enabled;
		AdjustLayoutButton.Enabled = Pdf417Bitmap != null && Enabled;
		SavePngButton.Enabled = Pdf417Bitmap != null && Enabled;
		SaveImageButton.Enabled = Pdf417Bitmap != null && Enabled;
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// paint barcode on the screen
	/////////////////////////////////////////////////////////////////////

	private void OnPaint(object sender, PaintEventArgs e)
		{
		// no image
		if(Pdf417Bitmap == null) return;

		// calculate image area width and height to preserve aspect ratio
		Rectangle ImageRect = new Rectangle
			{
			Height = (BarcodeImageArea.Width * Pdf417Bitmap.Height) / Pdf417Bitmap.Width
			};
		if(ImageRect.Height <= BarcodeImageArea.Height)
			{
			ImageRect.Width = BarcodeImageArea.Width;
			}
		else
			{
			ImageRect.Width = (BarcodeImageArea.Height * Pdf417Bitmap.Width) / Pdf417Bitmap.Height;
			ImageRect.Height = BarcodeImageArea.Height;
			}

		// calculate position
		ImageRect.X = BarcodeImageArea.X + (BarcodeImageArea.Width - ImageRect.Width) / 2;
		ImageRect.Y = BarcodeImageArea.Y + (BarcodeImageArea.Height - ImageRect.Height) / 2;
		e.Graphics.DrawImage(Pdf417Bitmap, ImageRect);
		return;
		}

	/////////////////////////////////////////////////////////////////////
	// resize screen
	/////////////////////////////////////////////////////////////////////

	private void OnResize(object sender, EventArgs e)
		{
		if(ClientSize.Width == 0) return;

		// center header label
		HeaderLabel.Top = 4;
		HeaderLabel.Left = (ClientSize.Width - HeaderLabel.Width) / 2;

		// options
		OptionsGroupBox.Top = 4;
		OptionsGroupBox.Left = 4;

		// put buttons at bottom
		ButtonsGroupBox.Top = ClientSize.Height - ButtonsGroupBox.Height - 4;
		ButtonsGroupBox.Left = (ClientSize.Width - ButtonsGroupBox.Width) / 2;

		// put data text box above buttons
		DataTextBox.Top = ButtonsGroupBox.Top - DataTextBox.Height - 4;
		DataTextBox.Left = 8;
		DataTextBox.Width = ClientSize.Width - 16;

		// put data label above text box
		DataLabel.Top = DataTextBox.Top - DataLabel.Height - 3;
		DataLabel.Left = 4;

		// image area
		BarcodeImageArea.X = OptionsGroupBox.Right + 4;
		BarcodeImageArea.Y = HeaderLabel.Bottom + 4;
		BarcodeImageArea.Width = ClientSize.Width - OptionsGroupBox.Right - 8;
		BarcodeImageArea.Height = DataLabel.Top - HeaderLabel.Bottom - 8;

		// force re-paint
		Invalidate();
		return;
		}
	}
}
