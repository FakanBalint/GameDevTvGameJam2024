using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct ConnectedPoint
{
    public MovingPoint point;
    public Vector2 direction;
    public Vector2 notNormalisedDirection;

    public ConnectedPoint(MovingPoint point, Vector2 direction, Vector2 notNormalisedDirection)
    {
        this.point = point;
        this.direction = direction;
        this.notNormalisedDirection = notNormalisedDirection;
    }
}

public class MovingPoint : MonoBehaviour
{
    [SerializeField] private  GameObject AmmoBoxPrefab;
    [SerializeField] private Sprite EnabledSprite;
    [SerializeField] private Sprite DisabledSprite;

    public bool isPlayerOn;
    [SerializeField] protected bool isAccessible = true;
    [SerializeField] protected List<ConnectedPoint> connectedPoints = new List<ConnectedPoint>();
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private GameObject enabledArrowPrefab;
    [SerializeField] private GameObject disabledArrowPrefab;
    private List<GameObject> arrows = new List<GameObject>();

    public Transform GetTransform()
    {
        return transform;
    }

    public bool IsPlayerOn()
    {
        return isPlayerOn;
    }

    public void SetPlayerOn(bool isPlayerOn)
    {
        this.isPlayerOn = isPlayerOn;
        UpdateArrows();
    }

    public bool IsAccessible()
    {
        return isAccessible;
    }

    public void SetAccessible(bool isAccessible)
    {
        this.isAccessible = isAccessible;

        GetComponent<SpriteRenderer>().sprite = isAccessible ? EnabledSprite : DisabledSprite;
        UpdateArrows();
    }

    public List<ConnectedPoint> GetConnectedPoints()
    {
        return connectedPoints;
    }

    protected virtual void Start()
    {
        StartCoroutine(SpawnAmmoBox());
        Collider2D ownCollider = GetComponent<Collider2D>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider == ownCollider)
            {
                continue;
            }

            Vector2 dir = (collider.transform.position - ownCollider.transform.position).normalized;
            Vector2 notNormalisedDir = (collider.transform.position - ownCollider.transform.position);
            MovingPoint point = collider.gameObject.GetComponent<MovingPoint>();
            connectedPoints.Add(new ConnectedPoint(point, new Vector2(Mathf.RoundToInt(dir.x), Mathf.RoundToInt(dir.y)), notNormalisedDir));
        }

        UpdateArrows();
    }

    IEnumerator SpawnAmmoBox()
    {
        yield return new WaitForSeconds(GetAmmoBoxSpawn());
            Instantiate(AmmoBoxPrefab, transform.position, Quaternion.identity);
        SpawnAmmoBox();
    }

    private void UpdateArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }

        arrows.Clear();

        if (isPlayerOn)
        {
            foreach (ConnectedPoint connectedPoint in connectedPoints)
            {
                GameObject arrowPrefab = connectedPoint.point.IsAccessible() ? enabledArrowPrefab : disabledArrowPrefab;
                Vector3 offsetPosition = transform.position + (Vector3)connectedPoint.notNormalisedDirection.normalized * arrowOffsetDistance;
                GameObject arrow = Instantiate(arrowPrefab, offsetPosition, Quaternion.identity, transform);
                arrow.transform.right = connectedPoint.notNormalisedDirection.normalized; // Rotate the arrow to point in the direction

                arrows.Add(arrow);
            }
        }
    }

    private void Update()
    {
        UpdateArrows();
        if(!IsAccessible()){
            StopCoroutine(SpawnAmmoBox());
        }
    }


    private float GetAmmoBoxSpawn(){
        return Random.Range(300, 30);
    }

    [SerializeField] private float radius = 1f;
    [SerializeField] private float arrowOffsetDistance = 0.5f; // Offset distance for the arrows
    public Color gizmoColor = Color.red;

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
