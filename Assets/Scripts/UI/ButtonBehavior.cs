using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject textField;


	void Awake ()
	{
		textField.SetActive (false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		textField.SetActive (true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		textField.SetActive (false);
	}


}
