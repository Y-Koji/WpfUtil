using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace WpfUtil.Behaviors
{
    public class DragDrop : Behavior<FrameworkElement>
    {
        public bool IsFile
        {
            get { return (bool)GetValue(IsFileProperty); }
            set { SetValue(IsFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for File.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFileProperty =
            DependencyProperty.Register("IsFile", typeof(bool), typeof(DragDrop), new PropertyMetadata(true));
        
        public bool IsFolder
        {
            get { return (bool)GetValue(IsFolderProperty); }
            set { SetValue(IsFolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Folder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFolderProperty =
            DependencyProperty.Register("IsFolder", typeof(bool), typeof(DragDrop), new PropertyMetadata(true));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(DragDrop), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewDragOver += OnPreviewDragOver;
            this.AssociatedObject.Drop += OnDrop;
            this.AssociatedObject.AllowDrop = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.PreviewDragOver += OnPreviewDragOver;
            this.AssociatedObject.Drop -= OnDrop;
        }

        private void OnPreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files.All(Directory.Exists) && !IsFolder)
                {
                    // ドラッグ対象がすべてフォルダ && フォルダは含まない場合
                    e.Effects = DragDropEffects.None;

                    goto EXIT;
                }

                if (files.All(File.Exists) && !IsFile)
                {
                    // ドラッグ対象がすべてファイル && フォルダは含まない場合
                    e.Effects = DragDropEffects.None;

                    goto EXIT;
                }

                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            EXIT:;
            e.Handled = true;
        }
        
        private void OnDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (null == files)
            {
                return;
            }

            IList<string> items = new List<string>();
            if (IsFile)
            {
                files.Where(File.Exists).ToList().ForEach(items.Add);
            }

            if (IsFolder)
            {
                files.Where(Directory.Exists).ToList().ForEach(items.Add);
            }

            if (Command?.CanExecute(items) ?? false)
            {
                if (Command.CanExecute(items))
                {
                    Command.Execute(items);
                }
            }
        }
    }
}
