using UnityEngine;

public class Ship : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetScore();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int score)
    {
        score_ += score;
    }

    public void ResetScore()
    {
        score_ = 0;
    }

    public int GetIndex()
    {
        return player_index_;
    }

    public int GetScore()
    {
        return score_;
    }

    private int score_;
    [SerializeField] int player_index_;
}
