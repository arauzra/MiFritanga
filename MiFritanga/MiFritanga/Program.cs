// ============================================
// MiFritanga - Juego Tycoon
// Program.cs — Archivo principal
// ============================================

// ============================================
// CONTENIDO — Miguel
// ============================================

string[] nombresPlatillos = {
    "Vigorón", "Nacatamal", "Quesillo", "Fritanga mixta",
    "Tajadas con queso", "Sopa de res", "Güirila con cuajada",
    "Maduro frito", "Enchilada", "Chancho con yuca"
};

string[] descripcionesPlatillos = {
    "Yuca cocida con chicharron y ensalada de repollo.",
    "Tamal de maiz relleno de carne y arroz, envuelto en hoja de platano.",
    "Tortilla enrollada con queso seco y crema.",
    "Combinacion de carnes asadas con arroz, frijoles y tajadas.",
    "Platano maduro frito con queso seco rallado encima.",
    "Caldo abundante de res con verduras y yuca.",
    "Tortilla de maiz tierno con cuajada fresca.",
    "Platano maduro frito, dulce y suavecito.",
    "Tortilla frita rellena de carne molida y queso.",
    "Chancho cocinado con yuca y curtido de cebolla."
};

double[] preciosPlatillos = {
    120.0, 110.0, 60.0, 120.0, 70.0,
    130.0, 80.0,  30.0, 90.0, 110.0
};

// Costo por platillo: carne, galloPinto, platano, yuca, lacteos
int[,] costoPorPlatillo = {
    { 1, 0, 1, 1, 0 },  // Vigorón
    { 1, 1, 0, 0, 0 },  // Nacatamal
    { 0, 0, 0, 0, 1 },  // Quesillo
    { 1, 1, 1, 0, 0 },  // Fritanga mixta
    { 0, 0, 1, 0, 1 },  // Tajadas con queso
    { 1, 1, 0, 1, 0 },  // Sopa de res
    { 0, 0, 0, 0, 1 },  // Güirila con cuajada
    { 0, 1, 1, 0, 0 },  // Maduro frito
    { 1, 1, 0, 0, 0 },  // Enchilada
    { 1, 0, 0, 1, 0 }   // Chancho con yuca
};

int[] precioPorIngrediente = { 50, 20, 10, 22, 35 };
string[] nombresIngredientes = { "Carne", "Gallo pinto/arroz", "Platano", "Yuca", "Lacteos" };

string[] eventosPositivosAuto = {
    "Dia de quincena! Los clientes gastan mas hoy.",
    "Critico gastronomico visito tu fritanga y quedo encantado!",
    "Un proveedor te dejo de regalo un excedente de platanos."
};

string[] eventosNegativosAuto = {
    "Se fue la luz. Los lacteos se danaron.",
    "Lluvia torrencial. Casi nadie salio hoy.",
    "Se acabo el gas. Tuviste que comprar emergencia.",
    "Rata en la cocina. Perdiste ingredientes y pagaste fumigacion.",
    "Cliente se quejo en redes sociales."
};

string[] eventosConDecision = {
    "POSITIVO_PROVEEDOR", "POSITIVO_RESERVA", "POSITIVO_INFLUENCER",
    "NEGATIVO_INSPECTOR", "NEGATIVO_EMPLEADO_AUMENTO",
    "NEGATIVO_RESFRIADO", "NEGATIVO_PROVEEDOR_PRECIO"
};

string MSG_SIN_DINERO      = "No tenes suficiente dinero para eso.";
string MSG_OPCION_INVALIDA = "Opcion no valida. Intenta de nuevo.";

string rutaHistorial = "historial.csv";
string rutaPartida   = "partida.csv";

// ============================================
// ESTADO GLOBAL — Rafael
// ============================================

