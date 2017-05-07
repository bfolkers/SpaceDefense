using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
	public Transform startPosition;
	public float StartDelay = 3f;         
	public float EndDelay = 3f;
	public float startingTime;
	public float endingTime;
	public float playTime;
	public Text waveText;
	public GameObject energyCore;
	public WaveSpawner waveSpawner;
	public GameObject waveZoom;
	public PlayerStats playerStats;
	public Animator anim;

	private int roundNumber;              
	private WaitForSeconds StartWait;     
	private WaitForSeconds EndWait;
	private PlayerInfoPost playerInfo;
	public HighScoreSubmission scoreSubmission;
	TowerHealth coreHealth;


	private void Start()
	{	
		startPosition = waveZoom.transform;
		startingTime = Time.time;
		StartWait = new WaitForSeconds(StartDelay);
		EndWait = new WaitForSeconds(EndDelay);
		coreHealth = energyCore.GetComponent<TowerHealth> ();

		playerStats.enemyKills = 0;
		playerStats.totalScore = 0;
		Debug.Log (JwtGetter.difficulty);
		roundNumber = 1;

		StartCoroutine(GameLoop());
	}
		
	private IEnumerator GameLoop()
	{
		yield return StartCoroutine(RoundStarting());
		yield return StartCoroutine(RoundPlaying());
		yield return StartCoroutine(RoundEnding());

		if (GameOver() == true)
		{
			endingTime = Time.time;
			playTime = endingTime - startingTime;
			playerInfo = new PlayerInfoPost ();
			playerInfo.duration = (int)playTime;
			playerInfo.difficulty = JwtGetter.difficulty;
			playerInfo.score = playerStats.totalScore;
			playerInfo.kills = playerStats.enemyKills;

			scoreSubmission.HighScoreSubmit (playerInfo);
		}
		else
		{
			++roundNumber;
			StartCoroutine(GameLoop());
		}
	}
		
	private IEnumerator RoundStarting()
	{
		int numOfEnemies = (roundNumber * roundNumber * (3 + JwtGetter.difficulty)+ (16 + JwtGetter.difficulty * 2))/(8/ (JwtGetter.difficulty + 1));
		int numOfGromm = 0;
		int numOfRedGromm = 0;
		if (numOfEnemies > 150) 
		{
			numOfGromm = (numOfEnemies - 150) / 12;
			numOfEnemies = 150;
		}
		if (numOfGromm > 5) {
			numOfRedGromm = (numOfGromm - 5) / 2;
			numOfGromm = 5;
		}
		waveSpawner.SpawnWave(numOfEnemies, roundNumber);

		if (roundNumber % 3 == 0 || roundNumber >= 8) 
		{
			waveSpawner.SpawnGromm ((roundNumber / 3) + numOfGromm);
		}
		if (roundNumber % 7 == 0 || roundNumber >= 10) 
		{
			waveSpawner.SpawnRedGromm ((roundNumber / 7) + numOfRedGromm);
		}


		waveText.text = "Wave " + roundNumber;

		yield return StartWait;


	}
		
	private IEnumerator RoundPlaying()
	{
		while (!NoEnemiesLeft() && !GameOver())
		{
			yield return null;
		}
	}
		
	private IEnumerator RoundEnding() 
	{
		int roundBonus = (1 + (int)Mathf.Floor(Mathf.Log (roundNumber))) * 5;
		yield return EndWait;
	}
		
	private bool NoEnemiesLeft()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
						
		return enemies.Length <= 2;
	}

	public bool GameOver()
	{
		return coreHealth.currentHealth <= 0f;
	}
	public int getRoundNumber() 
	{
		return roundNumber;
	}
}


