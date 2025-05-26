using System;
using System.Diagnostics;
using System.Device.Wifi;
using System.Threading;
using System.Net.Http;
using nanoFramework.Json;

namespace PeticionesWeb
{
    public class Program
    {
        // Parámetros de la red Wi-Fi
        const string ssid = "CARPE_DIEM";
        const string password = "Oing9002";
        const string url = "http://my.meteoblue.com/packages/basic-day_current?apikey=qwaPIF0891gV7uSm&lat=4.60971&lon=-74.0817&asl=2582&format=json&tz=GMT";
        //const string url = "http://jsonplaceholder.typicode.com/todos/1";
        //const string url = "https://timeapi.io/api/time/current/coordinate?latitude=4.610&longitude=-74.082";

        private static bool wifiConectado = false;

        public static void Main()
        {
            Debug.WriteLine("\nIniciando conexión a internet...");

            try
            {
                // Inicializar adaptador Wi-Fi
                WifiAdapter wifi = WifiAdapter.FindAllAdapters()[0];
                wifi.AvailableNetworksChanged += Wifi_AvailableNetworksChanged;
                // Escanear redes Wi-Fi disponibles
                wifi.ScanAsync();
                Thread.Sleep(5000); // Esperar a que finalice el escaneo
                // Esperar hasta que el Wi-Fi esté conectado
                while (!wifiConectado)
                {
                    Debug.WriteLine("\tEsperando conexión Wi-Fi...");
                    Thread.Sleep(2500);
                }
                // Realizar la petición HTTPS por hilo
                new Thread(RealizarPeticionHttp).Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en la conexión a internet: {ex.Message}");
            }
            // Mantener el programa en ejecución
            Thread.Sleep(Timeout.Infinite);
        }
        private static void Wifi_AvailableNetworksChanged(WifiAdapter sender, object e)
        {
            Debug.WriteLine("\nWifi_AvailableNetworksChanged - get report\n");
            // Get Report of all scanned Wifi networks
            WifiNetworkReport report = sender.NetworkReport;
            int count = 0;
            // Enumerate though networks looking for our network
            foreach (WifiAvailableNetwork net in report.AvailableNetworks)
            {
                // Show all networks found
                Debug.WriteLine($"#{count++}\tNet SSID :{net.Ssid},  BSSID : {net.Bsid},  rssi : {net.NetworkRssiInDecibelMilliwatts.ToString()},  signal : {net.SignalBars.ToString()}");
                // If its our Network then try to connect
                if (net.Ssid == ssid)
                {
                    // Disconnect in case we are already connected
                    sender.Disconnect();
                    Debug.WriteLine("\tDesconectando de Wifi network");
                    Debug.WriteLine($"\tIntentando conectar a red wifi {net.Ssid}");

                    // Connect to network
                    WifiConnectionResult result = sender.Connect(net, WifiReconnectionKind.Automatic, password);

                    // Display status
                    if (result.ConnectionStatus == WifiConnectionStatus.Success)
                    {
                        Debug.WriteLine($"\n\tConectado a red wifi: {net.Ssid}\n");
                        // ip de la esp
                        //Debug.WriteLine($"\tIP Address: {Dns.GetHostEntry().AddressList[0].ToString()}");
                        // salir inmediatamente si se conecta a la red wifi
                        wifiConectado = true; // Confirmar conexión
                        break;
                    }
                    else
                    {
                        Debug.WriteLine($"\tError {result.ConnectionStatus.ToString()} connecting o Wifi network");
                    }
                }
            }
        }

        private static void RealizarPeticionHttp()
        {
            while (true)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.SslProtocols = System.Net.Security.SslProtocols.Tls12; // Forzar TLS 1.2
                        HttpResponseMessage response = client.Get(url);
                        Debug.WriteLine($"Código de estado HTTP: {(int)response.StatusCode}");
                        if (response.IsSuccessStatusCode)
                        {
                            string responseData = response.Content.ReadAsString();
                            Debug.WriteLine("Respuesta recibida:\n");
                            Debug.WriteLine(responseData);
                        }
                        else
                        {
                            Debug.WriteLine($"Error en la petición: {response.StatusCode}\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error en la petición HTTPS: {ex.Message}");
                }
                Thread.Sleep(10000);
            }
        }
    }
}
