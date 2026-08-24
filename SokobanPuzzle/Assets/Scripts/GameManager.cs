using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Moves { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AddMove()
    {
        Moves++;

        Debug.Log("Moves: " + Moves);
    }

    public void ResetMoves()
    {
        Moves = 0;
    }

    public void LevelCompleted()
    {
        Debug.Log("?? LEVEL COMPLETE!");
    }
}