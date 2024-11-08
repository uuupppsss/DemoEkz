
using DemoEkz.Model;
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
				bool isautorized = await service.IfUserAutorized(Login);
				if (isautorized) MessageBox.Show("Вы авторизованы");
				else MessageBox.Show("Нужно сменить пароль");
			});
		}

        PasswordBox pwd_box;
        internal void SetPassBox(PasswordBox pwd_box)
        {
            this.pwd_box = pwd_box;
        }
    }
}
