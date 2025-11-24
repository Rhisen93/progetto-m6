using UnityEngine;

/// <summary>
/// Stato Jumping - Player sta saltando
/// </summary>
public class JumpingState : PlayerStateBase
{
    public JumpingState(PlayerStateMachine stateMachine, PlayerController playerController) 
        : base(stateMachine, playerController) { }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if (_playerController.Rigidbody.velocity.y < 0)
        {
            _stateMachine.ChangeState(_stateMachine.FallingState);
        }
    }

    public override void Exit()
    {
    }
}
