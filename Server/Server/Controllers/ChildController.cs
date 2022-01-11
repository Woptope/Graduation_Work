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
    public class ChildController : ControllerBase
    {
		private ChildRepository _ChildRepository;//репоизторий для операций с базой

		public ChildController(ChildRepository ChildRepository)
		{
			_ChildRepository = ChildRepository;
		}

		[Route("{id}", Name = "GetChild"), HttpGet]
		public ActionResult<Child> GetChild(Guid id)
		{
			return _ChildRepository.Get(id);
		}

		[Route("getForParent/{parentId}", Name = "GetAllChildrenForParent"), HttpGet]
		public ActionResult<IEnumerable<Child>> GetAllChildrenForParent(Guid parentId)
		{
			return _ChildRepository.GetAllChildrenForParent(parentId);
		}

		[Route("", Name = "GetChildren"), HttpGet]
		public ActionResult<IEnumerable<Child>> GetChildren()
		{

			return _ChildRepository.GetAll();
		}

		[Route("", Name = "AddChild"), HttpPost]
		public ActionResult AddChild(Child Child)
		{
			_ChildRepository.Add(Child);
			return Ok();
		}

		[Route("", Name = "UpdateChild"), HttpPut]
		public ActionResult UpdateChild(Child Child)
		{
			_ChildRepository.Update(Child);
			return Ok();
		}

		[Route("{id}", Name = "DeleteChild"), HttpDelete]
		public ActionResult DeleteChild(Guid id)
		{
			_ChildRepository.Delete(id);
			return Ok();
		}
	}
}