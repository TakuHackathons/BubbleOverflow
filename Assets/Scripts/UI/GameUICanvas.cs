using TMPro;
using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI timeCounterText;
    //[SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private TextMeshProUGUI text_dog;
    [SerializeField] private TextMeshProUGUI text_cat;
    [SerializeField] private TextMeshProUGUI text_bunny;
    [SerializeField] private TextMeshProUGUI text_horse;

    private float currentTimeSecond = 0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FieldController fieldControlerInstance = FieldController.Instance;
        text_dog.text = $"{fieldControlerInstance.DogScore}";
        text_cat.text = $"{fieldControlerInstance.CatScore}";
        text_bunny.text = $"{fieldControlerInstance.BunnyScore}";
        text_horse.text = $"{fieldControlerInstance.HorseScore}";

        //currentTimeSecond += Time.deltaTime;
        //timeCounterText.text = $"{Mathf.Floor(currentTimeSecond).ToString()} seconds";
        //scoreText.text = $"score: {GameController.Instance.Score.ToString()}";
    }
}
