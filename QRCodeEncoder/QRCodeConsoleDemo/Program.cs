/////////////////////////////////////////////////////////////////////
//
//	QR Code Encoder Library
//
//	QR Code console program
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
using System.IO;
using QRCodeEncoderLibrary;

namespace QRCodeConsoleDemo
{
class Program
	{
	static void Main(string[] args)
		{
		#if DEBUG
		// current directory
		string CurDir = Environment.CurrentDirectory;
		int Ptr = CurDir.IndexOf("\\bin\\debug", StringComparison.OrdinalIgnoreCase);
		if(Ptr > 0)
			{
			string WorkDir = CurDir.Substring(0, Ptr) + "\\Work";
			if(WorkDir != CurDir && Directory.Exists(WorkDir)) Environment.CurrentDirectory = WorkDir;
			}
		#endif

		try
			{
			QRCodeCommandLine.Encode(args);
			Console.WriteLine("Success");
			}
		catch (Exception Ex)
			{
			if(Ex.Message == "help")
				Console.WriteLine(QRCodeCommandLine.Help);
			else
				Console.WriteLine("Error:\r\n" + Ex.Message);
			}

		#if DEBUG
		Console.WriteLine("Press any key to close window.");
		Console.ReadKey();
		#endif
		}
	}
}
