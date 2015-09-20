using System;

namespace SampleWebBotClient.Helpers
{
    public static class EnumValueHelper<T>
    {
        public static T RandomEnumValue()
        {
            var value = Enum.GetValues(typeof (T));
            return (T)value.GetValue(new Random(DateTime.Now.Millisecond).Next(value.Length));
        }
    }
}