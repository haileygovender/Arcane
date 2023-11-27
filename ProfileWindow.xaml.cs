using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        UserDetails ud = new UserDetails();
        public ProfileWindow(UserDetails userDetails)
        {
            InitializeComponent();
            // Get the username and points from the UserDetails instance
            ud=userDetails;
            var username = ud.Username;
            var points = ud.UserPoints;

            // Determine the rank
            var rank = DetermineRank(points);

            // Create and set the DataContext
            DataContext = new UserDetails { Username = username, UserPoints = points, UserRank = rank };

            // Set the text in the UI
            txtUser.Text = username;
            txtPoints.Text = points.ToString();
            txtRank.Text = rank;
        }

        public static string DetermineRank(int points)
        {
            string rank = "";
          

            if (points == 0 || points < 500)
            {
                rank = "Rookie";
            }
            else if (points >= 500 || points < 1000)
            {
                rank = "Amateur";
            }
            else if (points >= 1000 || points < 2000)
            {
                rank = "Expert";
            }
            else if (points >= 2000)
            {
                rank = "Master";
            }
            else if (points>=3000)
            {
                rank = "Grand Master";
            }

            return rank;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var home = new HomeWindow(ud);
            home.Show();
            this.Close();
        }
    }

}
