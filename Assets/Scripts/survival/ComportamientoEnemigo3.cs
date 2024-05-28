using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ComportamientoEnemigo3 : ComporamientoEnemigo
{
    enum estado { siguiendo, cargandoAtaque, Atacando};

    public float distanciaCarga;
    public float fuerzaCarga;
    public Vector2 direccionCarga;
    private estado estadoActual;
    private Animator animator;

    private bool atackDirCalculed;

    // Start is called before the first frame update
    void Start()
    {
        enemigo = GetComponent<EstadisticasEnemigos>();
        jugador = FindObjectOfType<MovimientoJugador>().transform;
        animator = GetComponent<Animator>();

        estadoActual = estadoActual = estado.siguiendo;
        distanciaCarga = 5f;
        fuerzaCarga = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        switch(estadoActual)
        {
            case estado.siguiendo:

                print("Cargando ataqueeeeee!!");

                transform.position = Vector2.MoveTowards(transform.position, jugador.transform.position, enemigo.rapidezActual * Time.deltaTime);

                if (jugador.transform.position.x - transform.position.x > 0)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else
                {
                    transform.localScale = new Vector2(1, 1);
                }

                //Si llegamos al umbral del ataque, calculamos la direccion del dash y activamos el ataque
                if (Vector3.Distance(transform.position, jugador.position) <= distanciaCarga)
                {
                    estadoActual = estado.cargandoAtaque;
                }

                break;

            case estado.cargandoAtaque:
                print("Cargando ataqueeeeee!!");

                //Activamos el ataque pasados dos segundos y activamos animacion de carga
                Invoke("cargarAtaque", 1.5f);
                animator.SetBool("CargandoAtaque", true);

                direccionCarga = (jugador.position - this.transform.position).normalized;
                break;

            case estado.Atacando:

                print(direccionCarga.ToString());

                if (!atackDirCalculed)
                {
                    direccionCarga = (jugador.position - this.transform.position).normalized;
                    atackDirCalculed = true;
                }
                
                transform.Translate(direccionCarga * fuerzaCarga * Time.deltaTime);

                Invoke("finAtaque", 0.3f);
                break;
        }
    }

    void cargarAtaque()
    {
        estadoActual = estado.Atacando;
        animator.SetBool("CargandoAtaque", false);
    }

    void finAtaque()
    {
        estadoActual = estado.siguiendo;
        animator.SetBool("CargandoAtaque", false);

        atackDirCalculed = false;
    }
}
