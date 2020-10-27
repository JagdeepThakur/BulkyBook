using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BulkyBook.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = StaticData.Role_Admin + "," + StaticData.Role_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            applicationDbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll() 
        {
            var userList = applicationDbContext.ApplicationUsers.Include(a => a.Company).ToList();
            var userRoles = applicationDbContext.UserRoles.ToList();
            var roles = applicationDbContext.Roles.ToList();
            foreach (var user in userList)
            {
                var roleID = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleID).Name;
                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = " "
                    };
                }
            }
            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var userFromDb = applicationDbContext.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (userFromDb == null) 
            {
                return Json( new {success = false, message = "Error while Locking/Unlocking"});
            }
            if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked, unlock user
                userFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                userFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }
            applicationDbContext.SaveChanges();
            return Json(new { success = true, message = "Operation Successful." });
        }
        #endregion
    }
}
