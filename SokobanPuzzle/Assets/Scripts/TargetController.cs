using UnityEngine;

public class TargetController : MonoBehaviour
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
}