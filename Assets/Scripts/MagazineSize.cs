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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        // Initialize currentBullets to maxBullets at start
        currentBullets = maxBullets;
        magazineFull = currentBullets == maxBullets;
        StartCoroutine(bulletLoad());
    }

    private void Update()
    {
       // IncreaseReloadFrequency();
    }

    private void IncreaseReloadFrequency()
    {
        float elapsedTime = Time.realtimeSinceStartup;
        if ((int)elapsedTime % 20 == 0 && elapsedTime > 0) 
        {
            Debug.Log("Load speed increased.");
            bulletLoadFrequency *= 1.3f;
        }
    }

    IEnumerator bulletLoad()
    {
        while (true)
        {
            yield return new WaitForSeconds(2 / bulletLoadFrequency);
            
            if (!magazineFull)
            {
                currentBullets++;
                if (currentBullets >= maxBullets)
                {
                    currentBullets = maxBullets;
                    magazineFull = true;
                }
            }
        }
    }

    public bool canShoot(int bulletCount)
    {
        return currentBullets - bulletCount > 0;
    }

    public void RemoveBullets(int bulletCount){
        currentBullets -= bulletCount;
        if(currentBullets <= 0){
            currentBullets = 0;
        }
        magazineFull = false;
    }

    public int getCurrentBullets(){
        return currentBullets;
    }

    public void AddBullets(int bulletCount){
        currentBullets += bulletCount;
        if(currentBullets > maxBullets){
            currentBullets = maxBullets;
        }
        magazineFull = true;
    }
}
