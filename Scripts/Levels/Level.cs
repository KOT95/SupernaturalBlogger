using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TypeLevels typeLevel;
    [SerializeField] private LevelTimer levelTimer = new LevelTimer();
    [SerializeField] private LevelSupernatural levelSupernatural;

    public event Action<Block> Win = default;

    private CameraController cameraController;
    private Vector3 _pos;
    private Vector3 _rot;

    private void Awake()
    {
        _pos = player.transform.position;
        _rot = player.transform.eulerAngles;
    }

    private void OnEnable()
    {
        player.transform.position = _pos;
        player.transform.eulerAngles = _rot;

        cameraController = Camera.main.GetComponent<CameraController>();

        levelSupernatural.ResetSupernatural();
        
        cameraController.SetTarget(player.GetTargetCamera());
    }

    public void Activate()
    {
        levelSupernatural.RandomActivate();
        levelTimer.Finish += OnFinish;
        levelTimer.StartTimer();
    }

    public void Deactivate()
    {
        levelSupernatural.RandomDeactivate();
        levelTimer.Finish -= OnFinish;
        levelTimer.StopTime();
    }

    public TypeLevels Type()
    {
        return typeLevel;
    }

    private void OnFinish()
    {
        levelTimer.Finish -= OnFinish;
        cameraController.SetTarget(null);
        Win?.Invoke(levelSupernatural.Block);
    }
}
