using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> menus;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameManager gameManagerPrefab;

    private void Start()
    {
        ShowMenu("Start");
    }
    public void ShowMenu(string menuName)
    {
        foreach (GameObject menu in menus)
        {
            if (menuName == menu.name)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
        }
    }

    public void CloseMenu(string menuName)
    {
        foreach (GameObject menu in menus)
        {
            if (menuName == menu.name)
            {
                menu.SetActive(false);
            }
        }
    }
    public void StartNewGame()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            Destroy(gm.gameObject);
            print("deleted old game manager");
        }
        Instantiate(gameManagerPrefab);
        print("created new game manager");
        ShowMenu("");
    }

    public void UpdateHearts(int lives)
    {
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<Image>().color = new(0.5f, 0.1f, 0.1f, 1);
        }
        for (int i = 3; i > lives; i--)
        {
            hearts[i - 1].GetComponent<Image>().color = Color.white;
        }
    }
    public void ShowTransition(int levelIndex)
    {
        //show the level transition
    }

    public void QuitApplication()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
