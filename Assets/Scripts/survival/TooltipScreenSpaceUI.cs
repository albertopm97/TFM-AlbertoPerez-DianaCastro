using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipScreenSpaceUI : MonoBehaviour
{
    public static TooltipScreenSpaceUI Instance { get; private set; }

    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform backgroundRedTransform;
    private TextMeshProUGUI UIText;
    private RectTransform rectTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            print("Instancia de tooltip extra borrada");
        }
        

        backgroundRedTransform = transform.Find("Background").GetComponent<RectTransform>();
        UIText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();

        //setText("Probando el tultis");
        hideTooltip();
    }

    private void Update()
    {
        //Importante tener en cuenta la escala del canvas (cambia al cambiar de tamaño  la pantalla). En caso contrario el tooltip no se ajusta bien a la posicion del raton
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x; //Cualquier componente de la escala serviria, puesto que varía uniformemente en las 3.

        //Importante que el tooltip no se salga de pantalla por ningun extremo
        if (anchoredPosition.x + backgroundRedTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRedTransform.rect.width;
        }
        if (anchoredPosition.x < 0)
        {
            anchoredPosition.x = 0;
        }
        if (anchoredPosition.y + backgroundRedTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRedTransform.rect.height;
        }
        if (anchoredPosition.y < 0)
        {
            anchoredPosition.y = 0;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void setText(string tooltipText)
    {
        UIText.SetText(tooltipText);
        UIText.ForceMeshUpdate();

        Vector2 textSize = UIText.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);

        backgroundRedTransform.sizeDelta = textSize + paddingSize;
    }

    private void showTooltip(string text)
    {
        gameObject.SetActive(true);
        setText(text);
    }

    private void hideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void showTooltip_static(string text)
    {
        Instance.showTooltip(text);
    }

    public static void hideTooltip_static()
    {
        Instance.hideTooltip();
    }
}
