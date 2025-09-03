# Sidio.ObjectPool
This package provides an `IObjectPoolService<T>` interface and implementation that wraps an [`ObjectPool<T>`](https://www.nuget.org/packages/microsoft.extensions.objectpool/).

Key features:
- Simplify the usage of object pools
- Integrate with dependency injection
- Provide a `Borrow` method that returns an `IDisposable` wrapper around the pooled object, so it is automatically returned to the pool when disposed
- Provide a default implementation for `StringBuilder` objects
- Allow easy registration of custom object pools
- Support .NET standard 2.0 and .NET 8 or higher applications

[![build](https://github.com/marthijn/Sidio.ObjectPool/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.ObjectPool/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/Sidio.ObjectPool)](https://www.nuget.org/packages/Sidio.ObjectPool/)
[![Coverage Status](https://coveralls.io/repos/github/marthijn/Sidio.ObjectPool/badge.svg?branch=main)](https://coveralls.io/github/marthijn/Sidio.ObjectPool?branch=main)

## Usage (StringBuilder)
Service registration:
```csharp
services.AddStringBuilderObjectPool();
```

Usage:
```csharp
public class MyClass
{
    private readonly IObjectPoolService<StringBuilder> _stringBuilderPoolService;
    
    public MyClass(IObjectPoolService<StringBuilder> stringBuilderPoolService)
    {
        _stringBuilderPool = stringBuilderPoolService;
    }

    public void Example1()
    {
        var sb = _stringBuilderPoolService.Get();
        try
        {
            sb.Append("Hello, ");
            sb.Append("world!");
            Console.WriteLine(sb.ToString());
        }
        finally
        {
            _stringBuilderPoolService.Return(sb);
        }
    }
    
    public void Example2()
    {
        using var borrowedStringBuilder = _stringBuilderPoolService.Borrow();
        borrowedStringBuilder.Instance.Append("Hello, ");
        borrowedStringBuilder.Instance.Append("world!");
        Console.WriteLine(sb.Instance.ToString());
    }
}
```

## Usage (custom types)
```csharp
// Define a custom builder and object pool policy
public sealed class MyCustomPolicy : PooledObjectPolicy<MyCustomBuilder> {...}

// Register the object pool service
services.AddObjectPoolService((objectPoolProvider, serviceProvider) => objectPoolProvider.Create(new MyCustomPolicy()));

// Use the object pool service
public class MyClass
{
    public MyClass(IObjectPoolService<MyCustomBuilder> myCustomBuilderPoolService) {...}
}
```