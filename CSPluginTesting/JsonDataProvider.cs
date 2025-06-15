using System;

namespace CSPlugin
{
    public class JsonDataProvider
    {
        public static string[][] GetResult(string jsonData)
        {
            return new string[][]
            {
                new string[] { "SyncWithJSON", "Received", jsonData },
                new string[] { "Length", jsonData.Length.ToString(), "chars" },
                new string[] { "Timestamp", DateTime.Now.ToString(), "processed" }
            };
        }
    }
}