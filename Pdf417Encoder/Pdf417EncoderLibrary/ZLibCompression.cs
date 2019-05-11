/////////////////////////////////////////////////////////////////////
//
//	PDF417 Barcode Encoder
//
//	Pdf417Encoder class
//
//	Author: Uzi Granot
//	Version: 2.0
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
using System.IO;
using System.IO.Compression;

namespace Pdf417EncoderLibrary
{
internal static class ZLibCompression
	{
	internal static byte[] Compress
			(
			byte[] InputBuf
			)
		{
		// input length
		int InputLen = InputBuf.Length;

		// create output memory stream to receive the compressed buffer
		MemoryStream OutputStream = new MemoryStream();

		// deflate compression object
		DeflateStream Deflate = new DeflateStream(OutputStream, CompressionMode.Compress, true);

		// load input buffer into the compression class
		Deflate.Write(InputBuf, 0, InputLen);

		// compress, flush and close
		Deflate.Close();

		// compressed file length
		int OutputLen = (int) OutputStream.Length;

		// create empty output buffer
		byte[] OutputBuf = new Byte[OutputLen + 18];

		// Header is made out of 16 bits [iiiicccclldxxxxx]
		// iiii is compression information. It is WindowBit - 8 in this case 7. iiii = 0111
		// cccc is compression method. Deflate (8 dec) or Store (0 dec)
		// The first byte is 0x78 for deflate and 0x70 for store
		// ll is compression level 2
		// d is preset dictionary. The preset dictionary is not supported by this program. d is always 0
		// xxx is 5 bit check sum (31 - header % 31)
		// write two bytes in most significant byte first
		OutputBuf[8] = 0x78;
		OutputBuf[9] = 0x9c;

		// copy the compressed result
		OutputStream.Seek(0, SeekOrigin.Begin);
		OutputStream.Read(OutputBuf, 10, OutputLen);
		OutputStream.Close();

		// successful exit
		return OutputBuf;
		}
	}
}
