using DemoEkz.Model;
using DemoEkz.View;
using System.Windows;

namespace DemoEkz.ViewModel
{
    public class AdminViewVM:BaseVM
    {
        private Servise service;

        private List<RoomView> _rooms;

        public List<RoomView> Rooms
        {
            get => _rooms; 
            set 
            {
                _rooms = value;
                Signal();
            }
        }

        private List<CleaningDTO> _cleanings;

        public List<CleaningDTO> Cleanings
        {
            get => _cleanings;
            set
            {
                _cleanings = value;
                Signal();
            }
        }

        private List<UserDTO> _users;

        public List<UserDTO> Users
        {
            get => _users;
            set
            {
                _users = value;
                Signal();
            }
        }

        private List<GuestRegisterDTO> _reservations;

        public List<GuestRegisterDTO> Reservations
        {
            get => _reservations;
            set
            {
                _reservations = value;
                Signal();
            }
        }

        public CustomCommand<RoomView> UpdateRoom { get; set; }
        public CustomCommand<RoomView> CreateNewReservation { get; set; }
        public CustomCommand<RoomView> SetNewCleaning { get; set; }

        public CustomCommand<CleaningDTO> UpdateCleaning { get; set; }
        public CustomCommand<CleaningDTO> DeleteCleaning { get; set; }

        public CustomCommand<UserDTO> DeleteUser { get; set; }
        public CustomCommand<GuestRegisterDTO> DeleteReservation {  get; set; }

        public CustomCommand CreateNewUser { get; set; }

        public AdminViewVM()
        {
            service=Servise.Instance;
            GetRoomsList();
            GetCleaningsList();
            GetUsersList();
            GetReservationsList();

            UpdateRoom = new CustomCommand<RoomView>(r => 
            {
                if (r != null)
                {
                    service.CurrentRoom = r;
                    //обновить коллекцию

                }
            });

            CreateNewReservation = new CustomCommand<RoomView>(r =>
            {
                if (r != null)
                {
                    service.CurrentRoom = r;
                    GuestRegisterWindow win= new GuestRegisterWindow();
                    win.Closed += UnsetSelectedRoom;
                    //обновить коллекцию
                    
                    win.ShowDialog();
                }
            });

            SetNewCleaning = new CustomCommand<RoomView>(r =>
            {
                if (r != null)
                {
                    service.CurrentRoom = r;
                    AddNewCleaning win= new AddNewCleaning();
                    win.Closed += UnsetSelectedRoom;
                    //обновить коллекцию
                    win.Closed += UpdateCleaningsList;
                    win.ShowDialog();

                }
            });

            CreateNewUser = new CustomCommand(() =>
            {
                CreateUserWin win= new CreateUserWin();
                //обновить коллекцию
                win.Closed += UpdateUsersList;
                win.Show();
            });

            DeleteUser = new CustomCommand<UserDTO>(u =>
            {
                if (u != null)
                {
                    var ans = MessageBox.Show("Удалить? ", "Точно?", MessageBoxButton.YesNo);
                    if (ans == MessageBoxResult.Yes)
                    {
                        service.RemoveUser(u.Id);
                        //обновить коллекцию
                        GetUsersList();
                    }
                }
            });

            DeleteCleaning = new CustomCommand<CleaningDTO>(c =>
            {
                if (c != null)
                {
                    var ans = MessageBox.Show("Удалить? ", "Точно?", MessageBoxButton.YesNo);
                    if (ans == MessageBoxResult.Yes)
                    {
                        service.RemoveCleaning(c.Id);
                        //обновить коллекцию
                        GetCleaningsList();
                    }
                }
            });

            UpdateCleaning = new CustomCommand<CleaningDTO>(c =>
            {
                if (c != null)
                {
                    service.CurrentCleaning = c;
                    //обновить коллекцию
                }
            });

           DeleteReservation = new CustomCommand<GuestRegisterDTO>(r =>
            {
                if (r != null)
                {
                    var ans = MessageBox.Show("Удалить? ", "Точно?", MessageBoxButton.YesNo);
                    if (ans == MessageBoxResult.Yes)
                    {
                        service.RemoveReservation(r.Id);
                        //обновить коллекцию
                       GetReservationsList();
                    }
                }
            });
        }



        private async void GetRoomsList()
        {
            Rooms = await service.GetRoomsViewList();
        }

        private async void GetCleaningsList()
        {
            Cleanings = await service.GetCleaningsList();
        }

        private async void GetUsersList()
        {
            Users=await service.GetUsersList();
        }

        private async void GetReservationsList()
        {
            Reservations = await service.GetReservationsList();
        }

        private void UnsetSelectedRoom(object sender, EventArgs e)
        {
            service.CurrentRoom = null;
        }

        private void UpdateCleaningsList(object sender, EventArgs e)
        {
            GetCleaningsList();
        }

        private void UpdateUsersList(object sender, EventArgs e)
        {
            GetUsersList();
        }

        private void UpdateRoomsList(object sender, EventArgs e)
        {
            GetRoomsList();
        }
        private void UpdateReservationsList(object sender, EventArgs e)
        {
            GetReservationsList();
        }
    }
}