int    dia                      = 1;
double dinero                   = 1000.0;
int    reputacion               = 50;
int    mesas                    = 3;
int    empleados                = 2;
int    diasTotales              = 15;
int    diasEmpleadoEnfermo      = 0;
int    diasPrecioAlto           = 0;
int    diasSinIngredienteFresco = 0;
int[]  ingredientes             = { 5, 8, 5, 5, 4 };
Random rng                      = new Random();

// ============================================
// MENÚ DE INICIO — Rafael
// ============================================

MostrarMenuInicio();

void MostrarMenuInicio()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╔══════════════════════════════════════════╗");
    Console.WriteLine("║           MIFRITANGA                     ║");
    Console.WriteLine("╚══════════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("  [1] Nueva partida");
    Console.WriteLine("  [2] Continuar partida guardada");
    Console.WriteLine("  [3] Salir");
    Console.Write("\n  Opcion: ");

    string op = Console.ReadLine();

    if (op == "1")
    {
        // Borrar historial y partida anteriores
        if (File.Exists(rutaHistorial)) File.Delete(rutaHistorial);
        if (File.Exists(rutaPartida))   File.Delete(rutaPartida);
        MostrarIntroduccion();
        IniciarBucle();
    }
    else if (op == "2")
    {
        if (File.Exists(rutaPartida))
        {
            CargarPartida();
            MostrarExito("Partida cargada correctamente!");
            IniciarBucle();
        }
        else
        {
            MostrarError("No hay ninguna partida guardada.");
            Console.ReadLine();
            MostrarMenuInicio();
        }
    }
}

void MostrarIntroduccion()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╔══════════════════════════════════════════╗");
    Console.WriteLine("║        TU HISTORIA COMIENZA ACÁ          ║");
    Console.WriteLine("╚══════════════════════════════════════════╝");
    Console.ResetColor();
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("  Toda tu vida soñaste con tener tu propio negocio.");
    Console.WriteLine("  Después de años ahorrando córdoba a córdoba,");
    Console.WriteLine("  llegó el momento: abrís tu fritanga en el barrio.");
    Console.WriteLine();
    Console.WriteLine("  Tenés C$1,000, tres mesas, dos empleados y");
    Console.WriteLine("  suficientes ingredientes para arrancar.");
    Console.WriteLine("  El barrio ya sabe que abrís hoy.");
    Console.WriteLine();
    Console.WriteLine("  La meta es simple: sobrevivir 15 días sin quebrar.");
    Console.WriteLine("  Pero en una fritanga nada es simple.");
    Console.WriteLine("  Los clientes son exigentes, los imprevistos son reales");
    Console.WriteLine("  y la competencia no perdona.");
    Console.WriteLine();
    Console.WriteLine("  ¿Podés mantener tu fritanga a flote?");
    Console.ResetColor();
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("  Presiona ENTER para abrir las puertas...");
    Console.ResetColor();
    Console.ReadLine();
}

void IniciarBucle()
{
    while (dia <= diasTotales)
    {
        AplicarEfectosActivos();
        MostrarEstado();
        MostrarMenuPrincipal();

        string opcion = Console.ReadLine();

        switch (opcion)
        {
            case "1":
                EjecutarDia();
                GuardarPartida();
                dia++;
                break;
            case "2":
                AbrirTienda();
                break;
            case "3":
                MostrarHistorial();
                break;
            case "4":
                GuardarPartida();
                MostrarExito("Partida guardada. Hasta luego!");
                Console.ReadLine();
                return;
            default:
                MostrarError(MSG_OPCION_INVALIDA);
                break;
        }

        if (VerificarDerrota()) return;
    }

    MostrarResultadoFinal();
}

// ============================================
// UI — Miguel
// ============================================

void MostrarMenuPrincipal()
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("  Que haces hoy?");
    Console.ResetColor();
    Console.WriteLine("  [1] Abrir la fritanga  (iniciar el dia)");
    Console.WriteLine("  [2] Administrar        (comprar, contratar, expandir)");
    Console.WriteLine("  [3] Ver historial y progreso");
    Console.WriteLine("  [4] Guardar y salir");
    Console.Write("\n  Opcion: ");
}

