using UnityEngine;

/// <summary>
/// State Machine per gestire gli stati del player
/// Coordina le transizioni tra stati e aggiorna lo stato corrente
/// </summary>
public class PlayerStateMachine
{
    private IState _currentState;

    public IdleState IdleState { get; private set; }
    public WalkingState WalkingState { get; private set; }
    public RunningState RunningState { get; private set; }
    public JumpingState JumpingState { get; private set; }
    public FallingState FallingState { get; private set; }

    /// <summary>
    /// Ottiene lo stato corrente
    /// </summary>
    public IState CurrentState => _currentState;

    /// <summary>
    /// Inizializza la state machine con tutti gli stati
    /// </summary>
    /// <param name="playerController">Riferimento al player controller</param>
    public void Initialize(PlayerController playerController)
    {
        IdleState = new IdleState(this, playerController);
        WalkingState = new WalkingState(this, playerController);
        RunningState = new RunningState(this, playerController);
        JumpingState = new JumpingState(this, playerController);
        FallingState = new FallingState(this, playerController);

        _currentState = IdleState;
        _currentState.Enter();
    }

    /// <summary>
    /// Cambia lo stato corrente
    /// </summary>
    /// <param name="newState">Nuovo stato da attivare</param>
    public void ChangeState(IState newState)
    {
        if (_currentState == newState)
            return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    /// <summary>
    /// Aggiorna lo stato corrente (chiamato in Update)
    /// </summary>
    public void Update()
    {
        _currentState?.Update();
    }

    /// <summary>
    /// Aggiorna lo stato corrente (chiamato in FixedUpdate)
    /// </summary>
    public void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
}
