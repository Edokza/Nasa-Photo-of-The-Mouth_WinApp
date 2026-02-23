using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NasaPhoto_WinApp.Wpf.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object?, Task> _execute;

        public RelayCommand(Func<object?, Task> execute)
        {
            _execute = execute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public async void Execute(object? parameter)
        {
            await _execute(parameter);
        }
    }
}
