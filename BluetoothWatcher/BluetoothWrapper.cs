using InTheHand.Bluetooth;
using Microsoft.Toolkit.Uwp.Notifications;

namespace BluetoothWatcher;

public class BluetoothWrapper(BluetoothDevice device)
{
    public async Task<byte> GetBatterylevel()
    {
        var service = await device.Gatt.GetPrimaryServiceAsync(GattServiceUuids.Battery);
        var chara = await service.GetCharacteristicAsync(BluetoothUuid.GetCharacteristic("battery_level"));
        var batteryValue = await chara.ReadValueAsync();
        return batteryValue[0];
    }

    public void MakeLowBatteryWarningToast()=>
        MakeToast($"{device.Name} Battery Level Warning",$"{device.Name} battery level is below 25%!");
        
    public void MakeFullBatteryInfoToast()=>
        MakeToast($"{device.Name} Battery Level Info",$"{device.Name} battery level is 95% or higher!");

    private void MakeToast(string title, string body)
    {
        new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", 9813)
            .AddText(title)
            .AddText(body)
            .Show();
    }
}