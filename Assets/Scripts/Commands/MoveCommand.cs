using UnityEngine;

/// <summary>
/// Comando per il movimento del player
/// Incapsula la logica di movimento relativa alla camera
/// </summary>
public class MoveCommand : ICommand
{
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    private readonly Camera _camera;
    private readonly float _speed;
    private readonly float _horizontal;
    private readonly float _vertical;
    
    private Vector3 _previousPosition;

    /// <summary>
    /// Costruttore del comando di movimento
    /// </summary>
    /// <param name="rigidbody">Rigidbody del player</param>
    /// <param name="transform">Transform del player</param>
    /// <param name="camera">Camera principale per movimento relativo</param>
    /// <param name="speed">Velocità di movimento</param>
    /// <param name="horizontal">Input orizzontale</param>
    /// <param name="vertical">Input verticale</param>
    public MoveCommand(Rigidbody rigidbody, Transform transform, Camera camera, float speed, float horizontal, float vertical)
    {
        _rigidbody = rigidbody;
        _transform = transform;
        _camera = camera;
        _speed = speed;
        _horizontal = horizontal;
        _vertical = vertical;
    }

    public void Execute()
    {
        _previousPosition = _rigidbody.position;
        
        Vector3 direction = _camera.transform.forward * _vertical + _camera.transform.right * _horizontal;
        direction.y = 0f;
        direction.Normalize();

        _rigidbody.MovePosition(_rigidbody.position + direction * _speed * Time.fixedDeltaTime);

        if (direction != Vector3.zero)
        {
            _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        }
    }

    public void Undo()
    {
        _rigidbody.MovePosition(_previousPosition);
    }
}
