using System;
using System.Collections.Generic;

public class Personaje
{
    public string Nombre { get; set; }
    public string Raza { get; set; }
    public int Vida { get; set; }
    public int Fuerza { get; set; }
    public int Destreza { get; set; }
    public int Inteligencia { get; set; }
    public int Monedas { get; set; }
    public int Nivel { get; set; }
    public int Experiencia { get; set; }
    public int Defensa { get; set; }
    public int DanoBase { get; set; }
    public int MaxVida { get; set; }
    public int PuntosDeVida { get; set; }
    public int Casa { get; set; } // Nivel de la casa
    public List<Arma> Armas { get; set; }
    public List<Armadura> Armaduras { get; set; }

    public Personaje(string nombre, string raza, int vida, int fuerza, int destreza, int inteligencia)
    {
        Nombre = nombre;
        Raza = raza;
        Vida = vida;
        Fuerza = fuerza;
        Destreza = destreza;
        Inteligencia = inteligencia;
        Monedas = 0;
        Nivel = 1;
        Experiencia = 0;
        Defensa = 0;
        DanoBase = 10;
        MaxVida = vida;
        PuntosDeVida = vida;
        Casa = 0;
        Armas = new List<Arma>();
        Armaduras = new List<Armadura>();
    }

    public void SubirAtributo(string atributo)
    {
        switch (atributo.ToLower())
        {
            case "fuerza":
                Fuerza++;
                break;
            case "destreza":
                Destreza++;
                break;
            case "inteligencia":
                Inteligencia++;
                break;
        }
        Console.WriteLine($"{Nombre} ha aumentado su atributo de {atributo}.");
    }

    public void MostrarAtributos()
    {
        Console.WriteLine($"Atributos de {Nombre}:");
        Console.WriteLine($"Raza: {Raza}");
        Console.WriteLine($"Vida: {PuntosDeVida}/{MaxVida}");
        Console.WriteLine($"Fuerza: {Fuerza}");
        Console.WriteLine($"Destreza: {Destreza}");
        Console.WriteLine($"Inteligencia: {Inteligencia}");
        Console.WriteLine($"Nivel: {Nivel}");
        Console.WriteLine($"Monedas: {Monedas}");
        Console.WriteLine($"Casa: {Casa}");
        Console.WriteLine($"Armas: {string.Join(", ", Armas.ConvertAll(arma => arma.Nombre))}");
        Console.WriteLine($"Armaduras: {string.Join(", ", Armaduras.ConvertAll(arma => arma.Nombre))}");
    }

    public void GanarExperiencia(int cantidad)
    {
        Experiencia += cantidad;
        Console.WriteLine($"{Nombre} ha ganado {cantidad} puntos de experiencia.");
        if (Experiencia >= 100)
        {
            SubirNivel();
        }
    }

    public void SubirNivel()
    {
        Nivel++;
        Experiencia = 0;
        Fuerza += 2;
        Destreza += 2;
        Inteligencia += 1;
        PuntosDeVida = MaxVida;
        Console.WriteLine($"{Nombre} ha subido al nivel {Nivel}!");
    }

    public void UsarArma(Arma arma)
    {
        if (Armas.Contains(arma))
        {
            DanoBase += arma.Dano;
            Console.WriteLine($"{Nombre} ha equipado el {arma.Nombre}. Daño: {DanoBase}");
        }
        else
        {
            Console.WriteLine($"{Nombre} no tiene el {arma.Nombre} en su inventario.");
        }
    }

    public void UsarArmadura(Armadura armadura)
    {
        if (Armaduras.Contains(armadura))
        {
            Defensa += armadura.Defensa;
            Console.WriteLine($"{Nombre} ha equipado la {armadura.Nombre}. Defensa: {Defensa}");
        }
        else
        {
            Console.WriteLine($"{Nombre} no tiene la {armadura.Nombre} en su inventario.");
        }
    }
}

