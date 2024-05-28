using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ConnectedPoint
{
    public MovingPoint point;
    public Vector2 direction;

    public ConnectedPoint(MovingPoint point, Vector2 direction)
    {
        this.point = point;
        this.direction = direction;
    }
}

public class MovingPoint : MonoBehaviour
{
    [SerializeField] protected bool isAccessible = true;
    [SerializeField] protected List<ConnectedPoint> connectedPoints = new List<ConnectedPoint>();
    [SerializeField] private LayerMask targetLayer;

    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsAccessible()
    {
        return isAccessible;
    }

    public List<ConnectedPoint> GetConnectedPoints()
    {
        return connectedPoints;
    }

    protected virtual void Start()
    {
        Collider2D ownCollider = GetComponent<Collider2D>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        // Iterate over detected colliders if needed
        foreach (Collider2D collider in colliders)
        {
            if(collider == ownCollider)
            {
                continue;
            }

            Vector2 dir = (collider.transform.position - ownCollider.transform.position).normalized;
            //Debug.Log("Directions with: " + Mathf.RoundToInt(dir.x) + Mathf.RoundToInt(dir.y));
            MovingPoint point = collider.gameObject.GetComponent<MovingPoint>();
            connectedPoints.Add(new ConnectedPoint(point, new Vector2(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y))));
        }
    }

    [SerializeField] private float radius = 1f; // Radius of the circle
    public Color gizmoColor = Color.red; // Color of the gizmo


    // Draw a gizmo to visualize the circle
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
