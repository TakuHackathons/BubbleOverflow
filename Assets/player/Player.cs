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
        var gamepad_list = Gamepad.all;
        if (gamepad_index < gamepad_list.Count) gamepad = Gamepad.all[gamepad_index];
        else gamepad = null;
        anime_ = new PlayerStateAnimation(GetComponent<Animator>());
        bubble_detector_ = GetComponentInChildren<BubbleDetector>();

        anime_.ResetAllTriggers();
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
        if (gamepad == null) return;
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
        if (damage_time_remain_ > 0)
        {
            state_ = State.Damage;
        }
        if (stop_timer_ > 0)
        {
            stop_timer_ -= Time.deltaTime;
        }


        switch (state_)
        {
            case State.Idle:
                if (input_direction_.sqrMagnitude > 0.1f)
                {
                    ChangeState(State.Walk);
                }
                if (nearest_bubble_ && button_pressed_now_)
                {
                    ChangeState(State.Pickup);
                }
                break;

            case State.Walk:
                if (input_direction_.sqrMagnitude < 0.1f)
                {
                    ChangeState(State.Idle);
                }
                if (nearest_bubble_ && button_pressed_now_)
                {
                    ChangeState(State.Pickup);
                }
                break;

            case State.Pickup:
                // アニメが終わったらHoldに遷移
                if (stop_timer_ <= 0)
                {
                    ChangeState(State.Hold);
                }
                break;

            case State.Hold:
                // バブルが存在する時のみ、PutDownを行う
                if (!holding_bubble_ || !holding_bubble_.IsAlive())
                {
                    anime_.PlayPutDown();
                    ChangeState(State.Idle);
                    break;
                }
                if (!button_pressed_)
                {
                    if (input_direction_.sqrMagnitude > 0.1f)
                    {
                        ChangeState(State.Throw);
                    }
                    else
                    {
                        ChangeState(State.PutDown);
                    }
                }
                break;

            case State.PutDown:
                if (stop_timer_ <= 0)
                {
                    ChangeState(State.Idle);
                }
                break;

            case State.Throw:
                if (stop_timer_ <= 0)
                {
                    ChangeState(State.Idle);
                }
                break;

            case State.Damage:
                if (damage_time_remain_ <= 0) ChangeState(State.Idle);
                ChangeState(State.Idle);
                break;
        }
    }

    void ChangeState(State state)
    {
        switch (state)
        {
            case State.Idle:
                state_ = State.Idle;
                holding_bubble_ = null;
                anime_.PlayIdle();
                break;

            case State.Walk:
                state_ = State.Walk;
                holding_bubble_ = null;
                anime_.PlayWalk();
                break;

            case State.Pickup:
                state_ = State.Pickup;
                holding_bubble_ = nearest_bubble_;
                holding_bubble_.Pickup(this);
                anime_.PlayPickup();
                SoundController.Instance.PlayVoice(gamepad_index, Voice.PickUp);
                stop_timer_ = 0.7f;
                break;

            case State.Hold:
                state_ = State.Hold;
                anime_.PlayHold();
                break;

            case State.PutDown:
                state_ = State.PutDown;
                holding_bubble_.Put();
                holding_bubble_ = null;
                anime_.PlayPutDown();
                stop_timer_ = 0.5f;
                break;

            case State.Throw:
                state_ = State.Throw;
                var throw_dir = new Vector3(input_direction_.x, 0, input_direction_.y);
                holding_bubble_.Throw(this.transform.position, throw_dir);
                holding_bubble_ = null;
                anime_.PlayThrow();
                SoundController.Instance.PlayVoice(gamepad_index, Voice.Throw);
                stop_timer_ = 0.5f;
                break;

            case State.Damage:
                damage_time_remain_ -= Time.deltaTime;
                break;
        }
    }


    void UpdateProcess()
    {
        switch (state_)
        {
            case State.Idle:
                break;

            case State.Walk:
                MovePlayer();
                break;

            case State.Pickup:
                break;

            case State.Hold:
                break;

            case State.PutDown:
                break;

            case State.Throw:
                break;

            case State.Damage:
                damage_time_remain_ -= Time.deltaTime;
                break;
        }
    }

    void MovePlayer()
    {
        var p = this.transform.position;
        p += new Vector3(input_direction_.x, 0, input_direction_.y) * kMoveSpeed * Time.deltaTime;

        // 場外判定
        var norm2 = p.x * p.x + (p.z + 5) * (p.z + 5);
        if (norm2 > kEdgeOfField * kEdgeOfField) p = p / Mathf.Sqrt(norm2) * kEdgeOfField;

        this.transform.position = p;
    }

    public void SetDamage(int rank)
    {
        damage_time_remain_ = 1;
    }

    private State state_ = State.Idle;
    private Gamepad gamepad;
    private BubbleDetector bubble_detector_;
    private PlayerStateAnimation anime_;
    private Bubble nearest_bubble_ = null;
    private Bubble holding_bubble_ = null;

    private Vector2 input_direction_;
    private bool button_pressed_ = false;
    private bool button_pressed_previous_ = false;
    private bool button_pressed_now_ = false;

    private float damage_time_remain_ = 0;

    private const float kEdgeOfField = 90.0f;

    private float stop_timer_ = 0;

    [SerializeField] private float kMoveSpeed;
    [SerializeField] private int gamepad_index;

}
