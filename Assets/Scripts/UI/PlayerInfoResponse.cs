using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfoResponse
{
	public List <PlayerInfo> data;
}

[System.Serializable]
public class PlayerInfo
{
	public int score;
	public int kills;
	public int difficulty;
	public int duration;
	public string name;
	public string date_created;

}
[System.Serializable]
public class PlayerInfoPost
{
	public int score;
	public int kills;
	public int difficulty;
	public int duration;

}