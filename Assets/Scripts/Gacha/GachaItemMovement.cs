using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaItemMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool pulled;
    private bool moviendo;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(!moviendo && pulled)
        {
            StartCoroutine("moveItem");

            moviendo = true;
        }
          
    }

    public void startMoving()
    {
        pulled = true;
    }

    public void stopMoving()
    {
        pulled = false;
    }

    public IEnumerator moveItem()
    {
        Vector3 randomDir = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f);

        float randomForce = Random.Range(100f, 1000f);

        rb.AddForce(randomDir *  randomForce);

        yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

        moviendo = false;
    }
}
