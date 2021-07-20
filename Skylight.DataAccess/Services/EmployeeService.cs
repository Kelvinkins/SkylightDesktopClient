using Camguard.Business.IContract;
using Newtonsoft.Json;
using Skylight.Data.ViewModel;
using Skylight.DataAccess.Services;
using Skylight.DataAccess.Settings;
using Skylight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Camguard.Business.Service
{
    public class EmployeeService : IEmployeeService
    {

        public EmployeeService()
        {
        }



        public async Task<(HashSet<Employee> data, bool status, string message)> GetAllAsync(int limit, int offset)
        {
            HashSet<Employee> employees = new HashSet<Employee>();
            bool stat = false;
            string msg = "";
            try
            {
                var content = await GlobalService.client.GetAsync($"{BaseAddress.TestBaseAddress}/api/Employees/List?limit={limit}&offset={offset}&status=0").ConfigureAwait(false);
                var raw = await content.Content.ReadAsStringAsync();
                employees =  JsonConvert.DeserializeObject<HashSet<Employee>>(raw);
                stat = true;
                msg = "Record retrieved successfully";
            }
            catch (Exception ex)
            {
                stat = false;
                msg = $"Failed to retrieve record, error message: {ex.Message}";
            }
            return (employees, stat, msg);

        }

        public async Task<Employee> GetByIDAsync(int ID)
        {
            GlobalService.client.DefaultRequestHeaders.Accept.Clear();
            GlobalService.client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var streamTask = GlobalService.client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            //var employees = await JsonConvert.DeserializeObject<Employee>(await streamTask);
            //return employees;
            return null;
        }

    

        public List<Employee> Active()
        {
            throw new NotImplementedException();
        }

        public List<Employee> InActive()
        {
            throw new NotImplementedException();
        }

        Employee IEmployeeService.GetByIDAsync(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetBySearchTerm(string keyword)
        {
            throw new NotImplementedException();
        }

        public int GetActiveCount()
        {
            throw new NotImplementedException();
        }

        public int GetInActiveCount()
        {
            throw new NotImplementedException();
        }

        public int GetAllCount()
        {
            throw new NotImplementedException();
        }

        public Task<(bool status, string message)> Update(Employee model)
        {
            throw new NotImplementedException();
        }

        public async Task<(Employee data, bool status, string message)> AddAsync(Employee model)
        {
            Employee employee = new Employee();
            bool stat = false;
            string msg = "";
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                var content = await GlobalService.client.PostAsync($"{BaseAddress.TestBaseAddress}/api/Employees/Add", byteContent).ConfigureAwait(false);
                var raw = await content.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(raw);
                stat = true;
                msg = "Record saved successfully";
            }
            catch (Exception ex)
            {
                stat = false;
                msg = $"Failed to save record, error message: {ex.Message}";
            }
            return (employee, stat, msg);

        }
         
        
        public async Task<(Employee data, bool status, string message)> UpdateAsync(Employee model)
        {
            Employee employee = new Employee();
            bool stat = false;
            string msg = "";
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                var content = await GlobalService.client.PutAsync($"{BaseAddress.TestBaseAddress}/api/Employees/Update", byteContent).ConfigureAwait(false);
                var raw = await content.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(raw);
                stat = true;
                msg = "Record updated successfully";
            }
            catch (Exception ex)
            {
                stat = false;
                msg = $"Failed to update record, error message: {ex.Message}";
            }
            return (employee, stat, msg);

        }

        public Task<(bool status, string message)> AddBulkAsync(List<EmployeeViewModel> employees, List<DependantViewModel> dependants)
        {
            throw new NotImplementedException();
        }
    }
}
