using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class EventTypeService : BaseService
	{
		public EventTypeService() : base() { }
		public List<EventType> GetAllEventTypes()
		{
			string url = "eventType";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<EventType>>().Result;
		}

		public Guid AddEventType(EventType eventType)//добавление пользователя
		{
			var content = GetContent(eventType);
			string url = "eventType";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public Guid UpdateEventType(EventType eventType)//обновление информации о ползователе
		{
			var content = GetContent(eventType);
			string url = "eventType";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteEventType(Guid id)//удаление пользователя
		{
			string url = "eventType/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}