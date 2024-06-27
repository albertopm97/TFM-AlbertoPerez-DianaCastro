using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeController : MonoBehaviour
{
    public GameObject keySprite;
    private int GameToLoad;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Survival")
        {
            keySprite.SetActive(true);
            GameToLoad = 1;
            print("Arcade a cargar" +  GameToLoad);
        }

        if (collision.gameObject.tag == "Racing")
        {
            keySprite.SetActive(true);
            GameToLoad = 2;
        }

        if (collision.gameObject.tag == "Minigames")
        {
            keySprite.SetActive(true);
            GameToLoad = 3;
        }

        if (collision.gameObject.tag == "Arcade")
        {
            keySprite.SetActive(true);
            GameToLoad = 4;
            print("Arcade a cargar" + GameToLoad);
        }

        if (collision.gameObject.tag == "Ground")
        {
            keySprite.SetActive(false);
            GameToLoad = 0;
        }
    }

    public void loadGame()
    {

        ArcadeGameManager.Instance.stopMusic();

        switch (GameToLoad)
        {
            case 0:
                print("No arcade near Mia");
                break;
            case 1:
                SceneManager.LoadScene("Survival");
                break;
            case 2:
                SceneManager.LoadScene("Race");
                break;
            case 3:
                int randomGame = Random.Range(0, 3);
                switch (randomGame)
                {
                    case 0:

                        SceneManager.LoadScene("Jump");
                        break;

                    case 1:

                        SceneManager.LoadScene("FPS");
                        break;

                    case 2:

                        SceneManager.LoadScene("VineSwing");
                        break;
                }
                break;

                case 4:
                    SceneManager.LoadScene("Arcade");
                    break;

            default:
                break;
        }
    }
}
