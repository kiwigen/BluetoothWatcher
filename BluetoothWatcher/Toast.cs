using Microsoft.Toolkit.Uwp.Notifications;

namespace BluetoothWatcher;

public class Toast
{
    public static void MakeToast(string title, string body)
    {
        new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", 9813)
            .AddText(title)
            .AddText(body)
            .Show();
    }
}