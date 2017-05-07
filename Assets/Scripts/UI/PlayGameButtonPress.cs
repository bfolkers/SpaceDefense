using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayGameButtonPress : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler 

{
	public GameObject signOutButton;
	public GameObject signInButton;

	public void Awake() 
	{
		signOutButton.SetActive(false);
	}



	public void OnPointerClick(PointerEventData PointerEventData) 
	{
		Application.ExternalEval ("logIn()");
	}

	public void OnPointerEnter(PointerEventData PointerEventData)
	{

	}

	public void OnPointerExit(PointerEventData PointerEventData)
	{
	}

	public void OnPointerDown(PointerEventData PointerEventData) {
	}



}
