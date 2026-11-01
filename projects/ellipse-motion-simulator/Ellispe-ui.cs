//Author: Lavinia Hawkins
//Course: CPSC 223N
//Assignment 5 
//Due: 2026-May-5
//Program name: Ellispe Ball
//Purpose:  This program simulates the motion of a ball moving at a constant speed along an ellispe.  The user can specify the 
//speed of the ball in pixels per second.  The user can also pause and resume the motion of the ball.  The current speed and 
//location of the ball are displayed in the lower panel of the user interface.
//Programming language: C Sharp
//Date development of program began: 2026-April-25
//Date of last update: N/A
//Files in project: Ellispe-main.cs, Ellispe-ui.cs, run.sh
//This file name: Ellispe-ui.cs
//This file's name: Ellispe-ui.cs
//This file purpose: This file will implement the user interface
//Date last modified: 2026-May-4

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class Ellispe_interface_form : Form
{  //Declare data about the UI:
    private static readonly int formheight = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.95);
    private static readonly int formwidth = formheight;


    private Size maxframesize = new Size(formwidth, formheight);
    private Size minframesize = new Size(formwidth, formheight);
    private static readonly int titleheight = (int)(formheight * 0.1);
    private static readonly int graphicheight = (int)(formheight * 0.7);
    private static readonly int lowerpanelheight = (int)(formheight * 0.2);

    //Declare data about the ball:
    private double ball_radius = formwidth * 0.02;
    private double ball_linear_speed_pix_per_sec;
    private double ball_linear_speed_pix_per_tic;
    private static readonly double ellispe_center_x = formwidth * 0.5;
    private static readonly double ellispe_center_y = graphicheight * 0.5;
    private static readonly double ellispe_major_radius = formwidth * 0.46;
    private static readonly double ellispe_minor_radius = graphicheight * 0.46;
    private double ellispe_parameter_t;
    private double ellispe_delta_t;
    private double distance_per_tic;
    private double ball_center_current_coord_x = ellispe_center_x + ellispe_major_radius;
    private double ball_center_current_coord_y = ellispe_center_y;
    private double ball_upper_left_current_coord_x;
    private double ball_upper_left_current_coord_y;

    //Declare data about the motion clock:
    private static System.Timers.Timer ball_motion_control_clock = new System.Timers.Timer();
    private const double ball_motion_control_clock_rate = 50;  //Units are Hz

    //Declare data about the refresh clock;
    private static System.Timers.Timer graphic_area_refresh_clock = new System.Timers.Timer();
    private const double graphic_refresh_rate = 50;  //Units are Hz = #refreshes per second
                                                     //Declare pannels 
    private Panel title_panel = new Panel();
    private Panel ball_panel = new Panel();
    private Panel track_panel = new Panel();
    private Panel lower_panel = new Panel();
    private Panel button_panel = new Panel();
    private Panel input_panel = new Panel();
    private Bitmap static_graphic_background;
    private Panel display_pannel = new Panel();

    //Declare label
    private Label title_label = new Label();
    private Label speed_label = new Label();
    private Label show_speed_label = new Label();
    private Label display_title_label = new Label();
    private Label show_location_label = new Label();
    //Declare text box
    private TextBox speed_textbox = new TextBox();
    //Declare button
    private Button start_button = new Button();
    private Button quit_button = new Button();


    public Ellispe_interface_form(double speed, double v, double w)   //The constructor of this class
    {
        Width = formwidth;
        Height = formheight;
        MaximumSize = maxframesize;
        MinimumSize = minframesize;
        //Set the title of this form.
        Text = "Ellispe Motion";
        System.Console.WriteLine("formwidth = {0}. formheight = {1}.", formwidth, formheight);
        //Set the initial size of this form
        Size = new Size(formwidth, formheight);
        //Set the background color of this form
        BackColor = Color.Green;

        //Initialize ball coordinates now that sizes are known

        //title properties
        title_panel.Size = new Size(formwidth, titleheight);
        title_panel.Location = new Point(0, 0);
        title_panel.BackColor = Color.FromArgb(0, 20, 0); // dark header
        System.Console.WriteLine("title panel height = {0}, title panel width = {1}.", title_panel.Height, title_panel.Width);
        Controls.Add(title_panel);
        //title_label properties
        title_label.Text = "Ellispe Ball by Lavinia Hawkins";
        title_label.Font = new Font("DejaVu Sans", (int)(titleheight * 0.43), FontStyle.Bold);
        title_label.Location = new Point((int)(formwidth * 0.01), (int)(titleheight * 0.15));
        title_label.Size = new Size((int)(formwidth * 0.98), (int)(titleheight * 0.7));
        title_label.ForeColor = Color.White;
        title_label.TextAlign = ContentAlignment.MiddleCenter;
        title_panel.Controls.Add(title_label);
        //track_panel properties
        track_panel.Size = new Size(formwidth, graphicheight);
        track_panel.Location = new Point(0, titleheight);
        track_panel.BackColor = Color.Black;
        Controls.Add(track_panel);

        //Build and attach background after track_panel has size
        Build_Static_Background();
        track_panel.BackgroundImage = static_graphic_background;
        track_panel.BackgroundImageLayout = ImageLayout.None;

        //ball_panel properties
        ball_panel.Size = track_panel.Size;
        ball_panel.Location = new Point(0, 0);
        ball_panel.BackColor = Color.Transparent;
        ball_panel.Paint += new PaintEventHandler(DrawBall);

        track_panel.Controls.Add(ball_panel);
        ball_panel.BringToFront();

        //lower_panel properties
        lower_panel.Size = new Size(formwidth, lowerpanelheight);
        lower_panel.Location = new Point(0, (int)(formheight * 0.8));
        lower_panel.BackColor = Color.FromArgb(0, 0, 0);
        Controls.Add(lower_panel);
        //button_panel properties
        button_panel.Size = new Size((int)(formwidth * 0.12), lowerpanelheight);
        button_panel.Location = new Point((int)(formwidth * 0.88), 0);
        button_panel.BackColor = Color.FromArgb(0, 0, 40);
        lower_panel.Controls.Add(button_panel);
        //Display Values Panel properties
        display_pannel.Size = new Size((int)(formwidth * 0.43), lowerpanelheight);
        display_pannel.Location = new Point(0, 0);
        display_pannel.BackColor = Color.FromArgb(0, 0, 20);
        lower_panel.Controls.Add(display_pannel);
        //display_title_label properties
        display_title_label.Text = "Current Values";
        display_title_label.Font = new Font("DejaVu Sans", (int)(display_pannel.Height * 0.4 * 0.35), FontStyle.Bold);
        display_title_label.Location = new Point((int)(display_pannel.Width * 0.02), (int)(display_pannel.Height * 0.02));
        display_title_label.Size = new Size((int)(display_pannel.Width * 0.96), (int)(display_pannel.Height * 0.4));
        display_title_label.ForeColor = Color.White;
        display_title_label.TextAlign = ContentAlignment.MiddleCenter;
        display_pannel.Controls.Add(display_title_label);
        //show_speed_label properties
        show_speed_label.Text = "Current Speed: 0 pixels/sec";
        show_speed_label.Font = new Font("DejaVu Sans", (int)(display_pannel.Height * 0.26 * 0.35), FontStyle.Regular);
        show_speed_label.Location = new Point((int)(display_pannel.Width * 0.02), (int)(display_pannel.Height * 0.42));
        show_speed_label.Size = new Size((int)(display_pannel.Width * 0.96), (int)(display_pannel.Height * 0.26));
        show_speed_label.ForeColor = Color.White;
        display_pannel.Controls.Add(show_speed_label);
        //show_location_label properties
        show_location_label.Text = string.Format("Current Location: ({0}, {1})", ball_center_current_coord_x, ball_center_current_coord_y);
        show_location_label.Font = new Font("DejaVu Sans", (int)(display_pannel.Height * 0.2 * 0.35), FontStyle.Regular);
        show_location_label.Location = new Point((int)(display_pannel.Width * 0.02), (int)(display_pannel.Height * 0.7));
        show_location_label.Size = new Size((int)(display_pannel.Width * 0.96), (int)(display_pannel.Height * 0.26));
        show_location_label.ForeColor = Color.White;
        display_pannel.Controls.Add(show_location_label);
        //input_panel properties
        input_panel.Size = new Size((int)(formwidth * 0.45), lowerpanelheight);
        input_panel.Location = new Point((int)(formwidth * 0.43), 0);
        input_panel.BackColor = Color.FromArgb(0, 0, 30);
        lower_panel.Controls.Add(input_panel);
        //start_button properties
        start_button.Text = "Go";
        start_button.Font = new Font("DejaVu Sans", (int)(button_panel.Height * 0.18 * 0.45), FontStyle.Regular);
        start_button.Size = new Size((int)(button_panel.Width * 0.8), (int)(button_panel.Height * 0.22));
        start_button.Location = new Point((int)(button_panel.Width * 0.1), (int)(button_panel.Height * 0.18));
        start_button.BackColor = Color.FromArgb(0, 40, 0);
        start_button.ForeColor = Color.White;
        button_panel.Controls.Add(start_button);
        //quit_button properties
        quit_button.Text = "Quit";
        quit_button.Font = new Font("DejaVu Sans", (int)(button_panel.Height * 0.25 * 0.45), FontStyle.Regular);
        quit_button.Size = new Size((int)(button_panel.Width * 0.8), (int)(button_panel.Height * 0.22));
        quit_button.Location = new Point((int)(button_panel.Width * 0.1), (int)(button_panel.Height * 0.53));
        quit_button.BackColor = Color.FromArgb(80, 0, 0);
        quit_button.ForeColor = Color.White;
        button_panel.Controls.Add(quit_button);
        quit_button.Click += new EventHandler((sender, e) => { Application.Exit(); });
        //speed_label properties
        speed_label.Text = "Enter Speed (pixel/sec): ";
        speed_label.Font = new Font("DejaVu Sans", (int)(input_panel.Width * 0.0425), FontStyle.Regular);
        speed_label.Size = new Size((int)(input_panel.Width * 0.69), (int)(input_panel.Height * 0.4));
        speed_label.Location = new Point((int)(input_panel.Width * 0.02), (int)(input_panel.Height * 0.4));
        speed_label.ForeColor = Color.White;
        input_panel.Controls.Add(speed_label);
        //speed_textbox properties
        speed_textbox.Size = new Size((int)(input_panel.Width * 0.25), (int)(input_panel.Height * 0.4));
        speed_textbox.Font = new Font("DejaVu Sans", (int)(input_panel.Width * 0.0425), FontStyle.Regular);
        speed_textbox.Location = new Point((int)(input_panel.Width * 0.73), (int)(input_panel.Height * 0.4));
        input_panel.Controls.Add(speed_textbox);
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
    private void Build_Static_Background()
    {
        static_graphic_background = new Bitmap(track_panel.Width, track_panel.Height);

        Graphics graph = Graphics.FromImage(static_graphic_background);

        graph.Clear(track_panel.BackColor);

        Pen trackpen = new Pen(Color.FromArgb(255, 255, 255), (float)(formwidth * 0.005));
        Brush trackbrush = new SolidBrush(Color.FromArgb(5, 5, 5));
        Pen guidepen = new Pen(Color.FromArgb(225, 225, 225), (float)(formwidth * 0.005));

        graph.DrawEllipse(trackpen, (int)(ellispe_center_x - ellispe_major_radius), (int)(ellispe_center_y - ellispe_minor_radius),
                            (int)(2 * ellispe_major_radius), (int)(2 * ellispe_minor_radius));
        graph.FillEllipse(trackbrush, (int)(ellispe_center_x - ellispe_major_radius), (int)(ellispe_center_y - ellispe_minor_radius),
                            (int)(2 * ellispe_major_radius), (int)(2 * ellispe_minor_radius));

        graph.DrawLine(guidepen, 0, (int)(graphicheight * 0.5), formwidth, (int)(graphicheight * 0.5));//Draw the minor axis
        graph.DrawLine(guidepen, (int)(formwidth * 0.5), 0, (int)(formwidth * 0.5), graphicheight); //Draw the major axis

        graph.Dispose();
    }
    private void DrawBall(object sender, PaintEventArgs ee)
    {
        Graphics graph = ee.Graphics;


        ball_upper_left_current_coord_x = ball_center_current_coord_x - ball_radius;
        ball_upper_left_current_coord_y = ball_center_current_coord_y - ball_radius;
        graph.FillEllipse(Brushes.Red, (int)ball_upper_left_current_coord_x, (int)ball_upper_left_current_coord_y,
                                (float)(2.0 * ball_radius), (float)(2.0 * ball_radius));
    }

    protected void All_systems_go(Object sender, EventArgs events)
    {//The refreshclock is started.
        if (start_button.Text == "Go")
        {
            try
            {
                ball_linear_speed_pix_per_sec = Convert.ToDouble(speed_textbox.Text);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("An error occurred while converting the text in the textboxes to numbers.  Please check your input and try again.");
                System.Console.WriteLine("The error message is: {0}", ex.Message);
                return;
            }
            ball_linear_speed_pix_per_tic = ball_linear_speed_pix_per_sec / ball_motion_control_clock_rate;

            System.Console.WriteLine("The initial speed: {0} pixels/sec ", ball_linear_speed_pix_per_sec);
            show_speed_label.Text = string.Format("Current Speed: {0:F2} pixels/sec", ball_linear_speed_pix_per_sec);
            show_location_label.Text = string.Format("Current Location: ({0:F2}, {1:F2})", ball_center_current_coord_x, ball_center_current_coord_y);
            ball_panel.Invalidate();
            start_button.Text = "Pause";
            start_button.BackColor = Color.FromArgb(60, 0, 80);
            Start_graphic_clock(graphic_refresh_rate);
            Start_ball_clock(ball_motion_control_clock_rate);
        }
        else if (start_button.Text == "Resume")
        {
            start_button.Text = "Pause";
            start_button.BackColor = Color.FromArgb(60, 0, 80);
            Start_graphic_clock(graphic_refresh_rate);
            Start_ball_clock(ball_motion_control_clock_rate);

        }
        else
        {
            start_button.Text = "Resume";
            start_button.BackColor = Color.FromArgb(0, 80, 0);
            graphic_area_refresh_clock.Enabled = false;
            ball_motion_control_clock.Enabled = false;
            System.Console.WriteLine("The simulation is paused.  Click the Resume button to continue.");
            return;
        }
    }

    protected void Start_graphic_clock(double refresh_rate)
    {
        double actual_refresh_rate = 1.0;  //Minimum refresh rate is 1 Hz to avoid a potential division by a number close to zero
        double elapsed_time_between_tics;
        if (refresh_rate > actual_refresh_rate)
            actual_refresh_rate = refresh_rate;
        elapsed_time_between_tics = 1000.0 / actual_refresh_rate;  //elapsedtimebetweentics has units milliseconds.
        graphic_area_refresh_clock.Interval = (int)System.Math.Round(elapsed_time_between_tics);
        graphic_area_refresh_clock.Enabled = true;  //Start clock ticking.
    }

    protected void Start_ball_clock(double update_rate)
    {
        double elapsed_time_between_ball_moves;
        if (update_rate < 1.0) update_rate = 1.0;  //This program does not allow updates slower than 1 Hz.
        elapsed_time_between_ball_moves = 1000.0 / update_rate;  //1000.0ms = 1second.  
                                                                 //The variable elapsed_time_between_ball_moves has units "milliseconds".
        ball_motion_control_clock.Interval = (int)System.Math.Round(elapsed_time_between_ball_moves);
        ball_motion_control_clock.Enabled = true;   //Start clock ticking.
    }

    protected void Update_display(System.Object sender, ElapsedEventArgs evt)
    {
        ball_panel.Invalidate();  //This creates an artificial event so that the graphic area will repaint itself.
                                  //System.Console.WriteLine("The motion(d) clock ticked and the time is {0}", evt.SignalTime);  //Debug statement; remove it later.
        if (!ball_motion_control_clock.Enabled)
        {
            graphic_area_refresh_clock.Enabled = false;
            System.Console.WriteLine("The graphical area is no longer refreshing.  You may close the window.");
        }
    }

    protected void Update_ball_position(System.Object sender, ElapsedEventArgs evt)
    {
        distance_per_tic = ball_linear_speed_pix_per_tic;
        double x_prime = ellispe_major_radius * System.Math.Cos(ellispe_parameter_t);
        double y_prime = -ellispe_minor_radius * System.Math.Sin(ellispe_parameter_t);
        ellispe_delta_t = distance_per_tic / System.Math.Sqrt(System.Math.Pow(x_prime, 2) + System.Math.Pow(y_prime, 2));
        ellispe_parameter_t += ellispe_delta_t;

        ball_center_current_coord_x = ellispe_center_x + ellispe_major_radius * System.Math.Cos(ellispe_parameter_t);
        ball_center_current_coord_y = ellispe_center_y - ellispe_minor_radius * System.Math.Sin(ellispe_parameter_t);
        show_location_label.Text = string.Format("Current Location: ({0:F2}, {1:F2})", ball_center_current_coord_x, ball_center_current_coord_y);
    }//End of class Ellispe_interface_form
}//End of file Ellispe-ui.cs


