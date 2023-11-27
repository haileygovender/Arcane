using System;
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
using System.Windows.Shapes;

namespace ST10084623_PROG7312
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        //initiating user details class
        private UserDetails userDetails;

        public HomeWindow(UserDetails userDetails)
        {
            InitializeComponent();
            //declaring the class
            this.userDetails = userDetails;
           
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
           var order= new OrderWindow(userDetails);
            order.Show();
            this.Close();
        }

        private void btnMatch_Click(object sender, RoutedEventArgs e)
        {
            //allowing each window to have access of the data stored in the class
            var match = new MatchWindow(userDetails);
            match.Show();
            this.Close();   
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //close application

            CustomWindow messageBox = new CustomWindow();
            bool? result = messageBox.ShowDialog();

            if (result == true)
            {
                // User clicked "Yes"
                Application.Current.Shutdown();
            }
        }

        private void btnplayOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = new HowToPlayOrder(userDetails);
            order.Show();
            this.Close();
        }

        private void btnplayMatch_Click(object sender, RoutedEventArgs e)
        {
            var match = new HowToPLayMatch(userDetails);
            match.Show();
            this.Close();
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            var identify = new IdentifyNumbers(userDetails);
            identify.Show();
            this.Close();
        }

       
        private void btnplayIdentify_Click(object sender, RoutedEventArgs e)
        {
            var identify = new HowToPlayIndentify(userDetails);
            identify.Show();
            this.Close();
        }

        private void btnProfle_Click(object sender, RoutedEventArgs e)
        {
           
            var profile = new ProfileWindow(userDetails);
            profile.Show();
            this.Close();
        }
    }
}
