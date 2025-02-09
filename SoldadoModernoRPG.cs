using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public class Soldado
{
    public string Nombre { get; set; }
    public int Nivel { get; set; }
    public int Vida { get; set; }
    public int Ataque { get; set; }
    public int Defensa { get; set; }
    public int Moral { get; set; }
    public Dictionary<string, int> Armas { get; set; }
    public Dictionary<string, int> Municion { get; set; }
    public int Botiquines { get; set; }
    public string Estrategia { get; set; }
    public string Ubicacion { get; set; }
    public int Experiencia { get; set; }
    public string Camo { get; set; }
    public List<string> Habilidades { get; set; }
    public Mision Mision { get; set; }
    public Dictionary<string, int> Suministros { get; set; }

    public Soldado(string nombre)
    {
        Nombre = nombre;
        Nivel = 1;
        Vida = 100;
        Ataque = 15;
        Defensa = 8;
        Moral = 100;
        Armas = new Dictionary<string, int> { { "Pistola", 10 }, { "Rifle de asalto", 20 }, { "Escopeta", 25 } };
        Municion = new Dictionary<string, int> { { "Pistola", 30 }, { "Rifle de asalto", 20 }, { "Escopeta", 10 } };
        Botiquines = 3;
        Estrategia = "Estándar";
        Ubicacion = "Campamento Base";
        Experiencia = 0;
        Camo = "Estándar";
        Habilidades = new List<string> { "Disparo certero", "Escudo temporal" };
        Suministros = new Dictionary<string, int> { { "agua", 10 }, { "comida", 5 } };
    }

    public void SubirNivel()
    {
        Nivel++;
        Vida += 30;
        Ataque += 10;
        Defensa += 5;
        Moral = 100;
        Experiencia = 0;
        Console.WriteLine($"{Nombre} ha subido al nivel {Nivel}!");
    }

    public void RecibirDano(int dano)
    {
        int danoReal = Math.Max(0, dano - Defensa);
        Vida -= danoReal;
        Moral -= 5;
        Console.WriteLine($"{Nombre} ha recibido {danoReal} de daño. Vida restante: {Vida}");
    }

    public void UsarBotiquin()
    {
        if (Botiquines > 0)
        {
            Vida += 40;
            Botiquines--;
            Console.WriteLine($"{Nombre} usó un botiquín. Vida actual: {Vida}");
        }
        else
        {
            Console.WriteLine("No tienes botiquines disponibles.");
        }
    }

    public void GestionarSuministros()
    {
        if (Suministros["agua"] > 0 && Suministros["comida"] > 0)
        {
            Suministros["agua"]--;
            Suministros["comida"]--;
            Moral += 20;
            Console.WriteLine($"{Nombre} ha consumido suministros. Moral actual: {Moral}");
        }
        else
        {
            Console.WriteLine($"{Nombre} se está quedando sin suministros.");
        }
    }

    public void ObtenerArma(string arma, int dano)
    {
        Armas[arma] = dano;
        Console.WriteLine($"Has obtenido un(a) {arma} con daño {dano}!");
    }

    public void CambiarEstrategia(string nuevaEstrategia)
    {
        Estrategia = nuevaEstrategia;
        Console.WriteLine($"{Nombre} cambió su estrategia a {Estrategia}");
    }

    public void CambiarCamo(string nuevoCamo)
    {
        Camo = nuevoCamo;
        Console.WriteLine($"{Nombre} cambió su camuflaje a {Camo}");
    }

    public void Moverse(string nuevaUbicacion)
    {
        Ubicacion = nuevaUbicacion;
        Console.WriteLine($"{Nombre} se ha movido a {Ubicacion}");
    }

    public void LlamarApoyoHelicoptero()
    {
        Console.WriteLine($"{Nombre} llama un helicóptero para apoyo aéreo. ¡Aguanta!");
    }

    public void SolicitarArtilleria()
    {
        Console.WriteLine($"{Nombre} solicita un ataque de artillería sobre las posiciones enemigas.");
    }

    public void RecibirRefuerzos()
    {
        Console.WriteLine($"{Nombre} ha recibido refuerzos, soldados adicionales para ayudar en la batalla.");
    }

    public void LanzarGranada(Granada granada, Enemigo enemigo)
    {
        granada.Usar(enemigo);
    }

    public void UsarHabilidad(string habilidad, Enemigo enemigo)
    {
        if (habilidad == "Disparo certero")
        {
            Random rand = new Random();
            int danoExtra = rand.Next(10, 21);
            enemigo.RecibirDano(danoExtra);
            Console.WriteLine($"{Nombre} usa Disparo certero. ¡{danoExtra} de daño adicional!");
        }
        else if (habilidad == "Escudo temporal")
        {
            Defensa += 10;
            Console.WriteLine($"{Nombre} activa Escudo temporal. Defensa aumentada a {Defensa}");
        }
        else
        {
            Console.WriteLine("Habilidad no disponible.");
        }
    }

    public void AceptarMision(Mision mision)
    {
        Mision = mision;
        Console.WriteLine($"{Nombre} ha aceptado la misión: {mision.Descripcion}");
    }

    public void CompletarMision()
    {
        if (Mision != null)
        {
            Console.WriteLine($"{Nombre} ha completado la misión: {Mision.Descripcion}");
            Mision.Recompensar(this);
            Mision = null;
        }
        else
        {
            Console.WriteLine("No tienes misión activa.");
        }
    }
}

public class Mision
{
    public string Descripcion { get; set; }
    public Tuple<string, int> RecompensaArma { get; set; }
    public int RecompensaExperiencia { get; set; }

