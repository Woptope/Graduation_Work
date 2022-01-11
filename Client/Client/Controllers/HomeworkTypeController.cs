using Client.Models;
using Client.Models.ServerModels;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class HomeworkTypeController : Controller
    {
        private HomeworkTypeService _homeworkTypeService;

        public HomeworkTypeController()
        {
            _homeworkTypeService = new HomeworkTypeService();

        }
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<HomeworkType> homeworkTypes = _homeworkTypeService.GetAllHomeworkTypes();

            IEnumerable<HomeworkTypeModel> homeworkTypeModels = from item in homeworkTypes
                                                                select new HomeworkTypeModel
                                                                {
                                                                    Id = item.Id,
                                                                    Type = item.Type,
                                                                };

            return View("ListHomeworkTypesView", homeworkTypeModels);
        }

        public ActionResult DeleteHomeworkType(HomeworkTypeModel homeworkTypeModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            _homeworkTypeService.DeleteHomeworkType(homeworkTypeModel.Id);
            return RedirectToAction("Index");
        }

        public ActionResult EditHomeworkType(HomeworkTypeModel homeworkTypeModel)//вызов страницы изменения информации 
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            return View("EditHomeworkTypeView", homeworkTypeModel);
        }

        [HttpPost]
        public ActionResult UpdateHomeworkType(HomeworkTypeModel homeworkTypeModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            /*            if (!ModelState.IsValid)
                        {
                            return View("~/Views/Users/UpdateUserView.cshtml", groupModel);
                        }*/
            try
            {
                _homeworkTypeService.UpdateHomeworkType(new HomeworkType()
                {
                    Id = homeworkTypeModel.Id,
                    Type = homeworkTypeModel.Type
                });
            }
            catch (Exception)
            {
                return View("EditHomeworkTypeView", homeworkTypeModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddHomeworkType()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            return View("AddHomeworkTypeView");
        }

        [HttpPost]
        public ActionResult AddHomeworkType(HomeworkTypeModel homeworkTypeModel)//добавление пользователя
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
                _homeworkTypeService.AddHomeworkType(new HomeworkType()
                {
                    Type = homeworkTypeModel.Type,
                });

            }
            catch (Exception)
            {
                return View("AddHomeworkTypeView", homeworkTypeModel);
            }

            return RedirectToAction("Index");
        }
    }
}