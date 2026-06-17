// ============================================
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
    // TODO: Implementar lógica del día
    // 1. Llamar GenerarClientes()
    // 2. Llamar ProcesarPedidos() con la cantidad de clientes
    // 3. Llamar GenerarEvento()
    // 4. Calcular gastos del día (salarios de empleados)
    // 5. Actualizar dinero
    // 6. Llamar ActualizarReputacion()
    // 7. Llamar GuardarDia()
    // 8. Llamar MostrarResultadoDia()

    Console.WriteLine("[EjecutarDia] pendiente de implementar.");
    Console.ReadLine();
}

void AbrirTienda()
{
    // TODO: Mostrar submenú:
    // [1] Comprar ingredientes → ComprarIngredientes()
    // [2] Contratar empleado  → ContratarEmpleado()
    // [3] Agregar mesa        → AgregarMesa()
    // [4] Volver

    Console.WriteLine("[AbrirTienda] pendiente de implementar.");
    Console.ReadLine();
}

int GenerarClientes()
{
    // TODO: Calcular clientes según reputacion, mesas y factor aleatorio
    // Fórmula sugerida: base = mesas * empleados
    //                   bonus = reputacion / 20
    //                   aleatorio = rng.Next(-2, 3)
    //                   return base + bonus + aleatorio

    Console.WriteLine("[GenerarClientes] pendiente de implementar.");
    return 0;
}

double ProcesarPedidos(int cantidadClientes)
{
    // TODO: Por cada cliente:
    //   - Elegir platillo aleatorio: rng.Next(0, nombresPlatillos.Length)
    //   - Si hay ingredientes suficientes: sumar precio, restar costoIngredientes
    //   - Si no hay: clientesPerdidos++, bajar reputacion
    // Retornar total de ganancias

    Console.WriteLine("[ProcesarPedidos] pendiente de implementar.");
    return 0.0;
}

void ActualizarReputacion(int clientesAtendidos, int clientesPerdidos)
{
    // TODO: Si clientesAtendidos > clientesPerdidos: reputacion += 2
    //       Si clientesPerdidos > clientesAtendidos: reputacion -= 3
    //       Mantener entre 0 y 100

    Console.WriteLine("[ActualizarReputacion] pendiente de implementar.");
}

void GenerarEvento()
{
    // TODO: Si rng.Next(0, 100) < 30 (30% de probabilidad):
    //   - Si rng.Next(0,2) == 0: evento positivo
    //     → elegir de eventosPositivos, aplicar efecto positivo
    //   - Si no: evento negativo
    //     → elegir de eventosNegativos, aplicar efecto negativo
    //   - Llamar MostrarEvento() con la descripción

    Console.WriteLine("[GenerarEvento] pendiente de implementar.");
}

bool VerificarDerrota()
{
    // TODO: Si dinero <= 0, mostrar mensaje y retornar true
    //       Si no, retornar false

    return false;
}

void ComprarIngredientes()
{
    // TODO: Mostrar precio por unidad (ej: C$5 cada una)
    //       Preguntar cuántas unidades quiere comprar
    //       Verificar que tenga dinero: dinero >= cantidad * 5
    //       Si tiene: dinero -= cantidad * 5, ingredientes += cantidad
    //       Si no: mostrar mensaje de error

    Console.WriteLine("[ComprarIngredientes] pendiente de implementar.");
    Console.ReadLine();
}

void ContratarEmpleado()
{
    // TODO: Costo de contratación: C$100
    //       Verificar que dinero >= 100
    //       Si tiene: dinero -= 100, empleados++
    //       Si no: mostrar mensaje de error

    Console.WriteLine("[ContratarEmpleado] pendiente de implementar.");
    Console.ReadLine();
}

void AgregarMesa()
{
    // TODO: Costo de mesa: C$150
    //       Verificar que dinero >= 150
    //       Si tiene: dinero -= 150, mesas++
    //       Si no: mostrar mensaje de error

    Console.WriteLine("[AgregarMesa] pendiente de implementar.");
    Console.ReadLine();
}

