using nanoFramework.Networking;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SecureClient
{
    public class Program
    {
        //private static string MySsid = ""; // enter your SSID here or name of your WiFi network
        //private static string MyPassword = ""; // enter your SSID password here or password of your WiFi network
        private static string ApiKey = "69285e08908ba2461be431c348d1e02d"; 
        private static string city = "Bogota,CO"; // enter your city here
        private static string units = "metric"; // enter your units here
        static string data = string.Empty;
        public static void Main()
        {
            Debug.WriteLine("\nWaiting for network up and IP address...");
            // wait for network up and IP address
            bool success;
            CancellationTokenSource cs = new(60000); // 60 seconds timeout
            // setup network
            success = WifiNetworkHelper.Reconnect(); // this is a blocking call with no timeout
            //success = NetworkHelper.SetupAndConnectNetwork(cs.Token); // this is a blocking call with a timeout
            // check if we have an IP address
            if (!success)
            {
                Debug.WriteLine($"\t{DateTime.UtcNow.AddHours(-5)} Can't get a proper IP address, error: {NetworkHelper.Status}.");

                if (NetworkHelper.HelperException != null)
                {
                    Debug.WriteLine($"\tex: {NetworkHelper.HelperException}");
                }
                return;
            }
            else
            {
                Debug.WriteLine($"\t{DateTime.UtcNow.AddHours(-5)} Network connected");
            }
            // get host entry for How's my SSL test site
            IPHostEntry hostEntry = Dns.GetHostEntry("api.openweathermap.org");
            // need an IPEndPoint from that one above
            IPEndPoint ep = new IPEndPoint(hostEntry.AddressList[0], 80);
            // create a socket
            Debug.WriteLine($"\n{DateTime.UtcNow.AddHours(-5)} Opening socket...{hostEntry.AddressList[0]} ");
            // create a socket
            using (Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    Debug.WriteLine("\tConnecting...");
                    // connect socket
                    mySocket.Connect(ep);
                    // send request
                    string request = $"GET /data/2.5/weather?q={city}&appid={ApiKey}&units={units} HTTP/1.0\r\n\r\n"
                                    + "Host: api.openweathermap.org\r\n"
                                    + "Connection: close\r\n\r\n";
                    // convert request to byte array
                    byte[] buffer = Encoding.UTF8.GetBytes(request);
                    // send request
                    mySocket.Send(buffer);
                    // output bytes sent
                    Debug.WriteLine($"Wrote {buffer.Length} bytes");
                    // set up buffer to read data from socket
                    buffer = new byte[1024];
                    // trying to read from socket
                    int bytes = mySocket.Receive(buffer);
                    // output bytes read
                    Debug.WriteLine($"Read {bytes} bytes");
                    // check if we have data
                    if (bytes > 0)
                    {
                        // we have data!
                        // output as string
                        data = new String(Encoding.UTF8.GetChars(buffer));
                        Debug.WriteLine(data);
                        // get the different json parts
                        Debug.WriteLine("\nGetting the different json parts");
                        int startIndex = data.IndexOf('{');
                        int endIndex = data.LastIndexOf('}') + 1;
                        if (startIndex != -1 && endIndex != -1)
                        {
                            string jsonPart = data.Substring(startIndex, endIndex - startIndex);
                            Debug.WriteLine($"\nRespond JSON: {jsonPart}");
                        }
                        // get the temperature
                        Debug.WriteLine("\nGetting the temperature");
                        int tempIndex = data.IndexOf("temp");
                        if (tempIndex != -1)
                        {
                            int tempEndIndex = data.IndexOf(",", tempIndex);
                            string temp = data.Substring(tempIndex + 6, tempEndIndex - tempIndex - 6);
                            Debug.WriteLine($"Temperature: {temp}");
                        }
                        // get the humidity
                        Debug.WriteLine("\nGetting the humidity");
                        int humidityIndex = data.IndexOf("humidity");
                        if (humidityIndex != -1)
                        {
                            int humidityEndIndex = data.IndexOf(",", humidityIndex);
                            string humidity = data.Substring(humidityIndex + 10, humidityEndIndex - humidityIndex - 10);
                            Debug.WriteLine($"Humidity: {humidity}");
                        }
                    }
                }
                catch (SocketException ex)
                {
                    Debug.WriteLine($"** Socket exception occurred: {ex.Message} error code {ex.ErrorCode}!**");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"** Exception occurred: {ex.Message}!**");
                }
                finally
                {
                    Debug.WriteLine("\nClosing socket");
                    mySocket.Close();
                }
            }
            Thread.Sleep(Timeout.Infinite);
        }
        
    }
}