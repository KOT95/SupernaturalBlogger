using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private MapLevel[] mapLevels;
    [SerializeField] private LevelManager levelManager;
    
    private CameraController _cameraController;
    private TypeLevels _selectionTypeLevel;

    private void Awake()
    {
        foreach (var map in mapLevels)
        {
            map.SelectionLevel += ActivateLevel;
        }

        levelManager.HandoverBlock += SetBlockLevel;
    }

    public void ActivateMap()
    {
        _cameraController = Camera.main.GetComponent<CameraController>();
        _cameraController.SetTarget(null);
    }

    public void ActivateLevel(TypeLevels typeLevel)
    {
        _selectionTypeLevel = typeLevel;
        levelManager.ActivateLevel(_selectionTypeLevel);
    }

    public void CheckBuyMaps()
    {
        foreach (var mapLevel in mapLevels) mapLevel.CheckBuy();
    }

    public void ActivateTimer()
    {
        foreach (var mapLevel in mapLevels)
        {
            if (mapLevel.Type() == _selectionTypeLevel) { mapLevel.ActivateTimer(); }
        }
    }

    public void SetBlockLevel(Block block)
    {
        foreach (var mapLevel in mapLevels)
        {
            if (mapLevel.Type() == _selectionTypeLevel) { mapLevel.SetBlock(block); }
        }
        
        CheckBuyMaps();
    }
}
