using UnityEngine;

/// <summary>
/// Stato Walking - Player si muove camminando
/// </summary>
public class WalkingState : PlayerStateBase
{
    public WalkingState(PlayerStateMachine stateMachine, PlayerController playerController) 
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
        else if (_playerController.IsRunning)
        {
            _stateMachine.ChangeState(_stateMachine.RunningState);
        }
    }

    public override void Exit()
    {
    }
}
