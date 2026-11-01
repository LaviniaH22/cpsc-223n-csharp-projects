//Files in this project: Ricochet2_main.cs, Ricochet2-ui.cs, run.sh
//Author: Lavinia Hawkins
//Course: CPSC 223N
//Assignment 4
//Due per syllabus: 2026-April-19
//Program name:   Collision Detection
//Purpose:  This program is design to simulate the motion of a two balls that ricochets off the walls of a rectangular area and detect thier eventual 
//collision. The user can input the initial speed and direction of the two balls, and then start the simulation. The program will display both balls 
//location and distance between them in real-time.  The program will stop when the two balls collide. In which case, the user can choose to re-play 
//the simulation with the same initial conditions or re-initialize the system with new initial conditions.
//Programming language: C Sharp
//Date development of program began: 2026-Mar-8
//Date of last update: N/A
//Files in project: Ricochet2-main.cs, Ricochet2-ui.cs, run.sh
//This file name: Ricochet2-ui.cs
//This file's name: Ricochet2-ui.cs
//This file purpose: This file will activate the user interface
//Date last modified:   2026-April-12

using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Ricochet
{  public static void Main()
   {  System.Console.WriteLine("The ricochet ball program will begin now.");
      //Declare the basic quantities needed for a straight line motion application: speed and direction.
      double speed = 81.9; //C# units, namely: pixels/sec.
      //Direction may be specified in either of two ways: (a) an angle number, or (b) a vector u⃗ = <v,w>
      //In this program we use the second choice.
      double v = 37.2;
      double w = 21.8;
      Ricochet_interface_form ricochet_application = new Ricochet_interface_form(speed,v,w);
      Application.Run(ricochet_application);
      System.Console.WriteLine("This ricochet ball program has ended.  Bye.");
   }//End of Main function
}//End of Movingballs class
