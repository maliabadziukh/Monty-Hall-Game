using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public variables 
    public int lives = 3;
    public int cars = 0;
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
        uiManager.UpdateCars(cars);
        uiManager.UpdateHearts(lives);
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

    public void NextLevel()
    {
        if (currentLevel < levels.Length)
        {
            currentLevel++;
            print("Starting level " + currentLevel);
            uiManager.ShowTransition(currentLevel);
            levelManager.CleanUpLevel();
            StartCoroutine(WaitBeforeLevelSetup());

        }
        else
        {
            uiManager.ShowMenu("Win");
        }
    }



    public void LoseLife()
    {


        lives--;
        if (lives <= 0)
        {
            uiManager.ShowMenu("Death");
        }
        else
        {
            uiManager.UpdateHearts(lives);
        }
    }

    public void AddLife()
    {
        if (lives < 3) { lives++; }
        uiManager.UpdateHearts(lives);
    }

    public void AddCar()
    {
        cars++;
        uiManager.UpdateCars(cars);
    }

    private IEnumerator WaitBeforeLevelSetup()
    {
        yield return new WaitForSeconds(1);
        levelManager.SetUpLevel(levels[currentLevel - 1]);
    }

}
