using AareonTechnicalTest.Controllers.Utils;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AareonTechnicalTest.Controllers
{
    
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly SignInManager<Person> _signInManager;
        public IdentityController(ApplicationContext context, SignInManager<Person> signInManager) : base(context)
        {
            _signInManager = signInManager;
        }

        // POST api/<IdentityController>
        [HttpPost]
        [Route("SignIn")]
        public async Task<IdentityResult> SignIn(string UserName)
        {
            Person dbUser = this.Context.Users.Where(x => x.UserName.ToLower() == UserName.ToLower()).FirstOrDefault();
            if(dbUser != null)
            {
                await _signInManager.SignInAsync(dbUser, false);
                return IdentityResult.Success;

            }
            else
            {
                return IdentityResult.Failed();
            }
           
        }

        [HttpPost]
        [Route("SignOut")]
        public async Task<IdentityResult> SignOut(string UserName)
        {
            Person dbUser = this.Context.Users.Where(x => x.UserName.ToLower() == UserName.ToLower()).FirstOrDefault();
            if (dbUser != null)
            {
                await _signInManager.SignOutAsync();
                return IdentityResult.Success;

            }
            else
            {
                return IdentityResult.Failed();
            }

        }
    }
}
