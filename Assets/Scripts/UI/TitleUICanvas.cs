using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] private GameObject titleUiField;
    [SerializeField] private GameObject creditUiField;
    [SerializeField] private string gameSceneName;

    void Start()
    {
        titleUiField.gameObject.SetActive(true);
        creditUiField.gameObject.SetActive(false);
        SoundController.Instance.PlayBGM(BGM.Intro);
    }

    public void OnClickStartButton()
    {
        SoundController.Instance.PlaySE(SE.Ok);
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnClickCreditButton()
    {
        SoundController.Instance.PlaySE(SE.Ok);
        titleUiField.gameObject.SetActive(false);
        creditUiField.gameObject.SetActive(true);
    }

    public void OnClickCreditBackButton()
    {
        SoundController.Instance.PlaySE(SE.Cancel);
        titleUiField.gameObject.SetActive(true);
        creditUiField.gameObject.SetActive(false);
    }
}
