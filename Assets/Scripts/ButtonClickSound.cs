using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClickSound : MonoBehaviour, IPointerClickHandler {

	public AudioSource click;

	public void Awake ()
	{
		click = GetComponent<AudioSource> ();
	}

	public void OnPointerClick(PointerEventData PointerEventData) 
	{
		click.Play ();
	}

}
