using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class fadeUp : MonoBehaviour {

	public GameObject panel;
	public Image alpha;
	public Color panelColor;
	public Text text;
	public Color textColor;

	public void Awake() {
		text = GetComponent<Text> ();
		textColor = text.color;
		alpha = panel.GetComponent<Image>();
		panelColor = alpha.color;
		textColor.a = panelColor.a;
	}

	public void Update() {
		textColor.a = panelColor.a;
	}
}
