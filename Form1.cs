using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDP_AnimatedWalkingMan
{
    public partial class Form1 : Form
    {
        private Image animation; //Image object that holds picture of the animated image and x,y coordinates
        private List<string> imageMovements = new List<string>();//stores the filenames of  frame images
        private int steps = 0; //identifies index of imageMovements list
        private int slowDownFrameRate = 0; //used to slow down the switch between frames

        private int xCoord = 0; //image starting coordinates
        private int yCoord = 0;
        private int speed = 5; //speed image moves at
        private string direction = null;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;  //needed for smooth drawing of images
            imageMovements = System.IO.Directory.GetFiles($"images/walkingManFrames", "*.gif").ToList();//load image file names into a list
            animation = Image.FromFile(imageMovements[22]);// set the initial player image to the first item in the list
        }

        private void PaintEvent(object sender, PaintEventArgs e)
        {//Form paint event
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(animation, xCoord, 0, 400, 300);
        }
       
        private void refreshImage(object sender, EventArgs e)
        {//game timer event
            this.Invalidate();
            if (direction == "right" && (xCoord+300) < this.Width) //if the right hand key has been pressed then move right
            {
                xCoord += speed; //change the x coordinate
                animateImage(0, imageMovements.Count() - 1); //animate the image
            }
            else //if right key not pressed
            {
                animation = Image.FromFile(imageMovements[22]); //set image back to standing image
            }

        }

        private void animateImage(int start, int end)
        {//procedure to change the image file, incrementing the image location index each time it is called

            slowDownFrameRate += 1;
            if (slowDownFrameRate == 2) //wait until the procedure has been called a number of times before changing the image
                                         //this slows down the rate at which the image changes
            {
                steps++; //go to next image
                slowDownFrameRate = 0; //frame rate countdown again
            }
            if (steps > end || steps < start) //identify when to loop back to first image
            {
                steps = start;
            }
            animation = Image.FromFile(imageMovements[steps]);//change the image

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {//identify which key is down
            if (e.KeyCode == Keys.Right)
            {//when right key is held down set direction to right
                direction = "right";
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            direction = null;
        }
    }
}
