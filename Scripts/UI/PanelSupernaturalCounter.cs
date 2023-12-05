using TMPro;
using UnityEngine;

public class PanelSupernaturalCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void Reset()
    {
        UpdateText(0);
    }
    
    public void UpdateText(int counter) => text.text = counter.ToString();
}
