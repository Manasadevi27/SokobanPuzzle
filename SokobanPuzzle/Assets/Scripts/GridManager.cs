using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 5;
    public int height = 5;

    public float cellSize = 1f;

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        float x = gridPosition.x * cellSize;
        float z = gridPosition.y * cellSize;

        return new Vector3(x, 0, z);
    }

    private void Start()
    {
        Vector2Int playerPosition = new Vector2Int(2, 1);

        Debug.Log("Player Grid Position: " + playerPosition);
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = GetWorldPosition(
                    new Vector2Int(x, y)
                );

                Gizmos.DrawWireCube(
                    position,
                    new Vector3(cellSize, 0.1f, cellSize)
                );
            }
        }
    }
}