using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	public Transform enemyPrefab;
	public Transform Gromm;
	public Transform redGromm;
  	public int numberOfEnemies;
  	public float timeBetweenWaves;
  	public float spawnRate;
	public List<Transform> enemies;
  	private float timer = 2f;
	private bool hasSpawned = false;
	
	public void SpawnWave (int numberEnemies, float rate)
  	{
      for (int i = 0; i < numberEnemies; i++)
      {
			Invoke("SpawnEnemy", Random.Range(0f, rate));
      }
  	}
	public void SpawnGromm (int numberGromm)
	{
		for (int i = 0; i < numberGromm; i++) 
		{
			Invoke ("SpawnGromm", Random.Range (6f, 12f));
		}
	}
	public void SpawnRedGromm (int numberGromm)
	{
		for (int i = 0; i < numberGromm; i++) 
		{
			Invoke ("SpawnRedGromm", Random.Range (8f, 14f));
		}
	}

  void SpawnEnemy ()
  {
		// Get a random point on the wave spawner
		Vector3 randomPoint = new Vector3(Random.Range (-22f, 22f), transform.position.y, transform.position.z + Random.Range(-8f, 8f));
		// Create enemy at that random point
		Instantiate(enemyPrefab, randomPoint, transform.rotation);
  }
	void SpawnGromm ()
	{
		Vector3 randomPoint = new Vector3 (Random.Range (-22f, 22f), transform.position.y, transform.position.z);
		Instantiate (Gromm, randomPoint, transform.rotation);
	}
	void SpawnRedGromm ()
	{
		Vector3 randomPoint = new Vector3 (Random.Range (-22f, 22f), transform.position.y, transform.position.z);
		Instantiate (redGromm, randomPoint, transform.rotation);
	}
}
