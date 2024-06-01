using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

//[ExecuteInEditMode]

public class GroundGenerator : MonoBehaviour
{
    //References
    public SpriteShapeController spriteShapeController;
    public GameObject fuelCanister;
    public GameObject racefin;

    //Improtant to clear all canisters when changing the ground
    private List<GameObject> fuelCanisterList = new List<GameObject>();

    //variables for terrain generation
    private int levelLenght = 40;
    private float xMultiplier = 8f;
    private float yMultiplier = 2f; //Valores correctos entre 5 y 15
    private float curveSmoothness = 0.387f; //Valores correctos entre 

    //Variables for controlling the use of PerlinNoise
    public float noiseStep = 0.5f; //Aleatorio entre 1 y 100
    public float bottom = 10f; 

    private Vector3 lastPos;

    private void Start()
    {
        fuelCanisterList = new List<GameObject>();

        generateLevel();
    }

    private void generateLevel()
    {
        levelLenght = 40;
        xMultiplier = 8f;
        yMultiplier = Random.Range(5f, 10f);
        curveSmoothness = 0.387f;
        noiseStep = Random.Range(1f, 100f);

        //Clear all the canisters
        fuelCanisterList.Clear();

        //Clear the map
        spriteShapeController.spline.Clear();

        //iterating to create ground points = leverlength
        for(int i = 0; i < levelLenght; i++)
        {
            //Using perlin Noise to avoid sharped peaks
            lastPos = transform.position + new Vector3(i * xMultiplier, Mathf.PerlinNoise(0, i * noiseStep) * yMultiplier);
            spriteShapeController.spline.InsertPointAt(i, lastPos);

            if(i != 0 && i != levelLenght - 1)
            {
                spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
                spriteShapeController.spline.SetLeftTangent(i, Vector3.left * xMultiplier * curveSmoothness);
                spriteShapeController.spline.SetRightTangent(i, Vector3.right * xMultiplier * curveSmoothness);
            }

            //Every 10 lenght put a fuel canister
            if (i % 10 == 0 && i != 0 && i != levelLenght)
            {
                //Instantiate the canister and add to the list
                fuelCanisterList.Add(Instantiate(fuelCanister, new Vector3(lastPos.x, lastPos.y + 1, lastPos.z), Quaternion.identity));
            }

            if(i == levelLenght - 3)
            {

                Instantiate(racefin, new Vector3(lastPos.x, lastPos.y, lastPos.z), Quaternion.identity);
            }
        }

        spriteShapeController.spline.InsertPointAt(levelLenght, new Vector3(lastPos.x, transform.position.y - bottom));
        spriteShapeController.spline.InsertPointAt(levelLenght + 1, new Vector3(transform.position.x, transform.position.y - bottom));
    }
}
