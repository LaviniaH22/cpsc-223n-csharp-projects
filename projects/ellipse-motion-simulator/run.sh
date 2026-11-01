#!/bin/bash

# //Author: Lavinia Hawkins
# //Course: CPSC 223N
# //Assignment 5
# //Due: 2026-May-5
# //Program name: Ellispe Ball
# //Purpose:  This program simulates the motion of a ball moving at a constant speed along an ellispe.  The user can specify the 
# //speed of the ball in pixels per second.  The user can also pause and resume the motion of the ball.  The current speed and 
# //location of the ball are displayed in the lower panel of the user interface.
# //Programming language: C Sharp
# //Date development of program began: 2026-April-25
# //Date of last update: 2026-May-4
# //Files in project: Ellispe-main.cs, Ellispe-ui.cs, run.sh
# //This file name: run.sh
# //This file purpose: This file will compile the program and run the executable file
# //Date last modified: 2026-May-4


mcs -target:library Ellispe-ui.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:Ellispe-ui.dll


mcs Ellispe-main.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Ellispe-ui.dll -out:Ellispe.exe


mono ./Ellispe.exe
