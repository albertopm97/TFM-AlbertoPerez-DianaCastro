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
    public TextMeshProUGUI TiquetCountUI;

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

        TiquetCountUI.text = numTickets.ToString();
    }

    public void removeTicket()
    {
        numTickets--;

        TiquetCountUI.text = numTickets.ToString();

        print("Quitando tiquet");
    }
}
