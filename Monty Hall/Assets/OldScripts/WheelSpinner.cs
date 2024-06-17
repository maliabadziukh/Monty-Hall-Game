using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using TMPro;

public class Demo : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private TextMeshProUGUI spinButtonText;
    [SerializeField] private PickerWheel pickerWheel;

    private void Start()
    {
        spinButton.onClick.AddListener(() =>
        {
            spinButton.interactable = false;
            spinButtonText.text = "Spinning...";
            pickerWheel.OnSpinStart(() =>
      {
          Debug.Log("Spin start...");
      });

            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                Debug.Log("Spin end :");
                Debug.Log("Index   : " + wheelPiece.Index);
                Debug.Log("Chance  : " + wheelPiece.Chance);
                Debug.Log("Label   : " + wheelPiece.Label);
                Debug.Log("Amount  : " + wheelPiece.Amount);
                spinButton.interactable = true;
                spinButtonText.text = "Spin";
            });
            pickerWheel.Spin();
        });






    }
}