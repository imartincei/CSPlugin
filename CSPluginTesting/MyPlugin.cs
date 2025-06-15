using System;
using System.Threading.Tasks;
using CSMainAPI;

namespace MyPlugin
{
    /// <summary>
    /// Sample plugin that uses CSMainAPI to send results (without parameters)
    /// </summary>
    public class MyPlugin : ICSPlugin
    {
        /// <summary>
        /// Entry point for plugin without parameters
        /// </summary>
        public async Task ExecutePlugin()
        {
            // Simulate some async work
            await Task.Delay(1000);

            // Prepare result data
            string[][] resultData = new string[][]
            {
                new string[] { "MyPlugin", "NoParams", "Success" },
                new string[] { "Generated", DateTime.Now.ToString(), "data" },
                new string[] { "Using", "CSMainAPI", "Framework" }
            };

            // Send result back to CSMain using the API
            SendResult(resultData);
        }
    }
}