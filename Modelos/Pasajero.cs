using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminVuelos.Modelos
{
    internal class Pasajero
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public List<Reserva> Reservas { get; set; }
        public Pasajero()
        {
            Reservas = new List<Reserva>();
        }
        public Pasajero(int id, string nombre, string apellido, string dni)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Reservas = new List<Reserva>();

        }

    }

}
