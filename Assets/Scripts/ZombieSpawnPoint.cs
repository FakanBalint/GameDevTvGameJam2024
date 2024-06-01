using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnPoint : MovingPoint
{
    [SerializeField] private Vector2 range;
    [SerializeField] private GameObject Enemy;

    [SerializeField]private AnimationCurve spawnCurve;
    protected override void Start()
    {
        base.Start();
        isAccessible = false;

        StartCoroutine(Spawner());
    }


    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(spawnCurve.Evaluate(Time.deltaTime)-Random.Range(0,7));
        EnemyBehaviour enemy = Instantiate(Enemy, new Vector3(transform.position.x + Random.Range(range.x, range.y), transform.position.y + Random.Range(range.x, range.y), transform.position.z), Quaternion.identity).GetComponent<EnemyBehaviour>();
        enemy.SetCurrentPoint(this);
        StartCoroutine(Spawner());

    }

    protected override IEnumerator SpawnAmmoBox()
    {
        yield break;
    }
}
