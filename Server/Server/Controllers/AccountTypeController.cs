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
    public class AccountTypeController : ControllerBase
    {
		private AccountTypeRepository _accountTypeRepository;//репоизторий для операций с базой

		public AccountTypeController(AccountTypeRepository accountTypeRepository)
		{
			_accountTypeRepository = accountTypeRepository;
		}

		[Route("{id}", Name = "GetAccountType"), HttpGet]
		public ActionResult<AccountType> GetAccountType(Guid id)
		{

			return _accountTypeRepository.Get(id);
		}

		[Route("", Name = "GetAllAccountTypes"), HttpGet]
		public ActionResult<IEnumerable<AccountType>> GetAllAccountTypes()
		{
			return _accountTypeRepository.GetAll();
		}

		[Route("", Name = "AddAccountType"), HttpPost]
		public ActionResult AddAccountType(AccountType AccountType)
		{
			_accountTypeRepository.Add(AccountType);
			return Ok();
		}

		[Route("", Name = "UpdateAccountType"), HttpPut]
		public ActionResult UpdateAccountType(AccountType AccountType)
		{
			_accountTypeRepository.Update(AccountType);
			return Ok();
		}

		[Route("{id}", Name = "DeleteAccountType"), HttpDelete]
		public ActionResult DeleteAccountType(Guid id)
		{
			_accountTypeRepository.Delete(id);
			return Ok();
		}
	}
}