void MostrarEstado()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("══════════════════════════════════════════");
    Console.ResetColor();
    Console.WriteLine($"  Dia:          {dia} / {diasTotales}");
    Console.WriteLine($"  Dinero:       C${dinero:F2}");

    int barraLlena = reputacion / 10;
    string barra = "[" + new string('*', barraLlena) + new string('-', 10 - barraLlena) + "]";
    Console.Write($"  Reputacion:   {barra} {reputacion}/100");

    if (reputacion > 80)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  <- Excelente! Mucha gente quiere venir.");
    }
    else if (reputacion > 60)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  <- Buena. Tenes clientela fija.");
    }
    else if (reputacion > 40)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  <- Regular. Podria mejorar.");
    }
    else if (reputacion > 20)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  <- Mala. Poca gente se acerca.");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  <- PELIGRO! Un dia mas asi y cerras.");
    }
    Console.ResetColor();

    Console.WriteLine($"  Mesas:        {mesas}");
    Console.WriteLine($"  Empleados:    {empleados}");

    if (diasEmpleadoEnfermo > 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"                (1 empleado enfermo, {diasEmpleadoEnfermo} dia(s) restantes)");
        Console.ResetColor();
    }

    Console.WriteLine();
    Console.WriteLine("  INGREDIENTES:");
    for (int i = 0; i < nombresIngredientes.Length; i++)
    {
        Console.Write($"    {nombresIngredientes[i],-14}: {ingredientes[i],3} porciones");
        if (ingredientes[i] == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  AGOTADO!");
        }
        else if (ingredientes[i] <= 3)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  Poco!");
        }
        Console.ResetColor();
        Console.WriteLine();
    }

    if (diasPrecioAlto > 0)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n  ! Precios de ingredientes +20% por {diasPrecioAlto} dia(s) mas.");
        Console.ResetColor();
    }
    if (diasSinIngredienteFresco > 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  ! Sin proveedor de carne ni lacteos por {diasSinIngredienteFresco} dia(s) mas.");
        Console.ResetColor();
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("══════════════════════════════════════════");
    Console.ResetColor();
}

void MostrarResultadoDia(double ganancias, double gastos, int clientesAtendidos, int clientesPerdidos)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("  --- RESUMEN DEL DIA ---");
    Console.ResetColor();
    Console.WriteLine($"  Clientes atendidos:  {clientesAtendidos}");

    if (clientesPerdidos > 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  Clientes perdidos:   {clientesPerdidos}  <- Se fueron sin comer!");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  Clientes perdidos:   0  <- Perfecto!");
        Console.ResetColor();
    }

    Console.WriteLine($"  Ganancias:           C${ganancias:F2}");
    Console.WriteLine($"  Gastos:              C${gastos:F2}");

    double balance = ganancias - gastos;
    Console.Write("  Balance del dia:     ");
    if (balance >= 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"C${balance:F2}  OK");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"C${balance:F2}  NEGATIVO");
    }
    Console.ResetColor();
    Console.WriteLine($"  Reputacion actual:   {reputacion}/100");
    Console.WriteLine("  -----------------------");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("  Presiona ENTER para continuar...");
    Console.ResetColor();
    Console.ReadLine();
}

void MostrarEvento(string descripcion, bool esPositivo)
{
    Console.WriteLine();
    Console.ForegroundColor = esPositivo ? ConsoleColor.Green : ConsoleColor.Red;
    Console.WriteLine("  ╔══════════════════════════════════════╗");
    Console.WriteLine(esPositivo
        ? "  ║         BUENAS NOTICIAS              ║"
        : "  ║            MALA SUERTE               ║");
    Console.WriteLine("  ╠══════════════════════════════════════╣");
    // Truncar si es muy largo
    string texto = descripcion.Length > 36 ? descripcion.Substring(0, 36) : descripcion;
    Console.WriteLine($"  ║  {texto.PadRight(36)}  ║");
    Console.WriteLine("  ╚══════════════════════════════════════╝");
    Console.ResetColor();
}

