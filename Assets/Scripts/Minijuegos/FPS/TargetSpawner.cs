using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    public GameObject target1;
    public GameObject target2;
    public GameObject target3;

    bool instantiating;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!instantiating)
        {
            StartCoroutine(spawnTarget(1f));
        }
        
    }

    IEnumerator spawnTarget(float time)
    {
        instantiating = true;

        float random = Random.Range(0f, 100f);
        GameObject target;

        if (random <= 20)
        {
            target = target3;
        }
        else if (random > 20 && random <= 60)
        {
            target = target2;
        }
        else
        {
            target = target1;
        }

        int randomPointpos = Random.Range(0, spawnPoints.Count);

        //Creamos la bala y seteamos su posicion y su rotacion (sin hacer hijo para evitar bugs de movimiento)
        GameObject instantiatedTarget = Instantiate(target);
        instantiatedTarget.transform.position = spawnPoints[randomPointpos].position;
        instantiatedTarget.transform.rotation = Quaternion.identity;
        Destroy(instantiatedTarget, 20f);

        yield return new WaitForSeconds(time);

        instantiating = false;
    }
}
