using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
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

namespace ST10084623_PROG7312
{
    /// <summary>
    /// Interaction logic for Matching.xaml
    /// </summary>
    public partial class MatchWindow : Window
    {
        //initiating User Details class to pull data
        private UserDetails ud;
        public MatchWindow(UserDetails userDetails)
        {
            InitializeComponent();
            this.ud = userDetails;
        }

        public static int qCount = 4;
        public static bool callQ = true;
        public static int myScore;
        public static int maxScore;
        public

        Random rnd = new Random();
        IDictionary<string, string> baseQuestions = new Dictionary<string, string>();
        IDictionary<string, string> useQuestions = new Dictionary<string, string>();

        //dewey decimal syste, identify areas

        private void loadDefaults()
        {
            baseQuestions.Clear();
            useQuestions.Clear();

            //List of possible questions
            baseQuestions.Add("000-999", "General Works");
            baseQuestions.Add("100-999", "Philosophy and Psychology");
            baseQuestions.Add("200-999", "Religion");
            baseQuestions.Add("300-999", "Social Sciences");
            baseQuestions.Add("400-999", "Language");
            baseQuestions.Add("500-999", "Natural Sciences and Mathematics");
            baseQuestions.Add("600-999", "Technology");
            baseQuestions.Add("700-999", "The arts");
            baseQuestions.Add("800-999", "Literature and Rhetoric");
            baseQuestions.Add("900-999", "History, Biography and Geography");
        }

        internal static void FindChildren<T>(List<T> results, DependencyObject startNode)
            where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);

            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) ||
                    (current.GetType()).GetTypeInfo().IsSubclassOf(typeof(T)))
                {
                    T asType = (T)current;
                    results.Add(asType);

                }
                FindChildren<T>(results, current);

            }
        }

        private int CalcScore()
        {
            int score = 0;
            List<ListBoxItem> list = new List<ListBoxItem>();
            FindChildren(list,listboxTwo ) ;
            for (int i = 0; i < qCount; i++)
            {
                string callNumber;
                string description;
                if (!callQ)
                {
                    callNumber = listboxOne.Items[i].ToString();
                    description = listboxTwo.Items[i].ToString();
                }
                else
                {
                    callNumber = listboxTwo.Items[i].ToString();
                    description = listboxOne.Items[i].ToString();
                }
                //now deal with the used qs and unused qa
                if (useQuestions[callNumber] == description)
                {
                    //pick a color to ligh them up
                    //right answer
                    list[i].Background = new SolidColorBrush(Colors.Green);
                    score++;
                }
                else
                {
                    list[i].Background = new SolidColorBrush(Colors.Red);
                }
            }
          /*  for (int i = qCount; i < listboxTwo.Items.Count; i++)
            {
                list[i].Background = new SolidColorBrush(Colors.PaleVioletRed);
            }*/
            return score;
        }


        private void btnMoveUp_Click(object sender, RoutedEventArgs e)
        {
            //up btn
            Controls.SwapIndexes(-1, listboxTwo);
            //can pull the recolor option here
        }

        private void btnMoveDown_Click(object sender, RoutedEventArgs e)
        {
            //up btn
            Controls.SwapIndexes(1, listboxTwo);
            //can pull the recolor option here
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //submit btn
            //submit answers to be checked
            //pull the mthod --setup the textblocks
            //and gamification features--if you need to

            int score = CalcScore();

            btnMoveUp.IsEnabled = false;
            btnMoveDown.IsEnabled = false;

            //any other btns or features you want to disable at this stage

            loadDefaults();


            //load the defaults
            myScore = myScore + score;//add to the score
            maxScore = maxScore + qCount;//checks that you cant excute

            if (score == 4)
            {
                WinnerWindow messageBox = new WinnerWindow();
                messageBox.ShowDialog();
            }
            else
            {
                RunOutWindow messageBox = new RunOutWindow();
                messageBox.ShowDialog();
            }

            scoreTb.Text = "Score: " + myScore + "/" + maxScore;
           
         
        }


        //Dictionary --> kvp --> method
        private void getKVP(out string call, out string desc)
        {
            KeyValuePair<string, string> kvp;
            int index = rnd.Next(baseQuestions.Count());
            kvp = baseQuestions.ElementAt(index);
            useQuestions.Add(kvp);
            baseQuestions.Remove(kvp);
            call = kvp.Key;
            desc = kvp.Value;
        }

        private void repopulatLists()
        {
            listboxOne.Items.Clear();
            listboxTwo.Items.Clear();
            //alt between call numbers and descriptions
            if (callQ)
            {
                //generate 4 callNo + 4 DEscriptions
                for (int i = 0; i < 4; i++)
                {
                    getKVP(out string callNo, out string desc);
                    listboxOne.Items.Add(callNo);
                    listboxTwo.Items.Add(desc);
                }
                //generate 3 morre desc
                for (int i = 0; i < 3; i++)
                {
                    getKVP(out _, out string desc);
                    listboxTwo.Items.Add(desc);
                }
                //Prep alt
                callQ = false;
            }
            else
            {
                //generate 4 callNo + 4 DEscriptions
                for (int i = 0; i < 4; i++)
                {
                    getKVP(out string callNo, out string desc);
                    listboxOne.Items.Add(desc);
                    listboxTwo.Items.Add(callNo);
                }
                //generate 3 morre desc
                for (int i = 0; i < 3; i++)
                {
                    getKVP(out string callNo, out _);
                    listboxTwo.Items.Add(callNo);
                }
                //Prep alt
                callQ = true;
            }
            //TODO shuffle lists
            Controls.randomizeList(listboxOne);
            Controls.randomizeList(listboxTwo);
        }


        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            //new game btn
            if (btnSubmit.IsEnabled == true)
            {
               CustomNewGame messageBox = new CustomNewGame();

                bool? result = messageBox.ShowDialog();
                if (result==true)
                {
                    ud.UserPoints += myScore *10; // increments points each time user scores points
                    myScore = 0;
                    maxScore = 0;
                    scoreTb.Text = "MATCH COLUMNS";
                    btnSubmit.IsEnabled = true;
                    btnMoveDown.IsEnabled = true;
                    btnMoveUp.IsEnabled = true;
                }
                else
                {
                    return;  //prep alt 

                }

            }

            loadDefaults();
            repopulatLists();

            //btns enabled
            btnMoveUp.IsEnabled = true;
            btnMoveDown.IsEnabled = true;
            btnSubmit.IsEnabled = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CustomWindow messageBox = new CustomWindow();

            bool? result = messageBox.ShowDialog();
            if (result == true)
            {

                ud.UserPoints += myScore * 10; // increments points each time user scores points
                var home = new HomeWindow(ud);
                home.Show();
                this.Close();

            }
        }

       
    }
}
