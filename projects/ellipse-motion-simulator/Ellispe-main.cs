//Author: Lavinia Hawkins
//Course: CPSC 223N
//Assignment 5
//Due: 2026-May-5
//Program name: Ellispe Ball
//Purpose:  This program simulates the motion of a ball moving at a constant speed along an ellispe.  The user can specify the 
//speed of the ball in pixels per second.  The user can also pause and resume the motion of the ball.  The current speed and 
//location of the ball are displayed in the lower panel of the user interface.
//Programming language: C Sharp
//Date development of program began: 2026-April-25
//Date of last update: N/A
//Files in project: Ellispe-main.cs, Ellispe-ui.cs, run.sh
//This file name: Ellispe-ui.cs
//This file's name: Ellispe-ui.cs
//This file purpose: This file will activate the user interface
//Date last modified: 2026-May-4

using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Ellispe
{  public static void Main()
   {  System.Console.WriteLine("The ellispe ball program will begin now.");
      //Declare the basic quantities needed for a straight line motion application: speed and direction.
      double speed = 81.9; //C# units, namely: pixels/sec.
      //Direction may be specified in either of two ways: (a) an angle number, or (b) a vector u⃗ = <v,w>
      //In this program we use the second choice.
      double v = 37.2;
      double w = 21.8;
      Ellispe_interface_form ellispe_application = new Ellispe_interface_form(speed,v,w);
      Application.Run(ellispe_application);
      System.Console.WriteLine("This ellispe ball program has ended.  Bye.");
   }//End of Main function
}//End of Movingballs class
