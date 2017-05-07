using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class HighScoreSubmission : MonoBehaviour { 

	public GameObject gameOverScreen;
	public GameObject env;

	private AudioSource music;

	public void Awake()
	{
		music = env.GetComponent<AudioSource>();
	}

	public IEnumerator WaitForPost(PlayerInfoPost stats)
	{
		//      return www;
		Dictionary<string,string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");
		headers.Add ("Authorization", "Bearer " + JwtGetter.jawt);
		byte[] json = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(stats));
		string url = "https://radiant-savannah-12174.herokuapp.com/";
		WWW www = new WWW(url, json, headers);
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
			music.Stop ();
			gameOverScreen.SetActive (true);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
	public void HighScoreSubmit( PlayerInfoPost stats) {
		if (JwtGetter.hasToken == true) {
			StartCoroutine (WaitForPost (stats));
		} else {
			music.Stop ();
			gameOverScreen.SetActive (true);
		}
	}
}