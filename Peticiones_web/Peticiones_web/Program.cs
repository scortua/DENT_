using System;
using System.Diagnostics;
using System.Device.Wifi;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace PeticionesWeb
{
    public class Program
    {
        // Parámetros de la red Wi-Fi
        const string ssid = "name";
        const string password = "pass";
        const string url = "https://jsonplaceholder.typicode.com/posts/1";

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

                // Realizar la petición HTTPS
                RealizarPeticionHttps(url);
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

        private static void RealizarPeticionHttps(string url)
        {
            Debug.WriteLine($"Realizando petición HTTPS a: {url}\n");
            try
            {
                using (var client = new HttpClient())
                {
                    // Establecer la política de seguridad TLS 1.3
                    client.SslProtocols = System.Net.Security.SslProtocols.Tls13;

                    // Si es necesario, agregar certificados SSL al cliente
                    // Esto se usa si el servidor requiere validación de certificado
                    // client.HttpsAuthentCerts = new X509Certificate[] { certificadoRaiz };

                    // Realizar la petición GET
                    HttpResponseMessage response = client.Get(url);
                    string responseData = response.Content.ReadAsString();

                    Debug.WriteLine("Respuesta recibida:");
                    Debug.WriteLine(responseData);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en la petición HTTPS: {ex.Message}");
            }
        }
    }
}
