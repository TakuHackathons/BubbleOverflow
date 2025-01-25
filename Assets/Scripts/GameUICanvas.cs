using TMPro;
using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeCounterText;

    private float currentTimeSecond = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeSecond += Time.deltaTime;
        timeCounterText.text = $"{Mathf.Floor(currentTimeSecond).ToString()} seconds";
    }
}
