using UnityEngine;

/// <summary>
/// Classe base astratta per gli stati del player
/// Fornisce riferimenti comuni a tutti gli stati
/// </summary>
public abstract class PlayerStateBase : IState
{
    protected PlayerStateMachine _stateMachine;
    protected PlayerController _playerController;

    /// <summary>
    /// Costruttore base per gli stati
    /// </summary>
    /// <param name="stateMachine">Riferimento alla state machine</param>
    /// <param name="playerController">Riferimento al player controller</param>
    public PlayerStateBase(PlayerStateMachine stateMachine, PlayerController playerController)
    {
        _stateMachine = stateMachine;
        _playerController = playerController;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}
