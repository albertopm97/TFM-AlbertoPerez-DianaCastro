using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationController : MonoBehaviour
{
    //public static DecorationController instance;

    //Decoration references
    public GameObject TeddyBear;
    public GameObject StarSticker;
    public GameObject RacingWolfSticker;
    public GameObject GhostSticker;
    public GameObject OldConsole;
    public GameObject MageFigurine;
    public GameObject FoxyHeroFigurine;
    public GameObject ElPerroFigurine;
    public GameObject KatyCat;
    public GameObject BunnyStressToy;

    private void Start()
    {
        /*if(instance == null)
        {
            instance = this;
        }
        else
        {
            print("Extra instance of DecorationController deleted");
        }*/
    }

    public void unlcockTeddyBear()
    {
        TeddyBear.SetActive(true);
    }

    public void unlcockStarSticker()
    {
        StarSticker.SetActive(true);
    }

    public void unlcockRacingWolfSticker()
    {
        RacingWolfSticker.SetActive(true);
    }

    public void unlcockGhostSticker()
    {
        GhostSticker.SetActive(true);
    }

    public void unlcockOldConsole()
    {
        OldConsole.SetActive(true);
    }
    public void unlcockMageFigurine()
    {
        MageFigurine.SetActive(true);
    }
    public void unlcockFoxyHeroFigurine()
    {
        FoxyHeroFigurine.SetActive(true);
    }
    public void unlcockElPerroFigurine()
    {
        ElPerroFigurine.SetActive(true);
    }
    public void unlcockKatyCat()
    {
        KatyCat.SetActive(true);
    }

    public void unlcockBunnyStressToy()
    {
        BunnyStressToy.SetActive(true);
    }
}
