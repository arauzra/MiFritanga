// ============================================
// MiFritanga - Juego Tycoon
// MiFritanga_Miguel.cs — Parte de Miguel
// CONTENIDO + UI (mensajes, menús, display)
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

// Descripción de cada platillo (mismo orden que nombresPlatillos)
string[] descripcionesPlatillos = {
    "Yuca cocida con chicharrón y ensalada de repollo.",
    "Tamal de maíz relleno de carne y arroz, envuelto en hoja de plátano.",
    "Tortilla enrollada con queso seco y crema.",
    "Combinación de carnes asadas con arroz, frijoles y tajadas.",
    "Plátano maduro frito con queso seco rallado encima.",
    "Caldo abundante de res con verduras y yuca.",
    "Tortilla de maíz tierno con cuajada fresca.",
    "Plátano maduro frito, dulce y suavecito.",
    "Tortilla frita rellena de carne molida y queso.",
    "Chancho cocinado con yuca y curtido de cebolla."
};

double[] preciosPlatillos = {
    60.0, 50.0, 30.0, 80.0, 40.0,
    90.0, 35.0, 25.0, 45.0, 70.0
};

int[] costoIngredientes = {
    3, 4, 2, 5, 2, 6, 2, 1, 3, 5
};

// ============================================
// EVENTOS — Miguel
// Cada evento positivo tiene su efecto asociado
// ============================================

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

// Mensajes de error reutilizables — Miguel
string MSG_SIN_DINERO = "No tenés suficiente dinero para eso.";
string MSG_SIN_INGREDIENTES = "¡Se acabaron los ingredientes! Comprá más en la tienda.";
string MSG_OPCION_INVALIDA = "Opción no válida. Intentá de nuevo.";

// ============================================
// UI — Miguel
// Mensajes, menús y display en consola
// ============================================

void MostrarBienvenida()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("╔══════════════════════════════════════════╗");
    Console.WriteLine("║                                          ║");
    Console.WriteLine("║       🍖  BIENVENIDO A MIFRITANGA  🍖   ║");
    Console.WriteLine("║                                          ║");
    Console.WriteLine("║   ¡Administrá tu propia fritanga         ║");
    Console.WriteLine("║        nicaragüense y sobreviví          ║");
    Console.WriteLine("║            30 días de negocio!           ║");
    Console.WriteLine("║                                          ║");
    Console.WriteLine("╚══════════════════════════════════════════╝");
    Console.ResetColor();

    Console.WriteLine();
    Console.WriteLine("  Tenés C$500 de capital inicial.");
    Console.WriteLine("  Vendé platillos, gestioná tus ingredientes");
    Console.WriteLine("  y mantené tu reputación en alto.");
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("  Presioná ENTER para comenzar...");
    Console.ResetColor();
    Console.ReadLine();
}

void MostrarMenuPrincipal()
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("  ¿Qué hacés hoy?");
    Console.ResetColor();
    Console.WriteLine("  [1] Abrir la fritanga  (iniciar el día)");
    Console.WriteLine("  [2] Administrar        (comprar, contratar, expandir)");
    Console.WriteLine("  [3] Ver historial y progreso");
    Console.WriteLine("  [4] Salir del juego");
    Console.Write("\n  Opción: ");
}

void MostrarEstado()
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("══════════════════════════════════════════");
    Console.ResetColor();
    Console.WriteLine($"  📅  Día:          {dia} / {diasTotales}");
    Console.WriteLine($"  💵  Dinero:        C${dinero:F2}");

    // Barra visual de reputación
    int barraLlena = reputacion / 10;
    string barra = "[" + new string('█', barraLlena) + new string('░', 10 - barraLlena) + "]";
    Console.Write($"  ⭐  Reputación:    {barra} {reputacion}/100");

    if (reputacion >= 75)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("  ← ¡Excelente!");
    }
    else if (reputacion >= 40)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("  ← Regular");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  ← ¡En peligro!");
    }
    Console.ResetColor();

    Console.WriteLine($"  🪑  Mesas:         {mesas}");
    Console.WriteLine($"  👤  Empleados:     {empleados}");

    // Advertencia si hay pocos ingredientes
    Console.Write($"  🧺  Ingredientes:  {ingredientes} unidades");
    if (ingredientes < 20)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("  ⚠️  ¡Pocos ingredientes!");
        Console.ResetColor();
    }
    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("══════════════════════════════════════════");
    Console.ResetColor();
}

