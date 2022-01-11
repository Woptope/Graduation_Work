using Client.Models;
using Client.Models.ServerModels;
using Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class EventController : Controller
    {
        private TeacherService _teacherService;
        private EventService _eventService;
        private EventTypeService _eventTypeService;
        private EventGroupService _eventGroupService;
        private RatingEventService _ratingEventService;
        private GroupService _groupService;
        private HomeworkForGroupService _homeworkForGroupService;
        public EventController()
        {
            _eventService = new EventService();
            _eventTypeService = new EventTypeService();
            _teacherService = new TeacherService();
            _ratingEventService = new RatingEventService();
            _groupService = new GroupService();
            _homeworkForGroupService = new HomeworkForGroupService();
            _eventGroupService = new EventGroupService();
        }
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<Event> events = _eventService.GetAllEvents();

            IEnumerable<EventModel> eventModels = from item in events
                                                        select new EventModel
                                                        {
                                                            EventId = item.Id,
                                                            Name = item.Name,
                                                            Description = item.Description,
                                                            EventDateTime = item.EventDateTime,
                                                            EventDateTimeStr = item.EventDateTime.ToString("dd/MM/yyyy"),
                                                            LikesCount = item.LikesCount,
                                                            DisLikesCount = item.DisLikesCount,
                                                            EventTypeId = item.EventTypeId,
                                                            EventType = item.EventType.Type,
                                                            EventForGroups = item.EventGroups
                                                        };


            List<Group> groups = _groupService.GetAllGroups();


            EventModel eventModel = new EventModel
            {
                Events = eventModels,
                Groups = new MultiSelectList(groups, "Id", "Name")
            };
            return View("ListEventView", eventModel);
        }

        public ActionResult DeleteEvent(EventModel eventModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            _eventService.DeleteEvent(eventModel.EventId);
            return RedirectToAction("Index");
        }

        public ActionResult EditEvent(EventModel eventModel)//вызов страницы изменения информации 
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            IEnumerable<SelectListItem> eventTypes = from item in _eventTypeService.GetAllEventTypes()
                                                        select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };


            eventModel.EventTypes = eventTypes;

            Event Event = _eventService.GetEvent(eventModel.EventId);
            eventModel.EventForGroups = Event.EventGroups;

            List<Group> groups = _groupService.GetAllGroups();
            List<Guid> guids = new List<Guid>();
            foreach (var temp in eventModel.EventForGroups)
            {
                guids.Add(temp.GroupId);
            }

            eventModel.Groups = new MultiSelectList(groups, "Id", "Name", guids);



            return View("EditEventView", eventModel);
        }

        [HttpPost]
        public ActionResult UpdateEvent(EventModel eventModel)
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
                Event Event = _eventService.GetEvent(eventModel.EventId);
                List<EventGroup> eventGroups = Event.EventGroups.ToList();//Старые дз для группы



                foreach (var item in eventGroups)
                {
                    if (!eventModel.GroupIds.Contains(item.GroupId))
                    {
                        _eventGroupService.DeleteEventGroup(item.EventId, item.GroupId);
                    }
                }

                foreach (var item in eventModel.GroupIds)
                {
                    if (!eventGroups.Any(x => (x.GroupId == item)))
                    {
                        EventGroup eventGroup = new EventGroup
                        {
                            GroupId = item,
                            EventId = eventModel.EventId
                        };
                        _eventGroupService.AddEventGroup(eventGroup);
                    }
                }


                _eventService.UpdateEvent(new Event()
                {
                    Id = eventModel.EventId,
                    Name = eventModel.Name,
                    Description = eventModel.Description,
                    EventDateTime = eventModel.EventDateTime,
                    LikesCount = eventModel.LikesCount,
                    DisLikesCount = eventModel.DisLikesCount,
                    EventTypeId = eventModel.EventTypeId
                });
            }
            catch (Exception)
            {
                return View("EditEventView", eventModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddEvent()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            IEnumerable<SelectListItem> eventTypes = from item in _eventTypeService.GetAllEventTypes()
                                                        select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };
            List<Group> groups = _groupService.GetAllGroups();

            EventModel eventModel = new EventModel
            {
                EventTypes = eventTypes,
                Groups = new MultiSelectList(groups, "Id", "Name")
            };

            return View("AddEventView", eventModel);
        }

        [HttpPost]
        public ActionResult AddEvent(EventModel eventModel)//добавление пользователя
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
                Guid newEvent = _eventService.AddEvent(new Event()
                {
                    Name = eventModel.Name,
                    Description = eventModel.Description,
                    EventDateTime = eventModel.EventDateTime,
                    LikesCount = eventModel.LikesCount,
                    DisLikesCount = eventModel.DisLikesCount,
                    EventTypeId = eventModel.EventTypeId
                });

                foreach (var item in eventModel.GroupIds)
                {
                    EventGroup eventGroup = new EventGroup
                    {
                        EventId = newEvent,
                        GroupId = item
                    };
                    _eventGroupService.AddEventGroup(eventGroup);
                }
            }
            catch (Exception)
            {
                return View("AddEventView", eventModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ShowEvent(EventModel eventModel)//просмотр информации о пользователе
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            Event Event = _eventService.GetEvent(eventModel.EventId);


            foreach (var item in Event.EventGroups)
            {
                eventModel.GroupsForShow += item.Group.Name + "\r\n";
            }

            return View("ShowEventView", eventModel);
        }

        public ActionResult LikeEvent(EventModel eventModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            try
            {
                Event Event = _eventService.GetEvent(eventModel.EventId);

                ICollection<RatingEvent> ratingEvents = Event.RatingEvent; //Список всех, кто лайкнул домашку

                if (!ratingEvents.Any(x => (x.UserId.ToString() == Session["userId"].ToString())))
                {
                    RatingEvent ratingEvent = new RatingEvent
                    {
                        UserId = Guid.Parse(Session["userId"].ToString()),
                        EventId = eventModel.EventId,
                        Rating = true
                    };

                    _ratingEventService.AddRatingEvent(ratingEvent);
                    _eventService.AddLike(eventModel.EventId);
                }

                else
                {
                    foreach (var item in ratingEvents.ToArray())
                    {
                        if (item.UserId.ToString() == Session["userId"].ToString() && item.Rating == false)
                        {
                            item.Rating = true;
                            _ratingEventService.UpdateRatingEvent(item);
                            _eventService.AddLike(eventModel.EventId);
                            _eventService.DeleteDislike(eventModel.EventId);
                        }
                    }
                }

            }
            catch (Exception)
            {
                return View("ListEventView", eventModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DisLikeEvent(EventModel eventModel)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            try
            {
                Event Event = _eventService.GetEvent(eventModel.EventId);

                ICollection<RatingEvent> ratingEvents = Event.RatingEvent; //Список всех, кто лайкнул домашку

                if (!ratingEvents.Any(x => (x.UserId.ToString() == Session["userId"].ToString())))
                {
                    RatingEvent ratingEvent = new RatingEvent
                    {
                        UserId = Guid.Parse(Session["userId"].ToString()),
                        EventId = eventModel.EventId,
                        Rating = true
                    };

                    _ratingEventService.AddRatingEvent(ratingEvent);
                    _eventService.AddDislike(eventModel.EventId);
                }

                else
                {
                    foreach (var item in ratingEvents.ToArray())
                    {
                        if (item.UserId.ToString() == Session["userId"].ToString() && item.Rating == true)
                        {
                            item.Rating = false;
                            _ratingEventService.UpdateRatingEvent(item);
                            _eventService.AddDislike(eventModel.EventId);
                            _eventService.DeleteLike(eventModel.EventId);
                        }
                    }
                }

            }
            catch (Exception)
            {
                return View("ListEventView", eventModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SearchEvents(EventModel eventModel)//поиск сессий по фильтрам
        {
            if (Session["userId"] == null)
            {
                return View("LoginView", new LoginModel());
            }

            List<Event> events = _eventService.GetAllEvents();

            if (!string.IsNullOrEmpty(eventModel.Name))
            {
                events = events.Where(x => x.Name.Contains(eventModel.Name)).ToList();
            }

            if (!string.IsNullOrEmpty(eventModel.Description))
            {
                events = events.Where(x => x.Description.Contains(eventModel.Description)).ToList();
            }

            if (eventModel.EventDateFrom != null)
            {
                events = events.Where(x => x.EventDateTime.Date >= eventModel.EventDateFrom.Value.Date).ToList();
            }

            if (eventModel.EventDateTo != null)
            {
                events = events.Where(x => x.EventDateTime.Date <= eventModel.EventDateTo.Value.Date).ToList();
            }

            if (eventModel.LikesCountFrom != null)
            {
                events = events.Where(x => x.LikesCount >= eventModel.LikesCountFrom).ToList();
            }

            if (eventModel.LikesCountTo != null)
            {
                events = events.Where(x => x.LikesCount <= eventModel.LikesCountTo).ToList();
            }

            if (eventModel.GroupIds != null)
            {

                events = events.Where(x => x.EventGroups.Any(y => eventModel.GroupIds.Contains( y.GroupId))).ToList();
            }

            IEnumerable<EventModel> eventModels = from item in events
                                                  select new EventModel
                                                  {
                                                      EventId = item.Id,
                                                      Name = item.Name,
                                                      Description = item.Description,
                                                      EventDateTime = item.EventDateTime,
                                                      EventDateTimeStr = item.EventDateTime.ToString("dd/MM/yyyy"),
                                                      LikesCount = item.LikesCount,
                                                      DisLikesCount = item.DisLikesCount,
                                                      EventTypeId = item.EventTypeId,
                                                      EventType = item.EventType.Type,
                                                      EventForGroups = item.EventGroups
                                                  };
            eventModel.GroupIds = null;
            List<Group> groups = _groupService.GetAllGroups();
            eventModel.Events = eventModels;
            eventModel.Groups = new MultiSelectList(groups, "Id", "Name");




            return View("ListEventView", eventModel);
        }
    }
}