using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 holdObjectOffset = new Vector3(0f, 1f, 0f);

    private PlayerStateMachine stateMachine;
    private PlayerInput playerInput;
    private Rigidbody rb;
    private Animator animator;
    private Vector3 moveDirection;
    private IInteractable heldObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        stateMachine = new PlayerStateMachine(this);

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                        RigidbodyConstraints.FreezeRotationZ |
                        RigidbodyConstraints.FreezePositionY;
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveDirection = new Vector3(input.x, 0f, input.y);
    }

    public void OnInteract(InputValue value)
    {
        if (value.isPressed)
        {
            var currentState = stateMachine.GetCurrentState();
            if (currentState == PlayerStateMachine.PlayerStateType.Hold)
            {
                if (moveDirection.magnitude > 0.1f)
                {
                    stateMachine.ChangeState(PlayerStateMachine.PlayerStateType.Throw);
                }
                else
                {
                    stateMachine.ChangeState(PlayerStateMachine.PlayerStateType.PutDown);
                }
            }
            else if (currentState == PlayerStateMachine.PlayerStateType.Idle ||
                     currentState == PlayerStateMachine.PlayerStateType.Walk)
            {
                stateMachine.ChangeState(PlayerStateMachine.PlayerStateType.Pickup);
            }
            Debug.Log(currentState);
        }
    }

    public void UpdateMovement()
    {
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        if (moveDirection.x != 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = moveDirection.x < 0 ? 1 : -1;
            transform.localScale = newScale;
        }
    }

    public void SetHeldObject(IInteractable interactable)
    {
        heldObject = interactable;
        if (heldObject != null)
        {
            Transform objectTransform = ((MonoBehaviour)heldObject).transform;
            objectTransform.SetParent(transform);
            objectTransform.localPosition = holdObjectOffset;
        }
    }

    public void ReleaseHeldObject()
    {
        if (heldObject != null)
        {
            Transform objectTransform = ((MonoBehaviour)heldObject).transform;
            objectTransform.SetParent(null);
            heldObject = null;
        }
    }

    public IInteractable GetHeldObject()
    {
        return heldObject;
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    public void OnAnimationComplete()
    {
        stateMachine.OnAnimationComplete();
    }
}