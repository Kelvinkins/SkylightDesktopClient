using Camguard.Business.Service;
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
    /// Interaction logic for uscEmployee.xaml
    /// </summary>
    public partial class uscEmployee : UserControl
    {
        public static EmployeeService employeeService = new EmployeeService();
        public static HashSet<Employee> data = new HashSet<Employee>();
        public static int CompanyID { get; set; }
        public static bool LoadByCompany { get; set; }

        public uscEmployee()
        {
            InitializeComponent();
            LoadByCompany = false;

        }
        public uscEmployee(int companyID)
        {
            InitializeComponent();
            CompanyID = companyID;
            LoadByCompany = true;
        }

        private void contEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            if (LoadByCompany)
            {
                var result = employeeService.GetAllAsync(3000, 0).Result;
                data = result.data.Where(a => a.CompanyID == CompanyID).ToHashSet();
            }
            else
            {
                var result = employeeService.GetAllAsync(3000, 0).Result;
                data = result.data.ToHashSet();
            }
            grdEmployee.ItemsSource = data;


        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            HashSet<Employee> searchResult = new HashSet<Employee>();
            try
            {
                var tasks = new List<Task>();
                string searchTerm = txtSearch.Text;
                var t = Task.Run(() => {
                    searchResult = data.Where(a => (!string.IsNullOrEmpty(a.Surname) && a.Surname.Contains(searchTerm))
               || !string.IsNullOrEmpty(a.OtherName) && a.OtherName.Contains(searchTerm)).ToHashSet();
                });
                tasks.Add(t);

                Task result = Task.WhenAll(tasks); ;
                try
                {
                    result.Wait();
                }
                catch { }
                grdEmployee.ItemsSource = searchResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            HashSet<Employee> searchResult = new HashSet<Employee>();
            try
            {
                var tasks = new List<Task>();
                string searchTerm = txtSearch.Text;
                var t = Task.Run(() => {
                     searchResult = data.Where(a => (!string.IsNullOrEmpty(a.Surname) && a.Surname.Contains(searchTerm))
                || !string.IsNullOrEmpty(a.OtherName) && a.OtherName.Contains(searchTerm)).ToHashSet();
                });
                tasks.Add(t);

                Task result = Task.WhenAll(tasks); ;
                try
                {
                     result.Wait();
                }
                catch { }
                grdEmployee.ItemsSource = searchResult;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            frmEmployee frm = new frmEmployee();
            frm.ShowDialog();
        }


        private void grdEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdEmployee.SelectedItem == null) return;
            var data = grdEmployee.SelectedItem as Employee;
            var frm = new frmEmployee(data);
            frm.Title = $"{data.Surname} {data.OtherName}({data.OldID})";
            frm.Show();
            //MessageBox.Show(string.Format("The Person you double clicked on is - Name: {0}, Address: {1}, Email: {2}", selecteEmployee.Surname, selecteEmployee.OtherName, selecteEmployee.Email));
        }

    }
}