    public Mision(string descripcion, Tuple<string, int> recompensaArma = null, int recompensaExperiencia = 0)
    {
        Descripcion = descripcion;
        RecompensaArma = recompensaArma;
        RecompensaExperiencia = recompensaExperiencia;
    }

    public void Recompensar(Soldado soldado)
    {
        if (RecompensaArma != null)
        {
            soldado.ObtenerArma(RecompensaArma.Item1, RecompensaArma.Item2);
        }
        if (RecompensaExperiencia > 0)
        {
            soldado.Experiencia += RecompensaExperiencia;
        }
        Console.WriteLine($"{soldado.Nombre} ha recibido sus recompensas.");
    }
}

public class Enemigo
{
    public string Nombre { get; set; }
    public int Vida { get; set; }
    public int Ataque { get; set; }
    public string Estrategia { get; set; }
    public string Tipo { get; set; }
    public Dictionary<string, int> Armamento { get; set; }

    public Enemigo(string nombre, int vida, int ataque, string estrategia, string tipo)
    {
        Nombre = nombre;
        Vida = vida;
        Ataque = ataque;
        Estrategia = estrategia;
        Tipo = tipo;
        Armamento = new Dictionary<string, int> { { "Pistola", 12 }, { "Rifle de asalto", 18 } };
    }

    public void RecibirDano(int dano)
    {
        Vida -= dano;
        Console.WriteLine($"{Nombre} ha recibido {dano} de daño. Vida restante: {Vida}");
    }

    public void Atacar(Soldado soldado)
    {
        soldado.RecibirDano(Ataque);
        Console.WriteLine($"{Nombre} ataca a {soldado.Nombre} con {Ataque} de daño.");
    }

    public void CambiarEstrategia(string nuevaEstrategia)
    {
        Estrategia = nuevaEstrategia;
        Console.WriteLine($"{Nombre} cambió su estrategia a {Estrategia}");
    }

    public void EquiparArma(string arma, int dano)
    {
        Armamento[arma] = dano;
        Console.WriteLine($"{Nombre} ha equipado un(a) {arma} con daño {dano}!");
    }
}

public class Granada
{
    public string Tipo { get; set; }
    public int Dano { get; set; }

    public Granada(string tipo, int dano)
    {
        Tipo = tipo;
        Dano = dano;
    }

    public void Usar(Enemigo enemigo)
    {
        enemigo.RecibirDano(Dano);
        Console.WriteLine($"¡Granada {Tipo} lanzada! Causa {Dano} de daño.");
    }
}

public class Artilleria
{
    public int Dano { get; set; }

    public Artilleria()
    {
        Dano = 50;
    }

    public void Lanzar(Enemigo enemigo)
    {
        enemigo.RecibirDano(Dano);
        Console.WriteLine($"¡Ataque de artillería lanzado! Causa {Dano} de daño.");
    }
}

public class Clima
{
    private List<string> Condiciones;

    public Clima()
    {
        Condiciones = new List<string> { "Soleado", "Lluvia", "Tormenta", "Niebla" };
    }

    public void AfectarCombate()
    {
        Random rand = new Random();
        string clima = Condiciones[rand.Next(Condiciones.Count)];
        if (clima == "Soleado")
        {
            Console.WriteLine("El clima es soleado. ¡Buen momento para atacar!");
        }
        else if (clima == "Lluvia")
        {
            Console.WriteLine("La lluvia dificulta la puntería.");
        }
        else if (clima == "Tormenta")
        {
            Console.WriteLine("La tormenta reduce la visibilidad y los daños son más impredecibles.");
        }
        else if (clima == "Niebla")
        {
            Console.WriteLine("La niebla hace que sea más difícil localizar a los enemigos.");
        }
    }
}

class Program
{
    static void Combate(Soldado soldado, Enemigo enemigo, Clima clima)
    {
        Console.WriteLine($"¡Un {enemigo.Nombre} ha aparecido!");
        Random rand = new Random();
        while (soldado.Vida > 0 && enemigo.Vida > 0)
        {
            clima.AfectarCombate();
            Console.WriteLine("¿Qué quieres hacer?");
            string accion = Console.ReadLine().ToLower();
            if (accion == "atacar")
            {
                int dano = rand.Next(soldado.Ataque - 5, soldado.Ataque + 5);
                enemigo.RecibirDano(dano);
            }
            else if (accion == "defender")
            {
                soldado.Defensa += 5;
                Console.WriteLine($"Te preparas para defenderte. Defensa aumentada a {soldado.Defensa}");
            }
            else if (accion == "usar habilidad")
            {
                Console.WriteLine("Elige una habilidad: Disparo certero, Escudo temporal");
                string habilidad = Console.ReadLine();
                soldado.UsarHabilidad(habilidad, enemigo);
            }
            else if (accion == "salir")
            {
                break;
            }
            else
            {
                Console.WriteLine("Acción no reconocida.");
            }

            if (rand.Next(0, 2) == 0)
            {
                enemigo.Atacar(soldado);
            }
            else
            {
                Console.WriteLine($"{enemigo.Nombre} no ha atacado en esta ronda.");
            }
        }

        if (soldado.Vida <= 0)
        {
            Console.WriteLine($"{soldado.Nombre} ha caído en combate...");
        }
        else if (enemigo.Vida <= 0)
        {
            Console.WriteLine($"{enemigo.Nombre} ha sido derrotado!");
        }
    }

    static void Main()
    {
        Clima clima = new Clima();
        Soldado soldado = new Soldado("Carlos");
        Enemigo enemigo = new Enemigo("Soldado Enemigo", 100, 20, "ofensiva", "humano");

        Combate(soldado, enemigo, clima);
    }
}
