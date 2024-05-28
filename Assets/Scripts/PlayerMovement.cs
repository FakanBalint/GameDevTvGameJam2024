using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovingPoint currentPoint;
    private bool isMoving = false;
    private Transform playerTransform;
    [SerializeField]float MovementSpeed = 0.5f;
    private void Start()
    {
        playerTransform = transform;
        SnapToCurrentPoint();
    }

    private void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
          MoveToConnectedPoint(Vector2.right); 
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            MoveToConnectedPoint(Vector2.left);
        }

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            MoveToConnectedPoint(Vector2.up);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            MoveToConnectedPoint(Vector2.down);
        }
    }

    private void MoveToConnectedPoint(Vector2 direction)
    {
        foreach (var connectedPoint in currentPoint.GetConnectedPoints())
        {
            if (connectedPoint.point.IsAccessible() && Vector2.Dot(connectedPoint.direction.normalized, direction) > 0.5f)
            {
                StartCoroutine(MoveToPoint(connectedPoint.point));
                break;
            }
        }
    }

    private IEnumerator MoveToPoint(MovingPoint targetPoint)
    {
        isMoving = true;
        Vector2 startPosition = playerTransform.position;
        Vector2 endPosition = targetPoint.GetTransform().position;
        float elapsedTime = 0;

        while (elapsedTime < MovementSpeed)
        {
            playerTransform.position = Vector2.Lerp(startPosition, endPosition, (elapsedTime / MovementSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = endPosition;
        currentPoint = targetPoint;
        isMoving = false;
    }

    private void SnapToCurrentPoint()
    {
        playerTransform.position = currentPoint.GetTransform().position;
    }
}
