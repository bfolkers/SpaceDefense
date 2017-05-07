using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

	public GameObject tower1Level2;
	public GameObject tower1Level3;
	public GameObject tower2Level2;
	public GameObject tower2Level3;
	public GameObject tower3Level2;
	public GameObject tower3Level3;
	public GameObject destroyedTower;

	public QuatlooManager moneyManager;
	int upgradeCost;
	int diamondUpgradeCost;
	TowerInfo towerInfo;
	TowerHealth towerHealth;



	GameObject[,] allUpgrades;


	void Start()
	{
		allUpgrades = new GameObject[3,2];

		allUpgrades [0, 0] = tower1Level2;
		allUpgrades [0, 1] = tower1Level3;
		allUpgrades [1, 0] = tower2Level2;
		allUpgrades [1, 1] = tower2Level3;
		allUpgrades [2, 0] = tower3Level2;
		allUpgrades [2, 1] = tower3Level3;
	}

	
	public PlacedTowerManager placedTowers;

	public void upgradeTower (Vector3 towerLocation)
	{
		GameObject tower = placedTowers.retrieveSpecificTower (towerLocation);
		int towerType = tower.GetComponent<TowerInfo> ().getTowerType ();
		int towerLevel = tower.GetComponent<TowerInfo> ().getTowerLevel ();
		if (towerLevel < 3) 
		{
			GameObject towerUpgrade = allUpgrades[towerType-1, towerLevel-1];
			towerInfo = towerUpgrade.GetComponent<TowerInfo> ();
			upgradeCost = towerInfo.getTowerCost ();
			diamondUpgradeCost = towerInfo.getDiamondCost ();
			if (upgradeCost < moneyManager.GetBalance () && (diamondUpgradeCost <= moneyManager.GetDiamondBalance())) 
			{
				moneyManager.Withdraw (upgradeCost);
				moneyManager.withdrawDiamonds (diamondUpgradeCost);
				GameObject upgradedTower = Instantiate(towerUpgrade, towerLocation, Quaternion.Euler(0f,0f,0f));
				placedTowers.removeExistingTower (towerLocation);
				placedTowers.pushNewTower (upgradedTower);
			}

		}

	}

	public void healTower(Vector3 towerLocation)
	{
		GameObject tower = placedTowers.retrieveSpecificTower (towerLocation);
		towerHealth = tower.GetComponent<TowerHealth> ();
		int costToHeal = Mathf.FloorToInt (towerHealth.getStartingHealth () - towerHealth.getCurrentHealth ());
		moneyManager.Withdraw (costToHeal);
		towerHealth.healTower ();

	}

	public void destroyTower(Vector3 towerLocation)
	{
//		GameObject tower = placedTowers.retrieveSpecificTower (towerLocation);
		GameObject towerRubble = Instantiate (destroyedTower, towerLocation, Quaternion.Euler (0f, 0f, 0f));
		placedTowers.removeExistingTower (towerLocation);
		placedTowers.pushNewTower (towerRubble);
	}
}
