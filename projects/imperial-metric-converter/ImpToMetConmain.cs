//Author: Lavinia Hawkins
//Course: CPSC 223N
//Due: 2026-Feb-8
//Program name: Imperial to Metric Conversion
//Purpose:  This program will compute a given numeric value in imperial syestem(inches) to the metric system(meters)
//Programming language: C Sharp
//Date development of program began: 2026-Jan-31
//Date of last update: N/A
//Files in project: ImpToMetConmain.cs, ImpToMetConlogic.cs, ITMCuserinterface.cs
//This file's name: ImpToMetConmain.cs
//This file purpose: This file will activate the user interface
//Date last modified: 2021-Aug-25
using System;
//using System.Drawing;
using System.Windows.Forms;  //Needed for "Application" on next to last line of Main
public class computeImpToMetConnumbermain
{  static void Main(string[] args)
   {System.Console.WriteLine("Welcome to the Main method of the Imperial to Metric Conversion program.");
    ITMCuserinterface itmcapp = new ITMCuserinterface();
    Application.Run(itmcapp);
    System.Console.WriteLine("Main method will now shutdown.");
   }//End of Main
}//End of ImpToMetCon
