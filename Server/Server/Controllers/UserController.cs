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
    public class UserController : ControllerBase
    {
		private UserRepository _UserRepository;//репоизторий для операций с базой

		public UserController(UserRepository UserRepository)
		{
			_UserRepository = UserRepository;
		}

		[Route("{id}", Name = "GetUser"), HttpGet]
		public ActionResult<User> GetUser(Guid id)
		{

			return _UserRepository.Get(id);
		}

		[Route("", Name = "GetUsers"), HttpGet]
		public ActionResult<IEnumerable<User>> GetUsers()
		{
			return _UserRepository.GetAll();
		}

		[Route("", Name = "AddUser"), HttpPost]
		public ActionResult<Guid> AddUser(User User)
		{
			return _UserRepository.Add(User);
		}

		[Route("", Name = "UpdateUser"), HttpPut]
		public ActionResult UpdateUser(User User)
		{
			_UserRepository.Update(User);
			return Ok();
		}

		[Route("{id}", Name = "DeleteUser"), HttpDelete]
		public ActionResult DeleteUser(Guid id)
		{
			_UserRepository.Delete(id);
			return Ok();
		}
	}
}