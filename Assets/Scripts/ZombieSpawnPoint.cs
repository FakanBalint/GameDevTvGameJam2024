using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnPoint : MovingPoint
{
    [SerializeField] private Vector2 range;
    [SerializeField] private GameObject Enemy;
    protected override void Start()
    {
        base.Start();
        isAccessible = false;

        StartCoroutine(Spawner());
    }


    IEnumerator Spawner()
    {

        Debug.Log("STRT");
        yield return new WaitForSeconds(Random.Range(5, 10));
        Debug.Log("Spawn " + transform.name);
        EnemyBehaviour enemy = Instantiate(Enemy, new Vector3(transform.position.x + Random.Range(range.x, range.y), transform.position.y, transform.position.z), Quaternion.identity).GetComponent<EnemyBehaviour>();
        enemy.SetCurrentPoint(this);
        StartCoroutine(Spawner());

    }
}
