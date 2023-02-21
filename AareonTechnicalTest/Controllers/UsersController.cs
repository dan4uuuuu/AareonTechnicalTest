using AareonTechnicalTest.Controllers.Utils;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.DTOs.Users;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AareonTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UsersController(ApplicationContext context, UserManager<Person> userManager,
        SignInManager<Person> signInManager, RoleManager<IdentityRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // POST api/<UsersController>
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IdentityResult> CreateUser(UserDTO user)
        {
            Person person = new Person()
            {
                AccessFailedCount = 0,
                Email = user.Email,
                FirstName = user.FirtsName,
                Surname = user.LastName,
                UserName = user.UserName,
                IsAdmin = user.isAdmin
            };

            var result = await _userManager.CreateAsync(person);

            if (result.Succeeded)
            {
                var role = user.isAdmin == true ? _roleManager.FindByNameAsync("Admin").Result : _roleManager.FindByNameAsync("User").Result;

                if (role != null)
                {
                    IdentityResult roleresult = await _userManager.AddToRoleAsync(person, role.Name);
                }
            }
            return result;
        }

        // DELETE api/<UsersController>/5
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IdentityResult> DeleteUser(string username)
        {
            Person dbUser = this.Context.Users.Where(x => x.UserName.ToLower() == username.ToLower()).FirstOrDefault();
            if(dbUser != null)
            {
                await _userManager.DeleteAsync(dbUser);
                return IdentityResult.Success;
            }
            return IdentityResult.Failed();

        }
    }
}
