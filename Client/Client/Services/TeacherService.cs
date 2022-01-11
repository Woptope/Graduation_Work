using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class TeacherService : BaseService
	{
		public TeacherService() : base() { }
		public List<Teacher> GetAllTeachers()
		{
			string url = "teacher";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<Teacher>>().Result;
		}

		public Teacher GetTeacher(Guid id)
		{
			string url = "teacher/" + id.ToString(); ;
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Teacher>().Result;
		}

		public Guid AddTeacher(Teacher teacher)//добавление пользователя
		{
			var content = GetContent(teacher);
			string url = "teacher";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public Guid UpdateTeacher(Teacher teacher)//обновление информации о ползователе
		{
			var content = GetContent(teacher);
			string url = "teacher";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteTeacher(Guid id)//удаление пользователя
		{
			string url = "teacher/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}