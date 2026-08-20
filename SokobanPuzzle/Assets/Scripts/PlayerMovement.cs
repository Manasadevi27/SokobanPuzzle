using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 targetPosition;
    private Vector2Int playerGridPosition;

    private void Start()
    {
        playerGridPosition =
            GridManager.Instance.WorldToGrid(transform.position);

        targetPosition = transform.position;

        Debug.Log("PLAYER START POSITION: " + playerGridPosition);
    }

    private void Update()
    {
        MovePlayer();

        if (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            return;
        }

        HandleInput();
    }

    private void HandleInput()
    {
        Vector2Int direction = Vector2Int.zero;

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            direction = Vector2Int.up;
            Debug.Log("W PRESSED");
        }
        else if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            direction = Vector2Int.down;
            Debug.Log("S PRESSED");
        }
        else if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            direction = Vector2Int.left;
            Debug.Log("A PRESSED");
        }
        else if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            direction = Vector2Int.right;
            Debug.Log("D PRESSED");
        }

        if (direction != Vector2Int.zero)
        {
            TryMove(direction);
        }
    }

    private void TryMove(Vector2Int direction)
    {
        Vector2Int playerNextPosition =
            playerGridPosition + direction;

        Debug.Log(
            "Trying to move to: " + playerNextPosition
        );

        // WALL
        if (GridManager.Instance.IsWallAt(playerNextPosition))
        {
            Debug.Log("PLAYER BLOCKED BY WALL");
            return;
        }

        // BOX
        BoxController box =
            GridManager.Instance.GetBoxAt(playerNextPosition);

        if (box != null)
        {
            Debug.Log("BOX FOUND: " + box.name);

            Vector2Int boxNextPosition =
                playerNextPosition + direction;

            Debug.Log(
                "Checking next box position: "
                + boxNextPosition
            );

            // Wall behind box
            if (GridManager.Instance.IsWallAt(boxNextPosition))
            {
                Debug.Log("BOX IS BLOCKED BY WALL");
                return;
            }

            // Another box behind box
            BoxController secondBox =
                GridManager.Instance.GetBoxAt(boxNextPosition);

            if (secondBox != null)
            {
                Debug.Log(
                    "BOX IS BLOCKED BY ANOTHER BOX: "
                    + secondBox.name
                );

                return;
            }

            // Push box
            box.MoveTo(boxNextPosition);

            Debug.Log("BOX PUSHED");
        }

        playerGridPosition = playerNextPosition;

        targetPosition =
            GridManager.Instance.GridToWorld(
                playerGridPosition,
                transform.position.y
            );

        Debug.Log(
            "PLAYER MOVING TO: "
            + playerGridPosition
        );
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }
}