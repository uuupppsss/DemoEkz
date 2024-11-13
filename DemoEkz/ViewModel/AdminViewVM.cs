using DemoEkz.Model;
using DemoEkz.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private RoomView _selectedRoom;

        public RoomView SelectedRoom
        {
            get => _selectedRoom; 
            set 
            {
                _selectedRoom = value;
                Signal();
            }
        }

        public CustomCommand<RoomView> UpdateRoom { get; set; }
        public CustomCommand<RoomView> CreateNewReservation { get; set; }
        public CustomCommand<RoomView> SetNewCleaning { get; set; }

        public AdminViewVM()
        {
            service=Servise.Instance;
            GetRoomsList();

            UpdateRoom = new CustomCommand<RoomView>(r => 
            {
                if (r != null)
                {
                    service.CurrentRoom = r;
                }
            });

            CreateNewReservation = new CustomCommand<RoomView>(r =>
            {
                if (r != null)
                {
                    service.CurrentRoom = r;
                }
            });

            SetNewCleaning = new CustomCommand<RoomView>(r =>
            {
                if (r != null)
                {
                    service.CurrentRoom = r;
                    AddNewCleaning win= new AddNewCleaning();
                    win.Closed += UnsetSelectedRoom;
                    win.ShowDialog();

                }
            });
        }



        private async void GetRoomsList()
        {
            Rooms = await service.GetRoomsViewList();
        }

        private void UnsetSelectedRoom(object sender, EventArgs e)
        {
            service.CurrentRoom = null;
        }
    }
}
