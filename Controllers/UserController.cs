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
using NetProject.Models;
using NetProject.Session;
using NetProject.Util;

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
                new Claim("PHONE_NUMBER" , user.NumberPhone ?? ""),
                new Claim("GENDER" , user.Gender),
                new Claim("LEVEL" , user.Level ?? "no level"),
                new Claim(ClaimTypes.Role , user.Level.ToUpper())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties {
                
                
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);
            SessionFunction.SetUser(HttpContext.Session ,user);
            if (entryParams.GetUrl == null) return Redirect("/");
            return Redirect(entryParams.GetUrl);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
        [HttpPost]
        public IActionResult SignUp(SignUpEntryParams entryParams)
        {
            var splitURL = entryParams.GetURL != null ? entryParams.GetURL.Split("/") : "".Split("/");
            try
            {
                string mesSignUp_err = null;

                var rs = _userData.GetUserByEmail(entryParams.YourEmail);
                if (CheckValidate.checkIsEmpty(entryParams.YourName) || CheckValidate.checkIsEmpty(entryParams.YourEmail) || CheckValidate.checkIsEmpty(entryParams.YourPass)
                        || CheckValidate.checkIsEmpty(entryParams.YourNumPhone) || !CheckValidate.checkIsEmail(entryParams.YourEmail) || !CheckValidate.checkIsPass(entryParams.YourPass) || !CheckValidate.checkIsNumPhone(entryParams.YourNumPhone)
                )
                {
                    mesSignUp_err = "Dữ liệu nhập vào không hợp lệ";
                    SessionFunction.SetString(HttpContext.Session , "error_signUp", mesSignUp_err);
                    //thay vì response.sendRedirect(splitURL[splitURL.length - 1]);
                    // thì làm
                    return Redirect(splitURL[splitURL.Length - 1]);
                }
                else
                {
                    //rs.getRow khác 0 tương tự rs != null
                    //if (rs.getRow() != 0)
                    if (rs != null)
                    {
                        mesSignUp_err = "Email đã tồn tại";
                        //session.setAttribute("error_signUp", mesSignUp_err);
                        SessionFunction.SetString(HttpContext.Session, "error_signUp", mesSignUp_err);
                        //response.sendRedirect(splitURL[splitURL.length - 1]);
                        return Redirect(splitURL[splitURL.Length - 1]);
                    }
                    else
                    {
                        //AddFunction.addUser(yourName, yourEmail, Util.encrypt(yourPass), "user.jpg", yourNumPhone, yourGender
                        //    , "user", getYourQuestion, getYourAnswer, 1);
                        var newUser = new User
                        {
                            NameUser = entryParams.YourName,
                            Email = entryParams.YourEmail,
                            Password = entryParams.YourPass,
                            ImgeUser = "user.jpg",
                            NumberPhone = entryParams.YourNumPhone,
                            Gender = entryParams.YourGender,
                            Question = entryParams.YourQuestion,
                            Answer = entryParams.YourAnswer,
                            Active = 1,
                            Level = "no level"
                        };
                        _userData.AddUser(newUser);
                        var rs_2 = _userData.GetUser(entryParams.YourEmail, entryParams.YourPass);
                        //session.setAttribute("Auth", user);
                        SessionFunction.SetUser(HttpContext.Session, rs_2);
                        var claims = new List<Claim>()
                        {
                            new Claim("ID" , rs_2.Id + ""),
                            new Claim("NAME", rs_2.NameUser),
                            new Claim("EMAIL" , rs_2.Email),
                            new Claim("IMG_USER" , rs_2.ImgeUser),
                            new Claim("PHONE_NUMBER" , rs_2.NumberPhone),
                            new Claim("GENDER" , rs_2.Gender),
                            new Claim("LEVEL" , rs_2.Level ?? "no level"),
                            new Claim(ClaimTypes.Role , rs_2.Level.ToUpper())

                        };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {


                        };
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), authProperties);
                        //response.sendRedirect(splitURL[splitURL.length - 1]);
                        return Redirect(splitURL[splitURL.Length - 1]);
                    }
                }
            }
            catch (Exception E)
            {
                return Redirect("/");
            }
        }
    }
}