void MostrarResultadoFinal()
{
    Console.Clear();
    if (dia > diasTotales)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║   SOBREVIVISTE LOS 15 DIAS!              ║");
        Console.WriteLine("║   Tu fritanga es un exito!               ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║   CERRASTE LA FRITANGA                   ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
    }
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine($"  Dinero final:        C${dinero:F2}");
    Console.WriteLine($"  Reputacion final:    {reputacion}/100");
    Console.WriteLine($"  Dias sobrevividos:   {dia - 1}");
    Console.WriteLine();

    Console.Write("  Calificacion:  ");
    if (reputacion >= 80 && dia > diasTotales)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("*** FRITANGUERA LEGENDARIA");
    }
    else if (reputacion >= 60)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("**  Fritanguera Reconocida");
    }
    else if (reputacion >= 30)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("*   Fritanguera del Barrio");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("    Fritanguera Principiante");
    }
    Console.ResetColor();
    Console.WriteLine();

    if (File.Exists(rutaPartida)) File.Delete(rutaPartida);

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("  Presiona ENTER para salir...");
    Console.ResetColor();
    Console.ReadLine();
}

void MostrarError(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n  X  {mensaje}");
    Console.ResetColor();
}

void MostrarExito(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n  OK  {mensaje}");
    Console.ResetColor();
}

void MostrarNotificacion(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n  !  {mensaje}");
    Console.ResetColor();
}

// ============================================
// JUEGO — Carlos
// ============================================

void EjecutarDia()
{
    int clientesAtendidos = 0;
    int clientesPerdidos  = 0;

    int cantidadClientes = GenerarClientes();
    MostrarNotificacion($"Hoy llegaron {cantidadClientes} clientes a la fritanga.");

    double ganancias = ProcesarPedidos(cantidadClientes, ref clientesAtendidos, ref clientesPerdidos);

    GenerarEvento();

    double salarios = empleados * 60.0;
    if (diasEmpleadoEnfermo > 0) salarios += 80.0;
    double gastos = 50.0 + salarios;
    dinero += ganancias - gastos;

    ActualizarReputacion(clientesAtendidos, clientesPerdidos);
    GuardarDia(dia, ganancias, gastos, clientesAtendidos, clientesPerdidos);
    MostrarResultadoDia(ganancias, gastos, clientesAtendidos, clientesPerdidos);
}

void AplicarEfectosActivos()
{
    if (diasEmpleadoEnfermo      > 0) diasEmpleadoEnfermo--;
    if (diasPrecioAlto           > 0) diasPrecioAlto--;
    if (diasSinIngredienteFresco > 0) diasSinIngredienteFresco--;
}

void AbrirTienda()
{
    bool enTienda = true;
    while (enTienda)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  --- ADMINISTRAR ---");
        Console.ResetColor();
        Console.WriteLine("  [1] Comprar ingredientes");
        Console.WriteLine("  [2] Contratar empleado    (C$150)");
        Console.WriteLine("  [3] Agregar mesa          (C$200)");
        Console.WriteLine("  [4] Volver");
        Console.Write("\n  Opcion: ");

        switch (Console.ReadLine())
        {
            case "1": ComprarIngredientes(); break;
            case "2": ContratarEmpleado();   break;
            case "3": AgregarMesa();         break;
            case "4": enTienda = false;      break;
            default:  MostrarError(MSG_OPCION_INVALIDA); break;
        }
    }
}

