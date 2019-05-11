using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdf417EncoderLibrary
{
public static class Pdf417CommandLine
	{
	public static readonly string Help =
		"PDF417 barcode encoder console application support\r\n" +
		"AppName [optional arguments] input-file output-file\r\n" +
		"Output file must have .png extension\r\n" +
		"Options format /code:value or -code:value (the : can be =)\r\n" +
		"Encoding control. code=[encode|n], value=[auto|a|byte|b|text|t], default=a\r\n" +
		"Error correction level. code=[error|e], value=[0-8|low|l|normal|n|medium|m|high|h], default=n\r\n" +
		"Narrow bar width. code=[width|w], value=[1-100], default=2\r\n" +
		"Row height. code=[height|h], value=[3-300], default=6, min=3*width\r\n" +
		"Quiet zone. code=[quiet|q], value=[2-200], default=4, min=2*width\r\n" + 
		"Layout options. Only one of data columns, data rows or image ratio\r\n" +
		"Data columns. code=[col|c], value=[1-30], default=3\r\n" +
		"Data rows. code=[row|r], value=[3-90], no default\r\n" +
		"Image ratio (width/height). code=[ratio|o], value=[decimal > 0], no default\r\n" +
		"Text file format. code=[text|t], value=iso-8859-n, see notes below\r\n" +
		"Input file is binary unless text file option is specified\r\n" +
		"If input file format is text or t with no value, character set is iso-8859-1\r\n" +
		"If input file format is text or t value must be iso-8859-n and n must be 1-9, 13, 15\r\n";

	public static void Encode
			(
			string ArgumentsLine
			)
		{
		if(ArgumentsLine.IndexOf('"') < 0)
			{ 
			Encode(ArgumentsLine.Split(new char[] {' '}));
			return;
			}
		List<string> Args = new List<string>();
		int Ptr = 0;
		int Ptr1 = 0;
		int Ptr2 = 0;
		for(;;)
			{
			// skip white
			for(; Ptr < ArgumentsLine.Length && ArgumentsLine[Ptr] == ' '; Ptr++);
			if(Ptr == ArgumentsLine.Length) break;

			// test for quote
			if(ArgumentsLine[Ptr] == '"')
				{
				// look for next quote
				Ptr++;
				Ptr1 = ArgumentsLine.IndexOf('"', Ptr);
				if(Ptr1 < 0) throw new ArgumentException("Unbalanced double quote");
				Ptr2 = Ptr1 + 1;
				}
			else
				{
				// look for next white
				Ptr1 = ArgumentsLine.IndexOf(' ', Ptr);
				if(Ptr1 < 0) Ptr1 = ArgumentsLine.Length;
				Ptr2 = Ptr1;
				}
			Args.Add(ArgumentsLine.Substring(Ptr, Ptr1 - Ptr));
			Ptr = Ptr2;
			}
		Encode(Args.ToArray());
		return;
		}

	public static void Encode
			(
			string[] Args
			)
		{
		// help
		if(Args == null || Args.Length < 2) throw new ArgumentException("help");

		int DataColumns = 0;
		int DataRows = 0;
		double Ratio = 0;
		bool TextFile = false;
		string CharacterSet = null;
		string InputFileName = null;
		string OutputFileName = null;
		string Code;
		string Value;

		Pdf417Encoder Encoder = new Pdf417Encoder();

		for(int ArgPtr = 0; ArgPtr < Args.Length; ArgPtr++)
			{
			string Arg = Args[ArgPtr];
			
			// file name
			if(Arg[0] != '/' && Arg[0] != '-')
				{
				if(InputFileName == null)
					{
					InputFileName = Arg;
					continue;
					}
				if(OutputFileName == null)
					{
					OutputFileName = Arg;
					continue;
					}
				throw new ArgumentException(string.Format("Invalid option. Argument={0}", ArgPtr + 1));
				}

			// search for colon
			int Ptr = Arg.IndexOf(':');
			if(Ptr < 0) Ptr = Arg.IndexOf('=');
			if(Ptr > 0)
				{
				Code = Arg.Substring(1, Ptr - 1);
				Value = Arg.Substring(Ptr + 1);
				}
			else
				{
				Code = Arg.Substring(1);
				Value = string.Empty;
				}

			Code = Code.ToLower();
			Value = Value.ToLower();

			switch(Code)
				{
				case "encode":
				case "n":
					EncodingControl EC;
					switch(Value)
						{
						case "auto":
						case "a":
							EC = EncodingControl.Auto;
							break;
						
						case "byte":
						case "b":
							EC = EncodingControl.ByteOnly;
							break;
						
						case "text":
						case "t":
							EC = EncodingControl.TextAndByte;
							break;

						default:
							throw new ArgumentException("Encoding control option in error");
						}
					Encoder.EncodingControl = EC;
					break;
			
				case "error":
				case "e":
					ErrorCorrectionLevel ECL;
					switch(Value)
						{
						case "0":
							ECL = ErrorCorrectionLevel.Level_0;
							break;

						case "1":
							ECL = ErrorCorrectionLevel.Level_1;
							break;

						case "2":
							ECL = ErrorCorrectionLevel.Level_2;
							break;

						case "3":
							ECL = ErrorCorrectionLevel.Level_3;
							break;

						case "4":
							ECL = ErrorCorrectionLevel.Level_4;
							break;

						case "5":
							ECL = ErrorCorrectionLevel.Level_5;
							break;

						case "6":
							ECL = ErrorCorrectionLevel.Level_6;
							break;

						case "7":
							ECL = ErrorCorrectionLevel.Level_7;
							break;

						case "8":
							ECL = ErrorCorrectionLevel.Level_8;
							break;

						case "low":
						case "l":
							ECL = ErrorCorrectionLevel.AutoLow;
							break;

						case "normal":
						case "n":
							ECL = ErrorCorrectionLevel.AutoNormal;
							break;

						case "medium":
						case "m":
							ECL = ErrorCorrectionLevel.AutoMedium;
							break;

						case "high":
						case "h":
							ECL = ErrorCorrectionLevel.AutoHigh;
							break;

						default:
							throw new ArgumentException("Error correction level option in error");
						}
					Encoder.ErrorCorrection = ECL;
					break;
			
				case "width":
				case "w":
					if(!int.TryParse(Value, out int BarWidth)) BarWidth = -1;
					Encoder.NarrowBarWidth = BarWidth;
					break;
			
				case "height":
				case "h":
					if(!int.TryParse(Value, out int RowHeight)) RowHeight = -1;
					Encoder.RowHeight = RowHeight;
					break;
			
				case "quiet":
				case "q":
					if(!int.TryParse(Value, out int QuietZone)) QuietZone = -1;
					Encoder.QuietZone = QuietZone;
					break;

				case "col":
				case "c":
					if(DataRows != 0 || Ratio != 0)
						throw new ArgumentException("Only one value is allowed for Data Rows, Data Columns and Ratio");
					if(!int.TryParse(Value, out DataColumns) || DataColumns < 1 || DataColumns > 30)
						throw new ApplicationException("Data columns in error");
					break;
			
				case "row":
				case "r":
					if(DataColumns != 0 || Ratio != 0)
						throw new ArgumentException("Only one value is allowed for Data Rows, Data Columns and Ratio");
					if(!int.TryParse(Value, out DataRows) || DataRows < 3 || DataRows > 90)
						throw new ApplicationException("Data Rows in error");
					break;
			
				case "ratio":
				case "o":
					if(DataRows != 0 || DataColumns != 0)
						throw new ArgumentException("Only one value is allowed for Data Rows, Data Columns and Ratio");
					if(!double.TryParse(Value, out Ratio) || Ratio < 0.1 || Ratio > 100)
						throw new ApplicationException("Image aspect ratio (width/height) in error");
					break;
			
				case "text":
				case "t":
					TextFile = true;
					if(Value != string.Empty) CharacterSet = Value; 
					break;
			
				default:
					throw new ApplicationException(string.Format("Invalid argument no {0}, code {1}", ArgPtr + 1, Code));
				}
			}

		if(TextFile)
			{
			string InputText = File.ReadAllText(InputFileName);
			Encoder.Encode(InputText);
			}
		else
			{
			byte[] InputBytes = File.ReadAllBytes(InputFileName);
			Encoder.Encode(InputBytes);
			}

		if(DataColumns != 0)
			{ 
			if(!Encoder.SetDataColumns(DataColumns))
				{
				throw new ApplicationException("Set data columns failed");
				}
			}
		else if(DataRows != 0)
			{ 
			if(!Encoder.SetDataRows(DataRows))
				{
				throw new ApplicationException("Set data rows failed");
				}
			}
		else if(Ratio != 0)
			{ 
			if(!Encoder.WidthToHeightRatio(Ratio))
				{
				throw new ApplicationException("Set width to height aspect ratio failed");
				}
			}

		Encoder.SaveBarcodeToPngFile(OutputFileName);
		return;
		}
	}
}
