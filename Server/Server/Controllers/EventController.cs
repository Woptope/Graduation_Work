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
    public class EventController : ControllerBase
    {
		private EventRepository _EventRepository;//репоизторий для операций с базой

		public EventController(EventRepository EventRepository)
		{
			_EventRepository = EventRepository;
		}

		[Route("{id}", Name = "GetEvent"), HttpGet]
		public ActionResult<Event> GetEvent(Guid id)
		{

			return _EventRepository.Get(id);
		}

		[Route("", Name = "GetEvents"), HttpGet]
		public ActionResult<IEnumerable<Event>> GetEvents()
		{
			return _EventRepository.GetAll();
		}

		[Route("", Name = "AddEvent"), HttpPost]
		public ActionResult<Guid> AddEvent(Event Event)
		{
			return _EventRepository.Add(Event);
		}

		[Route("", Name = "UpdateEvent"), HttpPut]
		public ActionResult UpdateEvent(Event Event)
		{
			_EventRepository.Update(Event);
			return Ok();
		}

		[Route("{id}", Name = "DeleteEvent"), HttpDelete]
		public ActionResult DeleteEvent(Guid id)
		{
			_EventRepository.Delete(id);
			return Ok();
		}

		[Route("like/{id}", Name = "LikeEvent"), HttpGet]
		public ActionResult LikeEvent(Guid id)
		{
			_EventRepository.AddLike(id);
			return Ok();
		}

		[Route("deleteLike/{id}", Name = "DeleteLikeEvent"), HttpGet]
		public ActionResult DeleteLikeEvent(Guid id)
		{
			_EventRepository.DeleteLike(id);
			return Ok();
		}


		[Route("dislike/{id}", Name = "DisLikeEvent"), HttpGet]
		public ActionResult DisLikeEvent(Guid id)
		{
			_EventRepository.AddDisLike(id);
			return Ok();
		}

		[Route("deleteDisLike/{id}", Name = "DeleteDisLikeEvent"), HttpGet]
		public ActionResult DeleteDisLikeEvent(Guid id)
		{
			_EventRepository.DeleteDisLike(id);
			return Ok();
		}
	}
}