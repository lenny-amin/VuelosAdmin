using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminVuelos.Modelos;
using Libreria2025;

namespace AdminVuelos.Controladores
{
    internal class ReservaControlador
    {
        public static Reserva SeleccionReserva()
        {
            var opciones = new List<string[]>();
            opciones.Add(new string[] { "Numero", "Pasajeros", "Cantidad de asientos", "Reservante", "Numero de vuelo" });

            if (Program.usuario.Reservas.Count() == 0)
            {
                return null;
            }

            // !IMPORTANT cambiar a reservas del usuario, no globales
            foreach (Reserva reserva in Program.usuario.Reservas)
            {
                string p = "";
                foreach (Pasajero pasajero in reserva.Pasajeros)
                {
                    if (reserva.Pasajeros.IndexOf(pasajero) == reserva.Pasajeros.Count() - 1)
                    {
                        p += pasajero.Nombre + " " + pasajero.Apellido;
                    }
                    else
                    {
                        p += pasajero.Nombre + " " + pasajero.Apellido + ", ";
                    }
                }

                if (p.Length > 11) p = p.Substring(0, 11) + "...";

                opciones.Add(new string[]
                {
                    reserva.Id.ToString(),
                    p,
                    reserva.CantidadAsientos.ToString(),
                    reserva.Reservante.Nombre + " " + reserva.Reservante.Apellido,
                    reserva.Vuelo.Id.ToString()
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
            Console.Write("Ingrese el numero de reserva: ");
            int id = Herramienta.IngresoEnteros();

            return Program.Reservas.First(r => r.Id == id);
        }

        public static void Modificar()
        {
            Reserva reserva = SeleccionReserva();
            if (reserva == null)
            {
                Console.WriteLine("No tiene reservas. Presione cualquier tecla para volver");
                Console.ReadKey(true);
                return;
            }
            Console.WriteLine();
            Vuelo vuelo = VueloControlador.SeleccionVuelo(reserva.Vuelo);

            Console.Write($"\nIngrese la cantidad de asientos que quiere (actual: {reserva.CantidadAsientos}): ");
            int asientos = Herramienta.IngresoEnteros();

            Console.WriteLine();

            if (!VueloControlador.Validar(asientos, vuelo))
            {
                Console.WriteLine("La cantidad de asientos que eligio excede la cantidad de asientos disponibles");
                Console.ReadKey(true);
                Modificar();
            }

            Console.Write("\nLos pasajeros actuales son: ");
            ImprimirPasajeros(reserva.Pasajeros);
            Console.Write("\nIngrese 1 para guardar estos valores, o cualquier otro numero para cambiarlos... ");
            int opcion = Herramienta.IngresoEnteros();

            Console.WriteLine();


            List<Pasajero> pasajeros = [Program.usuario];
            if (opcion == 1) pasajeros = reserva.Pasajeros;


            if (opcion != 1)
            {
                if (asientos > 1)
                {
                    int id;
                    do
                    {
                        // fijarse que el pasajero no invite mas acompanantes de los que reservo asientos
                        if (pasajeros.Count() == asientos) break;
                        Herramienta.DibujoMenu("Elija un acompañante. Si no quiere, ponga 0", Program.Pasajeros.Where(p => p.Id != Program.usuario.Id).Select(p => p.Nombre + " " + p.Apellido).ToArray());
                        id = Herramienta.IngresoEnteros();

                        if (id == 0) break;

                        var pasajero = Program.Pasajeros.Where(p => p.Id != Program.usuario.Id).FirstOrDefault(p => p.Id == id);
                        if (pasajero == null)
                        {
                            Console.WriteLine("\nId invalido. Intente nuevamente....");
                            Console.ReadKey(true);
                            continue;
                        }

                        if (!VueloControlador.VerificarPasajeros(pasajero, vuelo))
                        {
                            Console.WriteLine("\nEl pasajero ya está en este vuelo.");
                            Console.ReadKey(true);
                            continue;
                        }

                        pasajeros.Add(pasajero);

                    } while (true);

                }
            }

            // si cambio el vuelo
            if (reserva.Vuelo.Id != vuelo.Id)
            {
                // eliminar el vuelo de la lista de reservas del vuelo
                reserva.Vuelo.Reservas = reserva.Vuelo.Reservas.Where(r => r.Id != reserva.Id).ToList();
            }

            reserva.Vuelo = vuelo;
            reserva.CantidadAsientos = asientos;
            reserva.Pasajeros = pasajeros;
            // restar la cantidad de asientos
            if (asientos != reserva.CantidadAsientos)
            {
                reserva.Vuelo.AsientosDisponibles += reserva.CantidadAsientos;
                reserva.Vuelo.AsientosDisponibles -= asientos;
            }

            Console.Clear();
            Console.WriteLine($"Ha editado la reserva {reserva.Id}");
            Console.WriteLine("Cantidad de asientos: " + reserva.CantidadAsientos);
            Console.WriteLine("Numero de vuelo: " + vuelo.Id);
            Console.Write("Pasajeros: ");
            ImprimirPasajeros(pasajeros);

            Console.WriteLine("\nPresione 1 para imprimir a archivo, o cualquier otra tecla para volver...");
            int seleccion = Herramienta.IngresoEnteros();
            if (seleccion == 1)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"../../../Reportes/ReporteModificacion{vuelo.Id}{reserva.Id}.txt");

                if (!File.Exists(path))
                {

                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine($"Ha editado la reserva {reserva.Id}");
                        Console.WriteLine("Cantidad de asientos: " + reserva.CantidadAsientos);
                        Console.WriteLine("Numero de vuelo: " + vuelo.Id);
                        sw.Write("Pasajeros:");
                        foreach (Pasajero pasajero in pasajeros)
                        {
                            if (pasajeros.IndexOf(pasajero) == pasajeros.Count() - 1)
                            {
                                sw.Write(pasajero.Nombre + " " + pasajero.Apellido);
                            }
                            else
                            {
                                sw.Write(pasajero.Nombre + " " + pasajero.Apellido + ", ");
                            }
                        }
                    }
                }
            }
            else return;

        }

