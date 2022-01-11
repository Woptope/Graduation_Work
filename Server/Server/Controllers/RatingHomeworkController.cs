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
    public class RatingHomeworkController : ControllerBase
	{
		private RatingHomeworkRepository _ratingHomeworkRepository;//репоизторий для операций с базой

		public RatingHomeworkController(RatingHomeworkRepository ratingHomeworkRepository)
		{
			_ratingHomeworkRepository = ratingHomeworkRepository;
		}

		[Route("{id}", Name = "GetRatingHomework"), HttpGet]
		public ActionResult<RatingHomework> GetRatingHomework(Guid id)
		{

			return _ratingHomeworkRepository.Get(id);
		}

		[Route("", Name = "GetRatingHomeworks"), HttpGet]
		public ActionResult<IEnumerable<RatingHomework>> GetRatingHomeworks()
		{
			return _ratingHomeworkRepository.GetAll();
		}

		[Route("", Name = "AddRatingHomework"), HttpPost]
		public ActionResult AddRatingHomework(RatingHomework ratingHomework)
		{
			_ratingHomeworkRepository.Add(ratingHomework);
			return Ok();
		}

		[Route("", Name = "UpdateRatingHomework"), HttpPut]
		public ActionResult UpdateChildInGroup(RatingHomework ratingHomework)
		{
			_ratingHomeworkRepository.Update(ratingHomework);
			return Ok();
		}

		[Route("", Name = "DeleteRatingHomework"), HttpDelete]
		public ActionResult DeleteRatingHomework(Guid idUser, Guid idHomework)
		{
			_ratingHomeworkRepository.Delete(idUser, idHomework);
			return Ok();
		}
	}
}