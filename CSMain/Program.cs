using System.Reflection;
using System.Text.Json;

if (args.Length == 0)
{
    Console.WriteLine("Usage: CSMain <dll-filename> [json-data|json-file]");
    Console.WriteLine("Example: CSMain CSPlugin.dll");
    Console.WriteLine("Example: CSMain CSPlugin.dll '{\"key\":\"value\"}'");
    Console.WriteLine("Example: CSMain CSPlugin.dll sample.json");
    return;
}

string dllPath = args[0];
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

if (!File.Exists(dllPath))
{
    Console.WriteLine($"Error: File '{dllPath}' not found.");
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
    
    // Find types that have a GetResult method (sync or async)
    foreach (Type type in assembly.GetTypes())
    {
        MethodInfo? getResultMethod = null;
        
        // Look for methods with different parameter signatures
        if (jsonData != null)
        {
            // First try to find methods that accept a string parameter (for JSON)
            getResultMethod = type.GetMethod("GetResultAsync", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null) ??
                             type.GetMethod("GetResult", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);
        }
        
        // Fallback to parameterless methods
        if (getResultMethod == null)
        {
            getResultMethod = type.GetMethod("GetResultAsync", BindingFlags.Public | BindingFlags.Static, null, Type.EmptyTypes, null) ??
                             type.GetMethod("GetResult", BindingFlags.Public | BindingFlags.Static, null, Type.EmptyTypes, null);
        }
        
        if (getResultMethod != null)
        {
            Console.WriteLine($"Found GetResult method in {type.FullName}");
            
            // Prepare parameters
            object?[] parameters = Array.Empty<object>();
            if (getResultMethod.GetParameters().Length > 0)
            {
                if (jsonData != null)
                {
                    Console.WriteLine("Passing JSON data to method");
                    parameters = new object?[] { jsonData };
                }
                else
                {
                    Console.WriteLine("Method expects parameters but none provided");
                    continue;
                }
            }
            
            // Check if the method is async
            bool isAsync = getResultMethod.ReturnType.IsGenericType && 
                          getResultMethod.ReturnType.GetGenericTypeDefinition() == typeof(Task<>);
            
            if (isAsync)
            {
                Console.WriteLine("Method is async, awaiting result...");
                
                // Invoke the async method
                dynamic? task = getResultMethod.Invoke(null, parameters);
                
                if (task != null)
                {
                    // Wait for the task to complete and get the result
                    await task;
                    object? result = task.Result;
                    
                    // Display the result
                    DisplayResult(result);
                }
            }
            else
            {
                Console.WriteLine("Method is synchronous, executing...");
                
                // Invoke the synchronous method
                object? result = getResultMethod.Invoke(null, parameters);
                
                // Display the result
                DisplayResult(result);
            }
            
            return;
        }
    }
    
    Console.WriteLine("No GetResult method found in the assembly.");
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
