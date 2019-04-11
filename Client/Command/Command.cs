using System;
using System.Windows.Input;

namespace Client.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> execute)
        {
            _execute = execute;

        }

        Action<object> _execute;
        public Func<bool> CanExecuteCommand { get; set; }

        public void TriggerCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteCommand != null)
                return CanExecuteCommand.Invoke();
            return true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
