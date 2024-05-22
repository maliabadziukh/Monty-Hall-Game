using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int health = 100;
    public int score = 0;
    public TMP_Text healthText;
    public TMP_Text scoreText;
    public Button door1;
    public Button door2;
    public Button door3;
    public Image door1Result;
    public Image door2Result;
    public Image door3Result;
    public Sprite arrowSprite;
    public Sprite healthBoostSprite;
    public Sprite damageSprite;

    private int correctDoor;
    private bool doorChosen;
    private int chosenDoor;

    void Start()
    {
        UpdateUI();
        StartNewRound();
    }

    void StartNewRound()
    {
        correctDoor = Random.Range(1, 4);
        doorChosen = false;
        chosenDoor = -1;

        ResetDoors();
        print("correct door: " + correctDoor);
    }
    void ResetDoors()
    {
        door1.interactable = true;
        door2.interactable = true;
        door3.interactable = true;

        door1Result.gameObject.SetActive(false);
        door2Result.gameObject.SetActive(false);
        door3Result.gameObject.SetActive(false);
    }


    public void ChooseDoor(int doorNumber)
    {
        if (doorChosen) return;

        doorChosen = true;
        chosenDoor = doorNumber;
        StartCoroutine(RevealDoors());
    }

    IEnumerator RevealDoors()
    {
        int revealedDoor = GetNonWinningDoor(chosenDoor, correctDoor);
        GetButton(revealedDoor).interactable = false;

        // Reveal the contents of the chosen door
        RevealDoor(chosenDoor);
        yield return new WaitForSeconds(2); // Wait for 2 seconds

        // Automatically decide whether to switch or stay
        if (Random.value > 0.5f) // Simulate random choice
        {
            chosenDoor = GetOtherDoor(chosenDoor, revealedDoor);
        }

        // Reveal the contents of the chosen door after switching
        RevealDoor(chosenDoor);
        yield return new WaitForSeconds(2); // Wait for 2 seconds

        ProcessChoice();
    }
    void RevealDoor(int doorNumber)
    {
        Image doorResult = GetDoorResult(doorNumber);
        doorResult.gameObject.SetActive(true);

        if (doorNumber == correctDoor)
        {
            doorResult.sprite = arrowSprite;
        }
        else
        {
            int outcome = Random.Range(0, 2);
            if (outcome == 0)
            {
                doorResult.sprite = damageSprite;
            }
            else
            {
                doorResult.sprite = healthBoostSprite;
            }
        }
    }

    void ProcessChoice()
    {
        if (chosenDoor == correctDoor)
        {
            IncreaseScore();
        }
        else
        {
            Image doorResult = GetDoorResult(chosenDoor);
            if (doorResult.sprite == damageSprite)
            {
                UpdateHealth(-10); // Damage
            }
            else if (doorResult.sprite == healthBoostSprite)
            {
                UpdateHealth(10); // Health boost
            }
        }

        StartNewRound();
    }


    int GetNonWinningDoor(int chosen, int correct)
    {
        int reveal = Random.Range(1, 4);
        while (reveal == chosen || reveal == correct)
        {
            reveal = Random.Range(1, 4);
        }
        return reveal;
    }

    int GetOtherDoor(int chosen, int revealed)
    {
        for (int i = 1; i <= 3; i++)
        {
            if (i != chosen && i != revealed)
            {
                return i;
            }
        }
        return -1;
    }

    Button GetButton(int doorNumber)
    {
        switch (doorNumber)
        {
            case 1: return door1;
            case 2: return door2;
            case 3: return door3;
            default: return null;
        }
    }
    Image GetDoorResult(int doorNumber)
    {
        switch (doorNumber)
        {
            case 1: return door1Result;
            case 2: return door2Result;
            case 3: return door3Result;
            default: return null;
        }
    }
    public void UpdateHealth(int amount)
    {
        health += amount;
        UpdateUI();
    }

    public void IncreaseScore()
    {
        score += 1;
        UpdateUI();
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + health;
        scoreText.text = "Score: " + score;
    }
}