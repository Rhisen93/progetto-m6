using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Verifica se il personaggio è a contatto con il terreno usando un Raycast
/// Invia eventi quando lo stato "grounded" cambia
/// </summary>
public class GroundChecker : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private float _groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayer;
    
    [Header("Events")]
    [SerializeField] private UnityEvent<bool> _onIsGroundedChanged;

    public bool IsGrounded { get; private set; }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * _groundCheckDistance);
    }

    private void Update()
    {
        bool wasGrounded = IsGrounded;

        IsGrounded = Physics.Raycast(
            transform.position + Vector3.up * 0.05f, 
            -Vector3.up, 
            _groundCheckDistance, 
            _groundLayer
        );

        if (wasGrounded != IsGrounded)
        {
            _onIsGroundedChanged?.Invoke(IsGrounded);
        }
    }
}