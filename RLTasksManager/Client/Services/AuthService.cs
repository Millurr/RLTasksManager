using RLTasksManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace RLTasksManager.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient http;

        public AuthService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin req)
        {
            var res = await http.PostAsJsonAsync("api/auth/login", req);
            return await res.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister req)
        {
            var res = await http.PostAsJsonAsync("api/auth/register", req);
            return await res.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
