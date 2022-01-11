using Client.Models;
using Client.Models.ServerModels;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class GroupController : Controller
    {
        private ChildService _childService;
        private GroupService _groupService;
        private TeacherService _teacherService;
        public GroupController()
        {
            _childService = new ChildService();
            _groupService = new GroupService();
            _teacherService = new TeacherService();

        }

        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<Group> groups = _groupService.GetAllGroups();

            IEnumerable<GroupModel> groupModels = from item in groups
                                                  select new GroupModel
                                                  {
                                                      GroupId = item.Id,
                                                      Name = item.Name,
                                                      Timetable = item.Timetable,
                                                      Description = item.Description,
                                                      TeacherId = item.TeacherId != null ? item.TeacherId.Value : Guid.Empty,
                                                      Teacher = item.Teacher
                                                  };
            var list = groupModels.ToArray();

            foreach (var group in list)
            {
                if (group.TeacherId == Guid.Empty)
                {
                    group.TeacherFio = "";
                }

                else
                {
                    Teacher teacher = _teacherService.GetTeacher(group.TeacherId);
                    group.TeacherFio = teacher.User.Fio;
                }
            }


            return View("ListGroupView", list);
        }

        public ActionResult DeleteGroup(GroupModel groupModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            _groupService.DeleteGroup(groupModel.GroupId);
            return RedirectToAction("Index");
        }

        public ActionResult EditGroup(GroupModel groupModel)//вызов страницы изменения информации 
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            IEnumerable<SelectListItem> teachers = from item in _teacherService.GetAllTeachers()
                                                   select new SelectListItem { Text = item.User.Fio, Value = item.Id.ToString() };


            groupModel.Teachers = teachers;

            return View("EditGroupView", groupModel);
        }

        [HttpPost]
        public ActionResult UpdateGroup(GroupModel groupModel)
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
                _groupService.UpdateGroup(new Group()
                {
                    Id = groupModel.GroupId,
                    Name = groupModel.Name,
                    Timetable = groupModel.Timetable,
                    Description = groupModel.Description,
                    TeacherId = groupModel.TeacherId
                });
            }
            catch (Exception)
            {
                return View("EditGroupView", groupModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddGroup()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            IEnumerable<SelectListItem> teachers = from item in _teacherService.GetAllTeachers()
                                                   select new SelectListItem { Text = item.User.Fio, Value = item.Id.ToString() };



            GroupModel groupModel = new GroupModel
            {
                Teachers = teachers
            };

            return View("AddGroupView", groupModel);
        }

        [HttpPost]
        public ActionResult AddGroup(GroupModel groupModel)//добавление пользователя
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
                _groupService.AddGroup(new Group()
                {
                    Name = groupModel.Name,
                    Timetable = groupModel.Timetable,
                    Description = groupModel.Description,
                    TeacherId = groupModel.TeacherId
                });
            }
            catch (Exception)
            {
                return View("AddGroupView", groupModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ShowGroup(Guid groupId)//просмотр информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            Group group = _groupService.GetGroup(groupId);
            ICollection<ChildInGroup> children = group.ChildrenInGroup;


            IEnumerable<ChildModel> childrenInGroup = from item in children
                                                      select new ChildModel
                                                      {
                                                          ChildId = item.ChildId,
                                                          Birthsday = item.Child.Birthsday,
                                                          Gender = item.Child.Gender,
                                                          Fio = item.Child.Fio,
                                                          BirthsdayStr = item.Child.Birthsday != null ? item.Child.Birthsday.Value.ToString("dd/MM/yyyy") : "n/a"
                                                      };

            var childrenInGroupList = childrenInGroup.ToArray();

            List<Child> allChildren = _childService.GetAllChildren();
            foreach (var item in allChildren.ToArray())
            {
                if (childrenInGroupList.Any(x => x.ChildId == item.Id))
                {
                    allChildren.Remove(item);
                }
            }

            IEnumerable<SelectListItem> childrenAdd = from item in allChildren
                                                      select new SelectListItem { Text = item.Fio, Value = item.Id.ToString() };

            GroupModel groupModel = new GroupModel
            {
                Children = childrenInGroup,
                ChildrenAdd = childrenAdd,
                GroupId = group.Id,
                Name = group.Name,
                Timetable = group.Timetable,
                Description = group.Description,
                TeacherId = group.TeacherId != null ? group.TeacherId.Value : Guid.Empty,
                Teacher = group.Teacher
            };

            if (groupModel.TeacherId == Guid.Empty)
            {
                groupModel.TeacherFio = "";
            }

            else
            {
                Teacher teacher = _teacherService.GetTeacher(groupModel.TeacherId);
                groupModel.TeacherFio = teacher.User.Fio;
            }



            return View("ShowGroupView", groupModel);
        }

    }
}