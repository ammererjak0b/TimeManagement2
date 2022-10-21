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
using TimeMgmtLib;

namespace TimeMgmtWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TimeMgmtFactory.Instance.Initialise();
        }

        //Button to exit Application
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Button to maximize Application
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        //Button to minimize Application
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Homepage_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to Page "Homepage"
            PagesNavigation.Navigate(new System.Uri("Pages/Homepage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Graphs_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to Page "Graphs"
            PagesNavigation.Navigate(new System.Uri("Pages/Graphs.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to Page "Settings"
            PagesNavigation.Navigate(new System.Uri("Pages/Settings.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Overview_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to Page "Overview"
            PagesNavigation.Navigate(new System.Uri("Pages/Overview.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            //if (TimeMgmtFactory.Instance.UserInstance.GetIsLoggedIn())
            //{
            //    //TODO: Navigate to profile page
            //}
            //else
            //{
            //Navigate to Page "Login"
            PagesNavigation.Navigate(new System.Uri("Pages/Login.xaml", UriKind.RelativeOrAbsolute));
            //}
            // TODO: make log out button somewhere
        }
    }
}
