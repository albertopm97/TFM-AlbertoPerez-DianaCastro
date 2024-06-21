using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UI_Timer : MonoBehaviour
{
    public float tiempoCrono = 30f;

    private TextMeshProUGUI timerUI;

    public FPSShootBullet shotController;

    // Start is called before the first frame update
    void Start()
    {
        timerUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tiempoCrono -= Time.deltaTime;
        actualizarCronoUI();

        if(tiempoCrono <= 0)
        {
            this.gameObject.SetActive(false);
            shotController.enabled = false;
            Minigame2Manager.Instance.gameOver();
        }
    }

    void actualizarCronoUI()
    {
        int minutos = Mathf.FloorToInt(tiempoCrono / 60);
        int segundos = Mathf.FloorToInt(tiempoCrono % 60);

        //tiempoCronoUI.text = string.Format("{0:00}:{1:00}", minutos, segundos);
        if (timerUI != null)
        {
            timerUI.text = string.Format("{00}", segundos);
        }

    }
}
