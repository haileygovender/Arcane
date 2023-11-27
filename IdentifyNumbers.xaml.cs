using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ST10084623_PROG7312
{
    /// <summary>
    /// Interaction logic for IdentifyNumbers.xaml
    /// </summary>
    public partial class IdentifyNumbers : Window
    {
        //initiating class
        private UserDetails ud;
        // global variables
        DispatcherTimer dt = new DispatcherTimer();
      
        // timer variable
        private int increment = 0;

        private int countFirstAttempts = 0;
        private int countTime = 0;
        // method to place timer in label
        private void Dt_Tick(object sender, EventArgs e)
        {
            increment++;

            lblTimer.Content = increment.ToString();

            if (increment ==25 )
            {
                ShowTimedMessageBox("Time is running out! Points will decrease if you don't complete the task quickly.", 5); //last for s seconds 
                ++countTime;
            }
            else if (increment == 50)
            {
                ShowTimedMessageBox("Time is running out! Points will decrease significantly if you don't complete the task quickly.", 5);
                ++countTime;
            }
        }

        public IdentifyNumbers(UserDetails userDetails)
        {
            InitializeComponent();
            this.ud = userDetails;
        }

        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
           
                btnBegin.Visibility = Visibility.Hidden;
                firstLevelList.Visibility = Visibility.Visible;
                firstLevelList.IsEnabled = true;
                btnFirstNext.Visibility = Visibility.Visible;
                btnReset.Visibility = Visibility.Hidden;

                Callnumber.LoadCallNumbers();

                // Goal to acheive
                LblAnswer.Content = Callnumber.myGoal;

                // preparation to display the correct UI elements
                thirdLevelList.Items.Clear();
                secondLevelList.Items.Clear();
                firstLevelList.Items.Clear();

                foreach (string i in Callnumber.firstLevelChoice)
                {
                    firstLevelList.Items.Add(i);
                }

                foreach (string i in Callnumber.secondLevelChoice)
                {
                    secondLevelList.Items.Add(i);
                }

                foreach (string i in Callnumber.thirdLevelChoice)
                {
                    thirdLevelList.Items.Add(i);
                }

                // creating timer to start on button click
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick += Dt_Tick;
                dt.Start();
            }

        private void btnFirstNext_Click(object sender, RoutedEventArgs e)
        {
            // error message if user selects nothing
            if (firstLevelList.SelectedItem == null)
            {
                MessageBox.Show("Come on, smarty pants! Pick an item to get started.");

            }
            else
            {
                // check users answer
                if (firstLevelList.SelectedItem.Equals(Callnumber.firstLevelAnswer))
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    var corResponses = new List<string> { "Excellent, that's correct!", "Wow, you nailed it!", "Great job!" };
                    int choice = rand.Next(corResponses.Count);
                    MessageBox.Show(corResponses[choice]);
                    // show next phase of game
                    firstLevelList.IsEnabled = true;
                    btnFirstNext.Visibility = Visibility.Hidden;
                    secondLevelList.Visibility = Visibility.Visible;
                    secondLevelList.IsEnabled = true;
                    btnSecondNext.Visibility = Visibility.Visible;
                    btnBegin.Visibility = Visibility.Hidden;
                    countFirstAttempts = 0;

                }
                else
                {
                    ++countFirstAttempts;
                    if (countFirstAttempts >= 3)
                    {
                        var attempts = new AttemptsWindow();
                        attempts.Show();
                    }
                    else
                    {
                        // negative random feedback for wrong answer
                        Random rand = new Random(Guid.NewGuid().GetHashCode());
                        var wrongResponses = new List<string> { "Incorrect choice, keep trying!", "Don't give up, try again!", "There's room for improvement, give it another shot." };
                        int choice = rand.Next(wrongResponses.Count);
                        MessageBox.Show(wrongResponses[choice]);
                    }
                   

                   
                }
            }

           
        }

        private void btnSecondNext_Click(object sender, RoutedEventArgs e)
        {
            //Error message if user selects nothing

            if (secondLevelList.SelectedItem == null)

            {

                MessageBox.Show("Ahh a trickster, You must choose if you want Glory.");

            }

            else

            {

                //Check user answer

                if (secondLevelList.SelectedItem.Equals(Callnumber.secondLevelAnswer))

                {

                    //Give Random positive feeback to user

                    Random rand = new Random(Guid.NewGuid().GetHashCode());

                    var corResponses = new List<string> { "Excellent, that's correct!", "Impressive! You're one step closer to victory. Well done!", "Spot on! You made that seem easy." };

                    int choice = rand.Next(corResponses.Count);

                    MessageBox.Show(corResponses[choice]);

                    //Show next phase of game

                    secondLevelList.IsEnabled = false;

                    btnSecondNext.Visibility = Visibility.Hidden;

                    thirdLevelList.Visibility = Visibility.Visible;

                    thirdLevelList.IsEnabled = true;

                    btnSubmit.Visibility = Visibility.Visible;

                    countFirstAttempts = 0;

                }

                else

                {
                    ++countFirstAttempts;
                    if (countFirstAttempts >= 3)
                    {
                        var attempts = new AttemptsWindow();
                        attempts.Show();
                    }
                    else
                    {

                        //Random Negative feedback for wrong answer

                        Random rand = new Random(Guid.NewGuid().GetHashCode());

                        var wroResponses = new List<string> { "Incorrect choice!", "Oh, I thought you had it. Not quite right!", "You were close, but your choice is incorrect." };

                        int choice = rand.Next(wroResponses.Count);

                        MessageBox.Show(wroResponses[choice]);

                    }

                    

                }

            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            // error message if user selects nothing
            if (thirdLevelList.SelectedItem == null)
            {
                MessageBox.Show("Ah, a clever one! You must decide if you seek glory.");
            }
            else
            {
                // check users answer
                string selectedAnswer = thirdLevelList.SelectedItem.ToString().Trim();

                // Check if the selected answer is contained in the list of correct answers
                if (Callnumber.thirdLevelAnswer.Contains(selectedAnswer))
                {
                    dt.Stop();
                    var winner = new WinnerBadge();
                    winner.Show();

                    // show next phase of the game or perform other actions as needed
                    thirdLevelList.IsEnabled = true;
                    btnReset.Visibility = Visibility.Visible;
                  

                }
                else
                {
                    ++countFirstAttempts;
                    if (countFirstAttempts >= 3)
                    {
                        var attempts = new AttemptsWindow();
                        attempts.Show();
                    }
                    else
                    {
                        // negative random feedback for the wrong answer
                        Random rand = new Random(Guid.NewGuid().GetHashCode());
                        var wrongResponses = new List<string> { "You're wrong", "Incorrect choice", "Try again next time" };
                        int choice = rand.Next(wrongResponses.Count);
                        MessageBox.Show(wrongResponses[choice]);
                        btnReset.Visibility = Visibility.Visible;
                    }
                    // debugging purposes
                    // MessageBox.Show("Correct Answers: " + string.Join(", ", Callnumber.thirdLevelAnswer));
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        { 
                btnBegin.Visibility = Visibility.Hidden;
                firstLevelList.Visibility = Visibility.Visible;
                firstLevelList.IsEnabled = true;
                btnFirstNext.Visibility = Visibility.Visible;
                secondLevelList.Visibility = Visibility.Hidden;
                secondLevelList.Visibility = Visibility.Hidden;
                btnSecondNext.Visibility = Visibility.Hidden;
                thirdLevelList.Visibility = Visibility.Hidden;

            //checking the number of attempts to allow user to receive points
            if (countFirstAttempts==0)
            {
                ud.UserPoints += 10;
            }
            else
            {
                ud.UserPoints += 5;
            }


            countFirstAttempts = 0;
            // Reset Timer
            increment = 0;
            dt.Interval = TimeSpan.FromSeconds(1);
           // dt.Tick += Dt_Tick;
            dt.Start();

            Callnumber.LoadCallNumbers();

                // Goal to acheive
                LblAnswer.Content = Callnumber.myGoal;

                // preparation to display the correct UI elements
                thirdLevelList.Items.Clear();
                secondLevelList.Items.Clear();
                firstLevelList.Items.Clear();

                foreach (string i in Callnumber.firstLevelChoice)
                {
                    firstLevelList.Items.Add(i);
                }

                foreach (string i in Callnumber.secondLevelChoice)
                {
                    secondLevelList.Items.Add(i);
                }

                foreach (string i in Callnumber.thirdLevelChoice)
                {
                    thirdLevelList.Items.Add(i);
                }

         
            
            btnReset.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomWindow messageBox = new CustomWindow();

            bool? result = messageBox.ShowDialog();
            if (result == true)
            {
                //checking the number of attempts to allow user to receive points
                if (countFirstAttempts == 0)
                {
                    ud.UserPoints += 15 - countTime;
                }
                else
                {
                    ud.UserPoints += 10 - countTime;
                }
                var home = new HomeWindow(ud);
                home.Show();
                this.Close();
            }
        }


        private void ShowTimedMessageBox(string message, int seconds)
        {
            MessageBox.Show(message);

            // Use a DispatcherTimer to automatically close the MessageBox after the specified time
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(seconds);
            timer.Tick += (sender, e) =>
            {
                // Close the MessageBox
                timer.Stop();
                //Application.Current.Windows.OfType<Window>() is used to find the first window of type MessageBox and close it
                var messageBox = Application.Current.Windows.OfType<Window>().FirstOrDefault(window => window is MessageBox);
                if (messageBox != null)
                {
                    messageBox.Close();
                }
            };
            timer.Start();
        }
       
    }
}
