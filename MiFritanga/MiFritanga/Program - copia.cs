x// ============================================
// MiFritanga - Juego Tycoon
// Program.cs — Archivo principal
// ============================================

// ============================================
// CONTENIDO — Miguel
// Platillos, eventos y textos del juego
// ============================================

string[] nombresPlatillos = {
    "Vigorón", "Nacatamal", "Quesillo", "Fritanga mixta",
    "Tajadas con queso", "Sopa de res", "Güirila con cuajada",
    "Maduro frito", "Enchilada", "Chancho con yuca"
};

double[] preciosPlatillos = {
    60.0, 50.0, 30.0, 80.0, 40.0,
    90.0, 35.0, 25.0, 45.0, 70.0
};

int[] costoIngredientes = {
    3, 4, 2, 5, 2, 6, 2, 1, 3, 5
};

string[] eventosPositivos = {
    "¡Un grupo de turistas visitó tu fritanga! Más clientes hoy.",
    "¡Día de quincena! La gente tiene más plata para gastar.",
    "¡Un periodista recomendó tu fritanga en las redes!",
    "¡Un proveedor te dio descuento en ingredientes hoy!",
    "¡Cliente satisfecho dejó propina extra!"
};

string[] eventosNegativos = {
    "Inspección sanitaria. Tuviste que cerrar 2 horas.",
    "Se fue la luz. Perdiste algunos ingredientes.",
    "Un empleado llegó tarde. Atendiste menos clientes.",
    "Lluvia fuerte. Pocos clientes salieron hoy.",
    "Se subió el precio de los ingredientes esta semana."
};

// ============================================
// ESTADO GLOBAL DEL JUEGO — Rafael
// ============================================

int dia = 1;
double dinero = 500.0;
int reputacion = 50;
int mesas = 3;
int empleados = 2;
int ingredientes = 100;
bool juegoActivo = true;
int diasTotales = 30;
Random rng = new Random();
string rutaHistorial = "historial.csv";

// ============================================
// BUCLE PRINCIPAL — Rafael
// ============================================

MostrarBienvenida();

while (juegoActivo && dia <= diasTotales)
{
    MostrarEstado();
    MostrarMenuPrincipal();

    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            EjecutarDia();
            dia++;
            break;
        case "2":
            AbrirTienda();
            break;
        case "3":
            MostrarHistorial();
            break;
        case "4":
            juegoActivo = false;
            break;
        default:
            Console.WriteLine("Opción no válida. Intentá de nuevo.");
            break;
    }

    if (VerificarDerrota())
        juegoActivo = false;
}

MostrarResultadoFinal();

// ============================================
// UI — Rafael
// Mensajes, menús y display en consola
// ============================================

void MostrarBienvenida()
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║         BIENVENIDO A MIFRITANGA      ║");
    Console.WriteLine("║   ¡Administrá tu propia fritanga!    ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.WriteLine("\nPresioná ENTER para comenzar...");
    Console.ReadLine();
}

void MostrarMenuPrincipal()
{
    Console.WriteLine("\n¿Qué hacés hoy?");
    Console.WriteLine("[1] Abrir la fritanga (iniciar el día)");
    Console.WriteLine("[2] Administrar (comprar, contratar, expandir)");
    Console.WriteLine("[3] Ver historial y progreso");
    Console.WriteLine("[4] Salir");
    Console.Write("\nOpción: ");
}

void MostrarEstado()
{
    Console.WriteLine("\n══════════════════════════════════════");
    Console.WriteLine($"  Día: {dia}/{diasTotales}");
    Console.WriteLine($"  Dinero:       C${dinero:F2}");
    Console.WriteLine($"  Reputación:   {reputacion}/100");
    Console.WriteLine($"  Mesas:        {mesas}");
    Console.WriteLine($"  Empleados:    {empleados}");
    Console.WriteLine($"  Ingredientes: {ingredientes} unidades");
    Console.WriteLine("══════════════════════════════════════");
}

void MostrarResultadoDia(double ganancias, double gastos, int clientesAtendidos, int clientesPerdidos)
{
    Console.WriteLine("\n--- Resumen del día ---");
    Console.WriteLine($"Clientes atendidos: {clientesAtendidos}");
    Console.WriteLine($"Clientes perdidos:  {clientesPerdidos}");
    Console.WriteLine($"Ganancias:  C${ganancias:F2}");
    Console.WriteLine($"Gastos:     C${gastos:F2}");
    Console.WriteLine($"Balance:    C${ganancias - gastos:F2}");
    Console.WriteLine("------------------------");
    Console.WriteLine("Presioná ENTER para continuar...");
    Console.ReadLine();
}

