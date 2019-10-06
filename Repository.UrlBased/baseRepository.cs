using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Repository.Contracts;
using Repository.Contracts.DTOs;

namespace Repository.UrlBased
{
    public abstract class baseRepository<T>:IRepository<T> where T : baseEntity
    {
        protected readonly string DataUrl;
        protected static WebClient _webClient;

        protected baseRepository()
        {
            DataUrl = ConfigurationManager.AppSettings[typeof(T).Name + "_Url"];
            _webClient = new WebClient();
        }

        public IList<T> GetAll()
        {
            var data = _webClient.DownloadString(DataUrl);
            if (!string.IsNullOrEmpty(data))
            {
                return JsonConvert.DeserializeObject<List<T>>(data);
            }

            return null;
        }

        public T GetById(int id)
        {
            var allData = GetAll();
            if (allData != null)
            {
                return allData.FirstOrDefault(d => d.Id == id);
            }

            return null;
        }
    }
}
