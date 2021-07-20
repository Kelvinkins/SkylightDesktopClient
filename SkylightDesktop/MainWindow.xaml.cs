using Camguard.Business.Service;
using SkylightDesktop.UI.UserControls;
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

namespace SkylightDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        public static uscEmployee uscEmployee = new uscEmployee();
        public static uscCompany uscCompany = new uscCompany();

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            homePlace.Children.Remove(uscEmployee);

        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            homePlace.Children.Remove(uscEmployee);

        }

        private void btnEnrollees_Click(object sender, RoutedEventArgs e)
        {
            homePlace.Children.Clear();
            homePlace.Children.Add(uscEmployee);

        }

        private void btnCompanies_Click(object sender, RoutedEventArgs e)
        {
            homePlace.Children.Clear();
            homePlace.Children.Add(uscCompany);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
