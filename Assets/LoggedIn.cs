using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggedIn : MonoBehaviour {
	public GameObject signOutButton;
	public GameObject signInButton;
	public void loginCheck(string arg) {
		if (arg == "true") 
		{
			signOutButton.SetActive (true);
			signInButton.SetActive (false);

		}
	}


}
