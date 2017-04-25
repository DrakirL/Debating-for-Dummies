using UnityEngine;
using System.Collections.Generic;

public class Chairman : Animator
{
    public List<AudioClip> chairSounds;

    void Start()
    {
        SetupAnimator();
    }

    public void StopAnimation()
    {
        animations[chosenAnimation].shouldLoop = false;
    }
}
