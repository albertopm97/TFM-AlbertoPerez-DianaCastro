using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{

    //Limites del mapa (Para que el jugador no se puda salir) --> el mapa es cuadrado
    public float limite = 40.0f;

    //Para referenciar mas tarde
    Rigidbody2D rb; 
    Transform tr;
    EstadisticasJugador jugador;
    PlayerInput playerInput;
    Animator animator;

    public GameObject barravida;

    /*Esto lo hacemos porque necesitamos acceder a la direccion desde otros scripts pero no queremos modificarlo en el inspector
     Es buena práctica mantener el inspector lo más limpio posible*/
    [HideInInspector] 
    public Vector2 direccion;


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

    //Hacemos uso del New Input System 
   /*public void OnMove(InputAction.CallbackContext context)
    {

        movimientoInput = context.ReadValue<Vector2>();
        //GestionInput();
    }*/

    // Start is called before the first frame update
    void Start()
    {
        jugador = GetComponent<EstadisticasJugador>();
        //Al inicio almacenamos en la variable el componente RigidBody2D del jugador
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        //Inicializamos el ultimo vector de movimiento a la posicion original del jugador (hacia la derecha)
        ultimoMovimiento = new Vector2(1, 0f);


    }

    // Update is called once per frame
    void Update()
    {
        GestionInput();

        //cambiamos escala a negativa si vamos en direccion derecha
        if (rb.velocity.x > 0)
        {
            if (barravida != null)
            {
                // Desvincula el objeto hijo del padre
                barravida.transform.SetParent(null);
            }

            transform.localScale = new Vector2(-1, 1);

            if (barravida != null)
            {
                // Desvincula el objeto hijo del padre
                barravida.transform.SetParent(this.transform);
            }
        }
        else if (rb.velocity.x < 0) //Si nos movemos a la izquierda, cambiamos la escala a negativo para invertir el prota (con todos sus componentes)
        {
            if (barravida != null)
            {
                // Desvincula el objeto hijo del padre
                barravida.transform.SetParent(null);
            }

            transform.localScale = new Vector2(1, 1);

            if (barravida != null)
            {
                // Desvincula el objeto hijo del padre
                barravida.transform.SetParent(this.transform);
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
        
        direccion = new Vector2(moverX, moverY).normalized;

        //Inicializamos el valor el ultimo vector horizontal y vertical
        if(direccion.x != 0)
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
        rb.velocity = new Vector2(direccion.x * jugador.velocidadMovimientoActual * Time.deltaTime, direccion.y * jugador.velocidadMovimientoActual * Time.deltaTime);

        if(direccion.x != 0 || direccion.y != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
