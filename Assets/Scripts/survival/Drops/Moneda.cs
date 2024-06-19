using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaXp : Recolectable, IColeccionable
{
    public int valor;

    //Implementamos la interfaz, en este caso al tomar una moneda de xp, la incrementamos en la medida necesaria y destruimos la moneda
    public void coger()
    {
        //EstadisticasJugador player = FindObjectOfType<EstadisticasJugador>();

        //Corrección bug
        jugador.sumarMonedas(valor);
        Debug.Log("Me han cogido");
    }
}
