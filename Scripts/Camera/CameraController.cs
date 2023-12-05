using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mapTarget;
    [SerializeField] private float smoothSpeed = 0.1f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Map map;
    [Header("Colors")] 
    [SerializeField] private Color colorMap;
    [SerializeField] private Color colorLevel;

    private Transform _target;
    private Camera _camera;
    private bool _isMovePlayer;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.backgroundColor = colorMap;
        UIController._instance.ActivateMapElements();
    }

    private void AnimCamera()
    {
        if (_target != null)
        {
            Vector3 targetPositionWithOffset = _target.position + offset;
            Quaternion targetQuaternion = Quaternion.LookRotation(-targetPositionWithOffset + _target.position);
            Vector3 targetRotation = targetQuaternion.eulerAngles;

            Vector3 rot = Vector3.Lerp(transform.eulerAngles, targetRotation, 0.6f);
            transform.DORotate(new Vector3(rot.x, transform.eulerAngles.y, transform.eulerAngles.z), 1).SetEase(Ease.InBack).OnComplete(() =>
            {
                transform.DORotate(targetRotation, 2).SetEase(Ease.OutElastic);
            });
            Vector3 pos = Vector3.Lerp(transform.position, targetPositionWithOffset, 0.95f);
            transform.DOMove(pos, 1).SetEase(Ease.InBack).OnComplete(() =>
            {
                transform.DOMove(targetPositionWithOffset, 2).SetEase(Ease.OutElastic).OnComplete(() =>
                {
                    _isMovePlayer = true;
                    UIController._instance.ActivateGameElements();
                    levelManager.CurrentLevel.Activate();
                });
            });

            _camera.DOColor(colorLevel, 0.7f).SetDelay(0.7f);
        }
        else
        {
            levelManager.CurrentLevel.Deactivate();
            Vector3 targetRotation = mapTarget.eulerAngles;

            Vector3 rot = Vector3.Lerp(transform.eulerAngles, targetRotation, 0.6f);
            transform.DORotate(new Vector3(rot.x, transform.eulerAngles.y, transform.eulerAngles.z), 1).SetEase(Ease.InBack).OnComplete(() =>
            {
                transform.DORotate(targetRotation, 2).SetEase(Ease.OutElastic);
            });
            Vector3 pos = Vector3.Lerp(transform.position, mapTarget.position, 0.95f);
            transform.DOMove(pos, 1).SetEase(Ease.InBack).OnComplete(() =>
            {
                transform.DOMove(mapTarget.position, 2).SetEase(Ease.OutElastic).OnComplete(() =>
                {
                    UIController._instance.ActivateMapElements();
                    levelManager.DeactivateLevels();
                    map.ActivateTimer();
                });
            });

            _camera.DOColor(colorMap, 0.7f).SetDelay(0.7f);
        }
    }

    private void FixedUpdate()
    {
        if (_isMovePlayer)
        {
            Quaternion OriginalRot = transform.rotation;
            transform.LookAt(_target);
            Quaternion NewRot = transform.rotation;
            transform.rotation = OriginalRot;
            transform.rotation = Quaternion.Lerp(transform.rotation, NewRot, 1.5f * Time.deltaTime);

            Vector3 targetPositionWithOffset = _target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPositionWithOffset, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        transform.DOKill();
        _isMovePlayer = false;
        UIController._instance.DeactivateElements();
        AnimCamera();
    }
}
