using System;
using System.Collections.Generic;
using Client.Models;
using Client.Models.ServerModels;
using Client.Services;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class PersonalAccountController : Controller
    {
        private AccountService _accountService;//сервис для вызова функций сервера
        private AccountTypeService _accountTypeService;
        private ParentService _parentService;
        private TeacherService _teacherService;
        private UserService _userService;
        private ChildService _childService;
        public PersonalAccountController()//конструктор
        {
            _parentService = new ParentService();
            _teacherService = new TeacherService();
            _accountService = new AccountService();
            _accountTypeService = new AccountTypeService();
            _userService = new UserService();
            _childService = new ChildService();
        }

        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }
            AccountModel accountModel = new AccountModel();
            if (Session["userRole"].ToString() == "Parent")
            {
                Parent parent = _parentService.GetParent(Guid.Parse(Session["userId"].ToString()));

                accountModel = new AccountModel
                {
                    UserId = parent.Id,
                    ParentId = parent.Id,
                    AccountId = parent.User.AccountId,
                    AccountTypeId = parent.User.Account.AccountTypeId,
                    Login = parent.User.Account.Login,
                    AccountType = parent.User.Account.AccountType.Type,
                    Fio = parent.User.Fio,
                    Email = parent.User.Email,
                    Birthsday = parent.User.Birthsday,
                    BirthsdayStr = parent.User.Birthsday != null ? parent.User.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a",
                    LargeFamilieStr = parent.LargeFamilie ? "Yes" : "No",
                    LargeFamilie = parent.LargeFamilie,
                    Phone = parent.User.Phone,
                    Password = parent.User.Account.Password
                };              
            }

            if (Session["userRole"].ToString() == "Teacher")
            {
                Teacher teacher = _teacherService.GetTeacher(Guid.Parse(Session["userId"].ToString()));

                accountModel = new AccountModel
                {
                    UserId = teacher.Id,
                    ParentId = teacher.Id,
                    AccountId = teacher.User.AccountId,
                    AccountTypeId = teacher.User.Account.AccountTypeId,
                    Login = teacher.User.Account.Login,
                    AccountType = teacher.User.Account.AccountType.Type,
                    Fio = teacher.User.Fio,
                    Email = teacher.User.Email,
                    Birthsday = teacher.User.Birthsday,
                    BirthsdayStr = teacher.User.Birthsday != null ? teacher.User.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a",
                    Phone = teacher.User.Phone,
                    Password = teacher.User.Account.Password,
                    Portfolio = teacher.Portfolio,
                    Experience = teacher.Experience
                };
            }

            if (Session["userRole"].ToString() == "Admin")
            {
                User user = _userService.GetUser(Guid.Parse(Session["userId"].ToString()));

                accountModel = new AccountModel
                {
                    UserId = user.Id,
                    ParentId = user.Id,
                    AccountId = user.AccountId,
                    AccountTypeId = user.Account.AccountTypeId,
                    Login = user.Account.Login,
                    AccountType = user.Account.AccountType.Type,
                    Fio = user.Fio,
                    Email = user.Email,
                    Birthsday = user.Birthsday,
                    BirthsdayStr = user.Birthsday != null ? user.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a",
                    Phone = user.Phone,
                    Password = user.Account.Password,
                };
            }
            return View("ShowPersonalAccountView", accountModel);
        }

        public ActionResult EditPersonalAccount(AccountModel accountModel)//вызов страницы изменения информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            /*            IEnumerable<SelectListItem> accountTypes = from item in _accountTypeService.GetAllAccountTypes()
                                                                   select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };
                        accountModel2.AccountTypes = accountTypes;*/

            return View("EditPersonalAccountView", accountModel);
        }

        [HttpPost]
        public ActionResult UpdatePersonalAccount(AccountModel accountModel)//изменение информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            /*            if (!ModelState.IsValid)
                        {
                            return View("~/Views/Users/UpdateUserView.cshtml", model);
                        }*/
            try
            {
                _accountService.UpdateAccount(new Account()
                {
                    Id = accountModel.AccountId,
                    Login = accountModel.Login,
                    Password = accountModel.Password,
                    AccountTypeId = accountModel.AccountTypeId
                });

                _userService.UpdateUser(new User()
                {
                    Id = accountModel.UserId,
                    Fio = accountModel.Fio,
                    Phone = accountModel.Phone,
                    Email = accountModel.Email,
                    Birthsday = accountModel.Birthsday,
                    AccountId = accountModel.AccountId

                });

                if (accountModel.AccountType == "Parent")
                {
                    _parentService.UpdateParent(new Parent()
                    {
                        Id = accountModel.ParentId,
                        LargeFamilie = accountModel.LargeFamilie,

                    });
                }

                if (accountModel.AccountType == "Teacher")
                {
                    _teacherService.UpdateTeacher(new Teacher()
                    {
                        Id = accountModel.TeacherId,
                        Portfolio = accountModel.Portfolio,
                        Experience = accountModel.Experience

                    });
                }
            }
            catch (Exception)
            {
                return View("EditPersonalAccountView", accountModel);
            }

            return RedirectToAction("Index");
        }
    }
}