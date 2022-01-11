using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class HomeworkTypeService : BaseService
	{
		public HomeworkTypeService() : base() { }
		public List<HomeworkType> GetAllHomeworkTypes()
		{
			string url = "homeworkType";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<HomeworkType>>().Result;
		}

		public Guid AddHomeworkType(HomeworkType homeworkType)//добавление пользователя
		{
			var content = GetContent(homeworkType);
			string url = "homeworkType";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public Guid UpdateHomeworkType(HomeworkType homeworkType)//обновление информации о ползователе
		{
			var content = GetContent(homeworkType);
			string url = "homeworkType";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteHomeworkType(Guid id)//удаление пользователя
		{
			string url = "homeworkType/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}