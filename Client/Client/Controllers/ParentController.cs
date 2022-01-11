using Client.Models;
using Client.Models.ServerModels;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ParentController : Controller
    {
        private AccountService _accountService;//сервис для вызова функций сервера
        private ParentService _parentService;
        private UserService _userService;
        private ChildService _childService;
        private AccountTypeService _accountTypeService;
        public ParentController()//конструктор
        {
            _parentService = new ParentService();
            _accountService = new AccountService();
            _userService = new UserService();
            _childService = new ChildService();
            _accountTypeService = new AccountTypeService();
        }

        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<Parent> parents = _parentService.GetAllParents();

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
                                                       LargeFamilieStr = item.LargeFamilie ? "Да" : "Нет",
                                                       LargeFamilie = item.LargeFamilie,
                                                       Phone = item.User.Phone,
                                                       Password = item.User.Account.Password
                                                   };



            return View("ListParentsView", parentList);
        }

        public ActionResult ShowAccount(AccountModel accountModel)//просмотр информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            return View("ShowParentView", accountModel);
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

            return View("EditParentView", accountModel);
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

                    _parentService.UpdateParent(new Parent()
                    {
                        Id = accountModel.ParentId,
                        LargeFamilie = accountModel.LargeFamilie,

                    });

            }
            catch (Exception)
            {
                return View("EditParentView", accountModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteAccount(AccountModel accountModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

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

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddAccount()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            AccountModel accountModel = new AccountModel();

            return View("AddParentView", accountModel);
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

                List<AccountType> accountTypes = _accountTypeService.GetAllAccountTypes();
                AccountType accountType =  accountTypes.Find(x => x.Type == "Parent");
                Guid accountTypeId = accountType.Id;

                Guid acc = _accountService.AddAccount(new Account()
                {
                    Login = accountModel.Login,
                    Password = accountModel.Password,
                    AccountTypeId = accountTypeId,
                });

                Guid us = _userService.AddUser(new User()
                {
                    Fio = accountModel.Fio,
                    Phone = accountModel.Phone,
                    Email = accountModel.Email,
                    Birthsday = accountModel.Birthsday,
                    AccountId = acc
                });

                    _parentService.AddParent(new Parent()
                    {
                        LargeFamilie = accountModel.LargeFamilie,
                        Id = us
                    });

            }
            catch (Exception)
            {


                return View("AddParentView", accountModel);
            }

            return RedirectToAction("Index");
        }

    }
}