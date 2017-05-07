using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {
	public void flipBool(float cooldownTime, bool boolToFlip)
	{
		StartCoroutine (flipBoolAfterTime (cooldownTime, boolToFlip));
	}
	IEnumerator flipBoolAfterTime(float cooldownTime, bool boolToFlip)
	{
		yield return new WaitForSeconds(cooldownTime);
		boolToFlip = !boolToFlip;
	}
}
