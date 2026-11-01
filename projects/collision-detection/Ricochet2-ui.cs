//Author: Lavinia Hawkins
//Course: CPSC 223N
//Assignment 4 
//Due per syllabus: 2026-April-19
//Program name: Collision Detection
//Purpose:  This program is design to simulate the motion of a two balls that ricochets off the walls of a rectangular area and detect thier eventual 
//collision. The user can input the initial speed and direction of the two balls, and then start the simulation. The program will display both balls 
//location and distance between them in real-time.  The program will stop when the two balls collide. In which case, the user can choose to re-play 
//the simulation with the same initial conditions or re-initialize the system with new initial conditions.
//Programming language: C Sharp
//Date development of program began: 2026-April-10
//Date of last update: N/A
//Files in project: Ricochet2-main.cs, Ricochet2-ui.cs, run.sh
//This file name: Ricochet2-ui.cs
//This file's name: Ricochet2-ui.cs
//This file purpose: This file will activate the user interface
//Date last modified: 2026-April-12

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Ricochet_interface_form : Form
{  //Declare data about the UI:
   private static readonly int formheight = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.95);
    private static readonly int formwidth = formheight + (int)(formheight * 0.2);  
 

    private Size maxframesize = new Size(formwidth,formheight);
    private Size minframesize = new Size(formwidth,formheight);
    int titleheight = (int)(formheight * 0.1);
    int graphicheight = (int)(formheight * 0.9);
   private int sidepannelwidth = (int)(formwidth * 0.25);
    int sidepanelheight = (int)(formheight * 0.9);
    
   //Declare data about the ball 1:
   private double ball1_radius = formwidth*0.02;
   private double ball1_linear_speed_pix_per_sec;
   private double ball1_linear_speed_pix_per_tic;
   private double ball1_direction_x;
   private double ball1_direction_y;
   private double ball1_delta_x;
   private double ball1_delta_y;
   private double ball1_center_initial_coord_x;
   private double ball1_center_initial_coord_y;
   private double ball1_center_current_coord_x;
   private double ball1_center_current_coord_y;
   private double ball1_upper_left_current_coord_x;
   private double ball1_upper_left_current_coord_y;
   //Declare data about the ball 2:
   private double ball2_radius = formwidth*0.04;
   private double ball2_linear_speed_pix_per_sec;
   private double ball2_linear_speed_pix_per_tic;
   private double ball2_direction_x;
   private double ball2_direction_y;
   private double ball2_delta_x;
   private double ball2_delta_y;
   private double ball2_center_initial_coord_x;
   private double ball2_center_initial_coord_y;
   private double ball2_center_current_coord_x;
   private double ball2_center_current_coord_y;
   private double ball2_upper_left_current_coord_x;
   private double ball2_upper_left_current_coord_y;

   private bool ball_collision_occurred = false;  //This variable is used to indicate whether a collision between the two balls has occurred.  It is used to prevent multiple detections of the same collision.

   //Declare data about the motion clock:
   private static System.Timers.Timer ball1_motion_control_clock = new System.Timers.Timer();
   private static System.Timers.Timer ball2_motion_control_clock = new System.Timers.Timer();
   private const double ball_motion_control_clock_rate = 43.5;  //Units are Hz

   //Declare data about the refresh clock;
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private const double graphic_refresh_rate = 23.3;  //Units are Hz = #refreshes per second
   //Declare pannels 
    private Panel title_panel = new Panel();
    private Panel graphic_panel = new Panel();
    private Panel side_panel = new Panel();
    private Panel button_panel = new Panel();
    private Panel input_ball1_panel = new Panel();
    private Panel input_ball2_panel = new Panel();

    //Declare side panel labels
  
    private Label title_ball1_label = new Label();
    private Label speed_ball1_label = new Label();
    private Label direction_ball1_label = new Label();
    private Label title_ball2_label = new Label();
    private Label speed_ball2_label = new Label();
    private Label direction_ball2_label = new Label();
    //Declare title and data display labels
    private Label title_label = new Label();
    private Label show_ball1_location_label = new Label();
    private Label show_ball2_location_label = new Label();
    private Label show_Distance_between_balls_label = new Label();

    //Declare text box
    private TextBox speed_ball1_textbox = new TextBox();
    private TextBox direction_ball1_textbox = new TextBox();
    private TextBox speed_ball2_textbox = new TextBox();
    private TextBox direction_ball2_textbox = new TextBox();
    //Declare button
    private Button start_button = new Button();
    private Button initialize_button = new Button();
    private Button quit_button = new Button();


   public Ricochet_interface_form(double speed,double v,double w)   //The constructor of this class
{  
    Width = formwidth;
    Height = formheight;
    MaximumSize = maxframesize;
    MinimumSize = minframesize;
    //Set the title of this form.
    Text = "Ricochet Motion";
    System.Console.WriteLine("formwidth = {0}. formheight = {1}.",formwidth,formheight);
    //Set the initial size of this form
    Size = new Size(formwidth,formheight);
    //Set the background color of this form
    BackColor = Color.Green;

    //Initialize ball coordinates now that sizes are known
    ball1_center_initial_coord_x = ball1_radius*2 + (int)(formwidth * 0.1);  //The initial x coordinate of the center of the ball is set to be a little more than one radius away from the left wall.
    ball1_center_initial_coord_y = ball1_radius*2 + (int)(formheight * 0.1);  //The initial y coordinate of the center of the ball is set to be a little more than one radius away from the upper wall.
    ball2_center_initial_coord_x = formwidth - ball2_radius*2 - (int)(formwidth * 0.25);  //The initial x coordinate of the center of the ball is set to be a little more than one radius away from the right wall.
    ball2_center_initial_coord_y = formheight - ball2_radius*2 - (int)(formheight * 0.25);  //The initial y coordinate of the center of the ball is set to be a little more than one radius away from the lower wall.
    ball1_center_current_coord_x = ball1_center_initial_coord_x;
    ball1_center_current_coord_y = ball1_center_initial_coord_y;
    ball2_center_current_coord_x = ball2_center_initial_coord_x;
    ball2_center_current_coord_y = ball2_center_initial_coord_y;
    //title properties
    title_panel.Size = new Size(formwidth,titleheight);
    title_panel.Location = new Point(0,0);
    title_panel.BackColor = Color.FromArgb(0,20,0); // dark header
    System.Console.WriteLine("title panel height = {0}, title panel width = {1}.", title_panel.Height, title_panel.Width);
    Controls.Add(title_panel);
    //title_label properties
    title_label.Text = "Ricochet Ball by Lavinia Hawkins";
    title_label.Font = new Font("DejaVu Sans", (int)(formwidth * 0.035), FontStyle.Bold);
    title_label.Location = new Point((int)(formwidth * 0.01), (int)(titleheight * 0.01));
    title_label.Size = new Size((int)(formwidth * 0.98), (int)(titleheight * 0.7));
    title_label.ForeColor = Color.White;
    title_label.TextAlign = ContentAlignment.MiddleCenter;
    title_panel.Controls.Add(title_label);
    //show_ball1_location_label properties
   double center_distance =System.Math.Sqrt(
      System.Math.Pow(ball1_center_current_coord_x - ball2_center_current_coord_x, 2) +
      System.Math.Pow(ball1_center_current_coord_y - ball2_center_current_coord_y, 2)
   );
   double distance_between_balls = center_distance - ball1_radius - ball2_radius;
    show_ball1_location_label.Text = string.Format("Red Ball Current Location: ({0}, {1})", (int)ball1_center_current_coord_x, (int)ball1_center_current_coord_y);
    show_ball1_location_label.Font = new Font("DejaVu Sans", (int)(formwidth * 0.01), FontStyle.Regular);
    show_ball1_location_label.Location = new Point((int)(formwidth * 0.01), (int)(titleheight * 0.72));
    show_ball1_location_label.Size = new Size((int)(formwidth * 0.29), (int)(titleheight * 0.28));
    show_ball1_location_label.ForeColor = Color.White;
    title_panel.Controls.Add(show_ball1_location_label);
    //show_ball2_location_label properties
    show_ball2_location_label.Text = string.Format("Blue Ball Current Location: ({0}, {1})", (int)ball2_center_current_coord_x, (int)ball2_center_current_coord_y);
    show_ball2_location_label.Font = new Font("DejaVu Sans", (int)(formwidth * 0.01), FontStyle.Regular);
    show_ball2_location_label.Location = new Point((int)(formwidth * 0.35), (int)(titleheight * 0.72));
    show_ball2_location_label.Size = new Size((int)(formwidth * 0.29), (int)(titleheight * 0.28));
    show_ball2_location_label.ForeColor = Color.White;
    title_panel.Controls.Add(show_ball2_location_label);
    //show_Distance_between_balls_label properties
    show_Distance_between_balls_label.Text = string.Format("Distance between balls: {0} pixels", (int)distance_between_balls);
    show_Distance_between_balls_label.Font = new Font("DejaVu Sans", (int)(formwidth * 0.01), FontStyle.Regular);
    show_Distance_between_balls_label.Location = new Point((int)(formwidth * 0.74), (int)(titleheight * 0.72));
    show_Distance_between_balls_label.Size = new Size((int)(formwidth * 0.25), (int)(titleheight * 0.28));
    show_Distance_between_balls_label.ForeColor = Color.White;
    title_panel.Controls.Add(show_Distance_between_balls_label);
    //graphic_panel properties
    graphic_panel.Size = new Size((int)(formwidth * 0.75), graphicheight);
    graphic_panel.Location = new Point(sidepannelwidth,(int)(formheight * 0.1));
    graphic_panel.BackColor = Color.FromArgb(0,0,0); 
    Controls.Add(graphic_panel);
    graphic_panel.Paint += new PaintEventHandler(Draw_Ball1);
    graphic_panel.Paint += new PaintEventHandler(Draw_Ball2);
    //side_panel properties
    side_panel.Size = new Size(sidepannelwidth, sidepanelheight);
    side_panel.Location = new Point(0,(int)(formheight * 0.1));
    side_panel.BackColor = Color.FromArgb(0,0,0); 
    Controls.Add(side_panel);
    //button_panel properties
    button_panel.Size = new Size(sidepannelwidth,(int)(sidepanelheight*0.3));
    button_panel.Location = new Point(0,(int)(sidepanelheight*0.7));
    button_panel.BackColor = Color.FromArgb(5,0,20); 
    side_panel.Controls.Add(button_panel);
    //input_ball1_panel properties
    input_ball1_panel.Size = new Size(sidepannelwidth,(int)(sidepanelheight*0.35));
    input_ball1_panel.Location = new Point(0,0);
    input_ball1_panel.BackColor = Color.FromArgb(20,0,0);
    side_panel.Controls.Add(input_ball1_panel);
    //input_ball2_panel properties
    input_ball2_panel.Size = new Size(sidepannelwidth,(int)(sidepanelheight*0.35));
    input_ball2_panel.Location = new Point(0,(int)(sidepanelheight*0.35));
    input_ball2_panel.BackColor = Color.FromArgb(0,0,20);
    side_panel.Controls.Add(input_ball2_panel);
    //start_button properties
    start_button.Text = "Start";
    start_button.Font = new Font("DejaVu Sans", (int)(button_panel.Height*0.25 * 0.45), FontStyle.Regular);
    start_button.Size = new Size((int)(button_panel.Width * 0.8), (int)(button_panel.Height * 0.22));
    start_button.Location = new Point((int)(button_panel.Width * 0.1), (int)(button_panel.Height * 0.06));
    start_button.BackColor = Color.FromArgb(0,40,0);
    start_button.ForeColor = Color.White;
    start_button.Enabled = false;
    button_panel.Controls.Add(start_button);
    //initialize_button properties
    initialize_button.Text = "Initialize";
    initialize_button.Font = new Font("DejaVu Sans", (int)(button_panel.Height*0.25 * 0.4), FontStyle.Regular);
    initialize_button.Size = new Size((int)(button_panel.Width * 0.8), (int)(button_panel.Height * 0.22));
    initialize_button.Location = new Point((int)(button_panel.Width * 0.1), (int)(button_panel.Height * 0.34));
    initialize_button.BackColor = Color.FromArgb(80,80,0);
    initialize_button.ForeColor = Color.White;
    button_panel.Controls.Add(initialize_button);
    initialize_button.Click += new EventHandler(Intialize_System);
    //quit_button properties
    quit_button.Text = "Quit";
    quit_button.Font = new Font("DejaVu Sans", (int)(button_panel.Height*0.25 * 0.45), FontStyle.Regular);
    quit_button.Size = new Size((int)(button_panel.Width * 0.8), (int)(button_panel.Height * 0.22));
    quit_button.Location = new Point((int)(button_panel.Width * 0.1), (int)(button_panel.Height * 0.62));
    quit_button.BackColor = Color.FromArgb(80,0,0);
    quit_button.ForeColor = Color.White;
    button_panel.Controls.Add(quit_button);
    quit_button.Click += new EventHandler((sender, e) => { Application.Exit(); });
      //title_ball1_label properties
   title_ball1_label.Text = "Red Ball";
   title_ball1_label.Font = new Font("DejaVu Sans", (int)(input_ball1_panel.Height*0.13), FontStyle.Bold);
   title_ball1_label.TextAlign = ContentAlignment.MiddleCenter;
   title_ball1_label.Size = new Size((int)(input_ball1_panel.Width * 0.94), (int)(input_ball1_panel.Height * 0.38));
   title_ball1_label.Location = new Point((int)(input_ball1_panel.Width * 0.03), (int)(input_ball1_panel.Height * 0.03));  
   title_ball1_label.ForeColor = Color.Red;
   input_ball1_panel.Controls.Add(title_ball1_label);
   //Ball1speed_label properties
    speed_ball1_label.Text = "Enter Speed (pixel/sec): ";
    speed_ball1_label.Font = new Font("DejaVu Sans", (int)(input_ball1_panel.Height*0.04), FontStyle.Regular);
    speed_ball1_label.Size = new Size((int)(input_ball1_panel.Width * 0.56), (int)(input_ball1_panel.Height * 0.22));
    speed_ball1_label.Location = new Point((int)(input_ball1_panel.Width * 0.03), (int)(input_ball1_panel.Height * 0.41));
    speed_ball1_label.ForeColor = Color.White;
    input_ball1_panel.Controls.Add(speed_ball1_label);
    //Ball1 speed_textbox properties
    speed_ball1_textbox.Size = new Size((int)(input_ball1_panel.Width * 0.39), (int)(input_ball1_panel.Height * 0.44));
    speed_ball1_textbox.Font = new Font("DejaVu Sans", (int)(input_ball1_panel.Height*0.08), FontStyle.Regular);
    speed_ball1_textbox.Location = new Point((int)(input_ball1_panel.Width * 0.59), (int)(input_ball1_panel.Height * 0.41));
    input_ball1_panel.Controls.Add(speed_ball1_textbox);
    //Ball1 direction_label properties
    direction_ball1_label.Text = "Enter Direction (degrees): ";
    direction_ball1_label.Font = new Font("DejaVu Sans", (int)(input_ball1_panel.Height*0.04), FontStyle.Regular);
    direction_ball1_label.Size = new Size((int)(input_ball1_panel.Width * 0.45), (int)(input_ball1_panel.Height * 0.22));
    direction_ball1_label.Location = new Point((int)(input_ball1_panel.Width * 0.03), (int)(input_ball1_panel.Height * 0.73));
    direction_ball1_label.ForeColor = Color.White;
    //Ball1 direction_textbox properties
    direction_ball1_textbox.Size = new Size((int)(input_ball1_panel.Width * 0.40), (int)(input_ball1_panel.Height * 0.44));
    direction_ball1_textbox.Font = new Font("DejaVu Sans", (int)(input_ball1_panel.Height*0.08), FontStyle.Regular);
    direction_ball1_textbox.Location = new Point((int)(input_ball1_panel.Width * 0.46), (int)(input_ball1_panel.Height * 0.73));
    input_ball1_panel.Controls.Add(direction_ball1_textbox);
    input_ball1_panel.Controls.Add(direction_ball1_label);
   //title_ball2_label properties
   title_ball2_label.Text = "Blue Ball";
   title_ball2_label.Font = new Font("DejaVu Sans", (int)(input_ball2_panel.Height*0.13), FontStyle.Bold);
   title_ball2_label.TextAlign = ContentAlignment.MiddleCenter;
   title_ball2_label.Size = new Size((int)(input_ball2_panel.Width * 0.94), (int)(input_ball2_panel.Height * 0.38));
   title_ball2_label.Location = new Point((int)(input_ball2_panel.Width * 0.03), (int)(input_ball2_panel.Height * 0.03));  
   title_ball2_label.ForeColor = Color.Blue;
   input_ball2_panel.Controls.Add(title_ball2_label);
   //Ball2speed_label properties
    speed_ball2_label.Text = "Enter Speed (pixel/sec): ";
    speed_ball2_label.Font = new Font("DejaVu Sans", (int)(input_ball2_panel.Height*0.04), FontStyle.Regular);
    speed_ball2_label.Size = new Size((int)(input_ball2_panel.Width * 0.56), (int)(input_ball2_panel.Height * 0.22));
    speed_ball2_label.Location = new Point((int)(input_ball2_panel.Width * 0.03), (int)(input_ball2_panel.Height * 0.41));
    speed_ball2_label.ForeColor = Color.White;
    input_ball2_panel.Controls.Add(speed_ball2_label);
    //Ball2 speed_textbox properties
    speed_ball2_textbox.Size = new Size((int)(input_ball2_panel.Width * 0.39), (int)(input_ball2_panel.Height * 0.44));
    speed_ball2_textbox.Font = new Font("DejaVu Sans", (int)(input_ball2_panel.Height*0.08), FontStyle.Regular);
    speed_ball2_textbox.Location = new Point((int)(input_ball2_panel.Width * 0.59), (int)(input_ball2_panel.Height * 0.41));
    input_ball2_panel.Controls.Add(speed_ball2_textbox);
    //Ball2 direction_label properties
    direction_ball2_label.Text = "Enter Direction (degrees): ";
    direction_ball2_label.Font = new Font("DejaVu Sans", (int)(input_ball2_panel.Height*0.04), FontStyle.Regular);
    direction_ball2_label.Size = new Size((int)(input_ball2_panel.Width * 0.45), (int)(input_ball2_panel.Height * 0.22));
    direction_ball2_label.Location = new Point((int)(input_ball2_panel.Width * 0.03), (int)(input_ball2_panel.Height * 0.73));
    direction_ball2_label.ForeColor = Color.White;
    //Ball2 direction_textbox properties
    direction_ball2_textbox.Size = new Size((int)(input_ball2_panel.Width * 0.40), (int)(input_ball2_panel.Height * 0.44));
    direction_ball2_textbox.Font = new Font("DejaVu Sans", (int)(input_ball2_panel.Height*0.08), FontStyle.Regular);
    direction_ball2_textbox.Location = new Point((int)(input_ball2_panel.Width * 0.46), (int)(input_ball2_panel.Height * 0.73));
    input_ball2_panel.Controls.Add(direction_ball2_textbox);
    input_ball2_panel.Controls.Add(direction_ball2_label);
    //Set up the motion clock.  This clock controls the rate of update of the coordinates of the ball 1.
    ball1_motion_control_clock.Enabled = false;
    //Assign a handler to this clock.
    ball1_motion_control_clock.Elapsed += new ElapsedEventHandler(Update_ball1_position);
    //Set up the motion clock.  This clock controls the rate of update of the coordinates of the ball 2.
    ball2_motion_control_clock.Enabled = false;
    //Assign a handler to this clock.
    ball2_motion_control_clock.Elapsed += new ElapsedEventHandler(Update_ball2_position);

    //Set up the refresh clock.
    graphic_area_refresh_clock.Enabled = false;  //Initially the clock controlling the rate of updating the display is stopped.
    //Assign a handler to this clock.
    graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_display);  

    //Set properties of the button (or maybe buttons)
    start_button.Click += new EventHandler(All_systems_go);

    CenterToScreen();

   }//End of constructor

   private void Draw_Ball1(object sender,PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
      ball1_upper_left_current_coord_x = ball1_center_current_coord_x - ball1_radius;
      ball1_upper_left_current_coord_y = ball1_center_current_coord_y - ball1_radius;
      graph.FillEllipse(Brushes.Red,(int)ball1_upper_left_current_coord_x,(int)ball1_upper_left_current_coord_y,
                        (float)(2.0*ball1_radius),(float)(2.0*ball1_radius));
   }
   private void Draw_Ball2(object sender,PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
      ball2_upper_left_current_coord_x = ball2_center_current_coord_x - ball2_radius;
      ball2_upper_left_current_coord_y = ball2_center_current_coord_y - ball2_radius;
      graph.FillEllipse(Brushes.Blue,(int)ball2_upper_left_current_coord_x,(int)ball2_upper_left_current_coord_y,
                        (float)(2.0*ball2_radius),(float)(2.0*ball2_radius));
   }
   protected void Intialize_System(Object sender,EventArgs events)
    {
    double direction1;
    double direction2;
    ball1_center_current_coord_x = ball1_center_initial_coord_x;
    ball1_center_current_coord_y = ball1_center_initial_coord_y;
    ball2_center_current_coord_x = ball2_center_initial_coord_x;
    ball2_center_current_coord_y = ball2_center_initial_coord_y;
    //The next statements initialize the data about the ball.
    try
    {  ball1_linear_speed_pix_per_sec = Convert.ToDouble(speed_ball1_textbox.Text);
        direction1 = Convert.ToDouble(direction_ball1_textbox.Text);
        ball2_linear_speed_pix_per_sec = Convert.ToDouble(speed_ball2_textbox.Text);
        direction2 = Convert.ToDouble(direction_ball2_textbox.Text);
    }
    catch(Exception ex)
    {  System.Console.WriteLine("An error occurred while converting the text in the textboxes to numbers.  Please check your input and try again.");
        System.Console.WriteLine("The error message is: {0}", ex.Message);
        return;
    }
    ball1_direction_x = System.Math.Cos(direction1* System.Math.PI/180);
    ball1_direction_y = System.Math.Sin(direction1 * System.Math.PI/180);
    ball1_linear_speed_pix_per_tic = ball1_linear_speed_pix_per_sec / ball_motion_control_clock_rate;
    ball1_delta_x = ball1_linear_speed_pix_per_tic * ball1_direction_x;
    ball1_delta_y = ball1_linear_speed_pix_per_tic * ball1_direction_y;
    ball2_direction_x = System.Math.Cos(direction2* System.Math.PI/180);
    ball2_direction_y = System.Math.Sin(direction2 * System.Math.PI/180);
    ball2_linear_speed_pix_per_tic = ball2_linear_speed_pix_per_sec / ball_motion_control_clock_rate;
    ball2_delta_x = ball2_linear_speed_pix_per_tic * ball2_direction_x;
    ball2_delta_y = ball2_linear_speed_pix_per_tic * ball2_direction_y;
   show_ball1_location_label.Text = string.Format("Red Ball Current Location: ({0}, {1})", (int)ball1_center_current_coord_x, (int)ball1_center_current_coord_y);
   show_ball2_location_label.Text = string.Format("Blue Ball Current Location: ({0}, {1})", (int)ball2_center_current_coord_x, (int)ball2_center_current_coord_y);
   double center_distance =System.Math.Sqrt(
      System.Math.Pow(ball1_center_current_coord_x - ball2_center_current_coord_x, 2) +
      System.Math.Pow(ball1_center_current_coord_y - ball2_center_current_coord_y, 2)
   );
   double distance_between_balls = center_distance - ball1_radius - ball2_radius;
   show_Distance_between_balls_label.Text =
      string.Format("Distance between balls: {0} pixels", (int)distance_between_balls);
    start_button.Enabled = true;  //The start button is enabled after the initialize button is clicked.
    graphic_panel.Invalidate();  //This creates an artificial event so that the graphic area will repaint itself.
    }
    
   protected void All_systems_go(Object sender,EventArgs events)
    {//The refreshclock is started.
      if(ball_collision_occurred)
      {  start_button.BackColor = Color.FromArgb(0,80,80);
         start_button.Text = "Re-Play";
           initialize_button.Enabled = true;
           initialize_button.BackColor = Color.FromArgb(80,80,0);
           initialize_button.Text = "Re-Initialize";
           ball_collision_occurred = false;
           graphic_area_refresh_clock.Enabled = false;
           ball1_motion_control_clock.Enabled = false;
           ball2_motion_control_clock.Enabled = false;
           System.Console.WriteLine("The simulation has ended due to a collision.  Click the Re-Play button to restart.");
         return;
      }
      else if(start_button.Text=="Start" || start_button.Text=="Resume"|| start_button.Text=="Re-Play"){
          if(start_button.Text=="Re-Play"){
          ball_collision_occurred = false; 
          ball1_center_current_coord_x = ball1_center_initial_coord_x;
          ball1_center_current_coord_y = ball1_center_initial_coord_y;
          ball2_center_current_coord_x = ball2_center_initial_coord_x;
          ball2_center_current_coord_y = ball2_center_initial_coord_y; 
          this.Intialize_System(sender, events);  
          }
          start_button.Text = "Pause";
          start_button.BackColor = Color.FromArgb(60,0,80);
          initialize_button.Enabled = false;  //The initialize button is disabled after the start button is clicked.
         initialize_button.BackColor = Color.FromArgb(40,40,0);
         Start_graphic_clock(graphic_refresh_rate);
         //The motion clock is started.
         Start_ball1_clock(ball_motion_control_clock_rate);
         Start_ball2_clock(ball_motion_control_clock_rate);
      }else
          {start_button.Text = "Resume";
           start_button.BackColor = Color.FromArgb(0,80,0);
           graphic_area_refresh_clock.Enabled = false;
           ball1_motion_control_clock.Enabled = false;
           ball2_motion_control_clock.Enabled = false;
           System.Console.WriteLine("The simulation is paused.  Click the start button to resume.");
           return;
          }
    }

   protected void Start_graphic_clock(double refresh_rate)
   {   double actual_refresh_rate = 1.0;  //Minimum refresh rate is 1 Hz to avoid a potential division by a number close to zero
       double elapsed_time_between_tics;
       if(refresh_rate > actual_refresh_rate) 
           actual_refresh_rate = refresh_rate;
       elapsed_time_between_tics = 1000.0/actual_refresh_rate;  //elapsedtimebetweentics has units milliseconds.
       graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_tics);
       graphic_area_refresh_clock.Enabled = true;  //Start clock ticking.
   }

