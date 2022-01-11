using Client.Models.ServerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Client.Services
{
    public class ChildService : BaseService
    {
        public ChildService() : base() { }
        public List<Child> GetAllChildren()
        {
            string url = "child";
            var response = Client.GetAsync(url).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<List<Child>>().Result;
        }


        public List<Child> GetAllChildrenForParent(Guid parentId)
        {
            string url = "child/getForParent/" + parentId.ToString();

            var response = Client.GetAsync(url).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<List<Child>>().Result;
        }

        public Guid AddChild(Child child)//добавление пользователя
        {
            var content = GetContent(child);
            string url = "child";
            var response = Client.PostAsync(url, content).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Guid>().Result;
        }

        public Guid UpdateChild(Child child)//обновление информации о ползователе
        {
            var content = GetContent(child);
            string url = "child";
            var response = Client.PutAsync(url, content).Result;

            response.EnsureSuccessStatusCode();

            return response.Content.ReadAsAsync<Guid>().Result;
        }
        public void DeleteChild(Guid id)//удаление пользователя
        {
            string url = "child/" + id.ToString();
            var response = Client.DeleteAsync(url).Result;

            response.EnsureSuccessStatusCode();
        }
    }
}