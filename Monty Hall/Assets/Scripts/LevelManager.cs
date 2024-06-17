using System.Collections;
using System.Collections.Generic;
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
    private float halfScreenWidth;

    //ui
    public Button confirmButton;

    void Start()
    {
        //initialize variables
        halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    public void SetUpLevel(LevelConfig levelConfig)
    {
        print("Setting up level...");
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
        confirmButton.interactable = true;
    }

    private void ShowFirstWheel()
    {

    }

    private void ShowSecondWheel()
    {

    }
}
