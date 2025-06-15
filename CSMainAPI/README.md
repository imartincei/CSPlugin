# CSMainAPI

A plugin framework for CSMain applications that provides standardized interfaces and callback mechanisms for creating async plugins.

## Installation

Install the CSMainAPI package via NuGet:

```bash
dotnet add package CSMainAPI
```

Or via Package Manager Console:
```
Install-Package CSMainAPI
```

## Overview

CSMainAPI enables developers to create plugins for CSMain applications with a standardized, interface-driven approach. The framework supports both parameterless and parameterized async plugin execution patterns.

## Quick Start

### 1. Choose Your Plugin Type

**Option A: Plugin without parameters**
```csharp
using CSMainAPI;

public class MyPlugin : ICSPlugin
{
    public async Task ExecutePlugin()
    {
        // Your plugin logic here
        await Task.Delay(1000); // Simulate work
        
        string[][] result = new string[][]
        {
            new string[] { "Status", "Success", "Complete" },
            new string[] { "Data", "Generated", DateTime.Now.ToString() }
        };
        
        // Send result back to CSMain (simplified syntax)
        SendResult(result);
    }
}
```

**Option B: Plugin with JSON parameters**
```csharp
using CSMainAPI;

public class MyPluginWithParams : ICSPluginWithParams
{
    public async Task ExecutePlugin(string jsonData)
    {
        // Process the JSON input
        await Task.Delay(2000); // Simulate processing
        
        string[][] result = new string[][]
        {
            new string[] { "Input", "Processed", jsonData },
            new string[] { "Length", jsonData.Length.ToString(), "chars" },
            new string[] { "Timestamp", DateTime.Now.ToString(), "processed" }
        };
        
        // Send result back to CSMain (simplified syntax)
        SendResult(result);
    }
}
```

### 2. Build Your Plugin

Compile your plugin to a .dll file:

```bash
dotnet build
```

### 3. Deploy

Place your plugin .dll in the CSMain application's Plugins folder. The CSMainAPI dependency will be automatically resolved.

## Interfaces

### ICSPlugin
For plugins that execute without parameters:
```csharp
public interface ICSPlugin
{
    Task ExecutePlugin();
}
```

### ICSPluginWithParams
For plugins that accept JSON parameters:
```csharp
public interface ICSPluginWithParams
{
    Task ExecutePlugin(string jsonData);
}
```

## API Methods

### SendResult(string[][] data)
Sends result data back to the CSMain application:
- **Parameter:** `data` - A jagged string array containing the result data
- **Usage:** Call this method to return results from your plugin to CSMain
- **Note:** No namespace prefix required due to global static using

### Initialize(Action<string[][]> callback)
*Used internally by CSMain - not needed in plugins*

## Best Practices

1. **Choose One Interface:** Implement either `ICSPlugin` OR `ICSPluginWithParams`, not both
2. **Async Operations:** Use async/await for I/O operations, network calls, or long-running tasks
3. **Error Handling:** Wrap your plugin logic in try-catch blocks
4. **Result Format:** Structure your results as meaningful string arrays
5. **Resource Cleanup:** Dispose of resources properly in your async methods

## Example Project Structure

```
MyPlugin/
├── MyPlugin.csproj
├── MyPlugin.cs
└── bin/Debug/net8.0/
    └── MyPlugin.dll
```

**MyPlugin.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <OutputType>Library</OutputType>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="CSMainAPI" Version="1.0.3" />
  </ItemGroup>
</Project>
```

## Advanced Usage

### Custom Result Structures
```csharp
// Status information
string[][] statusResult = new string[][]
{
    new string[] { "Operation", "Status", "Details" },
    new string[] { "Database", "Connected", "localhost:5432" },
    new string[] { "API", "Ready", "v2.1.0" }
};

// Data processing results
string[][] dataResult = new string[][]
{
    new string[] { "Records", "Processed", "1250" },
    new string[] { "Errors", "Found", "3" },
    new string[] { "Duration", "Elapsed", "00:02:15" }
};
```

### Error Handling
```csharp
public async Task ExecutePlugin()
{
    try
    {
        // Your plugin logic
        var result = await ProcessDataAsync();
        SendResult(result);
    }
    catch (Exception ex)
    {
        string[][] errorResult = new string[][]
        {
            new string[] { "Error", "Exception", ex.Message },
            new string[] { "Type", ex.GetType().Name, "Failed" }
        };
        SendResult(errorResult);
    }
}
```

## Requirements

- .NET 8.0 or later
- CSMain application with plugin support

## Support

For issues, questions, or contributions, please visit the [GitHub repository](https://github.com/imartincei/CSPlugin).

## License

This project is licensed under the MIT License.