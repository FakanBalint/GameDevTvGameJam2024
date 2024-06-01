using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovingPoint currentPoint;
    private bool isMoving = false;
    private Transform playerTransform;
    [SerializeField]float MovementSpeed = 0.5f;
    [SerializeField]SpriteRenderer spriteCharaterRenderer;
    [SerializeField]private AudioClip walkSound;
    private void Start()
    {
        playerTransform = transform;
        SnapToCurrentPoint();
    }

    private void Update()
    {
        if(currentPoint.IsAccessible() == false){
            GameManager.instance.GameOver();
        }



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
          FlipSprite(Vector2.right);
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            MoveToConnectedPoint(Vector2.left);
            FlipSprite(Vector2.left);
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

        AudioManager.instance.PlaySound(walkSound);
        Vector3 startPosition = playerTransform.position;
        Vector3 endPosition = targetPoint.GetTransform().position;
        float distance = Vector3.Distance(startPosition, endPosition);
        float moveDuration = distance / MovementSpeed;  // Calculate the duration based on speed

        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
             if (!targetPoint.IsAccessible())
            {
                // Move back to the start position if target becomes inaccessible
                yield return MoveBackToStart(startPosition, elapsedTime);
                yield break;
            }

            playerTransform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveDuration));
            currentPoint.SetPlayerOn(false);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = endPosition;
        currentPoint = targetPoint;
        currentPoint.SetPlayerOn(true);
        
        isMoving = false;
    }

    private IEnumerator MoveBackToStart(Vector3 startPosition, float elapsedTime)
    {
        float moveBackTime = elapsedTime;
        float distance = Vector3.Distance(startPosition, transform.position);
        float moveDuration = distance / MovementSpeed;
        while (moveBackTime > 0)
        {
            playerTransform.position = Vector3.Lerp(startPosition, playerTransform.position, (moveBackTime / moveDuration));
            moveBackTime -= Time.deltaTime;
            yield return null;
        }

        playerTransform.position = startPosition;
        isMoving = false;
    }

    private void SnapToCurrentPoint()
    {
        playerTransform.position = currentPoint.GetTransform().position;
    }
}
