using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using WpfUtil.Data;

namespace WpfUtil.Behaviors
{
    /// <summary>
    /// ListBoxコントロールでDeleteキーが押下された際に、項目を削除する機能
    /// </summary>
    public class ListBoxDeleteKey : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewKeyDown += OnKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.PreviewKeyDown -= OnKeyDown;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete)
            {
                return;
            }

            ListBox listBox = sender as ListBox;
            if (null == sender)
            {
                return;
            }

            var source = listBox.ItemsSource;
            var items = listBox.SelectedItems.Cast<object>().ToArray();
            listBox.UnselectAll();
            if (null != source)
            {
                // 処理概要
                // ICollection<T>.Remove(item)
                // を動的に実行する
                var sourceType = source.GetType();
                var generic = sourceType.GenericTypeArguments[0];
                var method = typeof(ICollection<>).MakeGenericType(generic).GetMethod("Remove");

                foreach (var item in items)
                {
                    if (item is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }

                    var callExpression = Expression.Call(
                        Expression.Constant(source), 
                        method,
                        Expression.Constant(item));
                    var fun = Expression.Lambda(callExpression);
                    fun.Compile().DynamicInvoke();
                }
            }
            else
            {
                foreach (var item in items)
                {
                    listBox.Items.Remove(item);
                }
            }
        }
    }
}
