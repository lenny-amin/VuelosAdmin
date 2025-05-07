using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminVuelos.Modelos;
using Libreria2025;

namespace AdminVuelos.Controladores
{
    internal class VueloControlador
    {

        public static void Reservar()
        {
            Console.Clear();
            var opciones = new List<string[]>();
            opciones.Add(new string[] { "Origen", "Fecha de salida", "Hora de salida", "Destino" });

            foreach (Vuelo vuelo in Program.Vuelos)
            {
                opciones.Add(new string[]
                {
                    vuelo.Origen,
                    vuelo.FechaSalida.ToShortDateString(),
                    vuelo.HoraSalida.ToShortTimeString(),
                    vuelo.Destino
                });
            }

            int rows = opciones.Count;
            int cols = opciones[0].Length;
            string[,] tabla = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    tabla[i, j] = opciones[i][j];
                }
            }

            Herramienta.DibujaTabla(tabla);
            Console.ReadKey();

        }
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
