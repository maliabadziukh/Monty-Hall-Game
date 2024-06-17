using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using TMPro;
using System;

public class Demo : MonoBehaviour
{
    [SerializeField] private Button spinButton;
    [SerializeField] private TextMeshProUGUI spinButtonText;
    [SerializeField] private PickerWheel pickerWheel;
    private LevelManager levelManager;
    private GameObject wheelObj;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        wheelObj = GameObject.Find("SpinningWheel(Clone)");
        spinButton.onClick.AddListener(() =>
        {
            spinButton.interactable = false;
            spinButtonText.text = "Spinning...";
            pickerWheel.OnSpinStart(() => { });
            pickerWheel.OnSpinEnd(wheelPiece =>
            {
                print(wheelPiece.Label);
                levelManager.ProcessWheelResult(wheelPiece.Label);
                Destroy(wheelObj);
            });
            pickerWheel.Spin();
        });
    }
}