int GenerarClientes()
{
    // Base de clientes según etapa del juego
    int clientesBase;
    if (dia <= 5)
    {
        // Etapa 1: inicio
        if      (reputacion > 80) clientesBase = 8;
        else if (reputacion > 60) clientesBase = 6;
        else if (reputacion > 40) clientesBase = 4;
        else                      clientesBase = 2;
    }
    else if (dia <= 10)
    {
        // Etapa 2: crecimiento
        if      (reputacion > 80) clientesBase = 14;
        else if (reputacion > 60) clientesBase = 11;
        else if (reputacion > 40) clientesBase = 8;
        else                      clientesBase = 4;
    }
    else
    {
        // Etapa 3: pico
        if      (reputacion > 80) clientesBase = 20;
        else if (reputacion > 60) clientesBase = 16;
        else if (reputacion > 40) clientesBase = 12;
        else                      clientesBase = 6;
    }

    // Factor aleatorio +-2
    int aleatorio = rng.Next(-2, 3);

    // Penalización si hay empleado enfermo
    if (diasEmpleadoEnfermo > 0) clientesBase -= 2;

    int total = clientesBase + aleatorio;
    if (total < 0) total = 0;
    return total;
}

double ProcesarPedidos(int cantidadClientes, ref int clientesAtendidos, ref int clientesPerdidos)
{
    double ganancias = 0.0;

    for (int i = 0; i < cantidadClientes; i++)
    {
        int indice = rng.Next(0, nombresPlatillos.Length);

        bool puedePreparar = true;
        for (int j = 0; j < 5; j++)
        {
            if (costoPorPlatillo[indice, j] > 0 && ingredientes[j] < costoPorPlatillo[indice, j])
            {
                puedePreparar = false;
                break;
            }
        }

        if (puedePreparar)
        {
            for (int j = 0; j < 5; j++)
                ingredientes[j] -= costoPorPlatillo[indice, j];

            ganancias += preciosPlatillos[indice];
            clientesAtendidos++;
        }
        else
        {
            clientesPerdidos++;
        }
    }

    if (clientesPerdidos > 0)
        MostrarError($"{clientesPerdidos} clientes se fueron por falta de ingredientes.");

    return ganancias;
}

void ActualizarReputacion(int clientesAtendidos, int clientesPerdidos)
{
    // Subida por clientes atendidos
    if (clientesAtendidos > 0)
        reputacion += 2;

    // Bajada por clientes perdidos: -5 por cada uno
    reputacion -= clientesPerdidos * 5;

    // Penalizacion adicional: -2 por cada 2 clientes perdidos
    reputacion -= (clientesPerdidos / 2) * 2;

    if (reputacion > 100) reputacion = 100;
    if (reputacion < 0)   reputacion = 0;
}

void GenerarEvento()
{
    if (rng.Next(0, 100) >= 40) return;

    bool esDecision = rng.Next(0, 2) == 0;

    if (esDecision)
    {
        ProcesarEventoConDecision(eventosConDecision[rng.Next(0, eventosConDecision.Length)]);
    }
    else
    {
        bool esPositivo = rng.Next(0, 2) == 0;
        if (esPositivo)
        {
            int idx = rng.Next(0, eventosPositivosAuto.Length);
            MostrarEvento(eventosPositivosAuto[idx], true);
            if      (idx == 0) { dinero += dinero * 0.2; MostrarNotificacion("Ganancias del dia +20%!"); }
            else if (idx == 1) { reputacion += 12; if (reputacion > 100) reputacion = 100; MostrarNotificacion("Reputacion +12!"); }
            else if (idx == 2) { ingredientes[2] += 15; MostrarNotificacion("Platano +15 porciones gratis!"); }
        }
        else
        {
            int idx = rng.Next(0, eventosNegativosAuto.Length);
            MostrarEvento(eventosNegativosAuto[idx], false);
            if      (idx == 0) { ingredientes[4] = 0; MostrarError("Todos los lacteos se danaron!"); }
            else if (idx == 1) { MostrarError("Clientes reducidos hoy por la lluvia."); }
            else if (idx == 2) { dinero -= 120; MostrarError("Pagaste C$120 de emergencia por el gas."); }
            else if (idx == 3)
            {
                ingredientes[1] -= 10; if (ingredientes[1] < 0) ingredientes[1] = 0;
                ingredientes[0] -= 8;  if (ingredientes[0] < 0) ingredientes[0] = 0;
                dinero -= 50;
                MostrarError("Perdiste ingredientes y pagaste C$50 de fumigacion.");
            }
            else if (idx == 4) { reputacion -= 12; if (reputacion < 0) reputacion = 0; MostrarError("Reputacion -12 por la queja en redes!"); }
        }
    }
}

