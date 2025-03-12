using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Marcador : ElementoEscritura
    {
        #region delegados
        public delegate void Delegado(int x, int y);
        #endregion

        #region atrivutos
        public bool _tapa;
        #endregion

        #region eventos
        public event Delegado Evento;
        #endregion

        #region constructores
        public Marcador(string color)
        {
            Color = color;
            Marca = "sharpie";
            Ancho = 2;
            Alto = 2;
            Largo = 15;
            Tapa = true;
        }

        public Marcador()
        {
            Color = string.Empty;
            Marca = string.Empty;
            Ancho = 0;
            Alto = 0;
            Largo = 0;
            Tapa = false;
        }
        #endregion

        #region metodos
        public void Escribir(string Texto)
        {
            if (!Tapa)
            {
                Console.WriteLine("[" + Color + "]: " + Texto);
            }
            else
            {
                if (Evento != null)
                {
                    Evento((int)Ancho, (int)Alto);
                }
            }
        }

        public void Destapar()
        {
            Tapa = false;
        }

        public void Tapar()
        {
            Tapa = true;
        }
        #endregion

        #region propiedades
        public bool Tapa
        {
            get { return _tapa; }
            set { _tapa = value; }
        }
        public string Estado
        {
            get
            {
                if (_tapa)
                {
                    return "Tapado";
                }
                else
                {
                    return "Destapado";
                }
            }
        }
        #endregion

        #region operadores
        public static Marcador operator +(Marcador m1, Marcador m2)
        {
            Marcador Resultado = new Marcador(m1.Color + "_" + m2.Color);
            Resultado.Marca = m1.Marca + "_" + m2.Marca;
            Resultado.Ancho = m1.Ancho + m2.Ancho;
            Resultado.Alto = m1.Alto + m2.Alto;
            Resultado.Largo = m1.Largo + m2.Largo;
            Resultado.Tapa = m1.Tapa | m2.Tapa;
            return Resultado;
        }
        #endregion

        public override string ToString()
        {
            return "Marcador marca: " + Marca + ", Color: " + Color + ", Respuesta clase padre: " + base.ToString();
        }

    }
}
