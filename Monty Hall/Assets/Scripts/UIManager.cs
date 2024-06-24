using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> menus;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameManager gameManagerPrefab;
    [SerializeField] private TMP_Text carsText;
    private LevelManager levelManager;


    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
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
            levelManager.CleanUpLevel();
            Destroy(gm.gameObject);
            print("deleted old game manager");
        }
        ShowMenu("");
    }

    public void UpdateHearts(int lives)
    {
        foreach (GameObject heart in hearts)
        {
            heart.GetComponent<Image>().color = Color.white;
        }
        for (int i = 3; i > lives; i--)
        {
            hearts[i - 1].GetComponent<Image>().color = new(0.5f, 0.1f, 0.1f, 1);
        }
    }
    public void UpdateCars(int cars)
    {
        carsText.text = cars.ToString();
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
