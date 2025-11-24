using UnityEngine;

/// <summary>
/// Piattaforma mobile che si muove avanti e indietro lungo l'asse X
/// </summary>
public class MovePlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _moveDistance = 5f;

    private Vector3 _startPosition;
    private int _direction = 1;
    
    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * _direction * Time.deltaTime);

        float distanceFromStart = transform.position.x - _startPosition.x;

        if (Mathf.Abs(distanceFromStart) >= _moveDistance)
        {
            _direction *= -1;
        }
    }
}
