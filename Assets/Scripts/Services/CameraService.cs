using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] 
    private Transform _cameraTransform;
    [SerializeField]
    private Transform _target;

    private Vector3 _camraOffset;


    private void Awake()
    {
        _camraOffset = _cameraTransform.position - _target.position;
    }

    private void LateUpdate()
    {
        _cameraTransform.position = _target.position + _camraOffset;
    }
}
