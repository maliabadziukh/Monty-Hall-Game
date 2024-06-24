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

    public void StartGame()
    {
        //reset all variables to starting values
        currentLevel = 0;
        lives = 3;
        print("game manager starting a new game");
        //start level
        NextLevel();
    }

    void NextLevel()
    {
        if (currentLevel < levels.Length)
        {
            currentLevel++;
            print("Starting level " + currentLevel);
            uiManager.ShowTransition(currentLevel);
            levelManager.SetUpLevel(levels[currentLevel - 1]);
        }
        else
        {
            WinGame();
        }
    }

    void WinGame()
    {
        //game end logic
    }
    void Die()
    {

    }
    public void LoseLife()
    {
        lives--;
        uiManager.UpdateHearts(lives);
        if (lives <= 0)
        {
            Die();
        }
    }
    public void AddLife()
    {
        if (lives < 3) { lives++; }
        uiManager.UpdateHearts(lives);
    }

}
