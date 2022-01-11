/*using Client.Models;
using Client.ServerModels.Enums;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private UserService userService;

        public HomeController()
        {
            userService = new UserService();
        }
        public ActionResult Main()
        {

            return View("~/Views/Home/Main.cshtml");
        }
        public ActionResult Index()
        {

            return View("~/Views/Login/LoginView.cshtml");
        }
        public ActionResult OnLogin(LoginModel model)//авторизация пользователя
        {
            if (!ModelState.IsValid)
            {
                return View("LoginView", model);
            }

            var users = userService.GetUsers().Where(x => x.Login == model.Login && x.Password == model.Password).ToList();
            if (!users.Any())
            {
                return View("LoginView", model);
            }

            Session["userRole"] = (UserRole)Enum.Parse(typeof(UserRole), users.First().Role);
            Session["userId"] = users.First().Id;
            Session["userLogin"] = users.First().Login;
            int test = (int)Session["userRole"];
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
*/