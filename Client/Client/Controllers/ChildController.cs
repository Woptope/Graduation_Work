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
    public class ChildController : Controller
    {
        private ChildService _childService;
        private ParentService _parentService;
        public ChildController()
        {
            _childService = new ChildService();
            _parentService = new ParentService();
        }

        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<Child> children = _childService.GetAllChildren();

            IEnumerable<ChildModel> childrenList = from item in children
                                                   select new ChildModel
                                                   {
                                                       ChildId = item.Id,
                                                       Fio = item.Fio,
                                                       Gender = item.Gender,
                                                       MotherId = item.MotherId,
                                                       FatherId = item.FatherId,
                                                       Mother = item.Mother,
                                                       Father = item.Father,
                                                       Birthsday = item.Birthsday,
                                                       BirthsdayStr = item.Birthsday != null ? item.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a"
                                                   };
            var list = childrenList.ToArray();

            foreach (var child in list)
            {
                if (child.Mother == null)
                {
                    child.MotherFio = "";
                }
                else child.MotherFio = child.Mother.User.Fio;

                if (child.Father == null)
                    {
                        child.FatherFio = "";
                    }
                else child.FatherFio = child.Father.User.Fio;
            }


            return View("ListChildrenView", list);
        }
        /*        MotherFio = item.Mother.User.Fio ?? "",
                                                               FatherFio = item.Father.User.Fio ?? "",*/
        public ActionResult DeleteChild(ChildModel childModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            _childService.DeleteChild(childModel.ChildId);
            return RedirectToAction("Index");
        }

        public ActionResult EditChild(ChildModel childModel)//вызов страницы изменения информации 
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            return View("EditChildView", childModel);
        }

        [HttpPost]
        public ActionResult UpdateChild(ChildModel childModel)
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
                _childService.UpdateChild(new Child()
                {
                    Id = childModel.ChildId,
                    Birthsday = childModel.Birthsday,
                    Fio = childModel.Fio,
                    Gender = childModel.Gender,
                    FatherId = childModel.FatherId,
                    MotherId = childModel.MotherId,
                });
            }
            catch (Exception)
            {
                return View("EditChildView", childModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ShowChildrenFromParent(Guid parentId)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<Child> children = _childService.GetAllChildrenForParent(parentId);

            IEnumerable<ChildModel> childrenList = from item in children
                                                   select new ChildModel
                                                   {
                                                       ChildId = item.Id,
                                                       Fio = item.Fio,
                                                       Gender = item.Gender,
                                                       MotherId = item.MotherId,
                                                       FatherId = item.FatherId,
                                                       Mother = item.Mother,
                                                       Father = item.Father,
                                                       Birthsday = item.Birthsday,
                                                       BirthsdayStr = item.Birthsday != null ? item.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a"
                                                   };
            var list = childrenList.ToArray();

            foreach (var child in list)
            {
                if (child.Mother == null)
                {
                    child.MotherFio = "";
                }
                else child.MotherFio = child.Mother.User.Fio;

                if (child.Father == null)
                {
                    child.FatherFio = "";
                }
                else child.FatherFio = child.Father.User.Fio;
            }


            return View("ListChildrenView", list);
        }

        [HttpGet]
        public ActionResult AddChild()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            IEnumerable<SelectListItem> parents = from item in _parentService.GetAllParents()
                                                       select new SelectListItem { Text = item.User.Fio, Value = item.Id.ToString() };


            List<SelectListItem>  parentList = parents.ToList();

            parentList.Insert(0,new SelectListItem() { Text = "", Value = "" });

            ChildModel childModel = new ChildModel
            {
                Parents = parentList
            };

            return View("AddChildView", childModel);
        }

        [HttpPost]
        public ActionResult AddChild(ChildModel childModel)//добавление пользователя
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
                _childService.AddChild(new Child()
                {
                    Birthsday = childModel.Birthsday,
                    Fio = childModel.Fio,
                    Gender = childModel.Gender,
                    MotherId = childModel.MotherId,
                    FatherId = childModel.FatherId
                });
            }
            catch (Exception)
            {
                return View("AddChildView", childModel);
            }

            return RedirectToAction("Index");
        }


    }
}