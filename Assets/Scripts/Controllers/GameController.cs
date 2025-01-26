using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class GameController : SingletonBehaviour<GameController>
{
    public int Score { get; private set; }
    public PlayerData myPlayer { get; private set; }

    [SerializeField] GameUICanvas uiCanvas;
    [SerializeField] float timeupSecond = 60f;
    
    private float currentTimeSecond = 0f;

    async void Start()
    {
        this.Score = 0;
        this.SetupPlayers();
        var wm = WebsocketManager.Instance;
        wm.OnRecievedMessage = RecievedMessage;
        wm.Connect();
    }

    private void RecievedMessage(string message)
    {
        WSBaseTemplate messageTmp = JsonConvert.DeserializeObject<WSBaseTemplate>(message);
        if (messageTmp.action == "connected")
        {
            var userData = messageTmp.parseData<UserData>();
            myPlayer = new PlayerData()
            {
                uuid = userData.userId,
            };
            Debug.Log(userData.userId);
        }
    }

    void Update()
    {
        currentTimeSecond += Time.deltaTime;
        if (currentTimeSecond >= timeupSecond)
        {
            uiCanvas.ShowResultWindow();
        }
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
        playerDataList.Add(myPlayer);
        for (int i = 0; i < playerNumberNames.Count; ++i)
        {
            PlayerNumberName playerNumberName = playerNumberNames[i];
            if (myPlayerName == playerNumberName)
            {
                myPlayer.playerNumberName = playerNumberName;
            } else
            {
                PlayerData playerData = new PlayerData()
                {
                    uuid = Guid.NewGuid().ToString(),
                    playerNumberName = playerNumberName
                };
                playerDataList.Add(playerData);
            }
        }
        FieldController.Instance.SpawnPlayers(playerDataList);
    }
}
