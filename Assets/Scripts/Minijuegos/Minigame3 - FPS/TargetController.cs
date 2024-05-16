using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Transform moveP1;
    public Transform moveP2;
    public float moveSpeed;

    private Rigidbody2D rb;
    private Vector3 direccion;

    public bool moviendo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(transform.position.y > 0)
        {
            direccion = Vector3.down;
        }
        else 
        { 
            direccion = Vector3.up;
        }

        
        moviendo = true;
    }

    private void FixedUpdate()
    {
        if (moviendo)
        {
            rb.velocity = new Vector2(0f, direccion.y * moveSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

        }
    }
}
