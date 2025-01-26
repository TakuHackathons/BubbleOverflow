using UnityEngine;

public class BubbleTester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ++count;

        if (count % 200 == 0)
        {
            var factory = bubble_factory.GetComponent<BubbleFactory>();
            var bubble = factory.Make(color, 1, new Vector3(0, 0, 0));
            switch (color)
            {
                case Bubble.Color.Red: color = Bubble.Color.Yellow; break;
                case Bubble.Color.Yellow: color = Bubble.Color.Blue; break;
                case Bubble.Color.Blue: color = Bubble.Color.Red; break;
                    //case Bubble.Color.Purple: color = Bubble.Color.Red; break;
            }
            bubble.Throw(new Vector3(Random.Range(-50.0f, 50.0f), 0, Random.Range(-50.0f, 50.0f)), new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized);
        }
    }

    int count = 0;
    Bubble.Color color = Bubble.Color.Blue;
    [SerializeField] private GameObject bubble_factory;
}
