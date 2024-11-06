using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoEkz
{
    public class Servise
    {
        private readonly HttpClient client;
        static Servise instance;
        public static Servise Instance
        {
            get
            {
                if (instance == null) 
                    instance = new Servise();
                return instance;
            }
        }
        public Servise()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5199/api/");
        }

    }
}
