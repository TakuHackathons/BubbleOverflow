using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class GameController : SingletonBehaviour<GameController>
{
    public int Score { get; private set; }
    public PlayerData myPlayer { get; private set; }

    async void Start()
    {
        this.Score = 0;
        this.SetupPlayers();
        var wm = WebsocketManager.Instance;
        wm.OnRecievedMessage = RecievedMessage;
        wm.Connect();
    }

    private void RecievedMessage(string messageBytes)
    {
        WSBaseTemplate messageTmp = JsonConvert.DeserializeObject<WSBaseTemplate>(messageBytes);
        Debug.Log(messageTmp.action);
        if (messageTmp.action == ActionNames.connected)
        {
            var userData = messageTmp.parseData<UserData>();
            Debug.Log(userData.userId);
        }
    }

    void Update()
    {
    }

    public void AddScore(int score)
    {
        this.Score = score;
    }

    public void SetupPlayers()
    {
        List<PlayerData> playerDataList = new List<PlayerData>();
        List<PlayerNumberName> playerNumberNames = Enum.GetValues(typeof(PlayerNumberName)).Cast<PlayerNumberName>().ToList();
        PlayerNumberName myPlayerName = playerNumberNames[UnityEngine.Random.Range(0, playerNumberNames.Count)];
        for (int i = 0; i < playerNumberNames.Count; ++i)
        {
            PlayerNumberName playerNumberName = playerNumberNames[i];
            PlayerData playerData = new PlayerData()
            {
                uuid = Guid.NewGuid().ToString(),
                playerNumberName = playerNumberName
            };
            if (myPlayerName == playerNumberName)
            {
                myPlayer = playerData;
            }
            playerDataList.Add(playerData);
        }
        FieldController.Instance.SpawnPlayers(playerDataList);
    }
}
