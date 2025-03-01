using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController : SingletonBehaviour<FieldController>
{
    [SerializeField] private GameObject fieldGameObj;
    [SerializeField] private List<PlayerTerritory> playerTerritories;
    [SerializeField] private List<PlayerRootNumberName> playerRootNumberNames;

    [SerializeField] private Ship ship_dog;
    [SerializeField] private Ship ship_cat;
    [SerializeField] private Ship ship_bunny;
    [SerializeField] private Ship ship_horse;

    public int DogScore
    {
        get
        {
            return ship_dog.GetScore();
        }
    }

    public int CatScore
    {
        get
        {
            return ship_cat.GetScore();
        }

    }

    public int BunnyScore
    {
        get
        {
            return ship_bunny.GetScore();
        }
    }

    public int HorseScore
    {
        get
        {
            return ship_horse.GetScore();
        }
    }

    public PlayerRoot GetWinnerPlayerRoot()
    {
        List<Ship> ships = new List<Ship> { ship_dog, ship_cat, ship_bunny, ship_horse };
        Ship winnerShip = ships.OrderBy((ship) => ship.GetScore()).LastOrDefault();
        Territory winnerTerritory = winnerShip.GetComponent<Territory>();
        PlayerRootNumberName playerRootNumberName = playerRootNumberNames.Find((playerRootNumberName) => playerRootNumberName.playerNumberName == winnerTerritory.PlayerNumberName);
        return playerRootNumberName.playerRoot;
    }

    private void Awake()
    {
        // 
        Utils.InstantiateTo(this.gameObject, fieldGameObj);
        
        for(int i = 0;i < playerTerritories.Count;++i)
        {
           Territory territory = playerTerritories[i].territory;
           territory.SetPlayerNumberName(playerTerritories[i].PlayerNumberName);
           territory.OnHitTerritory = HitTerritory;
        }
        
    }

    private void HitTerritory(PlayerNumberName pnn, GameObject hitObject)
    {
        GameController.Instance.AddScore(1);
    }

    void Start()
    {
        // ƒTƒEƒ“ƒh
        SoundController.Instance.PlayBGM(BGM.Game);
    }

    void Update()
    {
    }

    public void SpawnPlayers(List<PlayerData> spawnPlayerDataList)
    {
        /*
        for (int i = 0; i < playerRootNumberNames.Count; ++i)
        {
            PlayerRootNumberName playerRootNumberName = playerRootNumberNames[i];
            PlayerData spawnPlayerData = spawnPlayerDataList.Find((spawnPlayerData) => spawnPlayerData.playerNumberName == playerRootNumberName.playerNumberName);
            if (spawnPlayerData != null)
            {
                playerRootNumberName.playerRoot.gameObject.SetActive(true);
                playerRootNumberName.playerRoot.Init(spawnPlayerData);
            }
            else
            {
                playerRootNumberName.playerRoot.gameObject.SetActive(false);
            }
        }
        */
    }
}
