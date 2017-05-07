using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse2 : MonoBehaviour {

	public TowerSnapLocationsClass snapLocation;
	//	public TowerToPlaceSelection selectedTower;

	public GameObject tower1;
	public GameObject tower2;
	public GameObject tower3;


	GameObject towerToPlace;

	//	GameObject currentTowerInPending;

	Quaternion newTowerRotation = Quaternion.Euler(new Vector3(0, 0, 0));


	public float translucency = 0.5f;

	Material[] materials;
	Material[] materialCheck;
	Rigidbody rig;
	Color colorEnd;
	Color colorStart;

	bool towerSelected = false;

	//	bool allowTower1 = true;
	//	bool allowTower2 = true;
	bool[] towerPermissions = new bool[4];



	public float cooldown = 3;
	Plane ground;
	bool allowCreation = true;


	void Start () 
	{
		towerPermissions [0] = true;
		towerPermissions [1] = true;
		towerPermissions [2] = true;
		towerPermissions [3] = true;

		towerToPlace = tower1;
		//		towerModels[0] = tower1;
		ground = new Plane(Vector3.up, new Vector3(1, 0, 0));
		//		makeTranslucent (towerToPlace);
	}


	void Update () 
	{
		changeSelection (); 
		if (towerSelected) {
			if (allowCreation) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				float distance;
				if (ground.Raycast (ray, out distance)) {
					Vector3 hitPoint = ray.GetPoint (distance);
					float xSnap = 0.0f;
					float ySnap = 0.0f;
					float distanceX = Mathf.Infinity;
					float distanceY = Mathf.Infinity;
					foreach (Vector3 snapLoc in snapLocation.tileLocations) {
						float curDistanceX = Mathf.Abs (snapLoc.x - hitPoint.x);
						float curDistanceY = Mathf.Abs (snapLoc.z - hitPoint.z);
						if (curDistanceX < distanceX) {
							distanceX = curDistanceX;
							xSnap = snapLoc.x;
						}
						if (curDistanceY < distanceY) {
							distanceY = curDistanceY;
							ySnap = snapLoc.z;
						}
					}

					Vector3 snapToPoint = new Vector3 (xSnap, 0.0f, ySnap);
					rig = towerToPlace.GetComponent <Rigidbody> ();
					rig.MovePosition (snapToPoint);
				}
			}

			if (Input.GetMouseButton (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				float distance;
				if (ground.Raycast (ray, out distance)) {
					makeVisible (towerToPlace);
					towerToPlace = null;
					for (int i=0; i<towerPermissions.Length; i++) 
					{
						towerPermissions[i] = true;
					}
					towerSelected = false;
					//					Spawn ();
				}
			}
		}
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



	void makeTranslucent (GameObject tower)
	{
		materials = tower.GetComponentInChildren <MeshRenderer> ().materials;

		for (int i = 0; i < materials.Length; i++) 
		{
			colorStart = materials [i].color;
			colorEnd = new Color (colorStart.r, colorStart.g, colorStart.b, translucency);
			materials [i].color = colorEnd;
		}
	}

	void makeVisible (GameObject tower)
	{
		materials = tower.GetComponentInChildren <MeshRenderer> ().materials;

		for (int i = 0; i < materials.Length; i++) 
		{
			colorStart = materials [i].color;
			colorEnd = new Color (colorStart.r, colorStart.g, colorStart.b, 1.0f);
			materials [i].color = colorEnd;
		}
	}
	void prePlacementCreation (GameObject tower) {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float distance;
		if (ground.Raycast (ray, out distance)) {
			Vector3 startPoint = ray.GetPoint (distance);
			towerToPlace = Instantiate (tower, startPoint, newTowerRotation);
			makeTranslucent (towerToPlace);
		}
	}

	void changeSelection () 
	{

		//		if (allowTrigger) 
		//		{
		if (Input.GetAxis ("BuildTower1") != 0 && towerPermissions[1]) {
			if (towerToPlace != null) {
				Object.Destroy (towerToPlace);
			}
			prePlacementCreation (tower1);
			for (int i=0; i<towerPermissions.Length; i++) 
			{
				towerPermissions[i] = true;
			}
			towerPermissions [1] = false;

			towerSelected = true;
		}
		if (Input.GetAxis ("BuildTower2") != 0 && towerPermissions[2]) 
		{
			if (towerToPlace != null) {
				Object.Destroy (towerToPlace);
			}
			prePlacementCreation (tower2);
			for (int i=0; i<towerPermissions.Length; i++) 
			{
				towerPermissions[i] = true;
			}

			towerPermissions [2] = false;
			towerSelected = true;
		}
		if (Input.GetAxis ("BuildTower3") != 0 && towerPermissions [3]) 
		{
			if (towerToPlace != null) {
				Object.Destroy (towerToPlace);
			}
			prePlacementCreation (tower3);
			for (int i=0; i<towerPermissions.Length; i++) 
			{
				towerPermissions[i] = true;
			}

			towerPermissions [3] = false;
			towerSelected = true;

		}
		if (Input.GetAxis ("NullSelect") != 0 && towerPermissions[0]) {
			if (towerToPlace != null) {
				Object.Destroy (towerToPlace);
			}
			for (int i=0; i<towerPermissions.Length; i++) 
			{
				towerPermissions[i] = true;
			}
			towerSelected = false;
		}

		//		}

	}
	//	IEnumerator stopDuplicates()
	//	{
	//		yield return new WaitForSeconds(0.2f);
	//		allowTrigger = true;
	//	}


}