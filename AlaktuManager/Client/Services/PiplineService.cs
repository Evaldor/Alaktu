using AlaktuManager.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AlaktuManager.Client.Services
{
    public class PiplineService : IPiplineService
    {
        private readonly HttpClient httpClient;

        public PiplineService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Pipline>> GetPiplines()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Pipline>>("api/Piplines");
        }

        public Task<Pipline> AddPipline(Pipline pipline)
        {
            throw new NotImplementedException();
        }

        public Task DeletePipline(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Pipline> GetPipline(int Id)
        {
            throw new NotImplementedException();
        }
        public Task<Pipline> UpdatePipline(Pipline pipline)
        {
            throw new NotImplementedException();
        }
    }
}