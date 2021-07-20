using Skylight.Data.Models;
using Skylight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skylight.DataAccess.IContract
{
    public interface IGlobalService
    {
        Task<(HashSet<State> data, bool status, string message)> LoadStates();
        Task<(HashSet<Provider> data, bool status, string message)> LoadProviders();
        Task<(HashSet<Company> data, bool status, string message)> LoadCompanies();
        Task<(HashSet<Plan> data, bool status, string message)> LoadPolicy();
        Task<(HashSet<CompanyPolicy> data, bool status, string message)> LoadCompanyPolicy();
    }
}
