using UnityEngine;

public class LookAtUI : MonoBehaviour
{
    private void Update()
    {
        Camera camera = Camera.main;
        transform.LookAt(camera.transform);
    }
}
