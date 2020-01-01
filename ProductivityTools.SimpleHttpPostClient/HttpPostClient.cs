using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.SimpleHttpPostClient
{
    public class HttpPostClient
    {
        private string BaseUrl { get; set; }
        private bool Logging { get; set; }

        public HttpPostClient()
        {

        }

        public void EnableLogging(bool enable = true)
        {
            this.Logging = enable;
        }

        public void SetBaseUrl(string url)
        {
            this.BaseUrl = url;
        }

        public async Task<T> Post<T>(string controller, string action)
        {
            return await Post<T>(controller, action, null);
        }

        public async Task<T> Post<T>(string controller, string action, object obj)
        {
            HttpClient client = null;
            if (Logging)
            {
                client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            }
            else
            {
                client = new HttpClient();
            }

            Uri url = new Uri(BaseUrl + "/" + controller + "/" + action);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, action);

            var dataAsString = JsonConvert.SerializeObject(obj);
            var content = new StringContent(dataAsString,Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var resultAsString = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(resultAsString);
                return result;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}
