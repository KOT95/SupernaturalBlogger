using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private ShopPanel[] shopPanels;

    private void Start()
    {
        for (int i = 0; i < shopPanels.Length; i++)
        {
            if(i == 0)
                shopPanels[i].Activator();
            else
                shopPanels[i].Activator(false);
        }
    }

    public void ClickActivator(bool isActivator)
    {
        if (isActivator)
        {
            panel.alpha = 1;
            panel.interactable = true;
            panel.blocksRaycasts = true;
        }
        else
        {
            panel.alpha = 0;
            panel.interactable = false;
            panel.blocksRaycasts = false;
        }
    }

    public void ClickSwitchPanel(ShopPanel shopPanel)
    {
        foreach (var shop in shopPanels)
        {
            if(shop == shopPanel) 
                shop.Activator();
            else
                shop.Activator(false);
        }
    }
}
