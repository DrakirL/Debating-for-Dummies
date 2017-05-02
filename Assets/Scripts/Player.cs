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
        SetupAnimator();

        if (PlayerPrefs.GetInt("Sanity") > 0 && PlayerPrefs.GetInt("Reputation") > 0)
        {
            playersan = PlayerPrefs.GetInt("Sanity");
            playerrep = PlayerPrefs.GetInt("Reputation");

            playersan += 20;

            if (playersan > 75)
            {
                playersan = 75;
            }

            playerrep += 15;

            if (playerrep > 75)
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

    public void StopAnimation()
    {
        animations[chosenAnimation].shouldLoop = false;
    }
}