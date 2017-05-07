using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreGeneration : MonoBehaviour {

	public GameObject textName;
	public GameObject textLevel;
	public GameObject textScore;
	public GameObject parent;
	public Text textFieldName;
	public Text textFieldScore;
	public Text textFieldLevel;
	public Vector3 anchorName;
	public Vector3 anchorScore;
	public Vector3 anchorLevel;
	public Vector3 homePosition;

	private PlayerInfoResponse data;

	void Awake()
	{
		homePosition = parent.GetComponent<Transform> ().position;
		anchorLevel = homePosition;
		anchorName = homePosition;
		anchorScore = homePosition;
		textFieldName = textName.GetComponent<Text> ();
		textFieldScore = textScore.GetComponent<Text> ();
		textFieldLevel = textLevel.GetComponent<Text> ();
		StartCoroutine ("WaitForRequest");
	}

	public IEnumerator WaitForRequest()
	{
		//		return www;
		string url = "https://radiant-savannah-12174.herokuapp.com/";
		WWW www = new WWW(url);
		yield return www;
		// check for errors
		if (www.error == null)
		{
			data = JsonUtility.FromJson<PlayerInfoResponse>(www.data);
			renderHighScoreLevel ();
//			renderHighScoreScore ();
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	void renderHighScoreLevel()
	{
		anchorName.x -= 108f;
		anchorScore.x += 108f;
		for (var i = 0; i < 10; i++) {
			textFieldLevel.text = data.data [i].difficulty.ToString ();
			Instantiate (textFieldLevel, anchorLevel, Quaternion.Euler (0f, 0f, 0f), parent.transform);

			anchorLevel.y -= 25f; 
			textFieldScore.text = data.data [i].score.ToString ();
			Instantiate (textFieldScore, anchorScore, Quaternion.Euler (0f, 0f, 0f), parent.transform);

			anchorScore.y -= 25f; 
			textFieldName.text = data.data [i].name.ToString ();
			Instantiate (textFieldName, anchorName, Quaternion.Euler (0f, 0f, 0f), parent.transform);

			anchorName.y -= 25f;
		}
	}
}
