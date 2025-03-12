using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

// SANTIAGO CORTES TOVAR

namespace COORDENADAS
{
    internal class coordenadas
    {
        // atrivutos o propiedades de las coordenadas de la clase
        #region atrivutos
        private float _x;
        private float _y;
        #endregion
        // metodos o funciones de la clase
        #region metodos
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        #endregion
        // crear el objeto de la clase segun entradas
        #region constructores
        public coordenadas(float n, float a)
        {
            this._x = n;
            this._y= a;
        }
        public coordenadas()
        {
            _x = 1;
            _y = 0;
        }
        #endregion
        // propiedades que tienen los objetos de la clase
        #region propiedades
        public float norma(coordenadas c)
        {
            return (float)Math.Sqrt(c.X * c.X + c.Y * c.Y);
        }
        public float angulo(coordenadas c)
        {
            return (float)Math.Atan2(c.Y, c.X);
        }
        public float angulo_grados(coordenadas c)
        {
            return (float)(angulo(c) * 180 / Math.PI);
        }   
        #endregion
        // sobrecargar operadores de otras clases para la clase coordenadas
        #region sobrecarga operadores
        public static coordenadas operator +(coordenadas c1, coordenadas c2)
        {
            coordenadas c = new coordenadas();
            c.X = c1.X + c2.X;
            c.Y = c1.Y + c2.Y;
            return c;
        }
        public static coordenadas operator -(coordenadas c1, coordenadas c2)
        {
            coordenadas c = new coordenadas();
            c.X = c1.X - c2.X;
            c.Y = c1.Y - c2.Y;
            return c;
        }
        public static coordenadas operator *(coordenadas c1, coordenadas c2)
        {
            coordenadas c = new coordenadas();
            c.X = c1.X * c2.X;
            c.Y = c1.Y * c2.Y;
            return c;
        }
        #endregion
        // retorna el objeto en cadena, sin necesidad de extraer los atributos
        #region escribir el objeto
        public override string ToString()
        {
            return "(" + X + "," + Y + ")";
        }
        #endregion
    }
}
