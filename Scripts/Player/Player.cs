using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform targetCamera;
    [SerializeField] private PlayerMove playerMove = new PlayerMove();
    [SerializeField] private PlayerSkin playerSkin = new PlayerSkin();

    private void OnEnable()
    {
        playerMove.Init(transform);
        playerSkin.SwitchSkin();
    }

    private void Update()
    {
        playerMove.Move();
    }

    public Transform GetTargetCamera()
    {
        if(targetCamera != null)
            return targetCamera;

        return transform;
    }
}