        public static void Reservar()
        {
            List<Pasajero> pasajeros = [Program.usuario];
            Console.Clear();
            Vuelo vuelo = VueloControlador.SeleccionVuelo();



            Console.Write("\nIngrese la cantidad de asientos que quiere: ");
            int asientos = Herramienta.IngresoEnteros();

            Console.WriteLine();

            if (!VueloControlador.Validar(asientos, vuelo))
            {
                Console.WriteLine("La cantidad de asientos que eligio excede la cantidad de asientos disponibles");
                Console.ReadKey(true);
                Reservar();
            }

            if (asientos > 1)
            {
                int id;
                do
                {
                    // fijarse que el pasajero no invite mas acompanantes de los que reservo asientos
                    if (pasajeros.Count() == asientos) break;
                    Herramienta.DibujoMenu("Elija un acompañante. Si no quiere, ponga 0", Program.Pasajeros.Where(p => p.Id != Program.usuario.Id).Select(p => p.Nombre + " " + p.Apellido).ToArray());
                    id = Herramienta.IngresoEnteros();

                    if (id == 0) break;

                    var pasajero = Program.Pasajeros.Where(p => p.Id != Program.usuario.Id).FirstOrDefault(p => p.Id == id);
                    if (pasajero == null)
                    {
                        Console.WriteLine("\nId inválido. Intente nuevamente.");
                        Console.ReadKey(true);
                        continue;
                    }

                    if (!VueloControlador.VerificarPasajeros(pasajero, vuelo))
                    {
                        Console.WriteLine("\nEl pasajero ya está en este vuelo.");
                        Console.ReadKey(true);
                        continue;
                    }

                    pasajeros.Add(pasajero);

                } while (true);

            }

            int lastId = Program.Reservas.Count() != 0 ? Program.Reservas[Program.Reservas.Count() - 1].Id + 1 : 1;
            Reserva reserva = new Reserva(lastId, pasajeros, asientos, Program.usuario, vuelo);

            vuelo.AsientosDisponibles -= asientos;
            vuelo.Reservas.Add(reserva);

            Program.usuario.Reservas.Add(reserva);
            Program.Reservas.Add(reserva);

            Console.Clear();
            Console.WriteLine("Ha creado una reserva ");
            Console.WriteLine("Origen: " + vuelo.Origen);
            Console.WriteLine("Destino: " + vuelo.Destino);
            Console.WriteLine("Fecha: " + vuelo.FechaSalida.ToShortDateString() + " a las " + vuelo.HoraSalida.ToShortTimeString());
            Console.WriteLine("Cantidad de asientos: " + reserva.CantidadAsientos);
            Console.Write("Pasajeros: ");
            ImprimirPasajeros(pasajeros);

            Console.WriteLine("\nPresione 1 para imprimir a archivo, o cualquier otra tecla para volver...");
            int opcion = Herramienta.IngresoEnteros();
            if (opcion == 1)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"../../../Reportes/Reporte{vuelo.Id}{reserva.Id}.txt");

