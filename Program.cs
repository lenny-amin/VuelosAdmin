using Libreria2023;
using AdminVuelos.Modelos;
using AdminVuelos.Controladores;


namespace AdminVuelos
{
    internal class Program
    {
        public static List<Pasajero> Pasajeros = new List<Pasajero>();
        public static List<Reserva> Reservas = new List<Reserva>();
        public static List<Vuelo> Vuelos = new List<Vuelo>();
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
            Herramientas.DibujoMenu("Opciones", opciones);
            Console.Write("Seleccione: ");
            int seleccion = Herramientas.IngresoEnteros(1, opciones.Length);
            switch (seleccion)
            {
                case 1: MenuRegistrarse(); break;
                //case 2: nPasajero.MenuPasajeros(); break;
                case 3: break;
            }
        }
        public static void MenuUsuario()
        {
            Console.Clear();
            string[] opciones = {"Vuelos disponibles","Reservar vuelo","Editar reserva","Cancelar reserva","Mis reservas","Volver"};
            Herramientas.DibujoMenu("Opciones", opciones);
            Console.Write("Seleccione: ");
            int seleccion = Herramientas.IngresoEnteros(1, opciones.Length);
            switch (seleccion)
            {
                case 1: Menu(); break;
                case 2: VueloControlador.Reservar(); MenuUsuario(); break;
                case 5:Menu(); break;
            }

        }
        public static void Datos()
        {
            //pu
            Vuelos.Add(new Vuelo(1, "Buenos Aires", "Sao Paolo", new DateTime(2025, 06, 14), new TimeOnly(5), 10));

        }
    }
}
