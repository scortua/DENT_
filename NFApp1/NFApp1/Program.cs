using System;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Device.Gpio;
using System.Device.Wifi;

namespace NFApp1
{
    public class Program
    {
        static ArrayList Lista;
        static Queue Cola;
        static Stack Pila;
        static Hashtable Tabla;

        static Thread HiloHandler;
        static GpioPin led;
        static GpioPin button;
        static object sync = new object();
        public static void Main()
        {
            try
            {
                GpioController gpioControler = new GpioController(); // Controlador de GPIO
                led = gpioControler.OpenPin(2, PinMode.Output); // Pin 2
                led.Write(PinValue.Low); // Enciende el led    
                button = gpioControler.OpenPin(0, PinMode.Input); // Pin boot
                button.ValueChanged += Button_ValueChanged;

                new Thread(HiloButton).Start(); // Hilo para el led
                new Thread(HiloLed).Start();

                HiloHandler = new Thread(FuncionHilo); // Hilo para la función
                HiloHandler.Priority = ThreadPriority.Lowest; // Prioridad del hilo
                HiloHandler.Start(); // Inicia el hilo
                Lista = new ArrayList(); // Lista de elementos
                Cola = new Queue(); // Cola de elementos
                Pila = new Stack(); // Pila de elementos
                Tabla = new Hashtable(); // Tabla de elementos

                Debug.WriteLine("\nLista tiene " + Lista.Count + " elementos");
                Debug.WriteLine("Cola tiene " + Cola.Count + " elementos");
                Debug.WriteLine("Pila tiene " + Pila.Count + " elementos");
                Debug.WriteLine("Tabla tiene " + Tabla.Count + " elementos");

                Lista.Add(1); // Añade un elemento a la lista
                Lista.Add(2);
                Lista.Add(3);
                Lista.Add(4);
                Lista.Insert(0, 9); // Añade un elemento en la posición 0

                Cola.Enqueue(1); // Añade un elemento a la cola
                Cola.Enqueue(2);
                Cola.Enqueue(3);
                Cola.Enqueue(4);
                Cola.Enqueue(5);

                Pila.Push(1); // Añade un elemento a la pila
                Pila.Push(2);
                Pila.Push(3);
                Pila.Push(4);
                Pila.Push(5);

                Tabla.Add("uno", 1); // Añade un elemento a la tabla
                Tabla.Add("dos", 2);
                Tabla.Add("tres", 3);
                Tabla.Add("cuatro", 4);
                Tabla.Add("cinco", 5);

                Debug.WriteLine("\nLista tiene " + Lista.Count + " elementos");
                Debug.WriteLine("Cola tiene " + Cola.Count + " elementos");
                Debug.WriteLine("Pila tiene " + Pila.Count + " elementos");
                Debug.WriteLine("Tabla tiene " + Tabla.Count + " elementos");

                Lista.RemoveAt(0); // Elimina el elemento en la posición 0
                Lista.Remove(3); // Elimina el primer elemento que coincida con el valor

                Cola.Dequeue(); // Elimina el primer elemento de la cola

                Pila.Pop(); // Elimina el último elemento de la pila

                Tabla.Remove("dos"); // Elimina el elemento con la clave "dos"

                Debug.WriteLine("\nLista tiene " + Lista.Count + " elementos");
                Debug.Write("Elementos de lista: ");
                foreach (int i in Lista)
                {
                    Debug.Write(i + ", ");
                }

                Debug.WriteLine("\nCola tiene " + Cola.Count + " elementos");
                Debug.Write("Elementos de Cola: ");
                foreach (int i in Cola)
                {
                    Debug.Write(i + ", ");
                }

                Debug.WriteLine("\nPila tiene " + Pila.Count + " elementos");
                Debug.Write("Elementos de Pila: ");
                foreach (int i in Pila)
                {
                    Debug.Write(i + ", ");
                }

                Debug.WriteLine("\nTabla tiene " + Tabla.Count + " elementos");
                Debug.Write("Elementos de Tabla: ");
                foreach (DictionaryEntry i in Tabla)
                {
                    Debug.Write(i.Key + " = " + i.Value + ", ");
                }

                Debug.WriteLine("\n\nHello Word CSHARP");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }

            HiloHandler.Join(); // Espera a que el hilo termine
            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
        private static void Button_ValueChanged(object sender, PinValueChangedEventArgs e)
        {
            GpioPin button = (GpioPin)sender;


        }   

        public static void FuncionHilo()
        {
            int cont = 0;
            while (true)
            {
                try
                {
                    Debug.WriteLine("Ejecución del Hilo Numero: " + cont++);
                    Thread.Sleep(1000);
                    if (cont == 10)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error en el hilo: " + ex.ToString());
                }
            }
            Debug.WriteLine("Fin de la ejecución del hilo");
        }

        private static void HiloButton()
        {
            while (true)
            {
                try
                {
                    if(button.Read() == PinValue.Low)
                    {
                        Debug.WriteLine("Botón presionado");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error en el hilo: " + ex.ToString());
                }
                Thread.Sleep(10);
            }
        }

        private static void HiloLed()
        {
            int count = 0;
            int onTime = 0;
            lock (sync) // Bloquea el acceso a la sección crítica
            {
                while (true)
                {
                    //led.Toggle();
                    for (onTime = 0; onTime < 100; onTime++)
                    {
                        for (count = 0; count < 10; count++)
                        {
                            if (count < (onTime / 10))
                            {
                                led.Write(PinValue.High);
                            }
                            else
                            {
                                led.Write(PinValue.Low);
                            }
                        }
                        Thread.Sleep(1);
                    }
                }
                /*
                led.Write(PinValue.High);
                Thread.Sleep(100);
                led.Write(PinValue.Low);
                Thread.Sleep(100);
                */
            }
        }
    }
}
