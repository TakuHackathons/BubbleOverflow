using TMPro;
using UnityEngine;

public class GameUICanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_dog;
    [SerializeField] private TextMeshProUGUI text_cat;
    [SerializeField] private TextMeshProUGUI text_bunny;
    [SerializeField] private TextMeshProUGUI text_horse;
    [SerializeField] private ResultWindow resultWindow;

    private float currentTimeSecond = 0f;
    void Start()
    {
        resultWindow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FieldController fieldControlerInstance = FieldController.Instance;
        text_dog.text = $"{fieldControlerInstance.DogScore}";
        text_cat.text = $"{fieldControlerInstance.CatScore}";
        text_bunny.text = $"{fieldControlerInstance.BunnyScore}";
        text_horse.text = $"{fieldControlerInstance.HorseScore}";
    }

    public void ShowResultWindow()
    {
        resultWindow.gameObject.SetActive(true);
        resultWindow.ShowWinner();
    }
}
