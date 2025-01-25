using System;

public enum PlayerNumberName
{
    Player1,
    Player2,
    Player3,
    Player4,
}

[Serializable]
public class PlayerTerritory
{
    public PlayerNumberName PlayerNumberName;
    public Territory territory;
}
