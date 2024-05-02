using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowController : MonoBehaviour
{
    public Transform moveP1;
    public Transform moveP2;
    public float moveSpeed;

    private Rigidbody2D rb;
    private int lastPointVisited;
    private Vector3 direccion;

    public bool moviendo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPointVisited = 2;
        direccion = Vector3.left;
        moviendo = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (moviendo)
        {
            rb.velocity = new Vector2(direccion.x * moveSpeed * Time.deltaTime, 0);
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
            if(lastPointVisited == 2)
            {
                lastPointVisited = 1;
                direccion = Vector3.right;
            }
            else
            {
                lastPointVisited = 2;
                direccion = Vector3.left;
            }
        }
    }

}
