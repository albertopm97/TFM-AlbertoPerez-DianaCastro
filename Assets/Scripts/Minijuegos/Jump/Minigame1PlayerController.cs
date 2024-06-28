using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class Minigame1PlayerController : MonoBehaviour
{
    public GameObject enterHole;
    public GameObject hole;

    public float jumpForce;

    //Referencias
    public GameObject flecha;
    public Transform posicionSalto;
    Rigidbody2D rb;
    Animator animator;

    private bool saltando;

    [Header("Sonido")]
    //FMOD
    [SerializeField] private EventReference jumpFx;
    [SerializeField] private EventReference failFx;
    [SerializeField] private EventReference winFx;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
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

        flecha.GetComponent<ArrowController>().gameObject.SetActive(false);

        animator.SetBool("Jumping", true);

        FMODUnity.RuntimeManager.PlayOneShot(jumpFx);
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

            transform.localScale = new Vector2(1, -1);
        }

        if(collision.gameObject.tag == "Target")
        {

            this.gameObject.SetActive(false);

            hole.SetActive(false);

            enterHole.SetActive(true);

            FMODUnity.RuntimeManager.PlayOneShot(winFx);

            GachaTicketManager.instance.addTicket();

            Minigame2Manager.Instance.finish();
        }

        if (collision.gameObject.tag == "Ground")
        {

            transform.localScale = new Vector2(1, 1);

            transform.position = new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z);

            animator.SetBool("Jumping", false);

            rb.velocity = Vector3.zero;

            rb.gravityScale = 0f;

            FMODUnity.RuntimeManager.PlayOneShot(failFx);

            Minigame2Manager.Instance.gameOver();
        }
    }
}
