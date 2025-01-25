using TMPro;
using UnityEngine;

public class PlayerRoot : MonoBehaviour
{
    [SerializeField] private TextMeshPro playerNameText;
    [SerializeField] private GameObject playerObj;

    private PlayerData playerData;

    public void Init(PlayerData data)
    {
        this.playerData = data;
        if (GameController.Instance.myPlayer == data)
        {
            playerNameText.text = "you";
        }
//        GameObject playerGameObject = Utils.InstantiateTo(this.gameObject, playerObj);
//        playerGameObject.transform.parent = playerNameText.transform;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
