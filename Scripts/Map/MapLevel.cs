using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapLevel : MonoBehaviour
{
    [SerializeField] private TypeLevels typeLevel;
    [SerializeField] private MapLevelPrice mapLevelPrice = new MapLevelPrice();
    [SerializeField] private MapLevelTimer mapLevelTimer = new MapLevelTimer();
    [SerializeField] private LikesMove likesMove;

    public event Action<TypeLevels> SelectionLevel = default;
    
    private int _amountViews;
    private bool _isBuy;

    private void Start()
    {
        mapLevelPrice.Init();
        CheckBuy();
        mapLevelTimer.Init(typeLevel, _isBuy);
    }

    public void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject() || mapLevelTimer.IsTimer)
            return;
        
        if (_isBuy)
        {
            print("Load location!");
            SelectionLevel?.Invoke(typeLevel);
        }
        else
        {
            print("no!");
        }
    }

    public TypeLevels Type() { return typeLevel; }

    public void CheckBuy() { _isBuy = mapLevelPrice.CheckBuy(); }

    public void ActivateTimer()
    {
        if(_amountViews == 0) return;

        Vector2 viewportPosition = Camera.main.WorldToScreenPoint(transform.position);
        likesMove.AnimViews(viewportPosition, _amountViews);
        
        mapLevelTimer.Activate();
    }

    public void SetBlock(Block block)
    {
        _amountViews = 0;

        for (int i = 0; i < block.Supernatural; i++) _amountViews += 10;
        for (int i = 0; i < block.SupernaturalMiddle; i++) _amountViews += 20;
        for (int i = 0; i < block.SupernaturalHigh; i++) _amountViews += 30;
    }
}
