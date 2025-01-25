using UnityEngine;
using System;

public class PlayerStateMachine
{
    private readonly PlayerController player;
    private readonly Animator animator;
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
        animator = player.GetComponent<Animator>();
        currentStateType = PlayerStateType.Idle;
    }

    public void ChangeState(PlayerStateType newState)
    {
        ExitCurrentState();
        currentStateType = newState;
        EnterNewState();
    }

    private void ExitCurrentState()
    {
        switch (currentStateType)
        {
            case PlayerStateType.Hold:
                player.ReleaseHeldObject();
                break;
        }
    }

    private void EnterNewState()
    {
        switch (currentStateType)
        {
            case PlayerStateType.Idle:
                animator.SetTrigger("Idle");
                break;
            case PlayerStateType.Walk:
                animator.SetTrigger("Walk");
                break;
            case PlayerStateType.Pickup:
                animator.SetTrigger("Pickup");
                break;
            case PlayerStateType.Hold:
                animator.SetTrigger("Hold");
                break;
            case PlayerStateType.PutDown:
                animator.SetTrigger("PutDown");
                break;
            case PlayerStateType.Throw:
                animator.SetTrigger("Throw");
                var heldObject = player.GetHeldObject();
                if (heldObject != null)
                {
                    heldObject.OnThrow(player.GetMoveDirection());
                }
                break;
        }
    }

    public void Update()
    {
        switch (currentStateType)
        {
            case PlayerStateType.Idle:
                if (player.GetMoveDirection().magnitude > 0.1f)
                {
                    ChangeState(PlayerStateType.Walk);
                }
                break;

            case PlayerStateType.Walk:
                if (player.GetMoveDirection().magnitude < 0.1f)
                {
                    ChangeState(PlayerStateType.Idle);
                }
                break;
        }
    }

    public void FixedUpdate()
    {
        if (currentStateType == PlayerStateType.Walk ||
            currentStateType == PlayerStateType.Hold)
        {
            player.UpdateMovement();
        }
    }

    public PlayerStateType GetCurrentState()
    {
        return currentStateType;
    }

    public void OnAnimationComplete()
    {
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