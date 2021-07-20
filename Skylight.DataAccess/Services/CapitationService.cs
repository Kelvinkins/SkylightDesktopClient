using Camguard.Business.IContract;
using Skylight.Data;
using Skylight.Data.Models;
using Skylight.Models;
using Skylight.Models.Enums;
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
    public class CapitationService : ICapitationService
    {

        private static readonly HttpClient client = new HttpClient();
        private readonly IEmailService emailService;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        public CapitationService()
        {
        }

    


        public async Task<(bool status, string message)> Process(CapitationMaster model)
        {
            string message = "";

            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //var stringTask = client.PostAsync("https://api.github.com/orgs/dotnet/repos",JsonSerializer.Serialize(model));
                var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
                var repositories = await JsonSerializer.DeserializeAsync<List<ServiceResponse>>(await streamTask);

            }
            catch (Exception ex)
            {

            }
            return (true, message);

        }


        public async Task<(bool status, ServiceResponse message)> PublishCapitation()
        {
            string log = "";
            ServiceResponse response = new ServiceResponse();

            return (true, response);

        }

    }
}       
