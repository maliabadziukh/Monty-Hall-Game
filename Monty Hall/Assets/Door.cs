using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite doorOpenSprite;
    [SerializeField] private Sprite doorClosedSprite;
    [SerializeField] private bool hasCar;
    public int doorIndex;
    private bool isSelected = false;
    private bool isOpen = false;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorIndex = transform.GetSiblingIndex();
    }

    public void HideCar(bool hasCar)
    {
        this.hasCar = hasCar;
    }

    public void SelectDoor(int index)
    {
        if (doorIndex == index)
        {
            isSelected = true;
            spriteRenderer.color = Color.green;
        }
        else
        {
            isSelected = false;
            spriteRenderer.color = Color.white;
        }
    }
    public void ToggleOpen()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            spriteRenderer.sprite = doorOpenSprite;
        }
        else
        {
            spriteRenderer.sprite = doorClosedSprite;
        }
    }


}
