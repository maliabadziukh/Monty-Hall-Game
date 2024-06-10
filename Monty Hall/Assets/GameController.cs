using UnityEngine;

public class GameController : MonoBehaviour
{
    public Door[] doors;

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
    }

    public void SelectDoor(int doorIndex)
    {
        selectedDoorIndex = doorIndex;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SelectDoor(selectedDoorIndex);
        }
        print("Door index selected: " + selectedDoorIndex);
    }
}
