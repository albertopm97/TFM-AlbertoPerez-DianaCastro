using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Para referenciar mas tarde
    Rigidbody2D rb;
    Transform tr;
    public float moveSpeed;
    PlayerInput playerInput;
    Animator animator;

    /*Esto lo hacemos porque necesitamos acceder a la direccion desde otros scripts pero no queremos modificarlo en el inspector
     Es buena práctica mantener el inspector lo más limpio posible*/
    [HideInInspector]
    public Vector2 direccion;

    public GameObject enterKey;

    //Movimiento
    //public float velocidadMovimiento;   -> Antigua variable para el movimiento. Ahora se gestiona con el ScriptableObject del personaje
    //Las siguientes variables sirven para saber la ultima direccion en la que se movio el jugador (util al gestionar la inversion de animaciones, movimiento de proyectiles, ...)
    // [HideInInspector] 
    public float ultimoVectorHorizontal;
    //[HideInInspector]
    public float ultimoVectorVertical;
    //[HideInInspector]
    public Vector2 ultimoMovimiento;

    private Vector2 movimientoInput = Vector2.zero;

    void Start()
    {
        //Al inicio almacenamos en la variable el componente RigidBody2D del jugador
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();    

        //Inicializamos el ultimo vector de movimiento a la posicion original del jugador (hacia la derecha)
        ultimoMovimiento = new Vector2(1, 0f);

    }

    void Update()
    {

        GestionInput();

        /*Restriccion del movimiento del jugador al mapa
        if(tr.position.x < -40)
        {
            tr.position = new Vector3(-40, tr.position.y, tr.position.z);
        }
        if (tr.position.x > 40)
        {
            tr.position = new Vector3(40, tr.position.y, tr.position.z);
        }
        if (tr.position.y < -39.7)
        {
            tr.position = new Vector3(tr.position.x, -39.7f, tr.position.z);
        }
        if (tr.position.y > 40)
        {
            tr.position = new Vector3(tr.position.x, 40, tr.position.z);
        }*/

        //cambiamos escala a negativa si vamos en direccion derecha
        if (rb.velocity.x > 0)
        {
            if (enterKey != null)
            {
                // Desvincula el objeto hijo del padre
                enterKey.transform.SetParent(null);
            }

            transform.localScale = new Vector2(-1, 1);

            if (enterKey != null)
            {
                // Desvincula el objeto hijo del padre
                enterKey.transform.SetParent(this.transform);
            }

            //enterKey.transform.localScale = new Vector2(1, 1);
        }
        else if (rb.velocity.x < 0) //Si nos movemos a la izquierda, cambiamos la escala a negativo para invertir el prota (con todos sus componentes)
        {
            if (enterKey != null)
            {
                // Desvincula el objeto hijo del padre
                enterKey.transform.SetParent(null);
            }

            transform.localScale = new Vector2(1, 1);

            if (enterKey != null)
            {
                // Desvincula el objeto hijo del padre
                enterKey.transform.SetParent(this.transform);
            }
        }

    }

    //Usamos esta funcion para el movimiento ya que funciona mejor con las físicas (es independiente del frame rate)
    private void FixedUpdate()
    {
        Mover();
    }

    void GestionInput()
    {
        //No tomamos inputs si el juego ha finalizado (gameOver)
        /*if (GameManager.instancia.juegoFinalizado)
        {
            return;
        }*/

        //float moverX = Input.GetAxisRaw("Horizontal");
        //float moverY = Input.GetAxisRaw("Vertical");

        movimientoInput = playerInput.actions["Move"].ReadValue<Vector2>();

        float moverX = movimientoInput.x;
        float moverY = movimientoInput.y;

        //En la calle solo nos podemos mover en dos direcciones
        direccion = new Vector2(moverX, 0).normalized;

        //Inicializamos el valor el ultimo vector horizontal y vertical
        if (direccion.x != 0)
        {
            ultimoVectorHorizontal = direccion.x;
            ultimoMovimiento = new Vector2(ultimoVectorHorizontal, 0f); //ajustamos ultimo movimiento en x
        }

        if (direccion.y != 0)
        {
            ultimoVectorVertical = direccion.y;
            ultimoMovimiento = new Vector2(0f, ultimoVectorVertical); //ajustamos ultimo movimiento en y
        }

        if (direccion.x != 0 && direccion.y != 0)
        {
            ultimoMovimiento = new Vector2(ultimoVectorHorizontal, ultimoVectorVertical); //ajustamos ultimo movimiento en x,y
        }
    }
    void Mover()
    {
        //No tomamos inputs si el juego ha finalizado (gameOver)
        /*if (GameManager.instancia.juegoFinalizado)
        {
            return;
        }*/

        //se podria hacer la operacion en una sola vez, pero parece ser que haciendolo así se gana algo de rendimiento y facilita al motor de fisicas
        rb.velocity = new Vector2(direccion.x * moveSpeed * Time.deltaTime, 0);

        if (direccion.x != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
