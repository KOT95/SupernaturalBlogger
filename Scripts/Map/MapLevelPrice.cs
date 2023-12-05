using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MapLevelPrice
{
    [SerializeField] private Material material;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private int likes;
    [SerializeField] private int subscribers;
    [SerializeField] private Image panelCurrency;
    [SerializeField] private TextMeshProUGUI textLikes;
    [SerializeField] private TextMeshProUGUI textSubscribers;
    
    private Material[] _startMaterials;
    private bool _isBuy;

    public void Init()
    {
        _startMaterials = meshRenderer.materials;
    }

    public bool CheckBuy()
    {
        if (likes > Currency._instance.Likes || subscribers > Currency._instance.Subscribers)
        {
            List<Material> listMat = new List<Material>();
            foreach (var num in _startMaterials) listMat.Add(material);
            meshRenderer.materials = listMat.ToArray();
            
            panelCurrency.gameObject.SetActive(true);
            textLikes.text = likes.ToString();
            textSubscribers.text = subscribers.ToString();
            
            _isBuy = false;
        }
        else
        {
            _isBuy = true;
        }

        return _isBuy;
    }
}
