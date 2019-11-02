using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfUtil.Commands
{
    public class ActionCommand : ICommand
    {
        private Func<object, bool> _CanExecute { get; } = null;
        private Action<object> _Execute { get; } = null;

        public event EventHandler CanExecuteChanged;

        public ActionCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        public bool CanExecute(object parameter)
            => _CanExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter)
            => _Execute?.Invoke(parameter);

        public static ICommand Create(Action<object> execute, Func<object, bool> canExecute = null)
        {
            return new ActionCommand(execute, canExecute);
        }
    }
}