void ProcesarEventoConDecision(string tipo)
{
    string r;

    if (tipo == "POSITIVO_PROVEEDOR")
    {
        MostrarEvento("Proveedor ofrece carne al 50%!", true);
        Console.Write("  Compras 20 porciones extra por C$500? (s/n): ");
        r = Console.ReadLine().ToLower();
        if (r == "s" && dinero >= 500) { dinero -= 500; ingredientes[0] += 20; MostrarExito("Compraste 20 porciones de carne extra!"); }
        else if (r == "s") MostrarError("No tenes suficiente dinero.");
        else MostrarNotificacion("Rechazaste la oferta.");
    }
    else if (tipo == "POSITIVO_RESERVA")
    {
        MostrarEvento("Grupo escolar quiere reservar manana!", true);
        Console.Write($"  Tenes {mesas} mesas. Necesitas 5 minimo. Aceptas? (s/n): ");
        r = Console.ReadLine().ToLower();
        if (r == "s" && mesas >= 5) { reputacion += 8; dinero += 300; MostrarExito("Reserva aceptada! +C$300 y +reputacion."); }
        else if (r == "s") MostrarError("No tenes suficientes mesas.");
        else MostrarNotificacion("Rechazaste la reserva.");
    }
    else if (tipo == "POSITIVO_INFLUENCER")
    {
        MostrarEvento("Influencer quiere promocionarte!", true);
        Console.Write("  Le das comida gratis (C$80) a cambio de +15 reputacion? (s/n): ");
        r = Console.ReadLine().ToLower();
        if (r == "s" && dinero >= 80) { dinero -= 80; reputacion += 15; if (reputacion > 100) reputacion = 100; MostrarExito("Trato hecho! Reputacion subio."); }
        else if (r == "s") MostrarError("No tenes suficiente dinero.");
        else MostrarNotificacion("Rechazaste al influencer.");
    }
    else if (tipo == "NEGATIVO_INSPECTOR")
    {
        MostrarEvento("Inspector sanitario llego!", false);
        Console.Write("  Cerras voluntariamente o te arriesgas? (c/r): ");
        r = Console.ReadLine().ToLower();
        if (r == "c") { dinero -= 170; MostrarNotificacion("Cerraste hoy. Perdiste las ganancias."); }
        else
        {
            if (rng.Next(0, 2) == 0) MostrarExito("Pasaste la inspeccion!");
            else { dinero -= 200; MostrarError("Multa de C$200 y cierre forzado."); }
        }
    }
    else if (tipo == "NEGATIVO_EMPLEADO_AUMENTO")
    {
        MostrarEvento("Tu empleado estrella pide aumento!", false);
        Console.Write("  Aceptas +C$100 permanente o lo dejas ir? (a/d): ");
        r = Console.ReadLine().ToLower();
        if (r == "a") { dinero -= 100; MostrarNotificacion("Aceptaste. Pagaras C$100 extra diario."); }
        else { empleados--; if (empleados < 1) empleados = 1; MostrarError("El empleado se fue. Menos capacidad."); }
    }
    else if (tipo == "NEGATIVO_RESFRIADO")
    {
        MostrarEvento("Empleado resfriado! 3 dias de subsidio.", false);
        diasEmpleadoEnfermo = 3;
        dinero -= 80;
        MostrarError("Perdiste un empleado por 3 dias. Pagaste C$80 de subsidio.");
    }
    else if (tipo == "NEGATIVO_PROVEEDOR_PRECIO")
    {
        MostrarEvento("Proveedor subio precios!", false);
        Console.Write("  Buscar nuevo proveedor (2 dias sin carne/lacteos) o pagar 20% mas? (b/p): ");
        r = Console.ReadLine().ToLower();
        if (r == "b") { diasSinIngredienteFresco = 2; MostrarNotificacion("Buscando proveedor. 2 dias sin carne ni lacteos."); }
        else { diasPrecioAlto = 5; MostrarNotificacion("Pagaras 20% mas por 5 dias."); }
    }
}

