using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
	public float startingHealth = 100f;          
	public float currentHealth;  
	public Slider slider;                        
	public Image fillImage;                      
	public Color fullHealthColor = Color.green;  
	public Color zeroHealthColor = Color.red;
	public UpgradeManager destructionManager;
	public LaserTowerAttack towerAttack;
	GameObject upgradeManager;
//	public GameObject explosionPrefab;


//	private AudioSource explosionAudio;          
//	private ParticleSystem explosionParticles;   
	private bool dead;            


	private void Awake()
	{
		if (GetComponent<LaserTowerAttack> () != null) {
			towerAttack = GetComponent<LaserTowerAttack> ();
		}
		upgradeManager = GameObject.FindGameObjectWithTag ("UpgradeManager");
		destructionManager = upgradeManager.GetComponent<UpgradeManager> ();
//		explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
//		explosionAudio = explosionParticles.GetComponent<AudioSource>();

//		explosionParticles.gameObject.SetActive(false);
		currentHealth = startingHealth;
		dead = false;
	}


	private void OnEnable()
	{
		slider.value = 10000f;
		fillImage.color = Color.Lerp (zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
	}


	public void TakeDamage(float amount)
	{
		// Adjust the tower's current health, update the UI based on the new health and check whether or not the tower is dead.
		currentHealth -= amount;
		if (currentHealth <= 0f && !dead) {
			OnDeath ();
		} else {
			SetHealthUI ();
		}
	}


	private void SetHealthUI()
	{
		// Adjust the value and colour of the slider.
		if (dead == false) {
			slider.value = currentHealth;
			fillImage.color = Color.Lerp (zeroHealthColor, fullHealthColor, currentHealth / startingHealth);
		}
	}


	private void OnDeath()
	{
		// Play the effects for the death of the tower and deactivate it.
		dead = true;

		if (GetComponent<LaserTowerAttack> () != null) {
			towerAttack.destroyLaser ();
		}
//		explosionParticles.transform.position = transform.position;
//		explosionParticles.gameObject.SetActive (true);
//		explosionParticles.Play ();
//		explosionAudio.Play ();
//		Destroy(gameObject);
		destructionManager.destroyTower(transform.position);

	}

	public float getCurrentHealth()
	{
		return currentHealth;
	}
	public float getStartingHealth()
	{
		return startingHealth;
	}
	public void healTower()
	{
		currentHealth = startingHealth;
		SetHealthUI ();
	}
}