using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class finalTextController : MonoBehaviour
{
    public TextMeshProUGUI finalText;
    [SerializeField] private EventReference failFx;
    [SerializeField] private EventReference winFx;

    private void OnEnable()
    {
        int finalPoints = PointsUI.instance.getcurrentPoints();

        if(finalPoints > 80)
        {
            FMODUnity.RuntimeManager.PlayOneShot(winFx);
            finalText.text = "Nice Shot";
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(failFx);
            finalText.text = "Bud Luck!";
        }
    }
}