void MostrarEvento(string descripcion)
{
    Console.WriteLine($"\n*** EVENTO: {descripcion} ***");
}

void MostrarResultadoFinal()
{
    Console.Clear();
    if (dia > diasTotales)
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║        ¡SOBREVIVISTE 30 DÍAS!        ║");
        Console.WriteLine("║       Tu fritanga es un éxito!       ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
    }
    else
    {
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║          CERRASTE LA FRITANGA        ║");
        Console.WriteLine("║        Se te acabó el dinero...      ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
    }
    Console.WriteLine($"\nDinero final:       C${dinero:F2}");
    Console.WriteLine($"Reputación final:   {reputacion}/100");
    Console.WriteLine($"Días sobrevividos:  {dia - 1}");
    Console.WriteLine("\nPresioná ENTER para salir...");
    Console.ReadLine();
}

// ============================================
// JUEGO — Carlos
// Lógica principal: clientes, pedidos, eventos
// ============================================

void EjecutarDia()
{
    // 1. Llamar GenerarClientes()
    int clientesGenerados = GenerarClientes();

    // 2. Llamar ProcesarPedidos() con la cantidad de clientes
    double gananciasDia = ProcesarPedidos(clientesGenerados);

    // Variables temporales (ya que ProcesarPedidos solo retorna el dinero)
    int clientesAtendidos = clientesGenerados;
    int clientesPerdidos = 0;

    // 3. Llamar GenerarEvento()
    GenerarEvento();

    // 4. Calcular gastos del día (salarios de empleados)
    // Asumiremos un salario de C$50 al día por cada empleado
    double gastosDia = empleados * 50.0;

    // 5. Actualizar dinero
    dinero += (gananciasDia - gastosDia);

    // 6. Llamar ActualizarReputacion()
    ActualizarReputacion(clientesAtendidos, clientesPerdidos);

    // 7. Llamar GuardarDia()
    GuardarDia(dia, gananciasDia, gastosDia, clientesAtendidos, clientesPerdidos);

    // 8. Llamar MostrarResultadoDia()
    MostrarResultadoDia(gananciasDia, gastosDia, clientesAtendidos, clientesPerdidos);
}
void AbrirTienda()
{
    bool enTienda = true;

    while (enTienda)
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         ADMINISTRAR FRITANGA         ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        // Mostramos el dinero actual para ayudar al jugador a decidir
        Console.WriteLine($"Dinero disponible: C${dinero:F2}\n");

        Console.WriteLine("[1] Comprar ingredientes");
        Console.WriteLine("[2] Contratar empleado");
        Console.WriteLine("[3] Agregar mesa");
        Console.WriteLine("[4] Volver al menú principal");
        Console.Write("\nElegí una opción: ");

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                ComprarIngredientes();
                break;
            case "2":
                ContratarEmpleado();
                break;
            case "3":
                AgregarMesa();
                break;
            case "4":
                enTienda = false; // Rompe el bucle y regresa al menú principal
                break;
            default:
                Console.WriteLine("Opción no válida. Presioná ENTER para intentar de nuevo.");
                Console.ReadLine();
                break;
        }
    }
}

int GenerarClientes()
{
    // Calculamos la base de clientes según la capacidad (mesas y empleados)
    int clientesBase = mesas * empleados;

    // Un bonus por buena reputación
    int bonus = reputacion / 20;

    // El factor aleatorio (suerte del día). rng.Next(-2, 3) da un número entre -2 y 2.
    int aleatorio = rng.Next(-2, 3);

    // Sumamos todo
    int totalClientes = clientesBase + bonus + aleatorio;

    // Evitamos que por mala suerte tengamos clientes negativos
    if (totalClientes < 0)
    {
        totalClientes = 0;
    }

    return totalClientes;
}

double ProcesarPedidos(int cantidadClientes)
{
    double gananciasDia = 0.0;
    int clientesPerdidos = 0;

    for (int i = 0; i < cantidadClientes; i++)
    {
        // 1. Elegir un platillo aleatorio
        int indicePlatillo = rng.Next(0, nombresPlatillos.Length);
        int costo = costoIngredientes[indicePlatillo];

        // 2. Verificar si hay ingredientes suficientes
        if (ingredientes >= costo)
        {
            // Atendemos al cliente
            ingredientes -= costo;
            gananciasDia += preciosPlatillos[indicePlatillo];
        }
        else
        {
            // No hay ingredientes: cliente perdido
            clientesPerdidos++;
        }
    }

    // Si hubo clientes perdidos, avisamos en consola (esto es útil para debuguear)
    if (clientesPerdidos > 0)
    {
        Console.WriteLine($"[!] Atención: {clientesPerdidos} clientes se fueron porque no había ingredientes.");
    }

    return gananciasDia;
}

