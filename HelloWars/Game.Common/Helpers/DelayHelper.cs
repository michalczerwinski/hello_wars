using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Common.Helpers
{
    public static class DelayHelper
    {
        public static void Delay(int miliSeconds)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                    new Action(() => Thread.Sleep(miliSeconds)));
            }
            catch
            {
                //DoNothing
            }
        }

        public static async Task DelayAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}
