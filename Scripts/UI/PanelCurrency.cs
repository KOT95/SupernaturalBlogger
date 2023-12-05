using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelCurrency : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textViews;
    [SerializeField] private TextMeshProUGUI textSubscribers;

    public void SetTextViews(int num)
    {
        textViews.text = num.ToString();
    }
    
    public void SetTextSubscribers(int num)
    {
        textSubscribers.text = num.ToString();
    }
}
