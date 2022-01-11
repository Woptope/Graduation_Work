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
    public class ParentController : ControllerBase
    {
		private ParentRepository _ParentRepository;//репоизторий для операций с базой

		public ParentController(ParentRepository ParentRepository)
		{
			_ParentRepository = ParentRepository;
		}

		[Route("{id}", Name = "GetParent"), HttpGet]
		public ActionResult<Parent> GetParent(Guid id)
		{
			return _ParentRepository.Get(id);
		}

		[Route("", Name = "GetParents"), HttpGet]
		public ActionResult<IEnumerable<Parent>> GetParents()
		{
			return _ParentRepository.GetAll();
		}

		[Route("", Name = "AddParent"), HttpPost]
		public ActionResult AddParent(Parent Parent)
		{
			_ParentRepository.Add(Parent);
			return Ok();
		}

		[Route("", Name = "UpdateParent"), HttpPut]
		public ActionResult UpdateParent(Parent Parent)
		{
			_ParentRepository.Update(Parent);
			return Ok();
		}

		[Route("{id}", Name = "DeleteParent"), HttpDelete]
		public ActionResult DeleteParent(Guid id)
		{
			_ParentRepository.Delete(id);
			return Ok();
		}
	}
}