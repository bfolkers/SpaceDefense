using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTiles : MonoBehaviour {
	
	public TowerSnapLocationsClass snapLocation;


	public GameObject plane_tile;
	public int xDimension = 4;
	public int yDimension = 4;
	public float tileSizeX=2.5f;
	public float tileSizeY=2.5f;

//	public ScriptableObject arrayData;

	Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));

	Vector3 tileDimensions;
	float tileY;
	float tileX;


	public Vector3 topRightCorner = new Vector3 (0.0f, 0.0f, 0.0f);

	void Start () {
		int numberOfTiles = xDimension * yDimension;
		Vector3[] snapLocationArray = new Vector3[numberOfTiles];
		Vector3[] tileLocationArray = new Vector3[numberOfTiles];



//		tileDimensions = plane_tile.GetComponent <MeshRenderer> ().bounds.size;
//		tileX = tileDimensions.x;
//		tileY = tileDimensions.z;
		tileX = tileSizeX;
		tileY = tileSizeY;

		snapLocation.tileDimensions = tileDimensions;
		snapLocation.tileRotation = rotation;

		int index = 0;
		for (int i = 0; i < xDimension; i++) 
		{
			for (int j = 0; j < yDimension; j++) 
			{
				float x = (float)i * tileX + transform.position.x;
				float y = (float)j * tileY + transform.position.y;

				float xSnap = x + tileX / 2f;
				float ySnap = y + tileY / 2f;

				Vector3 tileCoord = new Vector3 (x, 0.0f, y);
				Instantiate (plane_tile, tileCoord, rotation);
				tileLocationArray [index] = tileCoord;
				snapLocationArray [index] = new Vector3 (xSnap, 0.0f, ySnap);
				index += 1;
			}
				
		}

		snapLocation.snapLocations = snapLocationArray;
		snapLocation.tileLocations = tileLocationArray;

	}
}
