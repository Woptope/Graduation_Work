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
    public class HomeworkTypeController : ControllerBase
    {
		private HomeworkTypeRepository _HomeworkTypeRepository;//репоизторий для операций с базой

		public HomeworkTypeController(HomeworkTypeRepository HomeworkTypeRepository)
		{
			_HomeworkTypeRepository = HomeworkTypeRepository;
		}

		[Route("{id}", Name = "GetHomeworkType"), HttpGet]
		public ActionResult<HomeworkType> GetHomeworkType(Guid id)
		{

			return _HomeworkTypeRepository.Get(id);
		}

		[Route("", Name = "GetHomeworkTypes"), HttpGet]
		public ActionResult<IEnumerable<HomeworkType>> GetHomeworkTypes()
		{
			return _HomeworkTypeRepository.GetAll();
		}

		[Route("", Name = "AddHomeworkType"), HttpPost]
		public ActionResult AddHomeworkType(HomeworkType HomeworkType)
		{
			_HomeworkTypeRepository.Add(HomeworkType);
			return Ok();
		}

		[Route("", Name = "UpdateHomeworkType"), HttpPut]
		public ActionResult UpdateHomeworkType(HomeworkType HomeworkType)
		{
			_HomeworkTypeRepository.Update(HomeworkType);
			return Ok();
		}

		[Route("{id}", Name = "DeleteHomeworkType"), HttpDelete]
		public ActionResult DeleteHomeworkType(Guid id)
		{
			_HomeworkTypeRepository.Delete(id);
			return Ok();
		}
	}
}