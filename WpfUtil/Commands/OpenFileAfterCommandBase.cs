using System;
using System.Windows.Input;

namespace WpfUtil.Commands
{
    /// <summary>
    /// FileOpenCommandでファイル選択された場合に実行するコマンドのベースクラス
    /// </summary>
    public abstract class OpenFileAfterCommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter is string[] files)
            {
                return CanExecute(files);
            }

            return false;
        }

        public void Execute(object parameter)
        {
            if (parameter is string[] files)
            {
                Execute(files);
            }
        }

        protected abstract bool CanExecute(string[] files);
        protected abstract bool Execute(string[] files);
    }
}
