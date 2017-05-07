using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeMoney : MonoBehaviour {

	public int diamondAmount = 5;
	public float perTimeInSeconds = 5f;
	public GameObject quatlooObject;
	QuatlooManager moneyManager;

	void Awake()
	{
		quatlooObject = GameObject.FindGameObjectWithTag ("QuatlooObject");
		moneyManager = quatlooObject.GetComponent<QuatlooManager> ();
	}

	void Start () {
		InvokeRepeating ("getDiamonds", perTimeInSeconds, perTimeInSeconds);
	}
		
	void getDiamonds(){
		moneyManager.depositDiamonds (diamondAmount);
	}
}
