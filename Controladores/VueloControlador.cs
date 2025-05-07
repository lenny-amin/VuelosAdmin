using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminVuelos.Modelos;

namespace AdminVuelos.Controladores
{
    internal class VueloControlador
    {
        public static bool VerificarPasajeros(Pasajero pasajero, Vuelo vuelo)
        {
            foreach (Reserva reserva in vuelo.Reservas)
            {
                bool exists = reserva.Pasajeros.Where(p => p.Id == pasajero.Id).Any();
                if (exists)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Validar(int asientosDelPasajero, Vuelo vuelo)
        {
            if (asientosDelPasajero > vuelo.AsientosDisponibles)
            {
                return false;
            }
            else return true;
        }
    }
}
