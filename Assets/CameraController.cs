using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float sensitivity = 5f;
	public float smoothTime = 0.3f;
	Vector3 velocity = Vector3.zero;
	public GameObject Lights;


//	public Vector3


	void Update()
	{
		
		if(Input.GetAxis ("yAxisMove") > 0)
		{
			//			
			if (transform.position.z < 45f) {
				float newZ = transform.position.z + sensitivity * Input.GetAxis ("yAxisMove");
				Vector3 updatedPosition = new Vector3 (transform.position.x, transform.position.y, newZ);
//				transform.position = updatedPosition;
				transform.position = Vector3.SmoothDamp(transform.position, updatedPosition, ref velocity, smoothTime);


			} 
		}
		if(Input.GetAxis ("yAxisMove") < 0)
		{
			
			if (transform.position.z > -56f) {
				float newZ = transform.position.z + sensitivity * Input.GetAxis ("yAxisMove");
				Vector3 updatedPosition = new Vector3 (transform.position.x, transform.position.y, newZ);
//				transform.position = updatedPosition;
				transform.position = Vector3.SmoothDamp(transform.position, updatedPosition, ref velocity, smoothTime);
			} 
		}
//		if(Input.GetAxis ("xAxisMove") > 0)
//		{
//			if (transform.position.x < 50f) {
//				float newX = transform.position.x + sensitivity * Input.GetAxis ("xAxisMove");
//				Vector3 updatedPosition = new Vector3 (newX, transform.position.y, transform.position.z);
////				transform.position = updatedPosition;
//				transform.position = Vector3.SmoothDamp(transform.position, updatedPosition, ref velocity, smoothTime);
//			} 
//		}
//		if(Input.GetAxis ("xAxisMove") < 0)
//		{
//			if (transform.position.x > -50f) {
//				float newX = transform.position.x + sensitivity * Input.GetAxis ("xAxisMove");
//				Vector3 updatedPosition = new Vector3 (newX, transform.position.y, transform.position.z);
////				transform.position = updatedPosition;
//				transform.position = Vector3.SmoothDamp(transform.position, updatedPosition, ref velocity, smoothTime);
//			} 
//		}
	}
}
