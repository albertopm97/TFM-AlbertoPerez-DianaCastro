   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity; //Para FMOD

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

    [Header("IFrames")]
    //Para gestion de frames de invulnerabilidad (no recibir da�o constante y rapido)
    public float duracionInv;
    float temporizadorInv;
    bool esInvulnerable;

    //Para la barra de vida
    [Header("UI")]
    public Image barraHp;
    public TextMeshProUGUI MonedasUI;


    [Header("Sonido")]
    //FMOD
    [SerializeField] private EventReference coinFx;
    [SerializeField] private EventReference buyUpgradeFx;

    private void Start()
    {
        

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
    }

    public void sumarMonedas(int cantidad)
    {
        monedas += cantidad;
        actualizarMonedasUI();
        FMODUnity.RuntimeManager.PlayOneShot(coinFx);
    }

    public void actualizarMonedasUI()
    {
        MonedasUI.text = monedas.ToString();
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
        barraHp.fillAmount = vidaActual / vidaMaximaActual;
    }


    public void Morir()
    {
        if (!GameManager.instancia.juegoFinalizado)
        {
            GameManager.instancia.gameOver();
        }
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

    //La idea es que si te falta vida haya una curaci�n pasiva en funci�n del factor de recuperaci�n
    void curaPasiva()
    {
        if(vidaActual < vidaMaximaActual)
        {
            vidaActual += recuperacionActual * Time.deltaTime;

            //Incluso con la comprobaci�n anterior, a veces se pasa un poco de la vida maxima
            //Es necesaria una segunda comprobaci�n para dejar aplicado el m�ximo
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

        FMODUnity.RuntimeManager.PlayOneShot(buyUpgradeFx);
    }

    public bool trySpendGoldAmount(int goldAmount)
    {
        if(monedas >= goldAmount)
        {
            monedas -= goldAmount;
            actualizarMonedasUI();
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