                if (!File.Exists(path))
                {

                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Ha creado una reserva ");
                        sw.WriteLine("Origen: " + vuelo.Origen);
                        sw.WriteLine("Destino: " + vuelo.Destino);
                        sw.WriteLine("Fecha: " + vuelo.FechaSalida.ToShortDateString() + " a las " + vuelo.HoraSalida.ToShortTimeString());
                        sw.WriteLine("Cantidad de asientos: " + reserva.CantidadAsientos);
                        sw.Write("Pasajeros: ");
                        foreach (Pasajero pasajero in pasajeros)
                        {
                            if (pasajeros.IndexOf(pasajero) == pasajeros.Count() - 1)
                            {
                                sw.Write(pasajero.Nombre + " " + pasajero.Apellido);
                            }
                            else
                            {
                                sw.Write(pasajero.Nombre + " " + pasajero.Apellido + ", ");
                            }
                        }
                    }
                }
            }
            else return;

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

        public static void CancelarReserva()
        {
            Reserva reserva = SeleccionReserva();
            if (reserva == null)
            {
                Console.WriteLine("No tiene reservas. Presione cualquier tecla para volver");
                Console.ReadKey(true);
                return;
            }

            //Program.Reservas.Remove(reserva);
            Program.usuario.Reservas.RemoveAll(r => r.Id == reserva.Id);
        }

        public static void MisReservas()
        {
            var opciones = new List<string[]>();
            opciones.Add(new string[] { "Numero", "Pasajeros", "Cantidad de asientos", "Reservante", "Numero de vuelo" });

            if (Program.usuario.Reservas.Count() == 0)
            {
                Console.WriteLine("No hay reservas existentes. Toque cualquier tecla para volver");
                Console.ReadKey(true);
                return;
            }

            // !IMPORTANT cambiar a reservas del usuario, no globales
            foreach (Reserva reserva in Program.usuario.Reservas)
            {
                string p = "";
                foreach (Pasajero pasajero in reserva.Pasajeros)
                {
                    if (reserva.Pasajeros.IndexOf(pasajero) == reserva.Pasajeros.Count() - 1)
                    {
                        p += pasajero.Nombre + " " + pasajero.Apellido;
                    }
                    else
                    {
                        p += pasajero.Nombre + " " + pasajero.Apellido + ", ";
                    }
                }

                if (p.Length > 11) p = p.Substring(0, 11) + "...";

                opciones.Add(new string[]
                {
                    reserva.Id.ToString(),
                    p,
                    reserva.CantidadAsientos.ToString(),
                    reserva.Reservante.Nombre + " " + reserva.Reservante.Apellido,
                    reserva.Vuelo.Id.ToString()
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
            Console.WriteLine("Toque cualquier tecla para volver");
            Console.ReadKey(true);
            return;
        }
    }
}
