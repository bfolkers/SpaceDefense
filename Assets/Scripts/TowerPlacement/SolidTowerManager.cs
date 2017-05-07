using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidTowerManager : MonoBehaviour {
	
	Quaternion rotation = Quaternion.Euler(0f,0f,0f);

	public GridManager gridManager;
	public PlacedTowerManager placedTowerManager;
	bool justCreated = false;

	float cooldown = 0.5f;

	
	public bool createTowerSuccesfully(GameObject tower)
	{
		Vector3 location = gridManager.findNearestGridPoint ();
		if (!placedTowerManager.towerExistsHere (location) && gridManager.insideGrid()) 
		{
			GameObject newTower = Instantiate (tower, location, rotation);
			placedTowerManager.pushNewTower (newTower);
			justCreated = true;
			StartCoroutine (resetPermission());
			return true;
		}
		return false;
	}

	IEnumerator resetPermission()
	{
		yield return new WaitForSeconds(cooldown);
		justCreated = false;
	}
		
	public bool getPermissions()
	{
		return !justCreated;
	}
}
