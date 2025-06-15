using System;
using System.Threading.Tasks;

namespace AsyncPlugin
{
    public class DataProvider
    {
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
    }
}