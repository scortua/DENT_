using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ElementoEscritura
    {
        #region atrivutos
        protected string _color;
        public string _marca;
        public uint _ancho;
        public uint _alto;
        public uint _largo;
        #endregion

        #region constructores
        public ElementoEscritura(string color)
        {
            _color = color;
            _marca = "sharpie";
            _ancho = 2;
            _alto = 2;
            _largo = 15;
        }

        public ElementoEscritura()
        {
            _color = string.Empty;
            _marca = string.Empty;
            _ancho = 0;
            _alto = 0;
            _largo = 0;
        }
        #endregion

        #region propiedades
        public string Color
        {
            get 
            {
                return _color;
            }
            set
            {
                if(!string.IsNullOrEmpty(value))
                {
                    _color = Color;
                }
            }
        }
        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }
        public uint Ancho
        {
            get { return _ancho; }
            set { _ancho = value; }
        }
        public uint Alto
        {
            get { return _alto; }
            set { _alto = value; }
        }
        public uint Largo
        {
            get { return _largo; }
            set { _largo = value; }
        }
        #endregion

        public override string ToString()
        {
            return "Elemento de escritura marca: " + Marca + ", Color: " + Color + ", Respuesta clase padre: " + base.ToString();
        }

    }
}
