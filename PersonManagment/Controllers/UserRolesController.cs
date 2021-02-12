using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonManagment.Data.Models;

namespace PersonManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        public UserRolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
       
        [Route("Index")]
        public IEnumerable Index() => _roleManager.Roles.ToList();
        
        //public IActionResult Create()
        //{ 
        //    return View(); 
        //}
       

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> Create([FromBody] IdentityRole role)
        {
            if (role != null && !string.IsNullOrEmpty(role.Name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role.Name));
                if (result.Succeeded)
                {
                    return Ok(role.Name);
                }
                else
                {
                    return BadRequest(string.Join("; ", result.Errors.Select(x => x.Description)));
                }
            }
            else
            {
                return BadRequest("Неверные параметры");
            }
        }

        [HttpPost]
        [Route("DeleteRole")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
       
        [Route("UserList")]
        public IEnumerable UserList() => _userManager.Users.ToList();
        
        //[Route("Edit")]
        //public async Task<IActionResult> Edit(string userId)
        //{
        //    // получаем пользователя
        //    ApplicationUser user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        // получем список ролей пользователя
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var allRoles = _roleManager.Roles.ToList();
        //        ChangeRoleViewModel model = new ChangeRoleViewModel
        //        {
        //            UserId = user.Id,
        //            UserEmail = user.Email,
        //            UserRoles = userRoles,
        //            AllRoles = allRoles
        //        };
        //        return Ok(model);
        //    }

        //    return NotFound();
        //}

        /// <summary>
        /// Добавление роли пользователю
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(string userId, [FromBody] UserRoles roles)
        {
            // получаем пользователя
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);

                // получаем список ролей, которые нужно добавить
                var addedRoles = roles.roles.Except(userRoles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }

        /// <summary>
        /// Удаление роли пользователя
        /// </summary>
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> Remove(string userId, [FromBody] UserRoles roles)
        {
            // получаем пользователя
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);

                // получаем роли, которые нужно удалить
                var removedRoles = userRoles.Intersect(roles.roles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}
