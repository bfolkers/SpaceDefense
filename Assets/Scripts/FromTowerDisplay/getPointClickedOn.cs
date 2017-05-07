using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getPointClickedOn : MonoBehaviour {
	Plane ground;
	void Start ()
	{
		ground = new Plane(Vector3.up, new Vector3(1, 0, 0));
	}
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float distance;
			if (ground.Raycast (ray, out distance)) {


			}
		}
	}

}
