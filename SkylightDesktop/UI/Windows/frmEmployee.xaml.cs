using Camguard.Business.Service;
using Skylight.Data.Models;
using Skylight.DataAccess.Services;
using Skylight.Models;
using Skylight.Models.Enums;
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
using System.Windows.Shapes;

namespace SkylightDesktop.UI.Windows
{


   
    /// <summary>
    /// Interaction logic for frmEmployee.xaml
    /// </summary>
    public partial class frmEmployee : Window
    {


       void initLocalData()
        {
            cmbMaritalStatus.ItemsSource = Enum.GetValues(typeof(MaritalStat)).Cast<MaritalStat>();
            cmbGenotype.ItemsSource = Enum.GetValues(typeof(GenType)).Cast<GenType>();
            cmbGender.ItemsSource = Enum.GetValues(typeof(Sex)).Cast<Sex>();
            cmbStates.ItemsSource = GlobalService.GlobalState;
            cmbStates.DisplayMemberPath = "Name";
            cmbStates.SelectedValuePath = "StateID";

 

        }
        public Employee data = new Employee();
        public bool IsNew = true;
        public frmEmployee()
        {
            InitializeComponent();
            this.DataContext = data;
            IsNew = true;
            btnSaveChanges.Content = "Save";
            initLocalData();

        }

        public frmEmployee(Employee data)
        {
            this.data = data;
            InitializeComponent();
            this.DataContext = data;
            IsNew = false;
            var purchasePlans = GlobalService.GlobalCompanyPolicy.Where(a=>a.CompanyID==data.CompanyID);
            var plans = new List<Plan>();
            foreach (var plan in purchasePlans)
            {
                var pln = GlobalService.GlobalPlan.Where(a => a.PolicyID == plan.PolicyID).FirstOrDefault();
                plans.Add(pln);
            }

            cmbPlans.ItemsSource = plans;
            cmbPlans.DisplayMemberPath = "PolicyName";
            cmbPlans.SelectedValuePath = "PolicyID";

            var provider = GlobalService.GlobalProvider.Where(a => a.ProviderID == data.ProviderID).FirstOrDefault();
            cmbProviders.ItemsSource = GlobalService.GlobalProvider.Where(a => a.ProviderLevelID >= provider.ProviderLevelID);
            cmbProviders.DisplayMemberPath = "ProviderName";
            cmbProviders.SelectedValuePath = "ProviderID";
            btnSaveChanges.Content = "Update";
            initLocalData();
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                //data
                var result = uscEmployee.employeeService.AddAsync(data).Result;
                MessageBox.Show(result.message);
                uscEmployee.data.Add(result.data);
            }
            else
            {
                var result = uscEmployee.employeeService.UpdateAsync(data).Result;
                MessageBox.Show(result.message);
                var itemToRemove = uscEmployee.data.Where(a => a.Id == result.data.Id).FirstOrDefault();
                uscEmployee.data.Remove(itemToRemove);
                uscEmployee.data.Add(result.data);
            }
           
        }

        private void cmbPlans_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPlans.SelectedValue == null) return;
            var data = GlobalService.GlobalPlan.Where(a => a.PolicyID == cmbPlans.SelectedValue.ToString()).FirstOrDefault();
            //To be revisited
            cmbProviders.ItemsSource = GlobalService.GlobalProvider.Where(a=>a.ProviderLevelID>= data.ProviderLevelID);
            cmbProviders.DisplayMemberPath = "ProviderName";
            cmbProviders.SelectedValuePath = "ProviderID";
        }
    }
}
