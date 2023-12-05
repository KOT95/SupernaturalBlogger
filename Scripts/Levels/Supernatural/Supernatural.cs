using UnityEngine;

public abstract class Supernatural : MonoBehaviour
{
    [SerializeField] protected ParticleSystem particleSystem;

    public bool IsActivate { get; protected set; } = true;
    public TypeSupernatural TypeSupernatural { get; protected set; }

    public abstract void Activate();
    
    public void Deactivate() { IsActivate = true; }

    public abstract void Reset();
}
