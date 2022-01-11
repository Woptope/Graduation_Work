using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class HomeworkForGroupService : BaseService
	{
		public HomeworkForGroupService() : base() { }
		public List<HomeworkForGroup> GetAllHomeworkForGroups()
		{
			string url = "homeworkForGroup";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<HomeworkForGroup>>().Result;
		}

		public Guid AddHomeworkForGroup(HomeworkForGroup homeworkForGroup)//добавление 
		{
			var content = GetContent(homeworkForGroup);
			string url = "homeworkForGroup";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}

		public void DeleteHomeworkForGroup(Guid idHomework, Guid idGroup)//удаление 
		{

			string url = "homeworkForGroup?idHomework=" + idHomework.ToString() + "&idGroup=" + idGroup.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}