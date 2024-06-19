using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaController : MonoBehaviour
{

    public List<GachaItemMovement> gachaItems = new List<GachaItemMovement>();

    public void pull()
    {
        foreach (var item in gachaItems)
        {
            item.startMoving();
        }
    }
}
