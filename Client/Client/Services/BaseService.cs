using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Client.Services
{
    public class BaseService
    {
        protected HttpClient Client;
		public BaseService()//конструктор, настраивает сервис
		{
			Client = new HttpClient();
			Client.BaseAddress = new Uri("http://localhost:53312/api/");
			Client.DefaultRequestHeaders.Accept.Clear();
			Client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}
		protected StringContent GetContent(Object data)//получение данных из класса в правильном формате для отправки
		{
			return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
		}

	}
}