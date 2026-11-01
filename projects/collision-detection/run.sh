#!/bin/bash

# //Author: Lavinia Hawkins
# //Course: CPSC 223N
# //Assignment 4
# //Due per syllabus: 2026-April-19
# //Program name:   Collision Detection
# //Purpose:  This program is design to simulate the motion of a two balls that ricochets off the walls of a rectangular area and detect thier eventual 
# //collision. The user can input the initial speed and direction of the two balls, and then start the simulation. The program will display both balls 
# //location and distance between them in real-time.  The program will stop when the two balls collide. In which case, the user can choose to re-play 
# //the simulation with the same initial conditions or re-initialize the system with new initial conditions.
# //Programming language: C Sharp
# //Date development of program began: 2026-April-10
# //Date of last update: N/A
# //Files in project: Ricochet2-main.cs, Ricochet2-ui.cs, run.sh
# //This file name: run.sh
# //This file purpose: This file will compile the program and run the executable file
# //Date last modified: 2026-April-12


mcs -target:library Ricochet2-ui.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:Ricochet2-ui.dll


mcs Ricochet2-main.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Ricochet2-ui.dll -out:Ricochet2.exe


./Ricochet2.exe
