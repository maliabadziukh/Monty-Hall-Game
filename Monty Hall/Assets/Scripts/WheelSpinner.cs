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
            pickerWheel.OnSpinStart(() => { });
            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                spinButton.interactable = true;
                spinButtonText.text = "Spin";
            });
            pickerWheel.Spin();
        });
    }
}