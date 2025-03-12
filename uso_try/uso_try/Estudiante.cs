using System;
using System.Collections.Generic;
using System.Text;

namespace uso_try
{
    internal class Estudiante
    {
        public string Nombre { get; set; } = string.Empty;
        public long carnet { get; set; } = 0;

        public Estudiante(string nombre, long carnet)
        {
            if (nombre == null)
            {
                throw new ArgumentNullException("El nombre no puede ser nulo");

            }
            if (carnet < 0)
            {
                throw new ArgumentOutOfRangeException("El carnet no puede ser negativo");
            }

        }
    }
}
