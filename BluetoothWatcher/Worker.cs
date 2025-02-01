using InTheHand.Bluetooth;

namespace BluetoothWatcher;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private readonly int _taskDelay = 1000 * 60 * 5; //1000 Millisekunden = 1 Sekunde * 60 Sekunden * 5 Minuten

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {                
        var btDevices = (await Bluetooth.GetPairedDevicesAsync())
            .Select(x => new BluetoothWrapper(x)).ToList();
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            try
            {
                foreach (var btDevice in btDevices)
                {
                    if (await btDevice.GetBatterylevel() < 25)
                        btDevice.MakeLowBatteryWarningToast();
                    if(await btDevice.GetBatterylevel() >= 95)
                        btDevice.MakeLowBatteryWarningToast();
                        
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            
            await Task.Delay(_taskDelay, stoppingToken);
        }
    }
}