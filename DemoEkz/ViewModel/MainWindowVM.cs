

using DemoEkz.Model;
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
			SignInCommand = new CustomCommand(() =>
			{
				Pwd = pwd_box.Password;

			});
		}

        PasswordBox pwd_box;
        internal void SetPassBox(PasswordBox pwd_box)
        {
            this.pwd_box = pwd_box;
        }
    }
}
