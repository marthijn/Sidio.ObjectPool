# Sidio.ObjectPool
Extension and helper methods for [Microsoft.Extensions.ObjectPool](https://www.nuget.org/packages/microsoft.extensions.objectpool/).

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
services.AddObjectPoolService(provider => provider.Create(new MyCustomPolicy()));
```