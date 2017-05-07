using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
	public float startingHealth = 100f;            // The amount of health the enemy starts the game with.
	public float currentHealth;                   // The current health the enemy has.
	public float sinkSpeed = 1f;              	// The speed at which the enemy sinks through the floor when dead.
	public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
	public AudioClip deathClip;                 // The sound to play when the enemy dies.
	public int killBonus;
	public GameObject quatlooObject;
	public QuatlooManager quatlooManager;
	public GameObject energyCore;
	public PlayerStats playerStats;
	TowerHealth coreHealth;
	Manager manager;

  	[Header("Health UI")]
  	public Image healthFillImage;
  	public Canvas healthBar;
  	public Color fullHealthColor = Color.green;
  	public Color zeroHealthColor = Color.red;

	Animator anim;                              // Reference to the animator.
	AudioSource enemyAudio;                     // Reference to the audio source.
	ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
	CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
	bool isDead;                                // Whether the enemy is dead.
	bool isSinking;                             // Whether the enemy has started sinking through the floor.


	void Awake ()
	{
		// Setting up references.
		manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
		float randomNumber = UnityEngine.Random.Range (0f, (float)manager.getRoundNumber());
		startingHealth += Mathf.FloorToInt(randomNumber * 5f);
//		killBonus -= Mathf.FloorToInt((int)manager.getRoundNumber () / 2.5f);
//		if (killBonus < 0) {
//			killBonus = 0;
//		}

		int roundNumber = manager.getRoundNumber();
		if (roundNumber >= 3 && roundNumber <= 12) {
			killBonus -= 1;
			if (roundNumber >= 5) {
				killBonus -= 2;
				if (roundNumber >= 7) {
					killBonus -= 1;
					killBonus = killBonus - (JwtGetter.difficulty + 1);
					if (roundNumber >= 9) {
						killBonus -= 2;
						killBonus = killBonus / (JwtGetter.difficulty + 1);
					}
				}
			}
		}
		if (roundNumber >= 13) {
			killBonus = killBonus / (3 * (JwtGetter.difficulty + 1));
		}
		if (roundNumber >= 17) {
			killBonus = killBonus / (9 * (JwtGetter.difficulty + 1));
		} 
		quatlooObject = GameObject.FindGameObjectWithTag("QuatlooObject");
		quatlooManager = quatlooObject.GetComponent<QuatlooManager> ();
		anim = GetComponent <Animator> ();
		coreHealth = energyCore.GetComponent<TowerHealth> ();
		enemyAudio = GetComponent <AudioSource> ();
		hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();

		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
    	SetHealthUI ();
	}

	void Update ()
	{
		// If the enemy should be sinking...
		if(isSinking)
		{
			// ... move the enemy down by the sinkSpeed per second.
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}


	public void TakeDamage (float amount/*, Vector3 hitPoint*/)
	{
    	anim.SetTrigger ("Hurt");
		// If the enemy is dead...
		if (isDead)
			// ... no need to take damage so exit the function.
			return;

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

    
    	SetHealthUI ();

		// If the current health is less than or equal to zero...
		if (currentHealth <= 0f && !isDead)
		{
			// ... the enemy is dead.
			Death ();
		}
	}

  private void SetHealthUI ()
  {
      if (currentHealth != startingHealth)
      {
          healthBar.enabled = true;
          healthFillImage.fillAmount = currentHealth / startingHealth;
          healthFillImage.color = Color.Lerp (zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
      } 
	  else
      {
          healthBar.enabled = false;
      }
  }


	void Death ()
	{
    	if (anim.GetBool ("IsRunning"))
        	anim.SetBool ("IsRunning", false);

		// The enemy is dead.
		isDead = true;

		// Give the player a quatloo bonus when killing the enemy
		quatlooManager.Deposit(killBonus);

		// Turn the collider into a trigger so shots can pass through it.
		capsuleCollider.isTrigger = true;

    	// Find and disable the Nav Mesh Agent.
		GetComponent <NavMeshAgent> ().enabled = false;

		// Tell the animator that the enemy is dead.
		anim.SetTrigger ("Dead");

    	Invoke("StartSinking", 1f);

		// Increase the kills by one
		++playerStats.enemyKills;

		// Increase the score by the enemy's score value.
//		if (coreHealth.currentHealth <= 0f)
		playerStats.totalScore += 5 * (JwtGetter.difficulty + 1) ;

//		 (playerStats.enemyKills);
	}


	public void StartSinking ()
	{
		// Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
		GetComponent <Rigidbody> ().isKinematic = true;

		// The enemy should now sink.
		isSinking = true;

		// After 5 seconds destroy the enemy.
		Destroy (gameObject, 0.5f);
	}
}