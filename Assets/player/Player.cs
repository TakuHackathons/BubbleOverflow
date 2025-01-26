using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Pickup,
        Hold,
        PutDown,
        Throw,
        Damage
    }

    private void Awake()
    {
        animator_ = GetComponent<Animator>();
        player_input_ = GetComponent<PlayerInput>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateButton();
        TransitionState();
        UpdateProcess();
    }

    void UpdateButton()
    {
        button_pressed_now_ = false;
        if (!button_pressed_previous_ && button_pressed_previous_)
        {
            button_pressed_now_ = true;
        }
        button_pressed_previous_ = button_pressed_;
    }

    void TransitionState()
    {
        if (state_ != State.Damage)
        {
            damage_time_remain_ -= Time.deltaTime;
            if (damage_time_remain_ <= 0) state_ = State.Idle;
        }
        if (damage_time_remain_ > 0)
        {
            state_ = State.Damage;
        }


        switch (state_)
        {
            case State.Idle:
                if (input_direction_.sqrMagnitude > 0.1f)
                {
                    state_ = State.Walk;
                }
                if (nearest_bubble_ && button_pressed_now_)
                {
                    state_ = State.Pickup;
                    holding_bubble_ = nearest_bubble_;
                    holding_bubble_.Pickup(this);
                }
                break;

            case State.Walk:
                if (input_direction_.sqrMagnitude < 0.1f)
                {
                    state_ = State.Walk;
                }
                break;

            case State.Pickup:
                // アニメが終わったらHoldに遷移
                break;

            case State.Hold:
                if (!button_pressed_)
                {
                    if (input_direction_.sqrMagnitude > 0.1f)
                    {
                        state_ = State.Throw;
                        holding_bubble_.Throw(this.transform.position, input_direction_);
                        holding_bubble_ = null;
                    }
                    if (nearest_bubble_ && button_pressed_now_)
                    {
                        state_ = State.PutDown;
                        holding_bubble_.Put();
                        holding_bubble_ = null;
                    }
                }
                break;

            case State.PutDown:
                // アニメが終わったらstandに遷移
                break;

            case State.Throw:
                // アニメが終わったらstandに遷移
                break;

            case State.Damage:
                // 硬直が終わったらstandに遷移
                break;
        }
    }


    void UpdateProcess()
    {
        switch (state_)
        {
            case State.Idle:
                // アニメをstand
                break;

            case State.Walk:
                // アニメをwalk
                MovePlayer();
                break;

            case State.Pickup:
                // アニメをpickup
                break;

            case State.Hold:
                // アニメをkeep
                break;

            case State.PutDown:
                // アニメをputdown
                break;

            case State.Throw:
                // アニメをthrow
                break;

            case State.Damage:
                // アニメをdamage
                break;
        }
    }

    void MovePlayer()
    {
        var p = this.transform.position;
        p += new Vector3(input_direction_.x, 0, input_direction_.y) * kMoveSpeed * Time.deltaTime;
        this.transform.position = p;

        // TODO: 場外判定
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        input_direction_ = Vector2.ClampMagnitude(input, 1.0f);
        Debug.Log(input_direction_);
    }

    public void OnInteract(InputValue value)
    {
        button_pressed_ = value.isPressed;
    }

    public void SetDamage(int rank)
    {
        damage_time_remain_ = 1;
    }

    private State state_ = State.Idle;
    private bool is_left_ = true;
    private PlayerInput player_input_;
    private Animator animator_;
    private Bubble nearest_bubble_ = null;
    private Bubble holding_bubble_ = null;

    private Vector2 input_direction_;
    private bool button_pressed_ = false;
    private bool button_pressed_previous_ = false;
    private bool button_pressed_now_ = false;

    private float damage_time_remain_ = 0;

    [SerializeField] private float kMoveSpeed;

}
