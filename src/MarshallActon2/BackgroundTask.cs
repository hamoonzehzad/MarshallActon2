using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Microsoft.Extensions.Hosting;

namespace MarshallActon2
{
    internal sealed class BackgroundTask : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var devices = GetDevices();

                    foreach (var device in devices)
                    {
                        ConnectDevice(device);
                    }

                }
                catch { }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }

        private static List<BluetoothDeviceInfo> GetDevices()
        {
            using var bluetoothClient = new BluetoothClient();

            var devices = bluetoothClient
                .PairedDevices
                .Where(device => device.DeviceName.Contains("ACTON", StringComparison.OrdinalIgnoreCase))
                .ToList();

            return devices;
        }

        private static void ConnectDevice(BluetoothDeviceInfo device)
        {
            Console.WriteLine($"{device.DeviceName} => {(device.Connected ? "Connected" : "Disconnected")}");

            foreach (var service in device.InstalledServices)
            {
                ConnectDeviceService(device, service);
            }
        }

        private static void ConnectDeviceService(BluetoothDeviceInfo device, Guid service)
        {
            _ = Task.Run(() =>
            {
                var serviceName = BluetoothService.GetName(service);

                try
                {
                    using var bluetoothClient = new BluetoothClient();

                    var endPoint = new BluetoothEndPoint(device.DeviceAddress, service);

                    bluetoothClient.Connect(endPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            });
        }
    }
}