// ============================================
// DATOS — Mario
// Guardado, carga y estadísticas
// ============================================

    // TODO: Crear línea CSV:
    //       numeroDia,ganancias,gastos,clientesAtendidos,clientesPerdidos,reputacion
    //       Agregar al archivo con File.AppendAllText(rutaHistorial, linea + "\n")


    // TODO: Si !File.Exists(rutaHistorial): mostrar "Sin historial aún"
    //       Si existe: leer con File.ReadAllLines(rutaHistorial)
    //       Para cada línea: Split(',') y mostrar los datos formateados

    
    // TODO: Leer historial, recorrer con ciclo
    //       Guardar la mayor ganancia encontrada
    //       Retornar ese valor


    // TODO: Leer historial, sumar todas las ganancias
    //       Dividir entre cantidad de días jugados
    //       Retornar el promedio


    // TODO: Llamar ObtenerMejorDia() y ObtenerPromedioGanancias()
    //       Mostrar mejor día, promedio, y si el jugador está mejorando



void GuardarDia(int numeroDia, double ganancias, double gastos, int clientesAtendidos, int clientesPerdidos)
{
    try
    {
        StreamWriter archivo = new StreamWriter(rutaHistorial, true); // true = append
        archivo.WriteLine($"{numeroDia},{ganancias},{gastos},{clientesAtendidos},{clientesPerdidos},{reputacion}");
        archivo.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al guardar el día: {ex.Message}");
    }
}

void MostrarHistorial()
{
    if (!File.Exists(rutaHistorial))
    {
        Console.WriteLine("\nSin historial aún, avanzá en el juego!.");
        Console.ReadLine();
        return;
    }

    try
    {
        string[] lineas = File.ReadAllLines(rutaHistorial);

        Console.WriteLine("\n===== HISTORIAL =====");

        for (int i = 0; i < lineas.Length; i++)
        {
            string[] datos = lineas[i].Split(',');

            int numeroDia = int.Parse(datos[0]);
            double ganancias = double.Parse(datos[1]);
            double gastos = double.Parse(datos[2]);
            int clientesAtendidos = int.Parse(datos[3]);
            int clientesPerdidos = int.Parse(datos[4]);
            int reputacionDia = int.Parse(datos[5]);

            Console.WriteLine($"Día {numeroDia}: Ganancias C${ganancias} | Gastos C${gastos} | Clientes Atendidos {clientesAtendidos} | Clientes Perdidos {clientesPerdidos} | Reputación {reputacionDia}");
        }

        Console.WriteLine("======================");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al leer el historial: {ex.Message}");
    }

    Console.ReadLine();
}

double ObtenerMejorDia()
{
    if (!File.Exists(rutaHistorial))
        return 0.0;

    double mejorGanancia = 0.0;

    try
    {
        string[] lineas = File.ReadAllLines(rutaHistorial);

        for (int i = 0; i < lineas.Length; i++)
        {
            string[] datos = lineas[i].Split(',');
            double ganancias = double.Parse(datos[1]);

            if (ganancias > mejorGanancia)
                mejorGanancia = ganancias;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al calcular el mejor día: {ex.Message}");
    }

    return mejorGanancia;
}

double ObtenerPromedioGanancias()
{
    if (!File.Exists(rutaHistorial))
        return 0.0;

    double sumaGanancias = 0.0;
    int diasJugados = 0;

    try
    {
        string[] lineas = File.ReadAllLines(rutaHistorial);

        for (int i = 0; i < lineas.Length; i++)
        {
            string[] datos = lineas[i].Split(',');
            sumaGanancias = sumaGanancias + double.Parse(datos[1]);
            diasJugados = diasJugados + 1;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al calcular el promedio: {ex.Message}");
    }

    if (diasJugados == 0)
        return 0.0;

    return sumaGanancias / diasJugados;
}

void MostrarProgreso()
{
    double mejorDia = ObtenerMejorDia();
    double promedio = ObtenerPromedioGanancias();

    Console.WriteLine("\n--- Progreso ---");
    Console.WriteLine($"Mejor día: C${mejorDia}");
    Console.WriteLine($"Promedio de ganancias: C${promedio}");

    if (mejorDia > promedio)
        Console.WriteLine("Vas mejorando, ¡seguí así!");
    else
        Console.WriteLine("Podés mejorar tus ganancias diarias, seguí intentando.");

    Console.WriteLine("------------------------");
    Console.ReadLine();
}


