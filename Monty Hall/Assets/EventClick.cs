using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler
{
    private Door door;
    public GameController gameController;
    private void Awake()
    {
        door = GetComponent<Door>();
    }

    // Update is called once per frame
    public void OnPointerClick(PointerEventData eventData)
    {
        gameController.SelectDoor(door.doorIndex);
    }
}
