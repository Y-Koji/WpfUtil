using System;
using System.Windows.Input;

namespace WpfUtil.Commands
{
    public static class CommandConstant
    {
        private class NullCommandImpl : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return false;
            }

            public void Execute(object parameter)
            {

            }
        }

        public static ICommand NullCommand { get; } = new NullCommandImpl();
    }
}
