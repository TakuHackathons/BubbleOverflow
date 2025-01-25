using UnityEngine;
using System.Collections.Generic;

public class FieldController : MonoBehaviour
{
    [SerializeField] private GameObject fieldGameObj;
    [SerializeField] private List<PlayerTerritory> playerTerritories;

    private void Awake()
    {
        //Utils.InstantiateTo(this.gameObject, fieldGameObj);
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
}
