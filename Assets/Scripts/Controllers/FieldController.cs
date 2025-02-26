using System.Collections.Generic;
using UnityEngine;

public class FieldController : SingletonBehaviour<FieldController>
{
    [SerializeField] private GameObject fieldGameObj;
    [SerializeField] private List<PlayerTerritory> playerTerritories;
    [SerializeField] private List<PlayerRootNumberName> playerRootNumberNames;

    private void Awake()
    {
        // ƒTƒEƒ“ƒh
        SoundController.Instance.PlayBGM(BGM.Game);

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
