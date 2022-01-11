using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Client.Services
{
    public class AccountService : BaseService
    {
        public AccountService() : base() { }
        public List<Account> GetAllAccounts()
        {
            string url = "account";
            var response = Client.GetAsync(url).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<List<Account>>().Result;
        }

        public Guid AddAccount(Account account)//добавление пользователя
        {
            var content = GetContent(account);
            string url = "account";
            var response = Client.PostAsync(url, content).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Guid>().Result;
        }
        public Guid UpdateAccount(Account account)//обновление информации о ползователе
        {
            var content = GetContent(account);
            string url = "account";
            var response = Client.PutAsync(url, content).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Guid>().Result;
        }
        public void DeleteAccount(Guid id)//удаление пользователя
        {
            string url = "account/" + id.ToString();
            var response = Client.DeleteAsync(url).Result;

            response.EnsureSuccessStatusCode();
        }
    }
}