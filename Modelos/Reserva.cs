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
        public int CantidadAsientos { get; set; }
        public Pasajero Reservante { get; set; }
        public Vuelo Vuelo { get; set; }
        public Reserva() {
            Pasajeros = new List<Pasajero>();
        }
        public Reserva(int id, List<Pasajero> pasajeros, int cantidad, Pasajero reservante, Vuelo vuelo) {
            Id = id;
            Pasajeros = pasajeros;
            CantidadAsientos = cantidad;
            Reservante = reservante;
            Vuelo = vuelo;
        }
    }
}
