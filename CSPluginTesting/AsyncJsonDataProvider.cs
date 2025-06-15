using System;
using System.Threading.Tasks;

namespace CSPlugin
{
    public class AsyncJsonDataProvider
    {
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
    }
}