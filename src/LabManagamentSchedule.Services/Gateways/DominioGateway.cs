using LabManagamentSchedule.Core.AppSettings;
using LabManagamentSchedule.Core.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LabManagamentSchedule.Services.Gateways
{
    public class DominioGateway : IDominioGateway
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationBasicSettings authenticationBasic;
        public DominioGateway(HttpClient httpClient, AuthenticationBasicSettings authenticationBasic)
        {
            this.httpClient = httpClient;
            this.authenticationBasic = authenticationBasic;
        }

        public async Task<Dominio> GetDomain(string dominio)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), Convert.ToBase64String(Encoding.ASCII.GetBytes($"{authenticationBasic.UserName}:{authenticationBasic.Password}")));

            var response = await httpClient.GetAsync(httpClient.BaseAddress + dominio);

            if (!response.IsSuccessStatusCode)
                return null;

            var domain = JsonConvert.DeserializeObject<Dominio>(response.Content.ReadAsStringAsync().Result);

            return domain;
        }

        public async Task<IList<Dominio>> GetDomains()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationSchemes.Basic.ToString(), Convert.ToBase64String(Encoding.ASCII.GetBytes($"{authenticationBasic.UserName}:{authenticationBasic.Password}")));

            var response = await httpClient.GetAsync(httpClient.BaseAddress + "GetAll");

            if (!response.IsSuccessStatusCode)
                return null;

            var domains = JsonConvert.DeserializeObject<IList<Dominio>>(response.Content.ReadAsStringAsync().Result);

            return domains;
        }
    }
}
