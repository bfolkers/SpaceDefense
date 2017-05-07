using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAwake : MonoBehaviour {

	public GameObject sellButton;
	public GameObject upgradeButton;

	void Awake() 
	{
		sellButton.SetActive(false);
		upgradeButton.SetActive(false);
	}
}
