﻿/////////////////////////////////////////////////////////////////////
//
//	QR Code Encoder Library
//
//	QR Code encoder.
//
//	Author: Uzi Granot
//	Original Version: 1.0
//	Date: June 30, 2018
//	Copyright (C) 2018-2019 Uzi Granot. All Rights Reserved
//	For full version history please look at QREncoder.cs
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
//
//	Version History:
//
//	Version 1.0 2018/06/30
//		Original revision
//
//	Version 1.1 2018/07/20
//		Consolidate DirectShowLib into one module removing unused code
//
//	Version 2.0 2019/05/15
//		Split the combined QRCode encoder and decoder to two solutions.
//		Add support for .net standard.
//		Add save image to png file without Bitmap class.
/////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
#if NET462
using System.Drawing;
using System.Drawing.Imaging;
#endif

namespace QRCodeEncoderLibrary
{
/// <summary>
/// QR Code error correction code enumeration
/// </summary>
public enum ErrorCorrection
	{
	/// <summary>
	/// Low (01)
	/// </summary>
	L,

	/// <summary>
	/// Medium (00)
	/// </summary>
	M,

	/// <summary>
	/// Medium-high (11)
	/// </summary>
	Q,

	/// <summary>
	/// High (10)
	/// </summary>
	H,
	}

/// <summary>
/// QR Code encoding modes
/// </summary>
public enum EncodingMode
		{
		/// <summary>
		/// Terminator
		/// </summary>
		Terminator,

		/// <summary>
		/// Numeric
		/// </summary>
		Numeric,

		/// <summary>
		/// Alpha numeric
		/// </summary>
		AlphaNumeric,

		/// <summary>
		/// Append
		/// </summary>
		Append,

		/// <summary>
		/// byte encoding
		/// </summary>
		Byte,

		/// <summary>
		/// FNC1 first
		/// </summary>
		FNC1First,

		/// <summary>
		/// Unknown encoding constant
		/// </summary>
		Unknown6,

		/// <summary>
		/// Unknown encoding constant
		/// </summary>
		Unknown7,

		/// <summary>
		/// Kanji encoding (not implemented by this software)
		/// </summary>
		Kanji,

		/// <summary>
		/// FNC1 second
		/// </summary>
		FNC1Second,

		/// <summary>
		/// Unknown encoding constant
		/// </summary>
		Unknown10,

		/// <summary>
		/// Unknown encoding constant
		/// </summary>
		Unknown11,

		/// <summary>
		/// Unknown encoding constant
		/// </summary>
		Unknown12,

		/// <summary>
		/// Unknown encoding constant
		/// </summary>
		Unknown13,

		/// <summary>
		/// Unknown encoding constant
		/// </summary>
		Unknown14,

		/// <summary>
		/// Unknown encoding constant
		/// </summary>
		Unknown15,
		}

/// <summary>
/// QR Code Encoder class
/// </summary>
public class QREncoder
    {
	/// <summary>
	/// Version number
	/// </summary>
	public const string VersionNumber = "Ver 2.0.0 - 2019-05-15";

	/// <summary>
	/// QR code matrix.
	/// </summary>
	public bool[,] QRCodeMatrix {get; internal set;}

	/// <summary>
	/// Gets QR Code matrix version
	/// </summary>
	public int QRCodeVersion {get; internal set;}

	/// <summary>
	/// Gets QR Code matrix dimension in bits
	/// </summary>
	public int QRCodeDimension {get; internal set;}

	/// <summary>
	/// Gets QR Code image dimension
	/// </summary>
	public int QRCodeImageDimension {get; internal set;}

	/// <summary>
	/// Get mask code (0 to 7)
	/// </summary>
	public int MaskCode {get; internal set;}

	// internal variables
	internal byte[][] DataSegArray;
	internal int EncodedDataBits;
	internal int MaxCodewords;
	internal int MaxDataCodewords;
	internal int MaxDataBits;
	internal int ErrCorrCodewords;
	internal int BlocksGroup1;
	internal int DataCodewordsGroup1;
	internal int BlocksGroup2;
	internal int DataCodewordsGroup2;

	internal EncodingMode[] EncodingSegMode;
	internal byte[] CodewordsArray;
	internal int CodewordsPtr;
	internal uint BitBuffer;
	internal int BitBufferLen;
	internal byte[,] BaseMatrix;
	internal byte[,] MaskMatrix;
	internal byte[,] ResultMatrix;

	private static readonly byte[] PngFileSignature = new byte[] {137, (byte) 'P', (byte) 'N', (byte) 'G', (byte) '\r', (byte) '\n', 26, (byte) '\n'};

	private static readonly byte[] PngIendChunk = new byte[] {0, 0, 0, 0, (byte) 'I', (byte) 'E', (byte) 'N', (byte) 'D', 0xae, 0x42, 0x60, 0x82};

	/// <summary>
	/// QR Code error correction code (L, M, Q, H)
	/// </summary>
	public ErrorCorrection ErrorCorrection
		{
		get
			{
			return _ErrorCorrection;
			}
		set
			{
			// test error correction
			if(value < ErrorCorrection.L || value > ErrorCorrection.H)
				throw new ArgumentException("Error correction is invalid. Must be L, M, Q or H. Default is M");

			// save error correction level
			_ErrorCorrection = value;
			return;
			}
		}
	private ErrorCorrection _ErrorCorrection = ErrorCorrection.M;

	/// <summary>
	/// Module size (Default: 2)
	/// </summary>
	public int ModuleSize
		{
		get
			{
			return _ModuleSize;
			}
		set
			{
			if(value < 1 || value > 100)
				throw new ArgumentException("Module size error. Default is 2.");
			_ModuleSize = value;

			// quiet zone must be at least 4 times module size
			if(_QuietZone < 4 * value) _QuietZone = 4 * value;

			// recalculate image dimension
			QRCodeImageDimension = 2 * _QuietZone + QRCodeDimension * _ModuleSize;
			return;	
			}
		}
	private int _ModuleSize = 2;

	/// <summary>
	/// Quiet zone around the barcode in pixels (Default: 8)
	/// Must be at least 4 times module size
	/// </summary>
	public int QuietZone
		{
		get
			{
			return _QuietZone;
			}
		set
			{
			if(value < 4 * _ModuleSize || value > 400)
				throw new ArgumentException("Quiet zone must be at least 4 times the module size. Default is 8.");
			_QuietZone = value;

			// recalculate image dimension
			QRCodeImageDimension = 2 * _QuietZone + QRCodeDimension * _ModuleSize;
			return;	
			}
		}
	private int _QuietZone = 8;

	/// <summary>
	/// Encode one string into QRCode boolean matrix
	/// </summary>
	/// <param name="StringDataSegment">string data segment</param>
	public void Encode
			(
			string StringDataSegment
			)
		{
		// empty
		if(string.IsNullOrEmpty(StringDataSegment))
			throw new ArgumentNullException("String data segment is null or missing");

		// convert string to byte array
		byte[] BinaryData = Encoding.UTF8.GetBytes(StringDataSegment);

		// encode data
		Encode(new byte[][] {BinaryData});
		return;
		}

