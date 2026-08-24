using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        return new Vector2Int(
            Mathf.RoundToInt(worldPosition.x),
            Mathf.RoundToInt(worldPosition.z)
        );
    }

    public Vector3 GridToWorld(Vector2Int gridPosition, float y)
    {
        return new Vector3(
            gridPosition.x,
            y,
            gridPosition.y
        );
    }

    public bool IsWallAt(Vector2Int position)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

        foreach (GameObject wall in walls)
        {
            Vector2Int wallPosition = WorldToGrid(wall.transform.position);

            if (wallPosition == position)
            {
                return true;
            }
        }

        return false;
    }

    public BoxController GetBoxAt(Vector2Int position)
    {
        BoxController[] boxes =
            FindObjectsByType<BoxController>(FindObjectsSortMode.None);

        foreach (BoxController box in boxes)
        {
            if (box.GridPosition == position)
            {
                return box;
            }
        }

        return null;
    }

    public TargetController GetTargetAt(Vector2Int position)
    {
        TargetController[] targets =
            FindObjectsByType<TargetController>(
                FindObjectsSortMode.None
            );

        foreach (TargetController target in targets)
        {
            if (target.GridPosition == position)
            {
                return target;
            }
        }

        return null;
    }
}