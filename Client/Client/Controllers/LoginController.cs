using Client.Models;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()//конструктор
        {
            accountService = new AccountService();
        }

        private AccountService accountService;//сервис для вызова функций сервера
        public ActionResult Index()
        
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            //return View("~/Views/Account/AccountList.cshtml");

            return Redirect("/Group/Index");
        }
        public ActionResult OnLogin(LoginModel model)//авторизация пользователя
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Login/LoginView.cshtml", model);
            }

            var accounts = accountService.GetAllAccounts().Where(x => x.Login == model.Login && x.Password == model.Password).ToList();
            if (!accounts.Any())
            {
                model.BadData = true;
                return View("~/Views/Login/LoginView.cshtml", model);
            }

            Session["userRole"] = accounts.First().AccountType.Type;
            Guid test = accounts.First().User.Id;
            Session["userId"] = accounts.First().User.Id;
            Session["userLogin"] = accounts.First().Login;
            Session["userFio"] = accounts.First().User.Fio;
            return RedirectToAction("Index");
        }
        public ActionResult AuthLogOut()//выход пользователя из системы
        {
            Session["userLogin"] = null;
            Session["userRole"] = null;
            Session["userId"] = null;

            return RedirectToAction("Index");
        }
    }
}