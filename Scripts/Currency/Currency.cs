using System;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public static Currency _instance;

    [SerializeField] private int amountLikesForSubscribers;
    [SerializeField] private int amountSubscribers;
    [Space] 
    [SerializeField] private int bonusLikes;
    [SerializeField] private int amountSubscribersForBonus;

    public event Action UpdateCurrency = default;

    public int Likes
    {
        get
        {
            int data = PlayerPrefs.GetInt("Likes", 0);
            
            if(data != 0)
                return data;

            return 0;
        }
        set
        {
            if (value != 0)
            {
                PlayerPrefs.SetInt("Likes", value);
                UIController._instance.SetLikes(value);
            }
            else
                PlayerPrefs.DeleteKey("Likes");
        }
    }
    
    public int Subscribers
    { 
        get
        {
            int data = PlayerPrefs.GetInt("Subscribers", 0);
            
            if(data != 0)
                return data;

            return 0;
        }
        set
        {
            if (value != 0)
            {
                PlayerPrefs.SetInt("Subscribers", value);
                UIController._instance.SetSubscribers(value);
            }
            else
                PlayerPrefs.DeleteKey("Subscribers");
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        UIController._instance.SetLikes(Likes);
        UIController._instance.SetSubscribers(Subscribers);
    }

    public void AddCurrency(int amount)
    {
        int numBonus = Subscribers / amountSubscribersForBonus;
        amount += numBonus * bonusLikes;
        
        Likes += amount;

        int num = amount / amountLikesForSubscribers;
        Subscribers += num * amountSubscribers;
        
        UpdateCurrency?.Invoke();
    }
}
