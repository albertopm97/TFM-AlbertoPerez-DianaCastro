using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

[ExecuteInEditMode]

public class GroundGenerator : MonoBehaviour
{
    //References
    public SpriteShapeController spriteShapeController;
    public GameObject fuelCanister;

    //Improtant to clear all canisters when changing the ground
    private List<GameObject> fuelCanisterList = new List<GameObject>();

    //variables for terrain generation
    [Range(3f, 100f)] public int levelLenght = 50;
    [Range(1f, 50f)] public float xMultiplier = 2f;
    [Range(1f, 50f)] public float yMultiplier = 2f;
    [Range(0f, 1f)] public float curveSmoothness = 0.5f;

    //Variables for controlling the use of PerlinNoise
    public float noiseStep = 0.5f;
    public float bottom = 10f;

    private Vector3 lastPos;

    //when some value changes in the editor or on script load
    private void OnValidate()
    {
        //Clear all the canisters
        foreach (GameObject g in fuelCanisterList)
        {
            if (g != null)
            {
                fuelCanisterList.Remove(g);
                Destroy(g);
            }
        }

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
                fuelCanisterList.Add(Instantiate(fuelCanister, new Vector3(lastPos.x, lastPos.y + 2, lastPos.z), Quaternion.identity));
            }
        }

        spriteShapeController.spline.InsertPointAt(levelLenght, new Vector3(lastPos.x, transform.position.y - bottom));
        spriteShapeController.spline.InsertPointAt(levelLenght + 1, new Vector3(transform.position.x, transform.position.y - bottom));
    }
}
