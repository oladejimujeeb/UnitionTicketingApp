using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using UnitionTicketingApp.DTO;

namespace UnitionTicketingApp.Implementation.Service
{
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole>roleManager,
            SignInManager<IdentityUser> signInManager) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<BaseResponse> CreateUser(CreateUserModel model)
        {
            var response = new BaseResponse();
            if(model == null || !string.IsNullOrWhiteSpace(model.Role))
            {
                response.Message = "Invalid request model";
            }
            var role = await _roleManager.FindByNameAsync(model.Role);
            if (role is null)
            {
                response.Message = "Role not exist";
            }
            var user = new IdentityUser 
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
                await _signInManager.SignInAsync(user, isPersistent: false);
                response.Status = true;
                response.Message = "User Created Successfully";
                return response;
            }

            var error = string.Join(", ", result.Errors.Select(x=>x.Description));
            response.Message = error;
            return response;
        }

        public async Task<BaseResponse>SignIn(LoginViewModel model)
        {
            var response = new BaseResponse();
            
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                response.Status = true;
                response.Message = "Login Successful";
                return response ;
            }
            response.Message = "Login Failed";
            return response;
        }
        public BaseResponse<List<RoleViewModel>>AllRoles()
        {
            var response = new BaseResponse<List<RoleViewModel>>();
            var roles = _roleManager.Roles.ToList().Select(x=> new RoleViewModel
            {
                Id = x.Id,
                RoleName = x.Name,
            }).ToList();                
            response.Status = true;
            response.Data = roles;
            return response;
        }
    }
}
