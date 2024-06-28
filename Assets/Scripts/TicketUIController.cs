using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TicketUIController : MonoBehaviour
{

    public static TicketUIController instance;

    private TextMeshProUGUI textTiquetUI;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            print("Extra instace of ticketUIController deleted");
        }

        textTiquetUI = GetComponent<TextMeshProUGUI>();
    }
}
