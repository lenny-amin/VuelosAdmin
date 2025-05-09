using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminVuelos.Modelos
{
    internal class Vuelo
    {
        public int Id { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public DateTime FechaSalida { get; set; }
        public TimeOnly HoraSalida { get; set; }
        public int AsientosDisponibles { get; set; }
        public List<Reserva> Reservas { get; set; }

        public Vuelo() {
            Reservas = new List<Reserva>();
        }


        public Vuelo(int id, string origen, string destino, DateTime fechaSalida, TimeOnly horaSalida, int asientosDisponibles)
        {
            Id = id;
            Origen = origen;
            Destino = destino;
            FechaSalida = fechaSalida;
            HoraSalida = horaSalida;
            AsientosDisponibles = asientosDisponibles;

            Reservas = new List<Reserva>();

        }
    }
}
