using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Door[] doors;
    public Button confirmButton;
    private int selectedDoorIndex;
    private int revealedDoorIndex;

    private void Start()
    {
        SetUpNewRound();
    }

    void SetUpNewRound()
    {
        int winningDoorIndex = Random.Range(0, doors.Length);
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].HideCar(i == winningDoorIndex);
        }

        confirmButton.interactable = false;
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
        PromptToSwitch();
    }

    private void RevealAnotherDoor()
    {
        print("revealing a door...");
        for (int i = 0; i < doors.Length; i++)
        {
            if (i != selectedDoorIndex && !doors[i].hasCar)
            {
                doors[i].Reveal();
            }
        }

    }
    private void PromptToSwitch()
    {
        print("do you wanna switch???");
    }
}
