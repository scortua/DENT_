using System;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Device.Gpio;

namespace Uso_Threats
{
    public class Program
    {
        static Thread HiloHandler; // Thread para el manejo de eventos
        static GpioPin Led;        // Pin del led
        static GpioPin Button;     // Pin del botón
        static object sync = new object(); // Objeto para sincronización
        public static void Main()
        {
            try
            {
                // Configuración de los pines
                GpioController controller = new GpioController(); // Controlador de los pines
                Led = controller.OpenPin(2);                      // Pin del led
                Led.SetPinMode(PinMode.Output);                   // Modo de salida
                Button = controller.OpenPin(0, PinMode.Input);    // Pin del botón

                Button.ValueChanged += Button_ValueChanged; // Evento del botón

                new Thread(HiloButton).Start(); // Inicia el hilo del botón

                HiloHandler = new Thread(FuncionHilo);// Hilo para el manejo de eventos
                HiloHandler.Priority = ThreadPriority.Lowest; // Prioridad del hilo
                HiloHandler.Start(); // Inicia el hilo
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            Debug.WriteLine("\n\nHello from nanoFramework!");

            HiloHandler.Join(); // Espera a que el hilo termine para cerrar la aplicación
            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }

        private static void FuncionHilo() // Funcion del hilo verificando el uso de hilos
        {
            int count = 0;
            while (true)
            {
                try
                {
                    Debug.WriteLine("\t\tHilo Numero: " + count++);
                    Thread.Sleep(1000);
                    if (count == 15)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error en el hilo: " + ex.ToString());
                }
            }
            Debug.WriteLine("\n\tFin de la ejecucion del hilo\n");
        }

        private static void Button_ValueChanged(object sender, PinValueChangedEventArgs e) // Evento del botón
        {
            Thread hiloLed = new Thread(HiloLed);
            hiloLed.Start(); // Inicia el hilo del led
        }

        private static void HiloButton() // Hilo del botón para verificar si se presiona
        {
            while (true)
            {
                try
                {
                    if (Button.Read() == PinValue.Low)
                    {
                        Debug.WriteLine("\tBoton presionado");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error en el hilo: " + ex.ToString());
                }
                Thread.Sleep(10);
            }
        }

        private static void HiloLed() // Hilo del led para encenderlo en parpadeos
        {
            lock (sync) // Bloquea el acceso a la secci�n cr�tica
            {
                Led.Toggle();
                Thread.Sleep(500);
                Led.Toggle();
                Thread.Sleep(500);
                Led.Toggle();
                Thread.Sleep(500);
            }
        }
    }
}
