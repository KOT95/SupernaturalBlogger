using UnityEngine;

public class VideoCameraController : MonoBehaviour
{
    [SerializeField] private VideoCamera[] videoCameras;
    [SerializeField] private LevelSupernatural supernatural;

    private void OnEnable()
    {
        foreach (var videoCamera in videoCameras)
        {
            videoCamera.CheckNumber(PlayerPrefs.GetInt("Camera"), supernatural);
        }
    }
}
