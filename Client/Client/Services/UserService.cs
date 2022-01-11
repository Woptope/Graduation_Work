using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class UserService : BaseService
	{
		public UserService() : base() { }
		public List<User> GetAllUsers()
		{
			string url = "user";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<User>>().Result;
		}

		public User GetUser(Guid id)
		{
			string url = "user/" + id.ToString(); ;
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<User>().Result;
		}
		public Guid AddUser(User user)//добавление пользователя
		{
			var content = GetContent(user);
			string url = "user";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public Guid UpdateUser(User user)//обновление информации о ползователе
		{
			var content = GetContent(user);
			string url = "user";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteUser(Guid id)//удаление пользователя
		{
			string url = "user/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}