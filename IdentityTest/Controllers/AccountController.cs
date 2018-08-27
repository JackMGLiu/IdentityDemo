using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTest.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Login(string returnUrl)
        {
            //if (!string.IsNullOrEmpty(returnUrl))
            //{
            //    return Redirect(returnUrl);
            //}
            return View();
        }

        public async Task<IActionResult> Denied()
        {
            return View();
        }

        #region 模拟登录

        public async Task<IActionResult> MakeLogin()
        {
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name,"liujian"),
                new Claim(ClaimTypes.Role,"admin")
            };

            //必须要加CookieAuthenticationDefaults.AuthenticationScheme，不然无法解析
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Ok();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }


        #endregion
    }
}