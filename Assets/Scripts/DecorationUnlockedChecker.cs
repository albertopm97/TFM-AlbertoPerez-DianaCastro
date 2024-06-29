using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DecorationUnlockedChecker : MonoBehaviour
{
    public static DecorationUnlockedChecker instance;

    //Decoration references
    public bool TeddyBearUnlocked;
    public bool StarStickerUnlocked;
    public bool RacingWolfStickerUnlocked;
    public bool GhostStickerUnlocked;
    public bool OldConsoleUnlocked;
    public bool MageFigurineUnlocked;
    public bool FoxyHeroFigurineUnlocked;
    public bool ElPerroFigurineUnlocked;
    public bool KatyCatUnlocked;
    public bool BunnyStressToyUnlocked;

    public List<string> itemsNotUnlocked = new List<string>();

    void Awake()
    {
        // Hacer que este objeto persista entre escenas
        DontDestroyOnLoad(this);

        itemsNotUnlocked.Add("TeddyBear");
        itemsNotUnlocked.Add("StarSticker");
        itemsNotUnlocked.Add("RacingWolfSticker");
        itemsNotUnlocked.Add("GhostSticker");
        itemsNotUnlocked.Add("OldConsole");
        itemsNotUnlocked.Add("MageFigurine");
        itemsNotUnlocked.Add("FoxyHeroFigurine");
        itemsNotUnlocked.Add("ElPerroFigurine");
        itemsNotUnlocked.Add("KatyCat");
        itemsNotUnlocked.Add("BunnyStressToy");
    }

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            print("Extra instance of decorationUnlockedChecker deleted");
        }

        // Suscribirse al evento
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Desuscribirse del evento
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Código a ejecutar cuando se carga una nueva escena
        Debug.Log("Nueva escena cargada: " + scene.name);

        if(scene.name == "AulaClub")
        {
            print("Activando Decoraciones desbloqueadas");

            GameObject.Find("ConversacionInicial").SetActive(false);

            DecorationController controller = FindObjectOfType<DecorationController>();

            if(TeddyBearUnlocked)
            {
                //DecorationController.instance.unlcockTeddyBear();
                controller.TeddyBear.SetActive(true);

            }

            if (StarStickerUnlocked)
            {
                //DecorationController.instance.unlcockStarSticker();
                controller.StarSticker.SetActive(true);
            }

            if (RacingWolfStickerUnlocked)
            {
                //DecorationController.instance.unlcockRacingWolfSticker();
                controller.RacingWolfSticker.SetActive(true);
            }

            if (GhostStickerUnlocked)
            {
                //DecorationController.instance.unlcockGhostSticker();
                controller.GhostSticker.SetActive(true);
            }

            if (OldConsoleUnlocked)
            {
                //DecorationController.instance.unlcockOldConsole();
                controller.OldConsole.SetActive(true);
            }

            if (MageFigurineUnlocked)
            {
                //DecorationController.instance.unlcockMageFigurine();
                controller.MageFigurine.SetActive(true);
            }

            if (FoxyHeroFigurineUnlocked)
            {
                //DecorationController.instance.unlcockFoxyHeroFigurine();
                controller.FoxyHeroFigurine.SetActive(true);
            }

            if (ElPerroFigurineUnlocked)
            {
                //DecorationController.instance.unlcockElPerroFigurine();
                controller.ElPerroFigurine.SetActive(true);
            }

            if (KatyCatUnlocked)
            {
                //DecorationController.instance.unlcockKatyCat();
                controller.KatyCat.SetActive(true);
            }

            if (BunnyStressToyUnlocked)
            {
                //DecorationController.instance.unlcockBunnyStressToy();
                controller.BunnyStressToy.SetActive(true);
            }
        }
    }
}