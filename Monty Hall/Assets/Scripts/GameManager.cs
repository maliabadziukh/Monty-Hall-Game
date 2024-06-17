using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public variables 
    public int lives = 3;
    public int currentLevel = 0;
    public LevelConfig[] levels;

    //private variables
    private LevelManager levelManager;
    private UIManager uiManager;


    void Start()
    {
        //init variables
        levelManager = FindObjectOfType<LevelManager>();
        uiManager = FindObjectOfType<UIManager>();



        StartGame();
    }

    void StartGame()
    {
        //reset all variables to starting values
        currentLevel = 0;
        lives = 3;

        //start level
        NextLevel();
    }

    void NextLevel()
    {
        if (currentLevel < levels.Length)
        {
            currentLevel++;
            uiManager.ShowTransition(currentLevel);
            levelManager.SetUpLevel(levels[currentLevel - 1]);
        }
        else
        {
            EndGame();
        }
    }

    void EndGame()
    {
        //game end logic
    }


}
