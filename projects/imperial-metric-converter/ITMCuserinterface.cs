///Author: Lavinia Hawkins
//Course: CPSC 223N
//Due: 2026-Feb-8
//Program name: Imperial to Metric Conversion
//Purpose:  This program will compute a given numeric value in imperial syestem(inches) to the metric system(meters)
//Programming language: C Sharp
//Date development of program began: 2026-Jan-31
//Date of last update: N/A
//Files in project: ImpToMetConmain.cs, ImpToMetConlogic.cs, ITMCuserinterface.cs
//This file's name: ITMCuserinterface.cs
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;




public class ITMCuserinterface: Form
{private Label welcome = new Label();
 private Label author = new Label();
 private Label inchlabel = new Label();
 private TextBox inchinput = new TextBox();
 private Label outputinfo = new Label();
 private Button computebutton = new Button();
 private Button clearbutton = new Button();
 private Button exitbutton = new Button();
 private Panel headerpanel = new Panel();
 private Graphicpanel displaypanel = new Graphicpanel();
 private Panel controlpanel = new Panel();
 private Size maximumITMCinterfacesize = new Size(1024,800);
 private Size minimumITMCinterfacesize = new Size(1024,800);
 private enum Status {Initial_display,Successful_calculation,Error};
 private static Status outcome = Status.Initial_display;
 private enum Execution_state {Executing, Waiting_to_terminate};             //<== New in version 2.2
 private Execution_state current_state = Execution_state.Executing;
 private static System.Timers.Timer exit_clock = new System.Timers.Timer();  //<== New in version 2.2
 

 public ITMCuserinterface()  //Constructor begins here
   {//Set the size of the user interface box.
    MaximumSize = maximumITMCinterfacesize;
    MinimumSize = minimumITMCinterfacesize;
    //Initialize text strings
    Text = "";
    welcome.Text = "Welcome to Imperial to Metric Conversion Calculator";
    author.Text = "Author: Lavinia Hawkins";
    inchlabel.Text = "Enter inches:";
    inchinput.Text = "";
    outputinfo.Text = "Result will display here.";
    computebutton.Text = "Compute";
    clearbutton.Text = "Clear";
    exitbutton.Text = "Exit";
    
    //Set sizes
    Size = new Size(1024,800);
    welcome.Size = new Size(1000,130);
    author.Size = new Size(864,60);
    inchlabel.Size = new Size(480,100);
    inchinput.Size = new Size(300,66);
    outputinfo.Size = new Size(944,180);   //This label has a large height to accommodate 2 lines output text. 
    computebutton.Size = new Size(310,130);
    clearbutton.Size = new Size(310,130);
    exitbutton.Size = new Size(310,130);
    headerpanel.Size = new Size(1024,200);
    displaypanel.Size = new Size(1024,400);
    controlpanel.Size = new Size(1024,200);
    //intalizing colors
      Color navyBackground = Color.FromArgb(3, 0, 46);     // #03002e
      Color headerBlue = Color.FromArgb(1, 0, 72);         // #010048
      Color displayBlue = Color.FromArgb(1, 0, 87);        // #010057
      Color controlBlue = Color.FromArgb(2, 0, 108);       // #02006c
      Color accentBlue = Color.FromArgb(9, 0, 136);        // #090088

      Color whiteText = Color.FromArgb(255, 255, 255);     // pure white

      Color computeVibrant = Color.FromArgb(0, 120, 255);  // vibrant blue
      Color clearVibrant = Color.FromArgb(0, 180, 255);    // vibrant cyan
      Color exitVibrant = Color.FromArgb(230, 50, 50);     // vibrant red

      Color inputWhite = Color.FromArgb(255, 255, 255);
      Color inputBlack = Color.FromArgb(0, 0, 0);
    //Setting colors
      this.BackColor = inputBlack;

      headerpanel.BackColor = navyBackground;
      displaypanel.BackColor = controlBlue;
      controlpanel.BackColor = headerBlue;

      headerpanel.BorderStyle = BorderStyle.FixedSingle;
      displaypanel.BorderStyle = BorderStyle.FixedSingle;
      controlpanel.BorderStyle = BorderStyle.FixedSingle;

      welcome.ForeColor = whiteText;
      author.ForeColor = whiteText;
      inchlabel.ForeColor = whiteText;
      outputinfo.ForeColor = whiteText;

      outputinfo.BackColor = displaypanel.BackColor;
      outputinfo.BorderStyle = BorderStyle.None;

      inchinput.BackColor = inputWhite;
      inchinput.ForeColor = inputBlack;

      computebutton.BackColor = computeVibrant;
      computebutton.ForeColor = whiteText;

      clearbutton.BackColor = clearVibrant;
      clearbutton.ForeColor = inputBlack;

      exitbutton.BackColor = exitVibrant;
      exitbutton.ForeColor = whiteText;

      computebutton.FlatStyle = FlatStyle.Popup;
      clearbutton.FlatStyle = FlatStyle.Popup;
      exitbutton.FlatStyle = FlatStyle.Popup;

        

    
    //Set fonts
    welcome.Font = new Font("Times New Roman",43,FontStyle.Bold);
    author.Font = new Font("Times New Roman",36,FontStyle.Regular);
    inchlabel.Font = new Font("Arial",60,FontStyle.Regular);
    inchinput.Font = new Font("Arial",62,FontStyle.Regular);
    outputinfo.Font = new Font("Arial",52,FontStyle.Regular);
    computebutton.Font = new Font("Liberation Serif",50,FontStyle.Regular);
    clearbutton.Font = new Font("Liberation Serif",50,FontStyle.Regular);
    exitbutton.Font = new Font("Liberation Serif",50,FontStyle.Regular);
    
    //Set position of text within a label
    welcome.TextAlign = ContentAlignment.MiddleCenter;
    author.TextAlign = ContentAlignment.MiddleCenter;
    inchinput.TextAlign = HorizontalAlignment.Left;
    outputinfo.TextAlign = ContentAlignment.MiddleLeft;

    //Set locations
    headerpanel.Location = new Point(0,0);
    welcome.Location = new Point(24,3);
    author.Location = new Point(40,133);
    inchlabel.Location = new Point(40,80);
    inchinput.Location = new Point(521,80);
    outputinfo.Location = new Point(60,200);
    computebutton.Location = new Point(24,20);
    clearbutton.Location = new Point(358,20);
    exitbutton.Location = new Point(692,20);
    headerpanel.Location = new Point(0,0);
    displaypanel.Location = new Point(0,200);
    controlpanel.Location = new Point(0,600);

    //Associate the Compute button with the Enter key of the keyboard
    AcceptButton = computebutton;

    //Add controls to the form
    Controls.Add(headerpanel);
    headerpanel.Controls.Add(welcome);
    headerpanel.Controls.Add(author);
    Controls.Add(displaypanel);
    displaypanel.Controls.Add(inchlabel);
    displaypanel.Controls.Add(inchinput);
    displaypanel.Controls.Add(outputinfo);
    Controls.Add(controlpanel);
    controlpanel.Controls.Add(computebutton);
    controlpanel.Controls.Add(clearbutton);
    controlpanel.Controls.Add(exitbutton);

    //Register the event handler.  In this case each button has an event handler, but no other 
    //controls have event handlers.
    computebutton.Click += new EventHandler(computeITMCnumber);
    clearbutton.Click += new EventHandler(cleartext);
    exitbutton.Click += new EventHandler(stoprun);  //The '+' is required.

    //Configure the clock that controls the shutdown      //<== New in version 2.2
    exit_clock.Enabled = false;     //Clock is turned off at start program execution.
    exit_clock.Interval = 7500;     //7500ms = 7.5seconds.  Clock will tick at intervals of 7.5 seconds
    exit_clock.Elapsed += new ElapsedEventHandler(shutdown);   //Attach a method to the clock.

    //Open this user interface window in the center of the display.
    CenterToScreen();

   }//End of constructor Fibuserinterface

