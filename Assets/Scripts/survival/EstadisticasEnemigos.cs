using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasEnemigos : MonoBehaviour
{
    public ScriptableObjectEnemigos estadisticas;

    public Animator animator;

    //Estadisticas actuales del enemigo
    [HideInInspector]
    public float rapidezActual;
    [HideInInspector]
    public float vidaActual;
    [HideInInspector]
    public float damageActual;

    public float distanciaLimite = 20f;
    Transform jugador;

    //Particulas para la muerte
    public GameObject blood;

    //Funcion llamada al cargar el script
    void Awake()
    {
        animator = GetComponent<Animator>();

        rapidezActual = estadisticas.Rapidez;
        vidaActual = estadisticas.VidaMaxima;
        damageActual = estadisticas.Damage;
    }

    void Start()
    {
        //jugador = FindObjectOfType<EstadisticasJugador>().transform;

        //Correcci�n de bug
        jugador = FindObjectOfType<EstadisticasJugador>().transform;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, jugador.position) >= distanciaLimite)
        {
            recolocarEnemigo();
        }
    }

    public void recibirAtaque(float dmg)
    {
        vidaActual -= dmg;

        //Si la vida baja a 0 lo matamos
        if(vidaActual <= 0)
        {
            matarEnemigo();
        }
    }

    //Para matar un enemigo simplemente destruimos su gameObject
    public void matarEnemigo()
    {
        Destroy(gameObject);

        GameObject aux = Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(aux, 2f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Si la colision es con un jugador, le aplicamos el da�o actual del enemigo
        if (collision.gameObject.CompareTag("Jugador"))
        {
            EstadisticasJugador jugador = collision.gameObject.GetComponent<EstadisticasJugador>();
            jugador.RecibirAtaque(damageActual);
        }
    }

    private void OnDestroy()
    {
        //Si la escena no esta cargada no hacemos nada (evitar drops al cerrar el juego)
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        SpawnerEnemigos spawner = FindObjectOfType<SpawnerEnemigos>();
        if (spawner != null)
            spawner.OnEnemyKilled();
    }

    void recolocarEnemigo()
    {
        SpawnerEnemigos spawner = FindObjectOfType<SpawnerEnemigos>();
        if(spawner != null)
        {
            transform.position = jugador.position + spawner.puntosSpawn[Random.Range(0, spawner.puntosSpawn.Count - 1)].position;
        }
        
    }

    public void SpawnEnd()
    {
        animator.SetBool("Spawning", false);
    }
}
