using System;
using System.Threading;
using System.Diagnostics;
using System.Device.Gpio;

namespace GPIO
{
    public class Program
    {
        static GpioPin ledPin;
        static GpioPin buttonPin;
        static Thread HiloHandler;
        static int ms = 1, count = 0;
        public static void Main()
        {
            try
            {
                GpioController gpio = new GpioController();
                ledPin = gpio.OpenPin(2, PinMode.Output); // GPIO2 led de tarjeta ESP32
                ledPin.Write(PinValue.Low);
                buttonPin = gpio.OpenPin(0, PinMode.Input); // GPIO0 boton de tarjeta ESP32

                new Thread(Hiloled).Start(); 
                HiloHandler = new Thread(ParalelHilo); // Creaci�n del hilo
                HiloHandler.Priority = ThreadPriority.Lowest; // Prioridad del hilo
                HiloHandler.Start(); // Inicia el hilo

                buttonPin.ValueChanged += ButtonPin_ValueChanged;

                Debug.WriteLine("\nAqui el c�digo se ejecuta a la vez.\n");

            }
            catch (Exception ex) // Manejo de excepciones
            {
                Debug.WriteLine(ex.Message);
            }        
            Debug.WriteLine("Hello from nanoFramework!");
            HiloHandler.Join(); // Espera a que el hilo termine
            Thread.Sleep(Timeout.Infinite);
        }
        /*
         * �	Ejecuci�n paralela: Este c�digo se ejecuta en un hilo separado (iniciado con new Thread(Hiloled).Start() en el m�todo Main), permitiendo que el programa principal siga ejecut�ndose sin interrupciones.
         * �	Bucle infinito: El while(true) crea un ciclo infinito que hace que el LED parpadee continuamente.
         * �	Control de tiempo: El tiempo de parpadeo se controla mediante la variable ms que puede ser modificada por el evento del bot�n, haciendo que el LED parpadee m�s lentamente cuando se presiona el bot�n.
         * �	Operaciones GPIO: Utiliza los m�todos Write() del pin GPIO para alternar entre estados High (encendido) y Low (apagado).
            Este enfoque multihilo es fundamental en sistemas embebidos para manejar m�ltiples tareas concurrentes, como monitorear sensores mientras se controlan actuadores.
         */
        private static void Hiloled()
        {
            while (true)
            {
                ledPin.Write(PinValue.High);
                Thread.Sleep(ms);
                ledPin.Write(PinValue.Low);
                Thread.Sleep(ms);
            }
        }
        /*
         * Hilo que muestra el trabajo paralelo de un hilo en el programa principal.
         */
        public static void ParalelHilo()
        {
            int cont = 0;
            while (true)
            {
                try
                {
                    Debug.WriteLine("Ejecucion del Hilo Numero: " + cont++);
                    Thread.Sleep(1000);
                    if (cont == 25)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error en el hilo: " + ex.ToString());
                }
            }
            Debug.WriteLine("Fin de la ejecucion del hilo");
        }
        /*
         * Este m�todo es un manejador de eventos que se activa cada vez que cambia el valor del 
         * pin del bot�n en la placa ESP32. El m�todo est� configurado como callback para el evento 
         * ValueChanged del pin GPIO0.
         */
        private static void ButtonPin_ValueChanged(object sender, PinValueChangedEventArgs e)
        {
            if (e.ChangeType == PinEventTypes.Falling)
            {
                ms *= 2;
                Console.WriteLine("\n#" + count++ + " �Bot�n presionado!\tTiempo: " + ms + " miliseg");
            }
            else if (e.ChangeType == PinEventTypes.Rising)
            {
                Console.WriteLine("\n�Bot�n soltado!\n");
            }
        }
    }
}
