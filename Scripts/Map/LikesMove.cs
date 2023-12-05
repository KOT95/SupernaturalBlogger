using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class LikesMove : MonoBehaviour
{
    [SerializeField] private Transform prefabLikes;
    [SerializeField] private int amount;
    [SerializeField] private float radius;
    [SerializeField] private Transform pointMove;

    private List<Transform> _likes = new List<Transform>();

    public void AnimViews(Vector3 pos, int likesAmount)
    {
        for (int i = 0; i < amount; i++)
        {
            Transform view = Instantiate(prefabLikes, pos, Quaternion.identity);
            view.SetParent(transform);
            _likes.Add(view);

            Vector3 posRandom = pos + Random.insideUnitSphere * radius;
            posRandom.z = 0;

            view.DOMove(posRandom, 1f).OnComplete(() =>
            {
                view.DOMove(pointMove.position, 0.5f).OnComplete(() =>
                {
                    Destroy(view.gameObject);
                    _likes.Remove(view);
                    
                    if(_likes.Count == 0)
                        Currency._instance.AddCurrency(likesAmount);
                });
            }).SetEase(Ease.OutCubic);
        }
    }
}
