using UnityEngine;

/// <summary>
/// Stato Falling - Player sta cadendo
/// </summary>
public class FallingState : PlayerStateBase
{
    public FallingState(PlayerStateMachine stateMachine, PlayerController playerController) 
        : base(stateMachine, playerController) { }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if (_playerController.IsGrounded)
        {
            if (_playerController.IsMoving)
            {
                _stateMachine.ChangeState(_playerController.IsRunning ? 
                    _stateMachine.RunningState : _stateMachine.WalkingState);
            }
            else
            {
                _stateMachine.ChangeState(_stateMachine.IdleState);
            }
        }
    }

    public override void Exit()
    {
    }
}
