using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ZoomIn.Models;

namespace ZoomIn.Controllers
{
    public class LoginRegisterController : Controller
    {

        private readonly ModelContext _context;

        public LoginRegisterController(ModelContext context)
        {
            _context = context;
        }
        // GET: Register
        public IActionResult Register()
        {
            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId");

            ViewData["CreditcName"] = new SelectList(_context.Creditcards, "CardId", "Cardnumber");
            return View();
        }

        // POST: Register
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,Fname,Lname,Country,City,Gender,Birthday,UserImage,Registerdate,UserUsername,UserPassword,ConfirmPassword,UserEmail,RoleId,CreditcId,ImageFile")] Enduser enduser)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    enduser.RoleId = _context.Userroles.Where(c => c.Rolename == "Photographer").FirstOrDefault().RoleId;

                    await enduser.ImageFile.CopyToAsync(memoryStream);
                    enduser.UserImage = memoryStream.ToArray();
                    enduser.ImageExtension = Path.GetExtension(enduser.ImageFile.FileName).Split('.')[1];

                    enduser.Registerdate = DateTime.Now;

                    _context.Add(enduser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Login", "LoginRegister");
                }
            }
            ViewData["CreditcId"] = new SelectList(_context.Creditcards, "CardId", "CardId", enduser.CreditcId);
            ViewData["CreditcName"] = new SelectList(_context.Creditcards, "CardId", "Cardnumber", enduser.CreditcId);
            return View(enduser);
        }

        //Validate Username Uniqueness
        /*[AcceptVerbs("Get","Post")]
        public async Task<IActionResult> IsUserNameEsist(string username)
        {
            return Json(_context.Endusers.Any(x => x.UserUsername == username));
        }

        //Validate Email Uniqueness
        [AcceptVerbs("Get", "Post")]
        public JsonResult IsEmailEsist(string email)
        {
            return Json(_context.Endusers.Any(x => x.UserEmail == email));
        }*/

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var authenticatedUser = _context.Endusers
                .Where(x => x.UserUsername == username && x.UserPassword == password)
                /*.Include(e => e.Creditc)
                .Include(e => e.Role)*/
                .SingleOrDefault();

            if (authenticatedUser != null)
            {
                HttpContext.Session.SetString("UserObject",JsonConvert.SerializeObject(authenticatedUser));
                HttpContext.Session.SetString("UserID", authenticatedUser.UserId.ToString());
                HttpContext.Session.SetString("UserName", authenticatedUser.UserUsername);
                HttpContext.Session.SetString("UserImage", Convert.ToBase64String(authenticatedUser.UserImage));
                HttpContext.Session.SetString("UserImageExt", authenticatedUser.ImageExtension);
                HttpContext.Session.SetString("UserRoleId", authenticatedUser.RoleId.ToString());

                _context.Add(new Loginloger()
                {
                    LoginCounter = _context.Accesslogers.Count() + 1,
                    LoginDate = DateTime.Now,
                    LoginUserId = authenticatedUser.UserId
                });
                _context.SaveChangesAsync();

                switch (authenticatedUser.RoleId)
                {
                    case 1:
                        return RedirectToAction("Index", "Dashboard");
                    case 2:
                        return RedirectToAction("Index", "Balances");
                    case 3:
                        return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return View();
        }
    }
}
