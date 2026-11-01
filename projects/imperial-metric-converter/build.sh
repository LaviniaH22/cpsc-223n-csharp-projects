#!/bin/bash
# //Author: Lavinia Hawkins
# //Course: CPSC 223N
# //Due: 2026-Feb-8
# //Program name: Imperial to Metric Conversion
# //Purpose:  This program will compute a given numeric value in imperial syestem(inches) to the metric system(meters)
# //Programming language: C Sharp
# //Date development of program began: 2026-Jan-31
# //Date of last update: N/A
# //Files in project: ImpToMetConmain.cs, ImpToMetConlogic.cs, ITMCuserinterface.cs
# //This file's name: ImpToMetConlogic.cs

echo First remove old binary files
rm -f *.dll
rm -f *.exe

echo View the list of source files
ls -l

echo Compile ImpToMetConlogic.cs to create the file: ImpToMetConlogic.dll
mcs -target:library -out:ImpToMetConlogic.dll ImpToMetConlogic.cs

echo Compile ITMCuserinterface.cs to create the file: ITMCuserinterface.dll
mcs -target:library -r:System.Drawing.dll -r:System.Windows.Forms.dll -r:ImpToMetConlogic.dll -out:ITMCuserinterface.dll ITMCuserinterface.cs

echo Compile ImpToMetConmain.cs and link the two previously created dll files to create an executable file. 
mcs -r:System -r:System.Windows.Forms -r:ITMCuserinterface.dll -out:ITMC.exe ImpToMetConmain.cs

echo View the list of files in the current folder
ls -l

echo Run the Assignment 1 program.
./ITMC.exe

echo The script has terminated.












