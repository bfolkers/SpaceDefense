using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerTransformData", menuName = "UsefulUtilities/TowerTransforms", order = 1)]
public class TowerTransformsSO : ScriptableObject {

	public static List<Transform> TowerTransforms;

	public static TowerTransformsSO Instance;

	void Awake() 
	{
		Instance = this;
	}
}
