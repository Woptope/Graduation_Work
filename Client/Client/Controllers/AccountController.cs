using Client.Models;
using Client.Models.ServerModels;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private AccountService _accountService;//сервис для вызова функций сервера
        private AccountTypeService _accountTypeService;
        private ParentService _parentService;
        private TeacherService _teacherService;
        private UserService _userService;
        private ChildService _childService;
        public AccountController()//конструктор
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

            List<Parent> parents = _parentService.GetAllParents();
            List<Teacher> teachers = _teacherService.GetAllTeachers();
            IEnumerable<SelectListItem> accountTypes = from item in _accountTypeService.GetAllAccountTypes()
                                                       select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };

            IEnumerable<AccountModel> parentList = from item in parents
                                                   select new AccountModel
                                                   {
                                                       UserId = item.User.Id,
                                                       ParentId = item.Id,
                                                       AccountId = item.User.AccountId,
                                                       AccountTypeId = item.User.Account.AccountTypeId,
                                                       Login = item.User.Account.Login,
                                                       AccountType = item.User.Account.AccountType.Type,
                                                       Fio = item.User.Fio,
                                                       Email = item.User.Email,
                                                       Birthsday = item.User.Birthsday,
                                                       BirthsdayStr = item.User.Birthsday != null ? item.User.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a",
                                                       LargeFamilieStr = item.LargeFamilie ? "Yes" : "No",
                                                       LargeFamilie = item.LargeFamilie,
                                                       Phone = item.User.Phone,
                                                       Password = item.User.Account.Password
                                                   };

            IEnumerable<AccountModel> teacherList = from item in teachers
                                                    select new AccountModel
                                                    {
                                                        TeacherId = item.Id,
                                                        UserId = item.User.Id,
                                                        AccountId = item.User.AccountId,
                                                        AccountTypeId = item.User.Account.AccountTypeId,
                                                        Login = item.User.Account.Login,
                                                        AccountType = item.User.Account.AccountType.Type,
                                                        Fio = item.User.Fio,
                                                        Email = item.User.Email,
                                                        Birthsday = item.User.Birthsday,
                                                        BirthsdayStr = item.User.Birthsday != null ? item.User.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a",
                                                        Phone = item.User.Phone,
                                                        Password = item.User.Account.Password,
                                                        Portfolio = item.Portfolio,
                                                        Experience = item.Experience
                                                    };

            IEnumerable<AccountModel> accountModels = parentList.Concat(teacherList);

            return View("ListAccountView", accountModels);
        }

        public ActionResult ShowAccount(AccountModel accountModel)//просмотр информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            return View("ShowAccountView", accountModel);
        }

        public ActionResult EditAccount(AccountModel accountModel)//вызов страницы изменения информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            /*            IEnumerable<SelectListItem> accountTypes = from item in _accountTypeService.GetAllAccountTypes()
                                                                   select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };
                        accountModel2.AccountTypes = accountTypes;*/

            return View("EditAccountView", accountModel);
        }

        [HttpPost]
        public ActionResult UpdateAccount(AccountModel accountModel)//изменение информации о пользователе
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
                return View("EditAccountView", accountModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteAccount(AccountModel accountModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            if (accountModel.AccountType == "Parent")
            {
                List<Child> children = _childService.GetAllChildren();
                foreach (var child in children)
                {
                    if (child.FatherId == accountModel.ParentId && child.MotherId == null || child.MotherId == accountModel.ParentId && child.FatherId == null)
                    {
                        _childService.DeleteChild(child.Id);
                    }
                    else
                    {
                        if (accountModel.ParentId == child.MotherId)
                        {
                            child.MotherId = null;
                            _childService.UpdateChild(child);
                        }

                        if (accountModel.ParentId == child.FatherId)
                        {
                            child.FatherId = null;
                            _childService.UpdateChild(child);
                        }
                    }
                }

                _parentService.DeleteParent(accountModel.ParentId);
                _userService.DeleteUser(accountModel.ParentId);
                _accountService.DeleteAccount(accountModel.AccountId);
            }

            if (accountModel.AccountType == "Teacher")
            {
                _teacherService.DeleteTeacher(accountModel.TeacherId);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddAccount()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            IEnumerable<SelectListItem> accountTypes = from item in _accountTypeService.GetAllAccountTypes()
                                                       select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };
            AccountModel accountModel = new AccountModel
            {
                AccountTypes = accountTypes
            };

            return View("AddAccountView", accountModel);
        }

        [HttpPost]
        public ActionResult AddAccount(AccountModel accountModel)//добавление пользователя
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            /*            if (!ModelState.IsValid)
                        {
                            return View("~/Views/Users/AddUserView.cshtml", model);
                        }*/
            try
            {
                /*                _accountService.AddAccount(new Account()
                                {
                                    Login = accountModel2.Login,
                                    Password = accountModel2.Password,
                                    AccountTypeId = accountModel2.AccountTypeId,
                                });*/
                /*
                                _userService.AddUser(new User()
                                {
                                    Fio = accountModel2.Fio,
                                    Phone = accountModel2.Phone,
                                    Email = accountModel2.Email,
                                    Birthsday = accountModel2.Birthsday,
                                    AccountId = _accountService.AddAccount(new Account()
                                    {
                                        Login = accountModel2.Login,
                                        Password = accountModel2.Password,
                                        AccountTypeId = accountModel2.AccountTypeId,
                                    })
                            });*/
                string role = _accountTypeService.GetAccountType(accountModel.AccountTypeId).Type;

                Guid acc = _accountService.AddAccount(new Account()
                {
                    Login = accountModel.Login,
                    Password = accountModel.Password,
                    AccountTypeId = accountModel.AccountTypeId,
                });

                Guid us = _userService.AddUser(new User()
                {
                    Fio = accountModel.Fio,
                    Phone = accountModel.Phone,
                    Email = accountModel.Email,
                    Birthsday = accountModel.Birthsday,
                    AccountId = acc
                });

                if (role == "Parent")
                {
                    _parentService.AddParent(new Parent()
                    {
                        LargeFamilie = accountModel.LargeFamilie,
                        Id = us
                    });
                }

                if (role == "Teacher")
                {
                    _teacherService.AddTeacher(new Teacher()
                    {
                        Portfolio = accountModel.Portfolio,
                        Experience = accountModel.Experience,
                        Id = us
                    });
                }
            }
            catch (Exception)
            {
                IEnumerable<SelectListItem> accountTypes = from item in _accountTypeService.GetAllAccountTypes()
                                                           select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };

                accountModel.AccountTypes = accountTypes;

                return View("AddAccountView", accountModel);
            }

            return RedirectToAction("Index");
        }

    }
}