using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] private GameObject titleUiField;
    [SerializeField] private GameObject creditUiField;

    void Start()
    {
        titleUiField.gameObject.SetActive(true);
        creditUiField.gameObject.SetActive(false);
    }

    public void OnClickStartButton()
    {
    }

    public void OnClickCreditButton()
    {
        titleUiField.gameObject.SetActive(false);
        creditUiField.gameObject.SetActive(true);
    }

    public void OnClickCreditBackButton()
    {
        titleUiField.gameObject.SetActive(true);
        creditUiField.gameObject.SetActive(false);
    }
}
