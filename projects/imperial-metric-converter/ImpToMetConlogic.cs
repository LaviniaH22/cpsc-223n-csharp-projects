//Author: Lavinia Hawkins
//Course: CPSC 223N
//Due: 2026-Feb-8
//Program name: Imperial to Metric Conversion
//Purpose:  This program will compute a given numeric value in imperial syestem(inches) to the metric system(meters)
//Programming language: C Sharp
//Date development of program began: 2026-Jan-31
//Date of last update: N/A
//Files in project: ImpToMetConmain.cs, ImpToMetConlogic.cs, ITMCuserinterface.cs
//This file's name: ImpToMetConlogic.cs
//This file purpose: This file contains the alogrithm for computing the correct conversion from inches to meters
public class ImpToMetConlogic
{
 public static double computeImpToMetConnumber(double inchnum)
   {
      double CONRATE = 0.0254;
      return inchnum * CONRATE;
   }//End of computeImpToMetConnumber

}//End of ImpToMetConlogic
