using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BTH1.Controllers
{
    
    public class AccessController : Controller
    {
        public static string check;
        QuanLyGiaoTrinhContext db = new QuanLyGiaoTrinhContext();

       

        [HttpGet]
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("UserName" ) == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [HttpPost]
        public IActionResult Login(ThuThu user)
        { 
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var obj = db.ThuThus.Where(x => x.Username == user.Username && x.MatKhau == user.MatKhau).FirstOrDefault();
                if(obj != null)
                {
                    if(obj.Quyen.ToString() == "0")
                    {
                        HttpContext.Session.SetString("UserName", obj.Username.ToString());
                        HttpContext.Session.SetString("Quyen", obj.Quyen.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        HttpContext.Session.SetString("UserName", obj.Username.ToString());
                        HttpContext.Session.SetString("Quyen", obj.Quyen.ToString());
                        return RedirectToAction("HomeAdmin", "Admin");
                    }

                }
                else
                {
                    return RedirectToAction("Login", "Access");
                }
        
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Access");
        }
    }
}
