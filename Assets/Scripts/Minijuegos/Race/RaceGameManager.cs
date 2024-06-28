using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;
using FMODUnity; //Para FMOD

public class RaceGameManager : MonoBehaviour
{
    public static RaceGameManager Instance;

    public enum estadoDelJuego
    {
        Gameplay, Pausa, GameOver, Finish
    }

    [SerializeField] private GameObject gameOverScreen;
    public estadoDelJuego estadoDelJuegoActual;
    public estadoDelJuego estadoDelJuegoPrevio;
    private bool juegoFinalizado;

    //Referencias
    public GameObject menuPausa;
    public GameObject menuFinish;

    [Header("Sonido")]
    //FMOD
    [SerializeField] private EventReference winFx;
    [SerializeField] private EventReference gameOverFx;
    [SerializeField] private StudioEventEmitter loopRaceEvent;

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
        loopRaceEvent.Play();
    }

    void Update()
    {
        switch (estadoDelJuegoActual)
        {
            case estadoDelJuego.Gameplay:

                comprobadorPausa();
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
                    mostrarPantallaGameOver();
                    loopRaceEvent.Stop();
                    FMODUnity.RuntimeManager.PlayOneShot(gameOverFx);
                }
                break;

            case estadoDelJuego.Finish:

                if (!juegoFinalizado)
                {
                    juegoFinalizado = true;
                    Time.timeScale = 0f;
                    Debug.Log("FIN DEL JUEGO");
                    mostrarPantallaVictoria();
                    loopRaceEvent.Stop();
                    FMODUnity.RuntimeManager.PlayOneShot(winFx);
                    GachaTicketManager.instance.addTicket();
                }
                break;

            default:
                Debug.LogWarning("Estado que no existe");
                break;
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        gameOverScreen.SetActive(false);
        menuFinish.SetActive(false);

        //Volvemos a dejar el tiempo normal
        Time.timeScale = 1f;
    }

    public void gameOver()
    {
        cambiarEstadoActual(estadoDelJuego.GameOver);
    }

    public void finish()
    {
        cambiarEstadoActual(estadoDelJuego.Finish);
    }

    public void mostrarPantallaGameOver()
    {
        gameOverScreen.SetActive(true);
    }
    public void mostrarPantallaVictoria()
    {
        menuFinish.SetActive(true);
    }

    public void exit()
    {
        loopRaceEvent.Stop();
        FMODUnity.RuntimeManager.PauseAllEvents(false);
        Application.Quit();
        Debug.Log("Leaving app....");
    }

    public void leaveArcade()
    {
        loopRaceEvent.Stop();
        FMODUnity.RuntimeManager.PauseAllEvents(false);
        SceneManager.LoadScene("Arcade");
    }
}