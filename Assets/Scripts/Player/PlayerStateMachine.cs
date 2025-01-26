using UnityEngine;

public class PlayerStateMachine
{
    private readonly PlayerController player;
    private readonly Animator animator;
    private PlayerStateType currentStateType;
    private bool isInAnimation = false;

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
        // アニメーション中は特定の状態遷移のみ許可
        if (isInAnimation &&
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
        // 全てのアニメーショントリガーをリセット
        animator.ResetTrigger("Pickup");
        animator.ResetTrigger("Hold");
        animator.ResetTrigger("PutDown");
        animator.ResetTrigger("Throw");

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
                animator.SetFloat("Speed", 0f);
                isInAnimation = false;
                break;
            case PlayerStateType.Walk:
                animator.SetFloat("Speed", 1f);
                isInAnimation = false;
                break;
            case PlayerStateType.Pickup:
                animator.SetTrigger("Pickup");
                isInAnimation = true;
                break;
            case PlayerStateType.Hold:
                animator.SetTrigger("Hold");
                isInAnimation = false;
                break;
            case PlayerStateType.PutDown:
                animator.SetTrigger("PutDown");
                isInAnimation = true;
                break;
            case PlayerStateType.Throw:
                animator.SetTrigger("Throw");
                isInAnimation = true;
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
    }

    public void FixedUpdate()
    {
        // アニメーション中は移動状態の更新をスキップ
        if (isInAnimation) return;

        float speed = player.GetMoveDirection().magnitude;
        animator.SetFloat("Speed", speed);

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
        isInAnimation = false;
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