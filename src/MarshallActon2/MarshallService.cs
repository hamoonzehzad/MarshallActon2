using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace MarshallActon2;

public static class MarshallService
{
    private static IReadOnlyDictionary<string, Guid> Services { get; } = GetServices();

    private static BluetoothClient Client { get; } = new();

    private static IReadOnlyDictionary<string, Guid> GetServices()
    {
        var services = new Dictionary<string, Guid>()
        {
            { nameof(BluetoothService.AudioSource), BluetoothService.AudioSource },
            { nameof(BluetoothService.AudioSink), BluetoothService.AudioSink },
            { nameof(BluetoothService.AVRemoteControlTarget), BluetoothService.AVRemoteControlTarget },
            { nameof(BluetoothService.AVRemoteControl), BluetoothService.AVRemoteControl }
        }
        .AsReadOnly();

        return services;
    }

    public static async Task ConnectAsync()
    {
        var devices = GetDevices();

        await ConnectDevicesAsync(devices);
    }

    public static async Task ConnectDevicesAsync(List<BluetoothDeviceInfo> devices)
    {
        foreach (var device in devices)
        {
            Console.WriteLine(device.DeviceName);
            Console.WriteLine();

            try
            {
                CheckInstalledServices(device);

                await ConnectDeviceAsync(device);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine();
            }

            Console.WriteLine("---------------------------------");
            Console.WriteLine();
        }
    }

    private static void CheckInstalledServices(BluetoothDeviceInfo device)
    {
        Console.WriteLine("Checking Installed Services");
        Console.WriteLine();

        foreach (var service in Services)
        {
            Console.Write($"{service.Key}: ");

            if (!device.InstalledServices.Contains(service.Value))
            {
                device.SetServiceState(service.Value, true);
            }
            
            Console.WriteLine($"Checked");
        }

        Console.WriteLine();
    }

    private static async Task ConnectDeviceAsync(BluetoothDeviceInfo device)
    {
        Console.Write("State: ");

        if (!device.Connected)
        {
            var endpoint = new BluetoothEndPoint(device.DeviceAddress, BluetoothService.AudioSink);

            await Task.Delay(TimeSpan.FromSeconds(5));
            
            Client.Connect(endpoint);
        }

        Console.WriteLine("Connected");
        Console.WriteLine();
    }

    private static List<BluetoothDeviceInfo> GetDevices()
    {
        using var bluetoothClient = new BluetoothClient();

        var devices = bluetoothClient
            .PairedDevices
            .Where(device => device.DeviceName.Equals("ACTON II", StringComparison.OrdinalIgnoreCase))
            .ToList();

        return devices;
    }
}
