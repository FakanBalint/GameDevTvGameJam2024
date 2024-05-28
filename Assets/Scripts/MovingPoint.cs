using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ConnectedPoint
{
    public MovingPoint point;
    public Vector2 direction;
}

public class MovingPoint : MonoBehaviour
{
    [SerializeField] private bool isAccessible = true;
    [SerializeField] private List<ConnectedPoint> connectedPoints;

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
}
