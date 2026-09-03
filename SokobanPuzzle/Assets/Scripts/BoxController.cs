using UnityEngine;

public class BoxController : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }

    private void Start()
    {
        GridPosition =
            GridManager.Instance.WorldToGrid(transform.position);

        Debug.Log(
            "BOX START POSITION: " + GridPosition
        );
    }

    public void MoveTo(Vector2Int newGridPosition)
    {
        GridPosition = newGridPosition;

        transform.position =
            GridManager.Instance.GridToWorld(
                newGridPosition,
                transform.position.y
            );

        Debug.Log(
            "BOX MOVED TO: " + GridPosition
        );

        // Check target
        TargetController target =
            GridManager.Instance.GetTargetAt(GridPosition);

        if (target != null)
        {
            Debug.Log("🎯 BOX IS ON TARGET!");

            GameManager.Instance.CheckWin();
        }
    }
}