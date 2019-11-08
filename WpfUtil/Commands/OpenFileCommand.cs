using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace WpfUtil.Commands
{
    public class OpenFileCommand : Freezable, ICommand
    {
        private static OpenFileDialog Dialog { get; } = new OpenFileDialog();

        public event EventHandler CanExecuteChanged;
        
        /// <summary>実行可能か</summary>
        public bool IsCanExecute
        {
            get { return (bool)GetValue(IsCanExecuteProperty); }
            set { SetValue(IsCanExecuteProperty, value); CanExecuteChanged?.Invoke(this, new EventArgs()); }
        }

        // Using a DependencyProperty as the backing store for IsCanExecute.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCanExecuteProperty =
            DependencyProperty.Register("IsCanExecute", typeof(bool), typeof(OpenFileCommand), new PropertyMetadata(true));
        
        /// <summary>複数選択できるか</summary>
        public bool IsMultiSelect
        {
            get { return (bool)GetValue(IsMultiSelectProperty); }
            set { SetValue(IsMultiSelectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMultiSelect.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMultiSelectProperty =
            DependencyProperty.Register("IsMultiSelect", typeof(bool), typeof(OpenFileCommand), new PropertyMetadata(false));

        /// <summary>ファイル選択ダイアログ初期ディレクトリ</summary>
        public string InitialDirectory
        {
            get { return (string)GetValue(InitialDirectoryProperty); }
            set { SetValue(InitialDirectoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialDirectory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialDirectoryProperty =
            DependencyProperty.Register("InitialDirectory", typeof(string), typeof(OpenFileCommand), new PropertyMetadata(Environment.CurrentDirectory));
        
        /// <summary>選択可能ファイルフィルタ</summary>
        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Filter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(OpenFileCommand), new PropertyMetadata("すべてのファイル(*.*)|*.*"));
        
        public Window Owner
        {
            get { return (Window)GetValue(OwnerProperty); }
            set { SetValue(OwnerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Owner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OwnerProperty =
            DependencyProperty.Register("Owner", typeof(Window), typeof(OpenFileCommand), new PropertyMetadata(null));
        
        /// <summary>ファイルが選択されたときに実行するコマンド。コマンドパラメータはファイル一覧(string[])</summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(OpenFileCommand), new PropertyMetadata(null));
        
        public bool CanExecute(object parameter)
            => IsCanExecute;

        public void Execute(object parameter)
        {
            Dialog.InitialDirectory = InitialDirectory;
            Dialog.Filter = Filter;
            Dialog.Multiselect = IsMultiSelect;

            if ((null == Owner ? Dialog.ShowDialog() : Dialog.ShowDialog(Owner)) ?? false)
            {
                InitialDirectory = Path.GetDirectoryName(Dialog.FileName);

                if (Command?.CanExecute(Dialog.FileNames) ?? false)
                {
                    Command.Execute(Dialog.FileNames);
                }
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new OpenFileCommand();
        }
    }
}
