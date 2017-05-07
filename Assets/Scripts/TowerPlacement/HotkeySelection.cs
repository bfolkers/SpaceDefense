using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeySelection : MonoBehaviour {

	int selection = 0;

	void Start () 
	{
		
	}

	void Update ()
	{
		if (Input.GetAxis ("BuildTower1") != 0) 
		{
			selection = 1;
		}
		if (Input.GetAxis ("BuildTower2") != 0) 
		{
			selection = 2;
		}
		if (Input.GetAxis ("BuildTower3") != 0) 
		{
			selection = 3;
		}
		if (Input.GetAxis ("BuildWall") != 0) 
		{
			selection = 4;
		}
		if (Input.GetAxis ("NullSelect") != 0) 
		{
			selection = 0;
		}
	}

	public int getSelection()
	{
		return selection;
	}

}
