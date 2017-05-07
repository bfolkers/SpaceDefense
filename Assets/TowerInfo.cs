using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfo : MonoBehaviour {
	public int towerType;
	public int towerLevel;
	public int towerCost;
	public int sellForAmt;
	public int upgradeCost;
	public bool isNotRubble = true;

	public int diamondCost;
	public int diamondUpgradeCost;



	public int getTowerType()
	{
		return towerType;
	}
	public int getTowerLevel()
	{
		return towerLevel;
	}
	public int getTowerCost()
	{
		return towerCost;
	}
	public int getUpgradeCost()
	{
		return upgradeCost;
	}
	public int getTowerSellPrice()
	{
		TowerHealth towerHealth = GetComponent<TowerHealth> ();
		float currentHealth = towerHealth.getCurrentHealth ();
		float maxHealth = towerHealth.getStartingHealth ();
		float healthPercent = currentHealth / maxHealth;
		int sellAmount = (int)(healthPercent * sellForAmt);
		return sellAmount;
	}

	public int getDiamondCost()
	{
		return diamondCost;
	}
	public int getDiamondUpgradeCost()
	{
		return diamondUpgradeCost;
	}
}
