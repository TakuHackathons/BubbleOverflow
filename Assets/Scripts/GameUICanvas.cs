using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeCounterText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float currentTimeSecond = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeSecond += Time.deltaTime;
        timeCounterText.text = $"{Mathf.Floor(currentTimeSecond).ToString()} seconds";
        scoreText.text = $"score: {GameController.Instance.Score.ToString()}";
    }
}
