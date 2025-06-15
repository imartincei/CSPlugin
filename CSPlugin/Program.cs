using System;

namespace CSPlugin
{
    public class DataProvider
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
        
        public static async Task<string[][]> GetResultAsync()
        {
            // Simulate async work
            await Task.Delay(1000);
            
            return new string[][]
            {
                new string[] { "AsyncData1", "AsyncData2", "AsyncData3" },
                new string[] { "AsyncValue1", "AsyncValue2", "AsyncValue3" },
                new string[] { "AsyncItem1", "AsyncItem2", "AsyncItem3" }
            };
        }
        
        public static async Task<string[][]> GetResultAsync(string jsonData)
        {
            // Simulate async work
            await Task.Delay(5000);
            
            return new string[][]
            {
                new string[] { "AsyncWithJSON", "Received", jsonData },
                new string[] { "Length", jsonData.Length.ToString(), "chars" },
                new string[] { "Timestamp", DateTime.Now.ToString(), "processed" }
            };
        }
        
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
