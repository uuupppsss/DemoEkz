using System.Windows;
using DemoEkz.ViewModel;

namespace DemoEkz.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pwd_box.PasswordChar = '*';
            pwd_box.MaxLength = 50;
            ((MainWindowVM)DataContext).SetPassBox(pwd_box);
        }
    }
}