using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DataBase.Repository;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
		private MessageRepository _MessageRepository;//репоизторий для операций с базой

		public MessageController(MessageRepository MessageRepository)
		{
			_MessageRepository = MessageRepository;
		}

		[Route("{id}", Name = "GetMessage"), HttpGet]
		public ActionResult<Message> GetMessage(Guid id)
		{

			return _MessageRepository.Get(id);
		}

		[Route("", Name = "GetMessages"), HttpGet]
		public ActionResult<IEnumerable<Message>> GetMessages()
		{
			return _MessageRepository.GetAll();
		}

		[Route("homeworkMessages/{homeworkId}", Name = "GetHomeworkMessages"), HttpGet]
		public ActionResult<IEnumerable<Message>> GetHomeworkMessages(Guid homeworkId)
		{
			return _MessageRepository.GetHomeworkMessages(homeworkId);
		}

		[Route("eventMessages/{eventId}", Name = "GetEventMessages"), HttpGet]
		public ActionResult<IEnumerable<Message>> GetEventMessages(Guid eventId)
		{
			return _MessageRepository.GetEventMessages(eventId);
		}


		[Route("", Name = "AddMessage"), HttpPost]
		public ActionResult AddMessage(Message Message)
		{
			_MessageRepository.Add(Message);
			return Ok();
		}

		[Route("", Name = "UpdateMessage"), HttpPut]
		public ActionResult UpdateMessage(Message Message)
		{
			_MessageRepository.Update(Message);
			return Ok();
		}

		[Route("{id}", Name = "DeleteMessage"), HttpDelete]
		public ActionResult DeleteMessage(Guid id)
		{
			_MessageRepository.Delete(id);
			return Ok();
		}
	}
}