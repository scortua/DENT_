using System;
using System.Diagnostics;
using System.Threading;
using System.Device.Wifi;
using System.Net;
using System.IO;

namespace Wifi_
{
    public class Program
    {
        // Set the SSID & Password to your local Wifi network
        const string MYSSID = "XXXX";
        const string MYPASSWORD = "XXXX";
        private const string URL = "https://api.openweathermap.org/data/2.5/weather?q=Bogota,CO&appid=XXXX&units=metric";
        public static void Main()
        {
            try
            {
                Debug.WriteLine("\nIniciando aplicaci�n de escaneo WiFi...");
                Debug.WriteLine("Escaneando redes WiFi...");

                WifiAdapter wifi = WifiAdapter.FindAllAdapters()[0];
                wifi.AvailableNetworksChanged += Wifi_AvailableNetworksChanged;
                try
                {
                    Debug.WriteLine("\nstarting Wi-Fi scan");
                    wifi.ScanAsync();
                    Thread.Sleep(10000);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error de conexion de redes: {ex.Message}");
                }
                wifi.AvailableNetworksChanged -= Wifi_AvailableNetworksChanged;
                Thread.Sleep(5000);
                Debug.WriteLine("\nFin de la aplicaci�n de escaneo WiFi...");
                Debug.WriteLine($"\nRealizando petici�n HTTP a {URL}...");
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                    request.Method = "GET";
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                string content = reader.ReadToEnd();

                                Debug.WriteLine("Respuesta HTTP recibida:");
                                Debug.WriteLine($"C�digo de estado: {response.StatusCode}");
                                Debug.WriteLine("Contenido:");
                                Debug.WriteLine(content);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error de conexi�n HTTP: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error, problema de escaneo y conexion de redes");
                Debug.WriteLine("message:" + ex.Message);
                Debug.WriteLine("stack:" + ex.StackTrace);
            }
            Debug.WriteLine("Fin de la aplicaci�n");
            Thread.Sleep(Timeout.Infinite);
        }

        private static void Wifi_AvailableNetworksChanged(WifiAdapter sender, object e)
        {
            Debug.WriteLine("Wifi_AvailableNetworksChanged - get report");

            // Get Report of all scanned Wifi networks
            WifiNetworkReport report = sender.NetworkReport;
            int count = 0;
            // Enumerate though networks looking for our network
            foreach (WifiAvailableNetwork net in report.AvailableNetworks)
            {
                // Show all networks found
                Debug.WriteLine($"#{count++}\tNet SSID :{net.Ssid},  BSSID : {net.Bsid},  rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()},  signal : {net.SignalBars.ToString()}");

                // If its our Network then try to connect
                if (net.Ssid == MYSSID)
                {
                    // Disconnect in case we are already connected
                    sender.Disconnect();
                    Debug.WriteLine("\tDesconectando de Wifi network");
                    Debug.WriteLine($"\tIntentando conectar a red wifi {net.Ssid}");

                    // Connect to network
                    WifiConnectionResult result = sender.Connect(net, WifiReconnectionKind.Automatic, MYPASSWORD);

                    // Display status
                    if (result.ConnectionStatus == WifiConnectionStatus.Success)
                    {
                        Debug.WriteLine($"\tConectado a red wifi: {net.Ssid}\n");
                    }
                    else
                    {
                        Debug.WriteLine($"\tError {result.ConnectionStatus.ToString()} connecting o Wifi network");
                    }
                }
            }
        }
    }
}
