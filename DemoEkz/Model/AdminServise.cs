using DemoEkzApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DemoEkz.Model
{
    public class AdminServise
    {
        HttpClient client;
        public AdminServise()
        {
            client = new HttpClient { BaseAddress = new Uri("http://localhost:5199/api/") };
        }

        public async Task<List<GuestRegisterDTO>> GetReservationsList()
        {
            var responce = await client.GetAsync("RervationService/GetReservationsList");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<List<GuestRegisterDTO>>();
            }
            else return null;
        }

        public async void CreateNewUser(UserDTO user)
        {
            string arg = JsonSerializer.Serialize(user);
            var responce = await client.PostAsync("UserService/CreateNewUser", new StringContent(arg, Encoding.UTF8, "application/json"));
            responce.EnsureSuccessStatusCode();
        }

       public async Task<List<CleaningDTO>> GetCleaningsList()
       {
            var responce = await client.GetAsync("UserService/CreateNewUser");
            if (responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await responce.Content.ReadFromJsonAsync<List<CleaningDTO>>();
            }
            else return null;
       }

        public async void RemoveUser(int id)
        {
            var responce = await client.GetAsync($"UserService/CreateNewUser?user_id={id}");
            responce.EnsureSuccessStatusCode();
        }
    }
}
