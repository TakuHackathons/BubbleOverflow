using Newtonsoft.Json;
using UnityEngine;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private GameObject roomListView;
    [SerializeField] private GameObject roomListCellObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickCreateRoomButton()
    {
        WSBaseTemplate message = new WSBaseTemplate();
        message.action = "createdRoom";
        var user = new UserData();
        user.userId = GameController.Instance.myPlayer.uuid;
        message.data = user;
        WebsocketManager.Instance.SendWebSocketMessage(JsonConvert.SerializeObject(message));
    }
}
