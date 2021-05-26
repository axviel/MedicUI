using MedicDev.Models.Authentication;
using MedicDev.Models.UserProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MedicDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            //string userId = User.Claims.First(c => c.Type == "UserID").Value;
            //var user = await _userManager.FindByIdAsync(userId);

            string username = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            var user = await _userManager.FindByNameAsync(username);
            return new
            {
                user.Email,
                user.UserName
            };
        }

        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            // Check if passwords match
            if (model.NewPassword != model.ConfirmNewPassword)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Passwords do not match." });
            
            // Get user
            string username = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(username);

            // Update password
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            return Ok(result);
        }

        // is this necesary? why not save the getprofiledata in user service ?
        [HttpGet]
        [Route("UpdateAccount")]
        public async Task<Object> UpdateAccount()
        {
            string username = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            var user = await _userManager.FindByNameAsync(username);
            return new
            {
                user.Email
            };
        }

        [HttpPost]
        [Route("UpdateAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountModel model)
        {
            // Get user
            string username = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(username);

            // Update user
            //user.UserName = model.Username;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);

            return Ok(result);
        }

    }
}
