/////////////////////////////////////////////////////////////////////
//
//	PDF417 Barcode Encoder
//
//	Perspective transformation class
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
using System.Drawing;

namespace Pdf417EncoderDemo
{
/// <summary>
///	Create a Pdf417 barcode image for testing. The image is transformed
///	using perspective algorithm.
/// </summary>
internal class Perspective
	{
	private double CenterX;
	private double CenterY;
	private double CosRot;
	private double SinRot;
	private double CamDist;
	private double CosX;
	private double SinX;
	private double CamVectY;
	private double CamVectZ;
	private double CamPosY;
	private double CamPosZ;

	internal Perspective
			(
			double CenterX,
			double CenterY,
			double ImageRot,
			double CamDist,
			double RotX
			)
		{
		// center position
		this.CenterX = CenterX;
		this.CenterY = CenterY;

		// image rotation
		double RotRad = Math.PI * ImageRot / 180.0;
		CosRot = Math.Cos(RotRad);
		SinRot = Math.Sin(RotRad);
 
		// camera distance from Pdf417 barcode
		this.CamDist = CamDist;

		// x and z axis rotation constants
		double RotXRad = Math.PI * RotX / 180.0;
		CosX = Math.Cos(RotXRad);
		SinX = Math.Sin(RotXRad);

		// camera vector relative to Pdf417 barcode image
		CamVectY = SinX;
		CamVectZ = CosX;

		// camera position relative to Pdf417 barcode image
		CamPosY =  CamDist * CamVectY;
		CamPosZ =  CamDist * CamVectZ;

		// exit
		return;
		}

	// screen equation
	// CamVectX * X + CamVectY * Y + CamVectZ * Z = 0

	// line equations between Pdf417 barcode point to camera position
	// X = PosX + (CamPosX - PosX) * T
	// Y = PosY + (CamPosY - PosZ) * T
	// Z = PosZ + (CamPosZ - PosZ) * T

	// line intersection with screen
	// CamVectX * (PosX + (CamPosX - PosX) * T) +
	//		CamVectY * (PosY + (CamPosY - PosY) * T) +
	//			CamVectZ * (PosZ + (CamPosZ - PosZ) * T) = 0
	//
	// (CamVectX * (CamPosX - PosX) + CamVectY * (CamPosY - PosY) + CamVectZ * (CamPosZ - PosZ)) * T =
	//		- CamVectX * PosX - CamVectY * PosY - CamVectZ * PosZ;
	//
	//	T = -(CamVectX * PosX + CamVectY * PosY + CamVectZ * PosZ) /
	//		(CamVectX * (CamPosX - PosX) + CamVectY * (CamPosY - PosY) + CamVectZ * (CamPosY - PosZ));
	//	Q = CamVectX * PosX + CamVectY * PosY + CamVectZ * PosZ
	//	T = Q / (Q - CamDist)

	internal PointF ScreenPosition
			(
			double BarcodePosX,
			double BarcodePosY
			)
		{
		// rotation
		double PosX = CosRot * BarcodePosX - SinRot * BarcodePosY;
		double PosY = SinRot * BarcodePosX + CosRot * BarcodePosY;

		// temp values for intersection calclulation
		double CamToBarcode = CamVectY * PosY;
		double T = CamToBarcode / (CamToBarcode - CamDist);

		// screen position relative to screen center
		double ScrnPosX = CenterX + PosX * (1 - T);
		double TempPosY = PosY + (CamPosY - PosY) * T;
		double TempPosZ = CamPosZ * T; // - ScrnCenterZ;

		// rotate around x axis
		double ScrnPosY = CenterY + TempPosY * CosX - TempPosZ * SinX;

		// program test
		#if DEBUG
		double ScrnPosZ = TempPosY * SinX + TempPosZ * CosX;
		if(Math.Abs(ScrnPosZ) > 0.0001) throw new ApplicationException("Screen Z position must be zero");
		#endif

		return new PointF((float) ScrnPosX, (float) ScrnPosY);
		}

	internal void GetPolygon
			(
			double PosX,
			double PosY,
			double Width,
			double Height,
			PointF[] Polygon
			)
		{
		Polygon[0] = ScreenPosition(PosX, PosY);
		Polygon[1] = ScreenPosition(PosX + Width, PosY);
		Polygon[2] = ScreenPosition(PosX + Width, PosY + Height);
		Polygon[3] = ScreenPosition(PosX, PosY + Height);
		return;
		}
	}
}
