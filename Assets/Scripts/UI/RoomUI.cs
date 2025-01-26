using Newtonsoft.Json;
using UnityEngine;

public class RoomUI : MonoBehaviour
{
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
        Debug.Log("createRoom");
        WSBaseTemplate message = new WSBaseTemplate();
        message.action = "createdRoom";
        var user = new UserData();
        user.userId = GameController.Instance.myPlayer.uuid;
        message.data = user;
        WebsocketManager.Instance.SendWebSocketMessage(JsonConvert.SerializeObject(message));
    }
}
