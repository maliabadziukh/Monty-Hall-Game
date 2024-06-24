using System.Collections;
using System.Collections.Generic;
using EasyUI.PickerWheelUI;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject pickerWheelPrefab;
    private PickerWheel pickerWheel;

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
    }


    private void SetUpDoors(int doorNumber)
    {

        float spacing = halfScreenWidth * 2 / (doorNumber + 1);
        int winningDoorIndex = Random.Range(0, doorNumber);
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
        selectedDoorIndex = doorIndex;
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].SelectDoor(selectedDoorIndex);
        }
        confirmButton.gameObject.SetActive(true);
    }

    public void RevealNonWinningDoors(int doorNumber)
    {
        print("Revealing " + doorNumber + " doors...");
    }

    private void RevealChosenDoor()
    {
        print("revealing chosen door...");
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
    private void ShowSecondWheel()
    {

    }

    public void ProcessWheelResult(string result)
    {
        switch (result)
        {
            case "lose_life":
                gameManager.LoseLife();
                break;
            case "get_life":
                gameManager.AddLife();
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
        confirmButton.interactable = false;
        foreach (Door door in doors)
        {
            doors.Remove(door);
            Destroy(door.gameObject);
        }
    }
    private IEnumerator WaitAndShowFirstWheel()
    {
        yield return new WaitForSeconds(0.5f);
        ShowFirstWheel();
    }
}