using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 targetPosition;

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
        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
        }

        if (direction != Vector3.zero)
        {
            targetPosition += direction;
        }
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