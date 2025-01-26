using UnityEngine;

public class BubbleDetector : MonoBehaviour
{
    private SphereCollider bubbleSensor;

    void Start()
    {
        bubbleSensor = GetComponent<SphereCollider>();
        bubbleSensor.isTrigger = true;
    }

    public GameObject GetNearestBubble()
    {
        // 範囲内のBubbleタグを持つオブジェクトを取得
        Collider[] colliders = Physics.OverlapSphere(transform.position, bubbleSensor.radius);

        GameObject nearest = null;
        float minDistance = float.MaxValue;

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Bubble"))
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = col.gameObject;
                }
            }
        }

        return nearest;
    }
}