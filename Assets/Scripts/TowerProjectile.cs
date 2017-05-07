using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    public float projectileSpeed = 5f;
    public float projectileDamage = 10f;
    public float explosionRadius = 5f;
    private Vector3 target;
    private EnemyHealth enemyHealth;
    private Vector3 originalPosition;

    void Awake ()
    {
        originalPosition = transform.position;
    }

    public void Seek (Transform towerTarget, float attackDamage)
    {
        target = towerTarget.position;
        projectileDamage = attackDamage;
    }


    void Update () 
    {
        if (target == null)
        {
            Destroy (gameObject);
            return;
        }

        Vector3 direction = target - transform.position;
        float distanceThisFrame = projectileSpeed * Time.deltaTime;

        //	If the distance the bullet moves this frame 
        //	is greater than the actual distance to the target...
        if (direction.magnitude <= distanceThisFrame) {
            HitTarget ();
            return;
        }

        // Move and rotate towards target
        transform.Translate (direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget ()
    {
      // Check the projectiles's radius for enemies within range
      Collider[] colliders = Physics.OverlapSphere (transform.position, explosionRadius);
      foreach (Collider collider in colliders)
      {
          if (collider.CompareTag("Enemy"))
          {	
              enemyHealth = collider.GetComponent<EnemyHealth> ();
              enemyHealth.TakeDamage (projectileDamage);
          }
      }
      Destroy (gameObject);
    }
}
