using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minigame1PlayerController : MonoBehaviour
{
    public float jumpForce;

    //Referencias
    public GameObject flecha;
    public Transform posicionSalto;
    Rigidbody2D rb;

    private bool saltando;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (saltando)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(posicionSalto.position.x, posicionSalto.position.y), jumpForce * Time.deltaTime);
        }
        
    }

    public void jump()
    {
        saltando = true;

        //Paramos el movimiento de la flecha
        flecha.GetComponent<ArrowController>().moviendo = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            saltando = false;

            Vector3 newPos = flecha.transform.position;

            newPos.y = transform.position.y;

            transform.position = newPos;

            //activamos la gravedad para la caida
            rb.gravityScale = 1f;
        }
    }
}