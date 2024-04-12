using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejoras : MonoBehaviour
{ 
    public enum tipoMejora
    {
        Salud,
        CuraPasiva,
        VelocidadMovimiento,
        VelocidadProyectil,
        Damage,
        Poder,
        numProyectiles,
        velocidadAtaque,
        penetracion
    }

    /**
     * Funcion que recibe un tipo de mejora y una rareza y calcula el precio correspondiente en la tienda
     */
    public static int calcularCoste(tipoMejora tipo, int nivelMejora)
    {
        switch (tipo)
        {
            default:
            case tipoMejora.Salud:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;

            case tipoMejora.CuraPasiva:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;

            case tipoMejora.VelocidadMovimiento:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;

            case tipoMejora.VelocidadProyectil:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;

            case tipoMejora.Damage:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;

            case tipoMejora.Poder:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;

            case tipoMejora.numProyectiles:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;

            case tipoMejora.velocidadAtaque:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;

            case tipoMejora.penetracion:

                switch (nivelMejora)
                {
                    default:
                    case 0: return 10; break;
                    case 1: return 20; break;
                    case 2: return 30; break;
                    case 3: return 40; break;
                }
                break;
        }
    }

    /**
     * Metodo para obtener el icono correspondiente a la mejora desde los GameAssets (carpeta resoruces)
     */
    public static Sprite getSprite(tipoMejora tipo)
    {
        switch (tipo)
        {
            default:
                case tipoMejora.Salud: return GameAssets.i.Salud;
                case tipoMejora.CuraPasiva: return GameAssets.i.CuraPasiva;
                case tipoMejora.VelocidadMovimiento: return GameAssets.i.VelocidadMovimiento;
                case tipoMejora.VelocidadProyectil: return GameAssets.i.VelocidadProyectil;
                case tipoMejora.Damage: return GameAssets.i.Damage;
                case tipoMejora.Poder: return GameAssets.i.Poder;
                case tipoMejora.numProyectiles: return GameAssets.i.numProyectiles;
                case tipoMejora.velocidadAtaque: return GameAssets.i.velocidadAtaque;
                case tipoMejora.penetracion: return GameAssets.i.penetracion;
        }
    }
}
