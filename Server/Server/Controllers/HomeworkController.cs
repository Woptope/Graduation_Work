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
    public class HomeworkController : ControllerBase
    {
		private HomeworkRepository _HomeworkRepository;//репоизторий для операций с базой

		public HomeworkController(HomeworkRepository HomeworkRepository)
		{
			_HomeworkRepository = HomeworkRepository;
		}

		[Route("{id}", Name = "GetHomework"), HttpGet]
		public ActionResult<Homework> GetHomework(Guid id)
		{

			return _HomeworkRepository.Get(id);
		}

		[Route("", Name = "GetAllHomework"), HttpGet]
		public ActionResult<IEnumerable<Homework>> GetHomework()
		{
			return _HomeworkRepository.GetAll();
		}

		[Route("", Name = "AddHomework"), HttpPost]
		public ActionResult<Guid> AddHomework(Homework Homework)
		{
			return _HomeworkRepository.Add(Homework);

		}

		[Route("", Name = "UpdateHomework"), HttpPut]
		public ActionResult UpdateHomework(Homework Homework)
		{
			_HomeworkRepository.Update(Homework);
			return Ok();
		}

		[Route("{id}", Name = "DeleteHomework"), HttpDelete]
		public ActionResult DeleteHomework(Guid id)
		{
			_HomeworkRepository.Delete(id);
			return Ok();
		}

		[Route("like/{id}", Name = "LikeHomework"), HttpGet]
		public ActionResult LikeHomework(Guid id)
		{
			_HomeworkRepository.AddLike(id);
			return Ok();
		}

		[Route("deleteLike/{id}", Name = "DeleteLikeHomework"), HttpGet]
		public ActionResult DeleteLikeHomework(Guid id)
		{
			_HomeworkRepository.DeleteLike(id);
			return Ok();
		}


		[Route("dislike/{id}", Name = "DisLikeHomework"), HttpGet]
		public ActionResult DisLikeEvent(Guid id)
		{
			_HomeworkRepository.AddDisLike(id);
			return Ok();
		}

		[Route("deleteDisLike/{id}", Name = "DeleteDisLikeHomework"), HttpGet]
		public ActionResult DeleteDisLikeHomework(Guid id)
		{
			_HomeworkRepository.DeleteDisLike(id);
			return Ok();
		}
	}
}