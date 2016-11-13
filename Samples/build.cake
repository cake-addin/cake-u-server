#r "../Cake.UServer/bin/Debug/Cake.UServer.dll"

using Cake.UServer;

Task("Start").Does(() => {
    UServer(10000);
});

Task("Start-Web").Does(() => {
    UServer(new UServerSettings{
        Port = 11000,
        Path = "./Web"
    });
});


var target = Argument("target", "Default");
RunTarget(target);