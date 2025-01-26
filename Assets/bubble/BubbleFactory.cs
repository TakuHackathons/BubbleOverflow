using UnityEngine;

public class BubbleFactory : MonoBehaviour
{
    public Bubble Make(Bubble.Color color, int rank, Vector3 position)
    {
        GameObject obj = null;
        switch (color)
        {
            case Bubble.Color.Red: obj = GameObject.Instantiate(red); break;
            case Bubble.Color.Yellow: obj = GameObject.Instantiate(yellow); break;
            case Bubble.Color.Blue: obj = GameObject.Instantiate(blue); break;
            case Bubble.Color.Purple: obj = GameObject.Instantiate(purple); break;
        }
        var ret = obj.GetComponent<Bubble>();
        ret.Init(color, rank);
        ret.transform.position = position;
        return ret;
    }

    [SerializeField] GameObject red;
    [SerializeField] GameObject yellow;
    [SerializeField] GameObject blue;
    [SerializeField] GameObject purple;

}
