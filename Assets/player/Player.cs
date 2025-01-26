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
        gamepad = Gamepad.current;
        bubble_detector_ = GetComponentInChildren<BubbleDetector>();
        //child = transform.Find("BubbleSensor").gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateNearestBubble();
        UpdateButton();
        TransitionState();
        UpdateProcess();
    }
    void UpdateNearestBubble()
    {
        if (nearest_bubble_) nearest_bubble_.SetHighlight(false);
        var maybe_bubble = bubble_detector_.GetNearestBubble();
        if (!maybe_bubble) nearest_bubble_ = null;
        else nearest_bubble_ = maybe_bubble.GetComponent<Bubble>();
        if (nearest_bubble_) nearest_bubble_.SetHighlight(true);
    }

    void UpdateButton()
    {
        Vector2 input = new(gamepad.leftStick.x.value, gamepad.leftStick.y.value);
        input_direction_ = Vector2.ClampMagnitude(input, 1.0f);

        button_pressed_ = gamepad.buttonSouth.isPressed;
        button_pressed_now_ = false;
        if (!button_pressed_previous_ && button_pressed_)
        {
            button_pressed_now_ = true;
        }
        button_pressed_previous_ = button_pressed_;
    }

    void TransitionState()
    {
        if (state_ == State.Damage)
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
                if (nearest_bubble_ && button_pressed_now_)
                {
                    state_ = State.Pickup;
                    holding_bubble_ = nearest_bubble_;
                    holding_bubble_.Pickup(this);
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

        Debug.Log(state_);
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

    public void SetDamage(int rank)
    {
        damage_time_remain_ = 1;
    }

    private State state_ = State.Idle;
    private bool is_left_ = true;
    private Gamepad gamepad;
    private BubbleDetector bubble_detector_;
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