public class Arma
{
    public string Nombre { get; set; }
    public int Dano { get; set; }

    public Arma(string nombre, int dano)
    {
        Nombre = nombre;
        Dano = dano;
    }
}

public class Armadura
{
    public string Nombre { get; set; }
    public int Defensa { get; set; }

    public Armadura(string nombre, int defensa)
    {
        Nombre = nombre;
        Defensa = defensa;
    }
}

public class Enemigo
{
    public string Nombre { get; set; }
    public int Vida { get; set; }
    public int Dano { get; set; }
    public int Nivel { get; set; }

    public Enemigo(string nombre, int vida, int dano, int nivel)
    {
        Nombre = nombre;
        Vida = vida;
        Dano = dano;
        Nivel = nivel;
    }

    public void Atacar(Personaje personaje)
    {
        int danioTotal = Math.Max(0, Dano - personaje.Defensa);
        personaje.PuntosDeVida -= danioTotal;
        Console.WriteLine($"{Nombre} ataca a {personaje.Nombre} y causa {danioTotal} puntos de daño.");
    }

    public bool EstaVivo()
    {
        return Vida > 0;
    }

    public void RecibirDano(int dano)
    {
        Vida -= dano;
        Console.WriteLine($"{Nombre} recibe {dano} puntos de daño.");
    }

    public void Morir()
    {
        Console.WriteLine($"{Nombre} ha sido derrotado.");
    }
}

public class Mision
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int RecompensaExperiencia { get; set; }
    public int RecompensaMonedas { get; set; }
    public string Objetivo { get; set; }
    public bool Completada { get; set; }

    public Mision(string nombre, string descripcion, int recompensaExperiencia, int recompensaMonedas, string objetivo)
    {
        Nombre = nombre;
        Descripcion = descripcion;
        RecompensaExperiencia = recompensaExperiencia;
        RecompensaMonedas = recompensaMonedas;
        Objetivo = objetivo;
        Completada = false;
    }

    public void Completar(Personaje personaje)
    {
        if (!Completada)
        {
            Console.WriteLine($"Misión '{Nombre}' completada. Recompensa: {RecompensaExperiencia} EXP y {RecompensaMonedas} monedas.");
            personaje.GanarExperiencia(RecompensaExperiencia);
            personaje.Monedas += RecompensaMonedas;
            Completada = true;
        }
        else
        {
            Console.WriteLine($"Ya has completado la misión '{Nombre}'.");
        }
    }
}

public class Juego
{
    static void Pescar(Personaje personaje)
    {
        Console.WriteLine($"{personaje.Nombre} está pescando...");
        if (new Random().Next(1, 11) > 3)
        {
            Console.WriteLine("¡Has pescado un pez!");
            personaje.Monedas += 5;
            personaje.GanarExperiencia(10);
        }
        else
        {
            Console.WriteLine("No has pescado nada.");
        }
    }

    static void Cazar(Personaje personaje)
    {
        Console.WriteLine($"{personaje.Nombre} está cazando...");
        if (new Random().Next(1, 11) > 4)
        {
            Console.WriteLine("¡Has cazado un animal!");
            personaje.Monedas += 10;
            personaje.GanarExperiencia(20);
        }
        else
        {
            Console.WriteLine("No has cazado nada.");
        }
    }

    static void Talar(Personaje personaje)
    {
        Console.WriteLine($"{personaje.Nombre} está talando árboles...");
        if (new Random().Next(1, 11) > 2)
        {
            Console.WriteLine("¡Has talado un árbol!");
            personaje.Monedas += 3;
            personaje.GanarExperiencia(5);
        }
        else
        {
            Console.WriteLine("No has talado nada.");
        }
    }

    static void Minar(Personaje personaje)
    {
        Console.WriteLine($"{personaje.Nombre} está minando...");
        if (new Random().Next(1, 11) > 3)
        {
            Console.WriteLine("¡Has encontrado un mineral!");
            personaje.Monedas += 8;
            personaje.GanarExperiencia(15);
        }
        else
        {
            Console.WriteLine("No has encontrado nada.");
        }
    }

