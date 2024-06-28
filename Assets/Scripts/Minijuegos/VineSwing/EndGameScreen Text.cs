using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class EndGameScreenText : MonoBehaviour
{
    [SerializeField] private EventReference failFx;
    [SerializeField] private EventReference goodFx;
    private TextMeshProUGUI textMeshProUGUI;
    public DistanceCalculator dc;
    private float distance;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();

        distance = dc.getDistance();

        if (distance <= 0)
        {
            textMeshProUGUI.text = "SAD MONKE :(";
            FMODUnity.RuntimeManager.PlayOneShot(failFx);
        }
        else if (distance > 0 && distance <= 20)
        {
            textMeshProUGUI.text = "BAD JUMP!";
            FMODUnity.RuntimeManager.PlayOneShot(failFx);
        }
        else if (distance > 20 && distance <= 40)
        {
            textMeshProUGUI.text = "REGULAR JUMP!";
            FMODUnity.RuntimeManager.PlayOneShot(failFx);
        }
        else if (distance > 40 && distance <= 60)
        {
            textMeshProUGUI.text = "NICE JUMP!";
            FMODUnity.RuntimeManager.PlayOneShot(goodFx);
            GachaTicketManager.instance.addTicket();
        }
        else if (distance > 60 && distance <= 80)
        {
            textMeshProUGUI.text = "EXCELLENT JUMP!";
            FMODUnity.RuntimeManager.PlayOneShot(goodFx);
            GachaTicketManager.instance.addTicket();
        }
        else if (distance > 80)
        {
            textMeshProUGUI.text = "BRUTAL JUMP!";
            FMODUnity.RuntimeManager.PlayOneShot(goodFx);
            GachaTicketManager.instance.addTicket();
        }
    }
}
