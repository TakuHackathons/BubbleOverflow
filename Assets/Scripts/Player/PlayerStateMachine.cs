// PlayerStateMachine.cs
using UnityEngine;

public class PlayerStateMachine
{
    private readonly PlayerController player;
    private readonly PlayerStateAnimation stateAnimation;
    private PlayerStateType currentStateType;

    public enum PlayerStateType
    {
        Idle,
        Walk,
        Pickup,
        Hold,
        PutDown,
        Throw
    }

    public PlayerStateMachine(PlayerController playerController)
    {
        player = playerController;
        stateAnimation = new PlayerStateAnimation(player.GetComponent<Animator>());
        currentStateType = PlayerStateType.Idle;
        stateAnimation.PlayIdle();
    }

    public void ChangeState(PlayerStateType newState)
    {
        if (stateAnimation.IsInAnimation &&
            !(currentStateType == PlayerStateType.Walk && newState == PlayerStateType.Idle) &&
            !(currentStateType == PlayerStateType.Idle && newState == PlayerStateType.Walk))
        {
            return;
        }

        ExitCurrentState();
        currentStateType = newState;
        EnterNewState();
    }

    private void ExitCurrentState()
    {
        stateAnimation.ResetAllTriggers();

        switch (currentStateType)
        {
            case PlayerStateType.Hold:
            case PlayerStateType.PutDown:
            case PlayerStateType.Throw:
                player.ReleaseHeldObject();
                break;
        }
    }

    private void EnterNewState()
    {
        switch (currentStateType)
        {
            case PlayerStateType.Idle:
                stateAnimation.PlayIdle();
                break;
            case PlayerStateType.Walk:
                stateAnimation.PlayWalk();
                break;
            case PlayerStateType.Pickup:
                stateAnimation.PlayPickup();
                break;
            case PlayerStateType.Hold:
                stateAnimation.PlayHold();
                break;
            case PlayerStateType.PutDown:
                stateAnimation.PlayPutDown();
                break;
            case PlayerStateType.Throw:
                stateAnimation.PlayThrow();
                var heldObject = player.GetHeldObject();
                if (heldObject != null)
                {
                    heldObject.OnThrow(player.GetMoveDirection());
                }
                break;
        }
    }

    public void FixedUpdate()
    {
        if (stateAnimation.IsInAnimation) return;

        float speed = player.GetMoveDirection().magnitude;
        stateAnimation.UpdateSpeed(speed);

        if (currentStateType == PlayerStateType.Walk ||
            currentStateType == PlayerStateType.Hold)
        {
            player.UpdateMovement();
        }

        if (speed > 0.1f && currentStateType == PlayerStateType.Idle)
        {
            ChangeState(PlayerStateType.Walk);
        }
        else if (speed < 0.1f && currentStateType == PlayerStateType.Walk)
        {
            ChangeState(PlayerStateType.Idle);
        }
    }

    public PlayerStateType GetCurrentState()
    {
        return currentStateType;
    }

    public void OnAnimationComplete()
    {
        stateAnimation.OnAnimationComplete();
        switch (currentStateType)
        {
            case PlayerStateType.Pickup:
                ChangeState(PlayerStateType.Hold);
                break;
            case PlayerStateType.PutDown:
            case PlayerStateType.Throw:
                ChangeState(PlayerStateType.Idle);
                break;
        }
    }
}