using System;

namespace SampleWebBotClient.Helpers
{
    public static class EnumValueHelper<T>
    {
        public static T RandomEnumValue()
        {
            var v = Enum.GetValues(typeof (T));
            return (T) v.GetValue(new Random().Next(v.Length));
        }
    }
}