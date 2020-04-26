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
        public HttpClient HttpClient { get; private set; }

        public HttpPostClient()
        {
            this.HttpClient = new HttpClient();
        }

        public HttpPostClient(bool enableLogging)
        {
            if (enableLogging)
            {
                this.HttpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            }
            else
            {
                this.HttpClient = new HttpClient();
            }
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
            Uri url = new Uri(BaseUrl + "/" + controller + "/" + action);
            this.HttpClient.DefaultRequestHeaders.Accept.Clear();
            this.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, action);

            var dataAsString = JsonConvert.SerializeObject(obj);
            var content = new StringContent(dataAsString,Encoding.UTF8, "application/json");

            HttpResponseMessage response = await this.HttpClient.PostAsync(url, content);
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
