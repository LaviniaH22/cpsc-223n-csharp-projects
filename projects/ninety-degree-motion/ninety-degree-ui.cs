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
//This file's name: ninety-degree-ui.cs
//This file purpose: This file will activate the user interface
//Date last modified: 2026-Feb-28

//Suggestion to user: Feel free to change the values in the first five parameters and discover the effects of those changes.  
//For instance, modify the values in refresh_clock_rate, ball_linear_speed, motion_clock_rate.


using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Ninety_degree_turn_interface : Form
{  //Declare constants ordered by the specification document
     private static readonly int ui_width = (int)(Screen.PrimaryScreen.WorkingArea.Width * 0.97);
     private static readonly int ui_height = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.97);
   private const double refresh_clock_rate = 24.8;  //Hertz = tics per second
   private const double motion_clock_rate = 57;   //Hertz = tics per second
   private double ball_linear_speed = 98.3;   //pixel per second
     private Size maxframesize = new Size(ui_width,ui_height);
     private Size minframesize = new Size(ui_width,ui_height);

   //Declare the top panel and related attributes
   private Panel header = new Panel();
   private static readonly int header_height = (int)(ui_height * 0.1);
   private Label ninety_degree_label = new Label();

   //Declare the graphic panel and related attributes.
   private Graphicpanel graph_panel = new Graphicpanel();

   //Declare the controller panel
   private Panel controler = new Panel();

   //Declare objects (controls) to be placed on the controller panel.
   private Button go_and_pause = new Button();
   private TextBox speed_input = new TextBox();
   private Label speed_lable = new Label();
   private Button quit = new Button();

   //Declare vertices of the polygonal line where the ball will travel.
   private static Point v1;
   private static Point v2;
   private static Point v3;
   private static Point v4;

   //Declare the properties of the moving ball
   enum Compass {North,West,South,East};
   Compass current_direction = Compass.West;
   private static readonly double ball_radius = ui_width * 0.005;
   private static double ball_center_x; 
   private static double ball_center_y;
   private static double ball_corner_x;
   private static double ball_corner_y;
   private static double Δx;
   private static double Δy;
   private static double ball_speed_pixel_per_tic;

   //Declare properties of the clock controlling the ball.
   private int ball_clock_interval = (int)System.Math.Round(1000.0/motion_clock_rate);
   private static System.Timers.Timer ball_clock = new System.Timers.Timer();

   //Declare the refresh clock.
   private int refresh_clock_interval = (int)System.Math.Round(1000.0/refresh_clock_rate);
   private static System.Timers.Timer user_interface_refresh_clock = new System.Timers.Timer();

   //Auxiliary data
   private bool both_clocks_are_stopped = true;
   private int tic_counter = 0;

   //Begin constructor of this class
   public Ninety_degree_turn_interface()      //<====================
   {//Set properties of the form
    Width = ui_width;
    Height = ui_height;
    MaximumSize = maxframesize;
    MinimumSize = minframesize;
    BackColor = Color.FromArgb(18,18,18); // form background (very dark)
    Text = "Ninety Degree Turn";
    System.Console.WriteLine("form height = {0}, form width = {1}.", Height, Width);


    //Set up the top panel known as the header panel.
    header.Size = new Size(Width,header_height);
    header.Text = "90 Degree Turn";
    header.BackColor = Color.FromArgb(0,20,0); // dark header
    header.Location = new Point(0,0);
    System.Console.WriteLine("header panel height = {0}, header panel width = {1}.", header.Height, header.Width);

    //Set up the label to be placed in the top panel.
    ninety_degree_label.Font = new Font("Times New Roman",(int)(header_height*0.35),FontStyle.Bold);
    ninety_degree_label.Size = new Size((int)(ui_width*0.95),(int)(header_height*0.8));
    ninety_degree_label.Text = "Racing Ball by Lavinia Hawkins";
    ninety_degree_label.ForeColor = Color.White;
    ninety_degree_label.TextAlign = ContentAlignment.MiddleCenter;
    //Next statement: find a point that centers the placement of ninety_degree_label in the center of header_panel.
    ninety_degree_label.Location = new Point((header.Width-ninety_degree_label.Width)/2,(header.Height-ninety_degree_label.Height)/2);
    header.Controls.Add(ninety_degree_label);

    //Set up the graphically enabled central panel
    graph_panel.Location = new Point(0,header.Height);
    graph_panel.BackColor = Color.Black; // drawing area (near-black)
    graph_panel.Width = Width;   //The graph panel has width equal to the width of the form
    graph_panel.Height = (int)(ui_height*0.7);
    System.Console.WriteLine("graph panel height = {0}, graph panel width = {1}.", graph_panel.Height, graph_panel.Width);

    //Set up the polygonal path in which the ball travels
    v1 = new Point(graph_panel.Width-40,40);
    v2 = new Point(40,40);
    v3 = new Point(40,graph_panel.Height-40);
    v4 = new Point(graph_panel.Width-40,graph_panel.Height-40);

    //Set the initial location of the ball.
    ball_center_x = v1.X;
    ball_center_y = v1.Y;

    //Set up the control panel containing buttons and other display devices.
    controler.Width = Width;
    controler.Height = (int)(ui_height*0.2);
    controler.BackColor = Color.FromArgb(0,0,20); // dark control area
    controler.Location = new Point(0,header.Height+graph_panel.Height);
    System.Console.WriteLine("control panel height = {0}, control panel width = {1}.", controler.Height, controler.Width);

    //Set up the go_and_pause button and attach it to the controller panel
    go_and_pause.Size = new Size((int)(ui_width*0.2),(int)(controler.Height*0.6));
    go_and_pause.Text = "Start";
    go_and_pause.BackColor = Color.FromArgb(255,204,0); // gold accent
    go_and_pause.ForeColor = Color.Black;
    go_and_pause.Location = new Point((int)(ui_width*0.045),(int)(controler.Height*0.2));
    go_and_pause.Font = new Font("Times New Roman",(int)(controler.Height*0.185),FontStyle.Bold);
    go_and_pause.Click += new EventHandler(Go_pause);
    go_and_pause.Enabled = true;
    go_and_pause.TextAlign = ContentAlignment.MiddleCenter;
    controler.Controls.Add(go_and_pause);

    //Configure the label that will explain elapsed time
    speed_lable.Size = new Size((int)(ui_width*0.2),(int)(controler.Height*0.6));
    speed_lable.BackColor = Color.FromArgb(30,30,50);
    speed_lable.ForeColor = Color.White;
    speed_lable.Location = new Point((int)(ui_width*0.29),(int)(controler.Height*0.2));
    speed_lable.Font = new Font("Times New Roman",(int)(controler.Height*0.185),FontStyle.Bold);
    speed_lable.TextAlign = ContentAlignment.MiddleCenter;
    speed_lable.Text = "Enter Speed\n(Pixel/Sec)";
    controler.Controls.Add(speed_lable);
    //Configure the label that will display the time in seconds.
    speed_input.Size = new Size((int)(ui_width*0.2),(int)(controler.Height*0.7));
    speed_input.BackColor = Color.FromArgb(40,40,60);
    speed_input.ForeColor = Color.White;
    speed_input.Location = new Point((int)(ui_width*0.51),(int)(controler.Height*0.25));
    speed_input.Font = new Font("Arial",(int)(controler.Height*0.3),FontStyle.Bold);
    //speed_input.TextAlign = ContentAlignment.MiddleCenter;
    controler.Controls.Add(speed_input);
    //Set up the quit button and attach it to the controller panel
    quit.Size = new Size((int)(ui_width*0.2),(int)(controler.Height*0.6));
    quit.Text = "Exit";
    quit.BackColor = Color.FromArgb(220,20,60); // crimson-ish for danger
    quit.ForeColor = Color.White;
     quit.Location = new Point((int)(ui_width*0.755),(int)(controler.Height*0.2));
     quit.Font = new Font("Arial",(int)(controler.Height*0.185),FontStyle.Bold);
    quit.TextAlign = ContentAlignment.MiddleCenter;
    quit.Click += new EventHandler(Close_window);
    controler.Controls.Add(quit);

    //Prepare the refresh clock.  A button will start this clock ticking.
    user_interface_refresh_clock.Enabled = false;  //Initially this clock is stopped.
    user_interface_refresh_clock.Interval = refresh_clock_interval;
    user_interface_refresh_clock.Elapsed += new ElapsedEventHandler(Refresh_user_interface);

   //Prepare the ball clock.  A button will start this clock ticking.
    ball_clock.Enabled = false;  //Initially this clock is stopped.
    ball_clock.Interval = ball_clock_interval;
    ball_clock.Elapsed += new ElapsedEventHandler(Update_ball_coordinates);

    //Attach various elements (controls) to the form.
    Controls.Add(header);
    Controls.Add(graph_panel);
    Controls.Add(controler);


    //Make sure this UI window appears in the center of the monitor.
    CenterToScreen();

   }//End of constructor                               //<=======================

   //Handler for the ball clock.
   protected void Update_ball_coordinates(System.Object sender, ElapsedEventArgs even)
   {//This function is called each time the ball_clock makes one tic.  
    //That clock is often called the animation clock.
    tic_counter++;
    switch (current_direction)
    {  case Compass.West:
                 if(ball_center_x+Δx >= v2.X)
                      {ball_center_x += Δx;    //<= Ball moves directly West.
                       ball_center_y += Δy;    //However, Δy is 0.0 when moving West.
                      }
                 else //ball_center_x+Δx < v2.X
                      {//Ball changes direction to South
                       ball_center_x = v2.X;
                       ball_center_y = v2.Y - ball_center_x - Δx + v2.X;
                       Δx = 0.0;
                       Δy = +ball_speed_pixel_per_tic;
                       current_direction = Compass.South;
                      }//End of else
                 break;
        case Compass.South:
                 if(ball_center_y+Δy <= v3.Y)
                      {ball_center_x += Δx;   //Redundant: Δx = 0.0 when moving South.
                       ball_center_y += Δy;   //<== Ball moves directly South
                      }
                 else //ball_center_y+Δy > v3.Y
                      {//Ball changes direction to East.
                       ball_center_x = v3.X + ball_center_y + Δy - v3.Y;
                       ball_center_y = v3.Y;
                       Δx = +ball_speed_pixel_per_tic;
                       Δy = 0.0;
                       current_direction = Compass.East;
                      }//End of else
                 break;
        case Compass.East:
                 if(ball_center_x + Δx <= v4.X)
                      {ball_center_x += Δx;
                       ball_center_y += Δy;   //Redundant: Δy = 0.0 when moving East.
                      }
                 else //ball_center_x + Δx > v4.X is true.
                      {//Ball changes direction to North.
                       ball_center_x = v4.X;
                       ball_center_y = v4.Y - ball_center_x - Δx + v4.X;
                       Δx = 0.0;
                       Δy = -ball_speed_pixel_per_tic;
                       current_direction = Compass.North;
                      }
                 break;
        case Compass.North:
               if(ball_center_y + Δy >= v1.Y)
                      {ball_center_x += Δx;
                       ball_center_y += Δy;   //Redundant: Δx = 0.0 when moving North.
                      }
                 else //ball_center_y + Δy < v1.Y is true.
                      {//Ball stops moving.
                       ball_center_x = v1.X;
                       ball_center_y = v1.Y;
                       Δx = -ball_speed_pixel_per_tic;
                       Δy = 0.0;
                       current_direction = Compass.West;
                        }           
                      

                 break;
        default:
                 System.Console.WriteLine("You have a serious bug in this program.  Try again");
                 break;
    }//End of switch
   }//End of method Update_ball_coordinates

   //Handler for the Go_and_pause button
   protected void Go_pause(System.Object sender, EventArgs even)
   {if(both_clocks_are_stopped)
         {       
           if (string.IsNullOrWhiteSpace(speed_input.Text))
          {
            System.Console.WriteLine("Please enter a speed value first");
            speed_input.Text = "No Value!";
            return;
          }   
          //Change the message on the button
          go_and_pause.Text = "Pause";
          //Change the speed of the ball according to the value in the speed_input TextBox.
          ball_linear_speed = Convert.ToDouble(speed_input.Text);
          ball_speed_pixel_per_tic = ball_linear_speed/motion_clock_rate;  //pixels
          Δx = -ball_speed_pixel_per_tic;
          Δy = 0.0;
          //Start the refresh clock running.
          user_interface_refresh_clock.Enabled = true;

          //Start the animation clock running.
          ball_clock.Enabled = true;
         }
    else
         {//Stop the refresh clock.
          user_interface_refresh_clock.Enabled = false;

          //Stop the animation clock running.
          ball_clock.Enabled = false;

          //Change the message on the button
          go_and_pause.Text = "Go";
         }//End of if
    //Toggle the variable both_clocks_are_stopped to be its negative
    both_clocks_are_stopped = !both_clocks_are_stopped;
   }//End of event handler Go_pause

   //Declare the handler method for the refresh clock
   protected void Refresh_user_interface(System.Object sender, ElapsedEventArgs even)
   {//elapsed_time_value.Text = String.Format("{0:000.00}",(double)tic_counter/motion_clock_rate);
    graph_panel.Invalidate();
   }//End of event handler Refresh_user_interface

   //Declare the handler method of the Quit button.
   protected void Close_window(System.Object sender, EventArgs even)
   {System.Console.WriteLine("This program will close the UI window and stop execution.");
    Close();
   }//End of event handler Close_window


    //Define the Graphic Panel     
    public class Graphicpanel: Panel
    {private Pen schaffer = new Pen(Color.Purple,3);
        protected override void OnPaint(PaintEventArgs ee)
        {   Graphics graph = ee.Graphics;
            graph.DrawLine(schaffer,v1.X,v1.Y,v2.X,v2.Y);
            graph.DrawLine(schaffer,v2.X,v2.Y,v3.X,v3.Y);
            graph.DrawLine(schaffer,v3.X,v3.Y,v4.X,v4.Y);
            graph.DrawLine(schaffer,v4.X,v4.Y,v1.X,v1.Y);
            ball_corner_x = ball_center_x - ball_radius;
            ball_corner_y = ball_center_y - ball_radius;
            graph.FillEllipse(Brushes.Lime,
                              (int)System.Math.Round(ball_corner_x),
                              (int)System.Math.Round(ball_corner_y),
                              (int)System.Math.Round(2.0*ball_radius),
                              (int)System.Math.Round(2.0*ball_radius));
            base.OnPaint(ee);
        }//End of method OnPaint
    }//End of class Graphicpanel

}//End of class Ninety_degree_turn_interface

//======End of file=============================================================
