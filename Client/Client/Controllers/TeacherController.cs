using Client.Models;
using Client.Models.ServerModels;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace Client.Controllers
{
    public class TeacherController : Controller
    {
        private AccountService _accountService;//сервис для вызова функций сервера
        private ParentService _parentService;
        private UserService _userService;
        private ChildService _childService;
        private AccountTypeService _accountTypeService;
        private TeacherService _teacherService;
        public TeacherController()//конструктор
        {
            _parentService = new ParentService();
            _teacherService = new TeacherService();
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

            List<Teacher> teachers = _teacherService.GetAllTeachers();

            IEnumerable<AccountModel> teacherList = from item in teachers
                                                    select new AccountModel
                                                    {
                                                        TeacherId = item.Id,
                                                        UserId = item.User.Id,
                                                        Fio = item.User.Fio,
                                                        Email = item.User.Email,
                                                        Birthsday = item.User.Birthsday,
                                                        BirthsdayStr = item.User.Birthsday != null ? item.User.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a",
                                                        Phone = item.User.Phone,
                                                        Portfolio = item.Portfolio,
                                                        Experience = item.Experience
                                                    };



            return View("ListTeachersView", teacherList);
        }

        public ActionResult ShowTeacher(AccountModel accountModel)//просмотр информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            return View("ShowTeacherView", accountModel);
        }
    }
}