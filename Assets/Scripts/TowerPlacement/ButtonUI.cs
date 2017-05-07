using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour {
	public Button tower1ButtonUI;
	public Button tower2ButtonUI;
	public Button tower3ButtonUI;

	Button tower1Button;
	Button tower2Button;
	Button tower3Button;

	int selection = 0;

	void Start()
	{
		tower1Button = tower1ButtonUI.GetComponent<Button>();
		tower2Button = tower2ButtonUI.GetComponent<Button>();
		tower3Button = tower3ButtonUI.GetComponent<Button>();

		tower1Button.onClick.AddListener (Tower1ButtonClick);
		tower2Button.onClick.AddListener (Tower2ButtonClick);
		tower3Button.onClick.AddListener (Tower3ButtonClick);
	}

	public int getSelection() {
		return selection;
	}

	void Tower1ButtonClick()
	{
		selection = 1;
	}
	void Tower2ButtonClick()
	{
		selection = 2;
	}
	void Tower3ButtonClick()
	{
		selection = 3;
	}
}
