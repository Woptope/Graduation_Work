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
    public class RatingEventController : ControllerBase
    {
		private RatingEventRepository _ratingEventRepository;//репоизторий для операций с базой

		public RatingEventController(RatingEventRepository ratingEventRepository)
		{
			_ratingEventRepository = ratingEventRepository;
		}

		[Route("{id}", Name = "GetRatingEvent"), HttpGet]
		public ActionResult<RatingEvent> GetRatingEvent(Guid id)
		{
			return _ratingEventRepository.Get(id);
		}

		[Route("", Name = "GetRatingEvents"), HttpGet]
		public ActionResult<IEnumerable<RatingEvent>> GetRatingEvents()
		{
			return _ratingEventRepository.GetAll();
		}

		[Route("", Name = "AddRatingEvent"), HttpPost]
		public ActionResult AddRatingEvent(RatingEvent ratingEvent)
		{
			_ratingEventRepository.Add(ratingEvent);
			return Ok();
		}

		[Route("", Name = "UpdateRatingEvent"), HttpPut]
		public ActionResult UpdateChildInGroup(RatingEvent ratingEvent)
		{
			_ratingEventRepository.Update(ratingEvent);
			return Ok();
		}

		[Route("", Name = "DeleteRatingEvent"), HttpDelete]
		public ActionResult DeleteRatingEvent(Guid idUser, Guid idEvent)
		{
			_ratingEventRepository.Delete(idUser, idEvent);
			return Ok();
		}
	}
}