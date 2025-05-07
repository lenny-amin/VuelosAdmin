using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminVuelos.Modelos
{
    internal class Reserva
    {
        public int Id { get; set; }
        public List<Pasajero> Pasajeros { get; set; }
        public int CantidadPasajeros { get; set; }
        public Pasajero Reservante { get; set; }
        //public Vuelo Vuelo { get; set; }
        public Reserva() {
            Pasajeros = new List<Pasajero>();
        }
        public Reserva(int id, int cantidad, Pasajero reservante) {
            Id = id;
            CantidadPasajeros = cantidad;
            Reservante = reservante;
            //Vuelo = vuelo;
        }
    }
}
