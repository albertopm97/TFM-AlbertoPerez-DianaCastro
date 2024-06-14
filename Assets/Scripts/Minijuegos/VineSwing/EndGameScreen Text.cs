using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScreenText : MonoBehaviour
{

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
        }
        else if (distance > 0 && distance <= 20)
        {
            textMeshProUGUI.text = "BAD JUMP!";
        }
        else if (distance > 20 && distance <= 40)
        {
            textMeshProUGUI.text = "REGULAR JUMP!";
        }
        else if (distance > 40 && distance <= 60)
        {
            textMeshProUGUI.text = "NICE JUMP!";
        }
        else if (distance > 60 && distance <= 80)
        {
            textMeshProUGUI.text = "EXCELLENT JUMP!";
        }
        else if (distance > 80)
        {
            textMeshProUGUI.text = "BRUTAL JUMP!";
        }
    }
}
