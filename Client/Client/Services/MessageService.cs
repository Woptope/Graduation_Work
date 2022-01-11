using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class MessageService : BaseService
    {
        public MessageService() : base() { }
        public List<Message> GetAllMessages()
        {
            string url = "message";
            var response = Client.GetAsync(url).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<List<Message>>().Result;
        }
        public List<Message> GetHomeworkMessages(Guid homeworkId)
        {
            string url = "message/homeworkMessages/" + homeworkId.ToString();
            var response = Client.GetAsync(url).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<List<Message>>().Result;
        }

        public List<Message> GetEventMessages(Guid eventId)
        {
            string url = "message/eventMessages/" + eventId.ToString();
            var response = Client.GetAsync(url).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<List<Message>>().Result;
        }

        public Guid AddMessage(Message message)//добавление пользователя
        {
            var content = GetContent(message);
            string url = "message";
            var response = Client.PostAsync(url, content).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Guid>().Result;
        }
        public Guid UpdateMessage(Message message)//обновление информации о ползователе
        {
            var content = GetContent(message);
            string url = "message";
            var response = Client.PutAsync(url, content).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Guid>().Result;
        }
        public void DeleteMessage(Guid id)//удаление пользователя
        {
            string url = "message/" + id.ToString();
            var response = Client.DeleteAsync(url).Result;

            response.EnsureSuccessStatusCode();
        }
    }
}