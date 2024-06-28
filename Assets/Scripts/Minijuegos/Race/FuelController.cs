using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
    public static FuelController instance;

    [SerializeField] private Image fuelBar;
    [SerializeField, Range(0.1f, 5f)] private float fuelDrainSpeed = 1f;
    [SerializeField] private float maxFuel = 100f;
    [SerializeField] private Gradient fuelBarGradient;

    private float currentFuel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Extra instance of FuelController deleted");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentFuel = maxFuel;
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        currentFuel -= Time.deltaTime * fuelDrainSpeed;
        updateUI();

        if(currentFuel <= 0)
        {
            RaceGameManager.Instance.gameOver();
        }
    }

    private void updateUI()
    {
        fuelBar.fillAmount = currentFuel / maxFuel;
        fuelBar.color = fuelBarGradient.Evaluate(fuelBar.fillAmount);
    }

    public void fillFuel()
    {
        currentFuel = maxFuel;
        updateUI();
    }
}
