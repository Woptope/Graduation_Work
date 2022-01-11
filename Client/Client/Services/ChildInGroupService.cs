using System;
using Client.Models.ServerModels;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace Client.Services
{
    public class ChildInGroupService : BaseService
	{
		public ChildInGroupService() : base() { }
		public List<ChildInGroup> GetAllChildInGroups()
		{
			string url = "childInGroup";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<ChildInGroup>>().Result;
		}

		public Guid AddChildInGroup(ChildInGroup childInGroup)//добавление 
		{
			var content = GetContent(childInGroup);
			string url = "childInGroup";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}

		public void DeleteChildInGroup(Guid idChild, Guid idGroup)//удаление 
		{

			string url = "childInGroup?idChild=" + idChild.ToString() + "&idGroup=" + idGroup.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}