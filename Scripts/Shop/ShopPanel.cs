using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopPanel : MonoBehaviour
{
    [SerializeField] private string nameSave;
    [SerializeField] private ShopElement[] shopElements;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image icon;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        Currency._instance.UpdateCurrency += CheckElementsBuy;
        
        foreach (var shopElement in shopElements)
        {
            shopElement.NameSave = nameSave;
            shopElement.Select += NoSelect;
        }

        if (!PlayerPrefs.HasKey(nameSave)) PlayerPrefs.SetInt(nameSave, 0);
        foreach (var shopElement in shopElements)
        {
            if (shopElement.Num == PlayerPrefs.GetInt(nameSave)) shopElement.ActivatorSelectElement();
        }
    }

    private void CheckElementsBuy() { foreach (var shopElement in shopElements) shopElement.CheckBuy(); }

    private void NoSelect(ShopElement element)
    {
        foreach (var shopElement in shopElements)
        {
            if(shopElement != element)
                shopElement.ActivatorSelectElement(false);
            else
                icon.sprite = shopElement.Icon;
        }
    }
    
    public void Activator(bool activate = true)
    {
        if (activate)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            _button.interactable = false;

            foreach (var shopElement in shopElements)
            {
                if (shopElement.Num == PlayerPrefs.GetInt(nameSave)) icon.sprite = shopElement.Icon;
            }
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            _button.interactable = true;
        }
    }
}
