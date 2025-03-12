using System;
using System.Threading;
using System.Diagnostics;
using System.Collections;


namespace uso_try
{
    public class Program
    {
        static ArrayList Lista;
        static Queue Cola;
        static Stack Pila;
        static Hashtable Tabla;
        static Estudiante est;
        public static void Main()
        {
            try
            {
                Estudiante est = new Estudiante("SANTI",1000603927);
            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("Error por argumento nulo, " + e.ToString());
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine("Error por argumento fuera de rango, " + e.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error indeterminado, información = " + e.ToString());
            }
            finally
            {
                Debug.WriteLine("Finalizando el programa");
            }
            Debug.WriteLine("Hello from nanoFramework!");

            Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
    }
}
