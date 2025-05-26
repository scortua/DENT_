using System;
using System.Diagnostics;
using System.Threading;
using System.Device.Wifi;
using System.Net.Http;
using nanoFramework.Json;
using System.Text;
using System.Collections;

namespace peticiones_https
{
    public class Program
    {
        static Thread HiloHandler;
        const String SSID = "Santi's note 20";
        const String PASSWORD = "123456789ñ";
        private static bool _isConnected = false;
        const String URL = "http://my.meteoblue.com/packages/basic-day_current?apikey=qwaPIF0891gV7uSm&lat=4.60971&lon=-74.0817&asl=2582&format=json&tz=GMT";
        // variables - temp,hum,fec,ho
        public static String _temp = "Loading";
        public static String _hum = "Loading";
        // tiempo
        public static void Main()
        {
            Debug.WriteLine("\nHello from nanoFramework!");
            // Connectando a internet
            WifiAdapter wifi = WifiAdapter.FindAllAdapters()[0];
            wifi.AvailableNetworksChanged += ConnectToWifi;
            wifi.ScanAsync();
            Thread.Sleep(5000);
            while (!_isConnected)
            {
                Debug.Write(".");
                Thread.Sleep(1000);
            }     
            // Entrando en el loop que actualiza parametros
            HiloHandler = new Thread(RealizarPeticionHttp);
            HiloHandler.Priority = ThreadPriority.Lowest;
            HiloHandler.Start();
            while (true)
            {
                DateTime ahora = DateTime.UtcNow;
                DateTime ahoraLocal = ahora.AddHours(-5);
                String fecha = ahora.ToString("yyyy-MM-dd");
                String hora = ahoraLocal.ToString("HH:mm:ss");
                Debug.WriteLine($"\nFecha: {fecha} Hora: {hora}");
                Debug.WriteLine($"Temperatura: {_temp} Humedad: {_hum}");
                Thread.Sleep(1000); // Espera 1 segundos antes de la siguiente iteración                                
            }
                       
            Thread.Sleep(Timeout.Infinite); 
        }

        private static void ConnectToWifi(WifiAdapter sender, Object e)
        {
            WifiNetworkReport report = sender.NetworkReport;
            Debug.WriteLine();
            foreach (WifiAvailableNetwork net in report.AvailableNetworks)
            {
                Debug.WriteLine($"SSID: {net.Ssid} SignalStrength: {net.NetworkRssiInDecibelMilliwatts.ToString()}");
                if (net.Ssid == SSID)
                {
                    sender.Disconnect();
                    Debug.WriteLine($"\nConnecting to {SSID}...");
                    WifiConnectionResult result =  sender.Connect(net, WifiReconnectionKind.Automatic, PASSWORD);
                    if (result.ConnectionStatus == WifiConnectionStatus.Success)
                    {
                        _isConnected = true;
                        Debug.WriteLine($"Connected to : {net.Ssid}");
                        break;
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
                    using (HttpClient client = new HttpClient())
                    {                        
                        try
                        {
                            client.SslProtocols = System.Net.Security.SslProtocols.Tls12;
                            HttpResponseMessage response = client.Get(URL);
                            Debug.WriteLine($"\nCodigo respuesta : {(int)response.StatusCode}");
                            if (response.IsSuccessStatusCode)
                            {
                                string responseData = response.Content.ReadAsString();
                                //Debug.WriteLine($"Datos : {responseData}");
                                int len = responseData.Length;
                                //Debug.WriteLine($"Longitud de la respuesta: {len} caracteres");
                                // extraer datos temperatura
                                int index1 = responseData.IndexOf("data_current");
                                int index2 = responseData.IndexOf("data_day") - 2;
                                String data_current = responseData.Substring(index1, index2 - index1);
                                int index3 = data_current.IndexOf("temperature") + 13;
                                Program._temp = data_current.Substring(index3,4);                               
                                // extraer datos humedad
                                int index4 = responseData.IndexOf("relativehumidity_mean");
                                int index5 = responseData.IndexOf("predictability_class") - 2;
                                String relativehumidity_mean = responseData.Substring(index4, index5 - index4);
                                int index6 = relativehumidity_mean.IndexOf("[") + 1;
                                Program._hum = relativehumidity_mean.Substring(index6, 2);  

                            }
                            else
                            {
                                Debug.WriteLine($"Error: {(int)response.StatusCode} {response.ReasonPhrase}");
                            }
                        }
                        catch (System.Net.WebException ex)
                        {
                            Debug.WriteLine($"Error de red: {ex.Message}");                            
                        }
                        catch (System.Net.Sockets.SocketException ex)
                        {
                            Debug.WriteLine($"Error de socket: {ex.Message}");                           
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error general en la petición: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error al crear HttpClient: {ex.Message}");
                }
                Thread.Sleep(60000); // Espera 10 segundos antes de la siguiente petición
            }
        }



    }
}
