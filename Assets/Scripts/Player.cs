using UnityEngine;
using System.Collections;

public class Player : Animator
{
	public int playersan;
	public int playerrep;
	public int playerrepchange;

	//Happens after the debate
	public void AfterDebate ()
	{
		//sets sanity to 50 if below 50
		if (playersan < 50)
		{
			playersan = 50;
		}

		//Increases reputation
		playerrep = playerrep + playerrepchange;
        if (playerrep > 100)
        {
            playerrep = 100;
        }

		//saves the game
		PlayerPrefs.SetInt ("Sanity", playersan);
		PlayerPrefs.SetInt ("Reputation", playerrep);
	}
}