using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;

    public Level CurrentLevel { get; private set; }
    public event Action<Block> HandoverBlock = default;

    public void ActivateLevel(TypeLevels type)
    {
        if(CurrentLevel != null)
            DeactivateLevels();

        foreach (var level in levels) { if (type == level.Type()) CurrentLevel = level; }

        CurrentLevel.gameObject.SetActive(true);
        CurrentLevel.Win += HandoverBlockToMap;
    }

    public void DeactivateLevels()
    {
        foreach (var level in levels) level.gameObject.SetActive(false);
        CurrentLevel = null;
    }

    private void HandoverBlockToMap(Block block)
    {
        CurrentLevel.Win -= HandoverBlockToMap;
        HandoverBlock?.Invoke(block);
    }
}
