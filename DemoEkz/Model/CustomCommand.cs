using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DemoEkz.Model
{
    public class CustomCommand : ICommand
    {

        public Action _Execute;

        public CustomCommand(Action ExecuteMethod)
        {
            _Execute = ExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _Execute();
        }

    }
}
