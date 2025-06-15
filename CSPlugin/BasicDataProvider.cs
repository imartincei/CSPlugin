using System;

namespace CSPlugin
{
    public class BasicDataProvider
    {
        public static string[][] GetResult()
        {
            return new string[][]
            {
                new string[] { "Data1", "Data2", "Data3" },
                new string[] { "Value1", "Value2", "Value3" },
                new string[] { "Item1", "Item2", "Item3" }
            };
        }
    }
}