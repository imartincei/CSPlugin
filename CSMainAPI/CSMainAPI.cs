using System;
using System.Threading.Tasks;

namespace CSMainAPI
{
    /// <summary>
    /// Interface for plugins that execute without parameters
    /// </summary>
    public interface ICSPlugin
    {
        /// <summary>
        /// Plugin entry point without parameters
        /// </summary>
        Task ExecutePlugin();
    }

    /// <summary>
    /// Interface for plugins that execute with parameters
    /// </summary>
    public interface ICSPluginWithParams
    {
        /// <summary>
        /// Plugin entry point with JSON parameters
        /// </summary>
        /// <param name="jsonData">JSON input data</param>
        Task ExecutePlugin(string jsonData);
    }

    /// <summary>
    /// Simple API for plugins to send results back to CSMain
    /// </summary>
    public static class CSMainAPI
    {
        /// <summary>
        /// Callback action to send results back to CSMain
        /// </summary>
        private static Action<string[][]>? _resultCallback;

        /// <summary>
        /// Initializes the API with a callback function
        /// </summary>
        /// <param name="resultCallback">Function to handle plugin results</param>
        public static void Initialize(Action<string[][]> resultCallback)
        {
            _resultCallback = resultCallback;
        }

        /// <summary>
        /// Sends result data back to CSMain
        /// </summary>
        /// <param name="data">The result data to send</param>
        public static void SendResult(string[][] data)
        {
            if (_resultCallback == null)
            {
                throw new InvalidOperationException("CSMainAPI not initialized. Call Initialize() first.");
            }

            _resultCallback(data);
        }

        /// <summary>
        /// Gets the standard entry point method name for plugins
        /// </summary>
        public static string GetEntryPointName() => "ExecutePlugin";

        /// <summary>
        /// Gets the standard entry point method name for plugins with parameters
        /// </summary>
        public static string GetEntryPointWithParamsName() => "ExecutePlugin";
    }
}