vvoid ActualizarReputacion(int clientesAtendidos, int clientesPerdidos)
{
    // Evaluamos si el día fue mayormente bueno o malo
    if (clientesAtendidos > clientesPerdidos)
    {
        reputacion += 2; // Sube la reputación si atendiste a la mayoría
    }
    else if (clientesPerdidos > clientesAtendidos)
    {
        reputacion -= 3; // Baja más rápido si perdiste a la mayoría
    }
    // Nota: Si son exactamente iguales, la reputación se mantiene igual.

    // Nos aseguramos de mantener la reputación entre 0 y 100
    if (reputacion > 100)
    {
        reputacion = 100;
    }
    else if (reputacion < 0)
    {
        reputacion = 0;
    }
}

void GenerarEvento()
{
    // rng.Next(0, 100) genera un número del 0 al 99. 
    // Si es menor a 30, significa que hay un 30% de probabilidad de que ocurra algo.
    if (rng.Next(0, 100) < 30)
    {
        // 50% de probabilidad de que sea positivo (0) o negativo (1)
        if (rng.Next(0, 2) == 0)
        {
            // --- EVENTO POSITIVO ---
            int indice = rng.Next(0, eventosPositivos.Length);
            string descripcion = eventosPositivos[indice];

            // Aplicar el efecto positivo dependiendo de cuál evento salió
            switch (indice)
            {
                case 0: reputacion += 5; break;      // Turistas (sube reputación)
                case 1: dinero += 100.0; break;      // Quincena (más dinero de golpe)
                case 2: reputacion += 15; break;     // Periodista (mucha reputación)
                case 3: ingredientes += 30; break;   // Proveedor (ingredientes gratis)
                case 4: dinero += 50.0; break;       // Propina (algo de dinero extra)
            }

            // Nos aseguramos de que la reputación no pase de 100 por un evento
            if (reputacion > 100) reputacion = 100;

            MostrarEvento(descripcion);
        }
        else
        {
            // --- EVENTO NEGATIVO ---
            int indice = rng.Next(0, eventosNegativos.Length);
            string descripcion = eventosNegativos[indice];

            // Aplicar el efecto negativo dependiendo de cuál evento salió
            switch (indice)
            {
                case 0: reputacion -= 10; break;     // Inspección (baja reputación)
                case 1: ingredientes -= 20; break;   // Sin luz (se arruina la comida)
                case 2: reputacion -= 5; break;      // Empleado tarde (clientes molestos)
                case 3: reputacion -= 5; break;      // Lluvia (baja un poco la reputación)
                case 4: dinero -= 50.0; break;       // Subió todo (pierdes dinero)
            }

            // Nos aseguramos de que las estadísticas no bajen de cero
            if (reputacion < 0) reputacion = 0;
            if (ingredientes < 0) ingredientes = 0;

            MostrarEvento(descripcion);
        }
    }
}

bool VerificarDerrota()
{
    // Verificamos si el jugador se quedó sin fondos
    if (dinero <= 0)
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║            ¡BANCARROTA!              ║");
        Console.WriteLine("║  Te quedaste sin plata para operar.  ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.WriteLine("Presioná ENTER para continuar...");
        Console.ReadLine();

        return true; // Retorna true para indicarle al bucle principal que el juego terminó
    }

    return false; // Retorna false porque todavía hay dinero para seguir jugando
}

