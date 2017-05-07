using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


	public Manager manager;
	public GameObject env;
	public Text scoreText;
	public PlayerStats playerStats;

	private AudioSource music;

	public void Awake() 
	{
		music = env.GetComponent<AudioSource>();

	}

	public void Update()
	{
		if (manager.GameOver () == true) 
		{
			music.Stop ();
		}

		scoreText.text = "Score  " + playerStats.totalScore;
	}

}
