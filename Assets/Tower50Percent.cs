using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower50Percent : MonoBehaviour {

	public AudioSource alert;
	public bool isFifty = false;
	public TowerHealth towerHealth;
	public int count = 0;
	
	public void Awake()
	{
		alert = GetComponent<AudioSource> ();
		towerHealth = GetComponent<TowerHealth> ();
	}

	void Update () {
		if (isFifty == true && count == 0)
		{
			alert.Play();
			count++;
		}

		if (towerHealth.currentHealth < 500)
		{
			isFifty = true;
		}
	}
}
