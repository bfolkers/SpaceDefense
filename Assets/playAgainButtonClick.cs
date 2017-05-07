using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playAgainButtonClick : MonoBehaviour, IPointerClickHandler {

	public void OnPointerClick(PointerEventData PointerEventData) 
	{
		Application.LoadLevel("MainMenu");
	}
}