    static void MejorarCasa(Personaje personaje)
    {
        if (personaje.Monedas >= 50)
        {
            personaje.Monedas -= 50;
            personaje.Casa++;
            Console.WriteLine($"¡Tu casa ha sido mejorada! Nivel de la casa: {personaje.Casa}");
        }
        else
        {
            Console.WriteLine("No tienes suficientes monedas para mejorar tu casa.");
        }
    }

    static void Tienda(Personaje personaje)
    {
        Console.WriteLine("Bienvenido a la tienda.");
        Console.WriteLine("1. Comprar espada (30 monedas)");
        Console.WriteLine("2. Comprar escudo (25 monedas)");
        Console.WriteLine("3. Comprar armadura (40 monedas)");
        Console.WriteLine("4. Salir");
        int eleccion = int.Parse(Console.ReadLine());
        if (eleccion == 1 && personaje.Monedas >= 30)
        {
            Arma espada = new Arma("Espada", 15);
            personaje.Armas.Add(espada);
            personaje.Monedas -= 30;
            Console.WriteLine("Has comprado una espada.");
        }
        else if (eleccion == 2 && personaje.Monedas >= 25)
        {
            Armadura escudo = new Armadura("Escudo", 10);
            personaje.Armaduras.Add(escudo);
            personaje.Monedas -= 25;
            Console.WriteLine("Has comprado un escudo.");
        }
        else if (eleccion == 3 && personaje.Monedas >= 40)
        {
            Armadura armadura = new Armadura("Armadura", 20);
            personaje.Armaduras.Add(armadura);
            personaje.Monedas -= 40;
            Console.WriteLine("Has comprado una armadura.");
        }
        else if (eleccion == 4)
        {
            Console.WriteLine("Gracias por visitar la tienda.");
        }
        else
        {
            Console.WriteLine("No tienes suficientes monedas.");
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Bienvenido al juego!");
        Console.WriteLine("¿Cómo te llamas?");
        string nombre = Console.ReadLine();
        Console.WriteLine("¿Qué raza eres? (Humano, Elfo, Enano)");
        string raza = Console.ReadLine();
        Personaje jugador = new Personaje(nombre, raza, 100, 10, 10, 10);

        while (true)
        {
            Console.WriteLine("\n¿Qué deseas hacer?");
            Console.WriteLine("1. Ver atributos");
            Console.WriteLine("2. Mejorar atributos");
            Console.WriteLine("3. Realizar actividad (Pescar, Cazar, Talar, Minar)");
            Console.WriteLine("4. Mejorar casa");
            Console.WriteLine("5. Ir a la tienda");
            Console.WriteLine("6. Salir");
            int eleccion = int.Parse(Console.ReadLine());

            switch (eleccion)
            {
                case 1:
                    jugador.MostrarAtributos();
                    break;
                case 2:
                    Console.WriteLine("¿Qué atributo deseas mejorar? (Fuerza, Destreza, Inteligencia)");
                    string atributo = Console.ReadLine();
                    jugador.SubirAtributo(atributo);
                    break;
                case 3:
                    Console.WriteLine("¿Qué actividad deseas realizar? (Pescar, Cazar, Talar, Minar)");
                    string actividad = Console.ReadLine();
                    switch (actividad.ToLower())
                    {
                        case "pescar":
                            Pescar(jugador);
                            break;
                        case "cazar":
                            Cazar(jugador);
                            break;
                        case "talar":
                            Talar(jugador);
                            break;
                        case "minar":
                            Minar(jugador);
                            break;
                        default:
                            Console.WriteLine("Actividad no válida.");
                            break;
                    }
                    break;
                case 4:
                    MejorarCasa(jugador);
                    break;
                case 5:
                    Tienda(jugador);
                    break;
                case 6:
                    Console.WriteLine("¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}
