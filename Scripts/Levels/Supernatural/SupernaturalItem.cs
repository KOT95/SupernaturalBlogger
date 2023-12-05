using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class SupernaturalItem : Supernatural
{
    [SerializeField] private Vector3 minRotate, maxRotate;

    private const TypeSupernatural _type = TypeSupernatural.Low;
    private Vector3 _pos, _rot;

    private void Awake()
    {
        _pos = transform.position;
        _rot = transform.eulerAngles;
    }

    public override void Activate()
    {
        IsActivate = false;
        TypeSupernatural = _type;
        StartCoroutine(Anim());
    }
    
    private IEnumerator Anim()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        float RotateX = Random.Range(minRotate.x, maxRotate.x);
        float RotateY = Random.Range(minRotate.y, maxRotate.y);
        float RotateZ = Random.Range(minRotate.z, maxRotate.z);
        
        while (!IsActivate)
        {
            if(!particleSystem.isPlaying)
                particleSystem.Play();
            
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            transform.position = newPos;
            particleSystem.transform.position = newPos;
            transform.Rotate(RotateX, RotateY, RotateZ);

            yield return null;
        }

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        
        if(particleSystem.isPlaying)
            particleSystem.Stop();
    }

    public override void Reset()
    {
        if (_pos != Vector3.zero)
        {
            transform.position = _pos;
            transform.eulerAngles = _rot;
        }
    }
}
