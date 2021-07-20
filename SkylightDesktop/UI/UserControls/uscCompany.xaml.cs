using Camguard.Business.Service;
using Skylight.DataAccess.Services;
using Skylight.Models;
using SkylightDesktop.UI.Windows;
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

namespace SkylightDesktop.UI.UserControls
{
    /// <summary>
    /// Interaction logic for uscCompany.xaml
    /// </summary>
    public partial class uscCompany : UserControl
    {
        public static CompanyService employeeService = new CompanyService();
        public static HashSet<Company> data = new HashSet<Company>();
        public uscCompany()
        {
            InitializeComponent();
        }

        private void contCompany_Loaded(object sender, RoutedEventArgs e)
        {
            var result =  GlobalService.GlobalCompany;
            data = result.ToHashSet();
            grdCompany.ItemsSource = data;

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            HashSet<Company> searchResult = new HashSet<Company>();
            try
            {
                var tasks = new List<Task>();
                string searchTerm = txtSearch.Text;
                var t = Task.Run(() => {
                    searchResult = data.Where(a => (!string.IsNullOrEmpty(a.CompanyName) && a.CompanyName.Contains(searchTerm))
               || !string.IsNullOrEmpty(a.Description) && a.Description.Contains(searchTerm)).ToHashSet();
                });
                tasks.Add(t);

                Task result = Task.WhenAll(tasks); ;
                try
                {
                    result.Wait();
                }
                catch { }
                grdCompany.ItemsSource = searchResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            HashSet<Company> searchResult = new HashSet<Company>();
            try
            {
                var tasks = new List<Task>();
                string searchTerm = txtSearch.Text;
                var t = Task.Run(() => {
                     searchResult = data.Where(a => (!string.IsNullOrEmpty(a.CompanyName) && a.CompanyName.Contains(searchTerm))
                || !string.IsNullOrEmpty(a.Description) && a.Description.Contains(searchTerm)).ToHashSet();
                });
                tasks.Add(t);

                Task result = Task.WhenAll(tasks); ;
                try
                {
                     result.Wait();
                }
                catch { }
                grdCompany.ItemsSource = searchResult;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            //frmCompany frm = new frmCompany();
            //frm.ShowDialog();
        }


        private void grdCompany_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdCompany.SelectedItem == null) return;
            var data = grdCompany.SelectedItem as Company;
            uscEmployee emp = new uscEmployee(data.CompanyID);
            var frm = new Window();
            frm.Title = data.CompanyName;
            frm.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frm.Content = emp;
            frm.Show();
        }

    }
}
