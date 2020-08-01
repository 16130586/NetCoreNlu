using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NetProject.DbAccessor;
using NetProject.EntryParams;
using NetProject.Session;

namespace NetProject.Controllers
{
    public class UserController : Controller
    {
        private readonly UserData _userData;
        public UserController(UserData userData) {
            _userData = userData;
        }
        public  IActionResult Login(LoginEntryParams entryParams)
        {
            string email = entryParams.Email;
            string pwd = entryParams.Pwd;
            var user = _userData.GetUser(email, pwd);
            if(user == null)
            {
                var msg_error = "Email đăng nhập hoặc mật khẩu không hợp lệ";
                SessionFunction.SetString(HttpContext.Session, "error_login", msg_error);
                return Redirect(entryParams.GetUrl);

            }

            var claims = new List<Claim>()
            {
                new Claim("ID" , user.Id + ""),
                new Claim("NAME", user.NameUser),
                new Claim("EMAIL" , user.Email),
                new Claim("IMG_USER" , user.ImgeUser == null ? "user.jpg" : user.ImgeUser),
                new Claim("PHONE_NUMBER" , user.NumberPhone),
                new Claim("GENDER" , user.Gender),
                new Claim("LEVEL" , user.Level)

            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties {
                
                
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return Redirect(entryParams.GetUrl);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}