
using Skylight.Data.ViewModel;
using Skylight.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Camguard.Business.IContract
{
    public interface IEmployeeService
    {
        Task<(HashSet<Employee> data, bool status, string message)> GetAllAsync(int limit, int offset);

        List<Employee> Active();
        List<Employee> InActive();
        Employee GetByIDAsync(int ID);
        List<Employee> GetBySearchTerm(string keyword);

        int GetActiveCount();
        int GetInActiveCount();
        int GetAllCount();
        Task<(bool status, string message)> Update(Employee model);
        Task<(Employee data, bool status, string message)> AddAsync(Employee model);
        Task<(bool status, string message)> AddBulkAsync(List<EmployeeViewModel> employees, List<DependantViewModel> dependants);
        Task<(Employee data, bool status, string message)> UpdateAsync(Employee model);

    }
}
