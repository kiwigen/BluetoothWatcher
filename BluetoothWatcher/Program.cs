using BluetoothWatcher;



var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options => options.ServiceName = "BluetoothWatcher");
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();