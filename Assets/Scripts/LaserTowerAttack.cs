using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerAttack : MonoBehaviour
{
    [Header("Attributes")]
    public float attackRange = 5f;     // Turret attack range
    public float attackDamageRate = 1f; // Rate that the laser ticks.
    public float attackDamage = 2f;    // Attack Damage
    public float rotationSpeed = 10f;
    public Transform firePoint;         // Laser firepoint
    public LineRenderer lineRenderer;   // Laser line renderer
    public Transform currentTarget;     // Current target
    public GameObject laserParticlePrefab;
	public AudioSource fireSound;
	public AudioClip laserFire;
	public bool isPlayingSound = false;
    private ParticleSystem laserParticles;
	TowerHealth towerHealth;

    GameObject enemy;
    EnemyHealth enemyHealth;            // Enemy's health
    EnemyMovement enemyMovement;
    Transform hitPoint;

    bool inRange;

    void Awake ()
    {
//		towerHealth = GetComponent<TowerHealth> ();
		fireSound = GetComponent<AudioSource> ();
        laserParticles = Instantiate(laserParticlePrefab).GetComponent<ParticleSystem> ();
        laserParticles.gameObject.SetActive (false);
        lineRenderer = GetComponent<LineRenderer> ();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
		laserParticles.transform.position = firePoint.position; 
    }

    void Update ()
    {
        if (currentTarget == null || Vector3.Distance(transform.position, currentTarget.position) > attackRange)
        {
            if (lineRenderer.enabled == true)
            {
                lineRenderer.enabled = false;
                laserParticles.gameObject.SetActive(false);
                isPlayingSound = false;
                fireSound.Stop ();
            }
            return;
        }

        FireLaser ();
    }

	public void destroyLaser()
	{
		Object.Destroy (laserParticles);
	}

    void FireLaser ()
    {
        if (lineRenderer.enabled != true)
        {
            lineRenderer.enabled = true;
            if (isPlayingSound == false) {
                isPlayingSound = true;
                fireSound.Play ();
            }
        }
        laserParticles.transform.LookAt(hitPoint);
        laserParticles.gameObject.SetActive (true);

        lineRenderer.SetPosition (0, firePoint.position);
        lineRenderer.SetPosition (1, hitPoint.position);
        
        enemyHealth.TakeDamage (attackDamage);

        if (enemyHealth.currentHealth <= 0f)
        {
            laserParticles.gameObject.SetActive (false);
            lineRenderer.enabled = false;
            isPlayingSound = false;
            fireSound.Stop ();
        }
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
      if (closestEnemy != null && shortestDistance <= attackRange)
      {
        currentTarget = closestEnemy.transform;
        enemyHealth = currentTarget.GetComponent<EnemyHealth> ();
        enemyMovement = currentTarget.GetComponent<EnemyMovement> ();
        hitPoint = enemyMovement.eyes;


      } else {
        currentTarget = null;
      }
    }

    void OnDrawGizmos()
    {
       Gizmos.color = Color.cyan; 
       Gizmos.DrawWireSphere (transform.position, attackRange);
    }
}
