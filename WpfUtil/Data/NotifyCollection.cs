using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Windows.Threading;

namespace WpfUtil.Data
{
    public class NotifyCollection<T> : RealProxy
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private Dispatcher CurrentDispatcher { get; } = null;
        private ObservableCollection<T> Collection { get; } = new ObservableCollection<T>();

        public NotifyCollection() : base(typeof(INotifyCollection<T>))
        {
            CurrentDispatcher = Dispatcher.CurrentDispatcher;
            Collection.CollectionChanged += (sender, e) => CollectionChanged?.Invoke(sender, e);
        }

        public override IMessage Invoke(IMessage msg)
        {
            try
            {
                IMethodMessage mm = msg as IMethodMessage;
                IMethodCallMessage methodCall = (IMethodCallMessage)msg;
                if (mm.MethodName == "GetType")
                {
                    return new ReturnMessage(
                        typeof(INotifyCollection<T>),
                        null, 0, mm.LogicalCallContext,
                        (IMethodCallMessage)msg);
                }

                MethodInfo method = (MethodInfo)mm.MethodBase;
                object[] args = mm.Args;

                object ret = CurrentDispatcher.Invoke(() =>
                {
                    return method.Invoke(Collection, methodCall.InArgs);
                });

                return new ReturnMessage(
                    ret, null, 0, mm.LogicalCallContext, (IMethodCallMessage)msg);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static INotifyCollection<T> Create()
        {
            return (INotifyCollection<T>) new NotifyCollection<T>().GetTransparentProxy();
        }
    }
}
