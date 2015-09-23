using System;

namespace Common.Helpers
{
    public static class EnumValueHelper<T>
    {
        private static Random _rand = new Random(DateTime.Now.Millisecond);

        public static T RandomEnumValue()
        {
            var v = Enum.GetValues(typeof (T));
            return (T)v.GetValue(_rand.Next(v.Length));
        }
    }
}