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
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Ricochet_interface_form : Form
{  //Declare data about the UI:
   private static readonly int formheight = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.95);
    private static readonly int formwidth = formheight;
 

    private Size maxframesize = new Size(formwidth,formheight);
    private Size minframesize = new Size(formwidth,formheight);
    int titleheight = (int)(formheight * 0.1);
    int graphicheight = (int)(formheight * 0.7);
    int lowerpanelheight = (int)(formheight * 0.2);
    
   //Declare data about the ball:
   private double ball_radius = formwidth*0.02;
   private double ball_linear_speed_pix_per_sec;
   private double ball_linear_speed_pix_per_tic;
   private double ball_direction_x;
   private double ball_direction_y;
   private double ball_delta_x;
   private double ball_delta_y;
   private double ball_center_initial_coord_x;
   private double ball_center_initial_coord_y;
   private double ball_center_current_coord_x;
   private double ball_center_current_coord_y;
   private double ball_upper_left_current_coord_x;
   private double ball_upper_left_current_coord_y;

   //Declare data about the motion clock:
   private static System.Timers.Timer ball_motion_control_clock = new System.Timers.Timer();
   private const double ball_motion_control_clock_rate = 43.5;  //Units are Hz

   //Declare data about the refresh clock;
   private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
   private const double graphic_refresh_rate = 23.3;  //Units are Hz = #refreshes per second
   //Declare pannels 
    private Panel title_panel = new Panel();
    private Panel graphic_panel = new Panel();
    private Panel lower_panel = new Panel();
    private Panel button_panel = new Panel();
    private Panel input_panel = new Panel();

    private Panel aspeed_pannel = new Panel();

    //Declare label
    private Label title_label = new Label();
    private Label location_label = new Label();
    private Label comma_label = new Label();
    private Label parenthese_label = new Label();
    private Label speed_label = new Label();
    private Label direction_label = new Label();
    private Label aspeed_label = new Label();
    private Label show_speed_label = new Label();
    //Declare text box
    private TextBox location_x_textbox = new TextBox();
    private TextBox location_y_textbox = new TextBox();
    private TextBox speed_textbox = new TextBox();
    private TextBox direction_textbox = new TextBox();
    //Declare button
    private Button start_button = new Button();
    private Button initialize_button = new Button();
    private Button quit_button = new Button();
    private Button aspeed_increase_button = new Button();
    private Button aspeed_decrease_button = new Button();


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
    ball_center_initial_coord_x = formwidth * 0.5;
    ball_center_initial_coord_y = graphicheight * 0.5 + titleheight;
    ball_center_current_coord_x = ball_center_initial_coord_x;
    ball_center_current_coord_y = ball_center_initial_coord_y;

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
    //show_speed_label properties
    show_speed_label.Text = "Current Speed: 0 pixels/sec";
    show_speed_label.Font = new Font("DejaVu Sans", (int)(formwidth * 0.015), FontStyle.Regular);
    show_speed_label.Location = new Point((int)(formwidth * 0.01), (int)(titleheight * 0.72));  
    show_speed_label.Size = new Size((int)(formwidth * 0.98), (int)(titleheight * 0.28));
    show_speed_label.ForeColor = Color.White; 
    title_panel.Controls.Add(show_speed_label);
    //graphic_panel properties
    graphic_panel.Size = new Size(formwidth,graphicheight);
    graphic_panel.Location = new Point(0,(int)(formheight * 0.1));
    graphic_panel.BackColor = Color.FromArgb(0,0,0); 
    Controls.Add(graphic_panel);
    graphic_panel.Paint += new PaintEventHandler(Draw_Ball);
    //lower_panel properties
    lower_panel.Size = new Size(formwidth,lowerpanelheight);
    lower_panel.Location = new Point(0,(int)(formheight * 0.8));
    lower_panel.BackColor = Color.FromArgb(0,0,0); 
    Controls.Add(lower_panel);
    //button_panel properties
    button_panel.Size = new Size((int)(formwidth*0.2),lowerpanelheight);
    button_panel.Location = new Point((int)(formwidth*0.65),0);
    button_panel.BackColor = Color.FromArgb(0,0,30); 
    lower_panel.Controls.Add(button_panel);
    //Adjust Speed properties
    aspeed_pannel.Size = new Size((int)(formwidth*0.15),lowerpanelheight);
    aspeed_pannel.Location = new Point((int)(formwidth*0.85),0);
    aspeed_pannel.BackColor = Color.FromArgb(0,0,40); 
    lower_panel.Controls.Add(aspeed_pannel);
    //input_panel properties
    input_panel.Size = new Size((int)(formwidth*0.65),lowerpanelheight);
    input_panel.Location = new Point(0,0);
    input_panel.BackColor = Color.FromArgb(0,0,20);
    lower_panel.Controls.Add(input_panel);
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
    //location_label properties
    location_label.Text = "Enter Location ( x,y ): (";
    location_label.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    location_label.Size = new Size((int)(input_panel.Width * 0.47), (int)(input_panel.Height * 0.22));
    location_label.Location = new Point((int)(input_panel.Width * 0.1), (int)(input_panel.Height * 0.06));
    location_label.ForeColor = Color.White;
    input_panel.Controls.Add(location_label);
    //location_x_textbox properties
    location_x_textbox.Size = new Size((int)(input_panel.Width * 0.15), (int)(input_panel.Height * 0.22));
    location_x_textbox.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    location_x_textbox.Location = new Point((int)(input_panel.Width * 0.57), (int)(input_panel.Height * 0.06));
    input_panel.Controls.Add(location_x_textbox);
    //comma_label properties
    comma_label.Text = ",";
    comma_label.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    comma_label.Size = new Size((int)(input_panel.Width * 0.01), (int)(input_panel.Height * 0.22));
    comma_label.Location = new Point((int)(input_panel.Width * 0.72), (int)(input_panel.Height * 0.06));
    comma_label.ForeColor = Color.White;
    input_panel.Controls.Add(comma_label);
    //location_y_textbox properties
    location_y_textbox.Size = new Size((int)(input_panel.Width * 0.15), (int)(input_panel.Height * 0.22));
    location_y_textbox.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    location_y_textbox.Location = new Point((int)(input_panel.Width * 0.74), (int)(input_panel.Height * 0.06));
    input_panel.Controls.Add(location_y_textbox);
    //parenthese_label properties
    parenthese_label.Text = " )";
    parenthese_label.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    parenthese_label.Size = new Size((int)(input_panel.Width * 0.06), (int)(input_panel.Height * 0.22));
    parenthese_label.Location = new Point((int)(input_panel.Width * 0.88), (int)(input_panel.Height * 0.06));
    parenthese_label.ForeColor = Color.White;
    input_panel.Controls.Add(parenthese_label);
    //speed_label properties
    speed_label.Text = "Enter Speed (pixel/sec): ";
    speed_label.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    speed_label.Size = new Size((int)(input_panel.Width * 0.48), (int)(input_panel.Height * 0.22));
    speed_label.Location = new Point((int)(input_panel.Width * 0.1), (int)(input_panel.Height * 0.34));
    speed_label.ForeColor = Color.White;
    input_panel.Controls.Add(speed_label);
    //speed_textbox properties
    speed_textbox.Size = new Size((int)(input_panel.Width * 0.15), (int)(input_panel.Height * 0.22));
    speed_textbox.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    speed_textbox.Location = new Point((int)(input_panel.Width * 0.58), (int)(input_panel.Height * 0.34));
    input_panel.Controls.Add(speed_textbox);
    //direction_label properties
    direction_label.Text = "Enter Direction (degrees): ";
    direction_label.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    direction_label.Size = new Size((int)(input_panel.Width * 0.52), (int)(input_panel.Height * 0.22));
    direction_label.Location = new Point((int)(input_panel.Width * 0.1), (int)(input_panel.Height * 0.62));
    direction_label.ForeColor = Color.White;
    //direction_textbox properties
    direction_textbox.Size = new Size((int)(input_panel.Width * 0.15), (int)(input_panel.Height * 0.22));
    direction_textbox.Font = new Font("DejaVu Sans", (int)(input_panel.Width*0.03), FontStyle.Regular);
    direction_textbox.Location = new Point((int)(input_panel.Width * 0.62), (int)(input_panel.Height * 0.62));
    input_panel.Controls.Add(direction_textbox);
    input_panel.Controls.Add(direction_label);
    //speed adjust label properties
    aspeed_label.Text = "Adjust Speed: ";
    aspeed_label.Font = new Font("DejaVu Sans", (int)(aspeed_pannel.Width*0.09), FontStyle.Regular);
    aspeed_label.Size = new Size((int)(aspeed_pannel.Width * 0.8), (int)(aspeed_pannel.Height * 0.2));
    aspeed_label.Location = new Point((int)(aspeed_pannel.Width * 0.1), (int)(aspeed_pannel.Height * 0.1));
    aspeed_label.ForeColor = Color.White;
    aspeed_pannel.Controls.Add(aspeed_label);
    //speed increase button properties
    aspeed_increase_button.Text = "+10";
    aspeed_increase_button.Font = new Font("DejaVu Sans", (int)(aspeed_pannel.Width*0.09), FontStyle.Regular);
    aspeed_increase_button.Size = new Size((int)(aspeed_pannel.Width * 0.8), (int)(aspeed_pannel.Height * 0.2));
    aspeed_increase_button.Location = new Point((int)(aspeed_pannel.Width * 0.1), (int)(aspeed_pannel.Height * 0.30));
    aspeed_decrease_button.BackColor = Color.FromArgb(80,0,80);
    aspeed_increase_button.ForeColor = Color.White;
    aspeed_pannel.Controls.Add(aspeed_increase_button);
    aspeed_increase_button.Click += new EventHandler(Increase_speed);
    aspeed_increase_button.Enabled = false;  
    //speed decrease button properties
    aspeed_decrease_button.Text = "-10";
    aspeed_decrease_button.Font = new Font("DejaVu Sans", (int)(aspeed_pannel.Width*0.09), FontStyle.Regular);
    aspeed_decrease_button.Size = new Size((int)(aspeed_pannel.Width * 0.8), (int)(aspeed_pannel.Height * 0.2));
    aspeed_decrease_button.Location = new Point((int)(aspeed_pannel.Width * 0.1), (int)(aspeed_pannel.Height * 0.60));
    aspeed_decrease_button.ForeColor = Color.White;
    aspeed_decrease_button.BackColor = Color.FromArgb(0,0,80);
    aspeed_pannel.Controls.Add(aspeed_decrease_button);
    aspeed_decrease_button.Click += new EventHandler(Decrease_speed);
    aspeed_decrease_button.Enabled = false;  
    //Set up the motion clock.  This clock controls the rate of update of the coordinates of the ball.
    ball_motion_control_clock.Enabled = false;
    //Assign a handler to this clock.
    ball_motion_control_clock.Elapsed += new ElapsedEventHandler(Update_ball_position);

    //Set up the refresh clock.
    graphic_area_refresh_clock.Enabled = false;  //Initially the clock controlling the rate of updating the display is stopped.
    //Assign a handler to this clock.
    graphic_area_refresh_clock.Elapsed += new ElapsedEventHandler(Update_display);  

    //Set properties of the button (or maybe buttons)
    start_button.Click += new EventHandler(All_systems_go);

    CenterToScreen();

   }//End of constructor

   private void Draw_Ball(object sender,PaintEventArgs ee)
   {  Graphics graph = ee.Graphics;
      ball_upper_left_current_coord_x = ball_center_current_coord_x - ball_radius;
      ball_upper_left_current_coord_y = ball_center_current_coord_y - ball_radius;
      graph.FillEllipse(Brushes.Red,(int)ball_upper_left_current_coord_x,(int)ball_upper_left_current_coord_y,
                        (float)(2.0*ball_radius),(float)(2.0*ball_radius));
      //The next statement calls the method with the same name located in the super class.
      base.OnPaint(ee);
   }
   protected void Intialize_System(Object sender,EventArgs events)
    {
    double x;
    double y;
    double direction;
    //The next statements initialize the data about the ball.
    try
    {  ball_linear_speed_pix_per_sec = Convert.ToDouble(speed_textbox.Text);
        direction = Convert.ToDouble(direction_textbox.Text);
        x = Convert.ToDouble(location_x_textbox.Text);
        y = Convert.ToDouble(location_y_textbox.Text);
    }
    catch(Exception ex)
    {  System.Console.WriteLine("An error occurred while converting the text in the textboxes to numbers.  Please check your input and try again.");
        System.Console.WriteLine("The error message is: {0}", ex.Message);
        return;
    }
    ball_center_current_coord_x = x;
    ball_center_current_coord_y = y;
    ball_direction_x = System.Math.Cos(direction* System.Math.PI/180);
    ball_direction_y = System.Math.Sin(direction * System.Math.PI/180);
    ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec / ball_motion_control_clock_rate;
    ball_delta_x = ball_linear_speed_pix_per_tic * ball_direction_x;
    ball_delta_y = ball_linear_speed_pix_per_tic * ball_direction_y;
    
    System.Console.WriteLine("The initial speed: {0} pixels/sec \n The initial direction: {1} degrees \n The initial location: ({2}, {3})", ball_linear_speed_pix_per_sec, direction, x, y);
    start_button.Enabled = true;  //The start button is enabled after the system is initialized.
    show_speed_label.Text = string.Format("Current Speed: {0:F2} pixels/sec", ball_linear_speed_pix_per_sec);
    graphic_panel.Invalidate();  //This creates an artificial event so that the graphic area will repaint itself.
    }
    protected void Increase_speed(Object sender,EventArgs events)
    {  ball_linear_speed_pix_per_sec += 10.0;
        ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec / ball_motion_control_clock_rate;
      double new_delta_x = ball_linear_speed_pix_per_tic * ball_direction_x;
      if (ball_delta_x < 0)
      {
         new_delta_x = -new_delta_x;
      }
       ball_delta_x = new_delta_x;
      double new_delta_y = ball_linear_speed_pix_per_tic * ball_direction_y;
      if (ball_delta_y < 0)
      {
         new_delta_y = -new_delta_y;
      }
       ball_delta_y = new_delta_y;
       show_speed_label.Text = string.Format("Current Speed: {0:F2} pixels/sec", ball_linear_speed_pix_per_sec);
       Console.WriteLine("The current speed is: {0:F2} pixels/sec", ball_linear_speed_pix_per_sec);
    }
    protected void Decrease_speed(Object sender,EventArgs events)
    {  ball_linear_speed_pix_per_sec -= 10.0;
       if(ball_linear_speed_pix_per_sec < 0) ball_linear_speed_pix_per_sec = 0.0;
       ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec / ball_motion_control_clock_rate;
      double new_delta_x = ball_linear_speed_pix_per_tic * ball_direction_x;
      if (ball_delta_x < 0)
      {
         new_delta_x = -new_delta_x;
      }
       ball_delta_x = new_delta_x;
      double new_delta_y = ball_linear_speed_pix_per_tic * ball_direction_y;
      if (ball_delta_y < 0)
      {
         new_delta_y = -new_delta_y;
      }
       ball_delta_y = new_delta_y;
       show_speed_label.Text = string.Format("Current Speed: {0:F2} pixels/sec", ball_linear_speed_pix_per_sec);
       Console.WriteLine("The current speed is: {0:F2} pixels/sec", ball_linear_speed_pix_per_sec);
    }

   protected void All_systems_go(Object sender,EventArgs events)
    {//The refreshclock is started.
      if(start_button.Text=="Start" || start_button.Text=="Resume"){
          start_button.Text = "Pause";
          start_button.BackColor = Color.FromArgb(60,0,80);
          initialize_button.Enabled = false;  //The initialize button is disabled after the start button is clicked.
         initialize_button.BackColor = Color.FromArgb(40,40,0);
         aspeed_increase_button.Enabled = true;  //The increase speed button is enabled after the start button is clicked.
          aspeed_decrease_button.Enabled = true;  //The decrease speed button is enabled after the start button is clicked.
         Start_graphic_clock(graphic_refresh_rate);
         //The motion clock is started.
         Start_ball_clock(ball_motion_control_clock_rate);
      }else
          {start_button.Text = "Resume";
           start_button.BackColor = Color.FromArgb(0,80,0);
           initialize_button.Enabled = true;
           initialize_button.BackColor = Color.FromArgb(80,80,0);
           initialize_button.Text = "Re-Initialize";
           graphic_area_refresh_clock.Enabled = false;
           ball_motion_control_clock.Enabled = false;
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

protected void Start_ball_clock(double update_rate)
   {   double elapsed_time_between_ball_moves;
       if(update_rate < 1.0) update_rate = 1.0;  //This program does not allow updates slower than 1 Hz.
       elapsed_time_between_ball_moves = 1000.0/update_rate;  //1000.0ms = 1second.  
       //The variable elapsed_time_between_ball_moves has units "milliseconds".
       ball_motion_control_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball_moves);
       ball_motion_control_clock.Enabled = true;   //Start clock ticking.
   }

protected void Update_display(System.Object sender, ElapsedEventArgs evt)
   {  graphic_panel.Invalidate();  //This creates an artificial event so that the graphic area will repaint itself.
      //System.Console.WriteLine("The motion(d) clock ticked and the time is {0}", evt.SignalTime);  //Debug statement; remove it later.
      if(!ball_motion_control_clock.Enabled)
          {graphic_area_refresh_clock.Enabled = false;
           System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
          }
   }

   protected void Update_ball_position(System.Object sender, ElapsedEventArgs evt)
   {  ball_center_current_coord_x += ball_delta_x;
      ball_center_current_coord_y -= ball_delta_y;  //The minus sign is due to the upside down nature of the C# system.
      //System.Console.WriteLine("The motion(b) clock ticked and the time is {0}", evt.SignalTime);//Debug statement; remove later.

      //Determine if the ball has made a collision with the lower wall

    if(ball_center_current_coord_y + ball_radius >= graphicheight)
             ball_delta_y = -ball_delta_y;      
      //Determine if the ball has made a collision with the left wall
      if(ball_center_current_coord_x - ball_radius <= 0)
             ball_delta_x = -ball_delta_x;
      //Determine if the ball has made a collision with the upper wall
    if(ball_center_current_coord_y - ball_radius <= 0)  
             ball_delta_y = -ball_delta_y;
    //Determine if the ball has made a collision with the right wall
    if(ball_center_current_coord_x + ball_radius >= graphic_panel.Width)
             ball_delta_x = -ball_delta_x;
   }//End of method Update_ball_position

}//End of class Ricochet_interface_form



