using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerPlayerNameText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowWinner()
    {
        var winnerPlayerRoot = FieldController.Instance.GetWinnerPlayerRoot();
        winnerPlayerNameText.text = $"Winner {winnerPlayerRoot.PlayerName} !!!!!!";
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
