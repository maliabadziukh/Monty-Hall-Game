using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject doorsContainer;
    [SerializeField] private GameObject doorPrefab;
    private float halfScreenWidth;

    void Start()
    {
        //initialize variables
        halfScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        print("Screen width: " + halfScreenWidth);
        InstantiateDoors(8);
    }

    public void SetUpLevel(LevelConfig levelConfig)
    {

    }

    private void InstantiateDoors(int doorNumber)
    {
        float spacing = (halfScreenWidth * 2) / (doorNumber + 1);
        print("spacing: " + spacing);
        for (int i = 0; i < doorNumber; i++)
        {
            GameObject newDoor = Instantiate(doorPrefab, doorsContainer.transform);
            float xPos = -(halfScreenWidth) + spacing + (spacing * i);
            print("xPos: " + xPos);
            newDoor.transform.position = new Vector3(xPos, 0, 0);
        }
    }
}
