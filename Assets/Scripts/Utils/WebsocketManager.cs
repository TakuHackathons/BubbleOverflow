using UnityEngine;
using NativeWebSocket;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Websocket.Client;
using UnityEditor.PackageManager;
using System.Threading.Tasks;

public class WebsocketManager : SingletonBehaviour<WebsocketManager>
{
    [SerializeField] private string webSocketUrl;

    WebsocketClient websocket;
    private bool isConnected = false;

    public Action<string> OnRecievedMessage = null;

    async public void Connect()
    {
        var url = new Uri(webSocketUrl);
        websocket = new WebsocketClient(url);
        websocket.MessageReceived.Subscribe((msg) =>
        {
            if (OnRecievedMessage != null)
            {
                OnRecievedMessage(msg.Text);
            }
        });
        await Task.Run(() => websocket.Start());
        WSBaseTemplate openMessage = new WSBaseTemplate();
        openMessage.action = "connect";
        var user = new UserData();
        user.userId = PlayerPrefs.GetString("userUuid", null);
        openMessage.data = user;
        SendWebSocketMessage(JsonConvert.SerializeObject(openMessage));

        isConnected = true;
    }

    public async void SendWebSocketMessage(byte[] message)
    {
        // Sending bytes
        await Task.Run(() => websocket.Send(message));
    }

    public async void SendWebSocketMessage(string message)
    {
        await Task.Run(() => websocket.Send(message));
    }

    private async void OnApplicationQuit()
    {
        isConnected = false;
    }
}
