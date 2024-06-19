using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ScriptableObjectArma weaponStats;

    [Header("Prefabs niveles de ataque")]
    public GameObject atack2;
    public GameObject atack3;
    public GameObject atack4;
    public GameObject atack5;

    //Estadisticas necesarias
    public int numProjectiles;
    public int piercingActual;
    public float damageActual;
    public float velocidadAtaqueActual;
    public int numProyectilesActual;
    public float velocidadProyectilActual;
    public float poderActual;

    //Para enfriamiento
    private float cdActual;

    //Para calculo de direccion
    private Transform targetPos;
    protected Vector3 shootDir;


    // Start is called before the first frame update
    void Start()
    {
        //Inicializaciones de variables
        cdActual = weaponStats.Enfriamiento;
        numProjectiles = weaponStats.NumProject;
        piercingActual = weaponStats.Perforacion;
        damageActual = weaponStats.Damage;
        velocidadAtaqueActual = weaponStats.Enfriamiento;
        velocidadProyectilActual = weaponStats.Rapidez;
        numProyectilesActual = 1;
        poderActual = 1;
    }

    // Update is called once per frame
    void Update()
    {
        findClosestEnemy();

        cdActual -= Time.deltaTime;

        if (cdActual <= 0)
        {
            //Disparamos si hay algun enemigo activo en la escena
            if(targetPos != null)
            {
                PlayerShoot();
            }
            

            cdActual = velocidadAtaqueActual;
        }
    }

    void PlayerShoot()
    {

        GameObject intantiatePrefab;
        switch (numProyectilesActual)
        {
            case 1:
                intantiatePrefab = weaponStats.Prefab;
                break;

            case 2:
                intantiatePrefab = atack2;
                break;

            case 3:
                intantiatePrefab = atack3;
                break;

            case 4:
                intantiatePrefab = atack4;
                break;

            case 5:
                intantiatePrefab = atack5;
                break;

            default:
                intantiatePrefab = weaponStats.Prefab;
                break;
        }
        GameObject aux = Instantiate(intantiatePrefab, this.gameObject.transform.position, Quaternion.identity);
         
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

        //print("Tamaño array enemigos: " + enemies.Length);

        foreach (Enemy e in enemies)
        {
            float distanceToCurrentEnemy = (e.transform.position - this.transform.position).sqrMagnitude;

            if (distanceToCurrentEnemy < distanceToClosest)
            {
                distanceToClosest = distanceToCurrentEnemy;

                closestEnemy = e;
            }
        }

        //Comprobamos que haya enemigos actualmente en escena
        if(closestEnemy != null)
        {
            targetPos = closestEnemy.transform;

            //Dibujamos una linea que une al jugador con el enemigo mas cercano solo para debug
            Debug.DrawLine(this.transform.position, closestEnemy.transform.position); 
        }
        
    }
}
