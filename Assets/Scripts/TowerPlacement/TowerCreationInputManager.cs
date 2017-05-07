using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerCreationInputManager : MonoBehaviour 
{
	public Button tower1ButtonUI;
	public Button tower2ButtonUI;
	public Button tower3ButtonUI;
	public Button wallButtonUI;
	public Button cancelButtonUI;

	Button tower1Button;
	Button tower2Button;
	Button tower3Button;
	Button wallButton;
	Button cancelButton;

	public Button upgradeButtonUI;
	public Button deleteButtonUI;

	public GameObject upgradeButtonObj;
	public GameObject deleteButtonObj;

	public GameObject healButtonUI;
	public GameObject diamondButtonUI;


	Button upgradeButton;
	Button deleteButton;
	Button healButton;
	Button diamondButton;

	bool deleteButtonDown = false;
	bool upgradeButtonDown = false;
	bool healButtonDown = false;

//	bool hideTowerButtons = false;
//	bool cancelButtonDown = false;

	bool deleteOn = false;
	bool upgradeOn = false;
	bool healOn = false;
	int selection = 0;

//	bool buttonGettingClicked = false;

	void Start () 
	{
		tower1Button = tower1ButtonUI.GetComponent<Button>();
		tower2Button = tower2ButtonUI.GetComponent<Button>();
		tower3Button = tower3ButtonUI.GetComponent<Button>();
		wallButton = wallButtonUI.GetComponent<Button> ();
		cancelButton = cancelButtonUI.GetComponent<Button> ();

		upgradeButton = upgradeButtonUI.GetComponent<Button> ();
		deleteButton = deleteButtonUI.GetComponent<Button> ();
		healButton = healButtonUI.GetComponent<Button> ();
		diamondButton = diamondButtonUI.GetComponent<Button> ();


		tower1Button.onClick.AddListener (Tower1ButtonClick);
		tower2Button.onClick.AddListener (Tower2ButtonClick);
		tower3Button.onClick.AddListener (Tower3ButtonClick);
		wallButton.onClick.AddListener (wallButtonClick);
		cancelButton.onClick.AddListener (cancelButtonClick);

		upgradeButton.onClick.AddListener (upgradeButtonClicked);
		deleteButton.onClick.AddListener (deleteButtonClicked);
		healButton.onClick.AddListener (healButtonClicked);
	}

	void Update ()
	{
		if (Input.GetAxis ("BuildTower1") != 0) 
		{
			selection = 1;
		}
		if (Input.GetAxis ("BuildTower2") != 0) 
		{
			selection = 2;
		}
		if (Input.GetAxis ("BuildTower3") != 0) 
		{
			selection = 3;
		}
		if (Input.GetAxis ("BuildWall") != 0) 
		{
			selection = 4;
		}
		if (Input.GetAxis ("NullSelect") != 0) {
			healButtonUI.SetActive (false);
			upgradeButtonObj.SetActive(false);
			deleteButtonObj.SetActive(false);
			diamondButtonUI.SetActive (false);
			selection = 0;
		}

		if (selection != 0) {
			healButtonUI.SetActive (false);
			upgradeButtonObj.SetActive(false);
			deleteButtonObj.SetActive(false);
			diamondButtonUI.SetActive (false);
			deleteOn = false;
			upgradeOn = false;
			healOn = false;
		} 

		if (Input.GetAxis ("DeleteTower") != 0 && !deleteButtonDown) 
		{
			deleteButtonDown = true;
			deleteOn = true;
			upgradeOn = false;
			healOn = false;
		}
		if (Input.GetAxis("UpgradeTower") !=0 && !upgradeButtonDown) 
		{
			upgradeButtonDown = true;
			deleteOn = false;
			upgradeOn = true;
			healOn = false;
		}
		if (Input.GetAxis("HealTower") !=0 && !healButtonDown) 
		{
			healButtonDown = true;
			deleteOn = false;
			upgradeOn = false;
			healOn = true;
		}

		if (Input.GetAxis ("DeleteTower") == 0) 
		{
			deleteButtonDown = false;
		}
		if (Input.GetAxis ("UpgradeTower") == 0) 
		{
			upgradeButtonDown = false;
		}

//		if (!Input.anyKey) 
//		{
//			cancelButtonDown = false;
//		}
//
	}



	void Tower1ButtonClick()
	{
		selection = 1;
	}
	void Tower2ButtonClick()
	{
		selection = 2;
	}
	void Tower3ButtonClick()
	{
		selection = 3;
	}
	void wallButtonClick()
	{
		selection = 4;
	}

	void cancelButtonClick()
	{
		selection = 0;
		diamondButtonUI.SetActive (false);
		upgradeButtonObj.SetActive(false);
		deleteButtonObj.SetActive(false);
		healButtonUI.SetActive (false);
	}

	void upgradeButtonClicked()
	{
		deleteOn = false;
		upgradeOn = true;
		healOn = false;
	}
	void deleteButtonClicked()
	{
		deleteOn = true;
		upgradeOn = false;
		healOn = false;
	}
	void healButtonClicked()
	{
		healOn = true;
		deleteOn = false;
		upgradeOn = false;
	}

	public int getSelection()
	{
		return selection;
	}

	public void setSelection(int changeTo)
	{
		selection = changeTo;
	}

	public bool allowDelete ()
	{
		return deleteOn;

	}
	public bool allowUpgrade ()
	{
		return upgradeOn;
	}
	public bool allowHeal()
	{
		return healOn;
	}
	public void dissAllowDelete()
	{
		deleteOn = false;
	}
	public void dissAllowUpgrade()
	{
		upgradeOn = false;
	}
	public void dissAllowHeal()
	{
		healOn = false;
	}
		
}
