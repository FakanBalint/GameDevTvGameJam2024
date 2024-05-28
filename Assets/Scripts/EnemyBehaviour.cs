using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private MovingPoint currentPoint;
    private bool isMoving = false;
    [SerializeField] float MovementSpeed = 0.3f;
    [SerializeField] SpriteRenderer spriteCharaterRenderer;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float WaitTime;

    [SerializeField] private List<Vector2> inputList;

    public void SetCurrentPoint(MovingPoint point)
    {
        this.currentPoint = point;
    }
    private void Start()
    {
        inputList.Add(Vector2.up);
        inputList.Add(Vector2.right);
        inputList.Add(Vector2.left);
    }

    private void Update()
    {
        if (!isMoving)
        {
            HandleInput();
        }
    }
    private void FlipSprite(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteCharaterRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteCharaterRenderer.flipX = true;
        }

    }

    private void HandleInput()
    {
        Vector2 input = inputList[Random.Range(0, 2)];
      
        if (input == Vector2.right)
        {
            MoveToConnectedPoint(Vector2.right);
            FlipSprite(Vector2.right);
        }

        if (input == Vector2.left)
        {
            MoveToConnectedPoint(Vector2.left);
            FlipSprite(Vector2.left);
        }

        if (input == Vector2.up)
        {
            MoveToConnectedPoint(Vector2.up);
        }
    }

    private void MoveToConnectedPoint(Vector2 direction)
    {
        foreach (var connectedPoint in currentPoint.GetConnectedPoints())
        {
            if (Vector2.Dot(connectedPoint.direction.normalized, direction) > 0.5f)
            {
                StartCoroutine(MoveToPoint(connectedPoint.point));
                break;
            }
        }
    }

    private IEnumerator MoveToPoint(MovingPoint targetPoint)
    {
        isMoving = true;
        Transform targetTransform = targetPoint.transform;
        Vector3 startPosition = transform.position;
        Vector3 endPosition =  new Vector3(targetTransform.position.x + Random.Range(offset.x, offset.y), targetTransform.position.y + Random.Range(offset.x, offset.y), targetTransform.position.z);
        float distance = Vector3.Distance(startPosition, endPosition);
        float moveDuration = distance / MovementSpeed;  // Calculate the duration based on speed

        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;

        currentPoint = targetPoint;
        yield return new WaitForSeconds(WaitTime);
        currentPoint.SetAccessible(false);
        isMoving = false;
    }
}
