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
    public class GroupController : ControllerBase
    {
		private GroupRepository _GroupRepository;//репоизторий для операций с базой

		public GroupController(GroupRepository GroupRepository)
		{
			_GroupRepository = GroupRepository;
		}

		[Route("{id}", Name = "GetGroup"), HttpGet]
		public ActionResult<Group> GetGroup(Guid id)
		{

			return _GroupRepository.Get(id);
		}

		[Route("", Name = "GetGroups"), HttpGet]
		public ActionResult<IEnumerable<Group>> GetGroups()
		{
			return _GroupRepository.GetAll();
		}

		[Route("", Name = "AddGroup"), HttpPost]
		public ActionResult AddGroup(Group Group)
		{
			_GroupRepository.Add(Group);
			return Ok();
		}

		[Route("", Name = "UpdateGroup"), HttpPut]
		public ActionResult UpdateGroup(Group Group)
		{
			_GroupRepository.Update(Group);
			return Ok();
		}

		[Route("{id}", Name = "DeleteGroup"), HttpDelete]
		public ActionResult DeleteGroup(Guid id)
		{
			_GroupRepository.Delete(id);
			return Ok();
		}
	}
}