using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDialog : MonoBehaviour
{
    public GameObject flowchart;

    bool canTalk;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canTalk = false;
        }
    }

    public void talk()
    {
        if(canTalk)
        {
            print("Talking...");
            flowchart.SetActive(true);
        }
    }
}
