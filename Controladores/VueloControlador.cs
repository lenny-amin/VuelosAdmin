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
        public static void ListarVuelo()
        {
            Console.Clear();
            Vuelo vuelo = SeleccionVuelo();

            if (vuelo == null)
            {
                Program.Error("\nEste vuelo no existe");
                return;
            }

            Console.Clear();
            Console.WriteLine("\nOrigen: " + vuelo.Origen);
            Console.WriteLine("Destino: " + vuelo.Destino);
            Console.WriteLine("Fecha de salida: " + vuelo.FechaSalida.ToShortDateString());
            Console.WriteLine("Hora de salida: " + vuelo.HoraSalida.ToShortTimeString());
            Console.WriteLine("\nPasajeros:");
            foreach (Reserva reserva in vuelo.Reservas)
            {
                if (vuelo.Reservas.IndexOf(reserva) == vuelo.Reservas.Count() - 1)
                {
                    ImprimirPasajeros(reserva.Pasajeros);
                }
                else
                {
                    ImprimirPasajeros(reserva.Pasajeros);
                    Console.Write(", ");
                }
            }

            Console.WriteLine("\n\nIngrese 1 para imprimir a archivo, o cualquier otro numero para volver...");
            int opcion = Herramienta.IngresoEnteros();
            if (opcion == 1)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"../../../Reportes/ReporteVuelos{DateTime.Now.ToString("yyyy-MM-dd")}.txt");

                if (!File.Exists(path))
                {

                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("\nOrigen: " + vuelo.Origen);
                        sw.WriteLine("Destino: " + vuelo.Destino);
                        sw.WriteLine("Fecha de salida: " + vuelo.FechaSalida.ToShortDateString());
                        sw.WriteLine("Hora de salida: " + vuelo.HoraSalida.ToShortTimeString());
                        sw.WriteLine("\nPasajeros:");
                        foreach (Reserva reserva in vuelo.Reservas)
                        {
                            if (vuelo.Reservas.IndexOf(reserva) == vuelo.Reservas.Count() - 1)
                            {
                                foreach (Pasajero pasajero in reserva.Pasajeros)
                                {
                                    if (reserva.Pasajeros.IndexOf(pasajero) == reserva.Pasajeros.Count() - 1)
                                    {
                                        sw.Write(pasajero.Nombre + " " + pasajero.Apellido);
                                    }
                                    else
                                    {
                                        sw.Write(pasajero.Nombre + " " + pasajero.Apellido + ", ");
                                    }
                                }
                            }
                            else
                            {
                                foreach (Pasajero pasajero in reserva.Pasajeros)
                                {
                                    if (reserva.Pasajeros.IndexOf(pasajero) == reserva.Pasajeros.Count() - 1)
                                    {
                                        sw.Write(pasajero.Nombre + " " + pasajero.Apellido);
                                    }
                                    else
                                    {
                                        sw.Write(pasajero.Nombre + " " + pasajero.Apellido + ", ");
                                    }
                                }
                                sw.Write(", ");
                            }
                        }
                    }
                }

            }
            else return;
        }

        public static void ListadoPorDestino()
        {
            Console.Clear();

            // todos los destinos
            List<string> destinos = [];
            foreach (Vuelo vuelo in Program.Vuelos)
            {
                if (!destinos.Any(d => d == vuelo.Destino))
                {
                    destinos.Add(vuelo.Destino);
                }
            }

            foreach (string destino in destinos)
            {
                Console.WriteLine(destino);
                List<Vuelo> vuelosConDestino = Program.Vuelos.Where(v => v.Destino == destino).ToList();
                List<DateTime> tiempos = [];

                foreach (Vuelo vuelo in vuelosConDestino)
                {
                    // agregar a los tiempos
                    if (!tiempos.Any(t => t == vuelo.FechaSalida))
                    {
                        tiempos.Add(vuelo.FechaSalida);
                    }
                }

                foreach (DateTime tiempo in tiempos)
                {
                    Console.WriteLine(" " + tiempo.ToShortDateString());
                    foreach (Vuelo vuelo in vuelosConDestino)
                    {
                        if (tiempo == vuelo.FechaSalida)
                        {
                            Console.WriteLine("  Origen: " + vuelo.Origen);
                            Console.WriteLine("  Asientos disponibles: " + vuelo.AsientosDisponibles);
                            Console.WriteLine("  Hora de salida: " + vuelo.HoraSalida.ToShortTimeString() + "\n");
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.ReadKey(true);
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
            Destinos = Destinos.OrderByDescending(o => o.Value).ToDictionary();
            //foreach (var i in Destinos) { 
            //    Console.WriteLine($"{i.Key} {i.Value}");
            //}
            //Console.ReadKey(true);
            int rows = Destinos.Count + 1;
            int cols = 2;
            string[,] tabla = new string[rows, cols];
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
            Console.WriteLine("\n\nIngrese 1 para imprimir a archivo, o cualquier otro numero para volver...");
            int opcion = Herramienta.IngresoEnteros();
            if (opcion == 1)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"../../../Reportes/ReporteDestinosMasVisitados{DateTime.Now.ToString("yyyy-MM-dd")}.txt");

                if (!File.Exists(path))
                {

                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {

                        for (int i = 1; i < rows; i++)
                        {
                            string destino = tabla[i, 0] ?? "(Sin destino)";
                            string cantidad = tabla[i+1, 1] ?? "0";
                            sw.WriteLine($"Destino: {destino}, Cantidad de visitantes: {cantidad}");
                        }
                    }
                }

            }
            else return;

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
            if (Program.Vuelos.FirstOrDefault(v => v.Id == numeroVuelo) != null)
            {
                return Program.Vuelos.FirstOrDefault(v => v.Id == numeroVuelo);
            }
            else return null;
            //if (vueloSeleccionado)
            //{

            //}
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
