using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Animation
{
    public Sprite[] sprites;
    public float fps;
    public bool shouldLoop;
}

public class Animator : MonoBehaviour
{
    public Animation[] animations;
    protected int chosenAnimation;
    protected int currentSprite;

    protected bool paused;

    protected SpriteRenderer spriterend;
    protected Image image;

    public void PlayAnimation (int whatAnimation, bool shouldReplayCurrent)
    {
        paused = false;

        if (whatAnimation != chosenAnimation || shouldReplayCurrent == true)
        {
            //Start the chosen animation.
            currentSprite = 0;
            chosenAnimation = whatAnimation;
        }
    }

    protected void SetupAnimator()
    {
        //SetUp
        spriterend = GetComponent<SpriteRenderer>();
        if (spriterend == null)
        { 
            image = GetComponent<Image>();
        }

        StartCoroutine(Animate());
    }

    protected int finalFrame()
    {
        //Returns the index number for the final frame of the currently playing animation
        return animations[chosenAnimation].sprites.Length - 1;
    }

    public IEnumerator Animate()
    {
        while (true)
        {
            if (paused == false)
            {
                //Change sprite to the new chosen sprite
                if (spriterend != null)
                {
                    spriterend.sprite = animations[chosenAnimation].sprites[currentSprite];
                }
                else
                {
                    image.sprite = animations[chosenAnimation].sprites[currentSprite];
                }

                //If this is the last frame...
                if (finalFrame() == currentSprite)
                {
                    //...Restart the animation if it should loop
                    if (animations[chosenAnimation].shouldLoop == true)
                    {
                        currentSprite = 0;
                    }
                }
                
                //If this isn't the last frame...
                else
                {
                    //...Prepare the next frame
                    currentSprite++;
                }
            }
            //Wait before repeating
            yield return new WaitForSeconds(1.0f / animations[chosenAnimation].fps);
        }
    }
}