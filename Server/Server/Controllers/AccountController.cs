using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Server.DataBase.Repository;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountRepository _AccountRepository;//репоизторий для операций с базой

        public AccountController(AccountRepository AccountRepository)
        {
            _AccountRepository = AccountRepository;
        }

        [Route("{id}", Name = "GetAccount"), HttpGet]
        public ActionResult<Account> GetAccount(Guid id)
        {

            return _AccountRepository.Get(id);
        }

        [Route("", Name = "GetAccounts"), HttpGet]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            return _AccountRepository.GetAll();
        }

        [Route("", Name = "AddAccount"), HttpPost]
        public ActionResult<Guid> AddAccount(Account Account)
        {
            return _AccountRepository.Add(Account);
        }

        [Route("", Name = "UpdateAccount"), HttpPut]
        public ActionResult UpdateAccount(Account Account)
        {
            _AccountRepository.Update(Account);
            return Ok();
        }

        [Route("{id}", Name = "DeleteAccount"), HttpDelete]
        public ActionResult DeleteAccount(Guid id)
        {
            _AccountRepository.Delete(id);
            return Ok();
        }
    }
}