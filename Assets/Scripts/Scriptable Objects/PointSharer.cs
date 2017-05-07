using System.Collections;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "SharedPoint", menuName = "UsefulUtilities/PointSharer", order = 1)]
public class PointSharer : ScriptableObject {
	public string objectName = "Shared Point";
	public Vector3 point;
}
