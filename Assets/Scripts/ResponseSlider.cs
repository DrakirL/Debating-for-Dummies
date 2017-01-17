using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResponseSlider : Animator
{
    public GameObject[] Buttons;
    public int wantedButtons;

    void Start()
    {
        SetupAnimator();
    }

    //Plays entering animation and activates all wanted buttons
    public void PullOut(BasePolitician sender)
    {
        //Play entering animation
        PlayAnimation(0, false);

        //Activates all wanted buttons
        StartCoroutine(SpawnButtons(sender));
    }

    IEnumerator SpawnButtons(BasePolitician politician)
    {
        yield return new WaitForSeconds(animations[0].sprites.Length / animations[0].fps);
        for (int i = 0; i < wantedButtons; i++)
        {
            if (politician.currentDialogue.Responses[i].accessLevel == Response.Accessability.Free)
            {
                Buttons[i].SetActive(true);
            }

            // Test if this button needs a reseach point to be open
            else if (PlayerPrefs.GetInt("SubjectOrOpposition") == 0 && politician.currentDialogue.Responses[i].accessLevel == Response.Accessability.ResearchSubject)
            {
                Buttons[i].SetActive(true);
            }

            else if (PlayerPrefs.GetInt("SubjectOrOpposition") == 1 && politician.currentDialogue.Responses[i].accessLevel == Response.Accessability.ResearchOpposition)
            {
                Buttons[i].SetActive(true);
            }
        }
    }


    public void Retract(BasePolitician politician)
    {
        //Plays retract animation
        PlayAnimation(1, false);

        //Sets wantedButtons and the buttons' names to the appropriate values
        wantedButtons = politician.currentDialogue.Responses.Length;
        if (wantedButtons > 4)
        {
            wantedButtons = 4;
        }

        for (int i = 0; i < wantedButtons; i++)
        {
           Buttons[i].GetComponentInChildren<Text>().text = politician.currentDialogue.Responses[i].name;
        }

        //Deactivates all buttons
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].SetActive(false);
        }
    }
}