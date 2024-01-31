using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    public bool TestMode;

    public float projectileSpeed = 7.0f;
    
    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public Projectile projectilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (projectileSpeed <= 0)
        {
            projectileSpeed = 7.0f;
            if (TestMode) Debug.Log("Projectile speed less than 0! Defaulted to 7.0f.");
        }
        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            if (TestMode) Debug.Log("Set default values Shoot script. On Object " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Fire()
    {
        // Where the issue is 
        if (!sr.flipX)
        {
            Projectile curPurjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curPurjectile.speed = projectileSpeed;
        }
        else
        {
            Projectile curPurjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointRight.rotation);
            curPurjectile.speed = -projectileSpeed;
        }
    }
}
