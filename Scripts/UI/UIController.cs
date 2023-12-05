using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController _instance;
    
    [SerializeField] private Joystick joystick;
    [SerializeField] private PanelSupernaturalCounter panelSupernaturalCounter;
    [SerializeField] private PanelCurrency panelCurrency;
    [SerializeField] private PanelTimer panelTimer;
    [SerializeField] private Image game;
    [SerializeField] private Image map;

    private void Awake()
    {
        _instance = this;
    }

    public void ActivateGameElements()
    {
        JoyStickInteractable(false);
        game.gameObject.SetActive(true);
        map.gameObject.SetActive(false);
        JoyStickInteractable(true);
        panelSupernaturalCounter.Reset();
    }
    
    public void ActivateMapElements()
    {
        JoyStickInteractable(false);
        game.gameObject.SetActive(false);
        map.gameObject.SetActive(true);
    }

    public void DeactivateElements()
    {
        JoyStickInteractable(false);
        game.gameObject.SetActive(false);
        map.gameObject.SetActive(false);
    }

    private void JoyStickInteractable(bool state)
    {
        if (state)
        {
            joystick.enabled = state;
            joystick.GetComponent<Image>().raycastTarget = state;
        }
        else
        {
            joystick.ResetValues();
            joystick.enabled = state;
            joystick.GetComponent<Image>().raycastTarget = state;
        }
    }

    public void UpdateTextSupernaturalCounter(int counter)
    {
        panelSupernaturalCounter.UpdateText(counter);
    }

    public void SetTextTimerGame(TimeSpan remainingCooldown)
    {
        panelTimer.UpdateText(remainingCooldown);
    }

    public void SetLikes(int num) => panelCurrency.SetTextViews(num);
    public void SetSubscribers(int num) => panelCurrency.SetTextSubscribers(num);
}
