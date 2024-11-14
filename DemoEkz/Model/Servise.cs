using DemoEkz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace DemoEkz
{
    public class Servise
    {
        private readonly HttpClient client;
        static Servise instance;
        public UserDTO CurrentUser;
        public RoomView CurrentRoom;
        public CleaningDTO CurrentCleaning;
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

        //Юзеры

        public async void CreateNewUser(UserDTO user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("UserService/CreateNewUser", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else MessageBox.Show("Пользователь добавлен успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        public async void FirstAutorization(UserDTO user)
        {
            CurrentUser = null;
            try
            {
                string json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("UserService/FirstAutorization", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                    return;
                }
                else
                {
                    CurrentUser = await response.Content.ReadFromJsonAsync<UserDTO>();
                    MessageBox.Show($"Добро пожаловать, {CurrentUser.Login}");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        public async Task Autorization(UserDTO user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("UserService/Autorization", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else CurrentUser = await response.Content.ReadFromJsonAsync<UserDTO>();              
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        //public async Task<bool> IfUserAutorized(string user_login)
        //{
        //    try
        //    {
        //        var response = await client.GetAsync($"UserService/IfUserAutorized?user_login={user_login}");
        //        if (response.IsSuccessStatusCode)
        //            return await response.Content.ReadFromJsonAsync<bool>();
        //        else
        //        {
        //            MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Всё сломалось: {ex.Message}");
        //        return false ;
        //    }
        //}

        ////public async Task<bool> IfUserExist(string user_login)
        ////{
        ////    try
        ////    {
        ////        var response = await client.GetAsync($"UserService/IfUserExist?user_login={user_login}");
        ////        if (response.IsSuccessStatusCode)
        ////            return await response.Content.ReadFromJsonAsync<bool>();
        ////        else
        ////        {
        ////            MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
        ////            return false;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        MessageBox.Show($"Всё сломалось: {ex.Message}");
        ////        return false;
        ////    }
        ////}

        //public async Task<bool> IfPasswordMatch(UserDTO user)
        //{
        //    try
        //    {
        //        string json = JsonSerializer.Serialize(user);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = await client.PostAsync("UserService/IfPasswordMatch", content);
        //        if (response.IsSuccessStatusCode)
        //            return await response.Content.ReadFromJsonAsync<bool>();
        //        else
        //        {
        //            MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Всё сломалось: {ex.Message}");
        //        return false;
        //    }
        //}

        public async void RemoveUser(int id)
        {
            try
            {
                var response = await client.GetAsync($"UserService/RemoveUser?user_id={id}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else MessageBox.Show("Пользователь удалён успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        public async Task<List<UserDTO>> GetUsersList()
        {
            try
            {
                var response = await client.GetAsync($"UserService/GetUsersList");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                    return null;
                }
                else
                {
                    List<UserDTO> users= await response.Content.ReadFromJsonAsync<List<UserDTO>>();
                    if (users != null)
                    {
                        List<UserDTO> result = new List<UserDTO>();
                        foreach (var u in users)
                        {
                            if (u.IsAutorized)
                            {
                                result.Add(new UserDTO()
                                {
                                    Id = u.Id,
                                    Login = u.Login,
                                    Password = " "
                                }); ;
                            }
                            else
                            {
                                result.Add(u);
                            }
                        }
                        return result;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
                return null;
            }
        }

        //Брони
        public async void CreateNewReservation(GuestRegisterDTO reservation)
        {
            try
            {
                string json = JsonSerializer.Serialize(reservation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("ReservationService/CreateNewUser", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else MessageBox.Show("Бронь создана успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        public async void UpdateReservation(GuestRegisterDTO reservation)
        {
            try
            {
                string json = JsonSerializer.Serialize(reservation);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("ReservationService/UpdateReservation", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else MessageBox.Show("Бронь обновлена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        public async void RemoveReservation(int id)
        {
            try
            {
                var response = await client.GetAsync($"ReservationService/RemoveReservation?id={id}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else MessageBox.Show("Бронь удалена успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        public async Task<List<GuestRegisterDTO>> GetReservationsList()
        {
            try
            {
                var response = await client.GetAsync($"ReservationService/GetReservationsList");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                    return null;
                }
                else return await response.Content.ReadFromJsonAsync<List<GuestRegisterDTO>>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
                return null;
            }
        }

        //уборки
        public async Task<List<CleaningDTO>> GetCleaningsList()
        {
            try
            {
                var response = await client.GetAsync($"Cleaning/GetCleaningsList");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                    return null;
                }
                else return await response.Content.ReadFromJsonAsync<List<CleaningDTO>>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
                return null;
            }
        }

        public async void CreateNewCleaning(CleaningDTO cleaning)
        {
            try
            {
                string json = JsonSerializer.Serialize(cleaning);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Cleaning/CreateNewCleaning", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else MessageBox.Show("Уборка назначена успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        public async void RemoveCleaning(int id)
        {
            try
            {
                var response = await client.GetAsync($"Cleaning/RemoveCleaning?id={id}");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else MessageBox.Show("Уборка удалена успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        public async void UpdateCleaning(CleaningDTO cleaning)
        {
            try
            {
                string json = JsonSerializer.Serialize(cleaning);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("Cleaning/UpdateCleaning", content);

                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                }
                else MessageBox.Show("Уборка обновлена успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
            }
        }

        //номера
        private async Task<List<RoomDTO>> GetRoomsList()
        {
            try
            {
                var response = await client.GetAsync($"Rooms/GetRoomsList");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                    return null;
                }
                else return await response.Content.ReadFromJsonAsync<List<RoomDTO>>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
                return null;
            }
        }

        private async Task<List<OtchetDTO>> GetRoomsOtchetList()
        {
            try
            {
                var response = await client.GetAsync($"Rooms/GetRoomsOtchetList");
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Что-то пошло не так", $"Ошибка: {response.StatusCode}");
                    return null;
                }
                else return await response.Content.ReadFromJsonAsync<List<OtchetDTO>>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Всё сломалось: {ex.Message}");
                return null;
            }
        }

        public async Task<List<RoomView>> GetRoomsViewList()
        {
            List < OtchetDTO > otchets = await GetRoomsOtchetList();
            List<RoomDTO> rooms = await GetRoomsList();
            List<RoomView> result= new List<RoomView>();
            foreach (var room in rooms)
            {
                var report = otchets.Find(r => r.Room_id ==room.Num);
                if (report != null)
                {
                    result.Add(new RoomView
                    {
                        Floor = room.Floor,
                        Room_id = room.Num,
                        Category = room.Category,
                        Status = report.Status
                    });
                }
            }
            return result;
        }

    }
}
