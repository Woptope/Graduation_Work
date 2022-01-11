using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class AccountTypeService : BaseService
    {
		public AccountTypeService() : base() { }
		public List<AccountType> GetAllAccountTypes()
		{
			string url = "accounttype";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<AccountType>>().Result;
		}

		public AccountType GetAccountType(Guid id)
		{
			string url = "accounttype/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<AccountType>().Result;
		}
	}
}