	/// <summary>
	/// Encode array of strings into QRCode boolean matrix
	/// </summary>
	/// <param name="StringDataSegments">string data segments</param>
	public void Encode
			(
			string[] StringDataSegments
			)
		{
		// empty
		if(StringDataSegments == null || StringDataSegments.Length == 0)
			throw new ArgumentNullException("String data segments are null or empty");

		// loop for all segments
		for(int SegIndex = 0; SegIndex < StringDataSegments.Length; SegIndex++)
			{
			// convert string to byte array
			if(StringDataSegments[SegIndex] == null) throw new ArgumentNullException("One of the string data segments is null or empty");
			}

		// create bytes arrays
		byte[][] TempDataSegArray = new byte[StringDataSegments.Length][];

		// loop for all segments
		for(int SegIndex = 0; SegIndex < StringDataSegments.Length; SegIndex++)
			{
			// convert string to byte array
			TempDataSegArray[SegIndex] = Encoding.UTF8.GetBytes(StringDataSegments[SegIndex]);
			}
		
		// convert string to byte array
		Encode(TempDataSegArray);
		return;
		}

	/// <summary>
	/// Encode one data segment into QRCode boolean matrix
	/// </summary>
	/// <param name="SingleDataSeg">Data segment byte array</param>
	/// <returns>QR Code boolean matrix</returns>
	public void Encode
			(
			byte[] SingleDataSeg
			)
		{
		// test data segments array
		if(SingleDataSeg == null|| SingleDataSeg.Length == 0)
			throw new ArgumentNullException("Single data segment argument is null or empty");

		// encode data
		Encode(new byte[][] {SingleDataSeg});
		return;
		}

	/// <summary>
	/// Encode data segments array into QRCode boolean matrix
	/// </summary>
	/// <param name="DataSegArray">Data array of byte arrays</param>
	/// <returns>QR Code boolean matrix</returns>
	public void Encode
			(
			byte[][] DataSegArray
			)
		{
		// test data segments array
		if(DataSegArray == null|| DataSegArray.Length == 0)
			throw new ArgumentNullException("Data segments argument is null or empty");

		// reset result variables
		QRCodeMatrix = null;
		QRCodeVersion = 0;
		QRCodeDimension = 0;
		QRCodeImageDimension = 0;

		// loop for all segments
		int Bytes = 0;
		for(int SegIndex = 0; SegIndex < DataSegArray.Length; SegIndex++)
			{
			// input string length
			byte[] DataSeg = DataSegArray[SegIndex];
			if(DataSeg == null) DataSegArray[SegIndex] = new byte[0];
			else Bytes += DataSeg.Length;
			}
		if(Bytes == 0) throw new ArgumentException("There is no data to encode.");
		
		// save data segments array
		this.DataSegArray = DataSegArray;

		// initialization
		Initialization();

		// encode data
		EncodeData();

		// calculate error correction
		CalculateErrorCorrection();

		// iterleave data and error correction codewords
		InterleaveBlocks();

		// build base matrix
		BuildBaseMatrix();

		// load base matrix with data and error correction codewords
		LoadMatrixWithData();

		// data masking
		SelectBastMask();

		// add format information (error code level and mask code)
		AddFormatInformation();

		// output matrix each element is one module
		QRCodeMatrix = new bool[QRCodeDimension, QRCodeDimension];

		// convert result matrix to output matrix
		// Black=true, White=false
		for(int Row = 0; Row < QRCodeDimension; Row++)
			{
			for(int Col = 0; Col < QRCodeDimension; Col++)
				{
				if((ResultMatrix[Row, Col] & 1) != 0) QRCodeMatrix[Row, Col] = true;
				}
			}

		// exit
		return;
		}