 //Method to execute when the compute button receives an event, namely: receives a mouse click
 protected void computeITMCnumber(Object sender, EventArgs events)
   {Console.WriteLine("The function computeITMCnumber was called.");
    double inches;
    
    try
      {Console.WriteLine("Inch input = " + inchinput.Text);
       if(inchinput.Text.IndexOf('.') == -1)
       {
            throw new Exception("Non float input detected");
                
       }else
            {
            inches = Convert.ToDouble(inchinput.Text);
            double meters = ImpToMetConlogic.computeImpToMetConnumber(inches);
             outputinfo.Text = "The metric value is " + string.Format("{0:0.0000000}",meters);
             Console.WriteLine("The computed metric value is " + string.Format("{0:0.000}",meters));
            }
      }//End of try
    catch(FormatException malformed_input)
       {Console.WriteLine("Invalid input received.  Please try again. {0}",malformed_input.Message);
        outputinfo.Text = "Invalid input: no conversion computed.";
       }//End of catch
    catch(Exception nonfloat)
       {Console.WriteLine("{0} Please correct input and try again",nonfloat.Message);
        outputinfo.Text = "Non-floating point value in the input.";
       }//End catch
   }//End of computeinchnumber
   
 //Method to execute when the clear button receives an event, namely: receives a mouse click
 protected void cleartext(Object sender, EventArgs events)
   {inchinput.Text = ""; //Empty string
    outputinfo.Text = "Result will display here.";
    outcome = Status.Initial_display;
    displaypanel.Invalidate();
   }//End of cleartext

//Method to execute when the exit button receives an event, namely: receives a mouse click  <== New in version 2.2
protected void stoprun(Object sender, EventArgs events)
   {switch(current_state)
    {case Execution_state.Executing:
             exit_clock.Interval= 7500;     //7500ms = 7.5 seconds
             exit_clock.Enabled = true;
             exitbutton.Text = "Resume";
             current_state = Execution_state.Waiting_to_terminate;
             break;
     case Execution_state.Waiting_to_terminate:
             exit_clock.Enabled = false;
             exitbutton.Text = "Quit";
             current_state = Execution_state.Executing;
             break;
     }//End of switch statement
  }//End of method stoprun.  In C Sharp language "method" means "function".

protected void shutdown(System.Object sender, EventArgs even)                   //<== Revised for version 2.2
    {//This function is called when the clock makes its first "tick", 
     //which occurs 3.5 seconds after the clock starts.
     Close();       //That means close the main user interface window.
    }//End of method shutdown

 public class Graphicpanel: Panel
 {private Brush paint_brush = new SolidBrush(System.Drawing.Color.Green);
  public Graphicpanel() 
        {Console.WriteLine("A graphic enabled panel was created");}  //Constructor writes to terminal
  protected override void OnPaint(PaintEventArgs ee)
  {  Graphics graph = ee.Graphics;
     switch(outcome)
     {case Status.Initial_display: Console.WriteLine("Initial view of the UI is displayed");
           break;
      case Status.Successful_calculation: graph.FillEllipse(paint_brush,100,280,100,100);
           break;
      case Status.Error: graph.FillEllipse(Brushes.Red,800,280,100,100);
           break;
  }//End of switch
  //The next statement looks like recursion, but it really is not recursion.
  //In fact, it calls the method with the same name located in the super class.
  base.OnPaint(ee);
  }//End of OnPaint
 }//End of class Graphicpanel

}//End of clas Fibuserinterface

