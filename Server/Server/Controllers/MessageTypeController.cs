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
    public class MessageTypeController : ControllerBase
    {
		private MessageTypeRepository _MessageTypeRepository;//репоизторий для операций с базой

		public MessageTypeController(MessageTypeRepository MessageTypeRepository)
		{
			_MessageTypeRepository = MessageTypeRepository;
		}

		[Route("{id}", Name = "GetMessageType"), HttpGet]
		public ActionResult<MessageType> GetMessageType(Guid id)
		{

			return _MessageTypeRepository.Get(id);
		}

		[Route("", Name = "GetMessageTypes"), HttpGet]
		public ActionResult<IEnumerable<MessageType>> GetMessageTypes()
		{
			return _MessageTypeRepository.GetAll();
		}

		[Route("", Name = "AddMessageType"), HttpPost]
		public ActionResult AddMessageType(MessageType MessageType)
		{
			_MessageTypeRepository.Add(MessageType);
			return Ok();
		}

		[Route("", Name = "UpdateMessageType"), HttpPut]
		public ActionResult UpdateMessageType(MessageType MessageType)
		{
			_MessageTypeRepository.Update(MessageType);
			return Ok();
		}

		[Route("{id}", Name = "DeleteMessageType"), HttpDelete]
		public ActionResult DeleteMessageType(Guid id)
		{
			_MessageTypeRepository.Delete(id);
			return Ok();
		}
	}
}