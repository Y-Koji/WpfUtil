using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUtil.Data
{
    public interface INotifyCollection<T> : ICollection<T>, INotifyCollectionChanged
    {
    }
}
