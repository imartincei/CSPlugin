using System;
using System.Threading.Tasks;

namespace CSPlugin
{
    public class DataManager
    {
        /// <summary>
        /// Gets basic data synchronously without any parameters
        /// </summary>
        /// <returns>A jagged string array containing basic data</returns>
        public static string[][] GetBasicData()
        {
            return new string[][]
            {
                new string[] { "Data1", "Data2", "Data3" },
                new string[] { "Value1", "Value2", "Value3" },
                new string[] { "Item1", "Item2", "Item3" }
            };
        }

        /// <summary>
        /// Gets data asynchronously without any parameters
        /// </summary>
        /// <returns>A task returning a jagged string array containing async data</returns>
        public static async Task<string[][]> GetBasicDataAsync()
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

        /// <summary>
        /// Gets data synchronously with JSON input
        /// </summary>
        /// <param name="jsonData">JSON data to process</param>
        /// <returns>A jagged string array containing processed JSON data</returns>
        public static string[][] GetJsonData(string jsonData)
        {
            return new string[][]
            {
                new string[] { "SyncWithJSON", "Received", jsonData },
                new string[] { "Length", jsonData.Length.ToString(), "chars" },
                new string[] { "Timestamp", DateTime.Now.ToString(), "processed" }
            };
        }

        /// <summary>
        /// Gets data asynchronously with JSON input
        /// </summary>
        /// <param name="jsonData">JSON data to process</param>
        /// <returns>A task returning a jagged string array containing processed JSON data</returns>
        public static async Task<string[][]> GetJsonDataAsync(string jsonData)
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

        /// <summary>
        /// Gets all available data types in a single call
        /// </summary>
        /// <param name="jsonData">Optional JSON data for JSON-based methods</param>
        /// <returns>A comprehensive data structure containing all data types</returns>
        public static DataResult GetAllData(string? jsonData = null)
        {
            var result = new DataResult
            {
                BasicData = GetBasicData(),
                JsonData = jsonData != null ? GetJsonData(jsonData) : null
            };

            return result;
        }

        /// <summary>
        /// Gets all available data types asynchronously in a single call
        /// </summary>
        /// <param name="jsonData">Optional JSON data for JSON-based methods</param>
        /// <returns>A task returning a comprehensive data structure containing all data types</returns>
        public static async Task<AsyncDataResult> GetAllDataAsync(string? jsonData = null)
        {
            var basicDataTask = GetBasicDataAsync();
            var jsonDataTask = jsonData != null ? GetJsonDataAsync(jsonData) : Task.FromResult<string[][]?>(null);

            await Task.WhenAll(basicDataTask, jsonDataTask);

            var result = new AsyncDataResult
            {
                BasicAsyncData = await basicDataTask,
                JsonAsyncData = await jsonDataTask
            };

            return result;
        }
    }

    /// <summary>
    /// Data structure for synchronous data results
    /// </summary>
    public class DataResult
    {
        public string[][]? BasicData { get; set; }
        public string[][]? JsonData { get; set; }
    }

    /// <summary>
    /// Data structure for asynchronous data results
    /// </summary>
    public class AsyncDataResult
    {
        public string[][]? BasicAsyncData { get; set; }
        public string[][]? JsonAsyncData { get; set; }
    }
}