using UnityEngine;

public class Bubble : MonoBehaviour
{

    public enum Color
    {
        Red,
        Yellow,
        Blue,
        Purple
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
        float rate = 0.5f * rank_;
        this.transform.localScale = Vector3.one * rate;
    }

    public bool IsAlive()
    {
        return !is_dead_;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

        CheckOutOfBounds();
    }

    void UpdateVelocity()
    {
        // 速度変更
        const float rate = 1.0f - kFrictionVel;
        velocity_ *= rate;
    }

    void CheckOutOfBounds()
    {
        // TODO: 場外判定
    }


    public void SetHighlight(bool b)
    {
        highlight_ = b;
    }

    public void Pickup(Player_dummy player)
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

    void OnTriggerEnter(Collider collider)
    { // 接触した直後
        if (is_dead_) return;

        if (collider.gameObject.CompareTag("Bubble"))
        {
            var opponent = collider.gameObject.GetComponent<Bubble>();
            if (opponent.is_dead_) return;

            Vector3 mean_pos = (this.transform.position + opponent.transform.position) / 2.0f;

            if (color_ == opponent.color_)
            {
                var factory = bubble_factory.GetComponent<BubbleFactory>();
                factory.Make(color_, rank_ + opponent.rank_, mean_pos);
            }
            else
            {
                // TODO: 減点処理
            }
            opponent.is_dead_ = true;
            is_dead_ = true;
            Destroy(collider.gameObject);
            Destroy(this.gameObject);

        }
        if (collider.gameObject.CompareTag("Chara"))
        {
            // TODO: ひるみ
        }
        if (collider.gameObject.CompareTag("Ship"))
        {
            // TODO: 得点処理
            Destroy(this.gameObject);
        }

    }

    private Player_dummy player_;
    private State state_;
    private Color color_;
    private int rank_;
    [SerializeField] private Vector3 velocity_;
    private bool highlight_;
    private Vector3 put_position_;

    private bool is_dead_ = false;

    private const float kFrictionVel = 0.01f;
    private const float kFrictionFix = 0.01f;

    [SerializeField] private GameObject bubble_factory;


}
