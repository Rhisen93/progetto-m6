using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Controlla il movimento e il salto del player
/// Utilizza Command Pattern per input e State Machine per gestione stati
/// Movimento basato su input direzionale relativo alla camera
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _walkSpeed = 3f;
    [SerializeField] private float _runSpeed = 6f;
    [SerializeField] private float _jumpForce = 1.5f;
    
    [Header("References")]
    [SerializeField] private GroundChecker _groundChecker;
    
    [Header("Events")]
    [SerializeField] private UnityEvent _onJump;

    private Rigidbody _rb;
    private Camera _mainCamera;
    private InputHandler _inputHandler;
    private PlayerStateMachine _stateMachine;

    public Rigidbody Rigidbody => _rb;
    public InputHandler InputHandler => _inputHandler;
    public bool IsGrounded => _groundChecker != null && _groundChecker.IsGrounded;
    public bool IsMoving => Mathf.Abs(_inputHandler.Horizontal) > 0.01f || Mathf.Abs(_inputHandler.Vertical) > 0.01f;
    public bool IsRunning => Input.GetKey(KeyCode.LeftShift);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        
        _inputHandler = GetComponent<InputHandler>();
        if (_inputHandler == null)
        {
            _inputHandler = gameObject.AddComponent<InputHandler>();
        }
        
        _stateMachine = new PlayerStateMachine();
    }

    private void Start()
    {
        if (_groundChecker == null)
        {
            _groundChecker = GetComponentInChildren<GroundChecker>();
        }

        _stateMachine.Initialize(this);
    }

    private void Update()
    {
        _stateMachine.Update();
        
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
        
        HandleMovementInput();
    }

    /// <summary>
    /// Gestisce l'input di movimento tramite Command Pattern
    /// </summary>
    private void HandleMovementInput()
    {
        float horizontal = _inputHandler.Horizontal;
        float vertical = _inputHandler.Vertical;

        float currentSpeed = IsRunning ? _runSpeed : _walkSpeed;

        MoveCommand moveCommand = new MoveCommand(_rb, transform, _mainCamera, currentSpeed, horizontal, vertical);
        _inputHandler.SetMoveCommand(moveCommand);
        _inputHandler.ExecuteMoveCommand();
    }

    /// <summary>
    /// Gestisce l'input di salto tramite Command Pattern
    /// </summary>
    private void HandleJumpInput()
    {
        if (_inputHandler.JumpButtonDown && IsGrounded)
        {
            if (ParticleEffectManager.Instance != null)
            {
                ParticleEffectManager.Instance.PlayJumpEffect(transform.position);
            }
            
            JumpCommand jumpCommand = new JumpCommand(_rb, _jumpForce, _groundChecker, _onJump);
            _inputHandler.SetJumpCommand(jumpCommand);
            _inputHandler.ExecuteJumpCommand();
        }
    }

    /// <summary>
    /// Permette di ottenere lo stato corrente (per debug)
    /// </summary>
    public IState GetCurrentState()
    {
        return _stateMachine?.CurrentState;
    }

    /// <summary>
    /// Disabilita il controller (usato da FallDetector)
    /// </summary>
    public void DisableController()
    {
        enabled = false;
    }
}
