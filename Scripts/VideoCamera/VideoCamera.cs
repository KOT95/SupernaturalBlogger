using UnityEngine;

public class VideoCamera : MonoBehaviour
{
    [SerializeField] private VideoCameraRaycast videoCameraRaycast;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private int number;

    private LevelSupernatural _levelSupernatural;

    private void Activate(LevelSupernatural levelSupernatural)
    {
        gameObject.SetActive(true);
        _levelSupernatural = levelSupernatural;
        cameraObj.gameObject.SetActive(true);
    }

    private void Deactivate()
    {
        cameraObj.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void CheckNumber(int num, LevelSupernatural supernatural)
    {
        if(num == number)
            Activate(supernatural);
        else
            Deactivate();
    }

    private void Update()
    {
        if (videoCameraRaycast.RayToScan())
        {
            _levelSupernatural.AddCount(1, videoCameraRaycast.Supernatural.TypeSupernatural);
            videoCameraRaycast.Supernatural.Deactivate();
        }
    }
}
