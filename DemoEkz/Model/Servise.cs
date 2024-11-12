using DemoEkzApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoEkz
{
    public class Servise
    {
        private readonly HttpClient client;
        static Servise instance;
        public UserDTO CurrentUser;
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
            UserDTO answer = null;
            string arg = JsonSerializer.Serialize(user);
            var responce = await client.GetAsync($"UserService/Autorization?user={arg}");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                answer = await responce.Content.ReadFromJsonAsync<UserDTO>();  
            }
            CurrentUser = answer;
        }

        public async void FirstAutorization(UserDTO user, string newpassword)
        {
            UserDTO answer = null;
            string arg = JsonSerializer.Serialize(user);
            var responce = await client.GetAsync($"UserService/FirstAutorization?user={arg}&newPassword={newpassword}");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                answer = await responce.Content.ReadFromJsonAsync<UserDTO>();
            }
            CurrentUser = answer;
        }

        public async Task<bool> IfUserAutorized(string user_login)
        {
            var responce = await client.GetAsync($"UserService/IfUserExist?user_login={user_login}");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            else return false;
        }

        public async Task<bool> IfUserExist(string user_login)
        {
            var responce = await client.GetAsync($"UserService/IfUserExist?user_login={user_login}");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            else return false;

        }

        public async Task<bool> IfPasswordMatch(UserDTO user)
        {
            string arg = JsonSerializer.Serialize(user);
            var responce = await client.GetAsync($"UserService/IfUserExist?user={arg}");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<bool>();
            }
            else return false;
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

    }
}
