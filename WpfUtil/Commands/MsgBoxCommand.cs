using System;
using System.Windows;
using System.Windows.Input;

namespace WpfUtil.Commands
{
    public class MsgBoxCommand : Freezable, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool IsCanExecute
        {
            get { return (bool)GetValue(IsCanExecuteProperty); }
            set { SetValue(IsCanExecuteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCanExecute.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCanExecuteProperty =
            DependencyProperty.Register("IsCanExecute", typeof(bool), typeof(MsgBoxCommand), new PropertyMetadata(true));
        
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MsgBoxCommand), new PropertyMetadata(CommandConstant.NullCommand));
        
        public bool CanExecute(object parameter)
            => IsCanExecute;

        public void Execute(object parameter)
        {
            if (parameter is MsgBoxCommandParameter obj)
            {
                var result = MessageBox.Show(obj.Text, obj.Caption, obj.Button, obj.Image, MessageBoxResult.OK, obj.Options);

                if (Command?.CanExecute(result) ?? false)
                {
                    Command.Execute(result);
                }
            }
            else
            {
                string msg = parameter?.ToString() ?? string.Empty;
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new MsgBoxCommand();
        }
    }
}
