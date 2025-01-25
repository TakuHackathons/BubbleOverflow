using UnityEngine;
using NativeWebSocket;
using System;

public class WebsocketManager : SingletonBehaviour<WebsocketManager>
{
    [SerializeField] private string webSocketUrl;

    WebSocket websocket;
    private bool isConnected = false;

    public Action<byte[]> OnRecievedMessage = null;

    // Start is called before the first frame update
    async void Start()
    {
        websocket = new WebSocket(webSocketUrl);

        websocket.OnOpen += () =>
        {
            Debug.Log("Connection open!");
        };

        websocket.OnError += (e) =>
        {
            Debug.Log("Error! " + e);
        };

        websocket.OnClose += (e) =>
        {
            Debug.Log("Connection closed!");
        };

        websocket.OnMessage += (bytes) =>
        {
            Debug.Log("OnMessage!");
            Debug.Log(bytes);
            if (OnRecievedMessage != null)
            {
                OnRecievedMessage(bytes);
            }
        };
    }

    async public void Connect()
    {
        await websocket.Connect();
        isConnected = true;
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (isConnected)
        {
            websocket.DispatchMessageQueue();
        }
#endif
    }

    async void SendWebSocketMessage()
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.Send(new byte[] { 10, 20, 30 });

            // Sending plain text
            await websocket.SendText("plain text message");
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
        isConnected = false;
    }
}
