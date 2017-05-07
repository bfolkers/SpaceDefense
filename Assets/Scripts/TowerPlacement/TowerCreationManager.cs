using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreationManager : MonoBehaviour {

	public TowerCreationInputManager inputManager;
	public TranslucentManager translucentManager;
	public TowerSelectionManager towerSelector;
	public SolidTowerManager solidTowerManager;
	public QuatlooManager moneyManager;

	GameObject translucentTowerSelected;
	GameObject towerSelected;

	int selection = 0;
	int storedSelection = 0;
	public float cooldown = .5f;
	bool selectionChange = false;

	void Awake()
	{
		
	}


	void Update () {
		selection = inputManager.getSelection ();
		if (selection != storedSelection) 
		{
			selectionChange = true;
			storedSelection = selection;
		}

		if (selectionChange) 
		{
			
			if (selection == 0) 
			{
				translucentManager.destroyTranslucent ();
				selectionChange = false;
			} else {

				if (towerSelector.getTowerCost (selection) <= moneyManager.GetBalance ()) {
					
					translucentManager.destroyTranslucent ();
					translucentTowerSelected = towerSelector.getCurrentTranslucentTower (selection);
					translucentManager.createTranslucent (translucentTowerSelected);
					selectionChange = false;
				} else {
					inputManager.setSelection (0);
				}
			}
		}


		if (Input.GetMouseButton (0)) 
		{
			if (selection != 0) 
			{
				if (towerSelector.getTowerCost (selection) <= moneyManager.GetBalance()) {
					towerSelected = towerSelector.getCurrentTower (selection);
					if (solidTowerManager.createTowerSuccesfully (towerSelected)) {
						moneyManager.Withdraw(towerSelector.getTowerCost (selection));
						
						translucentManager.destroyTranslucent ();
						inputManager.setSelection (0);
					}
				}

			}
		}
	}

}
