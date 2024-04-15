using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    List<Transform> listaItemsTienda;
    private Sprite iconoMonedas;
    private Transform container;
    private Transform shopItemTemplate;

    private IShopCustomer customer;
    private Weapon weapon;

    private void Awake()
    {
        weapon = FindObjectOfType<Weapon>();
        container = transform.Find("container");
        shopItemTemplate = container.Find("ShopItem");
        customer = FindObjectOfType<EstadisticasJugador>();
        listaItemsTienda = new List<Transform>();
        //shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        /*
        crearBotonInterfaz(Mejoras.getSprite(Mejoras.tipoMejora.Salud), "Salud máxima", Mejoras.calcularCoste(Mejoras.tipoMejora.Salud, 2), 0, 1);
        crearBotonInterfaz(Mejoras.getSprite(Mejoras.tipoMejora.Damage), "Mas daño", Mejoras.calcularCoste(Mejoras.tipoMejora.Damage, 3), 1, 3);
        shopItemTemplate.gameObject.SetActive(false);*/
        
        iconoMonedas = GameAssets.i.dinero;

        //generarMenu();
    }
    

    private void crearBotonInterfaz(Mejoras.tipoMejora tipoMejora, Sprite icono, string nombre, int coste, int indicePosicion, int rareza)
    {
        Transform itemTransform = Instantiate(shopItemTemplate, container);
        listaItemsTienda.Add(itemTransform);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();

        float ItemHeight = 150f;
        itemRectTransform.anchoredPosition = new Vector2(0, -ItemHeight * indicePosicion);

        itemTransform.Find("Nombre").GetComponent<TextMeshProUGUI>().SetText(nombre);
        itemTransform.Find("Precio").GetComponent<TextMeshProUGUI>().SetText(coste.ToString());

        itemTransform.Find("IconoMejora").GetComponent<Image>().sprite = icono;

        Color colorFondo;
        switch(rareza)
        {
            case 0:

                colorFondo = new Color(0.901f, 0.862f, 0.862f, 1);
                break;

            case 1:

                colorFondo = new Color(0.258f, 0.564f, 0.960f, 1);
                break;

            case 2:

                colorFondo = new Color(0.325f, 0.294f, 0.454f, 1);
                break;

            case 3:

                colorFondo = new Color(0.631f, 0.372f, 0.133f, 1);
                break;

            default:
                colorFondo = new Color(0.901f, 0.862f, 0.862f, 1);
                break;
        }

        itemTransform.Find("Background").GetComponent<Image>().color = colorFondo;

        itemTransform.Find("IconoDinero").GetComponent<Image>().sprite = iconoMonedas;

        //Creamos el boton interactuable
        itemTransform.GetComponent<Button_UI>().ClickFunc = () =>
        {
            tryBuyItem(tipoMejora, rareza);

            //gameObject.GetComponent<Button_UI>()
            itemTransform.GetComponent<Button_UI>().ClickFunc = () => { };

            itemTransform.Find("Nombre").GetComponent<TextMeshProUGUI>().SetText("Agotado!");

            colorFondo = new Color(0.431f, 0.431f, 0.431f, 1);

            itemTransform.Find("Background").GetComponent<Image>().color = colorFondo;
        };

        itemTransform.gameObject.SetActive(true);
    }

    public void generarMenu()
    {
        desactivarMenu();

        Mejoras.tipoMejora tipoMejora;
        List<Mejoras.tipoMejora> mejorasDisponibles = new List<Mejoras.tipoMejora>();

        //Creamos una lista con todas las mejoras posibles para ir sacando las elegidas
        for(int i = 0; i < 8; i++)
        {
            mejorasDisponibles.Add((Mejoras.tipoMejora)i);
        }

        //Si estamos en el cup de alguna estadistica, sacamos sus mejoras de la pool
        if(weapon.numProyectilesActual >= 5) 
        { 
            mejorasDisponibles.Remove(Mejoras.tipoMejora.numProyectiles);
        }

        if (weapon.velocidadAtaqueActual <= 0.2f)
        {
            mejorasDisponibles.Remove(Mejoras.tipoMejora.velocidadAtaque);
        }

        //  HAY QUE ELEGIR 4 MEJORAS DISTINTAS 
        for (int i = 0; i < 4; i++)
        {
            //Elegimos una mejora y la sacamos de la lista
            tipoMejora = mejorasDisponibles[Random.Range(0, mejorasDisponibles.Count)];

            mejorasDisponibles.Remove(tipoMejora);

            float aleatorioRareza = Random.Range(0f, 100f);
            int rareza = 0;

            //PARA LA RAREZA, CUANTO MAS PEQUEÑO, MAS RARA ES LA RAREZA, DIVIDIR EN RANGOS (0-25 LEGEND, 25-50 EPIC, 50-75 POCO COMUN, 75-100 COMUN)
            if(aleatorioRareza > 75f)
            {
                rareza = 0;
            }
            else if(aleatorioRareza > 50f &&  rareza < 75f)
            {
                rareza = 1;
            }
            else if (aleatorioRareza > 25f && rareza < 50f)
            {
                rareza = 2;
            }
            else
            {
                rareza = 3;
            }


            //teniendo el tipo de mejora y la rareza solo falta crear el item del menu
            switch (tipoMejora)
            {
                default:
                case Mejoras.tipoMejora.Salud: crearBotonInterfaz(Mejoras.tipoMejora.Salud, Mejoras.getSprite(Mejoras.tipoMejora.Salud), "Salud máxima", Mejoras.calcularCoste(Mejoras.tipoMejora.Salud, rareza), i, rareza); break;
                case Mejoras.tipoMejora.CuraPasiva: crearBotonInterfaz(Mejoras.tipoMejora.CuraPasiva, Mejoras.getSprite(Mejoras.tipoMejora.CuraPasiva), "Cura pasiva", Mejoras.calcularCoste(Mejoras.tipoMejora.CuraPasiva, rareza), i, rareza); break;
                case Mejoras.tipoMejora.VelocidadMovimiento: crearBotonInterfaz(Mejoras.tipoMejora.VelocidadMovimiento, Mejoras.getSprite(Mejoras.tipoMejora.VelocidadMovimiento), "Velocidad de movimiento", Mejoras.calcularCoste(Mejoras.tipoMejora.VelocidadMovimiento, rareza), i, rareza); break;
                case Mejoras.tipoMejora.VelocidadProyectil: crearBotonInterfaz(Mejoras.tipoMejora.VelocidadProyectil, Mejoras.getSprite(Mejoras.tipoMejora.VelocidadProyectil), "Velocidad de proyectil", Mejoras.calcularCoste(Mejoras.tipoMejora.VelocidadProyectil, rareza), i, rareza); break;
                case Mejoras.tipoMejora.Damage: crearBotonInterfaz(Mejoras.tipoMejora.Damage, Mejoras.getSprite(Mejoras.tipoMejora.Damage), "Daño", Mejoras.calcularCoste(Mejoras.tipoMejora.Damage, rareza), i, rareza); break;
                case Mejoras.tipoMejora.Poder: crearBotonInterfaz(Mejoras.tipoMejora.Poder, Mejoras.getSprite(Mejoras.tipoMejora.Poder), "Poder", Mejoras.calcularCoste(Mejoras.tipoMejora.Poder, rareza), i, rareza); break;
                case Mejoras.tipoMejora.numProyectiles: crearBotonInterfaz(Mejoras.tipoMejora.numProyectiles, Mejoras.getSprite(Mejoras.tipoMejora.numProyectiles), "Mas proyectiles!", Mejoras.calcularCoste(Mejoras.tipoMejora.numProyectiles, rareza), i, rareza); break;
                case Mejoras.tipoMejora.velocidadAtaque: crearBotonInterfaz(Mejoras.tipoMejora.velocidadAtaque, Mejoras.getSprite(Mejoras.tipoMejora.velocidadAtaque), "Velocidad de ataque", Mejoras.calcularCoste(Mejoras.tipoMejora.velocidadAtaque, rareza), i, rareza); break;
                case Mejoras.tipoMejora.penetracion: crearBotonInterfaz(Mejoras.tipoMejora.penetracion, Mejoras.getSprite(Mejoras.tipoMejora.penetracion), "Penetración", Mejoras.calcularCoste(Mejoras.tipoMejora.penetracion, rareza), i, rareza); break;
            }
        }

        /* AL FINAL DEL BUCLE HAY QUE CREAR EL EL ITEM UI. POR EJEMPLO:
         * crearBotonInterfaz(Mejoras.getSprite(Mejoras.tipoMejora.Salud), "Salud máxima", Mejoras.calcularCoste(Mejoras.tipoMejora.Salud, 2), 0, 1);
         */
        shopItemTemplate.gameObject.SetActive(false);
    }

    public void desactivarMenu()
    {
        foreach(Transform t in listaItemsTienda)
        { 
            if(t != null)
                Destroy(t.gameObject);
        }
    }

    void tryBuyItem(Mejoras.tipoMejora tipoMejora, int rareza)
    { 
        customer.mejoraComprada(tipoMejora, rareza);
    }
}
