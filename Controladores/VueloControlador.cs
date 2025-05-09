using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminVuelos.Modelos;
using Libreria2025;
using System.IO;
using System.Reflection.Emit;

namespace AdminVuelos.Controladores
{
    internal class VueloControlador
    {
        public static void ListadoPorDestino()
        {
           
        }

        public static void DestinosMasVisitados()
        {
            Console.Clear();
            Dictionary<string, int> Destinos = []; 
            foreach (Reserva reserva in Program.Reservas)
            {
                if (Destinos.ContainsKey(reserva.Vuelo.Destino))
                {
                    Destinos[reserva.Vuelo.Destino] += reserva.Pasajeros.Count();
                }
                else { Destinos.Add(reserva.Vuelo.Destino, reserva.Pasajeros.Count()); }
                
            }
            Destinos=Destinos.OrderByDescending(o => o.Value).ToDictionary();
            //foreach (var i in Destinos) { 
            //    Console.WriteLine($"{i.Key} {i.Value}");
            //}
            //Console.ReadKey(true);
            int rows = Destinos.Count + 1;
            int cols = 2;
            string[,] tabla =  new string[rows, cols];
            tabla[0, 0] = "Destino";
            tabla[0, 1] = "Cantidad de Visitantes";

            int index = 1;
            foreach (var kvp in Destinos)
            {
                tabla[index, 0] = kvp.Key;
                tabla[index, 1] = kvp.Value.ToString();
                index++;
            }

            Herramienta.DibujaTabla(tabla);
            Console.ReadKey(true);
        }
        
        public static Vuelo SeleccionVuelo(Vuelo? v = null)
        {
            var opciones = new List<string[]>();
            opciones.Add(new string[] { "Numero", "Origen", "Fecha de salida", "Hora de salida", "Destino" });

            foreach (Vuelo vuelo in Program.Vuelos)
            {
                opciones.Add(new string[]
                {
                    vuelo.Id.ToString(),
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
            if (v != null) Console.Write($"Ingrese el numero de vuelo (actual - {v.Id}): ");
            else Console.Write("Ingrese el numero de vuelo: ");
            int numeroVuelo = Herramienta.IngresoEnteros();
            return Program.Vuelos.First(v => v.Id == numeroVuelo);
        }

        public static void ImprimirPasajeros(List<Pasajero> pasajeros)
        {
            foreach (Pasajero pasajero in pasajeros)
            {
                if (pasajeros.IndexOf(pasajero) == pasajeros.Count() - 1)
                {
                    Console.Write(pasajero.Nombre + " " + pasajero.Apellido);
                }
                else
                {
                    Console.Write(pasajero.Nombre + " " + pasajero.Apellido + ", ");
                }
            }
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
