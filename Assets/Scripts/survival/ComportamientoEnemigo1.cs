using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoEnemigo1 : ComporamientoEnemigo
{

    // Start is called before the first frame update
    void Start()
    {
        enemigo = GetComponent<EstadisticasEnemigos>();
        jugador = FindObjectOfType<MovimientoJugador>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(jugador.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
           transform.localScale = new Vector2(1, 1);
        }

        transform.position = Vector2.MoveTowards(transform.position, jugador.transform.position, enemigo.rapidezActual * Time.deltaTime);
    }
}
