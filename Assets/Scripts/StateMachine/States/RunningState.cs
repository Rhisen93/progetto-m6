using UnityEngine;

/// <summary>
/// Stato Running - Player si muove correndo
/// </summary>
public class RunningState : PlayerStateBase
{
    public RunningState(PlayerStateMachine stateMachine, PlayerController playerController) 
        : base(stateMachine, playerController) { }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if (_playerController.InputHandler.JumpButtonDown && _playerController.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.JumpingState);
        }
        else if (!_playerController.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.FallingState);
        }
        else if (!_playerController.IsMoving)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        else if (!_playerController.IsRunning)
        {
            _stateMachine.ChangeState(_stateMachine.WalkingState);
        }
    }

    public override void Exit()
    {
    }
}
