using UnityEngine;

/// <summary>
/// Stato Idle - Player fermo a terra
/// </summary>
public class IdleState : PlayerStateBase
{
    public IdleState(PlayerStateMachine stateMachine, PlayerController playerController) 
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
        else if (_playerController.IsMoving)
        {
            _stateMachine.ChangeState(_playerController.IsRunning ? 
                _stateMachine.RunningState : _stateMachine.WalkingState);
        }
    }

    public override void Exit()
    {
    }
}
