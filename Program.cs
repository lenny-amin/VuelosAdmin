using Libreria2023;
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
                MenuReservas();
            }else { Menu(); }

        }
        public static void Menu()
        {
            Console.Clear();
            string[] opciones = {"Registrarse","Vuelos","Salir"};
            Herramientas.DibujoMenu("Opciones", opciones);
            Console.Write("Seleccione: ");
            int seleccion = Herramientas.IngresoEnteros(1, opciones.Length);
            switch (seleccion)
            {
                case 1: MenuReservas(); break;
                //case 2: nPasajero.MenuPasajeros(); break;
                case 3: break;
            }
        }
        public static void MenuReservas()
        {
            Console.Clear();
            string[] opciones = {"Vuelos disponibles","Reservar vuelo","Editar reserva","Cancelar reserva","Mis reservas","Volver"};
            Herramientas.DibujoMenu("Opciones", opciones);
            Console.Write("Seleccione: ");
            int seleccion = Herramientas.IngresoEnteros(1, opciones.Length);
            switch (seleccion)
            {
                case 1: Menu(); break;
                case 5:Menu(); break;
            }

        }
        public static void Datos()
        {
            pu
        }
    }
}
