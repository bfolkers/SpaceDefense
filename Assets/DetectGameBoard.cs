using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGameBoard : MonoBehaviour {
	bool insideArea;

	public void OnMouseEnter()
	{
		insideArea = true;
	}


	public void OnMouseExit()
	{
		insideArea = false;
	}

	public bool detectGameBoard()
	{
		return insideArea;
	}
}
