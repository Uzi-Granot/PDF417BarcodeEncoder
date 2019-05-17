/////////////////////////////////////////////////////////////////////
//
//	QR Code Encoder Library
//
//	Adler32 checksum calculations
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

namespace QRCodeEncoderLibrary
{
internal static class Adler32
	{
	/////////////////////////////////////////////////////////////////////
	// Accumulate Adler Checksum
	/////////////////////////////////////////////////////////////////////
	internal static uint Checksum
			(
			byte[] Buffer,
			int Pos,
			int	Len
			)
		{
		const uint Adler32Base = 65521;

		// split current Adler chksum into two 
		uint AdlerLow = 1; // AdlerValue & 0xFFFF;
		uint AdlerHigh = 0; // AdlerValue >> 16;

		while(Len > 0) 
			{
			// We can defer the modulo operation:
			// Under worst case the starting value of the two halves is 65520 = (AdlerBase - 1)
			// each new byte is maximum 255
			// The low half grows AdlerLow(n) = AdlerBase - 1 + n * 255
			// The high half grows AdlerHigh(n) = (n + 1)*(AdlerBase - 1) + n * (n + 1) * 255 / 2
			// The maximum n before overflow of 32 bit unsigned integer is 5552
			// it is the solution of the following quadratic equation
			// 255 * n * n + (2 * (AdlerBase - 1) + 255) * n + 2 * (AdlerBase - 1 - UInt32.MaxValue) = 0
			Int32 n = Len < 5552 ? Len : 5552;
			Len -= n;
			while(--n >= 0) 
				{
				AdlerLow += (UInt32) Buffer[Pos++];
				AdlerHigh += AdlerLow;
				}
			AdlerLow %= Adler32Base;
			AdlerHigh %= Adler32Base;
			}
		return((AdlerHigh << 16) | AdlerLow);
		}
	}
}
