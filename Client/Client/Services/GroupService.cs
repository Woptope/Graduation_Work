using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class GroupService : BaseService
	{
		public GroupService() : base() { }
		public List<Group> GetAllGroups()
		{
			string url = "group";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<Group>>().Result;
		}

		public Group GetGroup(Guid id)
		{
			string url = "group/" + id.ToString(); ;
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Group>().Result;
		}


		public Guid AddGroup(Group group)//добавление пользователя
		{
			var content = GetContent(group);
			string url = "group";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public Guid UpdateGroup(Group group)//обновление информации о ползователе
		{
			var content = GetContent(group);
			string url = "group";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteGroup(Guid id)//удаление пользователя
		{
			string url = "group/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}