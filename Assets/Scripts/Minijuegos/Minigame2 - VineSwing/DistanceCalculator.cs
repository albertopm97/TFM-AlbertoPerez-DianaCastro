using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceCalculator : MonoBehaviour
{

    public GameObject startPosition;

    public TextMeshProUGUI distanceUI;
    private float distance;

    

    // Update is called once per frame
    void Update()
    {
        distance = (startPosition.transform.position.x + transform.position.x);

        distanceUI.text = distance.ToString("F1") + "M";
    }
}
