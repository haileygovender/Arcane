﻿using System;
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
    /// Interaction logic for UsernameWindow.xaml
    /// </summary>
    public partial class UsernameWindow : Window
    {
        public UsernameWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            UserDetails ud = new UserDetails();
            ud.Username = txtName.Text;
            var home = new HomeWindow(ud);
            home.Show();
            this.Close();
        }
    }
}
