using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FollowMouseAndClick : MonoBehaviour {

//	public TowerSnapLocationsClass snapLocation;

	public ButtonUI buttonUI;

	Vector3[] allGridPoints;

	public Button tower1ButtonUI;
	public Button tower2ButtonUI;
	public Button tower3ButtonUI;

	Button tower1Button;
	Button tower2Button;
	Button tower3Button;


	public GameObject tower1;
	public GameObject tower2;
	public GameObject tower3;
	public GameObject wall;

	public GameObject tower1Trans;
	public GameObject tower2Trans;
	public GameObject tower3Trans;
	public GameObject wallTrans;

	public float cooldown = 3;

	public int currentSelection = 0;
	public int storedCurrentSelection = 0;

	Vector3 snapToPoint;
	GameObject towerToPlace;
	GameObject towerToPlaceTrans;

	Quaternion newTowerRotation = Quaternion.Euler(new Vector3(0, 0, 0));

	Material[] materials;
	Material[] materialCheck;
	Color colorEnd;
	Color colorStart;

	Rigidbody rig;

	bool towerSelected = false;
	bool insideArea = true;

	bool[] towerPermissions = new bool[5];


	Plane ground;

	 
	bool allowCreation = true;

	public void OnMouseEnter()
	{
		insideArea = true;
//		currentSelection = storedCurrentSelection;
//		placeSelection ();

	}

//	public void OnMouseOver()
//	{
//	}

	public void OnMouseExit()
	{
		insideArea = false;
	}

	void Start () 
	{
//		CreateGridPoints.startingPoint = transform.position;
//		tower1Button = tower1ButtonUI.GetComponent<Button>();
//		tower2Button = tower2ButtonUI.GetComponent<Button>();
//		tower3Button = tower3ButtonUI.GetComponent<Button>();

//		tower1Button.onClick.AddListener (Tower1ButtonClick);
//		tower2Button.onClick.AddListener (Tower2ButtonClick);
//		tower3Button.onClick.AddListener (Tower3ButtonClick);

		allGridPoints = CreateGrid(30, 30);

		towerPermissions [0] = true;
		towerPermissions [1] = true;
		towerPermissions [2] = true;
		towerPermissions [3] = true;

		towerToPlace = null;

		ground = new Plane(Vector3.up, new Vector3(1, 0, 0));
	}
		
//
//	void Tower1ButtonClick()
//	{
//		currentSelection = 1;
//	}
//	void Tower2ButtonClick()
//	{
//		currentSelection = 2;
//	}
//	void Tower3ButtonClick()
//	{
//		currentSelection = 3;
//	}




	void Update () 
	{
		changeSelection (); 
		if (towerSelected) 
		{
			if (allowCreation) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				float distance;
				if (ground.Raycast (ray, out distance)) {
					Vector3 hitPoint = ray.GetPoint (distance);
					float xSnap = 0.0f;
					float ySnap = 0.0f;
					float distanceX = Mathf.Infinity;
					float distanceY = Mathf.Infinity;
//					foreach (Vector3 snapLoc in snapLocation.tileLocations) {
					foreach (Vector3 snapLoc in allGridPoints) 
					{
						float curDistanceX = Mathf.Abs (snapLoc.x - hitPoint.x);
						float curDistanceY = Mathf.Abs (snapLoc.z - hitPoint.z);
						if (curDistanceX < distanceX) 
						{
							distanceX = curDistanceX;
							xSnap = snapLoc.x;
						}
						if (curDistanceY < distanceY) 
						{
							distanceY = curDistanceY;
							ySnap = snapLoc.z;
						}
					}

					snapToPoint = new Vector3 (xSnap, 0.0f, ySnap);
					rig = towerToPlaceTrans.GetComponent <Rigidbody> ();
					rig.MovePosition (snapToPoint);
				}
			}

			if (Input.GetMouseButton (0)) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				float distance;
				if (ground.Raycast (ray, out distance)) 
				{
					makeVisible (towerToPlace, snapToPoint);
					towerToPlace = null;
					for (int i=0; i<towerPermissions.Length; i++) 
					{
						towerPermissions[i] = true;
					}
					towerSelected = false;
				}
			}
		}
	}

	Vector3[] CreateGrid(int xDimension=4, int yDimension=4, float tileWidth=2.5f, float tileHeight=2.5f)
	{
		Vector3[] gridArray = new Vector3[xDimension * yDimension];
		int index = 0;
		for (int i=0; i<xDimension; i++)
		{
			for (int j=0; j<yDimension; j++)
			{
				gridArray[index] = new Vector3(tileWidth*i + transform.position.x, 0.0f + transform.position.y, tileHeight*j + transform.position.z);
				index += 1;
			}
		}
		return gridArray;
	}

	void Test()
	{
	}

	void Spawn() 
	{	

		if (allowCreation) 
		{
			allowCreation = false;
			StartCoroutine(resetCreate ());
		}
	}
	IEnumerator resetCreate()
	{
		yield return new WaitForSeconds(cooldown);
		allowCreation = true;
	}
		

	void makeVisible (GameObject tower, Vector3 point)
	{
//		foreach (Vector3 snapLoc in allGridPoints) 
//		{
//			float curDistanceX = Mathf.Abs (snapLoc.x - hitPoint.x);
//			float curDistanceY = Mathf.Abs (snapLoc.z - hitPoint.z);
//			if (curDistanceX < distanceX) 
//			{
//				distanceX = curDistanceX;
//				xSnap = snapLoc.x;
//			}
//			if (curDistanceY < distanceY) 
//			{
//				distanceY = curDistanceY;
//				ySnap = snapLoc.z;
//			}
//		}
		Object.Destroy(towerToPlaceTrans);
		towerToPlace = Instantiate (tower, point, newTowerRotation);
		currentSelection = 0;
		storedCurrentSelection = 0;
	}
	void prePlacementCreation (GameObject tower) {
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float distance;
		if (ground.Raycast (ray, out distance)) {
			Vector3 startPoint = ray.GetPoint (distance);
			towerToPlaceTrans = Instantiate (tower, startPoint, newTowerRotation);
		}
	}

	void changeSelection()
	{
		if (Input.GetAxis ("BuildTower1") != 0) 
		{
			currentSelection = 1;
		}
		if (Input.GetAxis ("BuildTower2") != 0) 
		{
			currentSelection = 2;
		}
		if (Input.GetAxis ("BuildTower3") != 0) 
		{
			currentSelection = 3;
		}
		if (Input.GetAxis ("BuildWall") != 0) 
		{
			currentSelection = 4;
		}
		if (Input.GetAxis ("NullSelect") != 0) 
		{
			currentSelection = 0;
		}
		placeSelection ();
	}

	void placeSelection () 
	{
		if (insideArea) {
			
			if (currentSelection == 1 && towerPermissions [1]) {
				if (towerToPlace != null) {
					Object.Destroy (towerToPlace);
				}
				if (towerToPlaceTrans != null) {
					Object.Destroy (towerToPlaceTrans);
				}
				towerToPlace = tower1;
				prePlacementCreation (tower1Trans);
				for (int i = 0; i < towerPermissions.Length; i++) {
					towerPermissions [i] = true;
				}
				towerPermissions [1] = false;

				towerSelected = true;
			}
			if (currentSelection == 1 && towerPermissions [2]) {
				if (towerToPlace != null) {
					Object.Destroy (towerToPlace);
				}
				if (towerToPlaceTrans != null) {
					Object.Destroy (towerToPlaceTrans);
				}
				towerToPlace = tower2;
				prePlacementCreation (tower2Trans);
				for (int i = 0; i < towerPermissions.Length; i++) {
					towerPermissions [i] = true;
				}

				towerPermissions [2] = false;
				towerSelected = true;
			}
			if (currentSelection == 3 && towerPermissions [3]) {
				if (towerToPlace != null) {
					Object.Destroy (towerToPlace);
				}
				if (towerToPlaceTrans != null) {
					Object.Destroy (towerToPlaceTrans);
				}
				towerToPlace = tower3;
				prePlacementCreation (tower3Trans);
				for (int i = 0; i < towerPermissions.Length; i++) {
					towerPermissions [i] = true;
				}

				towerPermissions [3] = false;
				towerSelected = true;
				
			}
			if (currentSelection == 4 && towerPermissions [4]) {
				if (towerToPlace != null) {
					Object.Destroy (towerToPlace);
				}
				if (towerToPlaceTrans != null) {
					Object.Destroy (towerToPlaceTrans);
				}
				towerToPlace = wall;
				prePlacementCreation (wallTrans);
				for (int i = 0; i < towerPermissions.Length; i++) {
					towerPermissions [i] = true;
				}

				towerPermissions [4] = false;
				towerSelected = true;

			}
			if (currentSelection == 0 && towerPermissions [0]) {
				if (towerToPlace != null) {
					Object.Destroy (towerToPlace);
				}
				if (towerToPlaceTrans != null) {
					Object.Destroy (towerToPlaceTrans);
				}
				for (int i = 0; i < towerPermissions.Length; i++) {
					towerPermissions [i] = true;
				}
				towerSelected = false;
			}
		} else {
			
		}
	}
}
