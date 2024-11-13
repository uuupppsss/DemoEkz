using DemoEkz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DemoEkz.ViewModel
{
    public class AddNewCleaningVM:BaseVM
    {
        private Servise service;
        private int _roomId;

        public int RoomId
        {
            get => _roomId; 
            set 
            {
                _roomId = value;
                Signal();
            }
        }

        private string _cleaner;

        public string Cleaner
        {
            get => _cleaner;
            set
            {
                _cleaner = value;
                Signal();
            }
        }

        private DateTime _date;

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                Signal();
            }
        }

        public CustomCommand CreateCleaning {  get; set; }

        public AddNewCleaningVM()
        {
            service=Servise.Instance;
            if (service.CurrentRoom == null)
            {
                MessageBox.Show("Всё сломалось");
                return;
            }

            RoomId=service.CurrentRoom.Room_id;
            Date=DateTime.Now;
            
            CreateCleaning = new CustomCommand(() =>
            {
                if (Cleaner != null )
                {
                    service.CreateNewCleaning(new CleaningDTO()
                    {
                        RoomId = RoomId,
                        Cleaner = Cleaner,
                        Date = Date,
                        IsDone = false
                    });
                    
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
            });
        }
    }
}
