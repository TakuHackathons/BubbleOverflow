using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private enum State
    {
        Stand,
        Walk,
        Pickup,
        Hold,
        Throw,
        Put,
        Damage
    }

    public void Init(Gamepad gamepad)
    {
        gamepad_ = gamepad;
        nearest_bubble_ = null;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ...
    }

    // Update is called once per frame
    void Update()
    {
        // ハイライトオフ
        if (nearest_bubble_) nearest_bubble_.SetHighlight(false);

        // ハイライトするべきバブルを検索
        UpdateNearestBubble();

        // ステートごとの処理
        DoStateProcess();

    }

    void UpdateNearestBubble()
    {
        // nearest_bubble_ = ...
    }

    void DoStateProcess()
    {
        // gamepad_を使って処理


        x = gamepad_.leftStick.x.value;
        y = gamepad_.leftStick.y.value;

        if (gamepad_.buttonSouth.isPressed)
        {
            var obj = GameObject.Instantiate(spawn_bubble_).GetComponent<Bubble>();
            obj.Init(Bubble.Color.Red, 1);
            obj.Throw(new Vector3(0, 0, 0), new Vector3(x, 0, y) * 50.0f);
        }

    }

    void Damage(Vector3 direction, int bubble_rank)
    {

    }


    Gamepad gamepad_;
    State state_;

    Bubble nearest_bubble_;
    Bubble picking_bubble_;

    [SerializeField] public float x;
    [SerializeField] public float y;
    [SerializeField] GameObject spawn_bubble_;
}
