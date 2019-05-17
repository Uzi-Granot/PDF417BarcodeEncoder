/////////////////////////////////////////////////////////////////////
//
//	QR Code Encoder Library
//
//	QR Code Encoder command line
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

using System;
using System.Collections.Generic;
using System.IO;

namespace QRCodeEncoderLibrary
{
/// <summary>
/// Command line class
/// </summary>
public static class QRCodeCommandLine
	{
	/// <summary>
	/// Command line help text
	/// </summary>
	public static readonly string Help =
		"QRCode encoder console application support\r\n" +
		"AppName [optional arguments] input-file output-file\r\n" +
		"Output file must have .png extension\r\n" +
		"Options format /code:value or -code:value (the : can be =)\r\n" +
		"Error correction level. code=[error|e], value=[low|l|medium|m|quarter|q||high|h], default=m\r\n" +
		"Module size. code=[module|m], value=[1-100], default=2\r\n" +
		"Quiet zone. code=[quiet|q], value=[2-200], default=4, min=2*width\r\n" + 
		"Text file format. code=[text|t] see notes below\r\n" +
		"Input file is binary unless text file option is specified\r\n" +
		"If input file format is text character set is iso-8859-1\r\n";

	/// <summary>
	/// Encode QRCode using command line class
	/// </summary>
	/// <param name="CommandLine">Command line text</param>
	public static void Encode
			(
			string CommandLine
			)
		{
		// command line has no quote characters
		if(CommandLine.IndexOf('"') < 0)
			{ 
			Encode(CommandLine.Split(new char[] {' '}));
			return;
			}

		// command line has quote characters
		List<string> Args = new List<string>();
		int Ptr = 0;
		int Ptr1 = 0;
		int Ptr2 = 0;
		for(;;)
			{
			// skip white
			for(; Ptr < CommandLine.Length && CommandLine[Ptr] == ' '; Ptr++);
			if(Ptr == CommandLine.Length) break;

			// test for quote
			if(CommandLine[Ptr] == '"')
				{
				// look for next quote
				Ptr++;
				Ptr1 = CommandLine.IndexOf('"', Ptr);
				if(Ptr1 < 0) throw new ArgumentException("Unbalanced double quote");
				Ptr2 = Ptr1 + 1;
				}
			else
				{
				// look for next white
				Ptr1 = CommandLine.IndexOf(' ', Ptr);
				if(Ptr1 < 0) Ptr1 = CommandLine.Length;
				Ptr2 = Ptr1;
				}
			Args.Add(CommandLine.Substring(Ptr, Ptr1 - Ptr));
			Ptr = Ptr2;
			}
		Encode(Args.ToArray());
		return;
		}

	/// <summary>
	/// Command line encode
	/// </summary>
	/// <param name="Args">Arguments array</param>
	public static void Encode
			(
			string[] Args
			)
		{
		// help
		if(Args == null || Args.Length < 2) throw new ArgumentException("help");

		bool TextFile = false;
		string InputFileName = null;
		string OutputFileName = null;
		string Code;
		string Value;

		QREncoder Encoder = new QREncoder();

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
				case "error":
				case "e":
					ErrorCorrection EC;
					switch(Value)
						{
						case "low":
						case "l":
							EC = ErrorCorrection.L;
							break;

						case "medium":
						case "m":
							EC = ErrorCorrection.M;
							break;

						case "quarter":
						case "q":
							EC = ErrorCorrection.Q;
							break;

						case "high":
						case "h":
							EC = ErrorCorrection.H;
							break;

						default:
							throw new ArgumentException("Error correction option in error");
						}
					Encoder.ErrorCorrection = EC;
					break;
			
				case "module":
				case "m":
					if(!int.TryParse(Value, out int ModuleSize)) ModuleSize = -1;
					Encoder.ModuleSize = ModuleSize;
					break;
			
				case "quiet":
				case "q":
					if(!int.TryParse(Value, out int QuietZone)) QuietZone = -1;
					Encoder.QuietZone = QuietZone;
					break;

				case "text":
				case "t":
					TextFile = true;
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

		Encoder.SaveQRCodeToPngFile(OutputFileName);
		return;
		}
	}
}
