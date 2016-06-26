## Cake.UServer

Serving static files with [uHttpSharp](https://github.com/raistlinthewiz/uhttpsharp).

## Install

```
#addin "nuget:?package=Cake.UServer"
```

## Start server

```csharp
Task("server")
    .Does(() => {
        var settings = new UServerSettings {
            Path = "./",
            Port = 8080
        };
        UServer(settings);
    });
```

## License

- MIT