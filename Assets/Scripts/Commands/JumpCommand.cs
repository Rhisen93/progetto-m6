using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Comando per il salto del player
/// Incapsula la logica di salto con controllo ground
/// </summary>
public class JumpCommand : ICommand
{
    private readonly Rigidbody _rigidbody;
    private readonly float _jumpForce;
    private readonly GroundChecker _groundChecker;
    private readonly UnityEvent _onJump;

    /// <summary>
    /// Costruttore del comando di salto
    /// </summary>
    /// <param name="rigidbody">Rigidbody del player</param>
    /// <param name="jumpForce">Forza del salto</param>
    /// <param name="groundChecker">Ground checker per verificare se a terra</param>
    /// <param name="onJump">Evento da invocare quando si salta</param>
    public JumpCommand(Rigidbody rigidbody, float jumpForce, GroundChecker groundChecker, UnityEvent onJump)
    {
        _rigidbody = rigidbody;
        _jumpForce = jumpForce;
        _groundChecker = groundChecker;
        _onJump = onJump;
    }

    public void Execute()
    {
        if (_groundChecker != null && _groundChecker.IsGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _onJump?.Invoke();
        }
    }

    public void Undo()
    {
    }
}
