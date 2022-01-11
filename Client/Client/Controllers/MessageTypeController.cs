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
    public class MessageTypeController : Controller
    {
        private MessageTypeService _messageTypeService;

        public MessageTypeController()
        {
            _messageTypeService = new MessageTypeService();

        }
        public ActionResult Index()
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            List<MessageType> messageTypes = _messageTypeService.GetAllMessageTypes();


            return View("ListMessageTypesView", messageTypes);
        }

        public ActionResult DeleteMessageType(MessageType messageType)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            _messageTypeService.DeleteMessageType(messageType.Id);
            return RedirectToAction("Index");
        }

        public ActionResult EditMessageType(MessageType messageType)//вызов страницы изменения информации 
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            return View("EditMessageTypeView", messageType);
        }

        [HttpPost]
        public ActionResult UpdateMessageType(MessageType messageType)
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
                _messageTypeService.UpdateMessageType(messageType);
            }
            catch (Exception)
            {
                return View("EditMessageTypeView", messageType);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddMessageType()//вызов страницы добавления аккаунта
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }


            return View("AddMessageTypeView");
        }

        [HttpPost]
        public ActionResult AddMessageType(MessageType messageType)//добавление пользователя
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
                _messageTypeService.AddMessageType(new MessageType()
                {
                    Type = messageType.Type,
                });

            }
            catch (Exception)
            {
                return View("AddMessageTypeView", messageType);
            }

            return RedirectToAction("Index");
        }
    }
}