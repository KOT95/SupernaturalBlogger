using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private int num;
    [SerializeField] private int likes;
    [SerializeField] private int subscribers;
    [Space] 
    [SerializeField] private Sprite sprite;
    [SerializeField] private Image icon;
    [SerializeField] private Image iconBuy;
    [SerializeField] private TextMeshProUGUI textLikes;
    [SerializeField] private TextMeshProUGUI textSubscribers;
    [SerializeField] private Image selectedPanel;
    [SerializeField] private Image closePanel;

    public event Action<ShopElement> Select = default;
    public string NameSave { get; set; }
    public int Num { get { return num; } }
    public Sprite Icon { get { return sprite; } }

    private bool _isBuy;

    private void Start()
    {
        textLikes.text = likes.ToString();
        textSubscribers.text = subscribers.ToString();
        icon.sprite = sprite;
        iconBuy.sprite = sprite;

        CheckBuy();
    }
    
    public void CheckBuy()
    {
        if (Currency._instance.Likes >= likes && Currency._instance.Subscribers >= subscribers)
        {
            _isBuy = true;
            closePanel.gameObject.SetActive(false);
            iconBuy.gameObject.SetActive(true);
            icon.gameObject.SetActive(false);
        }
        else
        {
            closePanel.gameObject.SetActive(true);
            iconBuy.gameObject.SetActive(false);
            icon.gameObject.SetActive(true);
        }
    }

    public void ClickSelect()
    {
        if (_isBuy)
        {
            PlayerPrefs.SetInt(NameSave, num);
            ActivatorSelectElement();
            Select?.Invoke(this);
        }
    }

    public void ActivatorSelectElement(bool activate = true) { selectedPanel.gameObject.SetActive(activate); }
}
