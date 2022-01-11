using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class RatingHomeworkService : BaseService
	{
		public RatingHomeworkService() : base() { }
		public List<RatingHomework> GetAllRatingHomeworks()
		{
			string url = "ratingHomework";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<RatingHomework>>().Result;
		}

		public Guid AddRatingHomework(RatingHomework ratingHomework)//добавление 
		{
			var content = GetContent(ratingHomework);
			string url = "ratingHomework";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}

		public void DeleteRatingHomework(Guid idUser, Guid idHomework)//удаление 
		{

			string url = "ratingHomework?idUser=" + idUser.ToString() + "&idHomework=" + idHomework.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}

		public Guid UpdateRatingHomework(RatingHomework ratingHomework)//обновление информации о ползователе
		{
			var content = GetContent(ratingHomework);
			string url = "ratingHomework";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
	}
}