bool VerificarDerrota()
{
    if (dinero <= 0)
    {
        MostrarError("Te quedaste sin dinero! La fritanga cerro.");
        Console.ReadLine();
        MostrarResultadoFinal();
        return true;
    }
    if (reputacion <= 20)
    {
        MostrarError("Tu reputacion esta por el suelo. Nadie quiere comer aqui!");
        Console.ReadLine();
        MostrarResultadoFinal();
        return true;
    }
    return false;
}

void ComprarIngredientes()
{
    Console.WriteLine();
    Console.WriteLine("  Que ingrediente queres comprar?");
    for (int i = 0; i < nombresIngredientes.Length; i++)
    {
        int precio = precioPorIngrediente[i];
        if (diasPrecioAlto > 0) precio = (int)(precio * 1.2);

        string disponible = (diasSinIngredienteFresco > 0 && (i == 0 || i == 4)) ? "(NO DISPONIBLE)" : $"tenes: {ingredientes[i]}";
        Console.WriteLine($"  [{i + 1}] {nombresIngredientes[i],-14} C${precio}/porcion  {disponible}");
    }
    Console.WriteLine("  [6] Volver");
    Console.Write("\n  Opcion: ");

    string op = Console.ReadLine();
    if (op == "6") return;

    if (!int.TryParse(op, out int idx)) { MostrarError(MSG_OPCION_INVALIDA); return; }
    idx--;

    if (idx < 0 || idx > 4) { MostrarError(MSG_OPCION_INVALIDA); return; }
    if (diasSinIngredienteFresco > 0 && (idx == 0 || idx == 4)) { MostrarError("No disponible hasta conseguir nuevo proveedor."); return; }

    Console.Write($"  Cuantas porciones de {nombresIngredientes[idx]}? ");
    if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0) { MostrarError("Cantidad invalida."); return; }

    int costo = precioPorIngrediente[idx];
    if (diasPrecioAlto > 0) costo = (int)(costo * 1.2);
    double total = cantidad * costo;

    if (dinero >= total)
    {
        dinero -= total;
        ingredientes[idx] += cantidad;
        MostrarExito($"Compraste {cantidad} porciones de {nombresIngredientes[idx]}. Total: C${total}");
    }
    else MostrarError(MSG_SIN_DINERO);
}

void ContratarEmpleado()
{
    int costo = 150;
    MostrarNotificacion($"Costo: C${costo}. Tenes C${dinero:F2}.");
    if (dinero >= costo) { dinero -= costo; empleados++; MostrarExito($"Empleado contratado. Total: {empleados}"); }
    else MostrarError(MSG_SIN_DINERO);
}

void AgregarMesa()
{
    int costo = 200;
    MostrarNotificacion($"Costo: C${costo}. Tenes C${dinero:F2}.");
    if (dinero >= costo) { dinero -= costo; mesas++; MostrarExito($"Mesa agregada. Total: {mesas}"); }
    else MostrarError(MSG_SIN_DINERO);
}

// ============================================
// DATOS — Mario
// ============================================

void GuardarPartida()
{
    try
    {
        StreamWriter f = new StreamWriter(rutaPartida, false);
        f.WriteLine(dia);
        f.WriteLine(dinero);
        f.WriteLine(reputacion);
        f.WriteLine(mesas);
        f.WriteLine(empleados);
        f.WriteLine(diasEmpleadoEnfermo);
        f.WriteLine(diasPrecioAlto);
        f.WriteLine(diasSinIngredienteFresco);
        for (int i = 0; i < 5; i++) f.WriteLine(ingredientes[i]);
        f.Close();
    }
    catch (Exception ex) { MostrarError($"Error al guardar partida: {ex.Message}"); }
}

