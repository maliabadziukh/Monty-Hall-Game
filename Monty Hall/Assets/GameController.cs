using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private float delayBeforeReveal;
    [SerializeField] private float delayBetweenRounds;
    public Door[] doors;
    public Button confirmButton;
    public GameObject switchPrompt;
    public int score = 0;
    public int lives = 3;
    private int selectedDoorIndex;
    private int revealedDoorIndex;

    private void Start()
    {
        SetUpNewRound();
    }

    void SetUpNewRound()
    {
        print("starting new round");
        int winningDoorIndex = Random.Range(0, doors.Length);
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].ResetState();
            doors[i].HideCar(i == winningDoorIndex);
        }
        confirmButton.gameObject.SetActive(true);
        confirmButton.interactable = false;
        switchPrompt.SetActive(false);
    }

    public void SelectDoor(int doorIndex)
    {
        selectedDoorIndex = doorIndex;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SelectDoor(selectedDoorIndex);
        }
        confirmButton.interactable = true;
    }

    public void ConfirmDoorSelection()
    {
        print("selected door was:" + selectedDoorIndex);
        RevealAnotherDoor();
        switchPrompt.SetActive(true);
    }

    private void RevealAnotherDoor()
    {
        print("revealing a door...");
        for (int i = 0; i < doors.Length; i++)
        {
            if (i != selectedDoorIndex && !doors[i].hasCar)
            {
                doors[i].Reveal();
                revealedDoorIndex = i;
                break;
            }
        }

        confirmButton.gameObject.SetActive(false);
    }

    public void SwitchDoors()
    {
        print("switching doors...");
        selectedDoorIndex = 3 - selectedDoorIndex - revealedDoorIndex;
        SelectDoor(selectedDoorIndex);
        StartCoroutine(DelayBeforeReveal());
    }

    public void FinalReveal()
    {

        doors[selectedDoorIndex].Reveal();
        if (doors[selectedDoorIndex].hasCar)
        {
            score++;
        }
        else
        {
            lives--;
        }

        print("Score: " + score);
        print("Lives: " + lives);

        if (lives > 0)
        {
            StartCoroutine(DelayBeforeNextRound());
        }

    }

    IEnumerator DelayBeforeReveal()
    {
        yield return new WaitForSeconds(delayBeforeReveal);
        FinalReveal();
    }

    IEnumerator DelayBeforeNextRound()
    {
        yield return new WaitForSeconds(delayBetweenRounds);
        SetUpNewRound();
    }


}
