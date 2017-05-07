using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderDifficultySetting : MonoBehaviour {

	public int index = 0;
	public int difficultyLevel = 0;
	public GameObject textBox; 


	public Text difText;
	public Slider difLevel;
	public Image Fill;
	public AudioSource zap;
	public bool soundOn;


	// Use this for initialization
	void Awake () 
	{	
		soundOn = false;
		difText = textBox.GetComponent<Text> ();
		difLevel = GetComponent<Slider> ();
		zap = GetComponent<AudioSource> ();
		JwtGetter.difficulty = index;
	}
		

	void Update () 

	{
		if (difLevel.value != index) {
			soundOn = true;
			index = (int) difLevel.value;
			JwtGetter.difficulty = index;
		}
		if (soundOn == true) {
			if (difLevel.value == 0f) {
				difText.text = "Beginner";
				zap.Play (); 
				soundOn = false;
			}

			if (difLevel.value == 1f) {
				difText.text = "Novice";
				zap.Play (); 
				soundOn = false;
			}

			if (difLevel.value == 2f) {
				difText.text = "Soldier";
				zap.Play (); 
				soundOn = false;
			}

			if (difLevel.value == 3f) {
				difText.text = "Master";
				zap.Play (); 
				soundOn = false;
			}

			if (difLevel.value == 4f) {
				difText.text = "Overlord";
				zap.Play (); 
				soundOn = false;
			}

			Fill.color = Color.Lerp (Color.green, Color.red, (difLevel.value / 4));
		}  
//			
//

	}
		
}
