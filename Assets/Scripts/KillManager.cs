using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillManager : MonoBehaviour
{
	public static int kills;        // The player's score.

	void Awake ()
	{
		// Reset the score.
		kills = 0;
	}


	void Update ()
	{

	}

	public int GetKills() 
	{
		return kills;
	}

	public void AddKill()
	{
		++kills;
	}
}