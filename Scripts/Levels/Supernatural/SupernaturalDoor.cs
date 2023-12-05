using System.Collections;
using DG.Tweening;
using UnityEngine;

public sealed class SupernaturalDoor : Supernatural
{
    [SerializeField] private Transform[] doors;
    [SerializeField] private float openZ, closeZ;
    [SerializeField] private float timeMin, timeMax;

    private const TypeSupernatural _type = TypeSupernatural.Low;

    public override void Activate()
    {
        IsActivate = false;
        TypeSupernatural = _type;
        foreach (var door in doors)
        {
            Vector3 closeDir = new Vector3(door.eulerAngles.x, door.eulerAngles.y, closeZ);
            door.transform.eulerAngles = closeDir;
        }
        StartCoroutine(Anim());
    }

    private IEnumerator Anim()
    {
        while (!IsActivate)
        {
            float time = 0;
            
            if(!particleSystem.isPlaying)
                particleSystem.Play();

            foreach (var door in doors)
            {
                float RandomTimeOpen = Random.Range(timeMin, timeMax);
                float RandomTimeClose = Random.Range(timeMin, timeMax);

                time += RandomTimeOpen + RandomTimeClose;

                Vector3 openDir = new Vector3(door.eulerAngles.x, door.eulerAngles.y, openZ);
                Vector3 closeDir = new Vector3(door.eulerAngles.x, door.eulerAngles.y, closeZ);
                Transform doorT = door;
                doorT.DORotate(openDir, RandomTimeOpen).OnComplete(() =>
                {
                    doorT.DORotate(closeDir, RandomTimeClose);
                });
            }

            yield return new WaitForSeconds(time);
        }
        
        if(particleSystem.isPlaying)
            particleSystem.Stop();

        foreach (var door in doors)
        {
            Vector3 closeDir = new Vector3(door.eulerAngles.x, door.eulerAngles.y, closeZ);
            door.DORotate(closeDir, 1);
        }
    }

    public override void Reset()
    {
        foreach (var door in doors)
        {
            Vector3 closeDir = new Vector3(door.eulerAngles.x, door.eulerAngles.y, closeZ);
            door.transform.eulerAngles = closeDir;
        }
    }
}
