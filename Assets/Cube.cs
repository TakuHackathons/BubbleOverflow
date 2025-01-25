using UnityEngine;

public class Cube : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        ++count;

        if (count % 240 == 0)
        {
            GameObject obj = GameObject.Instantiate(bubble);
            bb = obj.GetComponent<Bubble>();

        }
        else if (count % 240 == 1)
        {
            bb.Throw(new Vector3(0, 0, 0), new Vector3(100.0f, 0, 0));
        }
    }

    Bubble bb;
    private int count = 0;

    [SerializeField] private GameObject bubble;

}
