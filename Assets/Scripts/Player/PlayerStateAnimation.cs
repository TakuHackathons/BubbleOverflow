using UnityEngine;

public class PlayerStateAnimation
{
    private readonly Animator animator;
    public bool IsInAnimation { get; private set; }

    public PlayerStateAnimation(Animator animator)
    {
        this.animator = animator;
        IsInAnimation = false;
    }

    public void ResetAllTriggers()
    {
        animator.ResetTrigger("Pickup");
        animator.ResetTrigger("Hold");
        animator.ResetTrigger("PutDown");
        animator.ResetTrigger("Throw");
    }

    public void PlayIdle()
    {
        animator.SetFloat("Speed", 0f);
        IsInAnimation = false;
    }

    public void PlayWalk()
    {
        animator.SetFloat("Speed", 1f);
        IsInAnimation = false;
    }

    public void PlayPickup()
    {
        animator.SetTrigger("Pickup");
        IsInAnimation = true;
    }

    public void PlayHold()
    {
        animator.SetTrigger("Hold");
        IsInAnimation = false;
    }

    public void PlayPutDown()
    {
        animator.SetTrigger("PutDown");
        IsInAnimation = true;
    }

    public void PlayThrow()
    {
        animator.SetTrigger("Throw");
        IsInAnimation = true;
    }

    public void UpdateSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void OnAnimationComplete()
    {
        IsInAnimation = false;
    }
}