void MostrarResultadoDia(double ganancias, double gastos, int clientesAtendidos, int clientesPerdidos)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("  ┌─────────────────────────────────────┐");
    Console.WriteLine("  │        RESUMEN DEL DÍA              │");
    Console.WriteLine("  └─────────────────────────────────────┘");
    Console.ResetColor();

    Console.WriteLine($"  👥  Clientes atendidos:  {clientesAtendidos}");

    if (clientesPerdidos > 0)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  😤  Clientes perdidos:   {clientesPerdidos}  ← ¡Se fueron sin comer!");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  😤  Clientes perdidos:   0  ← ¡Perfecto!");
        Console.ResetColor();
    }

    Console.WriteLine($"  📈  Ganancias:           C${ganancias:F2}");
    Console.WriteLine($"  📉  Gastos:              C${gastos:F2}");

    double balance = ganancias - gastos;
    Console.Write("  💰  Balance del día:     ");
    if (balance >= 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"C${balance:F2}  ✔");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"C${balance:F2}  ✘");
    }
    Console.ResetColor();

    Console.WriteLine("  ─────────────────────────────────────");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("  Presioná ENTER para continuar...");
    Console.ResetColor();
    Console.ReadLine();
}

void MostrarEvento(string descripcion)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine("  ╔══════════════════════════════════════╗");
    Console.WriteLine("  ║            ⚡ EVENTO ⚡               ║");
    Console.WriteLine("  ╠══════════════════════════════════════╣");
    // Centrar texto del evento (max 38 chars visibles)
    string texto = descripcion.Length > 38 ? descripcion.Substring(0, 38) : descripcion;
    Console.WriteLine($"  ║  {texto.PadRight(38)}║");
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
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   🏆  ¡SOBREVIVISTE LOS 30 DÍAS!  🏆    ║");
        Console.WriteLine("║       ¡Tu fritanga es un éxito!          ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("║   😢  CERRASTE LA FRITANGA  😢           ║");
        Console.WriteLine("║       Se te acabó el dinero...           ║");
        Console.WriteLine("║                                          ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
    }
    Console.ResetColor();

    Console.WriteLine();
    Console.WriteLine($"  💵  Dinero final:        C${dinero:F2}");
    Console.WriteLine($"  ⭐  Reputación final:    {reputacion}/100");
    Console.WriteLine($"  📅  Días sobrevividos:   {dia - 1}");
    Console.WriteLine();

    // Calificación final según reputación
    Console.Write("  🏅  Calificación:  ");
    if (reputacion >= 80 && dia > diasTotales)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("★★★  FRITANGUERA LEGENDARIA");
    }
    else if (reputacion >= 60)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("★★☆  Fritanguera Reconocida");
    }
    else if (reputacion >= 30)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("★☆☆  Fritanguera del Barrio");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("☆☆☆  Fritanguera Principiante");
    }
    Console.ResetColor();

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("  Presioná ENTER para salir...");
    Console.ResetColor();
    Console.ReadLine();
}

// ============================================
// FUNCIÓN AUXILIAR — Miguel
// Muestra el menú de un platillo con su descripción y precio
// Útil para cuando Carlos procese pedidos del día
// ============================================
void MostrarMenuPlatillos()
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("  ┌──────────────────────────────────────────────┐");
    Console.WriteLine("  │              MENÚ DE HOY 🍽️                  │");
    Console.WriteLine("  └──────────────────────────────────────────────┘");
    Console.ResetColor();

    for (int i = 0; i < nombresPlatillos.Length; i++)
    {
        Console.WriteLine($"  [{i + 1,2}] {nombresPlatillos[i],-22} C${preciosPlatillos[i],5:F2}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"       {descripcionesPlatillos[i]}");
        Console.ResetColor();
    }
    Console.WriteLine();
}

// ============================================
// MENSAJES DE ERROR Y NOTIFICACIÓN 
// ============================================
void MostrarError(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\n  ✘  {mensaje}");
    Console.ResetColor();
}

void MostrarExito(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n  ✔  {mensaje}");
    Console.ResetColor();
}

void MostrarNotificacion(string mensaje)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n  ℹ  {mensaje}");
    Console.ResetColor();
}