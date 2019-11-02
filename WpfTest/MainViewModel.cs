using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUtil.Commands;
using WpfUtil.Data;

namespace WpfTest
{
    public class MainViewModel
    {
        public INotifyCollection<string> Items { get; } = NotifyCollection<string>.Create();
        public ICommand AddItemCommand { get; }

        public MainViewModel()
        {
            AddItemCommand = ActionCommand.Create(parameter =>
            {
                Items.Add(DateTime.Now.ToString());
            });
        }
    }
}
