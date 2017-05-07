using System.Collections;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "TileLocationData", menuName = "TowerCreation/TileLocationData", order = 1)]
public class TowerSnapLocationsClass : ScriptableObject {
	public string objectName = "Tower Snap Locations";
	public Vector3[] snapLocations;
	public Vector3[] tileLocations;
	public Vector3 tileDimensions;
	public Quaternion tileRotation;
}
