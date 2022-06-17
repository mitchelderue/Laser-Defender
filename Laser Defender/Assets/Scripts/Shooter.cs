using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;


    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        // Start and stop firing
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        // Infinite loop on purpose
        while (true)
        {
            // Instantiatie projectiles
            GameObject instance = Instantiate(  projectilePrefab,
                                                transform.position,
                                                Quaternion.identity);

            // Enable the projectiles to hit the enemies
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Add movement to the projectiles

                rb.velocity = transform.up * projectileSpeed;
            }
            // Destroy projectiles after x seconds                                  
            Destroy(instance, projectileLifetime);

            if (useAI)
            {
                // Add our delay specified in firingRate to Enemies
                yield return new WaitForSeconds(GetRandomSpawnTime());
            }
            else
            {
                // Add our delay specified in firingRate to Player
                yield return new WaitForSeconds(baseFiringRate);
            }
        }
    }

    public float GetRandomSpawnTime()
    {
        float timeToNextProjectile = UnityEngine.Random.Range(baseFiringRate - firingRateVariance, 
                                        baseFiringRate + firingRateVariance);
        return Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
    }
}
