using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool hasCar;
    public int doorIndex;
    [SerializeField] private Sprite doorOpenSprite;
    [SerializeField] private Sprite doorClosedSprite;
    private GameObject car;
    private GameObject goat;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        doorIndex = transform.GetSiblingIndex();
        car = transform.Find("car").gameObject;
        goat = transform.Find("goat").gameObject;
        ResetState();
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

    public void ResetState()
    {
        hasCar = false;
        goat.SetActive(false);
        car.SetActive(false);
        spriteRenderer.sprite = doorClosedSprite;
    }
}
