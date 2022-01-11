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
    public class TeacherController : ControllerBase
    {
		private TeacherRepository _TeacherRepository;//репоизторий для операций с базой

		public TeacherController(TeacherRepository TeacherRepository)
		{
			_TeacherRepository = TeacherRepository;
		}

		[Route("{id}", Name = "GetTeacher"), HttpGet]
		public ActionResult<Teacher> GetTeacher(Guid id)
		{

			return _TeacherRepository.Get(id);
		}

		[Route("", Name = "GetTeachers"), HttpGet]
		public ActionResult<IEnumerable<Teacher>> GetTeachers()
		{
			return _TeacherRepository.GetAll();
		}

		[Route("", Name = "AddTeacher"), HttpPost]
		public ActionResult AddTeacher(Teacher Teacher)
		{
			_TeacherRepository.Add(Teacher);
			return Ok();
		}

		[Route("", Name = "UpdateTeacher"), HttpPut]
		public ActionResult UpdateTeacher(Teacher Teacher)
		{
			_TeacherRepository.Update(Teacher);
			return Ok();
		}

		[Route("{id}", Name = "DeleteTeacher"), HttpDelete]
		public ActionResult DeleteTeacher(Guid id)
		{
			_TeacherRepository.Delete(id);
			return Ok();
		}
	}
}