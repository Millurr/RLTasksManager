using RLTasksManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLTasksManager.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(UserRegister req);
        Task<ServiceResponse<string>> Login(UserLogin req);
    }
}
