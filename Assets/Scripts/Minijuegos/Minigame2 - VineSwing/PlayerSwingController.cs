using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isSwinging = false;
    private HingeJoint2D hingeJoint;
    private GameObject vine;
    private BoxCollider2D bc;

    public float swingForce;

    public CinemachineVirtualCamera vc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hingeJoint = gameObject.GetComponent<HingeJoint2D>();
        hingeJoint.enabled = false;
        bc = GetComponent<BoxCollider2D>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vine")
        {
            GrabVine(collision.gameObject);
        }
    }

    private void GrabVine(GameObject vine)
    {
        isSwinging = true;
        this.vine = vine;
        hingeJoint.connectedBody = vine.GetComponent<Rigidbody2D>();
        hingeJoint.enabled = true;

        print("Grabeada la verga");
        bc.enabled = false;


    }

    public void ReleaseVine()
    {
        if(isSwinging)
        {
            isSwinging = false;
            hingeJoint.connectedBody = null;
            hingeJoint.enabled = false;
            //rb.velocity = new Vector2(rb.velocity.x, 0); // Reinicia la velocidad vertical
            rb.AddForce(Vector2.right * swingForce, ForceMode2D.Impulse);

            vc.Follow = this.transform;
        }
        
    }

    public void swingRight()
    {
        rb.AddForce(Vector2.right * swingForce, ForceMode2D.Impulse);
    }

    public void swingLeft()
    {
        rb.AddForce(Vector2.left * swingForce, ForceMode2D.Impulse);
    }
}
