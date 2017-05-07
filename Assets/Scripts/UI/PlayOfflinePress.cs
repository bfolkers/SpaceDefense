using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayOfflinePress : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler 

{

	public void OnPointerClick(PointerEventData PointerEventData) 
	{
		SceneManager.LoadScene("MainMenu");
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
