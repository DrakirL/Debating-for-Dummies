using UnityEngine;
using System.Collections.Generic;

public class Player : Animator
{
	public int playersan;
	public int playerrep;
	public int playerrepchange;
    public List<AudioClip> playerSounds;

    //Happens when the debate starts
    public void BeforeDebate()
    {
        if (PlayerPrefs.GetInt("Sanity") > 0 && PlayerPrefs.GetInt("Reputation") > 0)
        {
            SetupAnimator();

            playersan = PlayerPrefs.GetInt("Sanity");
            playerrep = PlayerPrefs.GetInt("Reputation");

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
        }

        else
        {
            playerrep = 50;
            playersan = 50;
        }
    }

	//Happens after the debate
	public void AfterDebate ()
	{
		//saves the game
		PlayerPrefs.SetInt ("Sanity", playersan);
		PlayerPrefs.SetInt ("Reputation", playerrep);
	}
}