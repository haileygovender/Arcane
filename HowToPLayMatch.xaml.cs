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
    /// Interaction logic for HowToPLayMatch.xaml
    /// </summary>
    public partial class HowToPLayMatch : Window
    {
        private UserDetails _userDetails;
        public HowToPLayMatch(UserDetails userDetails)
        {
            InitializeComponent();
            _userDetails = userDetails;
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
          
            var home = new HomeWindow(_userDetails);
            home.Show();
            this.Close();
        }
    }
}
