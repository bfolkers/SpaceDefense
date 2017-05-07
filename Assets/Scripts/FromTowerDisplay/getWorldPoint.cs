using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getWorldPoint : MonoBehaviour {
	public Camera camera;
	public PointSharer sharedPoint;
	Vector3 point;
	Plane ground;
	RectTransform imgPosition;


	void Start () {
		point = sharedPoint.point;
		ground = new Plane(Vector3.up, new Vector3(1, 0, 0));
		imgPosition = GetComponent <RectTransform> ();
//		imgPosition.anchorMax = camera.rect.max;
//		imgPosition.anchorMin = camera.rect.min;
	}
	

	void Update () {
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float distance;
			if (ground.Raycast (ray, out distance)) {
				Vector3 hitPoint = ray.GetPoint (distance);
				Vector3 newPoint = camera.ScreenToWorldPoint (hitPoint);
				Vector3 mousePos = Input.mousePosition;
				mousePos.y -= camera.rect.position.x;
				imgPosition.anchoredPosition3D = mousePos;
			}
		}
	}
}
