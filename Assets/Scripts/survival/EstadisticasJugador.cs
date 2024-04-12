   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstadisticasJugador : MonoBehaviour, IShopCustomer
{
    [Header("Clase por defecto")]
    public ScriptableObjectPersonaje infoPJDefecto;
    ScriptableObjectPersonaje infoPersonaje;

    //Estadisticas generales actuales
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


    //Para gestion de subidas de niveles y cambios en la experiencia m�xima
    public List<RangoNivel> rangosNivel;

    //Para la gestion del inventario
    GestorInventario inventario;
    public int ranuraArma;
    public int ranuraPasivo;

    //pruebas
    public GameObject arma2;
    public GameObject pasivo1, pasivo2;*/

    [Header("IFrames")]
    //Para gestion de frames de invulnerabilidad (no recibir da�o constante y rapido)
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
        //Inicializamos la experiencia maxima incial como el primer incremento m�ximo de experiencia (0 + incremento = incremento)
        //maxExperiencia = rangosNivel[0].incrementoMaxExperiencia;

        actualizarBarraHP();
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

            if (vidaActual <= 0)
            {
                //sonidoMuerte.Play();
                Morir();
            }

            actualizarBarraHP();
        }
    }

    void actualizarBarraHP()
    {
        barraHp.fillAmount = vidaActual / infoPersonaje.VidaMaxima;
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
        if(vidaActual < infoPersonaje.VidaMaxima)
        {
            vidaActual += vidaACurar;
            Debug.Log("TE HAS CURADO");

            //Si al curar la vida sobrepasa el maximo, dejar el maximo en su lugar
            if (vidaActual > infoPersonaje.VidaMaxima)
            {
                vidaActual = infoPersonaje.VidaMaxima;
            }

            actualizarBarraHP();
        }
    }

    //La idea es que si te falta vida haya una curaci�n pasiva en funci�n del factor de recuperaci�n
    void curaPasiva()
    {
        if(vidaActual < infoPersonaje.VidaMaxima)
        {
            vidaActual += recuperacionActual * Time.deltaTime;

            //Incluso con la comprobaci�n anterior, a veces se pasa un poco de la vida maxima
            //Es necesaria una segunda comprobaci�n para dejar aplicado el m�ximo
            if(vidaActual > infoPersonaje.VidaMaxima)
            {
                vidaActual = infoPersonaje.VidaMaxima;
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
                cadena += "com�n";
                break;

            case 1:
                cadena += "poco com�n";
                break;

            case 2:
                cadena += "�pica";
                break;

            case 3:
                cadena += "legendaria";
                break;

            default:
                cadena += "com�n";
                    break;
        }

        Debug.Log(cadena);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IColeccionable item))
        {
            item.coger();
        }
    }
}
