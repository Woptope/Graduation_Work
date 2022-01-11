using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
	public class EventGroupService : BaseService
	{
		public EventGroupService() : base() { }
		public List<EventGroup> GetAllEventGroups()
		{
			string url = "eventGroup";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<EventGroup>>().Result;
		}

		public Guid AddEventGroup(EventGroup eventGroup)//добавление 
		{
			var content = GetContent(eventGroup);
			string url = "eventGroup";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}

		public void DeleteEventGroup(Guid idEvent, Guid idGroup)//удаление 
		{

			string url = "eventGroup?idEvent=" + idEvent.ToString() + "&idGroup=" + idGroup.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}