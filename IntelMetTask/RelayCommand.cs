using System;
using System.Windows.Input;

namespace IntelMetTask
{
    public class RelayCommand : ICommand
    {
        private readonly Action<string> _execute;
        private readonly Func<string, bool> _canExecute;
 
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
 
        public RelayCommand(Action<string> execute, Func<string, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
 
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((string)parameter);
        }
 
        public void Execute(object parameter)
        {
            _execute((string)parameter);
        }
    }
}