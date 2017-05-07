using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opacity : MonoBehaviour {

	public float translucency = 0.1f;

	Material[] materials;
	Material[] materialCheck;
	Color colorEnd;
	Color colorStart;


	void Start () {
		materials = GetComponentInChildren <MeshRenderer> ().materials;

		for (int i = 0; i < materials.Length; i++) 
		{
			colorStart = materials [i].color;
			colorEnd = new Color (colorStart.r, colorStart.g, colorStart.b, translucency);
			materials [i].color = colorEnd;
		}
	}
	

	void Update () {

	}
}
