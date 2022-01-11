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
    public class EventGroupController : ControllerBase
    {
		private EventGroupRepository _EventGroupRepository;//репоизторий для операций с базой

		public EventGroupController(EventGroupRepository EventGroupRepository)
		{
			_EventGroupRepository = EventGroupRepository;
		}

		[Route("{id}", Name = "GetEventGroup"), HttpGet]
		public ActionResult<EventGroup> GetEventGroup(Guid id)
		{

			return _EventGroupRepository.Get(id);
		}

		[Route("", Name = "GetEventGroups"), HttpGet]
		public ActionResult<IEnumerable<EventGroup>> GetEventGroups()
		{
			return _EventGroupRepository.GetAll();
		}

		[Route("", Name = "AddEventGroup"), HttpPost]
		public ActionResult<Guid> AddEventGroup(EventGroup EventGroup)
		{
			_EventGroupRepository.Add(EventGroup);
			return Ok();
		}

		[Route("", Name = "UpdateEventGroup"), HttpPut]
		public ActionResult UpdateEventGroup(EventGroup EventGroup)
		{
			_EventGroupRepository.Update(EventGroup);
			return Ok();
		}

		[Route("", Name = "DeleteEventGroup"), HttpDelete]
		public ActionResult DeleteEventGroup(Guid idEvent, Guid idGroup)
		{
			_EventGroupRepository.Delete(idEvent, idGroup);
			return Ok();
		}

	}
}