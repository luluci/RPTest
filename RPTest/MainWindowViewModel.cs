
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace RPTest
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ReactivePropertySlim<bool> IsEnable { get; set; }
        public ReactivePropertySlim<bool> IsEnableSync { get; set; }
        public AsyncReactiveCommand Btn1Command { get; set; }

        public MainWindowViewModel()
        {
            IsEnable = new ReactivePropertySlim<bool>(true);
            IsEnableSync = IsEnable.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                (x) => { return x; },
                (x) => { return !x; }
            );
            Btn1Command = new AsyncReactiveCommand();
            Btn1Command.WithSubscribe(
                async () =>
                {
                    IsEnable.Value = false;
                    await Task.Delay(1000);
                    IsEnable.Value = true;
                }
            );
        }

    }
}
