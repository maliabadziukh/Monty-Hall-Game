using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler
{
    private Door door;
    public LevelManager levelManager;
    private void Awake()
    {
        door = GetComponent<Door>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    public void OnPointerClick(PointerEventData eventData)
    {
        levelManager.SelectDoor(door.doorIndex);
    }
}
