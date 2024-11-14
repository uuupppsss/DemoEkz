using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DemoEkz.Model;
using DemoEkz.View;

namespace DemoEkz.ViewModel
{
    public class FirstAutorizationWindowVM:BaseVM
    {
        private Servise service;

        private string login;

        public string Login
        {
            get => login; 
            set { login = value; }
        }

        private string pwd;

        public string Pwd
        {
            get => pwd; 
            set 
            {
                pwd = value;
                Signal();
            }
        }

        private string repeat_pwd;

        public string Repeat_Pwd
        {
            get => repeat_pwd;
            set
            {
                repeat_pwd = value;
                Signal();
            }
        }

        public CustomCommand SaveChangesCommand { get; set; }

        public FirstAutorizationWindowVM()
        {
            Pwd = pwd_box.Password;
            Repeat_Pwd = repeat_pwd_box.Password;
            service=Servise.Instance;
            Login=service.CurrentUser.Login;

            if (service.CurrentUser == null)
            {
                MessageBox.Show("Всё сломалось");
                return;
            }

            SaveChangesCommand = new CustomCommand(async() =>
            {
                if (Pwd == Repeat_Pwd)
                {
                    await service.FirstAutorization(new UserDTO()
                    {
                        Login = Login,
                        Password=Pwd
                    });
                    if (service.CurrentUser != null)
                    {
                        MessageBox.Show($"Добро пожаловать, {service.CurrentUser.Login}");
                        GuestView win = new GuestView();
                        win.Show();
                        Window thiswin = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.DataContext == this);
                        thiswin?.Close();
                    }
                    else
                    {
                        MessageBox.Show("Что-то пошло не так :(");
                        return;
                    }
                }
                else MessageBox.Show("Пароли не совпадают, попробуйте ещё разок");
            });
        }


        PasswordBox pwd_box;
        PasswordBox repeat_pwd_box;
        internal void SetPassBoxes(PasswordBox pwd_box, PasswordBox repeat_pwd_box)
        {
            this.pwd_box = pwd_box;
            this.repeat_pwd_box = repeat_pwd_box;
        }
    }
}
