using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RLTasksManager.Server.Data;
using RLTasksManager.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLTasksManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo authRepo;

        public AuthController(IAuthRepo authRepo)
        {
            this.authRepo = authRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister req)
        {
            var res = await authRepo.Register(
                new User
                {
                    Email = req.Email
                }, req.Password);

            if (!res.Success)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin req)
        {
            var res = await authRepo.Login(req.Email, req.Password);

            if (!res.Success)
                return BadRequest(res);

            return Ok(res);
        }
    }
}
