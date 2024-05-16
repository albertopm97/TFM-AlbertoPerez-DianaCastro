using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame3FPSBullet : MonoBehaviour
{
    public float projectileSpeed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(projectileSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            //EventsManager.OnEnemyHitted?.Invoke(collision.gameObject, damage);
            print("Objetivo tocado");

            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
