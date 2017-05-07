using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[System.Serializable]
public class UserObj{
	public string user_name;
}

public class PostUserName : MonoBehaviour, IPointerClickHandler { 


	public Text userInput;
	public Text dialog;
	public GameObject play;
	public GameObject sign;
	public GameObject signOut;
	public GameObject userNameWindow;

	public IEnumerator PostUser (string user_name)
	{
		//		return www;
		Dictionary<string,string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "text/plain");
		headers.Add ("Authorization", "Bearer " + JwtGetter.jawt);
		UserObj post = new UserObj();
		post.user_name = user_name;
		byte[] json = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(post));
		string url = "https://radiant-savannah-12174.herokuapp.com/newuser";
		WWW www = new WWW(url, json, headers);
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.data);
			if (www.data == "Bad") {
				dialog.text = "Username is taken";
			} else {
				JwtGetter.username = www.data;
				signOut.SetActive (true);
				play.SetActive (true);
				userNameWindow.SetActive (false);
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
	public void UserPost( string user_name ) {
		StartCoroutine (PostUser(user_name));

	}

	public void OnPointerClick(PointerEventData PointerEventData)
	{
		UserPost (userInput.text);
	}
}