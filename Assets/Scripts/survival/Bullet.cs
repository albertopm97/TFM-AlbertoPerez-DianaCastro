using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Weapon weapon;
    private int piercing;
    private float dmg;

    private void Awake()
    {
        weapon = FindObjectOfType<Weapon>();
        piercing = weapon.piercingActual;
        dmg = weapon.damageActual * weapon.poderActual;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        EstadisticasEnemigos estadisticasEnemigo = collision.GetComponent<EstadisticasEnemigos>();

        if (enemy != null && estadisticasEnemigo != null)
        {
            print("Hit en objetivo: " + enemy.gameObject.name);
            estadisticasEnemigo.recibirAtaque(dmg);

            //atravesamos tantos enemigos como valor de piercing
            piercing--;

            if(piercing <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
