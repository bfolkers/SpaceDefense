using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuatlooManager : MonoBehaviour
{
	private static int balance;        // The player's current balance


	Text text;                         // Reference to the Quatloo Text component.
	public GameObject scoreText;

	
	private static int diamonds;
	Text diamondsText;
	public GameObject diamondText;


	void Awake ()
	{
		balance = 250;
		text = scoreText.GetComponent <Text> ();

		diamonds = 1;
		diamondsText = diamondText.GetComponent<Text>();
	}


	void Start ()
	{
		text.text = "Quatloos:" + balance.ToString();
		diamondsText.text = "D:" + diamonds.ToString ();
	}
		

	public int GetBalance()
	{
		return balance;
	}



	public void Deposit(int amount)
	{
		
		balance += amount;
	}
		

	public void Withdraw(int amount)
	{
		if (balance >= amount) 
		{
			
			balance -= amount;
		}
	}
	void Update()
	{
		text.text = "ø∫∫   " + balance.ToString() ;
		diamondsText.text = "D   " + diamonds.ToString ();
	}


	public int GetDiamondBalance()
	{
		return diamonds;
	}

	public void depositDiamonds (int amount)
	{
		diamonds += amount;
	}

	public void withdrawDiamonds(int amount)
	{
		diamonds -= amount;
	}

}