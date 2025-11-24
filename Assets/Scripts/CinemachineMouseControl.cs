using UnityEngine;
using Cinemachine;

/// <summary>
/// Controlla la rotazione orbitale della Virtual Camera con il mouse
/// La Virtual Camera deve essere child di un GameObject vuoto che segue il player
/// </summary>
public class CinemachineMouseControl : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _target; // Il player

    [Header("Mouse Settings")]
    [SerializeField] private float _mouseSensitivity = 3f;
    [SerializeField] private float _minPitch = -30f;
    [SerializeField] private float _maxPitch = 60f;

    [Header("Offset")]
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, 0); // Altezza camera dal player

    private float _yaw;
    private float _pitch;

    private void LateUpdate()
    {
        if (_target == null) return;

        _yaw += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _pitch -= Input.GetAxis("Mouse Y") * _mouseSensitivity;
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        Quaternion rotation = Quaternion.Euler(_pitch, _yaw, 0);
        
        transform.position = _target.position + _offset;
        transform.rotation = rotation;
    }
}
