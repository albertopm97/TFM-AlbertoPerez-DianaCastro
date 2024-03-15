using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ScriptableObjectArma weaponStats;

    private float cdActual;
    private Transform targetPos;
    protected Vector3 shootDir;


    // Start is called before the first frame update
    void Start()
    {
        cdActual = weaponStats.Enfriamiento;
    }

    // Update is called once per frame
    void Update()
    {
        findClosestEnemy();

        cdActual -= Time.deltaTime;

        if (cdActual <= 0)
        {
            PlayerShoot();

            cdActual = weaponStats.Enfriamiento;
        }
        
    }

    void PlayerShoot()
    {
        GameObject aux = Instantiate(weaponStats.Prefab, this.gameObject.transform.position, Quaternion.identity);
         
        //Calculamos la direccion del disparo
        shootDir = (targetPos.transform.position - this.transform.position).normalized;

        BaseBullet bullet = aux.GetComponent<BaseBullet>();

        bullet.initialize(shootDir, weaponStats.Rapidez);
    }

    void findClosestEnemy()
    {
        float distanceToClosest = Mathf.Infinity;

        Enemy closestEnemy = null;

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        print("Tamaño array enemigos: " + enemies.Length);

        foreach (Enemy e in enemies)
        {
            float distanceToCurrentEnemy = (e.transform.position - this.transform.position).sqrMagnitude;

            if (distanceToCurrentEnemy < distanceToClosest)
            {
                distanceToClosest = distanceToCurrentEnemy;

                closestEnemy = e;
            }
        }

        targetPos = closestEnemy.transform;

        //Dibujamos una linea que une al jugador con el enemigo mas cercano solo para debug
        Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
    }
}
