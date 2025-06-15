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
            CSMainAPI.SendResult(resultData);
        }
    }

    /// <summary>
    /// Sample plugin that uses CSMainAPI to send results (with parameters)
    /// </summary>
    public class MyPluginWithParams : ICSPluginWithParams
    {
        /// <summary>
        /// Entry point for plugin with JSON parameters
        /// </summary>
        /// <param name="jsonData">JSON input data</param>
        public async Task ExecutePlugin(string jsonData)
        {
            // Simulate some async work
            await Task.Delay(2000);

            // Process the JSON data and prepare result
            string[][] resultData = new string[][]
            {
                new string[] { "MyPluginWithParams", "WithParams", "Success" },
                new string[] { "Received", jsonData, "data" },
                new string[] { "Length", jsonData.Length.ToString(), "chars" },
                new string[] { "Processed", DateTime.Now.ToString(), "timestamp" }
            };

            // Send result back to CSMain using the API
            CSMainAPI.SendResult(resultData);
        }
    }
}