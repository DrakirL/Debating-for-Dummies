using UnityEngine;
using System.Collections.Generic;

public class Player : Animator
{
	public int playersan;
	public int playerrep;
    public List<AudioClip> playerSounds;

    //Happens when the debate starts
    public void BeforeDebate()
    {
        if (PlayerPrefs.GetInt("Sanity") > 0 && PlayerPrefs.GetInt("Reputation") > 0)
        {
            SetupAnimator();

            playersan = PlayerPrefs.GetInt("Sanity");
            playerrep = PlayerPrefs.GetInt("Reputation");

            playersan += 20;

            if (playersan > 50)
            {
                playersan = 50;
            }

            playerrep += 15;

            if (playerrep > 100)
            {
                playerrep = 75;
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