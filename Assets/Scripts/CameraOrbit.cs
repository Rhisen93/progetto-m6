using UnityEngine;

/// <summary>
/// Controlla una camera che orbita attorno a un target seguendo il movimento del mouse
/// </summary>
public class CameraOrbit : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -5);
    [SerializeField] private int _cameraAngle = 2;
    
    [Header("Mouse Settings")]
    [SerializeField] private float _mouseSensitivity = 3f;
    [SerializeField] private float _bottomClamp = -30f;
    [SerializeField] private float _topClamp = 60f;

    private float _yaw;
    private float _pitch;

    private void LateUpdate()
    {
        if (_target == null) return;
        
        _yaw += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _pitch -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
        _pitch = Mathf.Clamp(_pitch, _bottomClamp, _topClamp);

        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
        Vector3 desiredPosition = _target.position + rotation * _offset;
        
        transform.position = desiredPosition;
        transform.LookAt(_target.position + Vector3.up * _cameraAngle);
    }
}
