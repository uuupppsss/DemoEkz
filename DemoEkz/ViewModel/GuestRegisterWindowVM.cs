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

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                Signal();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                Signal();
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                Signal();
            }
        }

        private DateTime _checkInDate;
        public DateTime CheckInDate
        {
            get => _checkInDate;
            set
            {
                _checkInDate = value;
                Signal();
                UpdateTotalPrice(); 
            }
        }

        private DateTime _checkOutDate;    
        public DateTime CheckOutDate
        {
            get => _checkOutDate;
            set
            {
                _checkOutDate = value;
                Signal();
                UpdateTotalPrice(); 
            }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            private set
            {
                _totalPrice = value;
                Signal();
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
             set
            {
                _price = value;
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
            Price = service.CurrentRoom.Price;

			CreateReservation = new CustomCommand(() =>
			{
				service.CreateNewReservation(new GuestRegisterDTO()
				{

				});
			});
		}

        private void UpdateTotalPrice()
        {
            TimeSpan diff = CheckInDate - CheckOutDate;
            int days_between=(int)diff.TotalDays;
            TotalPrice = days_between * Price;
        }

    }
}