void CargarPartida()
{
    try
    {
        string[] lineas = File.ReadAllLines(rutaPartida);
        dia                      = int.Parse(lineas[0]);
        dinero                   = double.Parse(lineas[1]);
        reputacion               = int.Parse(lineas[2]);
        mesas                    = int.Parse(lineas[3]);
        empleados                = int.Parse(lineas[4]);
        diasEmpleadoEnfermo      = int.Parse(lineas[5]);
        diasPrecioAlto           = int.Parse(lineas[6]);
        diasSinIngredienteFresco = int.Parse(lineas[7]);
        for (int i = 0; i < 5; i++) ingredientes[i] = int.Parse(lineas[8 + i]);
    }
    catch (Exception ex) { MostrarError($"Error al cargar partida: {ex.Message}"); }
}

void GuardarDia(int numeroDia, double ganancias, double gastos, int clientesAtendidos, int clientesPerdidos)
{
    try
    {
        StreamWriter f = new StreamWriter(rutaHistorial, true);
        f.WriteLine($"{numeroDia},{ganancias},{gastos},{clientesAtendidos},{clientesPerdidos},{reputacion}");
        f.Close();
    }
    catch (Exception ex) { MostrarError($"Error al guardar dia: {ex.Message}"); }
}

void MostrarHistorial()
{
    if (!File.Exists(rutaHistorial)) { MostrarNotificacion("Sin historial aun."); Console.ReadLine(); return; }

    try
    {
        string[] lineas = File.ReadAllLines(rutaHistorial);
        Console.WriteLine("\n===== HISTORIAL =====");
        for (int i = 0; i < lineas.Length; i++)
        {
            string[] d  = lineas[i].Split(',');
            int nDia    = int.Parse(d[0]);
            double gan  = double.Parse(d[1]);
            double gas  = double.Parse(d[2]);
            int atend   = int.Parse(d[3]);
            int perd    = int.Parse(d[4]);
            int rep     = int.Parse(d[5]);
            Console.WriteLine($"  Dia {nDia,2}: Gan C${gan,7:F0} | Gas C${gas,5:F0} | Atend {atend,2} | Perd {perd,2} | Rep {rep,3}");
        }
        MostrarProgreso();
        Console.WriteLine("=====================");
    }
    catch (Exception ex) { MostrarError($"Error al leer historial: {ex.Message}"); }
    Console.ReadLine();
}

double ObtenerMejorDia()
{
    if (!File.Exists(rutaHistorial)) return 0.0;
    double mejor = 0.0;
    try
    {
        foreach (string linea in File.ReadAllLines(rutaHistorial))
        {
            double g = double.Parse(linea.Split(',')[1]);
            if (g > mejor) mejor = g;
        }
    }
    catch (Exception ex) { MostrarError($"Error: {ex.Message}"); }
    return mejor;
}

double ObtenerPromedioGanancias()
{
    if (!File.Exists(rutaHistorial)) return 0.0;
    double suma = 0.0; int dias = 0;
    try
    {
        foreach (string linea in File.ReadAllLines(rutaHistorial))
        {
            suma += double.Parse(linea.Split(',')[1]);
            dias++;
        }
    }
    catch (Exception ex) { MostrarError($"Error: {ex.Message}"); }
    return dias == 0 ? 0.0 : suma / dias;
}

void MostrarProgreso()
{
    double mejor    = ObtenerMejorDia();
    double promedio = ObtenerPromedioGanancias();
    Console.WriteLine("\n  --- Progreso ---");
    Console.WriteLine($"  Mejor dia:           C${mejor:F2}");
    Console.WriteLine($"  Promedio ganancias:  C${promedio:F2}");
    if (mejor > promedio) MostrarExito("Vas mejorando!");
    else MostrarNotificacion("Podes mejorar tus ganancias diarias.");
    Console.WriteLine("  -----------------");
}
