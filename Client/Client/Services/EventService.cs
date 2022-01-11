using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class EventService : BaseService
    {
		public EventService() : base() { }
		public List<Event> GetAllEvents()
		{
			string url = "event";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<Event>>().Result;
		}

		public Event GetEvent(Guid id)
		{
			string url = "event/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Event>().Result;
		}

		public Guid AddEvent(Event Event)
		{
			var content = GetContent(Event);
			string url = "event";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}

		public Guid UpdateEvent(Event Event)
		{
			var content = GetContent(Event);
			string url = "event";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteEvent(Guid id)
		{
			string url = "event/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public void AddLike(Guid id)//обновление информации о ползователе
		{
			string url = "event/like/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public void DeleteLike(Guid id)//обновление информации о ползователе
		{
			string url = "event/deleteLike/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public void AddDislike(Guid id)//обновление информации о ползователе
		{
			string url = "event/dislike/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public void DeleteDislike(Guid id)//обновление информации о ползователе
		{
			string url = "event/deleteDislike/" + id.ToString();
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}