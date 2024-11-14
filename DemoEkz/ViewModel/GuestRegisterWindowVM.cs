using DemoEkz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DemoEkz.ViewModel
{
    public class GuestRegisterWindowVM:BaseVM
    {
		private Servise service;


		private int _roomId;

		public int RoomId
		{
			get { return _roomId; }
			set 
			{
				_roomId = value;
				Signal();
			}
		}
		public CustomCommand CreateReservation { get; set; }

        public GuestRegisterWindowVM()
        {
            service = Servise.Instance;
            if (service.CurrentRoom == null)
            {
                MessageBox.Show("Всё сломалось");
                return;
            }

            RoomId = service.CurrentRoom.Room_id;
			CreateReservation = new CustomCommand(() =>
			{
				service.CreateNewReservation(new GuestRegisterDTO()
				{

				});
			});
		}

    }
}
