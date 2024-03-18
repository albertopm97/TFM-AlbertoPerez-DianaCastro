using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour 
{
    Vector3 shootDir;
    float rapidez;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shootDir = Vector3.right;
        rapidez = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        //método de movimiento de proyectil sin motor de físicas (cambiar bullet RigidBody de Dynamic a Kinematic
        //transform.position += shootDir * rapidez * Time.deltaTime; transform.position += shootDir * rapidez * Time.deltaTime; 
    }

    public void initialize(Vector3 dir, float speed)
    {
        shootDir = dir;
        rapidez = speed;

        //Debemos rotar el proyectil en la direccion en la que disparamos
        Vector3 normalizedDir = dir.normalized;
        float n = Mathf.Atan2(normalizedDir.y, normalizedDir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        transform.eulerAngles = new Vector3(0, 0, n);

        //También debemos destruir el objeto pasado un tiempo (no queremos acumular proyectiles)
        Destroy(this.gameObject, 5f);

        //impulsamos el proyectil
        rb.AddForce(shootDir * rapidez, ForceMode2D.Impulse);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if(enemy != null)
        {
            print("Hit en objetivo: " + enemy.gameObject.name);
            Destroy(gameObject);
        }
    }*/
}

