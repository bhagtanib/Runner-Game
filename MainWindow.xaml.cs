using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RunnerGame
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        //tracks player direction state.
        bool goLeft, goRight;

        //tracks the player's score
        int score;

        //tracks if the player has just been hit
        bool justHit = false;

        //stores player speed
        int playerSpeed = 25;

        //stores asteroid speed
        double speed = 10;

        //tracks the "time" of the session
        private int increment = 0;

        //allows player to be hit three times
        int HP = 3;


        DispatcherTimer gameTimer = new DispatcherTimer();
        



        public MainWindow()
        {
            //IsRunning = false;
            InitializeComponent();
            myCanvas.Focus();
            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            
       
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    goLeft = true;
                    break;
                case Key.D:
                    goRight = true;
                    break;
            }
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {

            // THIS IS THE GAME LOOP
            // got help with this from https://www.youtube.com/watch?v=kSoVL6MuL5o&ab_channel=MooICT

            // to stop the program https://stackoverflow.com/questions/2820357/how-do-i-exit-a-wpf-application-programmatically
            if (HP <= 0)
            {
                score = Convert.ToInt32(TimeLabel.Content.ToString());

                //help with making file https://www.c-sharpcorner.com/UploadFile/mahesh/create-a-text-file-in-C-Sharp/#:~:text=1%20.%20The%20following%204%20methods%20can%20be,several%20ways%20to%20create%20a%20file%20in%20C%23.
                string fileName = @"C:\Users\adang24\source\repos\cs-371-final-project-runner-game\RunnerGame\Scores.txt";
                
                
                try
                {
                    // for help with making files https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/read-write-text-file#:~:text=The%20WriteLine%20method%20writes%20a%20complete%20line%20of,Types%2C%20and%20then%20select%20Console%20Application%20under%20Templates.
                    StreamWriter file = new StreamWriter(fileName, true, Encoding.ASCII);
                    file.Write($"Your score is {score}\n");
                    file.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                
                MessageBox.Show("You lost all three lives. You lose!");
                
                System.Windows.Application.Current.Shutdown();
            }   
                
                
            // timer stuff
            increment++;
            TimeLabel.Content = increment.ToString();
            


            if (goLeft && Canvas.GetLeft(player) > 5)// if player is hitting A key and not on border of the screen
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed); // move the player left
            }
            if(goRight && Canvas.GetLeft(player) + (player.Width + 20) < Application.Current.MainWindow.Width) //if D is pressed and player is not off screen to the right
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);// move player right
            }
            // make astroids spawn randomly
            RandomizeRec(AsteroidPNG);
            RandomizeRec(AsteroidPNG2);
            RandomizeRec(AsteroidPNG3);
            //check for collisions between all asteroids and player
            checkCollision(AsteroidPNG);
            checkCollision(AsteroidPNG2);
            checkCollision(AsteroidPNG3);
        }

        //checks if player collides with an asteroid
        public void checkCollision(Rectangle coll)
        {
            double rect1x = Canvas.GetLeft(player);
            double rect1y = Canvas.GetTop(player);
            double rect2x = Canvas.GetLeft(coll);
            double rect2y = Canvas.GetTop(coll);
            double rect1W = player.Width;
            double rect2W = coll.Width;
            double rect1H = player.Height;
            double rect2H = coll.Height;

            if((rect1x < (rect2x + rect2W)) && ((rect1x + rect1W) > rect2x) && (rect1y < (rect2y + rect2H)) && ((rect1H + rect1y) > rect2y))
            {
                if (justHit == false) // cooldown so you dont die from one hit
                {
                    Console.Beep();
                    justHit = true;
                    HP--;
                    myCanvas.Background = new SolidColorBrush(Colors.Red); // to indicate you got hit
                }

            }
        }

        //randomizes the position of each asteroid
        public void RandomizeRec(Rectangle picture)
        {
            Random rnd = new Random();

            if (Canvas.GetTop(picture) > 550) // if asteroid is out of bounds
            {
                myCanvas.Background = new SolidColorBrush(Colors.White);
                justHit = false;

                picture.Height = rnd.Next(50) + 20;
                int randomNum = Convert.ToInt32(picture.Width);
                Canvas.SetTop((picture), rnd.Next(100) - 300); // rand num between 0 and -200
                Canvas.SetLeft((picture), rnd.Next(400 - randomNum));
            }
            if (Canvas.GetTop(picture) < 550) // if asteroid is NOT out of bounds
            {
                Canvas.SetTop(picture, Canvas.GetTop(picture) + speed); // move the asteroid down
            }
            if (speed <50) //top speed is 50
            {
                speed += Convert.ToInt32(TimeLabel.Content.ToString()) * 0.00001; // change the speed so the game gets harder over time
            }
        }


        //processes when keyboard keys are (not) being pressed.
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    goLeft = false;
                    break;
                case Key.D:
                    goRight = false;
                    break;
            }
        }

    }
}
