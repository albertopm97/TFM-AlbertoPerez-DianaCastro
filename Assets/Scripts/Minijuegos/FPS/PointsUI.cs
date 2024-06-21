using TMPro;
using UnityEngine;

public class PointsUI : MonoBehaviour
{
    public static PointsUI instance;

    private int currentPoints;
    private TextMeshProUGUI pointsUIText;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Extra isntance of moneyUI deleted");
        }

        currentPoints = 0;

        pointsUIText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refreshUI(int pointsToAdd)
    {
        print(pointsToAdd);
        currentPoints += pointsToAdd;

        pointsUIText.text = currentPoints.ToString();
    }

    public int getcurrentPoints()
    {
        return currentPoints;
    }
}
