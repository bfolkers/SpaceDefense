using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslucentManager : MonoBehaviour {

	public GridManager gridManager;
	Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);

	GameObject translucentTower;


	void Start () {
		
	}
	

	void Update () 
	{
		if (translucentTower != null) 
		{
			Vector3 moveToPoint = gridManager.findNearestGridPoint ();
			translucentTower.transform.position = moveToPoint;
		}
	}

	public void createTranslucent (GameObject tower)
	{
		Vector3 startPoint = gridManager.findNearestGridPoint ();
		translucentTower = Instantiate (tower, startPoint, rotation);
	}

	public void destroyTranslucent ()
	{
		if (translucentTower != null) 
		{
			Object.Destroy (translucentTower);
			translucentTower = null;
		}
	}
		
}
