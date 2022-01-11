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
    public class HomeworkChatController : Controller
    {
        private HomeworkService _homeworkService;
        private MessageService _messageService;
        private MessageTypeService _messageTypeService;
        public HomeworkChatController()
        {
            _homeworkService = new HomeworkService();
            _messageService = new MessageService();
            _messageTypeService = new MessageTypeService();
        }
        public ActionResult ShowHomeworkChat(Guid homeworkId)
        {
            if (Session["userId"] == null)
            {
                return View("~/Views/Login/LoginView.cshtml", new LoginModel());
            }

            Homework homework = _homeworkService.GetHomework(homeworkId);

            IEnumerable<MessageModel> messageModels = from item in _messageService.GetHomeworkMessages(homeworkId)
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
    .Select(x =>  x.UserFio + Environment.NewLine + x.MessageType + " " + x.MessageDateTime + Environment.NewLine + x.MessageText)
    .ToList());


            IEnumerable<SelectListItem> messageTypes = from item in _messageTypeService.GetAllMessageTypes()
                                                       select new SelectListItem { Text = item.Type, Value = item.Id.ToString() };

            HomeworkChatModel homeworkChatModel = new HomeworkChatModel
            {
                HomeworkId = homeworkId,
                Name = homework.Name,
                HomeWorkDateTimeStr = homework.HomeWorkDateTime.ToString("dd/MM/yyyy"),
                HomeworkType = homework.HomeworkType.Type,
                Messages = messages,
                TeacherFio = homework.Teacher.User.Fio,
                MessageTypes = messageTypes
            };



            return View("ShowHomeworkChatView", homeworkChatModel);
        }

        [HttpPost]
        public ActionResult SendMessage(HomeworkChatModel homeworkChatModel)//добавление пользователя
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
                    MessageText = homeworkChatModel.MessageText,
                    MessageDateTime = DateTime.Now,
                    HomeworkId = homeworkChatModel.HomeworkId,
                    IsAnonymous = homeworkChatModel.IsAnonymous,
                    MessageTypeId = homeworkChatModel.MessageTypeId,
                    UserId = Guid.Parse(Session["userId"].ToString()),
                });
            }


            catch (Exception)
            {
                return View("ShowHomeworkChatView", homeworkChatModel);
            }

            return RedirectToAction("ShowHomeworkChat", new { homeworkId = homeworkChatModel.HomeworkId });
        }
    }
}