using UnityEngine;

public class TargetController : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }

    private void Start()
    {
        GridPosition =
            GridManager.Instance.WorldToGrid(transform.position);

        Debug.Log("TARGET START POSITION: " + GridPosition);
    }
}