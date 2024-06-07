using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComportamientoEnemigo2 : ComporamientoEnemigo
{

    public List<Transform> basePatrolPoints; // Puntos de patrulla
    public float patrolSpeed = 2f; // Velocidad de patrulla
    public float chaseSpeed = 4f; // Velocidad de persecución
    public float chaseRange = 15f; // Rango de detección del jugador

    public List<Vector3> patrolPoints;
    private int currentPatrolIndex = 0;
    private bool patrolling = true;
     
    // Start is called before the first frame update
    void Start()
    {
        enemigo = GetComponent<EstadisticasEnemigos>();
        jugador = FindObjectOfType<MovimientoJugador>().transform;

        inicializarPatrolPoints();
    }

    // Update is called once per frame
    void Update()
    {
        // Si el jugador está dentro del rango de detección
        if (Vector3.Distance(transform.position, jugador.position) <= chaseRange)
        {
            patrolling = false; // Deja de patrullar
            ChasePlayer();
        }
        else
        {
            patrolling = true;
            Patrol();

            print(patrolPoints[0]);
            print(patrolPoints[1]);
            print(patrolPoints[2]);
            print(patrolPoints[3]);
        }
    }

    void inicializarPatrolPoints()
    {
        foreach (Transform t in basePatrolPoints)
        {
            patrolPoints.Add(t.position);
        }
    }

    void Patrol()
    {
        if (patrolPoints.Count == 0)
            return;

        // Mueve al enemigo hacia el siguiente punto de patrulla
        Vector3 targetPosition = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        if (targetPosition.x - transform.position.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

        // Si el enemigo llega al punto de patrulla, avanza al siguiente
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Count;
        }
    }

    void ChasePlayer()
    {
        // Mueve al enemigo hacia la posición del jugador
        transform.position = Vector3.MoveTowards(transform.position, jugador.position, enemigo.rapidezActual * Time.deltaTime);

        if (jugador.transform.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
