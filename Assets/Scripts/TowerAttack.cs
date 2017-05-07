using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour {

  [Header("Attributes")]
	public float range = 5f;
	public float timeBetweenAttacks = 2f;     // The time in seconds between each attack.
	public int attackDamage = 10;               // The amount of health taken away per attack.
	public Transform currentTarget;
  public GameObject projectilePrefab;
  public Transform firePoint;
  public Transform pivotPoint;
  public float rotationSpeed = 10f;
	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	TowerHealth towerHealth;
	GameObject enemy;
	// bool enemyInRange;                         // Whether enemy is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.
	public AudioSource fireProjectile;

	void Awake() 
	{
		fireProjectile = GetComponent<AudioSource> ();
		  towerHealth = GetComponent<TowerHealth> ();
      InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

  void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		float shortestDistance = Mathf.Infinity;
		GameObject closestEnemy = null;

		// Search all objects marked enemy
		foreach (GameObject enemy in enemies)
		{
			// Find closest one
			float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

			if (enemyDistance < shortestDistance)
			{
				shortestDistance = enemyDistance;
				closestEnemy = enemy;
			}
		}
		// Check if within range
		if (closestEnemy != null && shortestDistance <= range)
		{
			currentTarget = closestEnemy.transform;
      enemyHealth = currentTarget.GetComponent<EnemyHealth> ();
      pivotPoint.LookAt(currentTarget);
		} else {
			currentTarget = null;
		}
	}

	// void OnTriggerEnter (Collider other)
	// {
	// 	// If the entering collider is the enemy...
	// 	if(other.gameObject.CompareTag("Enemy"))
	// 	{
	// 		// ... the enemy is in range.

	// 		enemy = other.gameObject;
	// 		currentTarget = enemy.transform;

	// 		enemyInRange = true;
	// 		enemyHealth = enemy.GetComponent <EnemyHealth> ();
	// 	}
	// }

	// void OnTriggerExit (Collider other)
	// {
	// 	if (other.gameObject == enemy) 
	// 	{

	// 		enemyInRange = false;
	// 	}
	// }
	
	// Update is called once per frame
	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

    if (currentTarget == null)    // If no target, do nothing.
        return;

			// If the timer exceeds the time between attacks and the enemy is within range...
			if (timer >= timeBetweenAttacks && Vector3.Distance(transform.position, currentTarget.position) < range)
			{
				// ... attack.
				Fire ();

				// // If the tower has zero or less health...
				// if (enemyHealth.currentHealth <= 0f)
				// {

				// 	// Search for a new target in range.
				// 	ScanForEnemies ();
				// }
			}
	}

    void LockOnTarget()
    {
        // Get direction of target
        Vector3 direction = currentTarget.position - transform.position;

        // Get rotation to target
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Smooth the rotation
        Vector3 rotation = Quaternion.Lerp(pivotPoint.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;

        // Rotate turret on y-axis toward target
        pivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

	void ScanForEnemies ()
	{
		// Check the tower's radius for enemies within range
		Collider[] colliders = Physics.OverlapSphere (transform.position, range);
		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{	
				currentTarget = collider.transform;
				break;
			}
		}
	}

	void Fire () 
	{
		timer = 0f;

		if (enemyHealth.currentHealth > 0f) 
		{   
        GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        TowerProjectile projectile = projectileGO.GetComponent<TowerProjectile> ();
        projectile.Seek (currentTarget, attackDamage);
			fireProjectile.Play ();
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan; 
		Gizmos.DrawWireSphere (transform.position, range);
	}
}
