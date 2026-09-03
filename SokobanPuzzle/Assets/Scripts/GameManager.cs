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

    public void CheckWin()
    {
        BoxController[] boxes =
            FindObjectsByType<BoxController>(
                FindObjectsSortMode.None
            );

        TargetController[] targets =
            FindObjectsByType<TargetController>(
                FindObjectsSortMode.None
            );

        // Check box and target count
        if (boxes.Length != targets.Length)
        {
            Debug.Log(
                "Not complete: Box and target count do not match."
            );

            return;
        }

        // Check every box
        foreach (BoxController box in boxes)
        {
            TargetController target =
                GridManager.Instance.GetTargetAt(
                    box.GridPosition
                );

            if (target == null)
            {
                Debug.Log(
                    "Not complete: A box is not on a target."
                );

                return;
            }
        }

        // All boxes are on targets
        LevelCompleted();
    }

    public void LevelCompleted()
    {
        Debug.Log("LEVEL COMPLETE!");
    }
}