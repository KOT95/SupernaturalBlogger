using System;
using UnityEngine;

[Serializable]
public class Skins
{
    public GameObject[] ElementsSkin;
}

[Serializable]
public class PlayerSkin
{
    [SerializeField] private Skins[] skinsArray;

    public void SwitchSkin()
    {
        for (int i = 0; i < skinsArray.Length; i++)
        {
            if (i == PlayerPrefs.GetInt("Skin"))
            {
                foreach (var element in skinsArray[i].ElementsSkin)
                    element.gameObject.SetActive(true);
            }
            else
            {
                foreach (var element in skinsArray[i].ElementsSkin)
                    element.gameObject.SetActive(false);
            }
        }
    }
}
