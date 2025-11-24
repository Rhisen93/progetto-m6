using UnityEngine;

/// <summary>
/// Gestisce i parametri dell'Animator del personaggio in base all'input e allo stato
/// Compatibile con InputHandler ma mantiene fallback su Input diretto
/// </summary>
public class CharacterAnimationParamsHandler : MonoBehaviour
{
    [Header("Animation Parameters")]
    [SerializeField] private string _paramNameForward = "forward";
    [SerializeField] private string _paramNameVerticalSpeed = "vSpeed";
    [SerializeField] private string _paramNameIsGrounded = "isGrounded";
    [SerializeField] private string _paramNameJump = "jump";
    [SerializeField] private float _runMultiplier = 2f;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private InputHandler _inputHandler;
    
    /// <summary>
    /// Chiamato quando il player salta (via UnityEvent)
    /// </summary>
    public void OnJump()
    {
        if (_animator != null)
        {
            _animator.SetTrigger(_paramNameJump);
        }
    }

    /// <summary>
    /// Chiamato quando cambia lo stato grounded (via UnityEvent)
    /// </summary>
    /// <param name="isGrounded">True se il player è a terra</param>
    public void OnIsGroundedChanged(bool isGrounded)
    {
        if (_animator != null)
        {
            _animator.SetBool(_paramNameIsGrounded, isGrounded);
        }
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _inputHandler = GetComponent<InputHandler>();
    }
    
    private void Update()
    {
        if (_animator == null || _rigidbody == null) return;
        
        float vertical = _inputHandler != null ? _inputHandler.Vertical : Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && vertical > 0)
        {
            _animator.SetFloat(_paramNameForward, vertical * _runMultiplier);
        }
        else
        {
            _animator.SetFloat(_paramNameForward, vertical);
        }

        _animator.SetFloat(_paramNameVerticalSpeed, _rigidbody.velocity.y);
    }
}