void ComprarIngredientes()
{
    Console.Clear();
    Console.WriteLine("--- TIENDA DE INGREDIENTES ---");
    Console.WriteLine($"Dinero actual: C${dinero:F2}");
    Console.WriteLine($"Ingredientes actuales: {ingredientes} unidades");
    Console.WriteLine("Precio por unidad: C$5.00");
    Console.WriteLine("----------------------------------------");

    // Preguntar cuántas unidades quiere comprar
    Console.Write("¿Cuántas unidades de ingredientes querés comprar?: ");
    string entrada = Console.ReadLine();

    if (int.TryParse(entrada, out int cantidad) && cantidad > 0)
    {
        // Verificar que tenga dinero: dinero >= cantidad * 5
        double costoTotal = cantidad * 5.0;
        Console.WriteLine($"Costo total por {cantidad} unidades: C${costoTotal:F2}");

        if (dinero >= costoTotal)
        {
            // Si tiene: dinero -= cantidad * 5, ingredientes += cantidad
            dinero -= costoTotal;
            ingredientes += cantidad;
            Console.WriteLine("\n¡Compra exitosa! Los ingredientes ya están en bodega.");
        }
        else
        {
            // Si no: mostrar mensaje de error
            Console.WriteLine("\n¡Error! No tenés suficiente dinero para esta compra.");
        }
    }
    else
    {
        Console.WriteLine("\n¡Cantidad inválida! Debés ingresar un número entero mayor a cero.");
    }

    Console.WriteLine("\nPresioná ENTER para regresar al menú...");
    Console.ReadLine();
}

void ContratarEmpleado()
{
    double costoContratacion = 100.0;

    Console.WriteLine("\n--- CONTRATAR EMPLEADO ---");

    // Verificamos si hay suficiente dinero en la caja
    if (dinero >= costoContratacion)
    {
        dinero -= costoContratacion; // Restamos el costo
        empleados++;                 // Sumamos un trabajador al equipo

        Console.WriteLine("¡Excelente! Contrataste a un nuevo empleado por C$100.");
        Console.WriteLine($"Ahora tenés {empleados} empleados trabajando en la fritanga.");
    }
    else
    {
        // Mensaje de error si no ajusta la plata
        Console.WriteLine("¡Ideay! No tenés suficiente plata.");
        Console.WriteLine($"Contratar cuesta C$100 y solo tenés C${dinero:F2} en caja.");
    }

    Console.WriteLine("\nPresioná ENTER para regresar a la tienda...");
    Console.ReadLine();
}

void AgregarMesa()
{
    double costoMesa = 150.0;

    Console.WriteLine("\n--- AGREGAR MESA ---");

    // Verificamos si hay suficiente dinero para comprar la mesa
    if (dinero >= costoMesa)
    {
        dinero -= costoMesa; // Restamos el costo de la caja
        mesas++;             // Sumamos una mesa al local

        Console.WriteLine("¡Bárbaro! Compraste una mesa nueva por C$150.");
        Console.WriteLine($"Ahora tenés {mesas} mesas en la fritanga.");
    }
    else
    {
        // Mensaje en caso de no tener fondos suficientes
        Console.WriteLine("¡Falta billete para la mesa!");
        Console.WriteLine($"Cada mesa cuesta C$150 y solo tenés C${dinero:F2}.");
    }

    Console.WriteLine("\nPresioná ENTER para regresar a la tienda...");
    Console.ReadLine();
}

// ============================================
// DATOS — Mario
// Guardado, carga y estadísticas
// ============================================

void GuardarDia(int numeroDia, double ganancias, double gastos, int clientesAtendidos, int clientesPerdidos)
{
    // TODO: Crear línea CSV:
    //       numeroDia,ganancias,gastos,clientesAtendidos,clientesPerdidos,reputacion
    //       Agregar al archivo con File.AppendAllText(rutaHistorial, linea + "\n")

    Console.WriteLine("[GuardarDia] pendiente de implementar.");
}

void MostrarHistorial()
{
    // TODO: Si !File.Exists(rutaHistorial): mostrar "Sin historial aún"
    //       Si existe: leer con File.ReadAllLines(rutaHistorial)
    //       Para cada línea: Split(',') y mostrar los datos formateados

    Console.WriteLine("[MostrarHistorial] pendiente de implementar.");
    Console.ReadLine();
}

double ObtenerMejorDia()
{
    // TODO: Leer historial, recorrer con ciclo
    //       Guardar la mayor ganancia encontrada
    //       Retornar ese valor

    return 0.0;
}

double ObtenerPromedioGanancias()
{
    // TODO: Leer historial, sumar todas las ganancias
    //       Dividir entre cantidad de días jugados
    //       Retornar el promedio

    return 0.0;
}

void MostrarProgreso()
{
    // TODO: Llamar ObtenerMejorDia() y ObtenerPromedioGanancias()
    //       Mostrar mejor día, promedio, y si el jugador está mejorando

    Console.WriteLine("[MostrarProgreso] pendiente de implementar.");
    Console.ReadLine();
}
