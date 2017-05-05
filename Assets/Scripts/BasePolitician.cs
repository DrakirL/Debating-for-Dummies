using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Quote
{
    public string quote;
    public int animation;
    public int voiceClip;

    public enum Actors {Me, Opponent, Chairman}
    public Actors actor;

    public enum Categories {Standard, Insult, Bragging}
    public Categories type;
    public enum Effects {NoFilter, Shake}
    public Effects effect;

    public int yourSanChange;
    public int yourRepChange;
    public int sanChange;
    public int repChange;
}

[System.Serializable]
public class Phrase
{
    public Quote smartQuote;
    public Quote dumbQuote;
    public int sanityLimit;
}

[System.Serializable]
public class Dialogue
{
    public Phrase[] Phrases;
    public Response[] Responses;
}

[System.Serializable]
public class Response
{
    public string name;
    public enum Accessability {Free, ResearchSubject, ResearchOpposition}
    public Accessability accessLevel;
    public Dialogue Path;
}

public class BasePolitician : Animator
{
    protected GameObject PlayerGO;
    protected Player player;
    protected GameObject ChairmanGO;
    protected Chairman chairman;
    protected GameObject TextboxGO;
    protected Textbox tBox;
    protected AudioSource speaker;

    protected GameObject CameraGO;
    protected CustomCamera camera;
    protected GameObject SliderGO;
    protected ResponseSlider slider;
    protected GameObject NextButton;

    protected GameObject StatbarGO;
    protected StatBar playerReputationBar;
    protected StatBar playerSanityBar;
    protected StatBar opposingReputationBar;
    protected StatBar opposingSanityBar;

    public List<AudioClip> sounds;
    public Dialogue OpeningStatements;
    public Dialogue Breakdown;
    public Dialogue PlayerBreakdown;

    public int sanity;
    public int reputation;

    public Dialogue currentDialogue;
    protected int currentPhrase;

    public virtual void Setup()
    {
        PlayerGO = GameObject.Find("Player");
        ChairmanGO = GameObject.Find("Chairman");
        TextboxGO = GameObject.Find("Canvas");
        CameraGO = GameObject.Find("Main Camera");
        SliderGO = GameObject.Find("Slider");
        NextButton = GameObject.Find("NextButton");

        player = PlayerGO.GetComponent<Player>();
        chairman = ChairmanGO.GetComponent<Chairman>();
        tBox = TextboxGO.GetComponent<Textbox>();
        camera = CameraGO.GetComponent<CustomCamera>();
        slider = SliderGO.GetComponent<ResponseSlider>();
        speaker = GetComponent<AudioSource>();

        StatbarGO = GameObject.Find("Player reputation");
        playerReputationBar = StatbarGO.GetComponent<StatBar>();
        StatbarGO = GameObject.Find("Player sanity");
        playerSanityBar = StatbarGO.GetComponent<StatBar>();
        StatbarGO = GameObject.Find("Opposing reputation");
        opposingReputationBar = StatbarGO.GetComponent<StatBar>();
        StatbarGO = GameObject.Find("Opposing sanity");
        opposingSanityBar = StatbarGO.GetComponent<StatBar>();

        SetupAnimator();

        SetStartStats();
        ChooseDialogue(OpeningStatements);
        ContinueDialogue();
    }
    public virtual void SetStartStats()
    {
        player.BeforeDebate();
        //fill in rest for decendant
    }

    //Sets the first quote of the chosen dialogue as the next one to be played and prepares response buttons
    public virtual void ChooseDialogue(Dialogue Dialog)
    {
        //Prepare next quote
        currentDialogue = Dialog;
        currentPhrase = 0;

        //Retracts slider and prepares new responses
        slider.Retract(this);
    }

    //Plays the next quote in the current dialogue and shows responses if this new one is the last.
    public virtual void ContinueDialogue()
    {
        NextButton.SetActive(false);
        currentPhrase++;

        if (currentPhrase - 1 >= currentDialogue.Phrases.Length)
        {
            if (currentDialogue.Responses.Length == 0)
            {
                //Ends debate if there are no responses at the end of this dialogue
                EndDebate();
            }

            else
            {
                //Pull out the response slider.
                slider.PullOut(this);
            }
        }

        else
        {
            PlayPhrase(currentDialogue.Phrases[currentPhrase - 1]);
        }
    }

