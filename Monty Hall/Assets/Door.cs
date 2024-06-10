using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Sprite doorOpenSprite;
    [SerializeField] private Sprite doorClosedSprite;
    public bool hasCar;
    private GameObject car;
    private GameObject goat;
    public int doorIndex;
    private bool isOpen = false;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = doorClosedSprite;
        doorIndex = transform.GetSiblingIndex();
        car = transform.Find("car").gameObject;
        goat = transform.Find("goat").gameObject;
        car.SetActive(false);
        goat.SetActive(false);
    }

    public void HideCar(bool hasCar)
    {
        this.hasCar = hasCar;
    }

    public void SelectDoor(int index)
    {
        if (doorIndex == index)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }


    public void Reveal()
    {
        spriteRenderer.sprite = doorOpenSprite;
        if (hasCar)
        {
            car.SetActive(true);
        }
        else
        {
            goat.SetActive(true);
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
