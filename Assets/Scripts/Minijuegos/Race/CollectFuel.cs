using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class CollectFuel : MonoBehaviour
{
    [Header("Sonido")]
    //FMOD
    [SerializeField] private EventReference fuelFx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FuelController.instance.fillFuel();
            FMODUnity.RuntimeManager.PlayOneShot(fuelFx);
            Destroy(gameObject);
        }
    }
}
