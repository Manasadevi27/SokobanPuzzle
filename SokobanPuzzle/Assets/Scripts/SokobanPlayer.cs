using UnityEngine;

public class SokobanPlayer : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Grid")]
    [SerializeField] private float cellSize = 1f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        HandleInput();
        MovePlayer();
    }

    private void HandleInput()
    {
        if (isMoving)
            return;

        Vector2Int direction = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2Int.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2Int.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2Int.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2Int.right;
        }

        if (direction != Vector2Int.zero)
        {
            TryMove(direction);
        }
    }

    private void TryMove(Vector2Int direction)
    {
        Vector3 moveDirection = new Vector3(
            direction.x,
            0f,
            direction.y
        );

        Vector3 targetCell = transform.position + moveDirection * cellSize;

        // Check what's in front of the player
        Collider[] objectsAtTarget = Physics.OverlapBox(
            targetCell,
            Vector3.one * 0.4f
        );

        foreach (Collider obj in objectsAtTarget)
        {
            // WALL
            if (obj.CompareTag("Wall"))
            {
                Debug.Log("Wall ahead - cannot move.");
                return;
            }

            // BOX
            if (obj.CompareTag("Box"))
            {
                return TryPushBox(obj.gameObject, moveDirection);
            }
        }

        // EMPTY CELL
        MoveTo(targetCell);
    }

    private bool TryPushBox(GameObject box, Vector3 direction)
    {
        Vector3 boxTargetPosition =
            box.transform.position + direction * cellSize;

        // Check what's behind the box
        Collider[] objectsBehindBox = Physics.OverlapBox(
            boxTargetPosition,
            Vector3.one * 0.4f
        );

        foreach (Collider obj in objectsBehindBox)
        {
            // Box cannot be pushed into another box
            if (obj.CompareTag("Box"))
            {
                Debug.Log("Another box is behind - cannot push.");
                return false;
            }

            // Box cannot be pushed into a wall
            if (obj.CompareTag("Wall"))
            {
                Debug.Log("Wall behind box - cannot push.");
                return false;
            }
        }

        // The space behind the box is empty
        box.transform.position = boxTargetPosition;

        // Player moves into the box's old position
        Vector3 playerTargetPosition =
            transform.position + direction * cellSize;

        MoveTo(playerTargetPosition);

        return true;
    }

    private void MoveTo(Vector3 position)
    {
        targetPosition = position;
        isMoving = true;
    }

    private void MovePlayer()
    {
        if (!isMoving)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}