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
    public class HomeworkForGroupController : ControllerBase
    {
		private HomeworkForGroupRepository _HomeworkForGroupRepository;//репоизторий для операций с базой

		public HomeworkForGroupController(HomeworkForGroupRepository HomeworkForGroupRepository)
		{
			_HomeworkForGroupRepository = HomeworkForGroupRepository;
		}

		[Route("{id}", Name = "GetHomeworkForGroup"), HttpGet]
		public ActionResult<HomeworkForGroup> GetHomeworkForGroup(Guid id)
		{

			return _HomeworkForGroupRepository.Get(id);
		}

		[Route("", Name = "GetHomeworkForGroups"), HttpGet]
		public ActionResult<IEnumerable<HomeworkForGroup>> GetHomeworkForGroups()
		{
			return _HomeworkForGroupRepository.GetAll();
		}

		[Route("", Name = "AddHomeworkForGroup"), HttpPost]
		public ActionResult AddHomeworkForGroup(HomeworkForGroup HomeworkForGroup)
		{
			_HomeworkForGroupRepository.Add(HomeworkForGroup);
			return Ok();
		}

		[Route("", Name = "UpdateHomeworkForGroup"), HttpPut]
		public ActionResult UpdateHomeworkForGroup(HomeworkForGroup HomeworkForGroup)
		{
			_HomeworkForGroupRepository.Update(HomeworkForGroup);
			return Ok();
		}

		[Route("", Name = "DeleteHomeworkForGroup"), HttpDelete]
		public ActionResult DeleteHomeworkForGroup(Guid idHomework, Guid idGroup)
		{
			_HomeworkForGroupRepository.Delete(idHomework, idGroup);
			return Ok();
		}
	}
}