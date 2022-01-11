using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class HomeworkService : BaseService
	{
		public HomeworkService() : base() { }
		public List<Homework> GetAllHomework()
		{
			string url = "homework";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<Homework>>().Result;
		}

		public Homework GetHomework(Guid id)
		{
			string url = "homework/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Homework>().Result;
		}

		public Guid AddHomework(Homework homework)//добавление пользователя
		{
			var content = GetContent(homework);
			string url = "homework";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public Guid UpdateHomework(Homework homework)//обновление информации о ползователе
		{
			var content = GetContent(homework);
			string url = "homework";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteHomework(Guid id)//удаление пользователя
		{
			string url = "homework/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public void AddLike(Guid id)//обновление информации о ползователе
		{
			string url = "homework/like/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public void DeleteLike(Guid id)//обновление информации о ползователе
		{
			string url = "homework/deleteLike/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public void AddDislike(Guid id)//обновление информации о ползователе
		{
			string url = "homework/dislike/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public void DeleteDislike(Guid id)//обновление информации о ползователе
		{
			string url = "homework/deleteDislike/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}