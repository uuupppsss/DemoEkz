using DemoEkz.Model;
using DemoEkz.View;
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
			SignInCommand = new CustomCommand(() =>
			{
				Pwd = pwd_box.Password;
                Autorization();
            });
		}

		private async void Autorization()
		{
            await service.Autorization(new UserDTO()
            {
                Login = Login,
                Password = Pwd
            });
                if (service.CurrentUser!=null&&!service.CurrentUser.IsAutorized)
                {
                    FirstAutorizationWindow window = new FirstAutorizationWindow();
                    window.ShowDialog();
                }
                else
                {
                    Window win = null;
                    switch (service.CurrentUser.RoleId)
                    {
                        case 1:
                            win = new AdminView();
                            break;
                        case 2:
                            win = new GuestView();
                            break;
                        default:
                            MessageBox.Show("Всё сломалось");
                            return;

                    }
                    win.Show();
                    Window thiswin = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.DataContext == this);
                    thiswin?.Close();
                }
        }

        PasswordBox pwd_box;
        internal void SetPassBox(PasswordBox pwd_box)
        {
            this.pwd_box = pwd_box;
        }
    }
}
