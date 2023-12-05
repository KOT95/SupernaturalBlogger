using System;
using TMPro;
using UnityEngine;

public class PanelTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void UpdateText(TimeSpan remainingCooldown)
    {
        string cd =
            $"{remainingCooldown.Minutes:D2}:{remainingCooldown.Seconds:D2}";
        text.text = cd;
    }
}