using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Enemy
{
  public class EnemyAttack : MonoBehaviour
  {
    [Header("Attributes")]
    public float range = 5f;
    public float timeBetweenAttacks = 2f;     // The time in seconds between each attack.
    public int attackDamage = 10;               // The amount of health taken away per attack.

    [Header("Projectiles")]
    public bool canShoot;
    public GameObject firingParticlePrefab;
    private ParticleSystem firingParticles;
    public Transform firePoint;
    NavMeshAgent navMeshAgent;
    Animator anim;                              // Reference to the animator component.
    GameObject tower;                          // Reference to the tower GameObject.
	Vector3 towerPosition;
    TowerHealth towerHealth;                  // Reference to the tower's health.
	PlacedTowerManager placedTowers;
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    EnemyMovement enemyMovement;
    bool towerInRange;                         // Whether tower is within the trigger collider and can be attacked.
		GameObject playerBase;
		TowerHealth playerBaseHealth;
    float timer;                                // Timer for counting up to the next attack.
//		Transform target;
    public AudioSource hitTower;
		public bool attackDelay = false;

		Manager manager;

    void Awake ()
    {
      // Setting up the references.
      if (canShoot)
      {
          firingParticles = Instantiate(firingParticlePrefab).GetComponent<ParticleSystem> ();
          firingParticles.gameObject.SetActive (false);
      }
		manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
		float randomNumber = UnityEngine.Random.Range (0f, (float)manager.getRoundNumber()/2f) ;
		attackDamage += Mathf.FloorToInt(randomNumber);
		playerBase = GameObject.FindGameObjectWithTag ("PlayerBase");
		playerBaseHealth = playerBase.GetComponent<TowerHealth> ();
      enemyMovement = GetComponent<EnemyMovement> ();
      enemyHealth = GetComponent<EnemyHealth> ();
      navMeshAgent = GetComponent<NavMeshAgent> ();
      anim = GetComponent <Animator> ();
		hitTower = GetComponent <AudioSource> ();	

    }
		void Start() 
		{
			placedTowers = GameObject.FindGameObjectWithTag ("placedTowers").GetComponent<PlacedTowerManager> ();
		}


    void OnTriggerEnter (Collider other)
    {
      // If the entering collider is the tower...
      if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerBase"))
      {
        // ... the tower is in range.
        tower = other.gameObject;
				towerPosition = tower.transform.position;
        towerInRange = true;
        towerHealth = tower.GetComponent <TowerHealth> ();
      }
    }

    void OnTriggerExit (Collider other)
    {
      if (other.gameObject == tower) 
      {
          towerInRange = false;
      }
    }


    void Update ()
    {

			if (enemyHealth.currentHealth <= 0f) {
				return;
			}
//			target = enemyMovement.getCurrentTarget ();
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks and tower is in range...
			if (timer >= timeBetweenAttacks && towerInRange && (playerBaseHealth.getCurrentHealth() > 0 )) {
				
            // ... attack.
            navMeshAgent.isStopped = true;
            anim.SetBool ("IsRunning", false);
            Attack ();
            // If the tower has zero or less health...
				if(!placedTowers.nonRubbleTowerExistsHere(towerPosition) && (tower != playerBase && playerBaseHealth.getCurrentHealth() > 0))
            {
					
                if (canShoot)
                {
                    firingParticles.gameObject.SetActive (false);
                }
                navMeshAgent.isStopped = false;
                anim.SetBool ("IsRunning", true);
                // ... tell the animator the tower is dead.
                //  anim.SetTrigger ("towerDead");
            }
		} 
				
    }


    void Attack ()
    {
			if (placedTowers.nonRubbleTowerExistsHere (towerPosition) || (tower == playerBase && playerBaseHealth.getCurrentHealth() > 0)) {
				transform.LookAt (tower.transform);
				anim.SetTrigger ("Attacking");
				if (hitTower != null) {
					hitTower.PlayDelayed (.45f);
				}
			

				// Reset the timer.
				timer = 0f;

				if (canShoot) {
					firingParticles.transform.position = firePoint.position;
					firingParticles.transform.LookAt (tower.transform);
					firingParticles.gameObject.SetActive (true);
				}

				// If the tower has health to lose...
	      
				if (attackDelay) {
					Invoke ("doDamage", 0.5f);
				} else {
					doDamage ();
				}
			} 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
	
	void doDamage()
	{
			if(placedTowers.nonRubbleTowerExistsHere(towerPosition) || (tower == playerBase && playerBaseHealth.getCurrentHealth() > 0))
		{
			// ... damage the tower.
			towerHealth.TakeDamage (attackDamage);
		}

	}

  }


}

