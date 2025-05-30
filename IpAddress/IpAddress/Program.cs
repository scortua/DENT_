using System;
using System.Threading;
using System.Device.Wifi;
using System.Net
using System.Net.NetworkInformation;

public class Program
{
    public static void Main()
    {
        string ssid = "TU_SSID";
        string password = "TU_PASSWORD";

        // Inicializa el adaptador WiFi
        var wifi = new WifiAdapter();
        wifi.Scan();
        Thread.Sleep(2000); // Espera a que termine el escaneo

        // Conéctate a la red WiFi
        wifi.Connect(ssid, WifiReconnectionKind.Automatic, password);

        // Espera hasta que obtenga una IP válida
        while (wifi.NetworkInterface.IPv4Address == "0.0.0.0")
        {
            Thread.Sleep(500);
        }

        // Muestra la IP en consola
        Console.WriteLine("IP: " + wifi.NetworkInterface.IPv4Address);

        // Si quieres mostrar la IP en tu GUI:
        // IpAddr.SetText("IP: " + wifi.NetworkInterface.IPv4Address);

        Thread.Sleep(Timeout.Infinite);
    }
}