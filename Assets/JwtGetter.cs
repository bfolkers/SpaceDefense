using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JwtGetter : MonoBehaviour { 

	public static string username;
	public GameObject inputField;
	public static string jawt = "";
	public static bool hasToken = false;
	public Text inputText;
	public static int difficulty;
	public GameObject play;
	public GameObject sign;
	public GameObject signOut;

	public void Awake() {
		Scene currentScene = SceneManager.GetActiveScene ();
		string sceneName = currentScene.name;
		if (sceneName == "Login")
		inputField.SetActive (false);
	}


	public void Start(){
		Application.ExternalEval("sendJwt()");
		Application.ExternalEval("console.log('test')");
	}

	public void Get(){
		Application.ExternalEval("sendJwt()");
	}


	public void Check(){
		Application.ExternalEval("console.log('"+jawt+"')");
	}

	public void Got (string token){
		JwtGetter.jawt = token;
		JwtGetter.hasToken = true;
	}

	public void checkUserName()
	{
		if (JwtGetter.hasToken == true) {
			StartCoroutine (getUserName ());
		}
	}


	public IEnumerator getUserName()
	{
		//		return www;
		string url = "https://radiant-savannah-12174.herokuapp.com/is_user";
		Dictionary<string,string> headers = new Dictionary<string, string>();
		headers.Add ("Authorization", "Bearer " + JwtGetter.jawt);
		WWW www = new WWW(url, null, headers);
		yield return www;
		// check for errors
		if (www.error == null)
		{
			if (www.data == "") {
			
				generateUserNameGetter ();

			} else {
				signOut.SetActive (true);
				sign.SetActive (false);
				username = www.data;

			}
		}  
	}

	public void generateUserNameGetter () {
		inputField.SetActive (true);
		play.SetActive (false);
		sign.SetActive (false);
		signOut.SetActive (false);
	}

}
