#!/bin/bash

#//You agreed to accept Assignment 2 on this date March 1, 2026
# //Author: Lavinia Hawkins
# //Course: CPSC 223N
#//Assignment: 2
# //Due: 2026-Feb-8
# //Program name: Racing Ball
# //Purpose:  This program will create a user interface in which a ball will race along a polygonal line that has four vertices.  
# //The ball will start at the upper right vertex and move counterclockwise until it returns to the starting point.  The user can 
# //control the speed of the ball and can start
# //Programming language: C Sharp
# //Date development of program began: 2026-Feb-21
# //Date of last update: N/A
# //Files in project: ninety-degree-ui.cs ninety-degree-main.cs r.sh
# //This file's name: r.sh
# //This file purpose: This file will compile the source code files and run the program
# //Date last modified: 2026-Feb-28

echo First remove old binary files
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo "Compile the file ninety-degree-ui.cs:"
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -out:ninety-degree-ui.dll ninety-degree-ui.cs

echo "Compile and link ninety-degree-main.cs:"
mcs -r:System -r:System.Windows.Forms -r:ninety-degree-ui.dll -out:go.exe ninety-degree-main.cs

echo "Run the program Ninety Degree Turn"
./go.exe

echo "The bash script has terminated."

