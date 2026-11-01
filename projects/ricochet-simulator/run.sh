#!/bin/bash

# //Author: Lavinia Hawkins
# //Course: CPSC 223N
# //Assignment 3
# //Due: 2026-March-22
# //Program name: Ricochet Ball
# //Purpose:  This program is design to simulate the motion of a ball that ricochets off the walls of a rectangular area.  
# //The user can input the initial speed, direction, and location of the ball, and then start the simulation.  The user can 
# //also adjust the speed of the ball during the simulation using buttons.  The program will display the current speed of the 
# //ball and its location in real-time.  The program will stop when the ball goes beyond the boundaries of the rectangular area.  
# //This program is intended to provide ideas to students regarding how to make a full ricochet program.
# //Programming language: C Sharp
# //Date development of program began: 2026-Mar-8
# //Date of last update: N/A
# //Files in project: Ricochet-main.cs, Ricochet-ui.cs, run.sh
# //This file name: run.sh
# //This file purpose: This file will compile the program and run the executable file
# //Date last modified: 2026-Mar-16


mcs -target:library Ricochet-ui.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:Ricochet-ui.dll


mcs Ricochet-main.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Ricochet-ui.dll -out:Rico.exe


./Rico.exe
