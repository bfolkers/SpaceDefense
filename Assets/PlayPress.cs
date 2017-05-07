using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayPress : MonoBehaviour, IPointerClickHandler {

	public AudioSource click;

	private AudioSource[] allAudioSources;

	void StopAllAudio() {
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach( AudioSource audioS in allAudioSources) {
			audioS.Stop();
		}
	}

	public void Awake ()
	{
		click = GetComponent<AudioSource> ();
	}
		

	IEnumerator Example()
	{
		print(Time.time);
		yield return new WaitForSeconds(2);
		print(Time.time);
		Application.LoadLevel("LouMessin");
	}

	public void OnPointerClick(PointerEventData PointerEventData) 
	{
		StopAllAudio ();
		click.Play ();
		StartCoroutine(Example());
	}

}
