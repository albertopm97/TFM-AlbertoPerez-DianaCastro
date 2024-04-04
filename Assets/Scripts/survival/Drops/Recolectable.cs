using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aqui definimos los comportamientos comunes a todas los items recolectables
public class Recolectable : MonoBehaviour
{
    public EstadisticasJugador jugador;

    private void Start()
    {
        //jugador = FindObjectOfType<EstadisticasJugador>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jugador")
        {
            jugador = collision.GetComponent<EstadisticasJugador>();

            if(jugador != null)
            {
                Destroy(gameObject);
            }
        }
    }
}
