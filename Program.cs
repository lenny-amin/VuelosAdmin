using Libreria2025;
using AdminVuelos.Modelos;


namespace AdminVuelos
{
    internal class Program
    {
        public static List<Pasajero> Pasajeros = new List<Pasajero>();
        public static List<Reserva> Reservas = new List<Reserva>();
        public static List<Vuelo> Vuelos = new List<Vuelo>();
        public static bool Login = false; 

        static void Main(string[] args)
        {
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
            string[] opciones = {"Vuelos disponibles","Reservar vuelo","Editar reserva","Cancelar reserva","Mis reservas","Volver"};
            int seleccion = Herramienta.MenuSeleccionar(opciones, 1, "Personas");
            switch (seleccion)
            {
                case 1: Menu(); break;
                case 5:Menu(); break;
            }

        }
        public static void Datos()
        {
            
        }
    }
}
