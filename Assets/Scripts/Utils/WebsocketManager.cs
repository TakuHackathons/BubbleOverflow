using UnityEngine;
using NativeWebSocket;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class WebsocketManager : SingletonBehaviour<WebsocketManager>
{
    [SerializeField] private string webSocketUrl;

    WebSocket websocket;
    private bool isConnected = false;

    public Action<string> OnRecievedMessage = null;

    async public void Connect()
    {
        Debug.Log(webSocketUrl);
        websocket = new WebSocket(webSocketUrl);

        websocket.OnOpen += () =>
        {
            Dictionary<string, string> openMessage = new Dictionary<string, string>();
            openMessage["action"] = "connect";
            SendWebSocketMessage(JsonConvert.SerializeObject(openMessage));
//            SendWebSocketMessage(MessagePackSerializer.Serialize(openMessage, ContractlessStandardResolver.Options));
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
            if (OnRecievedMessage != null)
            {
                OnRecievedMessage(System.Text.Encoding.UTF8.GetString(bytes));
            }
        };
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

    async void SendWebSocketMessage(byte[] message)
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.Send(message);
        }
    }

    async void SendWebSocketMessage(string message)
    {
        if (websocket.State == WebSocketState.Open)
        {
            // Sending bytes
            await websocket.SendText(message);
        }
    }

    private async void OnApplicationQuit()
    {
        await websocket.Close();
        isConnected = false;
    }
}
