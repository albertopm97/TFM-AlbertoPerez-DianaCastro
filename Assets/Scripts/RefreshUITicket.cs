using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RefreshUITicket : MonoBehaviour
{
    private void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();

        text.text = GachaTicketManager.instance.numTickets.ToString();
    }
}
