using InTheHand.Net.Sockets;
using Microsoft.Extensions.Hosting;

namespace MarshallActon2
{
    internal sealed class BackgroundTask : BackgroundService
    {
        private const string _deviceName = "ACTON II";
    
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var bluetoothClient = new BluetoothClient();

                    var bluetoothDeviceInfo = bluetoothClient
                        .PairedDevices
                        .Where(deviceInfo => deviceInfo.DeviceName.Equals(_deviceName, StringComparison.OrdinalIgnoreCase))
                        .First();

                    Console.Write("Status: ");

                    if (bluetoothDeviceInfo.Connected)
                    {
                        Console.WriteLine("Connected");
                    }
                    else
                    {
                        Console.WriteLine("Disconnected");
                        Console.WriteLine();
                    }

                    if (!bluetoothDeviceInfo.Connected)
                    {
                        for (int i = 0; i < bluetoothDeviceInfo.InstalledServices.Count; i++)
                        {
                            try
                            {
                                if (bluetoothDeviceInfo.Connected)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.Write($"Attempt #{i + 1}: ");
                                    _ = Task.Run(() => bluetoothClient.Connect(bluetoothDeviceInfo.DeviceAddress, bluetoothDeviceInfo.InstalledServices.ElementAt(i)));
                                }
                            }
                            catch { }

                            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

                            bluetoothDeviceInfo = bluetoothClient
                                   .PairedDevices
                                   .Where(deviceInfo => deviceInfo.DeviceName.Equals(_deviceName, StringComparison.OrdinalIgnoreCase))
                                   .First();

                            if (bluetoothDeviceInfo.Connected)
                            {
                                Console.WriteLine("Succeeded");
                            }
                            else
                            {
                                Console.WriteLine("Failed");
                            }
                        }

                        Console.WriteLine();
                    }
                }
                catch { }

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }

        }
    }
}
