using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineSize : MonoBehaviour
{

    [SerializeField] private int maxBullets = 60;
    [SerializeField] private int currentBullets;
    [SerializeField] private float bulletLoadFrequency = 1;
    private bool magazineFull = true;
    public static MagazineSize instance { get; private set; }

    private void Awake()
    {
        if (instance != null || instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
            currentBullets = maxBullets;
        }
        StartCoroutine(bulletLoad());
    }

    private void Update()
    {
        IncreaseReloadFrequency();
    }
    private void IncreaseReloadFrequency()
    {
        float elapsedTime = Time.realtimeSinceStartup;
        if(elapsedTime % 20f == 0) 
        {
            Debug.Log("Load speed increased.");
            bulletLoadFrequency *= 1.3f;
        }
    }
    IEnumerator bulletLoad()
    {
        yield return new WaitForSeconds(2/bulletLoadFrequency);
        if (!magazineFull)
        {
            currentBullets++;
            if(currentBullets == maxBullets)
            {
                magazineFull = true;
            } else
            {
                magazineFull = false;
            }
            StartCoroutine(bulletLoad());
        } else
        {
            StartCoroutine(bulletLoad());
        }
    }

    public bool canShoot(int bulletCount)
    {
        if(currentBullets-bulletCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
