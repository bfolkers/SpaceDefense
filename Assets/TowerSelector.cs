using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerSelector : MonoBehaviour {
	public PlacedTowerManager placedTowers;
	public TowerCreationInputManager inputManager;
	public GridManager gridManager;
	public SolidTowerManager solidTowerManager;
	public UpgradeManager upgradeManager;

	public Text sellText;
	public Text upgradeText;
	public Text healText;

	public Text diamondUpgradeText;

	public TowerHealth towerHealthClass;

	int towerSellPrice;
	int towerUpgradePrice;
	int towerHealPrice;
	int towerDiamondUpgradePrice;

	bool updateTexts = false;


	public Vector3 selectedTowerLocation;
//	public Canvas canvas;
//	RectTransform canvasRect;
	public GameObject sell;
//	RectTransform sellTransform;
	public GameObject upgrade;
	public GameObject diamondUpgrade;
//	RectTransform upgradeTransform;
	public GameObject heal;
	Button sellButton;
	Button upgradeButton;
	Button healButton;

//	bool buttonBeingClicked = false;

	void Start () {
//		canvasRect = canvas.GetComponent<RectTransform>();
//		sellTransform = sell.GetComponent<RectTransform> ();
//		upgradeTransform = upgrade.GetComponent<RectTransform> ();
		sellButton = sell.GetComponent<Button> ();
		upgradeButton = upgrade.GetComponent<Button> ();
		healButton = heal.GetComponent<Button> ();
		sell.SetActive (false);
		upgrade.SetActive (false);
		heal.SetActive (false);
		diamondUpgrade.SetActive (false);

	}

	void Update()
	{
		
		if (sell.activeSelf) {
			
			towerSellPrice = placedTowers.retrieveSpecificTower (selectedTowerLocation).GetComponent<TowerInfo> ().getTowerSellPrice ();
			towerUpgradePrice = placedTowers.retrieveSpecificTower (selectedTowerLocation).GetComponent<TowerInfo> ().getUpgradeCost ();
			towerHealthClass = placedTowers.retrieveSpecificTower (selectedTowerLocation).GetComponent<TowerHealth> ();
			towerHealPrice = Mathf.FloorToInt (towerHealthClass.getStartingHealth () - towerHealthClass.getCurrentHealth ());
			towerDiamondUpgradePrice = placedTowers.retrieveSpecificTower (selectedTowerLocation).GetComponent<TowerInfo> ().getDiamondUpgradeCost ();
		}
		
		if (updateTexts) {
			sellText.text = "+" + towerSellPrice.ToString ();
			upgradeText.text = "-" + towerUpgradePrice.ToString ();
			healText.text = "-" + towerHealPrice.ToString ();
			diamondUpgradeText.text = "D   -" + towerDiamondUpgradePrice.ToString ();
		} 


		if (Input.GetMouseButton (0) && solidTowerManager.getPermissions ()) {
			Vector3 nearestPoint = gridManager.findNearestGridPoint ();
			if (placedTowers.towerExistsHere (nearestPoint)) {
				selectedTowerLocation = nearestPoint;
				sell.SetActive (true);
				updateTexts = true;

				if (!(towerUpgradePrice <= 0f)) {
					upgrade.SetActive (true);

				} else {
					upgrade.SetActive (false);

				}



				if (!(towerHealPrice <= 0f)) {
					heal.SetActive (true);
				} else {
					heal.SetActive (false);
				}

			} 
		} 
		
		if (inputManager.allowDelete () && placedTowers.towerExistsHere(selectedTowerLocation)) 
		{
			sell.SetActive (false);
			upgrade.SetActive (false);
			updateTexts = false;
			placedTowers.removeExistingTower (selectedTowerLocation, true);
			inputManager.dissAllowDelete ();

		}

		if (inputManager.allowUpgrade() && placedTowers.towerExistsHere(selectedTowerLocation)) 
		{
			updateTexts = true;
			upgradeManager.upgradeTower (selectedTowerLocation);
			inputManager.dissAllowUpgrade ();
		}

		if (inputManager.allowHeal() && placedTowers.towerExistsHere(selectedTowerLocation)) 
		{
			updateTexts = true;
			upgradeManager.healTower (selectedTowerLocation);
			inputManager.dissAllowHeal ();
		}

		if (towerHealPrice == 0) {
			heal.SetActive (false);
		} else {
			if (sell.activeSelf) {
				heal.SetActive (true);
			}
		}
			
		if (towerUpgradePrice == 0) {
			upgrade.SetActive (false);
		} 

		if (towerDiamondUpgradePrice == 0) {
			diamondUpgrade.SetActive (false);
		} else {
			if (sell.activeSelf) {
				diamondUpgrade.SetActive (true);
			}
		}
	}
}