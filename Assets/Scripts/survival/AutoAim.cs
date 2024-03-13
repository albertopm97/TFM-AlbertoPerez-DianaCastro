using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        findClosestEnemy();
    }

    void findClosestEnemy()
    {
        float distanceToClosest = Mathf.Infinity;

        Enemy closestEnemy = null;

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        print("Tamaño array enemigos: " + enemies.Length);

        foreach(Enemy e in enemies) 
        { 
            float distanceToCurrentEnemy = (e.transform.position - this.transform.position).sqrMagnitude;

            if(distanceToCurrentEnemy < distanceToClosest)
            {
                distanceToClosest = distanceToCurrentEnemy;

                closestEnemy = e;
            }
        }

        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        print(closestEnemy.name);
    }
}
