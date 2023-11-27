using System;
using System.Collections;
using System.Collections.Generic;
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

namespace ST10084623_PROG7312
{
   
    public partial class OrderWindow : Window
    {
        ArrayList myList;
        private UserDetails ud;
        //retrieving string value
        string currentItem = string.Empty;
        //current index value of string
        int index = 0;

        int ascendingCount = 0;
        int points = 0;

        LoadList load = new LoadList();

        private int initialTimer = 15; // Set your initial countdown time here
        private int countdownSeconds = 15;
        private DispatcherTimer timer;

        public OrderWindow(UserDetails userDetails)
        {
            InitializeComponent();
         this.ud=userDetails;
        }


        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start(); // Start the timer
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (countdownSeconds > 0)
            {
                countdownSeconds--;
                countdownText.Text = countdownSeconds.ToString();
                countdownProgressBar.Value = countdownSeconds;
            }
            else
            {
                timer.Stop();
                countdownText.Text = "Time's up!";
                countdownProgressBar.Value = 0;
                btnDone.IsEnabled = false;

                
                    RunOutWindow messageBox = new RunOutWindow();
                    messageBox.ShowDialog();
                
               
            }
        }
       

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.Items.Count==0)
            {
              btnDone.IsEnabled = false;
            }
            else
            {
                btnDone.IsEnabled = true;
                timer.Stop();
                CheckAscendingOrder();

            }
           

        }

        private void ShowCustomMessageBox(string message)
        {
            CustomScore messageBox = new CustomScore(message);
            messageBox.ShowDialog();
        }

        private  void CheckAscendingOrder()
        {
            int count = listBox2.Items.Count;//checks the number of items in listbox2

            if (listBox2.Items.Count < 10)
            {//if the number of items are less than 10
                btnDone.IsEnabled = false;
            }
            else
            {//else loop between the items are and check which are in correct position
                for (int i = 1; i < count; i++)
                {
                    string item1 = listBox2.Items[i - 1].ToString();
                    string item2 = listBox2.Items[i].ToString();

                    if (string.Compare(item1, item2) <= 0)
                    {
                        ascendingCount++;// count the number of correct positions
                    }

                }

                if (ascendingCount == 9)
                {
                    ShowCustomMessageBox($"You have scored {(ascendingCount + 1)*10} of 10");
                    points += (ascendingCount + 1)*10;
                    tbScore.Text = $" {points}";
                }
                else
                {
                    ShowCustomMessageBox($"You have scored {ascendingCount.ToString()} out of 10");
                    btnAnswer.IsEnabled = true;
                    points += ascendingCount *10;
                    tbScore.Text = $" {points}";
                }

               
               
                btnDone.IsEnabled = false;
                btnAnswer.IsEnabled = true;
                btnAdd.IsEnabled = false;
                btnRemove.IsEnabled = false;
            }




        }
        private void bindNewList()
        {

            listBox1.ItemsSource = null;
            listBox1.ItemsSource = myList;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //whena user has clicked done without completing task
            btnDone.IsEnabled = true;
            if (listBox1.SelectedItem==null)
            {
                return;
            }
                //retrieving value of selected item
                currentItem = listBox1.SelectedValue.ToString();
                index = listBox1.SelectedIndex;
                //add item to listbox2
                listBox2.Items.Add(currentItem);
                lbAnswer.Items.Add(currentItem);
                if (myList != null) // if list box 1 not null
                {
                    myList.RemoveAt(index);// remove item from that current index
                }

                bindNewList();
            
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            btnDone.IsEnabled = true;
            if (listBox2.SelectedItem==null)
            { return; }
                currentItem = listBox2.SelectedValue.ToString();// selects value
                index = listBox2.SelectedIndex;
                //add value back into the list
                myList.Add(currentItem);
                //remove value from selected index item in the listbox
                listBox2.Items.RemoveAt(listBox2.Items.IndexOf(listBox2.SelectedItem));
                lbAnswer.Items.RemoveAt(lbAnswer.Items.IndexOf(currentItem));
                bindNewList();
            
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            lbAnswer.Visibility=Visibility.Visible;

            countdownSeconds = initialTimer;
            countdownText.Text = countdownSeconds.ToString();
            countdownProgressBar.Maximum = initialTimer;
            countdownProgressBar.Value = initialTimer;
            InitializeTimer();

            listBox2.Items.Clear();
            lbAnswer.Items.Clear();
            myList = load.loadListBox();
            //binding listbox with list
            listBox1.ItemsSource = myList;
            ascendingCount = 0;
            lbAnswer.IsEnabled = false;
            btnDone.IsEnabled = true;
            btnAnswer.IsEnabled = false;
            btnAdd.IsEnabled = true;
            btnRemove.IsEnabled = true;
        }

        private void BubbleSortList(ListBox listBox)
        {
            int itemCount = listBox.Items.Count;
            for (int i = 0; i < itemCount - 1; i++)
            {
                for (int j = 0; j < itemCount - 1 - i; j++)
                {
                    ListBoxItem listBoxItem1 = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(j);
                    ListBoxItem listBoxItem2 = (ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(j + 1);

                    if (listBoxItem1 != null && listBoxItem2 != null)
                    {
                        string item1 = listBoxItem1.Content.ToString();
                        string item2 = listBoxItem2.Content.ToString();

                        // Compare and swap if necessary to sort in ascending order
                        if (string.Compare(item1, item2) > 0)
                        {
                            // Swap the items in the ListBox
                            string temp = item1;
                            listBoxItem1.Content = item2;
                            listBoxItem2.Content = temp;
                        }
                    }
                }
            }

         
        }


        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            
            BubbleSortList(lbAnswer);
            lbAnswer.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomWindow messageBox = new CustomWindow();

            bool? result = messageBox.ShowDialog();
            if (result == true)
            {
               ud.UserPoints+= points;
               
                var home = new HomeWindow(ud);
                home.Show();
                this.Close();

            }
           
        }
    }
}
