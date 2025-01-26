using UnityEngine;

public class BubbleTester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FirstInitiate();
        TogglePower(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!power) return;

        count += Time.deltaTime;

        if (count > 0.3)
        {
            count = 0;
            var factory = bubble_factory.GetComponent<BubbleFactory>();
            var angle = Random.Range(0, Mathf.PI * 2);
            var amp = Random.Range(0.0f, 50.0f);
            var bubble = factory.Make(color, 1, new Vector3(Mathf.Cos(angle) * amp, 0, Mathf.Sin(angle) * amp));
            switch (color)
            {
                case Bubble.Color.Red: color = Bubble.Color.Yellow; break;
                case Bubble.Color.Yellow: color = Bubble.Color.Blue; break;
                case Bubble.Color.Blue: color = Bubble.Color.Red; break;
                    //case Bubble.Color.Purple: color = Bubble.Color.Red; break;
            }
        }
    }

    public void FirstInitiate()
    {
        var factory = bubble_factory.GetComponent<BubbleFactory>();
        for (int k = 0; k < 12; ++k)
        {
            Bubble.Color col = (Bubble.Color)Random.Range(0, 2);
            var radius = 60.0f;
            var angle = Mathf.PI * 2 * k / 12;
            factory.Make(col, 1, new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle)));
        }
    }

    void TogglePower(bool b)
    {
        power = b;
    }

    bool power = false;
    float count = 0;
    Bubble.Color color = Bubble.Color.Blue;
    [SerializeField] private GameObject bubble_factory;
}
