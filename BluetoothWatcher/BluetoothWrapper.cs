using InTheHand.Bluetooth;//https://github.com/inthehand/32feet -- License in ./Licenses

namespace BluetoothWatcher;

public class BluetoothWrapper(BluetoothDevice device)
{
    private bool _triggerFullLevelMessage;

    public async Task<byte> GetBatterylevel()
    {
        var service = await device.Gatt.GetPrimaryServiceAsync(GattServiceUuids.Battery);
        var chara = await service.GetCharacteristicAsync(BluetoothUuid.GetCharacteristic("battery_level"));
        var batteryValue = await chara.ReadValueAsync();
        return batteryValue[0];
    }

    public void MakeLowBatteryWarningToast()
    { 
        _triggerFullLevelMessage = true;
        Toast.MakeToast($"{device.Name} Battery Level Warning", $"{device.Name} battery level is below 25%!");
    }

    public void MakeFullBatteryInfoToast()
    {
        if (_triggerFullLevelMessage)
        {
            Toast.MakeToast($"{device.Name} Battery Level Info", $"{device.Name} battery level is 95% or higher!");
            _triggerFullLevelMessage = false;
        }
    }
}