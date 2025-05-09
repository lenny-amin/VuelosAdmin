using Libreria2025;
using AdminVuelos.Modelos;
using AdminVuelos.Controladores;


namespace AdminVuelos
{
    internal class Program
    {
        public static List<Pasajero> Pasajeros = new List<Pasajero>();
        public static List<Reserva> Reservas = new List<Reserva>();
        public static List<Vuelo> Vuelos = new List<Vuelo>();
        public static Pasajero usuario;
        public static bool Login = true; 

        static void Main(string[] args)
        {
            Datos();
            if (Login)
            {
                MenuUsuario();
            }else { MenuRegistrarse(); }

        }
        public static void MenuRegistrarse()
        {
            Console.Clear();
            string[] opciones = {"Registrarse","Vuelos","Salir"};
            int seleccion = Herramienta.MenuSeleccionar(opciones, 1, "Personas");
            
            switch (seleccion)
            {
                case 1: MenuUsuario(); break;
                //case 2: nPasajero.MenuPasajeros(); break;
                case 3: break;
            }
        }
        public static void MenuUsuario()
        {
            Console.Clear();
            string[] opciones = {"Vuelos disponibles", "Listado por destinos y fechas","Destinos mas visitados","Reservar vuelo","Editar reserva","Cancelar reserva","Mis reservas","Volver"};
            int seleccion = Herramienta.MenuSeleccionar(opciones, 1, "Personas");
            switch (seleccion)
            {
                case 1: VueloControlador.ListarVuelo(); MenuUsuario(); break;
                case 2: VueloControlador.ListadoPorDestino(); MenuUsuario(); break;
                case 3: VueloControlador.DestinosMasVisitados(); MenuUsuario(); break;
                case 4: ReservaControlador.Reservar(); MenuUsuario(); break;
                case 5: ReservaControlador.Modificar(); MenuUsuario(); break;
                case 6: ReservaControlador.CancelarReserva(); MenuUsuario(); break;
                case 7: ReservaControlador.MisReservas(); MenuUsuario(); break;
            }
        }

        public static void Error(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey(true);
            return;
        }
        public static void Datos()
        {
            Vuelos.Add(new Vuelo(1, "Buenos Aires", "Sao Paolo", new DateTime(2025, 06, 14), new TimeOnly(5, 0, 0), 10));
            Vuelos.Add(new Vuelo(2, "Istanbul", "Moscu", new DateTime(2025, 02, 13), new TimeOnly(17, 30, 0), 10));
            Vuelos.Add(new Vuelo(3, "Moscu", "Sao Paolo", new DateTime(2025, 06, 14), new TimeOnly(13, 30, 0), 2));
            Pasajeros.Add(new Pasajero(1, "Samuel", "Peter", "48756921"));
            Pasajeros.Add(new Pasajero(2, "Abiel", "Moreno", "48756921"));
            Pasajeros.Add(new Pasajero(3, "Leny", "Amin", "48756921"));
            Pasajeros.Add(new Pasajero(4, "Jared", "Peter", "48734921"));

            //Reservas.Add(new Reserva(1, new List<Pasajero> { usuario, Pasajeros[0] }, 2, usuario, Vuelos[0]));
            //Reservas.Add(new Reserva(2, new List<Pasajero> { usuario, Pasajeros[1], Pasajeros[2] }, 3, usuario, Vuelos[1]));

            usuario = Pasajeros[Pasajeros.Count - 1];

            //Reservas.Add(new Reserva(1, new List<Pasajero> { usuario, Pasajeros[0] }, 2, usuario, Vuelos[0]));
            //Reservas.Add(new Reserva(2, new List<Pasajero> { usuario, Pasajeros[1], Pasajeros[2] }, 3, usuario, Vuelos[1]));
            //poner mas datos hardcodeados
            
        }
    }
}
