using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class RandomSupernatural
{
    [SerializeField] private Supernatural[] supernaturals;
    [SerializeField] private float minTime, maxTime;

    private Supernatural[] _supernaturalTemp;
    private bool _isActivate;

    public void Activate()
    {
        _supernaturalTemp = supernaturals;
        RandomList();
        _isActivate = true;
        Coroutines.StartRoutine(Activator());
    }

    public void Deactivate() { _isActivate = false; }

    private void RandomList()
    {
        for (int i = 0; i < _supernaturalTemp.Length; i++)
        {
            int random = Random.Range(0, _supernaturalTemp.Length);
            (_supernaturalTemp[i], _supernaturalTemp[random]) = (_supernaturalTemp[random], _supernaturalTemp[i]);
        }
    }

    private bool CheckList()
    {
        int num = 0;

        foreach (var supernatural in _supernaturalTemp)
        {
            if (supernatural.IsActivate)
                num++;
        }

        if (num > 0)
            return false;

        return true;
    }

    private IEnumerator Activator()
    {
        while (_isActivate)
        {
            if (CheckList())
                yield return new WaitUntil(() => !CheckList());

            foreach (var supernatural in _supernaturalTemp)
            {
                if (supernatural.IsActivate)
                {
                    supernatural.Activate();
                    RandomList();
                    break;
                }
            }
            
            float time = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(time);
        }
    }

    public void Reset()
    {
        foreach (var supernatural in supernaturals) supernatural.Reset();
    }
}
