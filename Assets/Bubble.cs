using UnityEngine;

public class Bubble : MonoBehaviour
{

    public enum Color
    {
        Red,
        Green,
        Blue
    }

    private enum State
    {
        Free,
        Hold
    }

    public void Init(Color color, int rank)
    {
        color_ = color;
        rank_ = rank;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        velocity_.x = 0;
        velocity_.y = 0;
        velocity_.z = 1;
        highlight_ = false;
        state_ = State.Free;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity();

        // 位置更新
        switch (state_)
        {
            case State.Free:
                break;

            case State.Hold:
                velocity_ = new Vector3(0, 0, 0);
                break;
        }
        // this.transform.position += velocity_;
        var p = this.transform.position;
        p += velocity_ * Time.deltaTime;
        this.transform.position = p;
    }

    void UpdateVelocity()
    {
        // 速度変更
        const float rate = 1.0f - kFrictionVel;
        velocity_ *= rate;
    }


    public void SetHighlight(bool b)
    {
        highlight_ = b;
    }

    public void Pickup(Player player)
    {
        state_ = State.Hold;
        player_ = player;
        put_position_ = this.transform.position;
        velocity_ = new Vector3(0, 0, 0);

    }

    public void Throw(Vector3 position, Vector3 velocity)
    {
        state_ = State.Free;
        this.transform.position = position;
        velocity_ = velocity;
    }

    public void Put()
    {
        state_ = State.Free;
        this.transform.position = put_position_;
        velocity_ = new Vector3(0, 0, 0);

    }

    private Player player_;
    private State state_;
    private Color color_;
    private int rank_;
    [SerializeField] private Vector3 velocity_;
    private bool highlight_;
    private Vector3 put_position_;

    private const float kFrictionVel = 0.01f;
    private const float kFrictionFix = 0.01f;

    [SerializeField] private GameObject bubble;

}
