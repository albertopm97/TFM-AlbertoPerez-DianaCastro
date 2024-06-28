using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity; //Para FMOD

public class MainMenuManager : MonoBehaviour
{
    [Header("Sonido")]
    //FMOD
    [SerializeField] private EventReference UIClick;
    [SerializeField] private StudioEventEmitter menuLoop;

    private void Start()
    {
        menuLoop.Play();
    }
    public void exitGame()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClick);
        menuLoop.Stop();
        Application.Quit();
    }

    public void startGame()
    {
        menuLoop.Stop();
        FMODUnity.RuntimeManager.PlayOneShot(UIClick);
        SceneManager.LoadScene("AulaClub");
    }
}
