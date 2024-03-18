using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int piercing;

    private void Awake()
    {
        Weapon weapon = FindObjectOfType<Weapon>();
        piercing = weapon.piercingActual;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            print("Hit en objetivo: " + enemy.gameObject.name);

            //atravesamos tantos enemigos como valor de piercing
            piercing--;

            if(piercing <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
