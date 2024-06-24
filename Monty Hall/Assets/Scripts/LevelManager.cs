using System;
using System.Collections;
using System.Collections.Generic;
using EasyUI.PickerWheelUI;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject doorsContainer;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private List<Door> doors;
    [SerializeField] private int selectedDoorIndex;
    [SerializeField] private int revealedDoorIndex;
    private GameManager gameManager;
    private UIManager uiManager;
    private LevelConfig levelConfig;
    private float halfScreenWidth;
    public Button confirmButton;
    public Button finalConfirmButton;
    [SerializeField] private GameObject pickerWheelPrefab;
    private PickerWheel pickerWheel;
    private bool isFinalChoice = false;


    void Start()
    {
        halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        uiManager = FindObjectOfType<UIManager>();
    }

    public void SetUpLevel(LevelConfig config)
    {
        gameManager = FindObjectOfType<GameManager>();
        print("Setting up level...");
        levelConfig = config;
        confirmButton.gameObject.SetActive(false);
        SetUpDoors(levelConfig.doorNumber);
        isFinalChoice = false;
    }


    private void SetUpDoors(int doorNumber)
    {

        float spacing = halfScreenWidth * 2 / (doorNumber + 1);
        int winningDoorIndex = UnityEngine.Random.Range(0, doorNumber);
        for (int i = 0; i < doorNumber; i++)
        {
            GameObject newDoor = Instantiate(doorPrefab, doorsContainer.transform);
            float xPos = -halfScreenWidth + spacing + (spacing * i);
            newDoor.transform.position = new Vector3(xPos, 0, 0);
            doors.Add(newDoor.GetComponent<Door>());
            doors[i].HideCar(i == winningDoorIndex);
        }
    }

    public void SelectDoor(int doorIndex)
    {
        int doorToSelect = doorIndex;

        for (int i = 0; i < doors.Count; i++)
        {
            if (!doors[i].isRevealed)
            {
                doors[i].SelectDoor(doorToSelect);
                selectedDoorIndex = doorToSelect;

            }
        }
        if (doors[selectedDoorIndex].isRevealed)
        {
            confirmButton.gameObject.SetActive(false);
            finalConfirmButton.gameObject.SetActive(false);

        }
        else
        {
            if (isFinalChoice)
            {
                finalConfirmButton.gameObject.SetActive(true);

            }
            else
            {
                confirmButton.gameObject.SetActive(true);
            }
        }

    }

    public void RevealNonWinningDoors(int doorsToReveal)
    {
        List<int> nonWinningDoors = new();
        for (int i = 0; i < doors.Count; i++)
        {
            if (i != selectedDoorIndex && !doors[i].hasCar)
            {
                nonWinningDoors.Add(i);
            }
        }
        System.Random random = new System.Random();
        for (int i = nonWinningDoors.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            (nonWinningDoors[j], nonWinningDoors[i]) = (nonWinningDoors[i], nonWinningDoors[j]);
        }
        for (int i = 0; i < doorsToReveal && i < nonWinningDoors.Count; i++)
        {
            doors[nonWinningDoors[i]].Reveal();
        }

        AllowToSwap();
    }

    public void RevealChosenDoor()
    {
        uiManager.ShowMenu("");
        doors[selectedDoorIndex].Reveal();
        if (doors[selectedDoorIndex].hasCar)
        {
            gameManager.AddCar();
        }
        else
        {
            gameManager.LoseLife();
        }
        StartCoroutine(WaitBeforeNextLevel());
    }

    public void ShowFirstWheel()
    {
        string[] wheelOptions = levelConfig.wheelOptions;
        if (pickerWheel != null)
        {
            Destroy(pickerWheel.gameObject);
        }
        if (wheelOptions.Length > 1)
        {
            Instantiate(pickerWheelPrefab);
            pickerWheel = GameObject.FindObjectOfType<PickerWheel>();
            WheelPiece[] wheelPieces = new WheelPiece[wheelOptions.Length];
            for (int i = 0; i < wheelOptions.Length; i++)
            {
                wheelPieces[i] = new WheelPiece() { Label = wheelOptions[i] };
            }
            pickerWheel.InitializeWheel(wheelPieces);
        }
        else
        {
            RevealChosenDoor();
        }

    }

    private void AllowToSwap()
    {
        isFinalChoice = true;
        confirmButton.gameObject.SetActive(false);
        uiManager.ShowMenu("Swap");

    }
    private void ShowSecondWheel()
    {

    }

    public void ProcessWheelResult(string result)
    {
        switch (result)
        {
            case "lose_life":
                gameManager.LoseLife();
                StartCoroutine(WaitBeforeReveal());
                break;
            case "get_life":
                gameManager.AddLife();
                StartCoroutine(WaitBeforeReveal());

                break;
            case "spin_again":
                StartCoroutine(WaitAndShowFirstWheel());
                break;
            case "reveal_1":
                RevealNonWinningDoors(1);
                break;
            case "reveal_2":
                RevealNonWinningDoors(2);
                break;
            case "reveal_3":
                RevealNonWinningDoors(3);
                break;
            case "reveal_4":
                RevealNonWinningDoors(4);
                break;
            case "reveal_5":
                RevealNonWinningDoors(5);
                break;
            case "reveal_6":
                RevealNonWinningDoors(6);

                break;
        }
    }

    public void CleanUpLevel()
    {
        confirmButton.gameObject.SetActive(false);
        List<Door> doorsToRemove = new List<Door>(doors);

        foreach (Door door in doorsToRemove)
        {
            doors.Remove(door);
            Destroy(door.gameObject);
        }
    }

    private IEnumerator WaitBeforeReveal()
    {
        yield return new WaitForSeconds(0.5f);
        RevealChosenDoor();
    }
    private IEnumerator WaitAndShowFirstWheel()
    {
        yield return new WaitForSeconds(0.5f);
        ShowFirstWheel();
    }

    private IEnumerator WaitBeforeNextLevel()
    {
        yield return new WaitForSeconds(1);
        gameManager.NextLevel();
    }
}