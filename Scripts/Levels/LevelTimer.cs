using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class LevelTimer
{
    [SerializeField] private float claimCooldown = 72;

    public event Action Finish;
    public bool IsTime { get; private set; }
    
    private DateTime? _lastClaimTime;

    public void StartTimer()
    {
        _lastClaimTime = DateTime.UtcNow;
        
        IsTime = true;
        
        Coroutines.StartRoutine(Timer());
    }

    public void StopTime()
    {
        IsTime = false;
    }
    
    private IEnumerator Timer()
    {
        while (IsTime)
        {
            var timeSpan = DateTime.UtcNow - _lastClaimTime.Value;
            
            if (_lastClaimTime.HasValue)
            {
                if (timeSpan.TotalHours > claimCooldown)
                {
                    _lastClaimTime = DateTime.UtcNow;
                    Finish?.Invoke();
                    IsTime = false;
                    yield break;
                }
            }
            
            var remainingCooldown = TimeSpan.FromHours(claimCooldown) - timeSpan;
            UIController._instance.SetTextTimerGame(remainingCooldown);

            yield return new WaitForSeconds(1);
        }
    }
}
