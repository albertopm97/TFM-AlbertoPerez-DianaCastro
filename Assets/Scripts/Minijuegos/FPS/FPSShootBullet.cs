using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class FPSShootBullet : MonoBehaviour
{
    public GameObject bullet;

    bool shooting = false;

    [SerializeField] private EventReference shotFx;

    private void Update()
    {
        //Gestion del disparo
        if (Input.GetMouseButtonDown(0))
        {  
            shoot();
        }

        //Arreglar r2
        if(Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    public void shoot()
    {
        GameObject instantiatedBullet = Instantiate(bullet);
        instantiatedBullet.transform.position = transform.position;
        instantiatedBullet.transform.rotation = transform.rotation;
        FMODUnity.RuntimeManager.PlayOneShot(shotFx);
        Destroy(instantiatedBullet, 10f);

    }
}
