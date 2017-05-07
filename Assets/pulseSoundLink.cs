using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class pulseSoundLink : MonoBehaviour {

	public AudioSource pulse;
	public ParticleSystem parts;

	public void Awake() {
		pulse = GetComponent<AudioSource> ();
		parts = GetComponent<ParticleSystem> ();
		pulse.Play ();
	}

	public void Update(){
		if (!parts) {
			pulse.Stop ();
		} 
	}


}
