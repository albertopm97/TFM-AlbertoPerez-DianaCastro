using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public enum estadoDelJuego
    {
        Gameplay, Pausa, GameOver, Tienda
    }

    public estadoDelJuego estadoDelJuegoActual;
    public estadoDelJuego estadoDelJuegoPrevio;

    public bool juegoFinalizado = false;
    public Text jugadorGanador;
    public bool mejorandoEquipamiento = false;
    public int jugadorSubiendoNivel;

    [Header("UI")]
    public GameObject menuPausa;
    public GameObject menuFinJuego;
    public GameObject menuTienda;
    public GameObject botonesTienda;

    [Header("SpawnerEnemigos")]
    public GameObject spawner;

    [Header("Cronometro")]
    public float tiempoCrono;
    public TextMeshProUGUI tiempoCronoUI;

    [Header("Sonido")]
    //public AudioSource loopJuego;

    public GameObject referenciaJugador;

    //Al comienzo de la partida desactivamos los menus 
    void Awake()
    {
        //Vamos a aplicar el patron singleton para evitar que haya mas de 1 instancia del gamemanager al mismo tiempo
        if(instancia == null)
        {
            instancia = this;
        }
        else
        {
            Debug.LogWarning("Borrada instancia extra del Game Manager");
        }

        desactivarMenus();
    }

    private void Start()
    {
        //TooltipScreenSpaceUI.showTooltip_static("Aqui probando el tooltis desde el GameMaster. GRAAAAANDE EL GAMEMASTER.");
    }

    void Update()
    {
        switch (estadoDelJuegoActual)
        {
            case estadoDelJuego.Gameplay:
                
                comprobadorPausa();
                actualizarCrono();
                /*if (!loopJuego.isPlaying)
                {
                    loopJuego.Play();
                }*/
                break;

            case estadoDelJuego.Pausa:
                
                comprobadorPausa();
                //loopJuego.Pause();
                break;

            case estadoDelJuego.GameOver:

                if (!juegoFinalizado)
                {
                    juegoFinalizado = true;
                    Time.timeScale = 0f;
                    Debug.Log("FIN DEL JUEGO");
                    tiempoCronoUI.text = "";
                    mostrarPantallaFinal();
                    //loopJuego.Stop();
                }
                break;

            case estadoDelJuego.Tienda:

                if (!mejorandoEquipamiento)
                {
                    mejorandoEquipamiento = true;
                    //Time.timeScale = 0f;
                    Debug.Log("Jugador usando la tienda");
                    menuTienda.SetActive(true);
                }
                break;

            default:
                Debug.LogWarning("Estado que no existe");
                break;
        }
    }

    public void cambiarEstadoActual(estadoDelJuego nuevoEstado)
    {
        estadoDelJuegoActual = nuevoEstado;
    }

    public void pausarJuego()
    {
        estadoDelJuegoPrevio = estadoDelJuegoActual;
        cambiarEstadoActual(estadoDelJuego.Pausa);
        Time.timeScale = 0f; //Literalmente para el tiempo
        menuPausa.SetActive(true);
        Debug.Log("Juego Pausado");
    }

    public void reanudarJuego()
    {
        if(estadoDelJuegoActual == estadoDelJuego.Pausa)
        {
            cambiarEstadoActual(estadoDelJuegoPrevio);
            Time.timeScale = 1f;
            menuPausa.SetActive(false);
            Debug.Log("Juego reanudado");
        }
    }

    void comprobadorPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(estadoDelJuegoActual == estadoDelJuego.Pausa)
            {
                reanudarJuego();
            }
            else
            {
                pausarJuego();
            }
        }
    }

    void desactivarMenus()
    {
        //menuPausa.SetActive(false);
        //menuFinJuego.SetActive(false);
        //menuTienda.SetActive(false);
    }

    public void gameOver()
    {
        cambiarEstadoActual(estadoDelJuego.GameOver);
    }

    public void mostrarPantallaFinal() 
    {
        // menuFinJuego.SetActive(true);
    }

    void actualizarCrono()
    {
        tiempoCrono -= Time.deltaTime;

        actualizarCronoUI();

        if(tiempoCrono <= 0)
        {
            inicioMenuTienda();
            tiempoCrono = 30;
        }
    }

    void actualizarCronoUI()
    {
        int minutos = Mathf.FloorToInt(tiempoCrono/60);
        int segundos = Mathf.FloorToInt(tiempoCrono % 60);

        //tiempoCronoUI.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        if(tiempoCronoUI != null)
        {
            tiempoCronoUI.text = string.Format("{00}", segundos);
        }
        
    }

    void inicioMenuTienda()
    {
        cambiarEstadoActual(estadoDelJuego.Tienda);
        UI_Shop scriptTienda = menuTienda.GetComponent<UI_Shop>();
        scriptTienda.generarMenu();
        botonesTienda.SetActive(true);

        //Hay que para el spawner y destruir a todos los enemigos que quedan en la escena
        
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(g);
        }

        spawner.gameObject.SetActive(false);
    }

    public void finMenuTienda()
    {
        cambiarEstadoActual(estadoDelJuego.Gameplay);
        UI_Shop scriptTienda = menuTienda.GetComponent<UI_Shop>();
        scriptTienda.desactivarMenu();
        mejorandoEquipamiento = false;
        botonesTienda.SetActive(false);
        //Time.timeScale = 1f;

        //comenzamos la siguiente wave
        spawner.gameObject.SetActive(true);
        spawner.GetComponent<SpawnerEnemigos>().comenzarSiguienteWave();

    }

    /*
    public void inicioMenuMejora()
    {
        cambiarEstadoActual(estadoDelJuego.Tienda);
        referenciaJugador.SendMessage("EliminarOpcionesYAplicarMejoras");
    }
    public void finMenuMejora()
    {
        mejorandoEquipamiento = false;
        Time.timeScale = 1f;
        menuTienda.SetActive(false);
        cambiarEstadoActual(estadoDelJuego.Gameplay);
    }*/
}
