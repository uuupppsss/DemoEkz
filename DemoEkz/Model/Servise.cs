using DemoEkzApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
            client = new HttpClient{ BaseAddress = new Uri("http://localhost:5199/api/") };
        }

        //работа с юзерами
        public async void CreateNewUser(UserDTO user)
        {
            string arg=JsonSerializer.Serialize(user);
            var responce = await client.PostAsync("UserService/CreateNewUser", new StringContent(arg, Encoding.UTF8, "application/json"));
            responce.EnsureSuccessStatusCode();
        }

        public async void Autorization(UserDTO user)
        {

        }

        //работа с бронями

        public async void CreateNewReservation(GuestRegisterDTO reservation)
        {

        }

        public async void UpdateReservation(GuestRegisterDTO reservation)
        {

        }

        public async void RemoveReservation(GuestRegisterDTO reservation)
        {

        }

        public async Task<List<GuestRegisterDTO>> GetReservationList()
        {

        }
    }
}
