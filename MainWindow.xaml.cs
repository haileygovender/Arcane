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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ST10084623_PROG7312
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //created an object of the time dispatcher
        DispatcherTimer dt = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            dt.Tick += new EventHandler(dt_Tick);//created an event
            dt.Interval = new TimeSpan(0, 0, 5);//how long the form will take to display the next
            dt.Start();//starts

        }
        private void dt_Tick(object sender, EventArgs e)
        {
            // object of the window
            var user = new UsernameWindow();
            user.Show();//shows window
            dt.Stop();//stops timer
            this.Close();//closes this window
        }
    }
}
