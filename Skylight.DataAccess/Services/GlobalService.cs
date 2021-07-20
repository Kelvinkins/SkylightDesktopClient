using Newtonsoft.Json;
using Skylight.Data.Models;
using Skylight.DataAccess.IContract;
using Skylight.DataAccess.Settings;
using Skylight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Skylight.DataAccess.Services
{
    public class GlobalService: IGlobalService
    {
        public static readonly HttpClient client = new HttpClient();
        public static HashSet<State> GlobalState = new HashSet<State>();
        public static HashSet<Company> GlobalCompany = new HashSet<Company>();
        public static HashSet<CompanyPolicy> GlobalCompanyPolicy = new HashSet<CompanyPolicy>();
        public static HashSet<Provider> GlobalProvider = new HashSet<Provider>();
        public static HashSet<Plan> GlobalPlan = new HashSet<Plan>();

        public async Task<(HashSet<State> data, bool status, string message)> LoadStates()
        {
            HashSet<State> states = new HashSet<State>();
            bool stat = false;
            string msg = "";
            try
            {
                var content = await client.GetAsync($"{BaseAddress.TestBaseAddress}/api/Employees/States").ConfigureAwait(false);
                var raw = await content.Content.ReadAsStringAsync();
                states = JsonConvert.DeserializeObject<HashSet<State>>(raw);
                stat = true;
                msg = "Record retrieved successfully";
            }
            catch (Exception ex)
            {
                stat = false;
                msg = $"Failed to retrieve record, error message: {ex.Message}";
            }
            return (states, stat, msg);

        }

        public async Task<(HashSet<Company> data, bool status, string message)> LoadCompanies()
        {
            HashSet<Company> companies = new HashSet<Company>();
            bool stat = false;
            string msg = "";
            try
            {
                var content = await client.GetAsync($"{BaseAddress.TestBaseAddress}/api/Employees/Companies").ConfigureAwait(false);
                var raw = await content.Content.ReadAsStringAsync();
                companies = JsonConvert.DeserializeObject<HashSet<Company>>(raw);
                stat = true;
                msg = "Record retrieved successfully";
            }
            catch (Exception ex)
            {
                stat = false;
                msg = $"Failed to retrieve record, error message: {ex.Message}";
            }
            return (companies, stat, msg);
        }


        public async Task<(HashSet<Provider> data, bool status, string message)> LoadProviders()
        {
            HashSet<Provider> providers = new HashSet<Provider>();
            bool stat = false;
            string msg = "";
            try
            {
                var content = await client.GetAsync($"{BaseAddress.TestBaseAddress}/api/Employees/Providers").ConfigureAwait(false);
                var raw = await content.Content.ReadAsStringAsync();
                providers = JsonConvert.DeserializeObject<HashSet<Provider>>(raw);
                stat = true;
                msg = "Record retrieved successfully";
            }
            catch (Exception ex)
            {
                stat = false;
                msg = $"Failed to retrieve record, error message: {ex.Message}";
            }
            return (providers, stat, msg);
        }

  

       public async Task<(HashSet<Plan> data, bool status, string message)> LoadPolicy()
        {
            HashSet<Plan> companyPolicies = new HashSet<Plan>();
            bool stat = false;
            string msg = "";
            try
            {
                var content = await client.GetAsync($"{BaseAddress.TestBaseAddress}/api/Employees/Plans").ConfigureAwait(false);
                var raw = await content.Content.ReadAsStringAsync();
                companyPolicies = JsonConvert.DeserializeObject<HashSet<Plan>>(raw);
                stat = true;
                msg = "Record retrieved successfully";
            }
            catch (Exception ex)
            {
                stat = false;
                msg = $"Failed to retrieve record, error message: {ex.Message}";
            }
            return (companyPolicies, stat, msg);
        }

        public async Task<(HashSet<CompanyPolicy> data, bool status, string message)> LoadCompanyPolicy()
        {
            HashSet<CompanyPolicy> companyPolicies = new HashSet<CompanyPolicy>();
            bool stat = false;
            string msg = "";
            try
            {
                var content = await client.GetAsync($"{BaseAddress.TestBaseAddress}/api/Employees/CompanyPolicies").ConfigureAwait(false);
                var raw = await content.Content.ReadAsStringAsync();
                companyPolicies = JsonConvert.DeserializeObject<HashSet<CompanyPolicy>>(raw);
                stat = true;
                msg = "Record retrieved successfully";
            }
            catch (Exception ex)
            {
                stat = false;
                msg = $"Failed to retrieve record, error message: {ex.Message}";
            }
            return (companyPolicies, stat, msg);
        }



    }
}
