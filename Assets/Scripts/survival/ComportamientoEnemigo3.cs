using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoEnemigo3 : ComporamientoEnemigo
{
    enum estado { siguiendo, cargandoAtaque, Atacando};

    public float distanciaCarga;
    private estado estadoActual;

    // Start is called before the first frame update
    void Start()
    {
        enemigo = GetComponent<EstadisticasEnemigos>();
        jugador = FindObjectOfType<MovimientoJugador>().transform;

        estadoActual = estadoActual = estado.siguiendo;
        distanciaCarga = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        switch(estadoActual)
        {
            case estado.siguiendo:
                transform.position = Vector2.MoveTowards(transform.position, jugador.transform.position, enemigo.rapidezActual * Time.deltaTime);

                if(Vector3.Distance(transform.position, jugador.position) <= distanciaCarga)
                {
                    estadoActual = estado.cargandoAtaque;
                }
                break;

            case estado.cargandoAtaque:
                print("Cargando ataqueeeeee!!");

                //Activamos el ataque pasados dos segundos
                Invoke("cargarAtaque", 2f);
                break;

            case estado.Atacando:

                print("Atacandoo :D");
                break;
        }
    }

    void cargarAtaque()
    {
        estadoActual = estado.Atacando;
    }
}
