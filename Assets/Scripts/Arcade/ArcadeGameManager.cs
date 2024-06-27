using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;
using FMODUnity; //Para FMOD

public class ArcadeGameManager : MonoBehaviour
{
    public static ArcadeGameManager Instance;

    public GameObject ticketManger;

    public enum estadoDelJuego
    {
        Gameplay, Pausa
    }

    public estadoDelJuego estadoDelJuegoActual;
    public estadoDelJuego estadoDelJuegoPrevio;
    private bool juegoFinalizado;

    //Referencias
    public GameObject menuPausa;

    [Header("Sonido")]
    //FMOD
    [SerializeField] private StudioEventEmitter loopGame;
    [SerializeField] private StudioEventEmitter AmbienceFx;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Extra instance of gameManager deleted");
        }

        Time.timeScale = 1.0f;

        estadoDelJuegoActual = estadoDelJuego.Gameplay;
    }

    private void Start()
    {
        loopGame.Play();

        if(AmbienceFx != null)
        {
            AmbienceFx.Play();
        }

        DontDestroyOnLoad(ticketManger);
    }

    void Update()
    {
        switch (estadoDelJuegoActual)
        {
            case estadoDelJuego.Gameplay:

                comprobadorPausa();
                break;

            case estadoDelJuego.Pausa:

                comprobadorPausa();
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

        FMODUnity.RuntimeManager.PauseAllEvents(true);
    }

    public void reanudarJuego()
    {
        if (estadoDelJuegoActual == estadoDelJuego.Pausa)
        {
            cambiarEstadoActual(estadoDelJuegoPrevio);
            Time.timeScale = 1f;
            menuPausa.SetActive(false);
            Debug.Log("Juego reanudado");

            FMODUnity.RuntimeManager.PauseAllEvents(false);
        }
    }

    void comprobadorPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estadoDelJuegoActual == estadoDelJuego.Pausa)
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
        menuPausa.SetActive(false);

        //Volvemos a dejar el tiempo normal
        Time.timeScale = 1f;
    }

    public void exit()
    {
        loopGame.Stop();
        if (AmbienceFx != null)
        {
            AmbienceFx.Stop();
        }
        FMODUnity.RuntimeManager.PauseAllEvents(false);
        Application.Quit();
        Debug.Log("Leaving app....");
    }

    public void stopMusic()
    {
        loopGame.Stop();
        if (AmbienceFx != null)
        {
            AmbienceFx.Stop();
        }
    }
}