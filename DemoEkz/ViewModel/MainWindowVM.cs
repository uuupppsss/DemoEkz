using DemoEkz.Model;
using DemoEkz.View;
using DemoEkzApi.Model;
using System.Windows;
using System.Windows.Controls;

namespace DemoEkz.ViewModel
{
    public class MainWindowVM:BaseVM
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

		public CustomCommand SignInCommand { get; set; }

        public MainWindowVM()
        {
			service = Servise.Instance;
			SignInCommand = new CustomCommand(async() =>
			{
				Pwd = pwd_box.Password;
				bool ifUserExist = await service.IfUserExist(Login);
				if (ifUserExist)
				{
					bool ifPasswordMatch = await service.IfPasswordMatch(new UserDTO { Login = Login, Password = Pwd });
					if (ifPasswordMatch)
					{
						bool isautorized = await service.IfUserAutorized(Login);
						if (isautorized)
						{
							service.Autorization(new UserDTO() { Login = Login, Password = Pwd });
							if (service.CurrentUser != null)
							{
                                MessageBox.Show($"Добро пожаловать, {service.CurrentUser.Login}");

                            }
						}
						else
						{
							MessageBox.Show("Нужно сменить пароль");
							FirstAutorizationWindow window = new FirstAutorizationWindow();
							window.ShowDialog();
						}
					}
					else MessageBox.Show("Неверный пароль");
				}
				else MessageBox.Show("Пользователь не найден");
				
			});
		}

        PasswordBox pwd_box;
        internal void SetPassBox(PasswordBox pwd_box)
        {
            this.pwd_box = pwd_box;
        }
    }
}
