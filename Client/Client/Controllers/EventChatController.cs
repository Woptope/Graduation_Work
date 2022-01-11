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
    public class EventChatController : Controller
    {
        private EventService _eventService;
        private MessageService _messageService;
        private MessageTypeService _messageTypeService;
        public EventChatController()
        {
            _eventService = new EventService();
            _messageService = new MessageService();
            _messageTypeService = new MessageTypeService();
        }
        public ActionResult ShowEventChat(Guid eventId)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            Event Event = _eventService.GetEvent(eventId);

            IEnumerable<MessageModel> messageModels = from item in _messageService.GetEventMessages(eventId)
                                                      select new MessageModel

                                                      {
                                                          MessageText = item.MessageText,
                                                          MessageDateTime = item.MessageDateTime,
                                                          UserFio = item.User.Fio,
                                                          MessageType = item.MessageType.Type,
                                                          IsAnonymous = item.IsAnonymous,

                                                      };
            List<MessageModel> messageModels1 = messageModels.ToList();

            foreach (var item in messageModels1)
            {
                if (item.IsAnonymous == true)
                {
                    item.UserFio = "Аноним";
                }
            }


            var messages = string.Join(Environment.NewLine + Environment.NewLine, messageModels1
    .OrderByDescending(x => x.MessageDateTime)
    .Select(x => x.UserFio + Environment.NewLine + x.MessageType + " " + x.MessageDateTime + Environment.NewLine + x.MessageText)
    .ToList());


            IEnumerable<SelectListItem> messageTypes = from item in _messageTypeService.GetAllMessageTypes()
                                                       select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };

            EventChatModel eventChatModel = new EventChatModel
            {
                EventId = eventId,
                Name = Event.Name,
                EventDateTimeStr = Event.EventDateTime.ToString("dd/MM/yyyy"),
                EventType = Event.EventType.Type,
                Messages = messages,
                MessageTypes = messageTypes
            };



            return View("ShowEventChatView", eventChatModel);
        }

        [HttpPost]
        public ActionResult SendMessage(EventChatModel eventChatModel)//добавление пользователя
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
                _messageService.AddMessage(new Message()
                {
                    MessageText = eventChatModel.MessageText,
                    MessageDateTime = DateTime.Now,
                    EventId = eventChatModel.EventId,
                    IsAnonymous = eventChatModel.IsAnonymous,
                    MessageTypeId = eventChatModel.MessageTypeId,
                    UserId = Guid.Parse(Session["userId"].ToString()),
                });
            }


            catch (Exception)
            {
                return View("ShowEventChatView", eventChatModel);
            }

            return RedirectToAction("ShowEventChat", new { eventId = eventChatModel.EventId });
        }
    }
}