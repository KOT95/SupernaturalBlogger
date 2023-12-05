using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines instance
    {
        get
        {
            if (m_instance == null)
            {
                var gameObj = new GameObject("[Coroutine manager]");
                m_instance = gameObj.AddComponent<Coroutines>();
                DontDestroyOnLoad(gameObj);
            }

            return m_instance;
        }
    }

    private static Coroutines m_instance;

    public static Coroutine StartRoutine(IEnumerator enumerator)
    {
        return instance.StartCoroutine(enumerator);
    }

    public static void StopRoutine(Coroutine routine)
    {
        instance.StopCoroutine(routine);
    }
}