	/// <summary>
	/// Save QRCode image to PNG file
	/// </summary>
	/// <param name="FileName">PNG file name</param>
	public void SaveQRCodeToPngFile
			(
			string FileName
			)
		{
		// exceptions
		if(FileName == null)
			throw new ArgumentNullException("SaveQRCodeToPngFile: FileName is null");
		if(!FileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
			throw new ArgumentException("SaveQRCodeToPngFile: FileName extension must be .png");
		if(QRCodeMatrix == null)
			throw new ApplicationException("QRCode must be encoded first");

		// file name to stream
		using(Stream OutputStream = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
			{ 
			// save file
			SaveQRCodeToPngFile(OutputStream);
			}
		return;
		}

	/// <summary>
	/// Save QRCode image to PNG stream
	/// </summary>
	/// <param name="OutputStream">PNG output stream</param>
	public void SaveQRCodeToPngFile
			(
			Stream OutputStream
			)
		{
		if(QRCodeMatrix == null)
			throw new ApplicationException("QRCode must be encoded first");

		// header
		byte[] Header = BuildPngHeader();

		// barcode data
		byte[] InputBuf = QRCodeMatrixToPng();

		// compress barcode data
		byte[] OutputBuf = PngImageData(InputBuf);

		// stream to binary writer
		BinaryWriter BW = new BinaryWriter(OutputStream);

		// write signature
		BW.Write(PngFileSignature, 0, PngFileSignature.Length);

		// write header
		BW.Write(Header, 0, Header.Length);

		// write image data
		BW.Write(OutputBuf, 0, OutputBuf.Length);

		// write end of file
		BW.Write(PngIendChunk, 0, PngIendChunk.Length);

		// flush all buffers
		BW.Flush();
		return;
		}

	/// <summary>
	/// convert black and white matrix to black and white image
	/// </summary>
	/// <returns>Black and white image in pixels</returns>
	public bool[,] ConvertQRCodeMatrixToPixels()
		{
		if(QRCodeMatrix == null)
			throw new ApplicationException("QRCode must be encoded first");

		// output matrix size in pixels all matrix elements are white (false)
		int ImageDimension = QRCodeImageDimension;
		bool[,] BWImage = new bool[ImageDimension, ImageDimension];

		int XOffset = _QuietZone;
		int YOffset = _QuietZone;

		// convert result matrix to output matrix
		for(int Row = 0; Row < QRCodeDimension; Row++)
			{
			for(int Col = 0; Col < QRCodeDimension; Col++)
				{
				// bar is black
				if(QRCodeMatrix[Row, Col])
					{
					for(int Y = 0; Y < ModuleSize; Y++)
						{
						for(int X = 0; X < ModuleSize; X++) BWImage[YOffset + Y, XOffset + X] = true;
						}
					}
				XOffset += ModuleSize;
				}
			XOffset = _QuietZone;
			YOffset += ModuleSize;
			}
		return BWImage;
		}

#if NET462
	/// <summary>
	/// Save barcode Bitmap to file
	/// </summary>
	/// <param name="FileName">File name</param>
	/// <param name="Format">Image file format (i.e. PNG, BMP, JPEG)</param>
	public void SaveQRCodeToFile
			(
			string FileName,
			ImageFormat Format
			)
		{
		// create Bitmap image of barcode
		Bitmap BarcodeImage = CreateQRCodeBitmap();

		// save image to file
		using(FileStream FS = new FileStream(FileName, FileMode.Create))
			{
			BarcodeImage.Save(FS, Format);
			}
		return;
		}

	/// <summary>
	/// Save barcode Bitmap to stream
	/// </summary>
	/// <param name="OutputStream">Output stream</param>
	/// <param name="Format">Image file format (i.e. PNG, BMP, JPEG)</param>
	public void SaveQRCodeToFile
			(
			Stream OutputStream,
			ImageFormat Format
			)
		{
		// create Bitmap image of barcode
		Bitmap BarcodeImage = CreateQRCodeBitmap();

		// save image
		BarcodeImage.Save(OutputStream, Format);

		// flush stream
		OutputStream.Flush();
		return;
		}

	/// <summary>
	/// Create Bitmap image of the Pdf417 barcode
	/// </summary>
	/// <returns>Barcode Bitmap</returns>
	public Bitmap CreateQRCodeBitmap()
		{
		return CreateQRCodeBitmap(Brushes.White, Brushes.Black);
		}

	/// <summary>
	/// Create Pdf417 barcode Bitmap image from boolean black and white matrix
	/// </summary>
	/// <param name="WhiteBrush">Background color (White brush)</param>
	/// <param name="BlackBrush">Bar color (Black brush)</param>
	/// <returns>Pdf417 barcode image</returns>
	public Bitmap CreateQRCodeBitmap
			(
			Brush WhiteBrush,
			Brush BlackBrush
			)
		{
		if(QRCodeMatrix == null)
			throw new ApplicationException("QRCode must be encoded first");

		// create picture object and make it white
		int ImageDimension = QRCodeImageDimension;
		Bitmap Image = new Bitmap(ImageDimension, ImageDimension);
		Graphics Graphics = Graphics.FromImage(Image);
		Graphics.FillRectangle(WhiteBrush, 0, 0, ImageDimension, ImageDimension);

		// x and y image pointers
		int XOffset = _QuietZone;
		int YOffset = _QuietZone;

		// convert result matrix to output matrix
		for(int Row = 0; Row < QRCodeDimension; Row++)
			{
			for(int Col = 0; Col < QRCodeDimension; Col++)
				{
				// bar is black
				if(QRCodeMatrix[Row, Col]) Graphics.FillRectangle(BlackBrush, XOffset, YOffset, ModuleSize, ModuleSize);
				XOffset += ModuleSize;
				}
			XOffset = _QuietZone;
			YOffset += ModuleSize;
			}

		// return image
		return Image;
		}
#endif

		////////////////////////////////////////////////////////////////////
	// Initialization
	////////////////////////////////////////////////////////////////////

	internal void Initialization()
		{
		// create encoding mode array
		EncodingSegMode = new EncodingMode[DataSegArray.Length];

		// reset total encoded data bits
		EncodedDataBits = 0;

		// loop for all segments
		for(int SegIndex = 0; SegIndex < DataSegArray.Length; SegIndex++)
			{
			// input string length
			byte[] DataSeg = DataSegArray[SegIndex];
			int DataLength = DataSeg.Length;

			// find encoding mode
			EncodingMode EncodingMode = EncodingMode.Numeric;
			for(int Index = 0; Index < DataLength; Index++)
				{
				int Code = StaticTables.EncodingTable[(int) DataSeg[Index]];
				if(Code < 10) continue;
				if(Code < 45)
					{
					EncodingMode = EncodingMode.AlphaNumeric;
					continue;
					}
				EncodingMode = EncodingMode.Byte;
				break;			
				}

			// calculate required bit length
			int DataBits = 4;
			switch(EncodingMode)
				{
				case EncodingMode.Numeric:
					DataBits += 10 * (DataLength / 3);
					if((DataLength % 3) == 1) DataBits += 4; 
					else if((DataLength % 3) == 2) DataBits += 7; 
					break;

				case EncodingMode.AlphaNumeric:
					DataBits += 11 * (DataLength / 2);
					if((DataLength & 1) != 0) DataBits += 6; 
					break;

				case EncodingMode.Byte:
					DataBits += 8 * DataLength;
					break;
				}

			EncodingSegMode[SegIndex] = EncodingMode;
			EncodedDataBits += DataBits;
			}

		// find best version
		int TotalDataLenBits = 0;
		for(QRCodeVersion = 1; QRCodeVersion <= 40; QRCodeVersion++)
			{
			// number of bits on each side of the QR code square
			QRCodeDimension = 17 + 4 * QRCodeVersion;
			QRCodeImageDimension = 2 * _QuietZone + QRCodeDimension * _ModuleSize;

			SetDataCodewordsLength();
			TotalDataLenBits = 0;
			for(int Seg = 0; Seg < EncodingSegMode.Length; Seg++) TotalDataLenBits += DataLengthBits(EncodingSegMode[Seg]);
			if(EncodedDataBits + TotalDataLenBits <= MaxDataBits) break;
			}

		if(QRCodeVersion > 40) throw new ApplicationException("Input data string is too long");
		EncodedDataBits += TotalDataLenBits;
		return;
		}
			
	////////////////////////////////////////////////////////////////////
	// QRCode: Convert data to bit array
	////////////////////////////////////////////////////////////////////
	internal void EncodeData()
		{
		// codewords array
		CodewordsArray = new byte[MaxCodewords];

		// reset encoding members
		CodewordsPtr = 0;
		BitBuffer = 0;
		BitBufferLen = 0;

		// loop for all segments
		for(int SegIndex = 0; SegIndex < DataSegArray.Length; SegIndex++)
			{
			// input string length
			byte[] DataSeg = DataSegArray[SegIndex];
			int DataLength = DataSeg.Length;

			// first 4 bits is mode indicator
			// numeric code indicator is 0001, alpha numeric 0010, byte 0100
			SaveBitsToCodewordsArray((int) EncodingSegMode[SegIndex], 4);

			// character count
			SaveBitsToCodewordsArray(DataLength, DataLengthBits(EncodingSegMode[SegIndex]));
			
			// switch based on encode mode
			switch(EncodingSegMode[SegIndex])
				{				
				// numeric mode
				case EncodingMode.Numeric: 
					// encode digits in groups of 3
					int NumEnd = (DataLength / 3) * 3;
					for(int Index = 0; Index < NumEnd; Index += 3) SaveBitsToCodewordsArray(
						100 * StaticTables.EncodingTable[(int) DataSeg[Index]] +
							10 * StaticTables.EncodingTable[(int) DataSeg[Index + 1]] +
								StaticTables.EncodingTable[(int) DataSeg[Index + 2]], 10);

					// we have one digit remaining
					if(DataLength - NumEnd == 1) SaveBitsToCodewordsArray(StaticTables.EncodingTable[(int) DataSeg[NumEnd]], 4);

					// we have two digits remaining
					else if(DataLength - NumEnd == 2) SaveBitsToCodewordsArray(10 * StaticTables.EncodingTable[(int) DataSeg[NumEnd]] +
						StaticTables.EncodingTable[(int) DataSeg[NumEnd + 1]], 7);
					break;

				// alphanumeric mode
				case EncodingMode.AlphaNumeric: 
					// encode digits in groups of 2
					int AlphaNumEnd = (DataLength / 2) * 2;
					for(int Index = 0; Index < AlphaNumEnd; Index += 2)
						SaveBitsToCodewordsArray(45 * StaticTables.EncodingTable[(int) DataSeg[Index]] + StaticTables.EncodingTable[(int) DataSeg[Index + 1]], 11);

					// we have one character remaining
					if(DataLength - AlphaNumEnd == 1) SaveBitsToCodewordsArray(StaticTables.EncodingTable[(int) DataSeg[AlphaNumEnd]], 6);
					break;
					

				// byte mode					
				case EncodingMode.Byte: 
					// append the data after mode and character count
					for(int Index = 0; Index < DataLength; Index++) SaveBitsToCodewordsArray((int) DataSeg[Index], 8);
					break;
				}
			}
			
		// set terminator
		if(EncodedDataBits < MaxDataBits) SaveBitsToCodewordsArray(0, MaxDataBits - EncodedDataBits < 4 ? MaxDataBits - EncodedDataBits : 4);

		// flush bit buffer
		if(BitBufferLen > 0) CodewordsArray[CodewordsPtr++] = (byte) (BitBuffer >> 24);

		// add extra padding if there is still space
		int PadEnd = MaxDataCodewords - CodewordsPtr;
		for(int PadPtr = 0; PadPtr < PadEnd; PadPtr++) CodewordsArray[CodewordsPtr + PadPtr] = (byte) ((PadPtr & 1) == 0 ? 0xEC : 0x11); 

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Save data to codeword array
	////////////////////////////////////////////////////////////////////
	internal void SaveBitsToCodewordsArray
			(
			int	Data,
			int	Bits
			)
		{
		BitBuffer |= (uint) Data << (32 - BitBufferLen - Bits);
		BitBufferLen += Bits;
		while(BitBufferLen >= 8)
			{
			CodewordsArray[CodewordsPtr++] = (byte) (BitBuffer >> 24);
			BitBuffer <<= 8;
			BitBufferLen -= 8;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Calculate Error Correction
	////////////////////////////////////////////////////////////////////
	internal void CalculateErrorCorrection()
		{
		// set generator polynomial array
		byte[] Generator = StaticTables.GenArray[ErrCorrCodewords - 7];

		// error correcion calculation buffer
		int BufSize = Math.Max(DataCodewordsGroup1, DataCodewordsGroup2) + ErrCorrCodewords;
		byte[] ErrCorrBuff = new byte[BufSize];

		// initial number of data codewords
		int DataCodewords = DataCodewordsGroup1;
		int BuffLen = DataCodewords + ErrCorrCodewords;

		// codewords pointer
		int DataCodewordsPtr = 0;

		// codewords buffer error correction pointer
		int CodewordsArrayErrCorrPtr = MaxDataCodewords;

		// loop one block at a time
		int TotalBlocks = BlocksGroup1 + BlocksGroup2;
		for(int BlockNumber = 0; BlockNumber < TotalBlocks; BlockNumber++)
			{
			// switch to group2 data codewords
			if(BlockNumber == BlocksGroup1)
				{
				DataCodewords = DataCodewordsGroup2;
				BuffLen = DataCodewords + ErrCorrCodewords;
				}

			// copy next block of codewords to the buffer and clear the remaining part
			Array.Copy(CodewordsArray, DataCodewordsPtr, ErrCorrBuff, 0, DataCodewords);
			Array.Clear(ErrCorrBuff, DataCodewords, ErrCorrCodewords);

			// update codewords array to next buffer
			DataCodewordsPtr += DataCodewords;

			// error correction polynomial division
			PolynominalDivision(ErrCorrBuff, BuffLen, Generator, ErrCorrCodewords);

			// save error correction block			
			Array.Copy(ErrCorrBuff, DataCodewords, CodewordsArray, CodewordsArrayErrCorrPtr, ErrCorrCodewords);
			CodewordsArrayErrCorrPtr += ErrCorrCodewords;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Polynomial division for error correction
	////////////////////////////////////////////////////////////////////

	internal static void PolynominalDivision
			(
			byte[] Polynomial,
			int PolyLength,
			byte[] Generator,
			int ErrCorrCodewords
			)
		{
		int DataCodewords = PolyLength - ErrCorrCodewords;

		// error correction polynomial division
		for(int Index = 0; Index < DataCodewords; Index++)
			{
			// current first codeword is zero
			if(Polynomial[Index] == 0) continue;

			// current first codeword is not zero
			int Multiplier = StaticTables.IntToExp[Polynomial[Index]];

			// loop for error correction coofficients
			for(int GeneratorIndex = 0; GeneratorIndex < ErrCorrCodewords; GeneratorIndex++)
				{
				Polynomial[Index + 1 + GeneratorIndex] = (byte) (Polynomial[Index + 1 + GeneratorIndex] ^ StaticTables.ExpToInt[Generator[GeneratorIndex] + Multiplier]);
				}
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Interleave data and error correction blocks
	////////////////////////////////////////////////////////////////////
	internal void InterleaveBlocks()
		{
		// allocate temp codewords array
		byte[] TempArray = new byte[MaxCodewords];

		// total blocks
		int TotalBlocks = BlocksGroup1 + BlocksGroup2;

		// create array of data blocks starting point
		int[] Start = new int[TotalBlocks];
		for(int Index = 1; Index < TotalBlocks; Index++) Start[Index] = Start[Index - 1] + (Index <= BlocksGroup1 ? DataCodewordsGroup1 : DataCodewordsGroup2);

		// step one. iterleave base on group one length
		int PtrEnd = DataCodewordsGroup1 * TotalBlocks;

		// iterleave group one and two
		int Ptr;
		int Block = 0;
		for(Ptr = 0; Ptr < PtrEnd; Ptr++)
			{
			TempArray[Ptr] = CodewordsArray[Start[Block]];
			Start[Block]++;
			Block++;
			if(Block == TotalBlocks) Block = 0;
			}

		// interleave group two
		if(DataCodewordsGroup2 > DataCodewordsGroup1)
			{
			// step one. iterleave base on group one length
			PtrEnd = MaxDataCodewords;

			Block = BlocksGroup1;
			for(; Ptr < PtrEnd; Ptr++)
				{
				TempArray[Ptr] = CodewordsArray[Start[Block]];
				Start[Block]++;
				Block++;
				if(Block == TotalBlocks) Block = BlocksGroup1;
				}
			}

		// create array of error correction blocks starting point
		Start[0] = MaxDataCodewords;
		for(int Index = 1; Index < TotalBlocks; Index++) Start[Index] = Start[Index - 1] + ErrCorrCodewords;

		// step one. iterleave base on group one length

		// iterleave all groups
		PtrEnd = MaxCodewords;
		Block = 0;
		for(; Ptr < PtrEnd; Ptr++)
			{
			TempArray[Ptr] = CodewordsArray[Start[Block]];
			Start[Block]++;
			Block++;
			if(Block == TotalBlocks) Block = 0;
			}

		// save result
		CodewordsArray = TempArray;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Load base matrix with data and error correction codewords
	////////////////////////////////////////////////////////////////////
	internal void LoadMatrixWithData()
		{
		// input array pointer initialization
		int Ptr = 0;
		int PtrEnd = 8 * MaxCodewords;

		// bottom right corner of output matrix
		int Row = QRCodeDimension - 1;
		int Col = QRCodeDimension - 1;

		// step state
		int State = 0;
		for(;;) 
			{
			// current module is data
			if((BaseMatrix[Row, Col] & StaticTables.NonData) == 0)
				{
				// load current module with
				if((CodewordsArray[Ptr >> 3] & (1 << (7 - (Ptr & 7)))) != 0) BaseMatrix[Row, Col] = StaticTables.DataBlack;
				if(++Ptr == PtrEnd) break;
				}

			// current module is non data and vertical timing line condition is on
			else if(Col == 6) Col--;

			// update matrix position to next module
			switch(State)
				{
				// going up: step one to the left
				case 0:
					Col--;
					State = 1;
					continue;

				// going up: step one row up and one column to the right
				case 1:
					Col++;
					Row--;
					// we are not at the top, go to state 0
					if(Row >= 0)
						{
						State = 0;
						continue;
						}
					// we are at the top, step two columns to the left and start going down
					Col -= 2;
					Row = 0;
					State = 2;
					continue;

				// going down: step one to the left
				case 2:
					Col--;
					State = 3;
					continue;

				// going down: step one row down and one column to the right
				case 3:
					Col++;
					Row++;
					// we are not at the bottom, go to state 2
					if(Row < QRCodeDimension)
						{
						State = 2;
						continue;
						}
					// we are at the bottom, step two columns to the left and start going up
					Col -= 2;
					Row = QRCodeDimension - 1;
					State = 0;
					continue;
				}
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Select Mask
	////////////////////////////////////////////////////////////////////
	internal void SelectBastMask()
		{
		int BestScore = int.MaxValue;
		MaskCode = 0;

		for(int TestMask = 0; TestMask < 8; TestMask++)
			{
			// apply mask
			ApplyMask(TestMask);

			// evaluate 4 test conditions
			int Score = EvaluationCondition1();
			if(Score >= BestScore) continue;
			Score += EvaluationCondition2();
			if(Score >= BestScore) continue;
			Score += EvaluationCondition3();
			if(Score >= BestScore) continue;
			Score += EvaluationCondition4();
			if(Score >= BestScore) continue;

			// save as best mask so far
			ResultMatrix = MaskMatrix;
			MaskMatrix = null;
			BestScore = Score;
			MaskCode = TestMask;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Evaluation condition #1
	// 5 consecutive or more modules of the same color
	////////////////////////////////////////////////////////////////////
	internal int EvaluationCondition1()
		{
		int Score = 0;

		// test rows
		for(int Row = 0; Row < QRCodeDimension; Row++)
			{
			int Count = 1;
			for(int Col = 1; Col < QRCodeDimension; Col++)
				{
				// current cell is not the same color as the one before
				if(((MaskMatrix[Row, Col - 1] ^ MaskMatrix[Row, Col]) & 1) != 0)
					{
					if(Count >= 5) Score += Count - 2;
					Count = 0;
					}
				Count++;
				}

			// last run
			if(Count >= 5) Score += Count - 2;
			}

		// test columns
		for(int Col = 0; Col < QRCodeDimension; Col++)
			{
			int Count = 1;
			for(int Row = 1; Row < QRCodeDimension; Row++)
				{
				// current cell is not the same color as the one before
				if(((MaskMatrix[Row - 1, Col] ^ MaskMatrix[Row, Col]) & 1) != 0)
					{
					if(Count >= 5) Score += Count - 2;
					Count = 0;
					}
				Count++;
				}

			// last run
			if(Count >= 5) Score += Count - 2;
			}
		return Score;
		}

	////////////////////////////////////////////////////////////////////
	// Evaluation condition #2
	// same color in 2 by 2 area
	////////////////////////////////////////////////////////////////////
	internal int EvaluationCondition2()
		{
		int Score = 0;
		// test rows
		for(int Row = 1; Row < QRCodeDimension; Row++) for(int Col = 1; Col < QRCodeDimension; Col++)
			{
			// all are black
			if(((MaskMatrix[Row - 1, Col - 1] & MaskMatrix[Row - 1, Col] & MaskMatrix[Row, Col - 1] & MaskMatrix[Row, Col]) & 1) != 0) Score += 3;

			// all are white
			else if(((MaskMatrix[Row - 1, Col - 1] | MaskMatrix[Row - 1, Col] | MaskMatrix[Row, Col - 1] | MaskMatrix[Row, Col]) & 1) == 0) Score += 3;
			}
		return Score;
		}

	////////////////////////////////////////////////////////////////////
	// Evaluation condition #3
	// pattern dark, light, dark, dark, dark, light, dark
	// before or after 4 light modules
	////////////////////////////////////////////////////////////////////
	internal int EvaluationCondition3()
		{
		int Score = 0;

		// test rows
		for(int Row = 0; Row < QRCodeDimension; Row++)
			{
			int Start = 0;

			// look for a lignt run at least 4 modules
			for(int Col = 0; Col < QRCodeDimension; Col++)
				{
				// current cell is white
				if((MaskMatrix[Row, Col] & 1) == 0) continue;

				// more or equal to 4
				if(Col - Start >= 4)
					{
					// we have 4 or more white
					// test for pattern before the white space
					if(Start >= 7 && TestHorizontalDarkLight(Row, Start - 7)) Score += 40;

					// test for pattern after the white space
					if(QRCodeDimension - Col >= 7 && TestHorizontalDarkLight(Row, Col))
						{
						Score += 40;
						Col += 6;
						}
					}

				// assume next one is white
				Start = Col + 1;
				}

			// last run
			if(QRCodeDimension - Start >= 4 && Start >= 7 && TestHorizontalDarkLight(Row, Start - 7)) Score += 40;
			}

		// test columns
		for(int Col = 0; Col < QRCodeDimension; Col++)
			{
			int Start = 0;

			// look for a lignt run at least 4 modules
			for(int Row = 0; Row < QRCodeDimension; Row++)
				{
				// current cell is white
				if((MaskMatrix[Row, Col] & 1) == 0) continue;

				// more or equal to 4
				if(Row - Start >= 4)
					{
					// we have 4 or more white
					// test for pattern before the white space
					if(Start >= 7 && TestVerticalDarkLight(Start - 7, Col)) Score += 40;

					// test for pattern after the white space
					if(QRCodeDimension - Row >= 7 && TestVerticalDarkLight(Row, Col))
						{
						Score += 40;
						Row += 6;
						}
					}

				// assume next one is white
				Start = Row + 1;
				}

			// last run
			if(QRCodeDimension - Start >= 4 && Start >= 7 && TestVerticalDarkLight(Start - 7, Col)) Score += 40;
			}

		// exit
		return Score;
		}

	////////////////////////////////////////////////////////////////////
	// Evaluation condition #4
	// blak to white ratio
	////////////////////////////////////////////////////////////////////

	internal int EvaluationCondition4()
		{
		// count black cells
		int Black = 0;
		for(int Row = 0; Row < QRCodeDimension; Row++) for(int Col = 0; Col < QRCodeDimension; Col++) if((MaskMatrix[Row, Col] & 1) != 0) Black++;

		// ratio
		double Ratio = (double) Black / (double) (QRCodeDimension * QRCodeDimension);

		// there are more black than white
		if(Ratio > 0.55) return (int) (20.0 * (Ratio - 0.5)) * 10;
		else if(Ratio < 0.45) return (int) (20.0 * (0.5 - Ratio)) * 10;
		return 0;
		}

	////////////////////////////////////////////////////////////////////
	// Test horizontal dark light pattern
	////////////////////////////////////////////////////////////////////
	internal bool TestHorizontalDarkLight
			(
			int	Row,
			int	Col
			)
		{
		return (MaskMatrix[Row, Col] & ~MaskMatrix[Row, Col + 1] & MaskMatrix[Row, Col + 2] & MaskMatrix[Row, Col + 3] &
					MaskMatrix[Row, Col + 4] & ~MaskMatrix[Row, Col + 5] & MaskMatrix[Row, Col + 6] & 1) != 0;
		}

	////////////////////////////////////////////////////////////////////
	// Test vertical dark light pattern
	////////////////////////////////////////////////////////////////////
	internal bool TestVerticalDarkLight
			(
			int	Row,
			int	Col
			)
		{
		return (MaskMatrix[Row, Col] & ~MaskMatrix[Row + 1, Col] & MaskMatrix[Row + 2, Col] & MaskMatrix[Row + 3, Col] &
					MaskMatrix[Row + 4, Col] & ~MaskMatrix[Row + 5, Col] & MaskMatrix[Row + 6, Col] & 1) != 0;
		}

	////////////////////////////////////////////////////////////////////
	// Add format information
	// version, error correction code plus mask code
	////////////////////////////////////////////////////////////////////
	internal void AddFormatInformation()
		{
		int Mask;

		// version information
		if(QRCodeVersion >= 7)
			{
			int Pos = QRCodeDimension - 11;
			int VerInfo = StaticTables.VersionCodeArray[QRCodeVersion - 7];

			// top right
			Mask = 1;
			for(int Row = 0; Row < 6; Row++) for(int Col = 0; Col < 3; Col++)
				{
				ResultMatrix[Row, Pos + Col] = (VerInfo & Mask) != 0 ? StaticTables.FixedBlack : StaticTables.FixedWhite;
				Mask <<= 1;
				}

			// bottom left
			Mask = 1;
			for(int Col = 0; Col < 6; Col++) for(int Row = 0; Row < 3; Row++)
				{
				ResultMatrix[Pos + Row, Col] =  (VerInfo & Mask) != 0 ? StaticTables.FixedBlack : StaticTables.FixedWhite;
				Mask <<= 1;
				}
			}

		// error correction code and mask number
		int FormatInfoPtr = 0; // M is the default
		switch(_ErrorCorrection)
			{
			case ErrorCorrection.L:
				FormatInfoPtr = 8;
				break;

			case ErrorCorrection.Q:
				FormatInfoPtr = 24;
				break;

			case ErrorCorrection.H:
				FormatInfoPtr = 16;
				break;
			}
		int FormatInfo = StaticTables.FormatInfoArray[FormatInfoPtr + MaskCode];

		// load format bits into result matrix
		Mask = 1;
		for(int Index = 0; Index < 15; Index++)
			{
			int FormatBit = (FormatInfo & Mask) != 0 ? StaticTables.FixedBlack : StaticTables.FixedWhite;
			Mask <<= 1;

			// top left corner
			ResultMatrix[StaticTables.FormatInfoOne[Index, 0], StaticTables.FormatInfoOne[Index, 1]] = (byte) FormatBit;

			// bottom left and top right corners
			int Row = StaticTables.FormatInfoTwo[Index, 0];
			if(Row < 0) Row += QRCodeDimension;
			int Col =StaticTables. FormatInfoTwo[Index, 1];
			if(Col < 0) Col += QRCodeDimension;
			ResultMatrix[Row, Col] = (byte) FormatBit;
			}
		return;
		}
	////////////////////////////////////////////////////////////////////
	// Set encoded data bits length
	////////////////////////////////////////////////////////////////////

	internal int DataLengthBits
			(
			EncodingMode EncodingMode
			)
		{
		// Data length bits
		switch(EncodingMode)
			{				
			// numeric mode
            case EncodingMode.Numeric: 
				return QRCodeVersion < 10 ? 10 : (QRCodeVersion < 27 ? 12 : 14);

			// alpha numeric mode
            case EncodingMode.AlphaNumeric: 
				return QRCodeVersion < 10 ? 9 : (QRCodeVersion < 27 ? 11 : 13);

			// byte mode
            case EncodingMode.Byte:
				return QRCodeVersion < 10 ? 8 : 16;
			}
		throw new ApplicationException("Encoding mode error");
		}

	////////////////////////////////////////////////////////////////////
	// Set data and error correction codewords length
	////////////////////////////////////////////////////////////////////

	internal void SetDataCodewordsLength()
		{
		// index shortcut
		int BlockInfoIndex = (QRCodeVersion - 1) * 4 + (int) _ErrorCorrection;

		// Number of blocks in group 1
		BlocksGroup1 = StaticTables.ECBlockInfo[BlockInfoIndex, StaticTables.BLOCKS_GROUP1];

		// Number of data codewords in blocks of group 1
		DataCodewordsGroup1 = StaticTables.ECBlockInfo[BlockInfoIndex, StaticTables.DATA_CODEWORDS_GROUP1];

		// Number of blocks in group 2
		BlocksGroup2 = StaticTables.ECBlockInfo[BlockInfoIndex, StaticTables.BLOCKS_GROUP2];

		// Number of data codewords in blocks of group 2
		DataCodewordsGroup2 = StaticTables.ECBlockInfo[BlockInfoIndex, StaticTables.DATA_CODEWORDS_GROUP2];

		// Total number of data codewords for this version and EC level
		MaxDataCodewords = BlocksGroup1 * DataCodewordsGroup1 + BlocksGroup2 * DataCodewordsGroup2;
		MaxDataBits = 8 * MaxDataCodewords;

		// total data plus error correction bits
		MaxCodewords = StaticTables.MaxCodewordsArray[QRCodeVersion];

		// Error correction codewords per block
		ErrCorrCodewords = (MaxCodewords - MaxDataCodewords) / (BlocksGroup1 + BlocksGroup2);

		// exit
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Build Base Matrix
	////////////////////////////////////////////////////////////////////

	internal void BuildBaseMatrix()
		{
		// allocate base matrix
		BaseMatrix = new byte[QRCodeDimension + 5, QRCodeDimension + 5];

		// top left finder patterns
		for(int Row = 0; Row < 9; Row++) for(int Col = 0; Col < 9; Col++) BaseMatrix[Row, Col] = StaticTables.FinderPatternTopLeft[Row, Col];

		// top right finder patterns
		int Pos = QRCodeDimension - 8;
		for(int Row = 0; Row < 9; Row++) for(int Col = 0; Col < 8; Col++) BaseMatrix[Row, Pos + Col] = StaticTables.FinderPatternTopRight[Row, Col];

		// bottom left finder patterns
		for(int Row = 0; Row < 8; Row++) for(int Col = 0; Col < 9; Col++) BaseMatrix[Pos + Row, Col] = StaticTables.FinderPatternBottomLeft[Row, Col];

		// Timing pattern
		for(int Z = 8; Z < QRCodeDimension - 8; Z++) BaseMatrix[Z, 6] = BaseMatrix[6, Z] = (Z & 1) == 0 ? StaticTables.FixedBlack : StaticTables.FixedWhite;

		// alignment pattern
		if(QRCodeVersion > 1)
			{
			byte[] AlignPos = StaticTables.AlignmentPositionArray[QRCodeVersion];
			int AlignmentDimension = AlignPos.Length;
			for(int Row = 0; Row < AlignmentDimension; Row++) for(int Col = 0; Col < AlignmentDimension; Col++)
				{
				if(Col == 0 && Row == 0 || Col == AlignmentDimension - 1 && Row == 0 || Col == 0 && Row == AlignmentDimension - 1) continue;

				int PosRow = AlignPos[Row];
				int PosCol = AlignPos[Col];
				for(int ARow = -2; ARow < 3; ARow++) for(int ACol = -2; ACol < 3; ACol++)
					{
					BaseMatrix[PosRow + ARow, PosCol + ACol] = StaticTables.AlignmentPattern[ARow + 2, ACol + 2];
					}
				}
			}

		// reserve version information
		if(QRCodeVersion >= 7)
			{
			// position of 3 by 6 rectangles
			Pos = QRCodeDimension - 11;

			// top right
			for(int Row = 0; Row < 6; Row++) for(int Col = 0; Col < 3; Col++) BaseMatrix[Row, Pos + Col] = StaticTables.FormatWhite;

			// bottom right
			for(int Col = 0; Col < 6; Col++) for(int Row = 0; Row < 3; Row++) BaseMatrix[Pos + Row, Col] = StaticTables.FormatWhite;
			}

		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask
			(
			int Mask
			)
		{
		MaskMatrix = (byte[,]) BaseMatrix.Clone();
		switch(Mask)
			{
			case 0:
				ApplyMask0();
				break;

			case 1:
				ApplyMask1();
				break;

			case 2:
				ApplyMask2();
				break;

			case 3:
				ApplyMask3();
				break;

			case 4:
				ApplyMask4();
				break;

			case 5:
				ApplyMask5();
				break;

			case 6:
				ApplyMask6();
				break;

			case 7:
				ApplyMask7();
				break;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask 0
	// (row + column) % 2 == 0
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask0()
		{
		for(int Row = 0; Row < QRCodeDimension ; Row += 2) for(int Col = 0; Col < QRCodeDimension; Col += 2)
			{
			if((MaskMatrix[Row, Col] & StaticTables.NonData) == 0) MaskMatrix[Row, Col] ^= 1;
			if((MaskMatrix[Row + 1, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 1] ^= 1;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask 1
	// row % 2 == 0
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask1()
		{
		for(int Row = 0; Row < QRCodeDimension; Row += 2) for(int Col = 0; Col < QRCodeDimension; Col++)
			if((MaskMatrix[Row, Col] & StaticTables.NonData) == 0) MaskMatrix[Row, Col] ^= 1;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask 2
	// column % 3 == 0
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask2()
		{
		for(int Row = 0; Row < QRCodeDimension; Row++) for(int Col = 0; Col < QRCodeDimension; Col += 3)
			if((MaskMatrix[Row, Col] & StaticTables.NonData) == 0) MaskMatrix[Row, Col] ^= 1;
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask 3
	// (row + column) % 3 == 0
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask3()
		{
		for(int Row = 0; Row < QRCodeDimension; Row += 3) for(int Col = 0; Col < QRCodeDimension; Col += 3)
			{
			if((MaskMatrix[Row, Col] & StaticTables.NonData) == 0) MaskMatrix[Row, Col] ^= 1;
			if((MaskMatrix[Row + 1, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 2] ^= 1;
			if((MaskMatrix[Row + 2, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 1] ^= 1;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask 4
	// ((row / 2) + (column / 3)) % 2 == 0
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask4()
		{
		for(int Row = 0; Row < QRCodeDimension; Row += 4) for(int Col = 0; Col < QRCodeDimension; Col += 6)
			{
			if((MaskMatrix[Row, Col] & StaticTables.NonData) == 0) MaskMatrix[Row, Col] ^= 1;
			if((MaskMatrix[Row, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row, Col + 1] ^= 1;
			if((MaskMatrix[Row, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row, Col + 2] ^= 1;

			if((MaskMatrix[Row + 1, Col] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col] ^= 1;
			if((MaskMatrix[Row + 1, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 1] ^= 1;
			if((MaskMatrix[Row + 1, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 2] ^= 1;

			if((MaskMatrix[Row + 2, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 3] ^= 1;
			if((MaskMatrix[Row + 2, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 4] ^= 1;
			if((MaskMatrix[Row + 2, Col + 5] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 5] ^= 1;

			if((MaskMatrix[Row + 3, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 3] ^= 1;
			if((MaskMatrix[Row + 3, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 4] ^= 1;
			if((MaskMatrix[Row + 3, Col + 5] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 5] ^= 1;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask 5
	// ((row * column) % 2) + ((row * column) % 3) == 0
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask5()
		{
		for(int Row = 0; Row < QRCodeDimension; Row += 6) for(int Col = 0; Col < QRCodeDimension; Col += 6)
			{
			for(int Delta = 0; Delta < 6; Delta++) if((MaskMatrix[Row, Col + Delta] & StaticTables.NonData) == 0) MaskMatrix[Row, Col + Delta] ^= 1;
			for(int Delta = 1; Delta < 6; Delta++) if((MaskMatrix[Row + Delta, Col] & StaticTables.NonData) == 0) MaskMatrix[Row + Delta, Col] ^= 1;
			if((MaskMatrix[Row + 2, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 3] ^= 1;
			if((MaskMatrix[Row + 3, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 2] ^= 1;
			if((MaskMatrix[Row + 3, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 4] ^= 1;
			if((MaskMatrix[Row + 4, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 4, Col + 3] ^= 1;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask 6
	// (((row * column) % 2) + ((row * column) mod 3)) mod 2 == 0
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask6()
		{
		for(int Row = 0; Row < QRCodeDimension; Row += 6) for(int Col = 0; Col < QRCodeDimension; Col += 6)
			{
			for(int Delta = 0; Delta < 6; Delta++) if((MaskMatrix[Row, Col + Delta] & StaticTables.NonData) == 0) MaskMatrix[Row, Col + Delta] ^= 1;
			for(int Delta = 1; Delta < 6; Delta++) if((MaskMatrix[Row + Delta, Col] & StaticTables.NonData) == 0) MaskMatrix[Row + Delta, Col] ^= 1;
			if((MaskMatrix[Row + 1, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 1] ^= 1;
			if((MaskMatrix[Row + 1, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 2] ^= 1;
			if((MaskMatrix[Row + 2, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 1] ^= 1;
			if((MaskMatrix[Row + 2, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 3] ^= 1;
			if((MaskMatrix[Row + 2, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 4] ^= 1;
			if((MaskMatrix[Row + 3, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 2] ^= 1;
			if((MaskMatrix[Row + 3, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 4] ^= 1;
			if((MaskMatrix[Row + 4, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row + 4, Col + 2] ^= 1;
			if((MaskMatrix[Row + 4, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 4, Col + 3] ^= 1;
			if((MaskMatrix[Row + 4, Col + 5] & StaticTables.NonData) == 0) MaskMatrix[Row + 4, Col + 5] ^= 1;
			if((MaskMatrix[Row + 5, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row + 5, Col + 4] ^= 1;
			if((MaskMatrix[Row + 5, Col + 5] & StaticTables.NonData) == 0) MaskMatrix[Row + 5, Col + 5] ^= 1;
			}
		return;
		}

	////////////////////////////////////////////////////////////////////
	// Apply Mask 7
	// (((row + column) % 2) + ((row * column) mod 3)) mod 2 == 0
	////////////////////////////////////////////////////////////////////

	internal void ApplyMask7()
		{
		for(int Row = 0; Row < QRCodeDimension; Row += 6) for(int Col = 0; Col < QRCodeDimension; Col += 6)
			{
			if((MaskMatrix[Row, Col] & StaticTables.NonData) == 0) MaskMatrix[Row, Col] ^= 1;
			if((MaskMatrix[Row, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row, Col + 2] ^= 1;
			if((MaskMatrix[Row, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row, Col + 4] ^= 1;

			if((MaskMatrix[Row + 1, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 3] ^= 1;
			if((MaskMatrix[Row + 1, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 4] ^= 1;
			if((MaskMatrix[Row + 1, Col + 5] & StaticTables.NonData) == 0) MaskMatrix[Row + 1, Col + 5] ^= 1;

			if((MaskMatrix[Row + 2, Col] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col] ^= 1;
			if((MaskMatrix[Row + 2, Col + 4] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 4] ^= 1;
			if((MaskMatrix[Row + 2, Col + 5] & StaticTables.NonData) == 0) MaskMatrix[Row + 2, Col + 5] ^= 1;

			if((MaskMatrix[Row + 3, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 1] ^= 1;
			if((MaskMatrix[Row + 3, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 3] ^= 1;
			if((MaskMatrix[Row + 3, Col + 5] & StaticTables.NonData) == 0) MaskMatrix[Row + 3, Col + 5] ^= 1;

			if((MaskMatrix[Row + 4, Col] & StaticTables.NonData) == 0) MaskMatrix[Row + 4, Col] ^= 1;
			if((MaskMatrix[Row + 4, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row + 4, Col + 1] ^= 1;
			if((MaskMatrix[Row + 4, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row + 4, Col + 2] ^= 1;

			if((MaskMatrix[Row + 5, Col + 1] & StaticTables.NonData) == 0) MaskMatrix[Row + 5, Col + 1] ^= 1;
			if((MaskMatrix[Row + 5, Col + 2] & StaticTables.NonData) == 0) MaskMatrix[Row + 5, Col + 2] ^= 1;
			if((MaskMatrix[Row + 5, Col + 3] & StaticTables.NonData) == 0) MaskMatrix[Row + 5, Col + 3] ^= 1;
			}
		return;
		}
	private byte[] BuildPngHeader()
		{ 
		// header
		byte[] Header = new byte[25];
					
		// header length
		Header[0] = 0;
		Header[1] = 0;
		Header[2] = 0;
		Header[3] = 13;

		// header label
		Header[4] = (byte) 'I';
		Header[5] = (byte) 'H';
		Header[6] = (byte) 'D';
		Header[7] = (byte) 'R';

		// image width
		int ImageDimension = QRCodeImageDimension;
		Header[8] = (byte) (ImageDimension >> 24);
		Header[9] = (byte) (ImageDimension >> 16);
		Header[10] = (byte) (ImageDimension >> 8);
		Header[11] = (byte) ImageDimension;

		// image height
		Header[12] = (byte) (ImageDimension >> 24);
		Header[13] = (byte) (ImageDimension >> 16);
		Header[14] = (byte) (ImageDimension >> 8);
		Header[15] = (byte) ImageDimension;

		// bit depth (1)
		Header[16] = 1;

		// color type (grey)
		Header[17] = 0;

		// Compression (deflate)
		Header[18] = 0;

		// filtering (up)
		Header[19] = 0; // 2;

		// interlace (none)
		Header[20] = 0;

		// crc
		uint Crc = CRC32.Checksum(Header, 4, 17);
		Header[21] = (byte) (Crc >> 24);
		Header[22] = (byte) (Crc >> 16);
		Header[23] = (byte) (Crc >> 8);
		Header[24] = (byte) Crc;

		// return header
		return Header;
		}

	internal static byte[] PngImageData
			(
			byte[] InputBuf
			)
		{
		// output buffer is:
		// Png IDAT length 4 bytes
		// Png chunk type IDAT 4 bytes
		// Png chunk data made of:
		//		header 2 bytes
		//		compressed data DataLen bytes
		//		adler32 input buffer checksum 4 bytes
		// Png CRC 4 bytes
		// Total output buffer length is 18 + DataLen

		// compress image
		byte[] OutputBuf = ZLibCompression.Compress(InputBuf);

		// png chunk data length
		int PngDataLen = OutputBuf.Length - 12;
		OutputBuf[0] = (byte) (PngDataLen >> 24);
		OutputBuf[1] = (byte) (PngDataLen >> 16);
		OutputBuf[2] = (byte) (PngDataLen >> 8);
		OutputBuf[3] = (byte) PngDataLen;

		// add IDAT
		OutputBuf[4] = (byte) 'I';
		OutputBuf[5] = (byte) 'D';
		OutputBuf[6] = (byte) 'A';
		OutputBuf[7] = (byte) 'T';

		// adler32 checksum
		uint ReadAdler32 = Adler32.Checksum(InputBuf, 0, InputBuf.Length);

		// ZLib checksum is Adler32 write it big endian order, high byte first
		int AdlerPtr = OutputBuf.Length - 8;
		OutputBuf[AdlerPtr++] = (byte) (ReadAdler32 >> 24);
		OutputBuf[AdlerPtr++] = (byte) (ReadAdler32 >> 16);
		OutputBuf[AdlerPtr++] = (byte) (ReadAdler32 >> 8);
		OutputBuf[AdlerPtr] = (byte) ReadAdler32;

		// crc
		uint Crc = CRC32.Checksum(OutputBuf, 4, OutputBuf.Length - 8);
		int CrcPtr = OutputBuf.Length - 4;
		OutputBuf[CrcPtr++] = (byte) (Crc >> 24);
		OutputBuf[CrcPtr++] = (byte) (Crc >> 16);
		OutputBuf[CrcPtr++] = (byte) (Crc >> 8);
		OutputBuf[CrcPtr++] = (byte) Crc;

		// successful exit
		return OutputBuf;
		}

	// convert barcode matrix to PNG image format
	internal byte[] QRCodeMatrixToPng()
		{
		// image width and height
		int ImageDimension = this.QRCodeImageDimension;

		// width in bytes including filter leading byte
		int PngWidth = (ImageDimension + 7) / 8 + 1;

		// PNG image array
		// array is all zeros in other words it is black image
		int PngLength = PngWidth * ImageDimension;
		byte[] PngImage = new byte[PngLength];

		// first row is a quiet zone and it is all white (filter is 0 none)
		int PngPtr;
		for(PngPtr = 1; PngPtr < PngWidth; PngPtr++) PngImage[PngPtr] = 255;

		// additional quiet zone rows are the same as first line (filter is 2 up)
		int PngEnd = QuietZone * PngWidth;
		for(; PngPtr < PngEnd; PngPtr += PngWidth) PngImage[PngPtr] = 2;

		// convert result matrix to output matrix
		for(int MatrixRow = 0; MatrixRow < QRCodeDimension; MatrixRow++)
			{
			// make next row all white (filter is 0 none)
			PngEnd = PngPtr + PngWidth;
			for(int PngCol = PngPtr + 1; PngCol < PngEnd; PngCol++) PngImage[PngCol] = 255;

			// add black to next row
			for(int MatrixCol = 0; MatrixCol < QRCodeDimension; MatrixCol++)
				{
				// bar is white
				if(!QRCodeMatrix[MatrixRow, MatrixCol]) continue;

				int PixelCol = ModuleSize * MatrixCol + QuietZone;
				int PixelEnd = PixelCol + ModuleSize;
				for(; PixelCol < PixelEnd; PixelCol++)
					{ 
					PngImage[PngPtr + (1 + PixelCol / 8)] &= (byte) ~(1 << (7 - (PixelCol & 7)));
					}
				}

			// additional rows are the same as the one above (filter is 2 up)
			PngEnd = PngPtr + ModuleSize * PngWidth;
			for(PngPtr += PngWidth; PngPtr < PngEnd; PngPtr += PngWidth) PngImage[PngPtr] = 2;
			}

		// bottom quiet zone and it is all white (filter is 0 none)
		PngEnd = PngPtr + PngWidth;
		for(PngPtr++; PngPtr < PngEnd; PngPtr++) PngImage[PngPtr] = 255;

		// additional quiet zone rows are the same as first line (filter is 2 up)
		for(; PngPtr < PngLength; PngPtr += PngWidth) PngImage[PngPtr] = 2;

		return PngImage;
		}
    }
}
