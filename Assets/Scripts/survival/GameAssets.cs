using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if(_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    [Header("Mejoras")]
    public Sprite Salud;
    public Sprite CuraPasiva;
    public Sprite VelocidadMovimiento;
    public Sprite VelocidadProyectil;
    public Sprite Damage;
    public Sprite Poder;
    public Sprite numProyectiles;
    public Sprite velocidadAtaque;
    public Sprite penetracion;

    [Header("Sprites interfaz")]
    public Sprite dinero;

    [Header("Decoraciones")]
    public Sprite TeddyBear;
    public Sprite StarSticker;
    public Sprite RacingWolfSticker;
    public Sprite GhostSticker;
    public Sprite OldConsole;
    public Sprite MageFigurine;
    public Sprite FoxyHeroFigurine;
    public Sprite ElPerroFigurine;
    public Sprite KatyCat;
    public Sprite BunnyStressToy;


}
