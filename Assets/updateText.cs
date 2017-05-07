using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateText : MonoBehaviour {

	public GameObject waveText;
	public Text displayText;
	public Text inputText;

	public void Awake() {
	
		displayText = GetComponent<Text> ();
		inputText = waveText.GetComponent<Text> ();
	}


	public void Update() {
	
//		displayText.text = inputText.text;
	}
}
