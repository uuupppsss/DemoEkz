using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoEkz.Model
{
    public class Client
    {
        private static HttpClient _httpClient;
        public static HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                    _httpClient = new HttpClient
                    {
                        BaseAddress = new Uri("http://localhost:5199/api/")
                    };
                return _httpClient;
            }
        }

        public static void SetToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
