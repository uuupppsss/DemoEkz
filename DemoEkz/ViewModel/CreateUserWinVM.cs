using DemoEkz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DemoEkz.ViewModel
{
    public class CreateUserWinVM:BaseVM
    {
        private Servise service;
		private string _login;

		public string Login
		{
			get => _login; 
			set 
			{
				_login = value;
				Signal();
			}
		}

        private string _pwd;

        public string Pwd
        {
            get => _pwd;
            set
            {
                _pwd = value;
                Signal();
            }
        }

        public CustomCommand CreateNewUserCommand { get; set; }

        public CreateUserWinVM()
        {
            service=Servise.Instance;

            CreateNewUserCommand = new CustomCommand(() =>
            {
                if (Login == null && Pwd == null)
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }
                else
                {
                    service.CreateNewUser(new UserDTO()
                    {
                        Login=Login,
                        Password=Pwd,
                        IsAutorized=false,
                        RoleId=2
                    });
                }
            });
        }

    }
}
