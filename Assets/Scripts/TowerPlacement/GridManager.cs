using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	public GameObject xzEndpoint;
	public int xDimension = 4;
	public int zDimension = 4;

	float xEnd;
	float zEnd;
	float xStart;
	float zStart;

	float xLength;
	float zLength;

	Vector3[] gridPoints;

	Plane ground;



	void Start () {
		gridPoints = new Vector3[xDimension * zDimension];
		Vector3 endpoint = xzEndpoint.transform.position;
		xEnd = endpoint.x;
		zEnd = endpoint.z;
		xStart = transform.position.x;
		zStart = transform.position.z;

		float xLengthTotal = Mathf.Abs (xStart - xEnd);
		float zLengthTotal = Mathf.Abs (zStart - zEnd);
		xLength = xLengthTotal / xDimension;
		zLength = zLengthTotal / zDimension;

		ground = new Plane(Vector3.up, new Vector3(1, 0, 0));

		CreateGrid ();
	}

	void CreateGrid()
	{
		int index = 0;
		for (int i = 0; i < xDimension; i++) 
		{
			for (int j = 0; j < zDimension; j++) 
			{
				float xPoint = xStart + i * xLength + xLength/2;
				float zPoint = zStart + j * zLength + zLength/2;
				gridPoints [index] = new Vector3 (xPoint, transform.position.y, zPoint);
				index += 1;
			}
		}
	}

	public Vector3[] getGrid()
	{
		return gridPoints;
	}

	public Vector3 findNearestGridPoint()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float distance;
		if (ground.Raycast (ray, out distance)) {
			Vector3 hitPoint = ray.GetPoint (distance);

			float xSnap = 0.0f;
			float ySnap = 0.0f;
			float distanceX = Mathf.Infinity;
			float distanceY = Mathf.Infinity;

			foreach (Vector3 point in gridPoints) {
				float curDistanceX = Mathf.Abs (point.x - hitPoint.x);
				float curDistanceY = Mathf.Abs (point.z - hitPoint.z);
				if (curDistanceX < distanceX) {
					distanceX = curDistanceX;
					xSnap = point.x;
				}
				if (curDistanceY < distanceY) {
					distanceY = curDistanceY;
					ySnap = point.z;
				}
			}

			Vector3 nearestPoint = new Vector3 (xSnap, 0.0f, ySnap);
			return nearestPoint;
		}
		return new Vector3 (Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
	}

	public bool insideGrid()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float distance;
		if (ground.Raycast (ray, out distance)) {
			Vector3 hitPoint = ray.GetPoint (distance);
			if (hitPoint.x > xEnd || hitPoint.z > zEnd || hitPoint.x < xStart || hitPoint.z < zStart) 
			{
				return false;
			}
			return true;
		}
		return false;
	}


}
