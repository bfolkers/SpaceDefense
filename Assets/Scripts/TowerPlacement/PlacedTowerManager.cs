using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedTowerManager : MonoBehaviour {
	public QuatlooManager moneyManager;
	TowerInfo towerInfo;
	TowerHealth towerHealth;

	List <Vector3> towerLocations = new List<Vector3>();
	List <GameObject> existingTowers = new List<GameObject>();

	public void pushNewTower(GameObject tower)
	{
		towerLocations.Add (tower.transform.position);
		existingTowers.Add (tower);
	}
	public List<Vector3> retrieveTowerLocations()
	{
		return towerLocations;
	}
	public List<GameObject> retrieveTowers()
	{
		return existingTowers;
	}

	public GameObject retrieveSpecificTower(Vector3 towerLocation)
	{
		GameObject towerToFind = null;
		foreach (GameObject tower in existingTowers) 
		{
			if (tower.transform.position == towerLocation)
			{
				towerToFind = tower;
			}
		}
		return towerToFind;
	}

	public void removeExistingTower (GameObject tower)
	{
		
		existingTowers.Remove (tower);
		towerLocations.Remove (tower.transform.position);
		Object.Destroy (tower);
	}
	public void removeExistingTower (Vector3 towerLocation, bool sell=false)
	{
		

		GameObject towerToFind = null;
		foreach (GameObject tower in existingTowers) 
		{
			if (tower.transform.position == towerLocation)
			{
				towerToFind = tower;
			}
		}

		if (towerToFind != null) 
		{
			if (sell) 
			{
				towerInfo = towerToFind.GetComponent<TowerInfo> ();
				moneyManager.Deposit (towerInfo.getTowerSellPrice());
			}
			existingTowers.Remove (towerToFind);
			Object.Destroy (towerToFind);
		}

		towerLocations.Remove (towerLocation);
	}



	public bool towerExistsHere(Vector3 towerLocation)
	{


		foreach (Vector3 location in towerLocations) 
		{
			if (location == towerLocation)
			{
				return true;
			}
		}
		return false;
	}
	public bool nonRubbleTowerExistsHere(Vector3 towerLocation)
	{
		if (towerExistsHere (towerLocation)) {
			GameObject tower = retrieveSpecificTower (towerLocation);
			TowerInfo towerInfo = tower.GetComponent<TowerInfo> ();
			return towerInfo.isNotRubble;
		} else {
			return false;
		}
	}

}
