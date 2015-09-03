using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Game.Common.Helpers
{
    public static class DelayHelper
    {
        public static void Delay(int miliSeconds)
        {
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                         new Action(() => Thread.Sleep(miliSeconds)));
        }
    }
}
