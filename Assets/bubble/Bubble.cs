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

    public AK.Wwise.Event BubbleThrowEvent;
    public AK.Wwise.Event BubbleCrushEvent;
    public AK.Wwise.Event BubblePickupEvent;
    public AK.Wwise.Event BubblePopEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highlight_ = false;
        state_ = State.Free;
        //SoundController.Instance.PlaySE(SE.BubblePop);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVelocity();

        // �ʒu�X�V
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
        // ���x�ύX
        const float rate = 1.0f - kFrictionVel;
        velocity_ *= rate;
    }

    void CheckOutOfBounds()
    {
        var p = transform.position;
        var norm2 = p.x * p.x + p.z * p.z;
        if (norm2 > kBoundOfField * kBoundOfField)
        {
            p = p / Mathf.Abs(norm2) * kBoundOfField;
        }
        transform.position = p;
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
        var p = this.transform.position;
        p = player_.transform.position + new Vector3(0, 0, hold_offset);
        this.transform.position = p;
        //SoundController.Instance.PlaySE(SE.BubblePickup);
        BubblePickupEvent.Post(gameObject);
    }

    public void Throw(Vector3 position, Vector3 direction)
    {
        state_ = State.Free;
        this.transform.position = position;
        velocity_ = direction * GetVelocityFromRank();
        //SoundController.Instance.PlaySE(SE.BubbleThrow);
        BubbleThrowEvent.Post(gameObject);
    }

    public void Put()
    {
        state_ = State.Free;
        this.transform.position = put_position_;
        velocity_ = new Vector3(0, 0, 0);
    }

    float GetVelocityFromRank()
    {
        if (rank_ == 1) return 250.0f;
        if (rank_ == 2) return 200.0f;
        if (rank_ == 3) return 150.0f;
        if (rank_ == 4) return 100.0f;
        if (rank_ == 5) return 50.0f;
        return 25.0f;
    }

    int GetScoreByRank()
    {
        if (rank_ == 1) return 1;
        if (rank_ == 2) return 4;
        if (rank_ == 3) return 9;
        if (rank_ == 4) return 16;
        if (rank_ == 5) return 25;
        return 36;
    }

    void OnTriggerEnter(Collider collider)
    { // �ڐG��������
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
            }
            opponent.is_dead_ = true;
            is_dead_ = true;
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
            //SoundController.Instance.PlaySE(SE.BubbleCrush);
            BubbleCrushEvent.Post(gameObject);

        }
        if (collider.gameObject.CompareTag("Chara"))
        {
            // TODO: �Ђ��
            BubbleCrushEvent.Post(gameObject);
        }
        if (collider.gameObject.CompareTag("Ship"))
        {
            // TODO: ���_����
            var ship = collider.gameObject.GetComponent<Ship>();
            ship.AddScore(GetScoreByRank());
            Destroy(this.gameObject);
            //SoundController.Instance.PlaySE(SE.BubbleCrush);
            BubblePopEvent.Post(gameObject);
        }

    }

    private Player player_;
    private State state_;
    private Color color_;
    private int rank_;
    [SerializeField] private Vector3 velocity_;
    private bool highlight_;
    private Vector3 put_position_;

    private bool is_dead_ = false;

    private const float kFrictionVel = 0.01f;
    private const float kFrictionFix = 0.01f;

    private const float kBoundOfField = 100.0f;

    [SerializeField] private GameObject bubble_factory;
    [SerializeField] private float hold_offset;

}
