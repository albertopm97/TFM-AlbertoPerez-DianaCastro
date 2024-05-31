using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class Minigame2Manager : MonoBehaviour
{
    public static Minigame2Manager Instance;

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
                    //loopJuego.Stop();
                }
                break;

            case estadoDelJuego.Finish:

                if (!juegoFinalizado)
                {
                    juegoFinalizado = true;
                    Time.timeScale = 0f;
                    Debug.Log("FIN DEL JUEGO");
                    mostrarPantallaVictoria();
                    //loopJuego.Stop();
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
    }

    public void reanudarJuego()
    {
        if (estadoDelJuegoActual == estadoDelJuego.Pausa)
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
        Application.Quit();
        Debug.Log("Leaving app....");
    }
}
