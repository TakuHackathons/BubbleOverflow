using UnityEngine;

public class GameController : SingletonBehaviour<GameController>
{
    public int Score { get; private set; }

    void Start()
    {
        this.Score = 0;
    }

    void Update()
    {
    }

    public void AddScore(int score)
    {
        this.Score = score;
    }
}
