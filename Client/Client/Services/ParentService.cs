using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class ParentService : BaseService
	{
		public ParentService() : base() { }
		public List<Parent> GetAllParents()
		{
			string url = "parent";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<Parent>>().Result;
		}

		public Parent GetParent(Guid id)
		{
			string url = "parent/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Parent>().Result;
		}

		public Guid AddParent(Parent parent)//добавление пользователя
		{
			var content = GetContent(parent);
			string url = "parent";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public Guid UpdateParent(Parent parent)//обновление информации о ползователе
		{
			var content = GetContent(parent);
			string url = "parent";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteParent(Guid id)//удаление пользователя
		{
			string url = "parent/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}