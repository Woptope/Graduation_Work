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
    public class ChildInGroupController : ControllerBase
    {
		private ChildInGroupRepository _ChildInGroupRepository;//репоизторий для операций с базой

		public ChildInGroupController(ChildInGroupRepository ChildInGroupRepository)
		{
			_ChildInGroupRepository = ChildInGroupRepository;
		}

		[Route("{id}", Name = "GetChildInGroup"), HttpGet]
		public ActionResult<ChildInGroup> GetChildInGroup(Guid id)
		{

			return _ChildInGroupRepository.Get(id);
		}

		[Route("", Name = "GetChildInGroups"), HttpGet]
		public ActionResult<IEnumerable<ChildInGroup>> GetChildInGroups()
		{
			return _ChildInGroupRepository.GetAll();
		}

		[Route("", Name = "AddChildInGroup"), HttpPost]
		public ActionResult AddChildInGroup(ChildInGroup ChildInGroup)
		{
			_ChildInGroupRepository.Add(ChildInGroup);
			return Ok();
		}

		[Route("", Name = "UpdateChildInGroup"), HttpPut]
		public ActionResult UpdateChildInGroup(ChildInGroup ChildInGroup)
		{
			_ChildInGroupRepository.Update(ChildInGroup);
			return Ok();
		}

		[Route("", Name = "DeleteChildInGroup"), HttpDelete]
		public ActionResult DeleteChildInGroup(Guid idChild, Guid idGroup)
		{
			_ChildInGroupRepository.Delete(idChild, idGroup);
			return Ok();
		}
	}
}