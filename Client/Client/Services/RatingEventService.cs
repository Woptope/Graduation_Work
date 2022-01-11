using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class RatingEventService : BaseService
	{
		public RatingEventService() : base() { }
		public List<RatingEvent> GetAllRatingEvents()
		{
			string url = "ratingEvent";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<RatingEvent>>().Result;
		}

		public Guid AddRatingEvent(RatingEvent ratingEvent)//добавление 
		{
			var content = GetContent(ratingEvent);
			string url = "ratingEvent";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}

		public void DeleteRatingEvent(Guid idUser, Guid idEvent)//удаление 
		{

			string url = "ratingEvent?idUser=" + idUser.ToString() + "&idEvent=" + idEvent.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public Guid UpdateRatingEvent(RatingEvent ratingEvent)//обновление информации о ползователе
		{
			var content = GetContent(ratingEvent);
			string url = "ratingEvent";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
	}
}