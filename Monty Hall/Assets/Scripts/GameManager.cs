using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private LevelManager levelManager;

    void Start()
    {
        //init variables
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();


        //start game
    }

    void StartGame()
    {
        //reset all variables to starting values

        //start level
        NextLevel();
    }

    void NextLevel()
    {
        //ui manager. show level transition
        levelManager.SetUpLevel();
    }


}
