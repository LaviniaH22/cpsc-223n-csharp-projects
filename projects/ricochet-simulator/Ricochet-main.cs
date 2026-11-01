//****************************************************************************************************************************
//Program name: "Ricochet Ball".  This program shows a ball moving in a straight line.  When it reaches a wall it ricochets *
//off of that wall and continues its linear motion.                                                                          *
//Copyright (C) 2017  Floyd Holliday                                                                                         *
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License  *
//version 3 as published by the Free Software Foundation.                                                                    *
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied         *
//warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.     *
//A copy of the GNU General Public License v3 is available here:  <https://www.gnu.org/licenses/>.                           *
//****************************************************************************************************************************








﻿//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**

//Author: F. Holliday
//Author's email: holliday@fullerton.edu

//Program name: Ricochet Ball
//Programming language: C#
//Date project began: November 24, 2017  
//Date project last updated: November 24, 2017

//Purpose: This program is one in a series of programs used as teaching examples in the C# programming course.  This 
//program demonstrates how an elastic ball collides physically with another hard object -- in this case a wall.

//The direction vector <v,w> is hardcoded into main.  To change the direction of initial travel change that vector and 
//re-compile the program.  This program is intended to provide ideas to students regarding how to make a full ricochet
//program.

//Hardware limitation: This program was built on Linux machine with an ordinary video card: Nvidia GeForce 970 with 4GB 
//memory.  However, that card was not able to render the size of form I really wanted, namely: 1600x1200.  At that size
//there was extreme amount of flicker.  The only solution, other than get a better computer, was to reduce the form size
//to 1100x900.  At that educed size the machine was able to render the images in fast real-time.

//Improvements needed:
//1.  An exit button
//2.  Real-time instantaneous display of coordinates
//3.  Ricochet capability on the north, west, and south sides.
//4.  Hyper jumping: in place of ricochet cause the ball to re-appear on the left as it exits from the right.

//Files in this project: Ricochet_main.cs, Ricochet-ui.cs, run.sh
//Author: Lavinia Hawkins
//Course: CPSC 223N
//Assignment 3
//Due: 2026-March-22
//Program name: Ricochet Ball
//Purpose:  This program is design to simulate the motion of a ball that ricochets off the walls of a rectangular area.  
//The user can input the initial speed, direction, and location of the ball, and then start the simulation.  The user can 
//also adjust the speed of the ball during the simulation using buttons.  The program will display the current speed of the 
//ball and its location in real-time.  The program will stop when the ball goes beyond the boundaries of the rectangular area.  
//This program is intended to provide ideas to students regarding how to make a full ricochet program.
//Programming language: C Sharp
//Date development of program began: 2026-Mar-8
//Date of last update: N/A
//Files in project: Ricochet-main.cs, Ricochet-ui.cs, run.sh
//This file name: Ricochet-ui.cs
//This file's name: Ricochet-ui.cs
//This file purpose: This file will activate the user interface
//Date last modified: 2026-Mar-16

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
