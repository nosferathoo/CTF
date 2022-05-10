using System;
using Lean.Pool;
using UnityEngine;
using NaughtyAttributes;
public class Bullet : LeanPooledRigidbody
{
    private const float ImpactEffectTTL = 3f;
    [SerializeField] [Tag] private string obstacleTag, enemyTag;
    [SerializeField] private GameObject obstacleImpactEffect, enemyImpactEffect;


    private void SpawnImpactEffect(GameObject effect, ContactPoint cp, Transform parent = null)
    {
        var obj = LeanPool.Spawn(effect, cp.point, Quaternion.LookRotation(cp.normal), parent);
        LeanPool.Despawn(obj,ImpactEffectTTL);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var cp = collision.contacts[0];

        if (collision.collider.CompareTag(obstacleTag))
        {
            SpawnImpactEffect(obstacleImpactEffect, cp);
        }
        else if (collision.collider.CompareTag(enemyTag))
        {
            SpawnImpactEffect(enemyImpactEffect, cp, collision.transform);
        }

        LeanPool.Despawn(gameObject);
    }
    
    
}
