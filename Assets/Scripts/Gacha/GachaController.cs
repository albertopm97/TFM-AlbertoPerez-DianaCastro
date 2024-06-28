using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity; //Para FMOD

public class GachaController : MonoBehaviour
{

    public List<GachaItemMovement> gachaItems = new List<GachaItemMovement>();

    public GameObject ItemPanel;
    public TextMeshProUGUI ItemNameUI;
    public Image ItemIcon;

    public PlayerInput playerInput;

    [Header("Sonido")]
    //FMOD
    [SerializeField] private EventReference pullFx;

    private bool pulled;

    public void pull()
    {
        if (!pulled)
        {
            pulled = true;
            if (GachaTicketManager.instance.numTickets > 0)
            {


                GachaTicketManager.instance.removeTicket();

                playerInput.enabled = false;

                FMODUnity.RuntimeManager.PlayOneShot(pullFx);

                moveItems();

                Invoke("getItem", 1.5f);

                Invoke("stopItems", 3f);
            }
            else
            {
                ItemNameUI.text = "Not enough gacha tickets!";
                ItemIcon.enabled = false;

                ItemPanel.SetActive(true);

                Invoke("ocultPanel", 2f);
            }
        }
        
        
    }

    //Funcion debe coger un item de decoracion aleatorio, sacarlo de la pull de objetos, notificarlo al script que controla los decorativos y mostrarlo al panel UI.
    public void getItem()
    {
        string RandomItem = null;

        if (DecorationUnlockedChecker.instance.itemsNotUnlocked.Count > 0)
        {
            RandomItem = DecorationUnlockedChecker.instance.itemsNotUnlocked[Random.Range(0, DecorationUnlockedChecker.instance.itemsNotUnlocked.Count - 1)];
            DecorationUnlockedChecker.instance.itemsNotUnlocked.Remove(RandomItem);
        }
        
        if(RandomItem != null)
        {
            switch (RandomItem)
            {
                case "TeddyBear":

                    DecorationUnlockedChecker.instance.TeddyBearUnlocked = true;
                    ItemNameUI.text = "The Teddy Bear!";
                    ItemIcon.sprite = GameAssets.i.TeddyBear;
                    break;

                case "StarSticker":

                    DecorationUnlockedChecker.instance.StarStickerUnlocked = true;
                    ItemNameUI.text = "The Star stiker!";
                    ItemIcon.sprite = GameAssets.i.StarSticker;
                    break;

                case "RacingWolfSticker":

                    DecorationUnlockedChecker.instance.RacingWolfStickerUnlocked = true;
                    ItemNameUI.text = "The Racing Wolf Stiker!";
                    ItemIcon.sprite = GameAssets.i.RacingWolfSticker;
                    break;

                case "GhostSticker":

                    DecorationUnlockedChecker.instance.GhostStickerUnlocked = true;
                    ItemNameUI.text = "The Ghost Stiker!";
                    ItemIcon.sprite = GameAssets.i.GhostSticker;
                    break;

                case "OldConsole":

                    DecorationUnlockedChecker.instance.OldConsoleUnlocked = true;
                    ItemNameUI.text = "The retro console!";
                    ItemIcon.sprite = GameAssets.i.OldConsole;
                    break;

                case "MageFigurine":

                    DecorationUnlockedChecker.instance.MageFigurineUnlocked = true;
                    ItemNameUI.text = "The Mage Figurine!";
                    ItemIcon.sprite = GameAssets.i.MageFigurine;
                    break;

                case "FoxyHeroFigurine":

                    DecorationUnlockedChecker.instance.FoxyHeroFigurineUnlocked = true;
                    ItemNameUI.text = "The Foxy Hero Figurine!";
                    ItemIcon.sprite = GameAssets.i.FoxyHeroFigurine;
                    break;

                case "ElPerroFigurine":

                    DecorationUnlockedChecker.instance.ElPerroFigurineUnlocked = true;
                    ItemNameUI.text = "The El Perro Figurine!";
                    ItemIcon.sprite = GameAssets.i.ElPerroFigurine;
                    break;

                case "KatyCat":

                    DecorationUnlockedChecker.instance.KatyCatUnlocked = true;
                    ItemNameUI.text = "The Katy cat!";
                    ItemIcon.sprite = GameAssets.i.KatyCat;
                    break;

                case "BunnyStressToy":

                    DecorationUnlockedChecker.instance.BunnyStressToyUnlocked = true;
                    ItemNameUI.text = "The Bunny stress toy!";
                    ItemIcon.sprite = GameAssets.i.BunnyStressToy;
                    break;
            }
        }
        else
        {
            ItemNameUI.text = "All items obtained!";
            ItemIcon.enabled = false; 
        }
        

        ItemPanel.SetActive(true);
    }

    public void moveItems()
    {
        foreach (var item in gachaItems)
        {
            item.startMoving();
        }
    }

    public void stopItems()
    {
        foreach (var item in gachaItems)
        {
            item.stopMoving();

            ocultPanel();
        }
    }

    public void ocultPanel()
    {
        ItemPanel.SetActive(false);

        playerInput.enabled = true;

        pulled = false;
    }

    public void returnToArcade()
    {
        SceneManager.LoadScene("Arcade");
    }
}
