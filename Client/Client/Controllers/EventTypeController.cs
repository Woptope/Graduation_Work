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
    public class EventTypeController : Controller
    {
        private EventTypeService _eventTypeService;

        public EventTypeController()
        {
            _eventTypeService = new EventTypeService();

        }
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<EventType> eventTypes = _eventTypeService.GetAllEventTypes();


            return View("ListEventTypesView", eventTypes);
        }

        public ActionResult DeleteEventType(EventType eventType)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            _eventTypeService.DeleteEventType(eventType.Id);
            return RedirectToAction("Index");
        }

        public ActionResult EditEventType(EventType eventType)//вызов страницы изменения информации 
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            return View("EditEventTypeView", eventType);
        }

        [HttpPost]
        public ActionResult UpdateEventType(EventType eventType)
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
                _eventTypeService.UpdateEventType(eventType);
            }
            catch (Exception)
            {
                return View("EditEventTypeView", eventType);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddEventType()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            return View("AddEventTypeView");
        }

        [HttpPost]
        public ActionResult AddEventType(EventType eventType)//добавление пользователя
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
                _eventTypeService.AddEventType(new EventType()
                {
                    Type = eventType.Type,
                });

            }
            catch (Exception)
            {
                return View("AddEventTypeView", eventType);
            }

            return RedirectToAction("Index");
        }
    }
}