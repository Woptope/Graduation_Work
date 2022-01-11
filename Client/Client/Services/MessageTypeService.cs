using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class MessageTypeService : BaseService
	{
		public MessageTypeService() : base() { }
		public List<MessageType> GetAllMessageTypes()
		{
			string url = "messageType";
			var response = Client.GetAsync(url).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<List<MessageType>>().Result;
		}

		public Guid AddMessageType(MessageType messageType)//добавление пользователя
		{
			var content = GetContent(messageType);
			string url = "messageType";
			var response = Client.PostAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public Guid UpdateMessageType(MessageType messageType)//обновление информации о ползователе
		{
			var content = GetContent(messageType);
			string url = "messageType";
			var response = Client.PutAsync(url, content).Result;

			response.EnsureSuccessStatusCode();

			return response.Content.ReadAsAsync<Guid>().Result;
		}
		public void DeleteMessageType(Guid id)//удаление пользователя
		{
			string url = "messageType/" + id.ToString();
			var response = Client.DeleteAsync(url).Result;

			response.EnsureSuccessStatusCode();
		}
	}
}