    //Plays the correct quote of the chosen phrase, including animations, zooming, camera effects and the changes in sanity and reputation.
    public virtual void PlayPhrase(Phrase citat)
    {
        if (citat.smartQuote.actor == Quote.Actors.Opponent)
        {
            opposingQuote(citat);
        }

        else if (citat.smartQuote.actor == Quote.Actors.Me)
        {
            yourQuote(citat);
        }

        else if (citat.smartQuote.actor == Quote.Actors.Chairman)
        {
            chairmanQuote(citat);
        }
    }
    public virtual void opposingQuote(Phrase citat)
    {
        if (sanity >= citat.sanityLimit || currentDialogue == Breakdown)
        {
            ChangeStats(citat.smartQuote);
            tBox.Type(citat.smartQuote.quote, this);
            camera.Zoom(1, citat.smartQuote);

            animations[citat.smartQuote.animation].shouldLoop = true;
            PlayAnimation(citat.smartQuote.animation, true);
            speaker.PlayOneShot(sounds[citat.smartQuote.voiceClip]);
        }

        else
        {
            ChangeStats(citat.dumbQuote);
            tBox.Type(citat.dumbQuote.quote, this);
            camera.Zoom(1, citat.dumbQuote);

            animations[citat.dumbQuote.animation].shouldLoop = true;
            PlayAnimation(citat.dumbQuote.animation, true);
            speaker.PlayOneShot(sounds[citat.dumbQuote.voiceClip]);
        }
    }
    public virtual void yourQuote(Phrase citat)
    {
        if (player.playersan >= citat.sanityLimit || currentDialogue == PlayerBreakdown)
        {
            ChangeStats(citat.smartQuote);
            tBox.Type(citat.smartQuote.quote, this);
            camera.Zoom(0, citat.smartQuote);

            player.animations[citat.smartQuote.animation].shouldLoop = true;
            player.PlayAnimation(citat.smartQuote.animation, true);
            speaker.PlayOneShot(player.playerSounds[citat.smartQuote.voiceClip]);
        }

        else
        {
            ChangeStats(citat.dumbQuote);
            tBox.Type(citat.dumbQuote.quote, this);
            camera.Zoom(0, citat.dumbQuote);

            player.animations[citat.dumbQuote.animation].shouldLoop = true;
            player.PlayAnimation(citat.dumbQuote.animation, true);
            speaker.PlayOneShot(player.playerSounds[citat.dumbQuote.voiceClip]);
        }
    }
    public virtual void chairmanQuote(Phrase citat)
    {
        ChangeStats(citat.smartQuote);
        tBox.Type(citat.smartQuote.quote, this);
        camera.Zoom(2, citat.smartQuote);

        chairman.animations[citat.smartQuote.animation].shouldLoop = true;
        chairman.PlayAnimation(citat.smartQuote.animation, true);
        speaker.PlayOneShot(chairman.chairSounds[citat.smartQuote.voiceClip]);
    }

    //Prevents the animation from looping and readies "Next"-button.
    public virtual void StopTalking()
    {
        if (currentPhrase - 1 != currentDialogue.Phrases.Length)
        {
            NextButton.SetActive(true);
        }

        animations[chosenAnimation].shouldLoop = false;
        player.StopAnimation();
        chairman.StopAnimation();
    }

    public virtual void Respond(int whatPath)
    {
        if (currentDialogue.Responses[whatPath].Path != null)
        {
            ChooseDialogue(currentDialogue.Responses[whatPath].Path);
            ContinueDialogue();
        }
    }

    //Ends the debate
    public virtual void EndDebate()
    {
        player.AfterDebate();
        SceneManager.LoadScene("Research");
    }

    //Alters both parties sanity and reputation depending on different factors
    public virtual void ChangeStats(Quote quote)
    {
        if (currentDialogue != Breakdown && currentDialogue != PlayerBreakdown)
        {
            //Standard simply adds/subtracts the given values.
            if (quote.type == Quote.Categories.Standard)
            {
                reputation += quote.repChange;
                sanity += quote.sanChange;

                player.playerrep += quote.yourRepChange;
                player.playersan += quote.yourSanChange;
            }

            //Insults cause a bit of base damage on san and rep, as well as deal extra repdamage if the assailant has higher rep
            else if (quote.type == Quote.Categories.Insult)
            {
                insultChange(quote);
            }

            //Bragging increases one's reputation by 10% and reduces the opposing side's sanity by the same amount
            else if (quote.type == Quote.Categories.Bragging)
            {
                braggingChange(quote);
            }

            //Makes sure no stat is above 100
            if (reputation > 100)
            {
                reputation = 100;
            }
            if (sanity > 100)
            {
                sanity = 100;
            }
            if (player.playerrep > 100)
            {
                player.playerrep = 100;
            }
            if (player.playersan > 100)
            {
                player.playersan = 100;
            }

            //Enables breakdown if any stat is 0 or lower
            if (reputation <= 0 || sanity <= 0)
            {
                //enable breakdown
                speaker.Stop();
                ChooseDialogue(Breakdown);
            }

            else if (player.playerrep <= 0 || player.playersan <= 0)
            {
                //enable player breakdown
                speaker.Stop();
                ChooseDialogue(PlayerBreakdown);
            }

            //Updates all statbars
            playerReputationBar.SetValue(player.playerrep);
            playerSanityBar.SetValue(player.playersan);
            opposingReputationBar.SetValue(reputation);
            opposingSanityBar.SetValue(sanity);
        }
    }
    public virtual void insultChange(Quote quote)
    {
        if (quote.actor == Quote.Actors.Me)
        {
            sanity -= quote.sanChange;
            reputation -= quote.repChange;

            if (player.playerrep > reputation)
            {
                reputation -= (player.playerrep - reputation) / 2;
            }
        }

        else
        {
            player.playersan -= quote.sanChange;
            player.playerrep -= quote.repChange;

            if (reputation > player.playerrep)
            {
                player.playerrep -= (reputation - player.playerrep) / 2;
            }
        }
    }
    public virtual void braggingChange(Quote quote)
    {
        if (quote.actor == Quote.Actors.Me)
        {
            player.playersan += quote.yourSanChange;
            sanity -= player.playerrep / 10;
            player.playerrep += player.playerrep / 10;
        }

        else
        {
            sanity += quote.sanChange;
            player.playersan -= reputation / 10;
            reputation += reputation / 10;
        }
    }

    void Start()
    {
        Setup();
    }
}