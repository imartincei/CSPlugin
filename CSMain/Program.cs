using System.Reflection;
using System.Text.Json;
using CSMainAPI;

Console.WriteLine("CSMain - Interactive Plugin Loader");
Console.WriteLine("===================================");

// Get DLL filename
string dllFilename;
if (args.Length > 0)
{
    dllFilename = args[0];
}
else
{
    Console.Write("Enter DLL filename: ");
    string? input = Console.ReadLine();
    dllFilename = string.IsNullOrWhiteSpace(input) ? "no input" : input;
}

// Construct full path to DLL in Plugins folder
string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins", dllFilename);
Console.WriteLine($"Looking for plugin at: {dllPath}");

// Get JSON data
string? jsonData = null;
if (args.Length > 1)
{
    string jsonInput = args[1];
    
    // Check if the input is a file path
    if (File.Exists(jsonInput))
    {
        Console.WriteLine($"Loading JSON from file: {jsonInput}");
        jsonData = File.ReadAllText(jsonInput);
    }
    else
    {
        // Treat as inline JSON data
        jsonData = jsonInput;
    }
}
else
{
    Console.Write("Enter JSON data (or press Enter to skip): ");
    string? input = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(input))
    {
        // Check if the input is a file path
        if (File.Exists(input))
        {
            Console.WriteLine($"Loading JSON from file: {input}");
            jsonData = File.ReadAllText(input);
        }
        else
        {
            // Treat as inline JSON data
            jsonData = input;
        }
    }
}

if (!File.Exists(dllPath))
{
    Console.WriteLine($"Error: Plugin '{dllFilename}' not found in Plugins folder.");
    return;
}

if (jsonData != null)
{
    Console.WriteLine($"JSON data provided: {jsonData}");
}

try
{
    // Load the assembly
    Assembly assembly = Assembly.LoadFrom(dllPath);
    
    // Set up CSMainAPI callback to capture results
    string[][]? pluginResult = null;
    Initialize(result => {
        pluginResult = result;
    });
    
    // Find CSMainAPI plugins
    foreach (Type type in assembly.GetTypes())
    {
        // Check if type implements ICSPlugin or ICSPluginWithParams
        bool implementsICSPlugin = typeof(ICSPlugin).IsAssignableFrom(type);
        bool implementsICSPluginWithParams = typeof(ICSPluginWithParams).IsAssignableFrom(type);
        
        if (implementsICSPlugin || implementsICSPluginWithParams)
        {
            Console.WriteLine($"Found CSMainAPI plugin: {type.FullName}");
            
            // Error if plugin implements both interfaces
            if (implementsICSPlugin && implementsICSPluginWithParams)
            {
                Console.WriteLine($"Error: Plugin {type.FullName} implements both ICSPlugin and ICSPluginWithParams. A plugin must implement only one interface.");
                return;
            }
            
            // Create instance of the plugin
            object? pluginInstance = Activator.CreateInstance(type);
            if (pluginInstance == null)
            {
                Console.WriteLine($"Failed to create instance of {type.FullName}");
                continue;
            }
            
            // Execute the appropriate method
            if (implementsICSPluginWithParams)
            {
                if (jsonData != null)
                {
                    Console.WriteLine("Executing CSMainAPI plugin with parameters...");
                    var pluginWithParams = (ICSPluginWithParams)pluginInstance;
                    await pluginWithParams.ExecutePlugin(jsonData);
                }
                else
                {
                    Console.WriteLine("Error: Plugin implements ICSPluginWithParams but no JSON data provided");
                    return;
                }
            }
            else if (implementsICSPlugin)
            {
                Console.WriteLine("Executing CSMainAPI plugin without parameters...");
                var plugin = (ICSPlugin)pluginInstance;
                await plugin.ExecutePlugin();
            }
            
            // Display result from callback
            if (pluginResult != null)
            {
                DisplayResult(pluginResult);
                return;
            }
            else
            {
                Console.WriteLine("Plugin executed but no result was sent via CSMainAPI.SendResult");
                return;
            }
        }
    }
    
    Console.WriteLine("No CSMainAPI plugins found in the assembly.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error loading or executing assembly: {ex.Message}");
}

static void DisplayResult(object? result)
{
    if (result is string[][] stringArray)
    {
        Console.WriteLine("Result:");
        for (int i = 0; i < stringArray.Length; i++)
        {
            Console.WriteLine($"Row {i}: [{string.Join(", ", stringArray[i])}]");
        }
    }
    else
    {
        Console.WriteLine($"Result: {result}");
    }
}
