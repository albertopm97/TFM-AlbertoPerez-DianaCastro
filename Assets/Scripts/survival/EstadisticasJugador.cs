   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstadisticasJugador : MonoBehaviour, IShopCustomer
{
    private Weapon referenciaAtaqueJugador;

    [Header("Clase por defecto")]
    public ScriptableObjectPersonaje infoPJDefecto;
    ScriptableObjectPersonaje infoPersonaje;

    //Estadisticas generales actuales
    [HideInInspector]
    public float vidaMaximaActual;
    
    public float vidaActual;
    [HideInInspector]
    public float recuperacionActual;
    [HideInInspector]
    public float velocidadMovimientoActual;
    [HideInInspector]
    public float poderActual;
    [HideInInspector]
    public float rapidezProyectilActual;
    [HideInInspector]
    public float imanObjetosActual;

    public int monedas;

    /*Gestion de experiencia
    [Header("Experiencia/Nivel")]
    public int experiencia = 0;
    public int nivel = 1;
    public int maxExperiencia;


    //Para gestion de subidas de niveles y cambios en la experiencia máxima
    public List<RangoNivel> rangosNivel;

    //Para la gestion del inventario
    GestorInventario inventario;
    public int ranuraArma;
    public int ranuraPasivo;

    //pruebas
    public GameObject arma2;
    public GameObject pasivo1, pasivo2;*/

    [Header("IFrames")]
    //Para gestion de frames de invulnerabilidad (no recibir daño constante y rapido)
    public float duracionInv;
    float temporizadorInv;
    bool esInvulnerable;

    //Para la barra de vida
    [Header("UI")]
    public Image barraHp;
    //public Image barraExp;

    /*Clase para gestion de los rangos de nivel. En cada rango, la experiencia necesaria para subir de nivel varia de forma distinta
    //Cabe destacar que el siguiente sistema de subida de lv no es de desarrollo propio, es el utilizado en el juego original "vampire survivor"
    // de lv 1 a 2 se necesitan 5 xp
    //desde lv 2 a lv 20 se incrementa la experienca necesaria en 10 cada lv (15 xp de lv 2 a 3, 25xp de lv 3 a 4, ...)
    //lv 21 a 40 el incremento es de 13xp
    //lv 40 en adelante incremento de 16xp


    [System.Serializable]
    public class RangoNivel
    {
        public int nivelInicial;
        public int nivelFinal;
        public int incrementoMaxExperiencia;
    }*/

    [Header("Sonido")]
    public AudioSource sonidoHit;
    public AudioSource sonidoMuerte;
    public AudioSource sonidoLvUp;
    public AudioSource sonidoMoneda;

    private void Start()
    {
        //Inicializamos la experiencia maxima incial como el primer incremento máximo de experiencia (0 + incremento = incremento)
        //maxExperiencia = rangosNivel[0].incrementoMaxExperiencia;

        actualizarBarraHP();

        referenciaAtaqueJugador = gameObject.GetComponent<Weapon>();
    }

    void Update()
    {
        if(temporizadorInv > 0)
        {
            temporizadorInv -= Time.deltaTime;
        }
        else if(esInvulnerable)
        {
            esInvulnerable = false;
        }

        curaPasiva();
        //actualizarBarraExp();
    }

    //Al llamarse al script
    private void Awake()
    {
        if(infoPersonaje == null)
        {
            infoPersonaje = infoPJDefecto;
        }

        vidaMaximaActual = infoPersonaje.VidaMaxima;
        vidaActual = infoPersonaje.VidaMaxima;
        recuperacionActual = infoPersonaje.Recuperacion;
        velocidadMovimientoActual = infoPersonaje.VelocidadMovimiento;
        poderActual = infoPersonaje.Poder;
        rapidezProyectilActual = infoPersonaje.RapidezProyectil;
        imanObjetosActual = infoPersonaje.ImanObjetos;
        monedas = 0;

        /*inventario = GetComponent<GestorInventario>();

        equiparNuevaArma(infoPersonaje.ArmaInicial);

        if(SelectorPersonaje.instancia != null)
        {
            SelectorPersonaje.instancia.DestruirSingleton();
        }*/
    }

    public void sumarMonedas(int cantidad)
    {
        monedas += cantidad;
    }

    public void RecibirAtaque(float dmg)
    {
        //Recibimos el ataque si no estamos en frames de invencibilidad
        if (!esInvulnerable)
        {
            vidaActual -= dmg;

            //Reiniciamos el temporizador de I-frames
            temporizadorInv = duracionInv;
            esInvulnerable = true;
            //sonidoHit.Play();

            if (vidaMaximaActual <= 0)
            {
                //sonidoMuerte.Play();
                Morir();
            }

            actualizarBarraHP();
        }
    }

    void actualizarBarraHP()
    {
        barraHp.fillAmount = vidaActual / vidaMaximaActual;
    }

    /*void actualizarBarraExp()
    {
        barraExp.fillAmount = experiencia / maxExperiencia;
    }*/

    public void Morir()
    {
        /*if (!GameManager.instancia.juegoFinalizado)
        {
            GameManager.instancia.gameOver();
        }*/
    }

    public void curar(float vidaACurar)
    {
        //Solo curar cuando se haya perdido vida
        if(vidaActual < vidaMaximaActual)
        {
            vidaActual += vidaACurar;
            Debug.Log("TE HAS CURADO");

            //Si al curar la vida sobrepasa el maximo, dejar el maximo en su lugar
            if (vidaActual > vidaMaximaActual)
            {
                vidaActual = vidaMaximaActual;
            }

            actualizarBarraHP();
        }
    }

    //La idea es que si te falta vida haya una curación pasiva en función del factor de recuperación
    void curaPasiva()
    {
        if(vidaActual < vidaMaximaActual)
        {
            vidaActual += recuperacionActual * Time.deltaTime;

            //Incluso con la comprobación anterior, a veces se pasa un poco de la vida maxima
            //Es necesaria una segunda comprobación para dejar aplicado el máximo
            if(vidaActual > vidaMaximaActual)
            {
                vidaActual = vidaMaximaActual;
            }

            actualizarBarraHP();
        }
    }

    public void mejoraComprada(Mejoras.tipoMejora tipoMejora, int rareza)
    {
        string cadena = "Comprada mejora de " + tipoMejora.ToString() + " de rareza ";

        switch (rareza) 
        { 
            case 0:
                cadena += "común";
                break;

            case 1:
                cadena += "poco común";
                break;

            case 2:
                cadena += "épica";
                break;

            case 3:
                cadena += "legendaria";
                break;

            default:
                cadena += "común";
                    break;
        }

        Debug.Log(cadena);


        //Switch anidado para determinar como se aplica cada mejora con su correspondiente rareza
        switch (tipoMejora)
        {
            default:
            case Mejoras.tipoMejora.Salud:

                switch (rareza)
                {
                    default:
                    case 0: vidaMaximaActual += 5; break;
                    case 1: vidaMaximaActual += 10; break;
                    case 2: vidaMaximaActual += 15; break;
                    case 3: vidaMaximaActual += 20; break;
                }
                break;

            case Mejoras.tipoMejora.CuraPasiva:

                switch (rareza)
                {
                    default:
                    case 0: recuperacionActual += 1; break;
                    case 1: recuperacionActual += 3; break;
                    case 2: recuperacionActual += 5; break;
                    case 3: recuperacionActual += 10; break;
                }
                break;

            case Mejoras.tipoMejora.VelocidadMovimiento:

                switch (rareza)
                {
                    default: 
                    case 0: velocidadMovimientoActual += 1; break;
                    case 1: velocidadMovimientoActual += 2; break;
                    case 2: velocidadMovimientoActual += 3; break;
                    case 3: velocidadMovimientoActual += 5; break;
                }
                break;

            case Mejoras.tipoMejora.VelocidadProyectil:

                switch (rareza)
                {
                    default:
                    case 0: referenciaAtaqueJugador.velocidadProyectilActual += 1; break;
                    case 1: referenciaAtaqueJugador.velocidadProyectilActual += 2; break;
                    case 2: referenciaAtaqueJugador.velocidadProyectilActual += 3; break;
                    case 3: referenciaAtaqueJugador.velocidadProyectilActual += 5; break;
                }
                break;

            case Mejoras.tipoMejora.Damage:

                switch (rareza)
                {
                    default:
                    case 0: referenciaAtaqueJugador.damageActual += 1; break;
                    case 1: referenciaAtaqueJugador.damageActual += 2; break;
                    case 2: referenciaAtaqueJugador.damageActual += 3; break;
                    case 3: referenciaAtaqueJugador.damageActual += 5; break;
                }
                break;

            case Mejoras.tipoMejora.Poder:

                switch (rareza)
                {
                    default:
                    case 0: referenciaAtaqueJugador.poderActual += 0.1f; break;
                    case 1: referenciaAtaqueJugador.poderActual += 0.2f; break;
                    case 2: referenciaAtaqueJugador.poderActual += 0.3f; break;
                    case 3: referenciaAtaqueJugador.poderActual += 0.5f; break;
                }
                break;

            case Mejoras.tipoMejora.numProyectiles:

                switch (rareza)
                {
                    default:
                    case 0: referenciaAtaqueJugador.numProyectilesActual += 1; break;
                    case 1: referenciaAtaqueJugador.numProyectilesActual += 2; break;
                    case 2: referenciaAtaqueJugador.numProyectilesActual += 3; break;
                    case 3: referenciaAtaqueJugador.numProyectilesActual += 4; break;
                }

                if (referenciaAtaqueJugador.numProyectilesActual > 5)
                {
                    referenciaAtaqueJugador.numProyectilesActual = 5;
                }
                break;

            case Mejoras.tipoMejora.velocidadAtaque:

                switch (rareza)
                {
                    default:
                    case 0: referenciaAtaqueJugador.velocidadAtaqueActual -= 0.1f; break;
                    case 1: referenciaAtaqueJugador.velocidadAtaqueActual -= 0.2f; break;
                    case 2: referenciaAtaqueJugador.velocidadAtaqueActual -= 0.3f; break;
                    case 3: referenciaAtaqueJugador.velocidadAtaqueActual -= 0.5f; break;
                }

                if (referenciaAtaqueJugador.velocidadAtaqueActual < 0.2f)
                {
                    referenciaAtaqueJugador.velocidadAtaqueActual = 0.2f;
                }
                break;

            case Mejoras.tipoMejora.penetracion:

                switch (rareza)
                {
                    default:
                    case 0: referenciaAtaqueJugador.piercingActual += 1; break;
                    case 1: referenciaAtaqueJugador.piercingActual += 2; break;
                    case 2: referenciaAtaqueJugador.piercingActual += 3; break;
                    case 3: referenciaAtaqueJugador.piercingActual += 4; break;
                }
                break;
        }
    }

    public bool trySpendGoldAmount(int goldAmount)
    {
        if(monedas >= goldAmount)
        {
            monedas -= goldAmount;
            return true;
        }
        else
        {
            return false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IColeccionable item))
        {
            item.coger();
        }
    }
}
