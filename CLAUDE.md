# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Structure

This is a C# solution containing two projects:
- **CSPlugin** - A library project (OutputType: Library) that provides data through the `DataProvider` class
- **CSMain** - A console application project that can consume the library

Both projects target .NET 8.0 with nullable reference types enabled.

## Common Commands

### Build
```bash
dotnet build
```

### Build specific project
```bash
dotnet build CSPlugin.csproj
dotnet build CSMain/CSMain.csproj
```

### Run the console application
```bash
dotnet run --project CSMain/CSMain.csproj
```

### Clean build artifacts
```bash
dotnet clean
```

## Architecture

The main library (`CSPlugin`) exposes a `DataProvider` class with a static `GetResult()` method that returns a jagged string array containing sample data. This appears to be a plugin-style architecture where the library provides data services that can be consumed by applications like the CSMain console app.

## Memories

- Use main instead of master