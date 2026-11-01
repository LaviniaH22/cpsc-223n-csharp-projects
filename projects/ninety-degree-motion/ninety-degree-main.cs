//You agreed to accept Assignment 2 on this date March 1, 2026
//Author: Lavinia Hawkins
//Course: CPSC 223N
//Assignment: 2
//Due: 2026-Feb-8
//Program name: Racing Ball
//Purpose:  This program will create a user interface in which a ball will race along a polygonal line that has four vertices.  
//The ball will start at the upper right vertex and move counterclockwise until it returns to the starting point.  The user can 
//control the speed of the ball and can start
//Programming language: C Sharp
//Date development of program began: 2026-Feb-21
//Date of last update: N/A
//Files in project: ninety-degree-ui.cs ninety-degree-main.cs r.sh
//This file's name: ninety-degree-main.cs
//This file purpose: This file will excute the main function that will activate the user interface
//Date last modified: 2026-Feb-28




using System;
using System.Windows.Forms;            //Needed for "Application" near the end of Main function.
public class Straight_line_main
{  public static void Main()
   {  System.Console.WriteLine("The Ninety Degree Turning program has begun.");
      Ninety_degree_turn_interface turn = new Ninety_degree_turn_interface();
      Application.Run(turn);
      System.Console.WriteLine("The Ninety Degree Turning program has ended.  Bye.");
   }//End of Main function
}//End of Straight_line_main class






//Ruler:=1=========2=========3=========4=========5=========6=========7=========8=========9=========0=========1=========2=========3**



