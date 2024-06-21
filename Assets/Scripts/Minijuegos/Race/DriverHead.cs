using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverHead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            RaceGameManager.Instance.gameOver();
        }
    }
}

