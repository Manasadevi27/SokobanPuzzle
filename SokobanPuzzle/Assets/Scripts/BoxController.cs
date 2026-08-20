using UnityEngine;

public class BoxController : MonoBehaviour
{
    public Vector2Int GridPosition
    {
        get
        {
            return new Vector2Int(
                Mathf.RoundToInt(transform.position.x),
                Mathf.RoundToInt(transform.position.z)
            );
        }
    }

    public void MoveTo(Vector2Int newPosition)
    {
        transform.position = new Vector3(
            newPosition.x,
            transform.position.y,
            newPosition.y
        );
    }
}