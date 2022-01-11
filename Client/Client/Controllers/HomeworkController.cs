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
    public class HomeworkController : Controller
    {
        private HomeworkService _homeworkService;
        private HomeworkTypeService _homeworkTypeService;
        private TeacherService _teacherService;
        private RatingHomeworkService _ratingHomeworkService;
        private GroupService _groupService;
        private HomeworkForGroupService _homeworkForGroupService;
        public HomeworkController()
        {
            _homeworkService = new HomeworkService();
            _homeworkTypeService = new HomeworkTypeService();
            _teacherService = new TeacherService();
            _ratingHomeworkService = new RatingHomeworkService();
            _groupService = new GroupService();
            _homeworkForGroupService = new HomeworkForGroupService();
        }
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<Homework> homeworks = _homeworkService.GetAllHomework();

            IEnumerable<HomeworkModel> homeworkModels = from item in homeworks
                                                        select new HomeworkModel
                                                        {
                                                            HomeworkId = item.Id,
                                                            Name = item.Name,
                                                            Description = item.Description,
                                                            HomeworkDateTime = item.HomeWorkDateTime,
                                                            HomeWorkDateTimeStr = item.HomeWorkDateTime.ToString("dd/MM/yyyy"),
                                                            LikesCount = item.LikesCount,
                                                            DisLikesCount = item.DisLikesCount,
                                                            LinkFile = item.LinkFile,
                                                            TeacherId = item.TeacherId,
                                                            HomeworkTypeId = item.HomeworkTypeId,
                                                            HomeworkType = item.HomeworkType.Type,
                                                            TeacherFio = item.Teacher.User.Fio,
                                                            HomeworkForGroups = item.HomeworkForGroups
                                                        };

            List<Group> groups = _groupService.GetAllGroups();


            HomeworkModel homeworkModel = new HomeworkModel
            {
                Homeworks = homeworkModels,
                Groups = new MultiSelectList(groups, "Id", "Name")
            };

            return View("ListHomeworkView", homeworkModel);
        }

        public ActionResult DeleteHomework(HomeworkModel homeworkModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            _homeworkService.DeleteHomework(homeworkModel.HomeworkId);
            return RedirectToAction("Index");
        }

        public ActionResult EditHomework(HomeworkModel homeworkModel)//вызов страницы изменения информации 
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            IEnumerable<SelectListItem> teachers = from item in _teacherService.GetAllTeachers()
                                                   select new SelectListItem { Text = item.User.Fio, Value = item.Id.ToString() };


            homeworkModel.Teachers = teachers;

            IEnumerable<SelectListItem> homeworkTypes = from item in _homeworkTypeService.GetAllHomeworkTypes()
                                                        select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };


            homeworkModel.HomeworkTypes = homeworkTypes;

            Homework homework = _homeworkService.GetHomework(homeworkModel.HomeworkId);
            homeworkModel.HomeworkForGroups = homework.HomeworkForGroups;

            List<Group> groups = _groupService.GetAllGroups();
            List<Guid> guids = new List<Guid>();
            foreach (var temp in homeworkModel.HomeworkForGroups)
            {
                guids.Add(temp.GroupId);
            }

            homeworkModel.Groups = new MultiSelectList(groups, "Id", "Name", guids);



            return View("EditHomeworkView", homeworkModel);
        }

        [HttpPost]
        public ActionResult UpdateHomework(HomeworkModel homeworkModel)
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
                Homework homework = _homeworkService.GetHomework(homeworkModel.HomeworkId);
                List<HomeworkForGroup> homeworkForGroups = homework.HomeworkForGroups.ToList();//Старые дз для группы



                foreach (var item in homeworkForGroups)
                {
                    if (!homeworkModel.GroupIds.Contains(item.GroupId))
                    {
                        _homeworkForGroupService.DeleteHomeworkForGroup(item.HomeworkId, item.GroupId);
                    }
                }

                foreach (var item in homeworkModel.GroupIds)
                {
                    if (!homeworkForGroups.Any(x => (x.GroupId == item)))
                    {
                        HomeworkForGroup homeworkForGroup = new HomeworkForGroup
                        {
                            GroupId = item,
                            HomeworkId = homeworkModel.HomeworkId
                        };
                        _homeworkForGroupService.AddHomeworkForGroup(homeworkForGroup);
                    }
                }


                _homeworkService.UpdateHomework(new Homework()
                {
                    Id = homeworkModel.HomeworkId,
                    Name = homeworkModel.Name,
                    Description = homeworkModel.Description,
                    HomeWorkDateTime = homeworkModel.HomeworkDateTime,
                    LikesCount = homeworkModel.LikesCount,
                    DisLikesCount = homeworkModel.DisLikesCount,
                    LinkFile = homeworkModel.LinkFile,
                    TeacherId = homeworkModel.TeacherId,
                    HomeworkTypeId = homeworkModel.HomeworkTypeId
                });
            }
            catch (Exception)
            {
                return View("EditHomeworkView", homeworkModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddHomework()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            IEnumerable<SelectListItem> teachers = from item in _teacherService.GetAllTeachers()
                                                   select new SelectListItem { Text = item.User.Fio, Value = item.Id.ToString() };


            IEnumerable<SelectListItem> homeworkTypes = from item in _homeworkTypeService.GetAllHomeworkTypes()
                                                        select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };
            List<Group> groups = _groupService.GetAllGroups();

            HomeworkModel homeworkModel = new HomeworkModel
            {
                HomeworkTypes = homeworkTypes,
                Teachers = teachers,
                Groups = new MultiSelectList(groups, "Id", "Name")
            };

            return View("AddHomeworkView", homeworkModel);
        }

        [HttpPost]
        public ActionResult AddHomework(HomeworkModel homeworkModel)//добавление пользователя
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
                Guid newHomework = _homeworkService.AddHomework(new Homework()
                {
                    Id = homeworkModel.HomeworkId,
                    Name = homeworkModel.Name,
                    Description = homeworkModel.Description,
                    HomeWorkDateTime = homeworkModel.HomeworkDateTime,
                    LikesCount = homeworkModel.LikesCount,
                    DisLikesCount = homeworkModel.DisLikesCount,
                    LinkFile = homeworkModel.LinkFile,
                    TeacherId = homeworkModel.TeacherId,
                    HomeworkTypeId = homeworkModel.HomeworkTypeId
                });

                foreach (var item in homeworkModel.GroupIds)
                {
                    HomeworkForGroup homeworkForGroup = new HomeworkForGroup
                    {
                        HomeworkId = newHomework,
                        GroupId = item
                    };
                    _homeworkForGroupService.AddHomeworkForGroup(homeworkForGroup);
                }
            }
            catch (Exception)
            {
                return View("AddHomeworkView", homeworkModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult LikeHomework(HomeworkModel homeworkModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            try
            {
                Homework homework = _homeworkService.GetHomework(homeworkModel.HomeworkId);

                ICollection<RatingHomework> ratingHomeworks = homework.RatingHomework; //Список всех, кто лайкнул домашку

                if (!ratingHomeworks.Any(x => (x.UserId.ToString() == Session["userId"].ToString())))
                {
                    RatingHomework ratingHomework = new RatingHomework
                    {
                        UserId = Guid.Parse(Session["userId"].ToString()),
                        HomeworkId = homeworkModel.HomeworkId,
                        Rating = true
                    };

                    _ratingHomeworkService.AddRatingHomework(ratingHomework);
                    _homeworkService.AddLike(homeworkModel.HomeworkId);
                }

                else
                {
                    foreach (var item in ratingHomeworks.ToArray())
                    {
                        if (item.UserId.ToString() == Session["userId"].ToString() && item.Rating == false)
                        {
                            item.Rating = true;
                            _ratingHomeworkService.UpdateRatingHomework(item);
                            _homeworkService.AddLike(homeworkModel.HomeworkId);
                            _homeworkService.DeleteDislike(homeworkModel.HomeworkId);
                        }
                    }
                }

            }
            catch (Exception)
            {
                return View("ListHomeworkView", homeworkModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DisLikeHomework(HomeworkModel homeworkModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            try
            {
                Homework homework = _homeworkService.GetHomework(homeworkModel.HomeworkId);

                ICollection<RatingHomework> ratingHomeworks = homework.RatingHomework; //Список всех, кто лайкнул домашку

                if (!ratingHomeworks.Any(x => (x.UserId.ToString() == Session["userId"].ToString())))
                {
                    RatingHomework ratingHomework = new RatingHomework
                    {
                        UserId = Guid.Parse(Session["userId"].ToString()),
                        HomeworkId = homeworkModel.HomeworkId,
                        Rating = true
                    };

                    _ratingHomeworkService.AddRatingHomework(ratingHomework);
                    _homeworkService.AddDislike(homeworkModel.HomeworkId);
                }

                else
                {
                    foreach (var item in ratingHomeworks.ToArray())
                    {
                        if (item.UserId.ToString() == Session["userId"].ToString() && item.Rating == true)
                        {
                            item.Rating = false;
                            _ratingHomeworkService.UpdateRatingHomework(item);
                            _homeworkService.AddDislike(homeworkModel.HomeworkId);
                            _homeworkService.DeleteLike(homeworkModel.HomeworkId);
                        }
                    }
                }

            }
            catch (Exception)
            {
                return View("ListHomeworkView", homeworkModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ShowHomework(HomeworkModel homeworkModel)//просмотр информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            Homework homework = _homeworkService.GetHomework(homeworkModel.HomeworkId);


            foreach (var item in homework.HomeworkForGroups)
            {
                homeworkModel.GroupsForShow += item.Group.Name + "\r\n";
            }

            return View("ShowHomeworkView", homeworkModel);
        }

        [HttpPost]
        public ActionResult SearchHomework(HomeworkModel homeworkModel)//поиск сессий по фильтрам
        {
            if (Session["userId"] == null)
            {
                return View("LoginView", new LoginModel());
            }

            List<Homework> homeworks = _homeworkService.GetAllHomework();

            if (!string.IsNullOrEmpty(homeworkModel.Name))
            {
                homeworks = homeworks.Where(x => x.Name.Contains(homeworkModel.Name)).ToList();
            }

            if (!string.IsNullOrEmpty(homeworkModel.Description))
            {
                homeworks = homeworks.Where(x => x.Description.Contains(homeworkModel.Description)).ToList();
            }

            if (homeworkModel.HomeworkDateFrom != null)
            {
                homeworks = homeworks.Where(x => x.HomeWorkDateTime.Date >= homeworkModel.HomeworkDateFrom.Value.Date).ToList();
            }

            if (homeworkModel.HomeworkDateTo != null)
            {
                homeworks = homeworks.Where(x => x.HomeWorkDateTime.Date <= homeworkModel.HomeworkDateTo.Value.Date).ToList();
            }

            if (homeworkModel.LikesCountFrom != null)
            {
                homeworks = homeworks.Where(x => x.LikesCount >= homeworkModel.LikesCountFrom).ToList();
            }

            if (homeworkModel.LikesCountTo != null)
            {
                homeworks = homeworks.Where(x => x.LikesCount <= homeworkModel.LikesCountTo).ToList();
            }

            if (homeworkModel.GroupIds != null)
            {
                homeworks = homeworks.Where(x => x.HomeworkForGroups.Any(y => homeworkModel.GroupIds.Contains(y.GroupId))).ToList();
            }

            IEnumerable<HomeworkModel> homeworkModels = from item in homeworks
                                                        select new HomeworkModel
                                                        {
                                                            HomeworkId = item.Id,
                                                            Name = item.Name,
                                                            Description = item.Description,
                                                            HomeworkDateTime = item.HomeWorkDateTime,
                                                            HomeWorkDateTimeStr = item.HomeWorkDateTime.ToString("dd/MM/yyyy"),
                                                            LikesCount = item.LikesCount,
                                                            DisLikesCount = item.DisLikesCount,
                                                            LinkFile = item.LinkFile,
                                                            TeacherId = item.TeacherId,
                                                            HomeworkTypeId = item.HomeworkTypeId,
                                                            HomeworkType = item.HomeworkType.Type,
                                                            TeacherFio = item.Teacher.User.Fio,
                                                            HomeworkForGroups = item.HomeworkForGroups
                                                        };

            List<Group> groups = _groupService.GetAllGroups();
            homeworkModel.Homeworks = homeworkModels;
            homeworkModel.Groups = new MultiSelectList(groups, "Id", "Name");

            return View("ListHomeworkView", homeworkModel);
        }


    }
}