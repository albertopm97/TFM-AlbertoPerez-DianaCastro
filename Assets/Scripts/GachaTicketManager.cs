using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GachaTicketManager : MonoBehaviour
{
    public static GachaTicketManager instance;

    public int numTickets;

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
    }
}
