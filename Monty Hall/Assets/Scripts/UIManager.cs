using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> menus;

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
    public void ShowTransition(int levelIndex)
    {
        //show the level transition
    }

}
