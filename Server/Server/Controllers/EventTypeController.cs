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
    public class EventTypeController : ControllerBase
    {
		private EventTypeRepository _EventTypeRepository;//репоизторий для операций с базой

		public EventTypeController(EventTypeRepository EventTypeRepository)
		{
			_EventTypeRepository = EventTypeRepository;
		}

		[Route("{id}", Name = "GetEventType"), HttpGet]
		public ActionResult<EventType> GetEventType(Guid id)
		{

			return _EventTypeRepository.Get(id);
		}

		[Route("", Name = "GetEventTypes"), HttpGet]
		public ActionResult<IEnumerable<EventType>> GetEventTypes()
		{
			return _EventTypeRepository.GetAll();
		}

		[Route("", Name = "AddEventType"), HttpPost]
		public ActionResult AddEventType(EventType EventType)
		{
			_EventTypeRepository.Add(EventType);
			return Ok();
		}

		[Route("", Name = "UpdateEventType"), HttpPut]
		public ActionResult UpdateEventType(EventType EventType)
		{
			_EventTypeRepository.Update(EventType);
			return Ok();
		}

		[Route("{id}", Name = "DeleteEventType"), HttpDelete]
		public ActionResult DeleteEventType(Guid id)
		{
			_EventTypeRepository.Delete(id);
			return Ok();
		}
	}
}