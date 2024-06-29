using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using JetBrains.Annotations;

public class GachaTicketManager : MonoBehaviour
{
    public static GachaTicketManager instance;

    public int numTickets;

private bool saved;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("Extra instance of gachaTicketManager deleted");
        }

        if (!saved)
        {
            DontDestroyOnLoad(this);
            saved = true;
        }
    }

    public void addTicket()
    {
        numTickets++;

        GameObject TextUI = GameObject.FindGameObjectWithTag("TicketText");

        TextUI.GetComponent<TextMeshProUGUI>().text = numTickets.ToString();
    }

    public void removeTicket()
    {
        numTickets--;

        GameObject TextUI = GameObject.FindGameObjectWithTag("TicketText");

        TextUI.GetComponent<TextMeshProUGUI>().text = numTickets.ToString();

        print("Quitando tiquet");
    }
}
