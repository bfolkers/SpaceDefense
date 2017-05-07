using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectionManager : MonoBehaviour {

	public GameObject tower1;
	public GameObject tower2;
	public GameObject tower3;
	public GameObject wall;

	public GameObject tower1Trans;
	public GameObject tower2Trans;
	public GameObject tower3Trans;
	public GameObject wallTrans;

	GameObject currentTowerSelected;
	GameObject currentTowerTranslucent;

	public void changeSelection(int selection)
	{
		if (selection == 1) 
		{
			currentTowerSelected = tower1;
			currentTowerTranslucent = tower1Trans;
		}
		if (selection == 2) 
		{
			currentTowerSelected = tower2;
			currentTowerTranslucent = tower2Trans;
		}
		if (selection == 3) 
		{
			currentTowerSelected = tower3;
			currentTowerTranslucent = tower3Trans;
		}
		if (selection == 4) 
		{
			currentTowerSelected = wall;
			currentTowerTranslucent = wallTrans;
		}
	}

	public GameObject getCurrentTower(int selection)
	{
		changeSelection (selection);
		return currentTowerSelected;
	}

	public GameObject getCurrentTranslucentTower(int selection)
	{
		changeSelection (selection);
		return currentTowerTranslucent;
	}
	public int getTowerCost (int selection)
	{
		changeSelection (selection);
		TowerInfo towerInfo = currentTowerSelected.GetComponent<TowerInfo> ();
		int towerCost = towerInfo.getTowerCost ();
		return towerCost;
	}

}
