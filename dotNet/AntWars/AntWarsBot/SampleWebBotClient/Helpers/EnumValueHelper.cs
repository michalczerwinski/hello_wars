using System;

namespace SampleWebBotClient.Helpers
{
    public static class EnumValueHelper<T>
    {
        private static Random _rand = new Random(DateTime.Now.Millisecond);

        public static T RandomEnumValue()
        {
            var value = Enum.GetValues(typeof(T));
            return (T)value.GetValue(_rand.Next(value.Length));
        }
    }
}