protected void Start_ball1_clock(double update_rate)
   {   double elapsed_time_between_ball1_moves;
       if(update_rate < 1.0) update_rate = 1.0;  //This program does not allow updates slower than 1 Hz.
       elapsed_time_between_ball1_moves = 1000.0/update_rate;  //1000.0ms = 1second.  
       //The variable elapsed_time_between_ball1_moves has units "milliseconds".
       ball1_motion_control_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball1_moves);
       ball1_motion_control_clock.Enabled = true;   //Start clock ticking.
   }
protected void Start_ball2_clock(double update_rate)
   {   double elapsed_time_between_ball2_moves;
       if(update_rate < 1.0) update_rate = 1.0;  //This program does not allow updates slower than 1 Hz.
       elapsed_time_between_ball2_moves = 1000.0/update_rate;  //1000.0ms = 1second.  
       //The variable elapsed_time_between_ball2_moves has units "milliseconds".
       ball2_motion_control_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball2_moves);
       ball2_motion_control_clock.Enabled = true;   //Start clock ticking.
   }
protected void Update_display(System.Object sender, ElapsedEventArgs evt)
   {  graphic_panel.Invalidate();  //This creates an artificial event so that the graphic area will repaint itself.
      //System.Console.WriteLine("The motion(d) clock ticked and the time is {0}", evt.SignalTime);  //Debug statement; remove it later.
      if(!ball1_motion_control_clock.Enabled || !ball2_motion_control_clock.Enabled)
          {graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
      show_ball1_location_label.Text = string.Format("Red Ball Current Location: ({0}, {1})", (int)ball1_center_current_coord_x, (int)ball1_center_current_coord_y);
      show_ball2_location_label.Text = string.Format("Blue Ball Current Location: ({0}, {1})", (int)ball2_center_current_coord_x, (int)ball2_center_current_coord_y);
      double center_distance =System.Math.Sqrt(
        System.Math.Pow(ball1_center_current_coord_x - ball2_center_current_coord_x, 2) +
        System.Math.Pow(ball1_center_current_coord_y - ball2_center_current_coord_y, 2)
      );
      double distance_between_balls = center_distance - ball1_radius - ball2_radius;
      show_Distance_between_balls_label.Text =
         string.Format("Distance between balls: {0} pixels", (int)distance_between_balls);
   }

   protected void Update_ball1_position(System.Object sender, ElapsedEventArgs evt)
   {  ball1_center_current_coord_x += ball1_delta_x;
      ball1_center_current_coord_y -= ball1_delta_y;  //The minus sign is due to the upside down nature of the C# system.
      //System.Console.WriteLine("The motion(b) clock ticked and the time is {0}", evt.SignalTime);//Debug statement; remove later.
      //Dermine if the ball has made a collision with the other ball
      double center_distance =System.Math.Sqrt(
        System.Math.Pow(ball1_center_current_coord_x - ball2_center_current_coord_x, 2) +
        System.Math.Pow(ball1_center_current_coord_y - ball2_center_current_coord_y, 2)
      );
      if(center_distance <= ball1_radius + ball2_radius)
      { 
         ball_collision_occurred = true;
         this.All_systems_go(sender, evt);      
      }
      //Determine if the ball has made a collision with the lower wall

    if(ball1_center_current_coord_y + ball1_radius*2 >= graphicheight)
             ball1_delta_y = -ball1_delta_y;      
      //Determine if the ball has made a collision with the left wall
      if(ball1_center_current_coord_x - ball1_radius <= 0)
             ball1_delta_x = -ball1_delta_x;
      //Determine if the ball has made a collision with the upper wall
    if(ball1_center_current_coord_y - ball1_radius <= 0)  
             ball1_delta_y = -ball1_delta_y;
    //Determine if the ball has made a collision with the right wall
    if(ball1_center_current_coord_x + ball1_radius >= graphic_panel.Width)
             ball1_delta_x = -ball1_delta_x;
   }//End of method Update_ball1_position
   protected void Update_ball2_position(System.Object sender, ElapsedEventArgs evt)
   {  ball2_center_current_coord_x += ball2_delta_x;
      ball2_center_current_coord_y -= ball2_delta_y;  //The minus sign is due to the upside down nature of the C# system.
      //System.Console.WriteLine("The motion(b) clock ticked and the time is {0}", evt.SignalTime);//Debug statement; remove later.
      //Dermine if the ball has made a collision with the other ball
      double center_distance =System.Math.Sqrt(
        System.Math.Pow(ball1_center_current_coord_x - ball2_center_current_coord_x, 2) +
        System.Math.Pow(ball1_center_current_coord_y - ball2_center_current_coord_y, 2)
      );
      if(center_distance <= ball1_radius + ball2_radius)
      { 
         ball_collision_occurred = true;
         this.All_systems_go(sender, evt);      
      }
         //YOU HAVE TO ADD TO BALL1 CLOCK COLLISION DECTECTION AND THEN ALL SYSTEMS GO REPLAY THAT REINITATES
      //Determine if the ball has made a collision with the lower wall

    if(ball2_center_current_coord_y + (int)(ball2_radius*1.9) >= graphicheight)
             ball2_delta_y = -ball2_delta_y;      
      //Determine if the ball has made a collision with the left wall
      if(ball2_center_current_coord_x - ball2_radius <= 0)
             ball2_delta_x = -ball2_delta_x;
      //Determine if the ball has made a collision with the upper wall
    if(ball2_center_current_coord_y - ball2_radius <= 0)  
             ball2_delta_y = -ball2_delta_y;
    //Determine if the ball has made a collision with the right wall
    if(ball2_center_current_coord_x + ball2_radius >= graphic_panel.Width)
             ball2_delta_x = -ball2_delta_x;
   }//End of method Update_ball2_position

}//End of class Ricochet_interface_form



