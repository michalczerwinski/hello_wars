using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class DelayHelper
    {
        public static async Task DelayAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}