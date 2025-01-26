using TMPro;
using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI timeCounterText;
    //[SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject ship_dog;
    [SerializeField] private GameObject ship_cat;
    [SerializeField] private GameObject ship_bunny;
    [SerializeField] private GameObject ship_horse;

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
        text_dog.text = $"{ship_dog.GetComponent<Ship>().GetScore()}";
        text_cat.text = $"{ship_cat.GetComponent<Ship>().GetScore()}";
        text_bunny.text = $"{ship_bunny.GetComponent<Ship>().GetScore()}";
        text_horse.text = $"{ship_horse.GetComponent<Ship>().GetScore()}";

        //currentTimeSecond += Time.deltaTime;
        //timeCounterText.text = $"{Mathf.Floor(currentTimeSecond).ToString()} seconds";
        //scoreText.text = $"score: {GameController.Instance.Score.ToString()}";
    }
}
