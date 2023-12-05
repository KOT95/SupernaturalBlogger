using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MapLevelTimer
{
    [SerializeField] private float claimCooldown = 72;
    [SerializeField] private Canvas canvasTimer;
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private Image slider;
    [SerializeField] private Image panelSlider;
    
    public bool IsTimer { get; private set; }
    
    private DateTime? _lastClaimTime
    {
        get
        {
            string data = PlayerPrefs.GetString($"lastTime{_typeLevel}", null);
            
            if(!string.IsNullOrEmpty(data))
                return DateTime.Parse(data);

            return null;
        }
        set
        {
            if(value != null)
                PlayerPrefs.SetString($"lastTime{_typeLevel}", value.ToString());
            else
                PlayerPrefs.DeleteKey($"lastTime{_typeLevel}");
        }
    }
    
    private TypeLevels _typeLevel;
    private bool _isBuy;

    public void Init(TypeLevels typeLevel, bool isBuy)
    {
        _typeLevel = typeLevel;
        _isBuy = isBuy;
        panelSlider.gameObject.SetActive(false);
        
        if (_lastClaimTime.HasValue && _isBuy)
            Activate();
    }

    public void Activate()
    {
        IsTimer = true;
        
        if (!_lastClaimTime.HasValue)
            _lastClaimTime = DateTime.UtcNow;
        
        canvasTimer.gameObject.SetActive(true);
        panelSlider.gameObject.SetActive(true);
        Coroutines.StartRoutine(MissionsStateUpdater());
    }

    private IEnumerator MissionsStateUpdater()
    {
        while (_lastClaimTime.HasValue)
        {
            UpdateState();
            yield return new WaitForSeconds(1);
        }
    }
    
    private void UpdateState()
    {
        var timeSpan = DateTime.UtcNow - _lastClaimTime.Value;
        
        UpdateUI();

        if (timeSpan.TotalMinutes > claimCooldown)
        {
            IsTimer = false;
            canvasTimer.gameObject.SetActive(false);
            panelSlider.gameObject.SetActive(false);
            _lastClaimTime = null;
        }
    }
    
    private void UpdateUI()
    {
        var timeSpan = DateTime.UtcNow - _lastClaimTime.Value;
        var elapsedCooldown = timeSpan.TotalMinutes;
        var remainingCooldown = TimeSpan.FromMinutes(claimCooldown) - timeSpan;

        string cd =
            $"{remainingCooldown.Minutes:D1}:{remainingCooldown.Seconds:D2}";

        textTimer.text = cd;
        slider.fillAmount = 1 - (float)(elapsedCooldown / claimCooldown);
    }
}
